using SSC.Client.Util;

using SSC.Shared.Wrappers;
using static SSC.Shared.Static.RaceStatic;

namespace SSC.Client.Events
{
    public class NotificationEvents
    {
        public NotificationEvents()
        {
            RaceEventCollection ev = RaceClient.Instance.EventCollection;

            ev.RegisterEvent<EventClientNotificationRejected>(OnActionRejected);
        }
        
        public void OnActionRejected(string action, string reason)
        {
            ChatHelper.SendMessage("^2RaceClient", $"^1Action {action} was rejected, reason: {reason}");
        }
    }
}
