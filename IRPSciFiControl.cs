namespace Spirograph_v1
{
    public interface IRPSciFiControl
    {
        string ControlId { get; }

        RPSciFiControlType ControlType { get; }

        void Register(RPSciFiControlBus bus);


    }   // interface IRPSciFiControl

}   // namespace Spirograph_v1

