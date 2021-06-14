using System;

namespace InterfacesAndAbstractClasses
{
    public static class CoordinateHelper
    {
        public static bool AreCoordinatesPositiveNumbers(Coordinate coordinate)
        {
            return (coordinate.X > 0 && coordinate.Y > 0 && coordinate.Z > 0);
        }

        public static double GetDistance(Coordinate startPosition, Coordinate destinationPosition)
        {
            return Math.Sqrt(Math.Pow(Convert.ToDouble(destinationPosition.X) - Convert.ToDouble(startPosition.X), 2) + Math.Pow(Convert.ToDouble(destinationPosition.Y) - Convert.ToDouble(startPosition.Y), 2) + Math.Pow(Convert.ToDouble(destinationPosition.Z) - Convert.ToDouble(startPosition.Z), 2));
        }
    }
}
