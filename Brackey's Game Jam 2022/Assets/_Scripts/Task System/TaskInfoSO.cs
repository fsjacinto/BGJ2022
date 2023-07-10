using UnityEngine;

namespace TaskSystem {
    [CreateAssetMenu(fileName = "TaskInfo", menuName = "Task/TaskInfo")]
    public class TaskInfoSO : ScriptableObject {
        //public int taskID;      // unique id
        public string taskName; // display name
    }
}