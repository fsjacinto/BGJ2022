using UnityEngine;

namespace DialogueSystem {
    [CreateAssetMenu(fileName = "ActorInfo", menuName = "Dialogue/ActorInfo")]
    public class ActorInfoSO : ScriptableObject {
        public string actorName;
        public Sprite actorSprite;
    }
}