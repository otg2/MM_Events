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
                SendTaskToSubteam(taskId, nextStatus, requestedBudget, comment);
            }
            else if (nextStatus == "PENDING FINANCIAL REQUEST")
            {
                var supervisor = GetSupervisor(task);
                SendTaskToSupervisor(taskId, supervisor, nextStatus, requestedBudget, comment);
            }
            else
            {
                SetTaskToInProgress(taskId, nextStatus, requestedBudget, comment);
            }

            SetTaskStatus(taskId, nextStatus);
        }

        private static string GetSupervisor(DataRow task)
        {
            return Data_Utilities.GetUserRole(task["TaskCreator"] as string);
        }

        public static void CancelTask(int taskId)
        {
            CloseTask(taskId);
        }

        public static void TaskFinished(int taskId)
        {
            SetTaskAsFinished(taskId);
        }

        private static void SetTaskAsFinished(int taskId)
        {
            Data_Utilities.SetTaskStatusToFinished(taskId);
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

        private static void SendTaskToSupervisor(int taskId, string supervisor, string nextStatus, decimal budget, string comment)
        {
            Data_Utilities.SetTaskResponsible(taskId, supervisor);
            Data_Utilities.SetTaskExtraBudget(taskId, budget);
            Data_Utilities.SetTaskStatus(taskId, nextStatus);
            Data_Utilities.SetTaskExtraComment(taskId, comment);
        }

        private static void SendTaskToSubteam(int taskId, string nextStatus, decimal requiredBudget, string comment)
        {
            Data_Utilities.SetTaskResponsible(taskId, "");
            Data_Utilities.SetTaskStatus(taskId, nextStatus);
            Data_Utilities.SetTaskExtraComment(taskId, comment);
            if (requiredBudget > 0)
            {
                Data_Utilities.SetTaskExtraBudget(taskId, 0m);
                Data_Utilities.SetTaskBudget(taskId, requiredBudget);
            }
        }
    }
}