using System;

namespace InterfacesAndAbstractClasses
{
    /// <summary>
    /// Bird class 
    /// </summary>
    public class Bird : IFlyable
    {
        /// <summary>
        /// Bird maximum distance is 200 km
        /// </summary>
        private const double MaxDistanceKm = 200;

        /// <summary>
        /// Min speed
        /// </summary>
        private const int MinSpeedKmH = 1;

        /// <summary>
        /// Max speed
        /// </summary>
        private const int MaxSpeedKmH = 20;

        /// <summary>
        /// Bird position (km) at the specific time
        /// </summary>
        private double specificTime;

        /// <summary>
        /// Start coordinate
        /// </summary>
        public Coordinate StartCoordinate { get; set; }

        /// <summary>
        /// Destination coordinate
        /// </summary>
        public Coordinate DestinationCoordinate { get; set; }

        /// <summary>
        /// Speed
        /// </summary>
        public int Speed { get; set; }

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
        /// Gets current object position (km)
        /// </summary>
        public double CurrentPosition
        {
            get { return SpecificTime * Speed; }

            private set { }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public Bird()
        {
            SetSpeed();
            StartCoordinate = new Coordinate(0, 0, 0);
        }

        /// <summary>
        /// Fly to the destination point
        /// </summary>
        /// <param name="destinationCoordinate">Destination coordinate</param>
        /// <returns>True if the object can fly to the destination point, false otherwise.</returns>
        public bool FlyTo(Coordinate destinationCoordinate)
        {

            double distance = Coordinate.GetDistance(StartCoordinate, destinationCoordinate);

            if (distance <= MaxDistanceKm)
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
        /// <returns>Fly time (h)</returns>
        public double GetFlyTime(Coordinate destinationCoordinate)
        {
            double distance = Coordinate.GetDistance(StartCoordinate, destinationCoordinate);
            double time = distance / Speed;

            return time;
        }

        /// <summary>
        /// Sets speed randomly
        /// </summary>
        private void SetSpeed()
        {
            Random rnd = new();
            Speed = rnd.Next(MinSpeedKmH, MaxSpeedKmH);
        }
    }
}

