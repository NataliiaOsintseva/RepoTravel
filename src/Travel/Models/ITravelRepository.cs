using System.Collections.Generic;

namespace Travel.Models
{
    public interface ITravelRepository
    {
        IEnumerable<Trip> GetAllTrips();
        IEnumerable<Trip> GetAllTripsWithStops();
    }
}