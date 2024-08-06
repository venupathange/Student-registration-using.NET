using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Drawing.Imaging;
using System.IO;
using System.Configuration;
using System.Text.RegularExpressions;
namespace webform
{
    public partial class Registration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ShowData();
        }

         protected void Button1_Click(object sender, EventArgs e)
          {

              SqlConnection con = new SqlConnection("Data Source = DESKTOP-76FS690\\MSSQLSERVER1; Initial Catalog=Registration; Integrated Security= True;");

              con.Open();
              SqlCommand cmd = new SqlCommand("insert into Registration (Firstname,Lastname,Email,DOB,Gender,Address,Phone,Photo,Class,Course) values (@Firstname,@Lastname,@Email,@DOB,@Gender,@Address,@Phone,@Photo,@Class,@Course)", con);
              cmd.Parameters.AddWithValue("@Firstname", TextBox1.Text);
              cmd.Parameters.AddWithValue("@Lastname", TextBox2.Text);
              cmd.Parameters.AddWithValue("@Email", TextBox3.Text);
              cmd.Parameters.AddWithValue("@DOB", Calendar1.SelectedDate);
              cmd.Parameters.AddWithValue("@Gender", DropDownList1.SelectedValue);
              cmd.Parameters.AddWithValue("@Address", TextBox6.Text);
              cmd.Parameters.AddWithValue("@Phone", TextBox7.Text);


              if (FileUpload1.HasFile)
              {
                  string str = FileUpload1.FileName;
                  FileUpload1.PostedFile.SaveAs(Server.MapPath("~/Upload/" + str));
                  string Image = "~/Upload/" + str.ToString();

                  cmd.Parameters.AddWithValue("@Photo", Image);
              }


              cmd.Parameters.AddWithValue("@Class", DropDownList1.SelectedValue);
              cmd.Parameters.AddWithValue("@Course", TextBox10.Text);
              cmd.ExecuteNonQuery();
              con.Close();
              Label1.Text = "Registered Successfully";
              ShowData();
          }
      
        protected void ShowData()
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection("Data Source = DESKTOP-76FS690\\MSSQLSERVER1; Initial Catalog=Registration; Integrated Security= True;");
            SqlDataAdapter adapt = new SqlDataAdapter("select * from Registration", con);
            con.Open();
            adapt.Fill(dt);
            con.Close();
            if (dt.Rows.Count > 0)
            {
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string firstName = GridView1.DataKeys[e.RowIndex].Value.ToString();
            string deleteSql = @"DELETE FROM Registration 
                         WHERE Firstname = @firstName;";
            using (var con = new SqlConnection("Data Source = DESKTOP-76FS690\\MSSQLSERVER1; Initial Catalog=Registration; Integrated Security= True;"))
            using (var cmd = new SqlCommand(deleteSql, con))
            {
                cmd.Parameters.Add("@firstName", SqlDbType.VarChar).Value = firstName;
                con.Open();
                int deleted = cmd.ExecuteNonQuery();
            }

            ShowData();
        }


        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

            GridViewRow row = GridView1.Rows[e.RowIndex];

            string firstname = (row.Cells[2].Controls[0] as TextBox).Text;
            string lastname = (row.Cells[3].Controls[0] as TextBox).Text;
            string email = (row.Cells[4].Controls[0] as TextBox).Text;
            string dobVal = (row.Cells[5].Controls[0] as TextBox).Text;
            string genderVal = (row.Cells[6].Controls[0] as TextBox).Text;
            string addressVal = (row.Cells[7].Controls[0] as TextBox).Text;
            string phoneVal = (row.Cells[8].Controls[0] as TextBox).Text;         
            string classVal = (row.Cells[9].Controls[0] as TextBox).Text;
            string courseVal = (row.Cells[10].Controls[0] as TextBox).Text;

         //   string str = (row.Cells[11].Controls[0] as FileUpload).PostedFile.FileName;
           
           // FileUpload imageVal = (row.Cells[2].Controls[0] as FileUpload);
           // imageVal.PostedFile.SaveAs(Server.MapPath("~/Upload/" + str));
            //string image = "~/Upload/" + str.ToString();


            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection("Data Source = DESKTOP-76FS690\\MSSQLSERVER1; Initial Catalog=Registration; Integrated Security= True;");
            con.Open();
            //SqlConnection con = new SqlConnection("Data Source = DESKTOP-76FS690\\MSSQLSERVER1; Initial Catalog=Registration; Integrated Security= True;");
            SqlCommand cmd = new SqlCommand("update Registration set Firstname = '" + firstname + "',  Lastname = '" + lastname + "', Email = '" + email + "', DOB= '" + dobVal + " ', Gender = '" + genderVal + "',Address = '" + addressVal + "', Phone = '" + phoneVal + "', Class = '" + classVal + "', Course = '" + courseVal + "' where Firstname = '" + firstname + "'", con);
            cmd.ExecuteNonQuery();
            SqlDataAdapter adapt = new SqlDataAdapter("select * from Registration", con);
            GridView1.EditIndex = -1;
            adapt.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
            con.Close();


        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get the selected value
            string selectedValue = DropDownList1.SelectedValue;
            string selectedText = DropDownList1.SelectedItem.Text;

            // Display the selected value
            gender.Text = "Selected Gender " + selectedValue ;
        }
        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get the selected value
            string selectedValue = DropDownList2.SelectedValue;
            string selectedText = DropDownList2.SelectedItem.Text;

            // Display the selected value
            Label2.Text = "Selected Class " + selectedValue ;
        }
        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;

        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            GridView1.DataBind();

        }
        /* protected void Button2_Click(object sender, EventArgs e)
         {
             SqlConnection con = new SqlConnection("Data Source = DESKTOP-76FS690\\MSSQLSERVER1; Initial Catalog=Registration; Integrated Security= True;");
             con.Open();
             //SqlConnection con = new SqlConnection("Data Source = DESKTOP-76FS690\\MSSQLSERVER1; Initial Catalog=Registration; Integrated Security= True;");
             SqlCommand cmd = new SqlCommand("update Registration set Firstname = '" + TextBox1.Text + "',  Lasttname = '" + TextBox2.Text + "', Email = '" + TextBox3.Text + "', DOB= '" + Calendar1.SelectedDate + " ', Gender = '" + TextBox5.Text + "',Address = '" + TextBox6.Text + "', Phone = '" + TextBox7.Text + "', Class = '" + TextBox9.Text + "',Class = '" + TextBox9.Text + "' where Firstname = '" + TextBox1.Text + "'", con);
             cmd.ExecuteNonQuery();
             con.Close();
             ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Success');", true);
              LoadRecord();
         }*/
    }

}