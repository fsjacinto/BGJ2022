using System.Collections.Generic;
using UnityEngine;
using DialogueSystem;

namespace TaskSystem {

    public class TaskObject : EnvironmentObject, IInteractable {
        [SerializeField] private int taskIndex;

        // Dialogues
        [SerializeField] private DialogueEvent noAccessDialogue;
        [SerializeField] private DialogueEvent yesAccessDialogue;
        [SerializeField] private DialogueEvent finishedDialogue;

        // Requirements
        [SerializeField] private List<int> prereqList;

        public bool CheckInteractConditions() {
            throw new System.NotImplementedException();
        }

        public void Interact() {
            throw new System.NotImplementedException();
        }
    }
}
