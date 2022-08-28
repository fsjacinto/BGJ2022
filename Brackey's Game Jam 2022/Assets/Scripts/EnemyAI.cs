using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Rigidbody enemyRB;
    public Collider enemyCOL;
    public GameObject enemySpriteGO;
    public GameObject enemyLightGO;
    //private NavMeshAgent agent;

    [SerializeField] private float enemySpeed = 5f;

    // Array of waypoints to walk from one to the next one
    [SerializeField] private Transform[] waypoints;

    //
    private int waypointIndex = 0;
    private Vector3 direction;
    private bool isDone = false;
    public bool traversed = false;
    public EnemyState enemyState;
    private bool isMovingToLast;

    public Location enemyLocation;

    [Header("Enemy Stairs")]
    [SerializeField] GameObject stepRayUpper;
    [SerializeField] GameObject stepRayLower;
    [SerializeField] float stepHeight = 0.2f;
    [SerializeField] float stepSmooth = 0.1f;

    // Flashlight Rotation
    [SerializeField] private GameObject flashLight;
    private float desiredRot;
    public float rotSpeed = 250;
    public float damping = 10;

    public enum EnemyState
    {
        Patrolling,
        Asleep
    }

    private void Awake()
    {
        desiredRot = flashLight.transform.eulerAngles.y;
    }

    private void Start()
    {
        enemyRB = GetComponent<Rigidbody>();
        //agent = GetComponent<NavMeshAgent>();
        enemySpriteGO.SetActive(false);
        enemyLightGO.SetActive(false);
        enemyCOL.enabled = false;
        enemyRB.constraints = RigidbodyConstraints.FreezePosition;


        isMovingToLast = true;
        enemyState = EnemyState.Asleep;
        stepRayUpper.transform.position = new Vector3(stepRayLower.transform.position.x, stepHeight, stepRayUpper.transform.position.z);
    }

    void FixedUpdate()
    {
        if (GameManager.instance.currentState != GameState.Exploration && GameManager.instance.currentState != GameState.Hiding) return; 

        StepClimb();

        if(enemyState == EnemyState.Patrolling)

        if (direction != Vector3.zero)
        {
            enemyRB.MovePosition(transform.position + direction.normalized * enemySpeed * Time.deltaTime);
        }  
    }

    private void Update()
    {
        if (GameManager.instance.currentState != GameState.Exploration && GameManager.instance.currentState != GameState.Hiding) return;

        /// Patrolling
        if (enemyState == EnemyState.Patrolling)
        {
            // Going from Wa
            if (waypointIndex <= waypoints.Length - 1 && isMovingToLast)
            {
                isMovingToLast = true;

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

            // Flashlight Rotate
            Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
            flashLight.transform.rotation = Quaternion.RotateTowards(flashLight.transform.rotation, toRotation, rotSpeed * Time.deltaTime);
        }

    }

    private void StepClimb()
    {
        RaycastHit hitLower;

        if (Physics.Raycast(stepRayLower.transform.position, transform.TransformDirection(Vector3.back), out hitLower, 0.2f))
        {
            RaycastHit hitUpper;
            if (!Physics.Raycast(stepRayUpper.transform.position, transform.TransformDirection(Vector3.back), out hitUpper, 0.2f))
            {
                enemyRB.position -= new Vector3(0f, -stepSmooth, 0f);
            }
        }
    }

    public void ChangeLocation(Location location)
    {
        enemyLocation = location;

        int layerToChange = LayerMask.NameToLayer(location.ToString());
        gameObject.layer = layerToChange;
    }
}
