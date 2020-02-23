using System;

namespace SSRC
{
    public static class EventWrapper
    {
        static string PROJECT_NAME = "ssc-racing";
        static string MODULE_NAME = "race";

        public static void RegisterEvent<T>(T delegateInstance) where T : Delegate
        {
            string methodName = typeof(T).Name;
            string eventName = $"{PROJECT_NAME}::{MODULE_NAME}::{methodName}";
            RaceServer.Instance.RegisterEvent(eventName, delegateInstance);
        }

    }
}
