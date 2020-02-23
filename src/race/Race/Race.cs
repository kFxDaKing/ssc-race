using System.Threading.Tasks;
using System.Collections.Generic;

using CitizenFX.Core;
using Newtonsoft.Json;

namespace SSRC
{
    public class Race
    {
        private string RaceName = "Unknown";
        private List<RaceStart> StartingPoints = new List<RaceStart>();
        private List<RaceCheckpoint> Checkpoints = new List<RaceCheckpoint>();
        private bool Debug = false;

        public Race(string name, bool debug)
        {
            RaceName = name;
            Debug = debug;
        }

        public async Task Update()
        {
            foreach (RaceCheckpoint checkpoint in Checkpoints)
            {
                await checkpoint.Render();
            }

            if (Debug)
            {
                foreach (RaceStart start in StartingPoints)
                {
                    await start.Render();
                }
            }
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

        public void AddPoint(RaceCheckpoint cp)
        {
            Checkpoints.Add(cp);
        }
    }
}
