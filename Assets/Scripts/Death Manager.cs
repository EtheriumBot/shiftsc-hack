using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathManager : MonoBehaviour
{
    private WaterManager waterManager;
    private PlayerOverheatLogic playerOverheatLogic;
    private bool isDead = false; // Prevents the scene from loading multiple times

    void Start()
    {
        // Keep the references in Start so we only find them once
        waterManager = WaterManager.reference;
        playerOverheatLogic = PlayerOverheatLogic.reference;
    }

    void Update()
    {
        // If we haven't died yet, keep checking the stats
        if (!isDead)
        {
            CheckDeathConditions();
        }
    }

    void CheckDeathConditions()
    {
        // Check if water is empty OR heat is full
        if (waterManager.waterLevel <= 0.01f || playerOverheatLogic.overheat >= 99.9f)
        {
            isDead = true; // Set to true so this block doesn't trigger again

            Debug.Log("Player has died! Loading the death screen.");
            SceneManager.LoadScene("DeathScreen");
        }
    }
}