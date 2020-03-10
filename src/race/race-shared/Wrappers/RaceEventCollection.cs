using SSC.Shared.Util;
using System;

namespace SSC.Shared.Wrappers
{
    public delegate void RaceEventRegisterProxy(string eventName, Delegate proxy);
    public delegate void RaceEventTriggerProxy(string eventName, params object[] args);

    public class RaceEventCollection
    {
        private RaceEventRegisterProxy registerProxy;
        private RaceEventTriggerProxy triggerLocalProxy;
        private RaceEventTriggerProxy triggerRemoteProxy;

        public RaceEventCollection(RaceEventRegisterProxy proxy, RaceEventTriggerProxy localProxy, 
            RaceEventTriggerProxy remoteProxy)
        {
            registerProxy = proxy;
            triggerLocalProxy = localProxy;
            triggerRemoteProxy = remoteProxy;
        }

        public void RegisterEvent<T>(T delegateInstance) where T : Delegate
        {
            string eventName = StringUtil.GetEventName<T>();
            registerProxy?.Invoke(eventName, delegateInstance);
        }

        public void InvokeEvent<T>(bool isRemote, params object[] args) where T : Delegate
        {
            string eventName = StringUtil.GetEventName<T>();

            if (isRemote)
                triggerRemoteProxy(eventName, args);
            else
                triggerLocalProxy(eventName, args);
        }
    }
}
