using UnityEngine;
// Note this line, if it is left out, the script won't know that the class 'Path' exists and it will throw compiler errors
// This line should always be present at the top of scripts which use pathfinding
using Pathfinding;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Seeker))]
public class Movement : MonoBehaviour {

    private Enemy enemy;
    private Seeker seeker;
    public Path path;

    public float speed;
    public bool repathEnabled = true;

    [Header("Intelligence")]
    [Range(3f, 25f)]
    public float visionRadius = 10f;
    [Range(1f, 10f)]
    public float avoidanceRadius = 1.5f;
    [Range(0f, 1f)]
    public float avoidanceRadiusMultiplier = 0.5f;


    [HideInInspector] public float squareAvoidanceRadius;
    [HideInInspector] public float distanceFromTarget;

    public Transform target;
    public List<Transform> targetsInRange = new List<Transform>();


    [HideInInspector] public float distanceToWaypoint;

    [HideInInspector] public float nextWaypointDistance = 3;
    [HideInInspector] private int currentWaypoint = 0;
    [HideInInspector] public float repathRate = 0.25f;
    [HideInInspector] private float lastRepath = float.NegativeInfinity;
    [HideInInspector] public bool reachedEndOfPath;

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
        squareAvoidanceRadius = squareAvoidanceRadius * avoidanceRadiusMultiplier * squareAvoidanceRadius;
    }

    public void OnPathComplete (Path p) {
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
        if (Time.time > lastRepath + repathRate && seeker.IsDone() && repathEnabled && target != null) {
            lastRepath = Time.time;

            seeker.StartPath(transform.position, target.position, OnPathComplete);
        }

        if (path == null) {
            // We have no path to follow yet, so don't do anything
            return;
        }

        reachedEndOfPath = false;
        // The distance to the next waypoint in the path
        while (true) {
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

        dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
        velocity = dir * speed;
        donePath = Vector2.Distance(transform.position, path.vectorPath[path.vectorPath.Count - 1]) < .15f;

        if(donePath && target == null) {
            // code to wander for a bit
            path = null;
            dir = Vector3.zero;
            velocity = Vector3.zero;
        }
    }

    public void SetNewPath(Vector3 target) {
        seeker.StartPath(transform.position, target, OnPathComplete);
    }

    public float CheckDistance(Vector3 position) {
        return Vector2.Distance(transform.position, position);
        
    }

    private void FixedUpdate() {
        targetsInRange = GetNearbyObjects();
        target = SearchClosestTarget(targetsInRange);

        if(target != null) {
             foreach (Transform obj in targetsInRange) {
                 if(obj.gameObject.tag == "Enemy" && obj.gameObject.GetComponent<Movement>().target == null) {
                     obj.gameObject.GetComponent<Movement>().target = target;
                 }
             }
        }

        Move();
    }

    private Transform SearchClosestTarget(List<Transform> arr) {
        float closetTargetDistance = 100f;
        int targetIndex = -1;

            for (int i = 0; i < arr.Count; i++) {     
                if ((arr[i].tag == "Player" && enemy.team != Enemy.Team.Player) || (arr[i].tag == "Enemy" && arr[i].gameObject.GetComponent<Enemy>().team != enemy.team)) {
                    if(CheckDistance(arr[i].gameObject.transform.position) < closetTargetDistance) {
                        closetTargetDistance = Vector2.Distance(transform.position, arr[i].gameObject.transform.position);
                        targetIndex = i;
                    }
                }
            }      

            // if a target exists, return it and set the distance and end any instance of return
            if(targetIndex > -1) {
                StopCoroutine(ReturnToPost());
                distanceFromTarget = CheckDistance(arr[targetIndex].gameObject.transform.position);
                return arr[targetIndex].gameObject.transform;
            }
            return null;
    }
    
     private List<Transform> GetNearbyObjects() {
        List<Transform> colliderTransforms = new List<Transform>();
        Collider2D[] inRange = Physics2D.OverlapCircleAll(transform.position, visionRadius, LayerMask.GetMask("Enemy"));

        foreach (Collider2D c in inRange)
        {
            if(c != GetComponent<Collider2D>()) {
                colliderTransforms.Add(c.transform);
            }
        }
        return colliderTransforms;
    }

    public Vector2 CalculateAvoidance(List<Transform> colliderTransforms) {
        Vector2 avoidanceMove = Vector2.zero;
        int nAvoid = 0;
        foreach(Transform item in colliderTransforms) {
            if(Vector2.SqrMagnitude(item.position - transform.position) < squareAvoidanceRadius) {
                nAvoid++;
                avoidanceMove += (Vector2)(transform.position - item.position);
            }
        }
        if(nAvoid > 0) {
            avoidanceMove /= nAvoid;
        }

        return avoidanceMove;
    }

    public IEnumerator ReturnToPost() {
        yield return new WaitForSeconds(10);
        path = null;
        donePath = true;
        enemy.finalSearch = false;
        enemy.state = Enemy.State.Return;
    }

    public void Move() {
        transform.position += dir * speed * Time.fixedDeltaTime;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, visionRadius);    
    }
}

                