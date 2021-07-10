using System;

namespace InterfacesAndAbstractClasses
{
    public class Plane : IFlyable
    {
        private double specificTime;

        /// <summary>
        /// Plane's min distance km
        /// </summary>
        private const double MinDistanceKm = 100;

        /// <summary>
        /// Plane's max distance km
        /// </summary>
        private const double MaxDistanceKm = 10000;

        /// <summary>
        /// Plane's max speed 
        /// </summary>
        private const double MaxSpeedKmH = 900;

        /// <summary>
        /// Plane change speed every 10 km
        /// </summary>
        private const double SpeedChangePeriodKm = 10;

        /// <summary>
        /// Plane speed increases up to 10 km/h every 10 km of flight 
        /// </summary>
        private const double SpeedValueChangePerPeriodKmH = 10;

        /// <summary>
        /// Start coordinate
        /// </summary>
        public Coordinate StartCoordinate { get; set; }

        /// <summary>
        /// Destination coordinate
        /// </summary>
        public Coordinate DestinationCoordinate { get; set; }

        /// <summary>
        /// Start speed
        /// </summary>
        public double StartSpeed { get; set; }

        /// <summary>
        /// Acceleration
        /// </summary>
        public double Acceleration { get; set; }

        /// <summary>
        /// Specific time
        /// </summary>
        public double SpecificTime
        {
            get { return specificTime; }

            set
            {
                if (value <= GetFlyTime(DestinationCoordinate))
                {
                    specificTime = value;
                }
                else
                {
                    Console.WriteLine("Specific time exeeds the flytime.");
                    specificTime = 0;
                }
            }
        }

        /// <summary>
        /// Position (km) at the specific point of time
        /// </summary>
        public double CurrentPosition
        {
            get { return StartSpeed * SpecificTime + Acceleration * Math.Pow(SpecificTime, 2) / 2; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="startSpeedKmH"> Start speed (km/h) </param>
        public Plane(double startSpeedKmH = 200)
        {
            StartSpeed = startSpeedKmH;
        }

        /// <summary>
        /// Fly to the destination point
        /// </summary>
        /// <param name="destinationCoordinate">Destination coordinate</param>
        /// <returns>True if the object can fly to the destination point, false otherwise.</returns>
        public bool FlyTo(Coordinate destinationCoordinate)
        {
            double distance = Coordinate.GetDistance(StartCoordinate, destinationCoordinate);

            if (distance >= MinDistanceKm && distance <= MaxDistanceKm)
            {
                DestinationCoordinate = destinationCoordinate;
                return true;
            }
            else
            {
                DestinationCoordinate = StartCoordinate;
                return false;
            }
        }

        /// <summary>
        /// Get fly time 
        /// </summary>
        /// <param name="destinationCoordinate">Destination coordinate</param>
        /// <returns>Fly time in hours</returns>
        public double GetFlyTime(Coordinate destinationCoordinate)
        {
            double distance = Coordinate.GetDistance(StartCoordinate, destinationCoordinate);
            double finalSpeed = GetPlaneFinalSpeed(distance);
            Acceleration = (Math.Pow(finalSpeed, 2) - Math.Pow(StartSpeed, 2)) / (distance * 2);
            double flyTime = (finalSpeed - StartSpeed) / Acceleration;

            return flyTime;
        }

        /// <summary>
        /// Gets plane final speed
        /// </summary>
        /// <param name="distance">Distance</param>
        /// <returns>Final speed if it doesn't exeed the max speed, max speed otherwise.</returns>
        private double GetPlaneFinalSpeed(double distance)
        {
            double finalSpeed = StartSpeed + distance / SpeedChangePeriodKm * SpeedValueChangePerPeriodKmH;

            // The plane cannot fly faster then 900 km per hour.
            return finalSpeed < MaxSpeedKmH ? finalSpeed : MaxSpeedKmH;
        }
    }
}
