using System.Collections;
using UnityEngine;

public class SlipperThrower : MonoBehaviour
{
    [Header("Throw Setup")]
    public GameObject slipperPrefab;
    public Transform throwPoint;
    public Camera playerCamera;

    [Header("Throw Settings")]
    public float throwForce = 15f;
    public float throwCooldown = 2f;

    private bool canThrow = true;

    // =========================================================
    // UPDATE - PC INPUT
    // =========================================================

    void Update()
    {
        if (GameManager.Instance == null)
            return;

        if (GameManager.Instance.currentState !=
            GameManager.GameState.Playing)
            return;

        // PC mouse control
        if (Input.GetMouseButtonDown(0) && canThrow)
        {
            StartCoroutine(ThrowRoutine());
        }
    }

    // =========================================================
    // MOBILE THROW BUTTON
    // =========================================================

    public void MobileThrow()
    {
        if (GameManager.Instance == null)
            return;

        if (GameManager.Instance.currentState !=
            GameManager.GameState.Playing)
            return;

        if (!canThrow)
            return;

        StartCoroutine(ThrowRoutine());
    }

    // =========================================================
    // THROW ROUTINE
    // =========================================================

    private IEnumerator ThrowRoutine()
    {
        canThrow = false;

        ThrowSlipper();

        yield return new WaitForSeconds(throwCooldown);

        canThrow = true;
    }

    // =========================================================
    // CREATE AND THROW SLIPPER
    // =========================================================

    private void ThrowSlipper()
    {
        if (slipperPrefab == null)
        {
            Debug.LogError("Slipper Prefab is missing!");
            return;
        }

        if (throwPoint == null)
        {
            Debug.LogError("Throw Point is missing!");
            return;
        }

        if (playerCamera == null)
        {
            Debug.LogError("Player Camera is missing!");
            return;
        }

        GameObject slipper = Instantiate(
            slipperPrefab,
            throwPoint.position,
            throwPoint.rotation
        );

        Rigidbody rb =
            slipper.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.AddForce(
                playerCamera.transform.forward * throwForce,
                ForceMode.Impulse
            );
        }
        else
        {
            Debug.LogError(
                "Slipper prefab has no Rigidbody!"
            );
        }
    }
}