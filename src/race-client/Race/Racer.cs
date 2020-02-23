namespace SSC.Client.Race
{
    public class Racer
    {
        public int RacerServerId = -1;

        public bool IsRacerAI
        {
            get
            {
                return RacerServerId == -1;
            }
        }

        public Racer(int serverId)
        {
            RacerServerId = serverId;
        }
    }
}
