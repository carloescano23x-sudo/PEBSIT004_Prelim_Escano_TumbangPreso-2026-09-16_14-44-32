using UnityEngine;
using UnityEngine.EventSystems;

public class MobileLook : MonoBehaviour
{
    [Header("Camera Setup")]
    public Transform playerBody;
    public Camera playerCamera;

    [Header("Touch Settings")]
    public float touchSensitivity = 0.15f;

    [Header("Vertical Look Limit")]
    public float minimumX = -45f;
    public float maximumX = 45f;

    private float xRotation = 0f;

    void Update()
    {
        if (GameManager.Instance == null)
            return;

        if (GameManager.Instance.currentState !=
            GameManager.GameState.Playing)
            return;

        HandleTouchLook();
    }

    void HandleTouchLook()
    {
        if (Input.touchCount == 0)
            return;

        Touch touch = Input.GetTouch(0);

        // Do not rotate camera when touching UI
        if (EventSystem.current != null)
        {
            if (EventSystem.current.IsPointerOverGameObject(
                touch.fingerId))
            {
                return;
            }
        }

        if (touch.phase == TouchPhase.Moved)
        {
            float touchX =
                touch.deltaPosition.x *
                touchSensitivity;

            float touchY =
                touch.deltaPosition.y *
                touchSensitivity;

            xRotation -= touchY;

            xRotation = Mathf.Clamp(
                xRotation,
                minimumX,
                maximumX
            );

            playerCamera.transform.localRotation =
                Quaternion.Euler(
                    xRotation,
                    0f,
                    0f
                );

            playerBody.Rotate(
                Vector3.up * touchX
            );
        }
    }
}