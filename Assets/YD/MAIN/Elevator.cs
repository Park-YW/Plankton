using UnityEngine;

public class Elevator : MonoBehaviour
{
    public Transform Bottom; // Reference to the bottom position
    public Transform Top; // Reference to the top position
    public GameObject Player; // Reference to the player object

    private bool isPlayerOnBottom = false;
    private SpriteRenderer bottomRenderer;
    public float detectionRadius = 0.5f; // Distance threshold to consider as "collision"

    void Start()
    {
        if (Bottom == null || Top == null || Player == null)
        {
            Debug.LogError("Please assign all necessary references in the Inspector.");
            return;
        }

        bottomRenderer = Bottom.GetComponent<SpriteRenderer>();
        if (bottomRenderer == null)
        {
            Debug.LogError("The Bottom object must have a SpriteRenderer component.");
        }
        else
        {
            // Set Bottom to fully transparent at the start
            bottomRenderer.color = new Color(1, 1, 1, 0);
            Debug.Log("Bottom object initialized as fully transparent.");
        }
    }

    void Update()
    {
        CheckPlayerProximity();

        if (isPlayerOnBottom && Input.GetKeyDown(KeyCode.L))
        {
            TeleportToTop();
        }
    }

    private void CheckPlayerProximity()
    {
        float distance = Vector3.Distance(Player.transform.position, Bottom.position);

        if (distance <= detectionRadius)
        {
            if (!isPlayerOnBottom)
            {
                isPlayerOnBottom = true;

                if (bottomRenderer != null)
                {
                    bottomRenderer.color = new Color(0, 0, 1, 0.5f); // Change to semi-transparent blue
                    Debug.Log("Bottom object color changed to blue.");
                }

                Debug.Log("Player is within detection radius of the bottom.");
            }
        }
        else
        {
            if (isPlayerOnBottom)
            {
                isPlayerOnBottom = false;

                if (bottomRenderer != null)
                {
                    bottomRenderer.color = new Color(1, 1, 1, 0); // Reset to fully transparent
                    Debug.Log("Bottom object color reset to transparent.");
                }

                Debug.Log("Player is outside detection radius of the bottom.");
            }
        }
    }

    private void TeleportToTop()
    {
        if (Player != null)
        {
            Player.transform.position = Top.position;
            Debug.Log("Player teleported to the top.");
        }
    }
}