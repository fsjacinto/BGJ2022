using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private Transform enemyModel;
    public EnemyMovement enemyMovement { get; private set; }
    public EnemyState enemyState;
    [field:SerializeField] public EnemyFlashlight enemyFlashlight { get; private set; }
    

    private void Awake() {
        enemyMovement = GetComponent<EnemyMovement>();
    }

    private void Start()
    {
        ChangeState(EnemyState.Asleep);
    }

    public void ChangeState(EnemyState state) {
        if (state == enemyState) return;

        enemyState = state;

        switch (state) {
            case EnemyState.Asleep:               
                enemyMovement.DisableEnemyMovement();
                ToggleEnemyVisibility(false);
                break;
            case EnemyState.Patrolling:  
                enemyMovement.EnableEnemyMovement();
                ToggleEnemyVisibility(true);
                break;
        }
    }

    public void StartPatrol() {
        ChangeState(EnemyState.Patrolling);
    }

    public void ToggleEnemyVisibility(bool visible) {
        enemyModel.gameObject.SetActive(visible);
    }
}
