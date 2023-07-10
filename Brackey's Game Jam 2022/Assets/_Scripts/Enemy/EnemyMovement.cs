using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Rigidbody enemyRigidBody;
    private Collider enemyCollider;
    private Vector3 moveDirection;

    [Header("Stairs Movement")]
    [SerializeField] GameObject stepRayUpper;
    [SerializeField] GameObject stepRayLower;
    [SerializeField] float stepHeight;
    [SerializeField] float stepSmooth;

    [Header("Enemy Configuration")]
    [SerializeField] private float enemySpeed;

    private void Awake() {
        enemyRigidBody = GetComponent<Rigidbody>();
        enemyCollider = GetComponent<Collider>();
    }

    void FixedUpdate() {
        if (GameManager.Instance.currentState != GameState.Exploration && 
            GameManager.Instance.currentState != GameState.Hiding) return;
        StepClimb();

        if (moveDirection != Vector3.zero) {
            enemyRigidBody.MovePosition(transform.position + moveDirection.normalized * enemySpeed * Time.deltaTime);
        }
    }

    private void StepClimb() {
        RaycastHit hitLower;

        if (Physics.Raycast(stepRayLower.transform.position, transform.TransformDirection(Vector3.back), out hitLower, 0.2f)) {
            RaycastHit hitUpper;
            if (!Physics.Raycast(stepRayUpper.transform.position, transform.TransformDirection(Vector3.back), out hitUpper, 0.2f)) {
                enemyRigidBody.position -= new Vector3(0f, -stepSmooth, 0f);
            }
        }
    }

    public void SetMoveDirection(Vector3 direction) {
        moveDirection = direction;
    }

    public Vector3 GetMoveDirection() {
        return moveDirection;
    }

    public void EnableEnemyMovement() {
        enemyCollider.enabled = true;
        enemyRigidBody.constraints = ~RigidbodyConstraints.FreezePosition;
    }

    public void DisableEnemyMovement() {
        enemyCollider.enabled = false;
        enemyRigidBody.constraints = RigidbodyConstraints.FreezePosition;
    }
}
