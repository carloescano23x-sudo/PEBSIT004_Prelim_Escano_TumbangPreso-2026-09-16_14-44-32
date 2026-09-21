using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SlipperThrower : MonoBehaviour
{
    // =========================================================
    // THROW SETUP
    // =========================================================

    [Header("Throw Setup")]
    public GameObject slipperPrefab;
    public Transform throwPoint;
    public Camera playerCamera;

    // =========================================================
    // THROW SETTINGS
    // =========================================================

    [Header("Throw Settings")]
    public float throwForce = 15f;
    public float throwCooldown = 2f;

    // Distance used when aiming through the center of the screen.
    public float aimDistance = 100f;

    // =========================================================
    // MOBILE UI
    // =========================================================

    [Header("Mobile UI")]
    public Button throwButton;

    // =========================================================
    // PRIVATE VARIABLES
    // =========================================================

    private bool canThrow = true;

    // =========================================================
    // UPDATE
    // =========================================================

    void Update()
    {
        // Make sure GameManager exists.
        if (GameManager.Instance == null)
        {
            return;
        }

        // Throwing is only allowed while Playing.
        if (GameManager.Instance.currentState !=
            GameManager.GameState.Playing)
        {
            return;
        }

        // -----------------------------------------------------
        // PC / UNITY EDITOR MOUSE INPUT
        // -----------------------------------------------------

#if UNITY_EDITOR || UNITY_STANDALONE

        if (Input.GetMouseButtonDown(0))
        {
            // Prevent UI clicks from also throwing a slipper.
            if (IsPointerOverUI())
            {
                Debug.Log(
                    "Mouse click ignored because pointer is over UI."
                );

                return;
            }

            TryThrow();
        }

#endif
    }

    // =========================================================
    // CHECK IF POINTER IS OVER UI
    // =========================================================

    private bool IsPointerOverUI()
    {
        if (EventSystem.current == null)
        {
            return false;
        }

        return EventSystem.current.IsPointerOverGameObject();
    }

    // =========================================================
    // MOBILE THROW BUTTON
    // =========================================================

    public void MobileThrow()
    {
        TryThrow();
    }

    // =========================================================
    // TRY THROW
    // =========================================================

    private void TryThrow()
    {
        // Make sure GameManager exists.
        if (GameManager.Instance == null)
        {
            return;
        }

        // Only throw during gameplay.
        if (GameManager.Instance.currentState !=
            GameManager.GameState.Playing)
        {
            return;
        }

        // Prevent another throw during cooldown.
        if (!canThrow)
        {
            return;
        }

        StartCoroutine(
            ThrowRoutine()
        );
    }

    // =========================================================
    // THROW ROUTINE
    // =========================================================

    private IEnumerator ThrowRoutine()
    {
        // Start cooldown.
        canThrow = false;

        // Disable mobile THROW button.
        if (throwButton != null)
        {
            throwButton.interactable = false;
        }

        // Create and launch slipper.
        ThrowSlipper();

        // Wait for cooldown.
        yield return new WaitForSeconds(
            throwCooldown
        );

        // Allow another throw.
        canThrow = true;

        // Enable mobile THROW button.
        if (throwButton != null)
        {
            throwButton.interactable = true;
        }
    }

    // =========================================================
    // CREATE AND THROW SLIPPER
    // =========================================================

    private void ThrowSlipper()
    {
        // -----------------------------------------------------
        // CHECK SLIPPER PREFAB
        // -----------------------------------------------------

        if (slipperPrefab == null)
        {
            Debug.LogError(
                "Slipper Prefab is missing!"
            );

            return;
        }

        // -----------------------------------------------------
        // CHECK THROW POINT
        // -----------------------------------------------------

        if (throwPoint == null)
        {
            Debug.LogError(
                "Throw Point is missing!"
            );

            return;
        }

        // -----------------------------------------------------
        // CHECK PLAYER CAMERA
        // -----------------------------------------------------

        if (playerCamera == null)
        {
            Debug.LogError(
                "Player Camera is missing!"
            );

            return;
        }

        // -----------------------------------------------------
        // CREATE SLIPPER
        // -----------------------------------------------------

        GameObject slipper =
            Instantiate(
                slipperPrefab,
                throwPoint.position,
                throwPoint.rotation
            );

        Debug.Log(
            "SLIPPER THROWN"
        );

        // -----------------------------------------------------
        // PLAY THROW SOUND
        // -----------------------------------------------------

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayThrowSound();
        }

        // -----------------------------------------------------
        // GET RIGIDBODY
        // -----------------------------------------------------

        Rigidbody rb =
            slipper.GetComponent<Rigidbody>();

        if (rb == null)
        {
            Debug.LogError(
                "Slipper prefab has no Rigidbody!"
            );

            Destroy(slipper);

            return;
        }

        // -----------------------------------------------------
        // AIM THROUGH CENTER OF SCREEN / CROSSHAIR
        // -----------------------------------------------------

        // 0.5, 0.5 is the exact center of the camera view.
        Ray aimRay =
            playerCamera.ViewportPointToRay(
                new Vector3(
                    0.5f,
                    0.5f,
                    0f
                )
            );

        Vector3 targetPoint;

        RaycastHit hit;

        // Check if the center of the screen is pointing
        // directly at an object.
        if (Physics.Raycast(
            aimRay,
            out hit,
            aimDistance,
            ~0,
            QueryTriggerInteraction.Ignore
        ))
        {
            // Aim directly at the point under the crosshair.
            targetPoint = hit.point;
        }
        else
        {
            // If the crosshair is pointing at the sky or
            // empty space, create a target far in front.
            targetPoint =
                aimRay.origin +
                aimRay.direction *
                aimDistance;
        }

        // -----------------------------------------------------
        // CALCULATE THROW DIRECTION
        // -----------------------------------------------------

        // The slipper starts at ThrowPoint, so calculate
        // the direction FROM ThrowPoint TO the point that
        // the crosshair is actually aiming at.
        Vector3 throwDirection =
            (
                targetPoint -
                throwPoint.position
            ).normalized;

        // -----------------------------------------------------
        // ROTATE SLIPPER TOWARD THROW DIRECTION
        // -----------------------------------------------------

        if (throwDirection != Vector3.zero)
        {
            slipper.transform.rotation =
                Quaternion.LookRotation(
                    throwDirection
                );
        }

        // -----------------------------------------------------
        // APPLY THROW FORCE
        // -----------------------------------------------------

        rb.AddForce(
            throwDirection *
            throwForce,
            ForceMode.Impulse
        );
    }

    // =========================================================
    // RESET THROW BUTTON
    // =========================================================

    public void ResetThrowButton()
    {
        canThrow = true;

        if (throwButton != null)
        {
            throwButton.interactable = true;
        }
    }
}