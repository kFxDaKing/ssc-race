using System;

namespace race_shared.Wrappers
{
    public delegate void EventWrapperBackingFunction(string eventName, Delegate backingFunction);

    public class EventWrapper
    {
        private string PROJECT_NAME = "ssc-racing";
        private string MODULE_NAME = "race";

        private EventWrapperBackingFunction backingFunction;

        public EventWrapper(EventWrapperBackingFunction backingFunc)
        {
            backingFunction = backingFunc;
        }

        public void RegisterEvent<T>(T delegateInstance) where T : Delegate
        {
            string methodName = typeof(T).Name;
            string eventName = $"{PROJECT_NAME}::{MODULE_NAME}::{methodName}";
            backingFunction?.Invoke(eventName, delegateInstance);
        }
    }
}
