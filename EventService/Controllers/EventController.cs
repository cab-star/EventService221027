using EventService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics.Metrics;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using static EventService.Models.Event;

namespace EventService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private static List<Event>? Events = new(); 
 
        /// <summary>
        /// Kaldes af en anden service, når den har brug for at offentliggøre (publishe) et event
        /// </summary>
        /// <param name="e">Information om den event, der er opstået</param>
        [HttpPost]
        public void RaiseEvent(Event e)
        {            
            e.EventNo = e.NextEventNo(Events);
            Events?.Add(e);
        }

        /// <summary>
        /// Henter events
        /// </summary>
        /// <param name="startIndex">Index på det første event der skal hentes</param>
        /// <param name="antal">Antallet af events der maksimalt skal hentes (der kan være færre)</param>
        /// <returns></returns>
        [HttpGet]
        public List<Event> ListEvents (int startIndex, int antal)
        {

            //Dan testdata
            Event e = new();
            e.TestData(Events);

            //Tjek om parametre har en værdi
            if (startIndex == null) 
            {
                startIndex = 0;
            }
            if (antal == null)
            {
                antal = Events.Count;
            }

            // Jeg ville egentlig helst have brugt eventId, da det er det "officielle" nummer på eventen. Men dewr er noget galt med  min tildeling med funktionen NextEventNo().
            // Så det var lidt lettere blot at bruge listens indeks.

            //Tjek om startindex er inden for range.
            if (startIndex < 0 || startIndex >= Events.Count) 
            {
                startIndex = 0; 
            }
            // Tjek om startindex + antal er inden for range. Hvis ikke, så medtag hele range.
            if (startIndex + antal > Events.Count)
            {
                
                antal = Events.Count - startIndex;
            }

            // Læg range ind i ny liste og returner den
            List<Event> EventsSubset = Events.GetRange(startIndex, antal);
            return EventsSubset;
        }
    


    }
}
