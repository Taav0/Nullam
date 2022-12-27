using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Nullam.Pages.Participants
{
    public class EditModel : PageModel
    {
		public ParticipantInfo participantInfo = new ParticipantInfo();
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
                    String sql = "SELECT * FROM private_participants WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
								participantInfo.id = "" + reader.GetInt32(0);
								participantInfo.firstName = reader.GetString(1);
								participantInfo.lastName = reader.GetString(2);
								participantInfo.securityNumber = reader.GetString(3);
								participantInfo.paymentMethod = reader.GetString(4);
								participantInfo.additionalInfo = reader.GetString(5);
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
        public void OnPost ()
        {
			participantInfo.id = Request.Form["id"];
			participantInfo.firstName = Request.Form["firstName"];
			participantInfo.lastName = Request.Form["lastName"];
			participantInfo.securityNumber = Request.Form["securityNumber"];
			participantInfo.paymentMethod = Request.Form["paymentMethod"];
			participantInfo.additionalInfo = Request.Form["additionalInfo"];

			if (participantInfo.id.Length == 0 || participantInfo.firstName.Length == 0 || participantInfo.lastName.Length == 0 ||
			   participantInfo.securityNumber.Length == 0 || participantInfo.paymentMethod.Length == 0)
			{
				errorMessage = "Kõik read on nõutud!";
				return;
			}

			try
            {
				String connectionString = "Data Source=DESKTOP-H7MTA24;Initial Catalog=nullam;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE private_participants " +
                                 "SET firstName=@firstName, lastName=@lastName, securityNumber=@securityNumber, paymentMethod=@paymentMethod, additionalInfo=@additionalInfo" + 
                                 "WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        
                        command.Parameters.AddWithValue("@firstName", (object)participantInfo.firstName);
                        command.Parameters.AddWithValue("@lastName", (object)participantInfo.lastName);
                        command.Parameters.AddWithValue("@securityNumber", (object)participantInfo.securityNumber);
                        command.Parameters.AddWithValue("@paymentMethod", (object)participantInfo.paymentMethod);
                        command.Parameters.AddWithValue("@additionalInfo", (object)participantInfo.additionalInfo);
						command.Parameters.AddWithValue("@id", (object)participantInfo.id);

						command.ExecuteNonQuery();
                    }
                }
			}
            catch(Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
            Response.Redirect("/Participants/Index");
		}
    }
}
