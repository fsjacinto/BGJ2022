using UnityEngine;

namespace TaskSystem {
    [System.Serializable]
    public class TaskItem {
        public string taskName;
        [HideInInspector] public bool isFinished = false;
        public TaskProgress taskProgress;
    }
}