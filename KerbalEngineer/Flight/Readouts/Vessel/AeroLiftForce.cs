#region Using Directives

using KerbalEngineer.Flight.Sections;
using KerbalEngineer.Helpers;

#endregion

namespace KerbalEngineer.Flight.Readouts.Vessel
{
    public class AeroLiftForce : ReadoutModule
    {
        #region Constructors
        
        public AeroLiftForce()
        {
            this.Name = "Total Lift";
            this.Category = ReadoutCategory.GetCategory("Vessel");
            this.HelpString = "Total lift force.";
            this.IsDefault = false;
        }

        #endregion

        #region Methods: public

        public override void Draw(SectionModule section)
        {
            this.DrawLine(Units.ToForce(AeroProcessor.LiftForce), section.IsHud);
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
