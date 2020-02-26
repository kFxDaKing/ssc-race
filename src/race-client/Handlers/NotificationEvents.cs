using System;
using CitizenFX.Core;
using SSC.Client.Util;
using SSC.Shared.Wrappers;

using SSC.Shared.Static;

namespace SSC.Client.Handlers
{
    public class NotificationEvents
    {
        public NotificationEvents()
        {
            RaceEventCollection ev = RaceClient.Instance.EventCollection;
            ev.RegisterEvent<RaceStatic.EventClientNotificationRejected>(OnActionRejected);
        }
        
        public void OnActionRejected(string action, string reason)
        {
            ChatHelper.SendMessage("^2RaceClient", $"^1Action {action} was rejected, reason: {reason}");
        }
    }
}
