using UnityEngine;

public class FirstPersonLook : MonoBehaviour
{
    public float mouseSensitivity = 150f;
    public Transform playerBody;

    private float xRotation = 0f;


    void Update()
    {
        if (GameManager.Instance == null)
    return;

if (GameManager.Instance.currentState !=
    GameManager.GameState.Playing)
    return;

        float mouseX = Input.GetAxis("Mouse X") *
                       mouseSensitivity * Time.deltaTime;

        float mouseY = Input.GetAxis("Mouse Y") *
                       mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -45f, 45f);

        transform.localRotation =
            Quaternion.Euler(xRotation, 0f, 0f);

        playerBody.Rotate(Vector3.up * mouseX);
    }
}