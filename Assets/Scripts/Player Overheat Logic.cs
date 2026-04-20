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

    public LayerMask visibilityMask;

    public static PlayerOverheatLogic reference;

    void Awake()
    {
        reference = this; // Set the static reference to this instance of the PlayerOverheatLogic
    }

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
            Debug.Log("No target object");
            return;
        }

        Vector3 origin = transform.position + Vector3.up * 1.5f;
        Vector3 direction = (targetObject.position - origin).normalized;
        float distance = Vector3.Distance(origin, targetObject.position);


        // If something blocks the ray before reaching the light, you're in shadow
        isObserved = !Physics.Raycast(origin, direction, distance, visibilityMask);
        Debug.Log(isObserved);

        RaycastHit hit;
        if (Physics.Raycast(origin, direction, out hit, distance))
        {
            Debug.Log("Hit: " + hit.collider.name);
            isObserved = false;
        }
        else
        {
            Debug.Log("Nothing hit");
            isObserved = true;
        }
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
            // Cool down when not looking
            overheat -= (overheatRate / 2) * Time.deltaTime;
        }

        // Keep overheat between 0 and maxOverheat
        overheat = Mathf.Clamp(overheat, 0, maxOverheat);
    }
}