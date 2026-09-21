using UnityEngine;

public class FirstPersonLook : MonoBehaviour
{
    // =========================================================
    // PC / EDITOR LOOK SETTINGS
    // =========================================================

    [Header("PC Look Settings")]
    public float mouseSensitivity = 150f;

    [Header("References")]
    public Transform playerBody;

    private float xRotation = 0f;

    // =========================================================
    // UPDATE
    // =========================================================

    void Update()
    {
        // This camera control is ONLY for Unity Editor / PC.
#if UNITY_EDITOR || UNITY_STANDALONE

        if (GameManager.Instance == null)
        {
            return;
        }

        if (GameManager.Instance.currentState !=
            GameManager.GameState.Playing)
        {
            return;
        }

        // -----------------------------------------------------
        // MOUSE INPUT
        // -----------------------------------------------------

        float mouseX =
            Input.GetAxis("Mouse X") *
            mouseSensitivity *
            Time.deltaTime;

        float mouseY =
            Input.GetAxis("Mouse Y") *
            mouseSensitivity *
            Time.deltaTime;

        // -----------------------------------------------------
        // VERTICAL LOOK
        // -----------------------------------------------------

        xRotation -= mouseY;

        xRotation =
            Mathf.Clamp(
                xRotation,
                -45f,
                45f
            );

        transform.localRotation =
            Quaternion.Euler(
                xRotation,
                0f,
                0f
            );

        // -----------------------------------------------------
        // HORIZONTAL LOOK
        // -----------------------------------------------------

        if (playerBody != null)
        {
            playerBody.Rotate(
                Vector3.up * mouseX
            );
        }

#endif
    }
}