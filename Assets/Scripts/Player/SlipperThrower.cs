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

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canThrow)
        {
            StartCoroutine(ThrowRoutine());
        }
    }

    IEnumerator ThrowRoutine()
    {
        canThrow = false;

        GameObject slipper = Instantiate(
            slipperPrefab,
            throwPoint.position,
            throwPoint.rotation
        );

        Rigidbody rb = slipper.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.AddForce(
                playerCamera.transform.forward * throwForce,
                ForceMode.Impulse
            );
        }

        yield return new WaitForSeconds(throwCooldown);

        canThrow = true;
    }
}