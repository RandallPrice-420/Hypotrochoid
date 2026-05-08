using System;
using System.Collections.Generic;


namespace Spirograph_v1
{
    public class RPSciFiControlBus
    {
        public event Action<RPSciFiControlEvent> OnEvent;

        private readonly Dictionary<string, IRPSciFiControl> _controls = [];


        public void Register(IRPSciFiControl control)
        {
            _controls.TryAdd(control.ControlId, control);

        }   // Register()


        public void Publish(string id, RPSciFiControlType type, string evt, object value = null)
        {
            OnEvent?.Invoke(new RPSciFiControlEvent
            {
                ControlId   = id,
                ControlType = type,
                EventName   = evt,
                Value       = value
            });

        }   // Publish()


    }   // class class RPSciFiControlBus

}   // namespace Spirograph_v1

