using UnityEngine;

public class WaterManager : MonoBehaviour
{

    public float waterLevel = 100f; // The current height of the water

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Make the water level go down over time
        waterLevel -= Time.deltaTime * 1f; // Adjust the speed of water level decrease here
    }

    private void OnTriggerEnter(Collider other)
    {
        // If we collide with a bottle, instantly bring waterlevel back to 100f
        if (other.CompareTag("Bottle"))
        {
            waterLevel = 100f;
            Destroy(other.gameObject); // Remove the bottle from the scene
        }
    }
}
