using UnityEngine;

public class EnemyPathManager : MonoBehaviour
{
    [SerializeField] private EnemyAI enemyAI;
    private EnemyMovement enemyMovement;
    private EnemyFlashlight enemyFlashlight;
    private int waypointIndex = 0;
    public bool waypointReached = false;

    // Array of waypoints to walk from one to the next one
    [SerializeField] private Transform[] waypoints;

    private void Awake() {
        enemyMovement = enemyAI.enemyMovement;
        enemyFlashlight = enemyAI.enemyFlashlight;
    }

    private void Update() {
        // Move enemy if gamestate is Exploration or hiding
        if (GameManager.Instance.currentState != GameState.Exploration && 
            GameManager.Instance.currentState != GameState.Hiding) return;

        if (enemyAI.enemyState != EnemyState.Patrolling) return;

        MoveThroughWaypoints();

        // Flashlight
        enemyFlashlight.RotateToDirection(enemyMovement.GetMoveDirection());
    }

    private void MoveThroughWaypoints() {
        // If waypoint is not yet reached, move enemy to waypoint
        if (!waypointReached) {
            enemyMovement.SetMoveDirection(waypoints[waypointIndex].transform.position - enemyAI.transform.position);

            // If player is close to waypoint. stop movement
            if (Mathf.Abs(enemyMovement.GetMoveDirection().x) < 0.1f && Mathf.Abs(enemyMovement.GetMoveDirection().z) < 0.1f) {
                enemyMovement.SetMoveDirection(Vector3.zero);
                waypointReached = true;
            }
        }
        // when reached, wait for enemy to finish room traversal before moving to next waypoint
        else {             
            if (!RoomTraversal.Instance.isEnemyTraversing) {
                waypointReached = false;

                // Loop waypointIndex
                if (waypointIndex == waypoints.Length - 1)
                    waypointIndex = 0;
                else
                    waypointIndex++;
            }
        }
    }
}
