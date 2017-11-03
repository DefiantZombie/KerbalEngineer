#region Using Directives

using KerbalEngineer.Flight.Sections;
using KerbalEngineer.Helpers;

#endregion

namespace KerbalEngineer.Flight.Readouts.Vessel
{
    public class AeroLID : ReadoutModule
    {
        #region Constructors

        public AeroLID()
        {
            this.Name = "L-I-D";
            this.Category = ReadoutCategory.GetCategory("Vessel");
            this.HelpString = "Lift Induced Drag force.";
            this.IsDefault = false;
        }

        #endregion

        #region Methods: public

        public override void Draw(SectionModule section)
        {
            this.DrawLine(Units.ToForce(AeroProcessor.LiftInducedDrag), section.IsHud);
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
