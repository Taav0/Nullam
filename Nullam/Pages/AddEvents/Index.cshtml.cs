using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Nullam.Pages.AddEvents
{
    public class IndexModel : PageModel
    {
        public List<EventInfo> listEvents = new List<EventInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=DESKTOP-H7MTA24;Initial Catalog=nullam;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString)) 
                {
                    connection.Open();
                    String sql = "SELECT * FROM events";
                    using (SqlCommand command = new SqlCommand(sql, connection)) 
                    {
                        using (SqlDataReader reader = command.ExecuteReader()) 
                        {
                            while (reader.Read()) 
                            {
                                EventInfo eventInfo = new EventInfo();
                                eventInfo.id = "" + reader.GetInt32(0);
                                eventInfo.name = reader.GetString(1);
                                eventInfo.eventDate = "" + reader.GetInt32(2);
                                eventInfo.eventLocation = reader.GetString(3);
                                eventInfo.info = reader.GetString(4);

                                listEvents.Add(eventInfo);
                            }
                        }
                    }
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }
    }
    public class EventInfo
    {
        public String id;
        public String name;
        public String eventDate;
        public String eventLocation;
        public String info;
    }
}
