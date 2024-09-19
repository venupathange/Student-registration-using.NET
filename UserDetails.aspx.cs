using System;
using System.Web.UI;
using MySql.Data.MySqlClient;

public partial class UserDetails : System.Web.UI.Page
{
    private string connectionString = "Server=127.0.0.1;Port=3306;Database=student_registration;User ID=root;Password=root;";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string firstName = Request.QueryString["First_Name"];
            if (!string.IsNullOrEmpty(firstName))
            {
                hdnFirstName.Value = firstName;
                LoadUserDetails(firstName);
            }
        }
    }

    private void LoadUserDetails(string firstName)
    {
        string query = "SELECT * FROM students WHERE First_Name = @FirstName";

        using (var conn = new MySqlConnection(connectionString))
        using (var cmd = new MySqlCommand(query, conn))
        {
            cmd.Parameters.AddWithValue("@FirstName", firstName);

            try
            {
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        lblDetails.Text = "First Name: " + reader["First_Name"] + "<br/>" +
                                          "Last Name: " + reader["Last_Name"] + "<br/>" +
                                          "Email: " + reader["Email"] + "<br/>" +
                                          "Phone Number: " + reader["Phone_Number"] + "<br/>" +
                                          "Date of Birth: " + reader["Date_Of_Birth"] + "<br/>" +
                                          "Address: " + reader["Address"] + "<br/>" + "id: " + reader["id"] + "<br/>"; ;
                        PopulateTextFields(reader);
                    }
                    else
                    {
                        lblDetails.Text = "User not found.";
                    }
                }
            }
            catch (Exception ex)
            {
                lblDetails.Text = "An error occurred while loading user details.";
                System.Diagnostics.Debug.WriteLine("Error loading user details: {ex.Message}");
            }
        }
    }

    private void PopulateTextFields(MySqlDataReader reader)
    {
        txtFirstName.Text = reader["First_Name"].ToString();
        txtLastName.Text = reader["Last_Name"].ToString();
        txtEmail.Text = reader["Email"].ToString();
        txtPhoneNumber.Text = reader["Phone_Number"].ToString();
        txtDOB.Text = reader["Date_Of_Birth"].ToString();
        txtAddress.Text = reader["Address"].ToString();
        txtid.Text = reader["id"].ToString();

    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        ToggleEditMode(true);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string originalFirstName = hdnFirstName.Value;
      //  if (string.IsNullOrEmpty(originalFirstName)) return;
      
        string query = "UPDATE `student_registration`.`students` SET First_Name = @NewFirstName, Last_Name = @LastName, Email = @Email, Phone_Number = @PhoneNumber, Date_Of_Birth = @DateOfBirth, Address = @Address WHERE id = @id";
                       

        using (var conn = new MySqlConnection(connectionString))
        using (var cmd = new MySqlCommand(query, conn))
        {
            cmd.Parameters.AddWithValue("@NewFirstName", txtFirstName.Text);
            cmd.Parameters.AddWithValue("@LastName", txtLastName.Text);
            cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
            cmd.Parameters.AddWithValue("@PhoneNumber", txtPhoneNumber.Text);
            // Handle DateOfBirth
            DateTime dob;
            if (DateTime.TryParse(txtDOB.Text, out dob))
            {
                // Format date in 'yyyy-MM-dd HH:mm:ss'
                cmd.Parameters.AddWithValue("@DateOfBirth", dob.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            else
            {
                cmd.Parameters.AddWithValue("@DateOfBirth", DBNull.Value); // or handle error as needed
            }

            cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
            cmd.Parameters.AddWithValue("@id", txtid.Text);

           // cmd.Parameters.AddWithValue("@DateOfBirth", txtDOB.Text);
          //  cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
           // cmd.Parameters.AddWithValue("@id", txtid.Text);
            try
            {
                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                lblDetails.Text = rowsAffected > 0 ? "User details updated successfully." : "Update failed. No rows affected.";
              conn.Close();
            }
            catch (Exception ex)
            {
                lblDetails.Text = "An error occurred while updating user details.";
                System.Diagnostics.Debug.WriteLine("Error updating user details: {ex.Message}");
            }
        }

        ToggleEditMode(false);
    }

   protected void btnCancel_Click(object sender, EventArgs e)
    {
        ToggleEditMode(false);
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string firstName = hdnFirstName.Value;
        if (string.IsNullOrEmpty(firstName)) return;

        string query = "DELETE FROM students WHERE First_Name = @FirstName";

        using (var conn = new MySqlConnection(connectionString))
        using (var cmd = new MySqlCommand(query, conn))
        {
            cmd.Parameters.AddWithValue("@FirstName", firstName);

            try
            {
                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                lblDetails.Text = rowsAffected > 0 ? "User deleted successfully." : "Delete failed.";
                if (rowsAffected > 0) ToggleEditMode(false);
            }
            catch (Exception ex)
            {
                lblDetails.Text = "An error occurred while deleting the user.";
                System.Diagnostics.Debug.WriteLine("Error deleting user: {ex.Message}");
            }
        }
    }

    private void ToggleEditMode(bool isEditMode)
    {
        lblDetails.Visible = !isEditMode;
        btnUpdate.Visible = !isEditMode;
        btnSave.Visible = isEditMode;
        btnCancel.Visible = isEditMode;
        btnDelete.Visible = !isEditMode;

        txtFirstName.Visible = isEditMode;
        txtLastName.Visible = isEditMode;
        txtEmail.Visible = isEditMode;
        txtPhoneNumber.Visible = isEditMode;
        txtDOB.Visible = isEditMode;
        txtAddress.Visible = isEditMode;
    }
}

//using System;
//using System.Web.UI;
//using MySql.Data.MySqlClient;

//public partial class UserDetails : System.Web.UI.Page
//{
//    private string connectionString = "Server=127.0.0.1;Port=3306;Database=student_registration;User ID=root;Password=root;";


//    protected void Page_Load(object sender, EventArgs e)
//    {
//        if (!IsPostBack)
//        {
//            string firstName = Request.QueryString["First_Name"];
//            if (!string.IsNullOrEmpty(firstName))
//            {
//                LoadUserDetails(firstName);
//            }
//        }
//    }

//    private void LoadUserDetails(string firstName)
//    {
//        string query = "SELECT * FROM students WHERE First_Name = @FirstName";

//        using (var conn = new MySqlConnection(connectionString))
//        using (var cmd = new MySqlCommand(query, conn))
//        {
//            cmd.Parameters.AddWithValue("@FirstName", firstName);

//            try
//            {
//                conn.Open();
//                using (var reader = cmd.ExecuteReader())
//                {
//                    if (reader.Read())
//                    {
//                        lblDetails.Text = "First Name:" + reader["First_Name"] + "<br/>" +
//                                          "Last Name: " + reader["Last_Name"] + "<br/>" +
//                                          "Email: " + reader["Email"] + "<br/>" +
//                                          "Phone Number: " + reader["Phone_Number"] + "<br/>" +
//                                          "Date of Birth: " + reader["Date_Of_Birth"] + "<br/>" +
//                                          "Address: " + reader["Address"] + "<br/>";
//                        PopulateTextFields(reader);
//                        ToggleEditMode(false); // Set the initial state to view mode
//                    }
//                    else
//                    {
//                        lblDetails.Text = "User not found.";
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                lblDetails.Text = "An error occurred while loading user details.";
//                // Log the exception details here (use a logging framework)
//                System.Diagnostics.Debug.WriteLine("Error loading user details: {ex.Message}");
//            }
//        }
//    }

//    private void PopulateTextFields(MySqlDataReader reader)
//    {
//        txtFirstName.Text = reader["First_Name"].ToString();
//        txtLastName.Text = reader["Last_Name"].ToString();
//        txtEmail.Text = reader["Email"].ToString();
//        txtPhoneNumber.Text = reader["Phone_Number"].ToString();
//        txtDOB.Text = reader["Date_Of_Birth"].ToString();
//        txtAddress.Text = reader["Address"].ToString();
//    }

//    protected void btnUpdate_Click(object sender, EventArgs e)
//    {
//        ToggleEditMode(true);
//    }
//        protected void btnSave_Click(object sender, EventArgs e)
//  {
//            string First_Name = FirstName.Value;
//     // if (string.IsNullOrEmpty(First_Name)) return;

//        string query = "UPDATE students SET First_Name = @FirstName, Last_Name = @LastName, Email = @Email, Phone_Number = @PhoneNumber, Date_Of_Birth = @DateOfBirth, Address = @Address WHERE First_Name = @OriginalFirstName";

//        using (var conn = new MySqlConnection(connectionString))
//        using (var cmd = new MySqlCommand(query, conn))
//        {
//            cmd.Parameters.AddWithValue("@FirstName", txtFirstName.Text);
//            cmd.Parameters.AddWithValue("@LastName", txtLastName.Text);
//            cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
//            cmd.Parameters.AddWithValue("@PhoneNumber", txtPhoneNumber.Text);
//            cmd.Parameters.AddWithValue("@DateOfBirth", txtDOB.Text);
//            cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
//          //  cmd.Parameters.AddWithValue("@OriginalFirstName", First_Name);

//            try
//            {
//                conn.Open();
//                int rowsAffected = cmd.ExecuteNonQuery();
//                lblDetails.Text = rowsAffected > 0 ? "User details updated successfully." : "Update failed. No rows affected.";
//            }
//            catch (Exception ex)
//            {
//                lblDetails.Text = "An error occurred while updating user details.";
//                System.Diagnostics.Debug.WriteLine("Error updating user details: {ex.Message}");
//            }
//        }

//        ToggleEditMode(false);
//    }
//    protected void btnCancel_Click(object sender, EventArgs e)
//    {
//        ToggleEditMode(false);
//    }

//    protected void btnDelete_Click(object sender, EventArgs e)
//    {
//        string firstName = Request.QueryString["First_Name"];
//        if (string.IsNullOrEmpty(firstName)) return;    

//        string query = "DELETE FROM students WHERE First_Name = @FirstName";

//        using (var conn = new MySqlConnection(connectionString))
//        using (var cmd = new MySqlCommand(query, conn))
//        {
//            cmd.Parameters.AddWithValue("@FirstName", firstName);

//            try
//            {
//                conn.Open();
//                int rowsAffected = cmd.ExecuteNonQuery();
//                lblDetails.Text = rowsAffected > 0 ? "User deleted successfully." : "Delete failed.";
//                if (rowsAffected > 0) ToggleEditMode(false);
//            }
//            catch (Exception ex)
//            {
//                lblDetails.Text = "An error occurred while deleting the user.";
//                // Log the exception details here (use a logging framework)
//                System.Diagnostics.Debug.WriteLine("Error deleting user: {ex.Message}");
//            }
//        }
//    }
//    private void ToggleEditMode(bool isEditMode)
//    {
//        lblDetails.Visible = !isEditMode;
//        btnUpdate.Visible = !isEditMode;
//        btnSave.Visible = isEditMode;
//        btnCancel.Visible = isEditMode;
//        btnDelete.Visible = !isEditMode;

//        txtFirstName.Visible = isEditMode;
//        txtLastName.Visible = isEditMode;
//        txtEmail.Visible = isEditMode;
//        txtPhoneNumber.Visible = isEditMode;
//        txtDOB.Visible = isEditMode;
//        txtAddress.Visible = isEditMode;
//    }
//}

////using System;
////using System.Web.UI;
////using MySql.Data.MySqlClient;

////public partial class UserDetails : System.Web.UI.Page
////{
////    private readonly string connectionString = "Server=127.0.0.1;Port=3306;Database=student_registration;User ID=root;Password=root;";

////    protected void Page_Load(object sender, EventArgs e)
////    {
////        if (!IsPostBack)
////        {
////            string firstName = Request.QueryString["First_Name"];
////            if (!string.IsNullOrEmpty(firstName))
////            {
////                LoadUserDetails(firstName);
////            }
////        }
////    }

////    private void LoadUserDetails(string firstName)
////    {
////        string query = "SELECT * FROM students WHERE First_Name = @FirstName";

////        using (var conn = new MySqlConnection(connectionString))
////        using (var cmd = new MySqlCommand(query, conn))
////        {
////            cmd.Parameters.AddWithValue("@FirstName", firstName);

////            try
////            {
////                conn.Open();
////                using (var reader = cmd.ExecuteReader())
////                {
////                    if (reader.Read())
////                    {
////                        lblDetails.Text = "First Name:"+ reader["First_Name"]+"<br/>" +
////                                          "Last Name: "+reader["Last_Name"]+"<br/>" +
////                                          "Email: "+reader["Email"]+"<br/>" +
////                                          "Phone Number: "+reader["Phone_Number"]+"<br/>" +
////                                          "Date of Birth: "+reader["Date_Of_Birth"]+"<br/>" +
////                                          "Address: "+reader["Address"]+"<br/>";
////                        PopulateTextFields(reader);
////                        ToggleEditMode(false); // Set the initial state to view mode
////                    }
////                    else
////                    {
////                        lblDetails.Text = "User not found.";
////                    }
////                }
////            }
////            catch (Exception ex)
////            {
////                lblDetails.Text = "An error occurred while loading user details.";
////                // Log the exception details here (use a logging framework)
////                System.Diagnostics.Debug.WriteLine("Error loading user details: {ex.Message}");
////            }
////        }
////    }

////    private void PopulateTextFields(MySqlDataReader reader)
////    {
////        txtFirstName.Text = reader["First_Name"].ToString();
////        txtLastName.Text = reader["Last_Name"].ToString();
////        txtEmail.Text = reader["Email"].ToString();
////        txtPhoneNumber.Text = reader["Phone_Number"].ToString();
////        txtDOB.Text = reader["Date_Of_Birth"].ToString();
////        txtAddress.Text = reader["Address"].ToString();
////    }

////    protected void btnUpdate_Click(object sender, EventArgs e)
////    {
////        string oldFirstName = Request.QueryString["First_Name"];
////        if (string.IsNullOrEmpty(oldFirstName)) return;

////        string query = "UPDATE students SET First_Name = @NewFirstName, Last_Name = @LastName, Email = @Email, Phone_Number = @PhoneNumber, Date_Of_Birth = @DateOfBirth, Address = @Address WHERE First_Name = @OldFirstName";

////        using (var conn = new MySqlConnection(connectionString))
////        using (var cmd = new MySqlCommand(query, conn))
////        {
////            cmd.Parameters.AddWithValue("@NewFirstName", txtFirstName.Text);
////            cmd.Parameters.AddWithValue("@LastName", txtLastName.Text);
////            cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
////            cmd.Parameters.AddWithValue("@PhoneNumber", txtPhoneNumber.Text);
////            cmd.Parameters.AddWithValue("@DateOfBirth", txtDOB.Text);
////            cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
////            cmd.Parameters.AddWithValue("@OldFirstName", oldFirstName);

////            try
////            {
////                conn.Open();
////                int rowsAffected = cmd.ExecuteNonQuery();
////                lblDetails.Text = rowsAffected > 0 ? "User details updated successfully." : "Update failed. No rows affected.";
////            }
////            catch (Exception ex)
////            {
////                lblDetails.Text = "An error occurred while updating user details.";
////                // Log the exception details here (use a logging framework)
////                System.Diagnostics.Debug.WriteLine("Error updating user details: {ex.Message}");
////            }
////        }

////        ToggleEditMode(true);
////    }

////    protected void btnCancel_Click(object sender, EventArgs e)
////    {
////        ToggleEditMode(true);
////    }

////    protected void btnDelete_Click(object sender, EventArgs e)
////    {
////        string firstName = Request.QueryString["First_Name"];
////        if (string.IsNullOrEmpty(firstName)) return;

////        string query = "DELETE FROM students WHERE First_Name = @FirstName";

////        using (var conn = new MySqlConnection(connectionString))
////        using (var cmd = new MySqlCommand(query, conn))
////        {
////            cmd.Parameters.AddWithValue("@FirstName", firstName);

////            try
////            {
////                conn.Open();
////                int rowsAffected = cmd.ExecuteNonQuery();
////                lblDetails.Text = rowsAffected > 0 ? "User deleted successfully." : "Delete failed.";
////                if (rowsAffected > 0) ToggleEditMode(false);
////            }
////            catch (Exception ex)
////            {
////                lblDetails.Text = "An error occurred while deleting the user.";
////                // Log the exception details here (use a logging framework)
////                System.Diagnostics.Debug.WriteLine("Error deleting user: {ex.Message}");
////            }
////        }
////    }

////    private void ToggleEditMode(bool isEditMode)
////    {
////        lblDetails.Visible = !isEditMode;
////        btnUpdate.Visible = !isEditMode;
////        btnCancel.Visible = !isEditMode;
////        btnDelete.Visible = !isEditMode;

////        txtFirstName.Visible = isEditMode;
////        txtLastName.Visible = isEditMode;
////        txtEmail.Visible = isEditMode;
////        txtPhoneNumber.Visible = isEditMode;
////        txtDOB.Visible = isEditMode;
////        txtAddress.Visible = isEditMode;
////    }
////}
