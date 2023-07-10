using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TaskSystem {
    public class TaskManager : Singleton<TaskManager> {
        public static event Action OnNewTaskStarted;
        [SerializeField] private TaskInfoSO[] taskInfoSOList;
        [field: SerializeField] public List<TaskItem> taskList { get; private set; }
        [field: SerializeField] public int currentTaskIndex { get; private set; } = 0;

        private void Start() {
            SetTasks();
            UpdateTaskProgress(taskInfoSOList[0], TaskProgress.IN_PROGRESS);
        }

        private void SetTasks() {
            foreach(TaskInfoSO taskInfoSO in taskInfoSOList) {
                TaskItem taskItem = new TaskItem();
                taskItem.taskName = taskInfoSO.taskName;
                taskItem.taskProgress = TaskProgress.NOT_STARTED;
                taskList.Add(taskItem);
            }
        }

        public void FinishTask(TaskInfoSO taskInfoSO) {
            UpdateTaskProgress(taskInfoSO, TaskProgress.FINISHED);

            // Start Next Task
            if (currentTaskIndex >= taskList.Count - 1) return;

            currentTaskIndex++;
            UpdateTaskProgress(taskList[currentTaskIndex], TaskProgress.IN_PROGRESS);

            OnNewTaskStarted?.Invoke();
        }

        private void UpdateTaskProgress(TaskInfoSO taskInfoSO, TaskProgress progress) {
            GetTaskItem(taskInfoSO).taskProgress = progress;
        }

        private void UpdateTaskProgress(TaskItem taskItem, TaskProgress progress) {
            taskItem.taskProgress = progress;
        }

        public TaskItem GetTaskItem(TaskInfoSO taskInfoSO) {
            return taskList.Single(task => task.taskName == taskInfoSO.taskName);
        }
    }
}