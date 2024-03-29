﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Nullam.Pages
{
    public class IndexModel : PageModel
    {
		public List<EventsInfo> listEvents = new List<EventsInfo>();
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
								EventsInfo eventInfo = new EventsInfo();
								eventInfo.id = "" + reader.GetInt32(0);
								eventInfo.name = reader.GetString(1);
								eventInfo.eventDate = reader.GetString(2);
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
	public class EventsInfo
	{
		public String id;
		public String name;
		public String eventDate;
		public String eventLocation;
		public String info;
		


	}

}
