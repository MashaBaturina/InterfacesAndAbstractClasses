using System;

namespace InterfacesAndAbstractClasses
{
    public struct Coordinate
    {
        private const int DefaultCoordinate = 0;

        public int X { get; private set; }

        public int Y { get; private set; }

        public int Z { get; private set; }

        public Coordinate(int x, int y, int z)
        {
            X = x < 0 ? DefaultCoordinate : x;
            Y = y < 0 ? DefaultCoordinate : y;
            Z = z < 0 ? DefaultCoordinate : z;
        }

        public static double GetDistance(Coordinate startPosition, Coordinate destinationPosition)
        {
            return Math.Sqrt(Math.Pow(Convert.ToDouble(destinationPosition.X) - Convert.ToDouble(startPosition.X), 2) + Math.Pow(Convert.ToDouble(destinationPosition.Y) - Convert.ToDouble(startPosition.Y), 2) + Math.Pow(Convert.ToDouble(destinationPosition.Z) - Convert.ToDouble(startPosition.Z), 2));
        }
    }
}
