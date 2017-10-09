using System;
using System.Data;

namespace MM_Events.Controls
{
    public static class TaskControl
    {
        public static void SubmitTask(int taskId, decimal budget, string comment)
        {
            var task = GetTaskForId(taskId);

            var nextStatus = GetNextTaskStatus(task["TaskStatus"] as string,Convert.ToDecimal(task["TaskBudget"]), budget);

            SetTaskStatus(taskId, nextStatus);
        }

        public static void CancelTask(int taskId)
        {
            CloseTask(taskId);
        }

        private static void CloseTask(int taskId)
        {
            Data_Utilities.CloseTask(taskId);
        }

        private static string GetNextTaskStatus(string taskStatus, decimal budget, decimal requestedBudgtet)
        {
            if (taskStatus == null)
            {
                throw new ArgumentNullException("Task has invalid status type");
            }

            if (taskStatus == "PENDING")
            {
                if (requestedBudgtet > 0m)
                {
                    return "PENDING FINANCIAL REQUEST";
                }
                else
                {
                    return "IN PROGRESS";
                }
            }
            else
            {
                return "PENDING";
            }
        }

        private static DataRow GetTaskForId(int taskId)
        {
            return Data_Utilities.GetTask(taskId);
        }

        private static void SetTaskStatus(int taskId, string nextStatus)
        {
            Data_Utilities.SetTaskStatus(taskId, nextStatus);
        }
    }
}