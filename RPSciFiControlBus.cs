using System;
using System.Collections.Generic;


namespace Spirograph_v1
{
    public class RPSciFiControlBus
    {
        // ---------------------------------------------------------------------
        // Public Events:
        // --------------
        //   OnEvent
        // ---------------------------------------------------------------------

        #region .  Public Events  .

        public event Action<RPSciFiControlEvent> OnEvent;

        #endregion



        // ---------------------------------------------------------------------
        // Private Variables:
        // ------------------
        //   _controls
        // ---------------------------------------------------------------------

        #region .  Private Properties  .

        private readonly Dictionary<string, IRPSciFiControl> _controls = [];

        #endregion



        // ---------------------------------------------------------------------
        // Public Methods:
        // ---------------
        //   Publish()
        //   Register()
        // ---------------------------------------------------------------------

        #region .  Publish()  .
        // ---------------------------------------------------------------------
        //   Method.......:  Publish()
        //   Description..:  
        //   Parameters...:  id    -
        //                   type  - 
        //                   evt   -
        //                   value -
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        public void Publish(string id, RPSciFiControlType type, string evt, object value = null)
        {
            OnEvent?.Invoke(new RPSciFiControlEvent
                               {    
                                   ControlId   = id,
                                   ControlType = type,
                                   EventName   = evt,
                                   Value       = value
                               }
                           );

        }   // Publish()
        #endregion


        #region .  Register()  .
        // ---------------------------------------------------------------------
        //   Method.......:  Register()
        //   Description..:  
        //   Parameters...:  control - the control to register.
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        public void Register(IRPSciFiControl control)
        {
            _controls.TryAdd(control.ControlId, control);

        }   // Register()
        #endregion


    }   // class class RPSciFiControlBus

}   // namespace Spirograph_v1

