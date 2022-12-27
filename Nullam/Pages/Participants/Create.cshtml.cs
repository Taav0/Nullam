using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Nullam.Pages.Participants
{
    public class CreateModel : PageModel
    {
        public ParticipantInfo participantInfo = new ParticipantInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
        }
        public void OnPost()
        {
			participantInfo.firstName = Request.Form["firstName"];
			participantInfo.lastName = Request.Form["lastName"];
			participantInfo.securityNumber = Request.Form["securityNumber"];
			participantInfo.paymentMethod = Request.Form["paymentMethod"];
			participantInfo.additionalInfo = Request.Form["additionalInfo"];

            if (participantInfo.firstName.Length == 0 || participantInfo.lastName.Length == 0 ||
				participantInfo.securityNumber.Length == 0 || participantInfo.paymentMethod.Length == 0)
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
                    String sql = "INSERT INTO private_participants " +
								 "(firstName, lastName, securityNumber, paymentMethod, additionalInfo) VALUES " +
								 "(@firstName, @lastName, @securityNumber, @paymentMethod, @additionalInfo);";

                    using (SqlCommand command = new SqlCommand(sql, connection)) 
                    {
                    command.Parameters.AddWithValue("@firstName", (object)participantInfo.firstName);
                    command.Parameters.AddWithValue("@lastName", (object)participantInfo.lastName);
                    command.Parameters.AddWithValue("@securityNumber", (object)participantInfo.securityNumber);
                    command.Parameters.AddWithValue("@paymentMethod", (object)participantInfo.paymentMethod);
                    command.Parameters.AddWithValue("@additionalInfo", (object)participantInfo.additionalInfo);

                    command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex) 
            {
                errorMessage = ex.Message;
                return;
            }
			participantInfo.firstName = ""; participantInfo.lastName = ""; participantInfo.securityNumber = ""; participantInfo.paymentMethod = "";
            successMessage = "Uus osaleja lisatud korrektselt";

            Response.Redirect("/Participants/Index");
        }
    }
}
