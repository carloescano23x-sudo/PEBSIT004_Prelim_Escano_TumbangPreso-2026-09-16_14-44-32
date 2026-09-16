using System.Collections;
using UnityEngine;

public class CanTarget : MonoBehaviour
{
    private bool hasBeenHit = false;

    private Vector3 startPosition;
    private Quaternion startRotation;

    private Rigidbody rb;

    void Start()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;

        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (hasBeenHit)
            return;

        if (collision.gameObject.CompareTag("Slipper"))
        {
            hasBeenHit = true;

            if (GameManager.Instance != null)
            {
                GameManager.Instance.AddScore(10);
            }

            StartCoroutine(ResetCan());
        }
    }

    IEnumerator ResetCan()
    {
        yield return new WaitForSeconds(2f);

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        float randomX = Random.Range(-3f, 3f);

        transform.position = new Vector3(
            randomX,
            startPosition.y,
            startPosition.z
        );

        transform.rotation = startRotation;

        hasBeenHit = false;
    }
}