using UnityEngine;
using System.Threading.Tasks;

public class RoomTraversal : Singleton<RoomTraversal>
{
    private bool isPlayerTraversing = false;
    public bool isEnemyTraversing = false;

    public async Task PlayerTraverse(Transform player, GameObject enter) {
        if(isPlayerTraversing) { return; }

        isPlayerTraversing = true;
        TransitionManager.Instance.TriggerRoomTransition();
        await Task.Delay(500);
        player.GetComponent<PlayerMovement>().PlayerFaceTo(enter.GetComponent<EnterDoor>().faceDirection); // player faces the exit's face direction
        player.position = enter.transform.position;

        isPlayerTraversing = false;
    }

    public async void EnemyTraverse(Transform enemy, GameObject enter) {
        if (isEnemyTraversing) { return; }
        isEnemyTraversing = true;

        await Task.Delay(500);
        enemy.position = enter.transform.position;

        isEnemyTraversing = false;
    }
}
