using System;


namespace Spirograph_v1
{
    public class RPSciFiControlEvent
    {
        public string   ControlId { get; set; }

        public RPSciFiControlType ControlType { get; set; }

        public string   EventName { get; set; }

        public object   Value     { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.Now;


    }   // class RPSciFiControlEvent

}   // namespace Spirograph_v1
