using System.Collections;
using UnityEngine;

public class SlipperThrow : MonoBehaviour
{
    // =========================================================
    // THROW SETUP
    // =========================================================

    public GameObject slipperPrefab;
    public Transform throwPoint;
    public Camera playerCamera;

    // =========================================================
    // THROW SETTINGS
    // =========================================================

    public float throwForce = 15f;
    public float throwCooldown = 2f;

    private bool canThrow = true;

    // =========================================================
    // UPDATE
    // =========================================================

    private void Update()
    {
        // Make sure GameManager exists
        if (GameManager.Instance == null)
            return;

        // Player can ONLY throw while the game state is Playing
        if (GameManager.Instance.currentState !=
            GameManager.GameState.Playing)
            return;

        // Throw slipper when left mouse button is clicked
        if (Input.GetMouseButtonDown(0) && canThrow)
        {
            StartCoroutine(ThrowRoutine());
        }
    }

    // =========================================================
    // THROW COOLDOWN
    // =========================================================

    private IEnumerator ThrowRoutine()
    {
        canThrow = false;

        ThrowSlipper();

        yield return new WaitForSeconds(throwCooldown);

        canThrow = true;
    }

    // =========================================================
    // THROW SLIPPER
    // =========================================================

    private void ThrowSlipper()
    {
        // Check slipper prefab
        if (slipperPrefab == null)
        {
            Debug.LogError("Slipper Prefab is not assigned!");
            return;
        }

        // Check throw point
        if (throwPoint == null)
        {
            Debug.LogError("Throw Point is not assigned!");
            return;
        }

        // Check camera
        if (playerCamera == null)
        {
            Debug.LogError("Player Camera is not assigned!");
            return;
        }

        // Create slipper
        GameObject slipper = Instantiate(
            slipperPrefab,
            throwPoint.position,
            throwPoint.rotation
        );

        // Get Rigidbody
        Rigidbody rb = slipper.GetComponent<Rigidbody>();

        if (rb != null)
        {
            // Throw slipper forward
            rb.AddForce(
                playerCamera.transform.forward * throwForce,
                ForceMode.Impulse
            );
        }
        else
        {
            Debug.LogError("Slipper prefab has no Rigidbody!");
        }
    }
}