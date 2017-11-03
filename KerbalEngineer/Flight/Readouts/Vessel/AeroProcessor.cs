namespace KerbalEngineer.Flight.Readouts.Vessel
{
    #region Using Directives

    using System;
    using UnityEngine;

    #endregion

    public class AeroProcessor : IUpdatable, IUpdateRequest
    {
        #region Fields

        private static readonly AeroProcessor instance = new AeroProcessor();

        private double liftForce;
        private double dragForce;
        private double angleOfAttack;
        private double gravityForce;
        private double liftInducedDrag;

        #endregion

        #region Properties

        public static AeroProcessor Instance
        {
            get { return instance; }
        }

        public static double LiftForce
        {
            get { return instance.liftForce; }
        }

        public static double DragForce
        {
            get { return instance.dragForce; }
        }

        public static double AngleOfAttack
        {
            get { return instance.angleOfAttack; }
        }

        public static double GravityForce
        {
            get { return instance.gravityForce; }
        }

        public static double LiftInducedDrag
        {
            get { return instance.liftInducedDrag; }
        }

        public bool UpdateRequested { get; set; }

        #endregion

        #region Methods

        public static void RequestUpdate()
        {
            instance.UpdateRequested = true;
        }

        public void Update()
        {
            var activeVessel = FlightGlobals.ActiveVessel;

            var lift = Vector3d.zero;
            var drag = Vector3d.zero;
            
            var count = activeVessel.Parts.Count;
            for(var i = 0; i < count; i++)
            {
                var part = activeVessel.Parts[i];

                drag += -part.dragVectorDir * part.dragScalar;
                
                if(!part.hasLiftModule)
                {
                    lift += Vector3.ProjectOnPlane(part.transform.rotation * (part.bodyLiftScalar * part.DragCubes.LiftForce), -part.dragVectorDir);
                }

                var moduleCount = part.Modules.Count;
                for(var j = 0; j < moduleCount; j++)
                {
                    var module = part.Modules[j] as ModuleLiftingSurface;

                    if (module == null) continue;

                    lift += module.liftForce;
                    drag += module.dragForce;
                }
            }

            var velocityDir = activeVessel.srf_vel_direction;
            var ld = lift + drag;
            var norm = Vector3d.Exclude(velocityDir, lift).normalized;
            this.liftForce = Vector3d.Dot(ld, norm);
            this.dragForce = Vector3d.Dot(ld, -velocityDir);
            this.liftInducedDrag = Vector3d.Dot(lift, -velocityDir);

            this.angleOfAttack = Vector3d.Dot((Vector3d)activeVessel.transform.forward, Vector3d.Exclude((Vector3d)activeVessel.transform.right, velocityDir).normalized);
            this.angleOfAttack = Math.Asin(this.angleOfAttack) * (180 / Math.PI);
            if (double.IsNaN(this.angleOfAttack))
                this.angleOfAttack = 0.0;

            this.gravityForce = activeVessel.totalMass * FlightGlobals.getGeeForceAtPosition((Vector3d)activeVessel.CoM).magnitude;

        }

        #endregion
    }
}
