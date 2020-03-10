using CitizenFX.Core;
using SSC.Client.Util;
using SSC.Shared.Wrappers;
using System;

namespace SSC.Client.Commands
{
    public class CreatorCommands
    {
        public CreatorCommands()
        {
            RaceCommandCollection cc = RaceClient.Instance.CommandCollection;

            RaceCommandDefinition cmdCreatorStart = cc.Create()
                .AddCommandName("creator", "start")
                .AddSuccessCallback(new Action<string>(OnCreatorStart))
                .AddFailedCallback(new Action<string>(OnCreatorStartFailed))
                .AddParam<string>("track_name", new RaceCommandCheckArgs { Min = 3, Max = 32 });

            cc.Register(cmdCreatorStart);
        }

        public void OnCreatorStart(string motd)
        {
            //Talk to some shared state that sets up a mock race object?
        }

        public void OnCreatorStartFailed(string reason) //FAILED
        {
            ChatHelper.SendMessage(nameof(CreatorCommands), $"Command failed: {reason}", 255, 0, 0);
        }
    }
}
