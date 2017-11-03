#region Using Directives

using KerbalEngineer.Flight.Sections;
using KerbalEngineer.Helpers;

#endregion

namespace KerbalEngineer.Flight.Readouts.Vessel
{
    public class AeroAOA : ReadoutModule
    {
        #region Constructors

        public AeroAOA()
        {
            this.Name = "AoA";
            this.Category = ReadoutCategory.GetCategory("Vessel");
            this.HelpString = "Angle of Attack.";
            this.IsDefault = false;
        }

        #endregion

        #region Methods: public

        public override void Draw(SectionModule section)
        {
            this.DrawLine(Units.ToAngle(AeroProcessor.AngleOfAttack), section.IsHud);
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
