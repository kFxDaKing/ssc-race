using CitizenFX.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static CitizenFX.Core.Native.API;

namespace SSRC
{
    public class RaceClient : BaseScript
    {
        private Race CurrentRace;

        public RaceClient()
        {
            Tick += OnClientUpdate;
        }

        private async Task OnClientUpdate()
        {
            if (CurrentRace == null)
            {
                await Task.FromResult(0);
                return;
            }

            await CurrentRace.Update();
            await Task.FromResult(0);
        }

        [Command("rc_create")]
        public void CommandCreateRace(int source, List<object> args, string raw)
        {
            if (CurrentRace != null)
            {
                ChatHelper.Print(nameof(RaceClient), "Race is already created!", 255, 0, 0);
                return;
            }

            if (args.Count < 1)
            {
                ChatHelper.Print(nameof(RaceClient), "Please specify a track name.", 255, 0, 0);
                return;
            }

            string raceName = (string)args[0];

            CurrentRace = new Race(raceName, true);
            ChatHelper.Print(nameof(RaceClient), "A new race has been created!", 0, 255, 0);
        }

        [Command("rc_cp")]
        public void CommandAddCheckpoint()
        {
            if (CurrentRace == null)
            {
                ChatHelper.Print(nameof(RaceClient), "Cannot add checkpoint if race doesn't exist.", 255, 0, 0);
                return;
            }

            Player localPlayer = LocalPlayer;
            Vector3 position = localPlayer.Character.Position;

            CurrentRace.AddPoint(new RaceCheckpoint(position));
        }

        [Command("rc_save")]
        public void CommandSaveRace()
        {
            if (CurrentRace == null)
            {
                ChatHelper.Print(nameof(RaceClient), "Cannot save track if race hasn't been created.", 255, 0, 0);
                return;
            }

            CurrentRace.Save();
        }


        [Command("rc_clear")]
        public void CommandClearRace()
        {
            CurrentRace = null;
        }
    }
}
