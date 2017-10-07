using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace MM_Events.Controls
{
    public static class Event_Request_Control 
    {
        public static void SubmitRequest(int requestId)
        {
            var _request = GetRequestForId(requestId);

            if (ShouldAccept(_request))
                HandleAcceptRequest(requestId);
            else
            {
                var _sendTo = GetNextReceiver(_request["ReqResp"] as string);
                SetResponsibleForRequest(requestId, _sendTo);
            }
        }

        private static void HandleAcceptRequest(int requestId)
        {
            AcceptEvent(requestId);
            CloseEventRequest(requestId);
        }

        private static void CloseEventRequest(int requestId)
        {
            Data_Utilities.setEventRequestToClosed(requestId);
        }

        private static void AcceptEvent(int requestId)
        {
            Data_Utilities.setEventStatusToAccepted(requestId);
        }

        private static bool ShouldAccept(DataRow request)
        {
            return request["ReqResp"] == "SCS" && request["ReqStatus"] == "APPROVED";
        }

        private static void SetResponsibleForRequest(int requestId, string sendTo)
        {
            Data_Utilities.setResponsibleForRequest(requestId, sendTo);
        }

        private static DataRow GetRequestForId(int requestId)
        {
            return Data_Utilities.getRequest(requestId);
        }

        private static string GetNextReceiver(string responsible)
        {
            switch (responsible)
            {
                case "CS":
                    return "SCS";
                case "SCS":
                    return "FM";
                case "FM":
                    return "ADM";
                case "ADM":
                    return "SCS";
                default:
                    throw new ArgumentException("Responsible party {0} not implemented for event task", responsible);
            }
        }
    }
}