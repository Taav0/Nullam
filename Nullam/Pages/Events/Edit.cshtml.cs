using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nullam.Pages.Participants;
using System.Data.SqlClient;

namespace Nullam.Pages.Events
{
    public class EditModel : PageModel
    {
		public EventInfo eventInfo = new EventInfo();
		public String errorMessage = "";
		public String successMessage = "";
		public void OnGet()
		{
			String id = Request.Query["id"];

			try
			{
				String connectionString = "Data Source=DESKTOP-H7MTA24;Initial Catalog=nullam;Integrated Security=True";
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();
					String sql = "SELECT * FROM events WHERE id=@id";
					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						command.Parameters.AddWithValue("@id", id);
						using (SqlDataReader reader = command.ExecuteReader())
						{
							if (reader.Read())
							{
								eventInfo.id = "" + reader.GetInt32(0);
								eventInfo.name = reader.GetString(1);
								eventInfo.eventDate = reader.GetString(2);
								eventInfo.eventLocation = reader.GetString(3);
								eventInfo.info = reader.GetString(4);
								
							}
						}
					}
				}

			}
			catch (Exception ex)
			{
				errorMessage = ex.Message;
			}
		}
		public void OnPost()
		{
			eventInfo.id = Request.Form["id"];
			eventInfo.name = Request.Form["firstName"];
			eventInfo.eventDate = Request.Form["lastName"];
			eventInfo.eventLocation = Request.Form["securityNumber"];
			eventInfo.info = Request.Form["paymentMethod"];
			

			/*if (eventInfo.name.Length == 0 || eventInfo.eventDate.Length == 0 ||
			   eventInfo.eventLocation.Length == 0 || eventInfo.info.Length == 0)
			{
				errorMessage = "Kõik read on nõutud!";
				return;
			}*/

			try
			{
				String connectionString = "Data Source=DESKTOP-H7MTA24;Initial Catalog=nullam;Integrated Security=True";
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();
					String sql = "UPDATE events " + "SET name=@name, eventDate=@eventDate, eventLocation=@eventLocation, info=@info" + " WHERE id=@id";
					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						command.Parameters.AddWithValue("@id", (object)eventInfo.id);
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
			Response.Redirect("/Events/Index");
		}
	}
}
