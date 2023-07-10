using System.Threading.Tasks;
using UnityEngine;

public class ExitDoor : EnvironmentObject, IInteractable {
    protected bool isLocked;

    [field: SerializeField] public GameObject enter { get; private set; } // the enter gameobject where the entity will teleport to
    
    // SFX
    private string openingDoorSFX = "Door_Open";
    private string closingDoorSFX = "Door_Close";

    private void Update() {
        if (CheckInteractConditions()) {
            Interact();
        }
    }

    protected override void OnTriggerEnter(Collider collider) {
        base.OnTriggerEnter(collider);
        collidingGO = collider.gameObject;

        if (collider.CompareTag("Enemy")) {
            RoomTraversal.Instance.EnemyTraverse(collidingGO.transform, enter);
        }
    }

    public void IsLocked(bool locked) {
        isLocked = locked;
    }

    public bool CheckInteractConditions() {
        if (Input.GetKeyDown(KeyCode.Space) &&
            isPlayerColliding &&
            GameManager.Instance.currentState == GameState.Exploration)
            return true;
        return false;
    }

    public async virtual void Interact() {
        if (!isLocked) {
            AudioManager.Instance.Play(openingDoorSFX);

            await RoomTraversal.Instance.PlayerTraverse(collidingGO.transform, enter);

            AudioManager.Instance.Play(closingDoorSFX);
        }
    }
}
