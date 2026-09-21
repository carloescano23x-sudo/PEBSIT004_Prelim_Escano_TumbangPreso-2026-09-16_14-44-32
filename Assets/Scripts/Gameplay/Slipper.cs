using UnityEngine;

public class Slipper : MonoBehaviour
{
    // =========================================================
    // SLIPPER SETTINGS
    // =========================================================

    [Header("Slipper Settings")]
    public float lifetime = 5f;

    // =========================================================
    // PRIVATE VARIABLES
    // =========================================================

    private bool resultProcessed = false;
    private bool successfulHit = false;

    // =========================================================
    // PUBLIC RESULT CHECK
    // =========================================================

    public bool ResultProcessed
    {
        get
        {
            return resultProcessed;
        }
    }

    public bool SuccessfulHit
    {
        get
        {
            return successfulHit;
        }
    }

    // =========================================================
    // START
    // =========================================================

    private void Start()
    {
        // Backup miss detection.
        Invoke(
            nameof(CheckResult),
            lifetime
        );
    }

    // =========================================================
    // COLLISION DETECTION
    // =========================================================

    private void OnCollisionEnter(
        Collision collision
    )
    {
        // Once this slipper already has a result,
        // it cannot create another result.
        if (resultProcessed)
        {
            return;
        }

        // Ground contact = immediate miss.
        if (collision.gameObject.CompareTag("Ground"))
        {
            RegisterMiss();
        }
    }

    // =========================================================
    // MARK AS HIT
    // =========================================================

    public bool MarkAsHit()
    {
        // If this slipper already missed or already hit,
        // the can must NOT award points.
        if (resultProcessed)
        {
            return false;
        }

        successfulHit = true;
        resultProcessed = true;

        CancelInvoke(
            nameof(CheckResult)
        );

        Debug.Log(
            "SLIPPER HIT REGISTERED"
        );

        // CanTarget owns the +10 score.
        Destroy(
            gameObject,
            1f
        );

        // Tell CanTarget this was a valid hit.
        return true;
    }

    // =========================================================
    // REGISTER MISS
    // =========================================================

    private void RegisterMiss()
    {
        if (resultProcessed)
        {
            return;
        }

        resultProcessed = true;
        successfulHit = false;

        CancelInvoke(
            nameof(CheckResult)
        );

        Debug.Log(
            "SLIPPER MISS REGISTERED"
        );

        // Play miss sound.
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayMissSound();
        }

        // Remove one life.
        if (GameManager.Instance != null)
        {
            GameManager.Instance.LoseLife();
        }
        else
        {
            Debug.LogError(
                "GameManager was not found!"
            );
        }

        // IMPORTANT:
        // Destroy immediately so a missed slipper
        // cannot bounce into the can afterward.
        Destroy(
            gameObject
        );
    }

    // =========================================================
    // BACKUP MISS CHECK
    // =========================================================

    private void CheckResult()
    {
        if (resultProcessed)
        {
            return;
        }

        RegisterMiss();
    }

    // =========================================================
    // CLEANUP
    // =========================================================

    private void OnDestroy()
    {
        CancelInvoke();
    }
}