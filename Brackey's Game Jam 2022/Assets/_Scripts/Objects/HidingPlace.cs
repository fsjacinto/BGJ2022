using UnityEngine;

public class HidingPlace : InteractableObject
{
    [SerializeField] private SpriteRenderer playerSR;
    [SerializeField] private Collider playerCollider;
    [SerializeField] private Rigidbody playerRB;

    protected override void Update() {
        if (!CheckInteractInput()) return;

        if (CheckInteractConditions()) {
            GameManager.Instance.UpdateGameState(GameState.Hiding);
            Hide();
        }
        else {
            GameManager.Instance.UpdateGameState(GameState.Exploration);
            Unhide();
        }
    }

    private void Unhide() {
        playerSR.enabled = true;
        playerCollider.enabled = true;
        playerRB.constraints = ~RigidbodyConstraints.FreezePosition;
    }

    private void Hide() {
        playerSR.enabled = false;
        playerCollider.enabled = false;
        playerRB.constraints = RigidbodyConstraints.FreezePosition;
    }
}
