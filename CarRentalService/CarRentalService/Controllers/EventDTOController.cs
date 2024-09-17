using CarRentalService.Domain.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CarRentalService.Web.Controllers
{
    public class EventDTOController : Controller
    {
        public async Task<IActionResult> Index()
        {
            HttpClient client = new HttpClient();
            string URL = "https://emsweb20240826182329.azurewebsites.net/api/Events/GetAllEvents";

            HttpResponseMessage response = await client.GetAsync(URL);

           
                // Deserialize the JSON response into a dynamic list
                var responseData = await response.Content.ReadAsStringAsync();
                var dynamicEvents = JsonConvert.DeserializeObject<List<dynamic>>(responseData);

                // Initialize a list of Events objects
                var eventsList = new List<Events>();

                // Map each dynamic event to the Events DTO
                foreach (var dynamicEvent in dynamicEvents)
                {
                    var eventItem = new Events
                    {
                        EventName = dynamicEvent.eventName,
                        HostName = dynamicEvent.hostName,
                        isPartnerEvent = dynamicEvent.isPartnerEvent,
                        ImageUrl = dynamicEvent.imageUrl
                    };

                    eventsList.Add(eventItem);
                }

                // Pass the list of Events objects to the view
                return View(eventsList);
            
        }
    }
}
