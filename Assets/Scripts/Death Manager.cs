using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathManager : MonoBehaviour
{
    private WaterManager wm;
    private PlayerOverheatLogic pol;
    private bool isDead = false; // Prevents the scene from loading multiple times

    void Start()
    {
        // Keep the references in Start so we only find them once
        wm = WaterManager.reference;
        pol = PlayerOverheatLogic.reference;
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
        if (wm.waterLevel <= 0.01f || pol.overheat >= 99.9f)
        {
            isDead = true; // Set to true so this block doesn't trigger again

            Debug.Log("Player has died! Loading the death screen.");
            SceneManager.LoadScene("DeathScreen");
        }
    }
}