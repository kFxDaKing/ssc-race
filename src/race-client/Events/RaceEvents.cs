using SSC.Client.Util;

using SSC.Shared.Wrappers;
using static SSC.Shared.Static.RaceStatic;

namespace SSC.Client.Events
{
    public class RaceEvents
    {
        public RaceEvents()
        {
            RaceEventCollection ev = RaceClient.Instance.EventCollection;

            ev.RegisterEvent<EventClientRaceAnnounced>(OnRaceAnnounced);
        }

        public void OnRaceAnnounced(string raceId)
        {
            ChatHelper.SendMessage("A race", $"A race has been announced, type `/join {raceId}` to join the race.");
        }
    }
}
