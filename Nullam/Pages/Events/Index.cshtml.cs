using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;


namespace Nullam.Pages.Events
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
								EventInfo EventInfo = new EventInfo();
								EventInfo.id = "" + reader.GetInt32(0);
								EventInfo.name = reader.GetString(1);
								EventInfo.eventDate = reader.GetString(2);
								EventInfo.eventLocation = reader.GetString(3);
								EventInfo.info = reader.GetString(4);
								
								listEvents.Add(EventInfo);
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
