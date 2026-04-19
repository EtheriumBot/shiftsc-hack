using UnityEngine;

public class GodPlane : MonoBehaviour
{
    [Header("References")]
    public Transform playerLandingSpot;
    public string playerTag = "Player";

    private bool roundComplete = false;
    private int questsCollected = 0;
    private int initialQuestCount = 0;

    void Start()
    {
        initialQuestCount = GameObject.FindGameObjectsWithTag("Quests").Length;
    }

    void Update()
    {
        if (roundComplete) return;

        int current = GameObject.FindGameObjectsWithTag("Quests").Length;
        int previous = initialQuestCount - questsCollected;

        if (current < previous)
        {
            questsCollected += previous - current;
            Debug.Log("GodPlane: Quests collected — " + questsCollected + " / 3, Remaining — " + (3 - questsCollected));
        }

        if (questsCollected >= 3)
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