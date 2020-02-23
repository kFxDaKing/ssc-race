using System.Collections.Generic;

namespace SSRC
{
    public class RaceJoinable
    {
        public string RaceId;
        public string TrackName;
        public string CarName;
        public int Laps; //TODO: use custom type to support lap AND timed races.
    }

    public class RaceCollection
    {
        private List<RaceJoinable> JoinableRaces = new List<RaceJoinable>();

        public bool IsRacePhysicallyPossible()
        {
            //Check if existing tracks don't clip into eachother.
            return true;
        }

        public void AddJoinableRace(string rid, string trackName, string carName, int laps)
        {
            bool ridExists = JoinableRaces.Exists((r) => r.RaceId == rid);
            //Assert that this RID isn't also an in-progress race RID.

            if (!ridExists)
            {
                RaceJoinable race = new RaceJoinable
                {
                    RaceId = rid,
                    TrackName = trackName,
                    CarName = carName,
                    Laps = laps
                };

                JoinableRaces.Add(race);
            }
        }
    }
}
