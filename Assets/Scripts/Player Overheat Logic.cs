using UnityEngine;

public class PlayerOverheatLogic : MonoBehaviour
{
    [Header("Settings")]
    public Transform targetObject;
    // The object that causes overheating
    public float overheatRate = 10f;    // How fast it goes up per second
    public float maxOverheat = 100f;
    public float rayDistance = 400f;

    [Header("Status")]
    public float overheat = 0f;
    public bool isObserved = false;

    void Update()
    {
        CheckVisibility();
        HandleOverheatLogic();

        
    }

    void CheckVisibility()
    {
        if (targetObject == null) return;

        // Calculate direction from player to target
        Vector3 direction = targetObject.position - transform.position;
        RaycastHit hit;

        // Cast the ray
        if (Physics.Raycast(transform.position, direction, out hit, rayDistance))
        {
            print(hit.transform.name);

            // Check if the object we hit is the target
            if (hit.transform.tag == "Player")
            {
                isObserved = true;
                return;
            }
            else
            {
                isObserved = false;
            }
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
            // Optional: Cool down when not looking
            overheat -= (overheatRate / 2) * Time.deltaTime;
        }

        // Keep overheat between 0 and maxOverheat
        overheat = Mathf.Clamp(overheat, 0, maxOverheat);
    }
}