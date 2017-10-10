using System;
using System.Data;

namespace MM_Events.Controls
{
    public static class TaskControl
    {
        public static void SubmitTask(int taskId, decimal requestedBudget, string comment)
        {
            var task = GetTaskForId(taskId);

            var nextStatus = GetNextTaskStatus(task["TaskStatus"] as string, Convert.ToDecimal(task["TaskBudget"]), requestedBudget);
            var subteam = task["TaskStatusMsg"] as string;
            var budget = Convert.ToDecimal(task["TaskBudget"]);

            if (nextStatus == "PENDING")
            {
                SendTaskToSubteam(taskId, nextStatus, budget, requestedBudget, subteam, comment);
            }
            else if (nextStatus == "PENDING FINANCIAL REQUEST")
            {
                SendTaskToSupervisor(taskId, nextStatus, requestedBudget, comment);
            }
            else
            {
                SetTaskToInProgress(taskId, nextStatus, requestedBudget, comment);
            }

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

        private static void SetTaskToInProgress(int taskId, string nextStatus, decimal budget, string comment)
        {
            Data_Utilities.SetTaskStatus(taskId, nextStatus);
        }

        private static void SendTaskToSupervisor(int taskId, string nextStatus, decimal budget, string comment)
        {
            Data_Utilities.SetTaskResponsible(taskId, "PM");
            Data_Utilities.SetTaskExtraBudget(taskId, budget);
            Data_Utilities.SetTaskStatus(taskId, nextStatus);
            Data_Utilities.SetTaskExtraComment(taskId, comment);
        }

        private static void SendTaskToSubteam(int taskId, string nextStatus, decimal budget, decimal requiredBudget, string subteam, string comment)
        {
            Data_Utilities.SetTaskResponsible(taskId, subteam);
            Data_Utilities.SetTaskStatus(taskId, nextStatus);
            Data_Utilities.SetTaskExtraComment(taskId, comment);
            if (requiredBudget > 0)
                Data_Utilities.SetTaskBudget(taskId, budget);
        }
    }
}