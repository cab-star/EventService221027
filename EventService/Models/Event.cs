using Microsoft.Extensions.Logging;

namespace EventService.Models
{
    /// <summary>
    /// Information om den event, der er opstået
    /// Tænk over, hvilke data det er relevant at event'et indeholder
    /// </summary>
    public class Event
    {

                public int EventNo { get; set; }        
        public int TableNo { get; set; }
        public int PizzaNo { get; set; }
        
        
       //Genereer testdata, så listen er populeret uanset om der POST'es først.
        public void TestData(List<Event>? Elist)
        {

            for (int i = 1; i < 4; i++)
            {
                Event e = new();
                e.EventNo = NextEventNo(Elist);              
                e.TableNo = i;
                e.PizzaNo = i * 10;

                Elist.Add(e);

            }

        }

        //Tildeler det næste Eventnummer - virker ikke
        public int NextEventNo(List<Event>? Elist)
        {
            if ((Elist != null) && (!Elist.Any()))
            {
                return Elist.Count + 1;
            }
            else
                return 1;

        }
    }
}
