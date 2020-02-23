using System;
using CitizenFX.Core;
using SSC.Client.Util;
using SSC.Shared.Wrappers;

namespace SSC.Client.Handlers
{
    public class MessageHandlers
    {
        public delegate void MessageActionRejected(string action, string reason);

        public MessageHandlers()
        {
            RaceEventCollection ev = RaceClient.Instance.EventCollection;
            ev.RegisterEvent<MessageActionRejected>(OnActionRejected);
        }
        
        public void OnActionRejected(string action, string reason)
        {
            ChatHelper.SendMessage("^2RaceClient", $"^1Action {action} was rejected, reason: {reason}");
        }
    }
}
