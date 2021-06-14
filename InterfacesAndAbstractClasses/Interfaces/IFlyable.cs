namespace InterfacesAndAbstractClasses
{
    public interface IFlyable
    {
        public bool FlyTo(Coordinate destinationPoint);

        public double GetFlyTime(Coordinate destinationPoint);
    }
}
