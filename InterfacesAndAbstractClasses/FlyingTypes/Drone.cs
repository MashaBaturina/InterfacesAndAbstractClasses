using System;

namespace InterfacesAndAbstractClasses
{
    public class Drone : IFlyable
    {
        private double specificTime;
        private int speed;

        /// <summary>
        /// Max distance (km)
        /// </summary>
        private const double MaxDistanceKm = 10;

        /// <summary>
        /// Max speed (Km/H)
        /// </summary>
        private const int MaxSpeedKmH = 140;

        /// <summary>
        /// Number of minutes in an hour
        /// </summary>
        private const int MinPerHour = 60;

        /// <summary>
        /// Freezes every 10 minutes (0,166 hour) in a hour. 
        /// </summary>
        private const double FreezeFrequencyHours = 0.1666;

        /// <summary>
        /// Freezes for 1 minute (0,0166 hour).
        /// </summary>
        private const double FreezeValueHours = 0.0166;

        /// <summary>
        /// x = t * v * DroneFreezeFrequencyHours / (DroneFreezeValueHours + DroneFreezeFrequencyHours)
        /// SpeedCoefficient = DroneFreezeFrequencyHours / (DroneFreezeValueHours + DroneFreezeFrequencyHours)
        /// </summary>
        private const double FreezeCoefficient = FreezeFrequencyHours / (FreezeFrequencyHours + FreezeValueHours);

        /// <summary>
        /// Start coordinate
        /// </summary>
        public Coordinate StartCoordinate { get; private set; }

        /// <summary>
        /// Destination coordinate
        /// </summary>
        public Coordinate DestinitionCoordinate { get; private set; }

        /// <summary>
        /// Speed 
        /// </summary>
        public int Speed
        {
            get { return speed; }

            private set
            {
                if (value < MaxSpeedKmH)
                {
                    speed = value;
                }
                else
                {
                    Console.WriteLine($"Max drone's speed is {MaxSpeedKmH} km/h. Drone speed has been set to {MaxSpeedKmH} km/h");
                    speed = MaxSpeedKmH;
                }
            }
        }

        /// <summary>
        /// Specific time
        /// </summary>
        public double SpecificTime
        {
            get { return specificTime; }

            set
            {
                if (value <= GetFlyTime(DestinitionCoordinate))
                {
                    specificTime = value;
                }
                else
                {
                    Console.WriteLine("The specific time exeeds the flytime.");
                    specificTime = 0;
                }
            }
        }

        /// <summary>
        /// Position (km) at the specific point of time
        /// </summary>
        public double CurrentPosition
        {
            get
            {
                return SpecificTime * Speed * FreezeCoefficient;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="speed">Speed</param>
        public Drone(int speed)
        {
            Speed = speed;
            StartCoordinate = new Coordinate(0, 0, 0);
        }

        /// <summary>
        /// Fly to the destination point
        /// </summary>
        /// <param name="destinitionCoordinate">Destination coordinate</param>
        /// <returns>True if the object can fly to the destination point, false otherwise.</returns>
        public bool FlyTo(Coordinate destinitionCoordinate)
        {
            bool areCoordinatesPositive = CoordinateHelper.AreCoordinatesPositiveNumbers(destinitionCoordinate);

            if (areCoordinatesPositive)
            {
                double flyTime = GetFlyTime(destinitionCoordinate);

                if (flyTime > GetMaxFlyTime())
                {
                    Console.WriteLine($"The destination point is too far. Drone's maximum distance is {MaxDistanceKm} km.");

                    DestinitionCoordinate = StartCoordinate;

                    return false;
                }
                else
                {
                    DestinitionCoordinate = destinitionCoordinate;
                    return true;
                }
            }
            else
            {
                Console.WriteLine("Some coordinates are not positive numbers.");
                return false;
            }
        }

        /// <summary>
        /// Get fly time 
        /// </summary>
        /// <param name="destinationCoordinate">Destination coordinate</param>
        /// <returns>Fly time in hours</returns>
        public double GetFlyTime(Coordinate destinitionPosition)
        {
            double defaultFlyTime = 0.0;
            bool areCoordinatesPositive = CoordinateHelper.AreCoordinatesPositiveNumbers(destinitionPosition);

            if (areCoordinatesPositive)
            {
                double distance = CoordinateHelper.GetDistance(StartCoordinate, destinitionPosition);
                double droneFlyTimeWithoutFreezes = distance / Speed;
                int freezesQuantity = (int)(droneFlyTimeWithoutFreezes / FreezeFrequencyHours);
                double droneFlyTimeWithFreezes = droneFlyTimeWithoutFreezes + freezesQuantity / MinPerHour;

                return droneFlyTimeWithFreezes;
            }
            else
            {
                Console.WriteLine("Some coordinates are not positive numbers.");

                return defaultFlyTime;
            }
        }

        /// <summary>
        /// Get max fly time
        /// </summary>
        /// <returns>Max fly time</returns>
        public double GetMaxFlyTime()
        {
            double droneFlyTimeWithoutFreezes = MaxDistanceKm / Speed;
            int freezesQuantity = (int)(droneFlyTimeWithoutFreezes / FreezeFrequencyHours);
            double droneMaxFlyTimeWithFreezes = droneFlyTimeWithoutFreezes + freezesQuantity / MinPerHour;

            return droneMaxFlyTimeWithFreezes;
        }
    }
}
