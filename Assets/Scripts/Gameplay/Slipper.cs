using UnityEngine;

public class Slipper : MonoBehaviour
{
    private bool hitCan = false;
    private bool resultProcessed = false;

    private void Start()
    {
        // Check whether the slipper missed after 5 seconds
        Invoke(nameof(CheckResult), 5f);
    }

    // Called by CanTarget when the slipper hits the can
    public void MarkAsHit()
    {
        if (resultProcessed)
            return;

        hitCan = true;
        resultProcessed = true;

        CancelInvoke(nameof(CheckResult));

        Debug.Log("CAN HIT!");

        if (GameManager.Instance != null)
        {
            GameManager.Instance.AddScore(10);
        }
        else
        {
            Debug.LogError("GameManager was not found!");
        }

        // Remove slipper shortly after hitting
        Destroy(gameObject, 1f);
    }

    private void CheckResult()
    {
        if (resultProcessed)
            return;

        resultProcessed = true;

        if (!hitCan)
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.LoseLife();
            }
            else
            {
                Debug.LogError("GameManager was not found!");
            }
        }

        // Remove slipper after the miss is processed
        Destroy(gameObject);
    }
}