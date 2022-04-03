using UnityEngine;
// Note this line, if it is left out, the script won't know that the class 'Path' exists and it will throw compiler errors
// This line should always be present at the top of scripts which use pathfinding
using Pathfinding;

[RequireComponent(typeof(Seeker))]
public class Movement : MonoBehaviour {

    [HideInInspector] public Transform targetPosition;
    private Enemy enemy;
    private Seeker seeker;
    public Path path;

    public float speed;

    [HideInInspector] public float distanceToWaypoint;

    [HideInInspector] public float nextWaypointDistance = 3;
    [HideInInspector] private int currentWaypoint = 0;
    [HideInInspector] public float repathRate = 0.25f;
    [HideInInspector] private float lastRepath = float.NegativeInfinity;
    [HideInInspector] public bool reachedEndOfPath;
    public bool repathEnabled = true;

    [HideInInspector] public Vector3 startPosition;
    [HideInInspector] public Vector3 velocity;
    [HideInInspector] public Vector3 dir;

    [HideInInspector] public bool donePath;

    private Animator animator;

    public void Start () {
        seeker = GetComponent<Seeker>();
        animator = transform.GetComponentInChildren<Animator>();
        enemy = GetComponent<Enemy>();

        speed = 3f;
        startPosition = transform.position;
    }

    public void OnPathComplete (Path p) {
        // Debug.Log("A path was calculated. Did it fail with an error? " + p.error);

        // Path pooling. To avoid unnecessary allocations paths are reference counted.
        // Calling Claim will increase the reference count by 1 and Release will reduce
        // it by one, when it reaches zero the path will be pooled and then it may be used
        // by other scripts. The ABPath.Construct and Seeker.StartPath methods will
        // take a path from the pool if possible. See also the documentation page about path pooling.
        p.Claim(this);
        if (!p.error) {
            if (path != null) path.Release(this);
            path = p;
            // Reset the waypoint counter so that we start to move towards the first point in the path
            currentWaypoint = 0;
        } else {
            p.Release(this);
        }
    }

    public void Update () {
        if (Time.time > lastRepath + repathRate && seeker.IsDone() && repathEnabled && targetPosition != null) {
            lastRepath = Time.time;

            // Start a new path to the targetPosition, call the the OnPathComplete function
            // when the path has been calculated (which may take a few frames depending on the complexity)
            seeker.StartPath(transform.position, targetPosition.position, OnPathComplete);
        }

        if (path == null) {
            // We have no path to follow yet, so don't do anything
            return;
        }

        // Check in a loop if we are close enough to the current waypoint to switch to the next one.
        // We do this in a loop because many waypoints might be close to each other and we may reach
        // several of them in the same frame.
        reachedEndOfPath = false;
        // The distance to the next waypoint in the path
        while (true) {
            // If you want maximum performance you can check the squared distance instead to get rid of a
            // square root calculation. But that is outside the scope of this tutorial.
            distanceToWaypoint = Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]);
            if (distanceToWaypoint < nextWaypointDistance) {
                // Check if there is another waypoint or if we have reached the end of the path
                if (currentWaypoint + 1 < path.vectorPath.Count) {
                    currentWaypoint++;
                } else {
                    // Set a status variable to indicate that the agent has reached the end of the path.
                    // You can use this to trigger some special code if your game requires that.
                    reachedEndOfPath = true;
                    break;
                }
            } else {
                break;
            }
        }

        // Slow down smoothly upon approaching the end of the path
        // This value will smoothly go from 1 to 0 as the agent approaches the last waypoint in the path.
        // var speedFactor = reachedEndOfPath ? Mathf.Sqrt(distanceToWaypoint/nextWaypointDistance) : 1f;

        // Direction to the next waypoint
        // Normalize it so that it has a length of 1 world unit
        dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
        // Multiply the direction by our desired speed to get a velocity
        
        // Vector3 velocity = dir * speed * speedFactor;
        velocity = dir * speed;
        //Debug.Log(path.vectorPath.Count);

        // if enemy is close enough to the last waypoint in the set path
        donePath = Vector2.Distance(transform.position, path.vectorPath[path.vectorPath.Count - 1]) < .15f;
        // Debug.Log(donePath);

        if(donePath && targetPosition == null) {
            // code to wander for a bit
            path = null;
            dir = Vector3.zero;
            velocity = Vector3.zero;
        }

    }

    public void SetNewPath(Vector3 target) {
        seeker.StartPath(transform.position, target, OnPathComplete);
    }

    private void FixedUpdate() {
        if(!enemy.frozen) {
            transform.position += dir * speed * Time.fixedDeltaTime;
        }
    }
}