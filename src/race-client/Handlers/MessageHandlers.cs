using System;
using CitizenFX.Core;
using SSC.Client.Util;

namespace SSC.Client.Handlers
{
    public class MessageHandlers
    {
        public MessageHandlers(EventHandlerDictionary events)
        {
            events.Add("ssrc.race::rejected", new Action<string, string>(OnActionRejected));
        }
        
        public void OnActionRejected(string action, string reason)
        {
            ChatHelper.SendMessage("^2RaceClient", $"^1Action {action} was rejected, reason: {reason}");
        }
    }
}
