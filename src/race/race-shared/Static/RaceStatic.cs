namespace SSC.Shared.Static
{
    /*
        Delegate naming scheme:
        <Event|Command><SRV|CL><Collection><MethodName>
    */

    public static class RaceStatic
    {
        #region Client
        public delegate void EventClientNotificationRejected(string action, string reason);
        public delegate void EventClientNotificationAccepted(string action);

        public delegate void EventClientRaceAnnounced(string raceId);
        #endregion

        #region Server

        public delegate void EventServerRaceHosted<T>(T player, string track, string car, int laps);
        public delegate void EventServerRaceJoined<T>(T player, string code);

        #endregion
    }
}
