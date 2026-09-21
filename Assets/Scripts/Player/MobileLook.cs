using UnityEngine;
using UnityEngine.EventSystems;

public class MobileLook : MonoBehaviour
{
    [Header("References")]
    public Transform playerBody;
    public Camera playerCamera;

    [Header("Mobile Look Settings")]
    [Range(0.01f, 0.20f)]
    public float touchSensitivity = 0.035f;

    [Range(0.01f, 1f)]
    public float smoothing = 0.20f;

    [Header("Look Limits")]
    public float minimumVerticalAngle = -35f;
    public float maximumVerticalAngle = 35f;

    private float verticalRotation = 0f;

    private Vector2 currentLookDelta;
    private Vector2 smoothLookDelta;

    private int aimingFingerId = -1;

    void Update()
    {
        if (GameManager.Instance == null)
        {
            return;
        }

        if (GameManager.Instance.currentState !=
            GameManager.GameState.Playing)
        {
            ResetTouch();
            return;
        }

#if UNITY_ANDROID || UNITY_IOS

        HandleMobileLook();

#endif
    }

    // =========================================================
    // MOBILE LOOK
    // =========================================================

    private void HandleMobileLook()
    {
        if (Input.touchCount == 0)
        {
            ResetTouch();
            return;
        }

        Touch? aimingTouch = null;

        // -----------------------------------------------------
        // FIND / KEEP AIMING FINGER
        // -----------------------------------------------------

        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);

            // Continue using the same aiming finger
            if (touch.fingerId == aimingFingerId)
            {
                aimingTouch = touch;
                break;
            }

            // Find a new finger for aiming
            if (aimingFingerId == -1 &&
                touch.phase == TouchPhase.Began)
            {
                // Ignore UI touches such as THROW and PAUSE
                if (IsTouchOverUI(touch.fingerId))
                {
                    continue;
                }

                aimingFingerId = touch.fingerId;
                aimingTouch = touch;

                break;
            }
        }

        if (!aimingTouch.HasValue)
        {
            return;
        }

        Touch activeTouch = aimingTouch.Value;

        // -----------------------------------------------------
        // RELEASE AIMING FINGER
        // -----------------------------------------------------

        if (activeTouch.phase == TouchPhase.Ended ||
            activeTouch.phase == TouchPhase.Canceled)
        {
            ResetTouch();
            return;
        }

        // -----------------------------------------------------
        // MOVE CAMERA
        // -----------------------------------------------------

        if (activeTouch.phase == TouchPhase.Moved)
        {
            Vector2 rawDelta =
                activeTouch.deltaPosition *
                touchSensitivity;

            smoothLookDelta = Vector2.Lerp(
                smoothLookDelta,
                rawDelta,
                1f - smoothing
            );

            currentLookDelta = smoothLookDelta;

            ApplyLook(currentLookDelta);
        }
    }

    // =========================================================
    // APPLY CAMERA MOVEMENT
    // =========================================================

    private void ApplyLook(Vector2 lookDelta)
    {
        if (playerBody == null ||
            playerCamera == null)
        {
            return;
        }

        // Horizontal rotation
        playerBody.Rotate(
            Vector3.up * lookDelta.x
        );

        // Vertical rotation
        verticalRotation -= lookDelta.y;

        verticalRotation = Mathf.Clamp(
            verticalRotation,
            minimumVerticalAngle,
            maximumVerticalAngle
        );

        playerCamera.transform.localRotation =
            Quaternion.Euler(
                verticalRotation,
                0f,
                0f
            );
    }

    // =========================================================
    // CHECK UI
    // =========================================================

    private bool IsTouchOverUI(int fingerId)
    {
        if (EventSystem.current == null)
        {
            return false;
        }

        return EventSystem.current
            .IsPointerOverGameObject(fingerId);
    }

    // =========================================================
    // RESET
    // =========================================================

    private void ResetTouch()
    {
        aimingFingerId = -1;

        currentLookDelta = Vector2.zero;
        smoothLookDelta = Vector2.zero;
    }
}