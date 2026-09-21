using System.Collections;
using UnityEngine;

public class CanTarget : MonoBehaviour
{
    // =========================================================
    // TARGET POSITIONS
    // =========================================================

    [Header("Target Positions")]
    public Transform[] targetPositions;

    // =========================================================
    // RESET SETTINGS
    // =========================================================

    [Header("Reset Settings")]
    public float easyResetDelay = 2f;
    public float mediumResetDelay = 1.5f;
    public float hardResetDelay = 1f;

    // =========================================================
    // PRIVATE VARIABLES
    // =========================================================

    private bool hasBeenHit = false;

    private Rigidbody rb;

    private Quaternion startRotation;

    private int lastPositionIndex = -1;

    // =========================================================
    // START
    // =========================================================

    void Start()
    {
        rb =
            GetComponent<Rigidbody>();

        startRotation =
            transform.rotation;

        if (rb == null)
        {
            Debug.LogError(
                "CanTarget requires a Rigidbody!"
            );
        }
    }

    // =========================================================
    // COLLISION
    // =========================================================

    private void OnCollisionEnter(
        Collision collision
    )
    {
        // Prevent the can from being scored twice
        // before it resets.
        if (hasBeenHit)
        {
            return;
        }

        // Only slippers can hit the can.
        if (!collision.gameObject.CompareTag("Slipper"))
        {
            return;
        }

        // Get the slipper script.
        Slipper slipper =
            collision.gameObject.GetComponent<Slipper>();

        // The object must have a Slipper component.
        if (slipper == null)
        {
            Debug.LogWarning(
                "Object tagged Slipper does not have Slipper.cs!"
            );

            return;
        }

        // -----------------------------------------------------
        // VALIDATE THE HIT
        // -----------------------------------------------------

        // MarkAsHit returns TRUE only if the slipper
        // has not already been counted as a miss.
        bool validHit =
            slipper.MarkAsHit();

        // If the slipper already touched the ground,
        // do NOT award points.
        if (!validHit)
        {
            Debug.Log(
                "CAN CONTACT IGNORED - SLIPPER ALREADY PROCESSED"
            );

            return;
        }

        // This is now officially a valid hit.
        hasBeenHit = true;

        // -----------------------------------------------------
        // ADD SCORE
        // -----------------------------------------------------

        if (GameManager.Instance != null)
        {
            GameManager.Instance.AddScore(
                10
            );
        }

        Debug.Log(
            "CAN HIT!"
        );

        // -----------------------------------------------------
        // HIT SOUND
        // -----------------------------------------------------

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayCanHitSound();
        }

        // -----------------------------------------------------
        // RESET CAN
        // -----------------------------------------------------

        StartCoroutine(
            ResetCan()
        );
    }

    // =========================================================
    // RESET CAN
    // =========================================================

    private IEnumerator ResetCan()
    {
        float resetDelay =
            GetResetDelay();

        yield return new WaitForSeconds(
            resetDelay
        );

        // Stop the can before repositioning.
        if (rb != null)
        {
            rb.linearVelocity =
                Vector3.zero;

            rb.angularVelocity =
                Vector3.zero;
        }

        MoveToRandomPosition();

        // Restore upright rotation.
        transform.rotation =
            startRotation;

        // Allow another hit.
        hasBeenHit =
            false;

        Debug.Log(
            "CAN RESET COMPLETE"
        );
    }

    // =========================================================
    // GET RESET DELAY
    // =========================================================

    private float GetResetDelay()
    {
        if (GameManager.Instance == null)
        {
            return easyResetDelay;
        }

        switch (
            GameManager.Instance.GetDifficulty()
        )
        {
            case GameManager.Difficulty.Hard:

                return hardResetDelay;

            case GameManager.Difficulty.Medium:

                return mediumResetDelay;

            default:

                return easyResetDelay;
        }
    }

    // =========================================================
    // MOVE CAN
    // =========================================================

    private void MoveToRandomPosition()
    {
        if (targetPositions == null ||
            targetPositions.Length == 0)
        {
            Debug.LogWarning(
                "No Can target positions assigned!"
            );

            return;
        }

        int availablePositionCount =
            Mathf.Clamp(
                GetAvailablePositionCount(),
                1,
                targetPositions.Length
            );

        int positionIndex;

        // Avoid immediately selecting
        // the same position again.
        if (availablePositionCount > 1)
        {
            do
            {
                positionIndex =
                    Random.Range(
                        0,
                        availablePositionCount
                    );
            }
            while (
                positionIndex ==
                lastPositionIndex
            );
        }
        else
        {
            positionIndex = 0;
        }

        Transform newPosition =
            targetPositions[
                positionIndex
            ];

        if (newPosition == null)
        {
            Debug.LogWarning(
                "A Can target position is not assigned!"
            );

            return;
        }

        transform.position =
            newPosition.position;

        lastPositionIndex =
            positionIndex;

        Debug.Log(
            "Can moved to target position: " +
            (positionIndex + 1)
        );
    }

    // =========================================================
    // AVAILABLE POSITIONS BY DIFFICULTY
    // =========================================================

    private int GetAvailablePositionCount()
    {
        if (GameManager.Instance == null)
        {
            return 3;
        }

        switch (
            GameManager.Instance.GetDifficulty()
        )
        {
            case GameManager.Difficulty.Hard:

                return 5;

            case GameManager.Difficulty.Medium:

                return 4;

            default:

                return 3;
        }
    }
}