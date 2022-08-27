using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private Rigidbody enemyRB;
    //private NavMeshAgent agent;

    [SerializeField] private float enemySpeed = 5f;

    // Array of waypoints to walk from one to the next one
    [SerializeField] private Transform[] waypoints;

    //
    private int waypointIndex = 0;
    private Vector3 direction;
    private bool isDone = false;
    public bool traversed = false;
    private EnemyState enemyState;
    private bool isMovingToLast;

    public Location enemyLocation;

    public enum EnemyState
    {
        Patrolling,
        Chasing
    }


    private void Start()
    {
        enemyRB = GetComponent<Rigidbody>();
        //agent = GetComponent<NavMeshAgent>();
        isMovingToLast = true;
        enemyState = EnemyState.Patrolling;
    }

    void FixedUpdate()
    {
        //// Get input
        //if(transform.position != waypoints[0].transform.position)
        //{
        //    mH = -1;
        //    mV = -1;
        //}
        //else


        //Vector3 m_Input = new Vector3(1, 0, Input.GetAxis("Vertical"));

        //// Apply the movement vector to the current position, which is
        //// multiplied by deltaTime and speed for a smooth MovePosition
        //enemyRB.velocity = new Vector3(mH * playerSpeed, enemyRB.velocity.y, mV * playerSpeed);

        if(enemyState == EnemyState.Patrolling)

        if (direction != Vector3.zero)
        {
            enemyRB.MovePosition(transform.position + direction * enemySpeed * Time.deltaTime);
            //enemyRB.MovePosition(Vector3.SmoothDamp(transform.position, waypoints[waypointIndex].transform.position,
            //    ref enemySpeed, Time.deltaTime));
        }
        //else
        //    isDone = true;

        //else
        //    waypointIndex++;
            
    }


    private void Update()
    {

        //Debug.Log(direction);

        /// Patrolling
        if(enemyState == EnemyState.Patrolling)
        {
            // Going from Wa
            if (waypointIndex <= waypoints.Length - 1 && isMovingToLast)
            {
                //Debug.Log(waypointIndex);
                //Debug.Log(direction);
                isMovingToLast = true;


                //Debug.Log(traversed);

                // Reaching Waypoints
                if (!traversed)
                {
                    direction = waypoints[waypointIndex].transform.position - transform.position;
                    //Debug.Log(direction);
                    if (Mathf.Abs(direction.x) < 0.1f && Mathf.Abs(direction.z) < 0.1f)
                        direction = Vector3.zero;
                }

                else {
                    direction = Vector3.zero;
                    traversed = false;
                    //waypointIndex++;
                }

                // Move on to Next Waypoint
                if (direction == Vector3.zero)
                {
                    //Debug.Log("Add");
                    waypointIndex++;
                }
            }
            else
            {
                waypointIndex = 0;
            }

            //else if (waypointIndex <= waypoints.Length - 1 && !isMovingToLast)
            //{
            //    isMovingToLast = false;


            //    // Reaching Waypoints
            //    if (!traversed)
            //    {
            //        direction = waypoints[waypointIndex].transform.position - transform.position;
            //        //Debug.Log(direction);
            //        if (Mathf.Abs(direction.x) < 0.1f && Mathf.Abs(direction.z) < 0.1f)
            //            direction = Vector3.zero;
            //    }

            //    else
            //    {
            //        direction = Vector3.zero;
            //        traversed = false;
            //        //waypointIndex++;
            //    }

            //    // Move on to Next Waypoint
            //    if (direction == Vector3.zero)
            //    {
            //        waypointIndex--;
            //        traversed = false;
            //    }
            //}

            //else
            //{
            //    Debug.Log("ELSe");
            //    //if (isMovingToLast)
            //    //    waypointIndex--;
            //    //else
            //    //    waypointIndex++;

            //    isMovingToLast = !isMovingToLast;
            //}
        }

    }

    

    // Attempt 6
    // ----------------------------------------------------------------------------------------
    //// Method that actually make Enemy walk
    //private void Move()
    //{
    //    // If Enemy didn't reach last waypoint it can move
    //    // If enemy reached last waypoint then it stops
    //    if (waypointIndex <= waypoints.Length - 1)
    //    {
    //        agent.SetDestination(waypoints[waypointIndex].transform.position);

    //        Vector3 distanceToWalkPoint = transform.position - waypoints[waypointIndex].transform.position;

    //        //Walkpoint reached
    //        if (distanceToWalkPoint.magnitude < 1f)
    //            waypointIndex += 1;
    //        //walkPointSet = false;
    //    }
    //    else
    //    {

    //        //transform.position = new Vector3(-13.8f, 0.34f, 1.36f);
    //        waypointIndex = 0;
    //    }
    //}




    //// Attempt 5?
    ////----------------------------------------
    //NavMeshAgent agent;

    //// Array of waypoints to walk from one to the next one
    //[SerializeField]
    //private Transform[] waypoints;

    //// Walk speed that can be set in Inspector
    //[SerializeField]
    //private float moveSpeed = 2f;

    //// Index of current waypoint from which Enemy walks
    //// to the next one
    //private int waypointIndex = 0;

    //[SerializeField] Light spotLight;
    //[SerializeField] float turnSpeed;

    //// Use this for initialization
    //private void Start()
    //{

    //    // Set position of Enemy as position of the first waypoint
    //    transform.position = waypoints[waypointIndex].transform.position;
    //    //StartCoroutine(TurnToFace(spotLight.transform, waypoints[waypointIndex].transform.position));
    //}

    //private void Awake()
    //{
    //    //controller = GetComponent<CharacterController>();
    //    agent = GetComponent<NavMeshAgent>();

    //}

    //protected void LateUpdate()
    //{
    //    transform.localEulerAngles = new Vector3(0, 0, transform.localEulerAngles.z);
    //}


    //// Update is called once per frame
    //private void Update()
    //{

    //    // Move Enemy
    //    Move();
    //}

    //// Method that actually make Enemy walk
    //private void Move()
    //{
    //    // If Enemy didn't reach last waypoint it can move
    //    // If enemy reached last waypoint then it stops
    //    if (waypointIndex <= waypoints.Length - 1)
    //    {
    //        agent.SetDestination(waypoints[waypointIndex].transform.position);

    //        Vector3 distanceToWalkPoint = transform.position - waypoints[waypointIndex].transform.position;

    //        //Walkpoint reached
    //        if (distanceToWalkPoint.magnitude < 1f)
    //            waypointIndex += 1;
    //        //walkPointSet = false;

    //        ////------------------------------------------
    //        //// Move Enemy from current waypoint to the next one
    //        //// using MoveTowards method
    //        //transform.position = Vector3.MoveTowards(transform.position,
    //        //   waypoints[waypointIndex].transform.position,
    //        //   moveSpeed * Time.deltaTime);

    //        ////spotLight.transform.LookAt(waypoints[waypointIndex].transform.position);

    //        //IncreaseWaypointIndex();
    //        ////------------------------------------------------------


    //        //// If Enemy reaches position of waypoint he walked towards
    //        //// then waypointIndex is increased by 1
    //        //// and Enemy starts to walk to the next waypoint
    //        //if (transform.position == waypoints[waypointIndex].transform.position)
    //        //{
    //        //    Debug.Log("TRRUUEE");
    //        //    waypointIndex += 1;
    //        //}
    //    }
    //    else
    //    {
    //        transform.position = new Vector3(-13.8f, 0.34f, 1.36f);
    //        //waypointIndex = 0;
    //    }
    //}


    //IEnumerator TurnToFace(Transform objectTransform, Vector3 lookTarget)
    //{
    //    Vector3 dirToLookTarget = (lookTarget - objectTransform.position).normalized;
    //    float targetAngle = 90 - Mathf.Atan2(dirToLookTarget.z, dirToLookTarget.x) * Mathf.Rad2Deg;

    //    while (Mathf.DeltaAngle(transform.eulerAngles.y, targetAngle) > 0.05f)
    //    {
    //        float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetAngle, turnSpeed * Time.deltaTime);
    //        transform.eulerAngles = Vector3.up * angle;

    //        yield return null;
    //    }
    //}

    //private void IncreaseWaypointIndex(){


    //    // If Enemy reaches position of waypoint he walked towards
    //    // then waypointIndex is increased by 1
    //    // and Enemy starts to walk to the next waypoint
    //    if (transform.position == waypoints[waypointIndex].transform.position)
    //    {
    //        StartCoroutine(TurnToFace(spotLight.transform, waypoints[waypointIndex].transform.position));
    //        Debug.Log("TRRUUEE");
    //        waypointIndex += 1;
    //    }

    //}

    // -------------------------------------------------------------------
    //public LayerMask whatIsGround, whatIsPlayer;

    //public NavMeshAgent agent;

    //public Transform player;

    ////Patroling
    //public Vector3 walkPoint;
    //bool walkPointSet;
    //public float walkPointRange;

    ////Attacking
    //public float timeBetweenAttacks;
    //bool alreadyAttacked;
    ////public GameObject projectile;

    ////States
    //public float sightRange, attackRange;
    //public bool playerInSightRange, playerInAttackRange;

    //private void Awake()
    //{
    //    agent = GetComponent<NavMeshAgent>();
    //}

    //private void Update()
    //{
    //    //Check for sight and attack range
    //    playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
    //    playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

    //    if (!playerInSightRange && !playerInAttackRange) Patroling();
    //    if (playerInSightRange && !playerInAttackRange) ChasePlayer();
    //    if (playerInAttackRange && playerInSightRange) AttackPlayer();
    //}

    //private void Patroling()
    //{
    //    if (!walkPointSet) SearchWalkPoint();

    //    if (walkPointSet)
    //        agent.SetDestination(walkPoint);

    //    Vector3 distanceToWalkPoint = transform.position - walkPoint;

    //    //Walkpoint reached
    //    if (distanceToWalkPoint.magnitude < 1f)
    //        walkPointSet = false;
    //}

    //private void SearchWalkPoint()
    //{
    //    //Calculate random point in range
    //    float randomZ = Random.Range(-walkPointRange, walkPointRange);
    //    float randomX = Random.Range(-walkPointRange, walkPointRange);

    //    walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

    //    if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
    //        walkPointSet = true;
    //}

    //private void ChasePlayer()
    //{
    //    agent.SetDestination(player.position);
    //}

    //private void AttackPlayer()
    //{
    //    //Make sure enemy doesn't move
    //    agent.SetDestination(transform.position);
    //}

    //private void OnTriggerEnter(Collider collision)
    //{
    //    if (collision.CompareTag("Player"))
    //    {
    //        Debug.Log("Game Over");
    //        //playerGO = collision.gameObject;
    //        //playerPrevPosition = playerGO.transform.position;


    //        //// show prompt

    //        //// enable hiding
    //        //hideEnabled = true;
    //    }
    //}








    // ------------------------------------------------------------------------------------------



    //public float speed = 5;
    //public float waitTime = .3f;
    //public float turnSpeed = 90;

    //public Transform pathHolder;




    //void Start()
    //{

    //    Vector3[] waypoints = new Vector3[pathHolder.childCount];
    //    for (int i = 0; i < waypoints.Length; i++)
    //    {
    //        waypoints[i] = pathHolder.GetChild(i).position;
    //        waypoints[i] = new Vector3(waypoints[i].x, transform.position.y, waypoints[i].z);
    //    }

    //    StartCoroutine(FollowPath(waypoints));

    //}

    //IEnumerator FollowPath(Vector3[] waypoints)
    //{
    //    transform.position = waypoints[0];

    //    int targetWaypointIndex = 1;
    //    Vector3 targetWaypoint = waypoints[targetWaypointIndex];
    //    transform.LookAt(targetWaypoint);

    //    while (true)
    //    {
    //        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint, speed * Time.deltaTime);
    //        if (transform.position == targetWaypoint)
    //        {
    //            targetWaypointIndex = (targetWaypointIndex + 1) % waypoints.Length;
    //            targetWaypoint = waypoints[targetWaypointIndex];
    //            yield return new WaitForSeconds(waitTime);
    //            yield return StartCoroutine(TurnToFace(targetWaypoint));
    //        }
    //        yield return null;
    //    }
    //}

    //IEnumerator TurnToFace(Vector3 lookTarget)
    //{
    //    Vector3 dirToLookTarget = (lookTarget - transform.position).normalized;
    //    float targetAngle = 90 - Mathf.Atan2(dirToLookTarget.z, dirToLookTarget.x) * Mathf.Rad2Deg;

    //    while (Mathf.DeltaAngle(transform.eulerAngles.y, targetAngle) > 0.05f)
    //    {
    //        float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetAngle, turnSpeed * Time.deltaTime);
    //        transform.eulerAngles = Vector3.up * angle;
    //        yield return null;
    //    }
    //}

    //void OnDrawGizmos()
    //{
    //    Vector3 startPosition = pathHolder.GetChild(0).position;
    //    Vector3 previousPosition = startPosition;

    //    foreach (Transform waypoint in pathHolder)
    //    {
    //        Gizmos.DrawSphere(waypoint.position, .3f);
    //        Gizmos.DrawLine(previousPosition, waypoint.position);
    //        previousPosition = waypoint.position;
    //    }
    //    Gizmos.DrawLine(previousPosition, startPosition);
    // }


    // --------------------------------------------------
    //public NavMeshAgent agent;

    //public Transform player;

    //public LayerMask whatIsGround, whatIsPlayer;

    ////Patroling
    //public Vector3 walkPoint;
    //bool walkPointSet;
    //public float walkPointRange;

    ////Attacking
    //public float timeBetweenAttacks;
    //bool alreadyAttacked;
    //public GameObject projectile;

    ////States
    //public float sightRange, attackRange;
    //public bool playerInSightRange, playerInAttackRange;

    //private void Awake()
    //{
    //    //player = GameObject.Find("PlayerObj").transform;
    //    agent = GetComponent<NavMeshAgent>();
    //}

    //private void Update()
    //{
    //    //Check for sight and attack range
    //    playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
    //    playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

    //    if (!playerInSightRange && !playerInAttackRange) Patroling();
    //    if (playerInSightRange && !playerInAttackRange) ChasePlayer();
    //    //if (playerInAttackRange && playerInSightRange) AttackPlayer();
    //}

    //private void Patroling()
    //{
    //    if (!walkPointSet) SearchWalkPoint();

    //    if (walkPointSet)
    //        agent.SetDestination(walkPoint);

    //    Vector3 distanceToWalkPoint = transform.position - walkPoint;

    //    //Walkpoint reached
    //    if (distanceToWalkPoint.magnitude < 1f)
    //        walkPointSet = false;
    //}
    //private void SearchWalkPoint()
    //{
    //    //Calculate random point in range
    //    float randomZ = Random.Range(-walkPointRange, walkPointRange);
    //    float randomX = Random.Range(-walkPointRange, walkPointRange);

    //    walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

    //    if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
    //        walkPointSet = true;
    //}

    //private void ChasePlayer()
    //{
    //    agent.SetDestination(player.position);
    //}
}
