using Microsoft.Data.Entity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Models
{
    public class TravelRepository : ITravelRepository
    {
        private TravelContext _context;
        private ILogger<TravelRepository> _logger;

        public TravelRepository(TravelContext context, ILogger<TravelRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IEnumerable<Trip> GetAllTrips()
        {
            try {
                return _context.Trips.OrderBy(t => t.Name).ToList();
            }
            catch(Exception ex)
            {
                _logger.LogError("Couldn't get trips from DataBase", ex);
                return null;
            }
        }

        public IEnumerable<Trip> GetAllTripsWithStops()
        {

            try {
                return _context.Trips
                    .Include(t => t.Stops)
                    .OrderBy(t => t.Name)
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("Couldn't get trips with stops from DataBase", ex);
                return null;
            }
        }
    }
}
