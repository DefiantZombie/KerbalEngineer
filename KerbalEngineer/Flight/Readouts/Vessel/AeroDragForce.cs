#region Using Directives

using KerbalEngineer.Flight.Sections;
using KerbalEngineer.Helpers;

#endregion

namespace KerbalEngineer.Flight.Readouts.Vessel
{
    public class AeroDragForce : ReadoutModule
    {
        #region Constructors

        public AeroDragForce()
        {
            this.Name = "Total Drag";
            this.Category = ReadoutCategory.GetCategory("Vessel");
            this.HelpString = "Total drag force.";
            this.IsDefault = false;
        }

        #endregion

        #region Methods: public

        public override void Draw(SectionModule section)
        {
            this.DrawLine(Units.ToForce(AeroProcessor.DragForce), section.IsHud);
        }

        public override void Reset()
        {
            FlightEngineerCore.Instance.AddUpdatable(AeroProcessor.Instance);
        }

        public override void Update()
        {
            AeroProcessor.RequestUpdate();
        }

        #endregion
    }
}
