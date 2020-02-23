using System.Threading.Tasks;
using System.Collections.Generic;

using CitizenFX.Core;
using Newtonsoft.Json;

namespace SSRC
{
    public enum RaceState
    {
        Loading,
        Gridding,
        Started,
        Finishing,
        Closed
    }

    public class Race
    {
        private string RaceName = "Unknown";

        private List<RaceStart> StartingPoints = new List<RaceStart>();
        private List<RaceCheckpoint> Checkpoints = new List<RaceCheckpoint>();
        private List<Racer> Racers = new List<Racer>();

        private bool Debug = false;

        public Race(string name, bool debug)
        {
            RaceName = name;
            Debug = debug;
        }

        public async Task CreatorUpdate()
        {
            foreach (RaceCheckpoint checkpoint in Checkpoints)
            {
                await checkpoint.Render();
            }

            if (Debug)
            {
                foreach (RaceStart start in StartingPoints)
                {
                    start.Render();
                }
            }
        }

        public async Task RaceUpdate()
        {
            await BaseScript.Delay(0);
        }

        public void Load()
        {
        }

        public void Save()
        {
            object payloadObject = new object[] {
                StartingPoints,
                Checkpoints
            };

            BaseScript.TriggerServerEvent("ssrc.race::savetrack", RaceName, JsonConvert.SerializeObject(payloadObject));
        }

        public void AddCheckpoint(RaceCheckpoint cp)
        {
            Checkpoints.Add(cp);
        }

        public void AddStart(RaceStart start)
        {
            StartingPoints.Add(start);
        }
    }
}
