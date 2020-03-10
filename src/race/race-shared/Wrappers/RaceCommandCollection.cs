using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SSC.Shared.Wrappers
{
    public delegate void RaceCommandRegisterProxy(string commandName, Delegate handler, bool restricted);
    public delegate void RaceCommandInvokable(int source, List<object> args, string raw);

    public struct RaceCommandCheckArgs
    {
        public int Min;
        public int Max;
    }

    public struct RaceCommandParam
    {
        public string Name;
        public Type Type;
        public RaceCommandCheckArgs CheckArgs;

        public RaceCommandParam(string name, Type type, RaceCommandCheckArgs checkArgs)
        {
            Name = name;
            Type = type;
            CheckArgs = checkArgs;
        }

        public bool IsParamValid(object theParam, out string reason)
        {
            reason = "OK";

            if (Type != theParam.GetType())
            {
                reason = $"Type mismatch, expected {Type.Name}, got {theParam.GetType().Name}";
            }
            else if (Type == typeof(string))
            {
                reason = CheckStringParam(theParam);
            }

            return reason == "OK";
        }

        public string CheckStringParam(object theParam)
        {
            string reason = "OK";
            string paramString = Convert.ToString(theParam);

            if (CheckArgs.Min > 0)
            {
                if (paramString.Length < CheckArgs.Min)
                {
                    reason = $"{Name} needs atleast {CheckArgs.Min} characters";
                }
            }

            if (CheckArgs.Max > 0)
            {
                if (paramString.Length >= CheckArgs.Max)
                {
                    reason = $"{Name} cannot exceed {CheckArgs.Max} characters";

                }
            }

            return reason;
        }
    }

    public class RaceCommandDefinition
    {
        public string BaseCommand;
        public string SubCommand;

        public Delegate OnCommandSuccess;
        public Delegate OnCommandFailed;

        public Dictionary<string, RaceCommandParam> Parameters;

        public RaceCommandDefinition()
        {
            Parameters = new Dictionary<string, RaceCommandParam>();
        }

        public RaceCommandDefinition AddCommandName(string baseCmd, string subCmd)
        {
            BaseCommand = baseCmd;
            SubCommand = subCmd;
            return this;
        }

        public RaceCommandDefinition AddSuccessCallback(Delegate onSuccessCb)
        {
            OnCommandSuccess = onSuccessCb;
            return this;
        }

        public RaceCommandDefinition AddFailedCallback(Delegate onFailedCb)
        {
            OnCommandFailed = onFailedCb;
            return this;
        }

        public RaceCommandDefinition AddParam<T>(string name, RaceCommandCheckArgs args)
        {
            Parameters.Add(name, new RaceCommandParam(
                name, typeof(T), args
            ));

            return this;
        }

        public RaceCommandDefinition AddSource()
        {
            if (!Parameters.ContainsKey("player"))
            {
                dynamic checkArgs = new {
                    PlayerSource = true
                };

                Parameters.Add("player", new RaceCommandParam(
                    "player", typeof(int), checkArgs
                ));
            }

            return this;
        }

        public void InvokeSuccess(object[] invokeParams)
        {
            OnCommandSuccess.DynamicInvoke(invokeParams);
        }

        public void InvokeFailed(string reason)
        {
            OnCommandFailed.DynamicInvoke(new[] { reason });
        }
    }

    public class RaceCommandCollection
    {
        private RaceCommandRegisterProxy registerProxy;
        private List<RaceCommandDefinition> commandDefinitions;

        public RaceCommandCollection(RaceCommandRegisterProxy registerP)
        {
            commandDefinitions = new List<RaceCommandDefinition>();

            registerProxy = registerP;
        }

        public RaceCommandDefinition Create() => new RaceCommandDefinition();

        public void Register(RaceCommandDefinition definition)
        {
            //TODO: Set the restricted/permissions parameter.
            registerProxy?.Invoke(definition.BaseCommand, new RaceCommandInvokable(ProcessCommandInvoke), false);
            commandDefinitions.Add(definition);
        }

        private void ProcessCommandInvoke(int source, List<object> args, string raw)
        {
            string baseCommand = raw.Split(' ')[0].Replace("/", "");

            if (args.Count < 1)
            {
                //Show base command usage.
                return;
            }

            string subCommand = (string)args[0];

            RaceCommandDefinition currentDefinition = null;

            foreach (RaceCommandDefinition def in commandDefinitions)
            {
                if (string.Compare(baseCommand, def.BaseCommand, true) == 0 &&
                    string.Compare(subCommand, def.SubCommand, true) == 0)
                {
                    currentDefinition = def;
                    break;
                }
            }

            if (currentDefinition != null)
            {
                bool isValid = true;
                string paramName = "";
                List<object> paramList = new List<object>();
                int paramCount = 1;

                if (currentDefinition.Parameters.Count > args.Count)
                {
                    //TODO: Move this to RaceCommandDefinition.
                    currentDefinition.OnCommandFailed.DynamicInvoke(new[] {
                        $"Parameter count doesn't match, expected {currentDefinition.Parameters.Count} params , got {args.Count} params."
                    });

                    return;
                }

                string paramInvalidReason = "";

                foreach (var paramKvp in currentDefinition.Parameters)
                {
                    paramName = paramKvp.Key;
                    var param = paramKvp.Value;
                    var arg = args[paramCount];

                    //Add player source.
                    if (string.Compare("player", param.Name) == 0)
                    {
                        paramList.Add(source);
                    }
                    //Else we copy over any of the args we got passed.
                    else if (param.IsParamValid(arg, out paramInvalidReason))
                    {
                        paramList.Add(arg);
                        paramCount++;
                    }

                    else
                    {
                        isValid = false;
                        break;
                    }
                }

                if (!isValid)
                {
                    currentDefinition.InvokeFailed(paramInvalidReason); 
                }
                else
                {
                    currentDefinition.InvokeSuccess(paramList.ToArray());
                }
            }
        }
    }
}
