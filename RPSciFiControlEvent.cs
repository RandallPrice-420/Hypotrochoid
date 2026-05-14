using System;


namespace Spirograph_v1
{
    public class RPSciFiControlEvent
    {
        // ---------------------------------------------------------------------
        // Public Properties:
        // ------------------
        //   ControlId
        //   ControlType
        //   EventName
        //   Value
        //   Timestamp
        // ---------------------------------------------------------------------

        #region .  Public Properties  .

        public string   ControlId { get; set; }

        public RPSciFiControlType ControlType { get; set; }

        public string   EventName { get; set; }

        public object   Value     { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.Now;

        #endregion


    }   // class RPSciFiControlEvent

}   // namespace Spirograph_v1
