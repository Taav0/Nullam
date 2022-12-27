using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nullam.Pages.Participants;
using System.Data.SqlClient;

namespace Nullam.Pages.Events
{
    public class CreateModel : PageModel
	{
		public EventInfo eventInfo = new EventInfo();
		public String errorMessage = "";
		public String successMessage = "";
		public void OnGet()
		{
		}
		public void OnPost()
		{
			eventInfo.name = Request.Form["name"];
			eventInfo.eventDate = Request.Form["eventDate"];
			eventInfo.eventLocation = Request.Form["eventLocation"];
			eventInfo.info = Request.Form["info"];
			

			if (eventInfo.name.Length == 0 || eventInfo.eventDate.Length == 0 ||
				eventInfo.eventLocation.Length == 0 || eventInfo.info.Length == 0)
			{
				errorMessage = "Kõik read on nõutud!";
				return;
			}

			try
			{
				String connectionStrin = "Data Source=DESKTOP-H7MTA24;Initial Catalog=nullam;Integrated Security=True";
				using (SqlConnection connection = new SqlConnection(connectionStrin))
				{
					connection.Open();
					String sql = "INSERT INTO events " +
								 "(name, eventDate, eventLocation, info) VALUES " +
								 "(@name, @eventDate, @eventLocation, @info);";

					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						command.Parameters.AddWithValue("@name", (object)eventInfo.name);
						command.Parameters.AddWithValue("@eventDate", (object)eventInfo.eventDate);
						command.Parameters.AddWithValue("@eventLocation", (object)eventInfo.eventLocation);
						command.Parameters.AddWithValue("@info", (object)eventInfo.info);
						
						command.ExecuteNonQuery();
					}
				}
			}
			catch (Exception ex)
			{
				errorMessage = ex.Message;
				return;
			}
			eventInfo.name = ""; eventInfo.eventDate = ""; eventInfo.eventLocation = ""; eventInfo.info = "";
			successMessage = "Uus osaleja lisatud korrektselt";

			Response.Redirect("/Events/Index");
		}
	}
}

