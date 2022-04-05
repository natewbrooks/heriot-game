using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
public class Movement : MonoBehaviour {

    private Enemy enemy;

    public bool repathEnabled = true;

    [Header("Intelligence")]
    [Range(3f, 25f)]
    public float visionRadius = 10f;



    [HideInInspector] public float distanceFromTarget;

    public Transform target;
    public List<Transform> targetsInRange = new List<Transform>();
    [HideInInspector] public UnityEngine.AI.NavMeshAgent agent;
    private Animator animator;


    [HideInInspector] public Vector3 startPosition;
    [HideInInspector] public Vector3 dir;
    [HideInInspector] public bool donePath;


    public void Start () {
        enemy = GetComponent<Enemy>();
        animator = transform.GetComponentInChildren<Animator>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();

        agent.updateRotation = false;
		agent.updateUpAxis = false;
        agent.speed = 3f;
        startPosition = transform.position;
    }

    public void Update () {
        dir = (agent.destination - transform.position).normalized;
        donePath = Vector2.Distance(transform.position, agent.destination) < .15f;

        targetsInRange = GetNearbyObjects();
        if(targetsInRange.Count > -1) {
            target = SearchClosestTarget(targetsInRange);
        }

        if(target != null) {
             foreach (Transform obj in targetsInRange) {
                if(obj.gameObject.tag == "Enemy" && obj.gameObject.GetComponent<Movement>().target == null) {
                    obj.gameObject.GetComponent<Movement>().target = target;
                }
            }
            agent.SetDestination(target.position);
        }
    }

    public float CheckDistance(Vector3 position) {
        return Vector2.Distance(transform.position, position);
        
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

    public void StartReturnSequence() {
        Debug.Log("before");
        StartCoroutine(ReturnToPost());
        Debug.Log("after");

        if(agent.hasPath == false) {
            agent.SetDestination(new Vector3(transform.position.x + Random.Range(-5f, 5f), transform.position.y + Random.Range(-5f, 5f), 0));
        }
    }

    public IEnumerator ReturnToPost() {
        yield return new WaitForSeconds(10);
        donePath = true;
        enemy.state = Enemy.State.Return;
        Debug.Log("ME");
    }

    // public void Move() {
    //     transform.position += dir * agent.speed * Time.fixedDeltaTime;
    // }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, visionRadius);    

        var path = GetComponent<UnityEngine.AI.NavMeshAgent>().path;
        for (int i = 0; i < path.corners.Length - 1; i++)
        {
            Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.red);
        }
    }
}
