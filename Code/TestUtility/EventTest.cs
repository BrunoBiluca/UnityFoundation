using System;

namespace UnityFoundation.TestUtility
{
    public class EventTest
    {
        public static EventTest Create(object sender, string eventName)
        {
            return new EventTest(sender, eventName);
        }

        public bool WasTriggered { get; private set; }
        public int TriggerCount { get; private set; }

        public EventTest(object sender, string eventName)
        {
            WasTriggered = false;
            TriggerCount = 0;
            SubscribeToEvent(sender, eventName);
        }

        private void SubscribeToEvent(object sender, string eventName)
        {
            var eventInfo = sender.GetType().GetEvent(eventName);
            var handler = Delegate.CreateDelegate(
                eventInfo.EventHandlerType, this, nameof(HandleTrigger)
            );
            eventInfo.AddEventHandler(sender, handler);
        }

        private void HandleTrigger()
        {
            WasTriggered = true;
            TriggerCount++;
        }
    }
}