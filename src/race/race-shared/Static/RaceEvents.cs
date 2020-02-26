namespace SSC.Shared.Static
{
    /*
        Delegate naming scheme:
        <Event|Command><SRV|CL><Collection><MethodName>
    */

    public static class RaceStatic
    {
        public delegate void EventClientNotificationRejected(string action, string reason);
        public delegate void EventClientNotificationAccepted(string action);    
    }
}
