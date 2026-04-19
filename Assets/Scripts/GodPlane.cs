using UnityEngine;

public class GodPlane : MonoBehaviour
{
    [Header("References")]
    public Transform playerLandingSpot;
    public string playerTag = "Player";

    private bool roundComplete = false;

    void Update()
    {
        if (roundComplete) return;

        int remaining = GameObject.FindGameObjectsWithTag("Quests").Length;

        Debug.Log("GodPlane: Quests remaining — " + remaining);

        if (remaining == 0)
            CompleteRound();
    }

    void CompleteRound()
    {
        roundComplete = true;

        GameObject player = GameObject.FindGameObjectWithTag(playerTag);
        if (player == null)
        {
            Debug.LogWarning("GodPlane: No player found with tag: " + playerTag);
            return;
        }

        // Disable gravity so the player lands on the plane instead of falling through
        Rigidbody rb = player.GetComponent<Rigidbody>();
        if (rb)
            rb.linearVelocity = Vector3.zero;

        player.transform.position = playerLandingSpot.position;

        Debug.Log("GodPlane: All quests complete — player elevated.");
    }
}