using TMPro;
using UnityEngine;

public class PlayerOverheatLogic : MonoBehaviour
{
    [Header("Settings")]
    public Transform targetObject;
    // The object that causes overheating
    public float overheatRate = 5f;    // How fast it goes up per second
    public float maxOverheat = 100f;
    public float rayDistance = 800f;

    [Header("Status")]
    public float overheat = 0f;
    public bool isObserved = false;

    [Header("Floating HUD Text")]
    public TextMeshPro outText;


    void Update()
    {
        CheckVisibility();
        HandleOverheatLogic();

        
    }

    void FixedUpdate()
    {
        // Update the text with the current overheat value
        outText.text = $"Overheat: {overheat:F1}%";

    }
    void CheckVisibility()
    {
        if (targetObject == null)
        {
            isObserved = false;
            return;
        }

        Vector3 origin = transform.position + Vector3.up * 1.5f;
        Vector3 direction = (targetObject.position - origin).normalized;
        float distance = Vector3.Distance(origin, targetObject.position);

        // If something blocks the ray before reaching the light, you're in shadow
        isObserved = !Physics.Raycast(origin, direction, distance);
    }

    private void OnDrawGizmos()
    {
        if (targetObject != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, targetObject.position);
        }
    }

    void HandleOverheatLogic()
    {
        if (isObserved)
        {
            // Use Time.deltaTime to make the increase frame-rate independent
            overheat += overheatRate * Time.deltaTime;
        }
        else
        {
            // Optional: Cool down when not looking
            overheat -= (overheatRate / 2) * Time.deltaTime;
        }

        // Keep overheat between 0 and maxOverheat
        overheat = Mathf.Clamp(overheat, 0, maxOverheat);
    }
}