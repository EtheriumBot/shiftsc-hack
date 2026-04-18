using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class NPCMovement : MonoBehaviour
{
    [Header("Path Settings")]
    public Transform pathParent;
    public float movementSpeed = 3f;
    public float arrivalDistance = 0.5f;
    public bool loopPath = true;

    private Transform[] waypoints;
    private int currentWaypointIndex = 0;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Initialize waypoints from the parent object
        waypoints = new Transform[pathParent.childCount];
        for (int i = 0; i < pathParent.childCount; i++)
        {
            waypoints[i] = pathParent.GetChild(i);
        }
    }

    void FixedUpdate()
    {
        if (waypoints.Length == 0) return;

        MoveTowardsWaypoint();
    }

    void MoveTowardsWaypoint()
    {
        Vector3 targetPosition = waypoints[currentWaypointIndex].position;
        // Keep the target at the same height as the NPC to avoid "tilting"
        targetPosition.y = transform.position.y;

        // Calculate direction
        Vector3 direction = (targetPosition - transform.position).normalized;

        // Apply Velocity
        rb.linearVelocity = direction * movementSpeed;

        // Handle Rotation (since Rigidbody rotation is frozen)
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.fixedDeltaTime * 10f);
        }

        // Check if we reached the waypoint
        float distance = Vector3.Distance(transform.position, targetPosition);
        if (distance < arrivalDistance)
        {
            SetNextWaypoint();
        }
    }

    void SetNextWaypoint()
    {
        currentWaypointIndex++;

        if (currentWaypointIndex >= waypoints.Length)
        {
            if (loopPath)
            {
                currentWaypointIndex = 0;
            }
            else
            {
                // Stop moving if not looping
                rb.linearVelocity = Vector3.zero;
                enabled = false;
            }
        }
    }
}