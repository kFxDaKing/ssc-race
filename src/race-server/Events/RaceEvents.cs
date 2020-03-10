using System.Collections.Generic;

using CitizenFX.Core;

using SSC.Shared.Util;
using SSC.Shared.Wrappers;

using static CitizenFX.Core.Native.API;
using static SSC.Shared.Static.RaceStatic;

namespace SSC.Server
{
    public class RaceEvents
    {
        //delegate void RaceHosted(Player player, string track, string car, int laps);
        //delegate void RaceJoined(Player player, string code);

        private Dictionary<string, RaceInstance> RaceInstances = new Dictionary<string, RaceInstance>();

        public RaceEvents()
        {
            RaceEventCollection ev = RaceServer.Instance.EventCollection;

            ev.RegisterEvent<EventServerRaceHosted<Player>>(OnRaceHosted);
            ev.RegisterEvent<EventServerRaceJoined<Player>>(OnRaceJoined);
        }

        public void OnRaceHosted([FromSource]Player player, string trackName, string carName, int laps)
        {
            Debug.WriteLine($"{nameof(RaceEvents)} - Hosting race with track {trackName} with car {carName} for {laps} laps.");

            if (laps <= 0)
            {
                //TODO: Notify the command invoker something went wrong.
                Debug.WriteLine($"^1 {nameof(RaceEvents)} - OnRaceHosted: Invalid lap count: {laps}");
                return;
            }

            //TODO: Do a check client-side to see if the vehicle requested is valid. Otherwise we don't annoy the server with it.
            //      Server can't check this unless we have JuanSink.

            string raceCode = StringUtil.GetRandomHexString(4);
            RaceInstance instance = new RaceInstance(trackName, carName, laps);

            //TODO: Check if the race isn't overlapping another track (IsRacePhysciallyPossible).
            RaceInstances.Add(raceCode, instance);

            Debug.WriteLine($"New race created race with RaceID {raceCode}");
        }

        public void OnRaceJoined([FromSource]Player player, string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                //TODO: Notify the client something went wrong.
                Debug.WriteLine($"^1 {nameof(RaceEvents)} -  OnRaceJoined: Invalid RID: {code}");
                return;
            }

            bool foundRace = RaceInstances.TryGetValue(code, out RaceInstance ri);

            if (!foundRace)
            {
                //TODO: Notify the client something went wrong.
                Debug.WriteLine($"^1 {nameof(RaceEvents)} -  OnRaceJoined: Could not find a race with RID {code}");
                return;
            }

            if (!ri.IsJoinable)
            {
                //TODO: Notify the client something went wrong.
                Debug.WriteLine($"^1 {nameof(RaceEvents)} -  OnRaceJoined: Race is already closed. RID: {code}");
                return;
            }

            ri.AssignPlayer(player);
        }
    }
}
 