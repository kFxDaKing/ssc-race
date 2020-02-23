using System;

using CitizenFX.Core;
using static CitizenFX.Core.Native.API;

namespace SSRC
{
    public class RaceServer : BaseScript
    {
        private static RaceServer _instance;
        public static RaceServer Instance => _instance;

        private RaceEvents raceEvents;

        private readonly RaceCollection RaceCollection = new RaceCollection();

        public RaceServer()
        {
            _instance = this;

            raceEvents = new RaceEvents();

            Debug.WriteLine($"^5{nameof(RaceServer)} is running!^7");

            EventHandlers.Add("ssrc.race::savetrack", new Action<string, string>(
                (name, json) => SaveTrack(name, json))
            );

            EventHandlers.Add("ssrc.race::startRace", new Action<Player, string, string, int>(CreateRace));
        }

        public void RegisterEvent(string name, Delegate delegateInstance)
        {
            EventHandlers.Add(name, delegateInstance);
            Debug.WriteLine($"^2{nameof(RaceServer)} - Registered event: {name}^7");
        }

        public void SaveTrack(string name, string json)
        {
            Debug.WriteLine($"{nameof(RaceServer)} Saving new track: {name}");
            SaveResourceFile(GetCurrentResourceName(), $"tracks/{name}.json", json, -1);
        }

        //TODO: Assert that trackName is valid.
        //TODO: Assert that carName is a valid car name.
        public void CreateRace([FromSource] Player source, string trackName, string carName, int laps)
        {
            if (laps <= 0)
            {
                source.TriggerEvent("ssrc.race::rejected", "ssrc.race::startrace", "A race must have more than 0 laps.");
                return;
            }

            if (string.IsNullOrEmpty(carName))
            {
                source.TriggerEvent("ssrc.race::rejected", "ssrc.race::startrace", "No car type specified.");
                return;
            }


            //string contents = LoadResourceFile(GetCurrentResourceName(), $"tracks/{trackName}.json");

            //if (string.IsNullOrEmpty(contents))
            //{
            //    source.TriggerEvent("ssrc.race::rejected", "ssrc.race::startrace", "The requested track does not exists.");
            //    return;
            //}

            if (!RaceCollection.IsRacePhysicallyPossible())
            {
                source.TriggerEvent("ssrc.race::rejected", "ssrc.race::startrace", "A race is blocking the current track from being loaded.");
                return;
            }

            string raceId = StringHelper.GetRandomHexString(4);
            RaceCollection.AddJoinableRace(raceId, trackName, carName, laps);

            foreach (Player player in Players)
            {
                player.TriggerEvent("ssrc.race::announceRace", raceId);
            }

            Debug.WriteLine($"^2Started race with track {trackName}' with car type: '{carName}' for {laps} laps^7");
        }
    }
}
