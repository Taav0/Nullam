using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;


namespace Nullam.Pages.Participants
{
    public class IndexModel : PageModel
    {
        public List<ParticipantInfo> listParticipants = new List<ParticipantInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=DESKTOP-H7MTA24;Initial Catalog=nullam;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM private_participants";
                    using (SqlCommand command = new SqlCommand(sql, connection )) 
                    { 
                        using (SqlDataReader reader = command.ExecuteReader()) 
                        {
                            while (reader.Read()) 
                            {
                                ParticipantInfo participantInfo= new ParticipantInfo();
                                participantInfo.id = "" + reader.GetInt32(0);
                                participantInfo.firstName = reader.GetString(1);
                                participantInfo.lastName = reader.GetString(2);
                                participantInfo.securityNumber = reader.GetString(3);
                                participantInfo.paymentMethod = reader.GetString(4);
                                participantInfo.additionalInfo = reader.GetString(5);
                                

                                listParticipants.Add(participantInfo);
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
    public class ParticipantInfo
	{
        public String id;
        public String firstName;
        public String lastName;
        public String securityNumber;
        public String paymentMethod;
        public String additionalInfo;
        
        
    }

}
