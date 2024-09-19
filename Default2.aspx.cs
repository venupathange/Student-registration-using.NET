using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using MySql.Data.MySqlClient;


public partial class Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {   
        string connectionString = "Server=127.0.0.1;Port=3306;Database=student_registration;User ID=root;Password=root;";

            // Define the SQL query
        string query = "SELECT * FROM students WHERE First_Name = @FirstName AND Date_Of_Birth = @DateOfBirth";

            // Create a new MySqlConnection object
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                // Create a MySqlCommand object with the query and connection
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@FirstName", txtFirstName.Text);
                cmd.Parameters.AddWithValue("@DateOfBirth", txtDateOfBirth.Text);

                try
                {
                    // Open the connection
                    conn.Open();

                    // Execute the query and get the result
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Redirect to the UserDetails page if login is successful
                            Response.Redirect("UserDetails.aspx?First_Name=" + txtFirstName.Text);
                        }
                        else
                        {
                            // Show an error message if login fails
                            Response.Write("Invalid login credentials.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle any errors that occurred during the connection or query execution
                    Response.Write("Error: " + ex.Message);
                }
            }
        }
    }



