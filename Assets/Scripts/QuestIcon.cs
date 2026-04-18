using UnityEngine;

public class QuestIcon : MonoBehaviour
{
    [Header("Movement Settings")]
    public float rotationSpeed = 50f;
    public float floatAmplitude = 0.5f;
    public float floatFrequency = 1f;

    [Header("Interaction Settings")]
    public GameObject particleEffectPrefab;

    private Vector3 startPosition;

    void Start()
    {
        // Store the starting position to hover around it
        startPosition = transform.position;
    }

    void Update()
    {
        // 1. Rotation
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);

        // 2. Hovering (Sine wave movement)
        float newY = startPosition.y + Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the other object's layer is "Player"
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            CollectQuest();
        }
    }

    void CollectQuest()
    {
        // 3. Spawn Particle System at the icon's current position
        if (particleEffectPrefab != null)
        {
            Instantiate(particleEffectPrefab, transform.position, Quaternion.identity);
        }

        // 4. Remove the icon
        Destroy(gameObject);
    }
}