using CitizenFX.Core;

namespace SSC.Server
{
    public enum RIState
    {
        Gridding,
        Countdown,
        InProgress,
        Closing,
        Closed
    }

    public struct RIMetadata
    {
        public string TrackName;
        public string CarName;
        public int Laps;

        public RIMetadata(string track, string car, int laps)
        {
            TrackName = track;
            CarName = car;
            Laps = laps;
        }
    }

    public class RIPlayer
    {
        public Player CfxPlayer;

        public RIPlayer(Player cfxPlayer)
        {
            CfxPlayer = cfxPlayer;
        }
    }

    public class RaceInstance
    {
        private RIMetadata raceMetadata;
        private RIState raceState = RIState.Gridding;

        public bool IsJoinable
        {
            get
            {
                return raceState == RIState.Gridding;
            }
        }

        public RaceInstance(string track, string car, int laps)
        {
            raceMetadata = new RIMetadata(
                track, car, laps
            );
        }

        public void AssignPlayer(Player player)
        {
        }
    }
}
