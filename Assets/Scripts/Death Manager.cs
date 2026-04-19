using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathManager : MonoBehaviour
{

    private WaterManager waterManager;
    private PlayerOverheatLogic playerOverheatLogic;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Get references to the WaterManager and PlayerOverheatLogic components
        waterManager = WaterManager.reference; // Assuming WaterManager has a static reference
        playerOverheatLogic = PlayerOverheatLogic.reference; // Assuming PlayerOverheatLogic has a static reference


        // If the water level hits 0% or the overheat hits 100%, we die and go to the death screen
        if (waterManager.waterLevel <= 0f || playerOverheatLogic.overheat >= 100f)
        {
            // Load the death screen scene here, for example:
            SceneManager.LoadScene("Death Screen");
            Debug.Log("Player has died! Load the death screen.");
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
