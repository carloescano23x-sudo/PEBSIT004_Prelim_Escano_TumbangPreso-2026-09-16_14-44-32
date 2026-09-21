using UnityEngine;
using UnityEngine.EventSystems;

public class MobileLook : MonoBehaviour
{
    [Header("References")]
    public Transform playerBody;
    public Camera playerCamera;

    [Header("Mobile Look Settings")]
    [Range(0.01f, 1f)]
    public float horizontalSensitivity = 0.12f;

    [Range(0.01f, 1f)]
    public float verticalSensitivity = 0.10f;

    [Header("Look Limits")]
    public float minimumVerticalAngle = -30f;
    public float maximumVerticalAngle = 30f;

    private float verticalRotation = 0f;
    private int aimingFingerId = -1;

    void Start()
    {
        if (playerCamera != null)
        {
            verticalRotation =
                NormalizeAngle(
                    playerCamera.transform.localEulerAngles.x
                );

            verticalRotation =
                Mathf.Clamp(
                    verticalRotation,
                    minimumVerticalAngle,
                    maximumVerticalAngle
                );
        }
    }

    void Update()
    {
        if (GameManager.Instance == null)
            return;

        if (GameManager.Instance.currentState !=
            GameManager.GameState.Playing)
        {
            aimingFingerId = -1;
            return;
        }

#if UNITY_ANDROID || UNITY_IOS
        HandleTouchLook();
#endif
    }

    private void HandleTouchLook()
    {
        // No fingers = absolutely no camera movement.
        if (Input.touchCount == 0)
        {
            aimingFingerId = -1;
            return;
        }

        // -----------------------------------------------------
        // FIND OR KEEP ONE AIMING FINGER
        // -----------------------------------------------------

        Touch? aimingTouch = null;

        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);

            // Keep using the same finger.
            if (touch.fingerId == aimingFingerId)
            {
                aimingTouch = touch;
                break;
            }

            // Select a new finger only when it first touches.
            if (aimingFingerId == -1 &&
                touch.phase == TouchPhase.Began)
            {
                // Never use UI touches for aiming.
                if (IsTouchOverUI(touch.fingerId))
                    continue;

                aimingFingerId = touch.fingerId;
                aimingTouch = touch;
                break;
            }
        }

        // Our aiming finger disappeared.
        if (!aimingTouch.HasValue)
        {
            aimingFingerId = -1;
            return;
        }

        Touch touchToUse = aimingTouch.Value;

        // -----------------------------------------------------
        // RELEASE
        // -----------------------------------------------------

        if (touchToUse.phase == TouchPhase.Ended ||
            touchToUse.phase == TouchPhase.Canceled)
        {
            aimingFingerId = -1;
            return;
        }

        // Only rotate while finger actually moves.
        if (touchToUse.phase != TouchPhase.Moved)
            return;

        // -----------------------------------------------------
        // DIRECT TOUCH MOVEMENT
        // -----------------------------------------------------

        float lookX =
            touchToUse.deltaPosition.x *
            horizontalSensitivity;

        float lookY =
            touchToUse.deltaPosition.y *
            verticalSensitivity;

        // Prevent an unusually large touch delta from
        // causing a sudden camera jump.
        lookX = Mathf.Clamp(lookX, -4f, 4f);
        lookY = Mathf.Clamp(lookY, -3f, 3f);

        // Horizontal rotation.
        if (playerBody != null)
        {
            playerBody.Rotate(
                0f,
                lookX,
                0f,
                Space.Self
            );
        }

        // Vertical rotation.
        if (playerCamera != null)
        {
            verticalRotation -= lookY;

            verticalRotation =
                Mathf.Clamp(
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
    }

    private bool IsTouchOverUI(int fingerId)
    {
        if (EventSystem.current == null)
            return false;

        return EventSystem.current.IsPointerOverGameObject(
            fingerId
        );
    }

    private float NormalizeAngle(float angle)
    {
        if (angle > 180f)
            angle -= 360f;

        return angle;
    }
}