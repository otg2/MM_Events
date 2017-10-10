using System;
using System.Data;

namespace MM_Events.Controls
{
    public static class FinancialRequestControl
    {
        public static void SubmitFinancialRequest(int requestId, bool approved)
        {
            var task = GetTaskForFinancialRequest(requestId);
            var responsible = task["TaskTeam"] as string;


            SubmitRequest(requestId, responsible, approved);
        }

        private static void SubmitRequest(int requestId, string responsible, bool approved)
        {
            Data_Utilities.SetResponsibleForRequest(requestId, responsible);
            if (approved)
                Data_Utilities.SetRequestStatus(requestId, "APPROVED");
            else
                Data_Utilities.SetRequestStatus(requestId, "REJECTED");
        }

        private static DataRow GetTaskForFinancialRequest(int requestId)
        {
            return Data_Utilities.GetTaskForRequest(requestId);
        }
    }
}