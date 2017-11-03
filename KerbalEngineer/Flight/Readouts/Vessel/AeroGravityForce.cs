#region Using Directives

using KerbalEngineer.Flight.Sections;
using KerbalEngineer.Helpers;

#endregion

namespace KerbalEngineer.Flight.Readouts.Vessel
{
    public class AeroGravityForce : ReadoutModule
    {
        #region Constructors

        public AeroGravityForce()
        {
            this.Name = "Gravity Force (weight)";
            this.Category = ReadoutCategory.GetCategory("Vessel");
            this.HelpString = "Current G Force in kN.";
            this.IsDefault = false;
        }

        #endregion

        #region Methods: public

        public override void Draw(SectionModule section)
        {
            this.DrawLine(Units.ToForce(AeroProcessor.GravityForce), section.IsHud);
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
