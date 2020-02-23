namespace SSRC
{
    public class RaceEventHandlers
    {
        public void OnRaceAnnounced(string raceId)
        {
            ChatHelper.SendMessage("A race", $"A race has been announced, type `/join {raceId}` to join the race.");
        }
    }
}
