using System;
using System.Data;
using System.IO;
using MySql.Data.MySqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGridView();
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string connectionString = "Server=127.0.0.1;Port=3306;Database=student_registration;User ID=root;Password=root;";
        string uploadsFolder = Server.MapPath("~/Uploads/");

        try
        {
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            if (FileUpload1.HasFile)
            {
                string fileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
                string filePath = Path.Combine(uploadsFolder, fileName);
                FileUpload1.SaveAs(filePath);

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO students (first_Name, last_Name, email, phone_number, date_of_birth, gender, address, document_path, resume) " +
                                   "VALUES (@FirstName, @LastName, @Email, @PhoneNumber, @DateOfBirth, @Gender, @Address, @DocumentPath, @resume)";

                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@student_id", TextBox1.Text.Trim());
                        cmd.Parameters.AddWithValue("@FirstName", TextBox1.Text.Trim());
                        cmd.Parameters.AddWithValue("@LastName", TextBox2.Text.Trim());
                        cmd.Parameters.AddWithValue("@Email", TextBox3.Text.Trim());
                        cmd.Parameters.AddWithValue("@PhoneNumber", TextBox6.Text.Trim());
                        cmd.Parameters.AddWithValue("@DateOfBirth", Calendar1.SelectedDate.ToString("yyyy-MM-dd"));
                        cmd.Parameters.AddWithValue("@Gender", DropDownList1.SelectedValue);
                        cmd.Parameters.AddWithValue("@Address", TextBox4.Text.Trim());
                        cmd.Parameters.AddWithValue("@DocumentPath", "~/Uploads/" + fileName);
                        cmd.Parameters.AddWithValue("@resume", "~/Uploads/" + fileName);

                        cmd.ExecuteNonQuery();
                    }

                    lblMessage.Text = "Student registered successfully.";
                    lblMessage.ForeColor = System.Drawing.Color.Green;

                    BindGridView();
                }

                UploadedImage.ImageUrl = "~/Uploads/" + fileName;
                UploadedImage.Visible = true;
            }
            else
            {
                lblMessage.Text = "Please upload a file.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
            Response.Redirect("~/Default2.aspx");
        }
        catch (Exception ex)
        {
            lblMessage.Text = "An error occurred: " + ex.Message;
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }

    protected void BindGridView()
    {
        string connectionString = "Server=127.0.0.1;Port=3306;Database=student_registration;User ID=root;Password=root;";

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = "SELECT student_id, first_Name, last_Name, email, phone_number, date_of_birth, gender, address, document_path, resume FROM students";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // Debug: Check column names
                    foreach (DataColumn column in dt.Columns)
                    {
                        System.Diagnostics.Debug.WriteLine("Column: " + column.ColumnName);
                    }

                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "An error occurred: " + ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }
    }

    protected void Calendar1_SelectionChanged(object sender, EventArgs e)
    {
        HiddenDateOfBirth.Value = Calendar1.SelectedDate.ToString("yyyy-MM-dd");

        if (ScriptManager.GetCurrent(this) != null)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "updateAge", "updateDateOfBirth();", true);
        }
    }

    protected void DeleteStudent(int studentId)
    {
        string connectionString = "Server=127.0.0.1;Port=3306;Database=student_registration;User ID=root;Password=root;";

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = "DELETE FROM students WHERE student_id = @StudentId";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@StudentId", studentId);
                    cmd.ExecuteNonQuery();
                }

                lblMessage.Text = "Student deleted successfully.";
                lblMessage.ForeColor = System.Drawing.Color.Green;

                BindGridView();
            }
            catch (Exception ex)
            {
                lblMessage.Text = "An error occurred: " + ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            try
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                int studentId = Convert.ToInt32(GridView1.DataKeys[rowIndex].Value);

                DeleteStudent(studentId);
            }
            catch (Exception ex)
            {
                lblMessage.Text = "An error occurred: " + ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        try
        {
            if (FileUpload2.HasFile)
            {
                string fileName = Path.GetFileName(FileUpload2.PostedFile.FileName);
                string filePath = Server.MapPath("~/Uploads/") + fileName;

                FileUpload2.SaveAs(filePath);

                lblMessage.Text = "File uploaded successfully.";
                lblMessage.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                lblMessage.Text = "Please select a file to upload.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = "An error occurred: " + ex.Message;
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        try
        {
            if (FileUpload1.HasFile)
            {
                string fileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
                string filePath = Server.MapPath("~/Uploads/") + fileName;

                FileUpload1.SaveAs(filePath);

                lblMessage.Text = "File uploaded successfully.";
                lblMessage.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                lblMessage.Text = "Please select a file to upload.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = "An error occurred: " + ex.Message;
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }
}
