using System;

namespace SSC.Shared.Wrappers
{
    public delegate void RaceEventProxyFunction(string eventName, Delegate proxy);

    public class RaceEventCollection
    {
        private string PROJECT_NAME = "ssc-racing";
        private string MODULE_NAME = "race";

        private RaceEventProxyFunction proxyFunction;

        public RaceEventCollection(RaceEventProxyFunction proxy)
        {
            proxyFunction = proxy;
        }

        public void RegisterEvent<T>(T delegateInstance) where T : Delegate
        {
            string methodName = typeof(T).Name;
            string eventName = $"{PROJECT_NAME}::{MODULE_NAME}::{methodName}";
            proxyFunction?.Invoke(eventName, delegateInstance);
        }
    }
}
