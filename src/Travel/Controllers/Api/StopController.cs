using AutoMapper;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Travel.Models;
using Travel.ViewModels;

namespace Travel.Controllers.Api
{
    [Route("api/trips/{tripName}/stops")]
    public class StopController : Controller
    {
        private ILogger<StopController> _logger;
        private ITravelRepository _repository;

        public StopController(ITravelRepository repository, ILogger<StopController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet("")]
        public JsonResult Get(string tripName)
        {
            try {
                var results = _repository.GetTripByName(tripName);

                if(results == null)
                {
                    return Json(null);
                }

                return Json(Mapper.Map<IEnumerable<StopViewModel>>(results.Stops));
            }
            catch(Exception ex)
            {
                _logger.LogError($"Failed to get stops for trip {tripName}", ex);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                //var response = HttpBadRequest();
                return Json("Error occured while trying to find trip name");
            }
        }

        public JsonResult Post(string tripName, [FromBody]StopViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Map to the Entity
                    var newStop = Mapper.Map<Stop>(vm);
                    // Lookup  Coordinates

                    // Save to DB
                    _repository.AddStop(tripName, newStop);

                    if (_repository.SaveAll())
                    {
                        Response.StatusCode = (int)HttpStatusCode.Created;
                        return Json(Mapper.Map<StopViewModel>(newStop));
                    }
                }
            }
            catch(Exception ex)
            {
                _logger.LogError("Failed to save new stop", ex);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json("Error occured while trying to find trip name");
            }

            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json("Validation of new stop failed");
        }

    }
}
