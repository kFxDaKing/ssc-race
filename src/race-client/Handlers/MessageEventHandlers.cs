namespace SSRC
{
    public class MessageEventHandlers
    {
        public void OnActionRejected(string action, string reason)
        {
            ChatHelper.Print("RaceClient", $"Action {action} was rejected, reason: {reason}");
        }
    }
}
