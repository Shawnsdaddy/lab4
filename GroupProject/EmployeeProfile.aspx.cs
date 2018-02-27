using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Configuration;

public partial class employeeProfile : System.Web.UI.Page
{

    [System.Web.Services.WebMethod()]
    [System.Web.Script.Services.ScriptMethod()]

    protected void Page_Load(object sender, EventArgs e)
    {
        HttpContext.Current.Response.AddHeader("Cache-Control", "no-cache, no-store, must-revalidate");
        HttpContext.Current.Response.AddHeader("Pragma", "no-cache");
        HttpContext.Current.Response.AddHeader("Expires", "0");

        if (Session["loggedIn"] == null)
        {
            Response.Redirect("default.aspx");
        }
        
        if (!Page.IsPostBack)
        {
            ShowEmpImage(Session["loggedIn"].ToString());
            SqlConnection sc = new SqlConnection();
            sc.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
            sc.Open();
            SqlCommand insert = new SqlCommand();
            String userName = Session["loggedIn"].ToString();
            insert.Connection = sc;
            insert.CommandText = "select FirstName, LastName, MI, [E-Mail],ManagerID from person where person.username = '" + userName + "' ";
            SqlDataReader reader = insert.ExecuteReader();

            if (reader.HasRows)
            {
                reader.Read();
                txtFirstName.Text = reader["FirstName"].ToString();
                txtLastName.Text = reader["LastName"].ToString();
                txtMI.Text = reader["MI"].ToString();
                txtEmail.Text = reader["E-Mail"].ToString();
                txtManagerID.Text = reader["ManagerID"].ToString();
                //bool verify = SimpleHash.VerifyHash(password, "MD5", pwHash);
            }
            sc.Close();
        }
    }

    protected void btnChangePassword_Click(object sender, EventArgs e)
    {


        string username = Session["loggedIn"].ToString();
        String oldpass = Session["Password"].ToString();
        if (oldpass == txtOldPass.Text)
        {
            string passwordHashNew = SimpleHash.ComputeHash(oldpass, "MD5", null);
            SqlConnection sc = new SqlConnection();
            sc.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
            sc.Open();
            SqlCommand insert = new SqlCommand();
            insert.Connection = sc;
            insert.CommandText = "UPDATE [dbo].[Person] SET [Password] = @Password WHERE username= @username";
            insert.Parameters.AddWithValue("@Password", passwordHashNew);
            insert.Parameters.AddWithValue("@username", username);
            insert.ExecuteNonQuery();
            sc.Close();
            Response.Write("<script>alert('New password has been set up')</script>");
        }
        else
        {
            Response.Write("<script>alert('OldPassword incorrect')</script>");
            popPass.Show();
        }
    }

    protected void btnChangeProfile_Click(object sender, EventArgs e)
    {
        try
        {
            string username = Session["loggedIn"].ToString();
            SqlConnection sc = new SqlConnection();
            sc.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
            sc.Open();
            SqlCommand insert = new SqlCommand();
            insert.Connection = sc;
            insert.CommandText = "select [E-mail] from [Person] where [E-mail] = @Email and username!=@username";
            insert.Parameters.AddWithValue("@Email", txtEmail.Text);
            insert.Parameters.AddWithValue("@username", username);
            SqlDataReader reader = insert.ExecuteReader();

            if (reader.HasRows)
            {
                Response.Write("<script>alert('Email record has already existed in Database')</script>");
                reader.Close();
                sc.Close();
                popProfile.Show();
            }
            else
            {
                reader.Close();
                insert.CommandText = "UPDATE [dbo].[Person] SET [FirstName] = @FirstName,[LastName] = @LastName,[MI] = @MI,[E-mail] = @Email, [LastUpdated] = @LastUpdated,[LastUpdatedBy] = @LastUpdatedBy, [ManagerID] = @ManagerID WHERE username=" +
                    "@username";
                insert.Parameters.AddWithValue("@FirstName", txtFirstName.Text);
                insert.Parameters.AddWithValue("@LastName", txtLastName.Text);

                insert.Parameters.AddWithValue("@LastUpdatedBy", Session["loggedIn"].ToString());
                insert.Parameters.AddWithValue("@LastUpdated", DateTime.Now.ToShortDateString());

                if (txtMI.Text.Trim() == "")
                {
                    insert.Parameters.AddWithValue("@MI", DBNull.Value);
                }
                else
                {
                    insert.Parameters.AddWithValue("@MI", txtMI.Text.Trim());
                }

                if (txtManagerID.Text.Trim() == "")
                {
                    insert.Parameters.AddWithValue("@ManagerID", DBNull.Value);
                }
                else
                {
                    insert.Parameters.AddWithValue("@ManagerID", txtManagerID.Text.Trim());
                }
                insert.ExecuteNonQuery();
                sc.Close();
                Response.Write("<script>alert('Information has been updated to database')</script>");
            }
        }
        catch
        {
            
            Response.Write("<script>alert('ManagerID not found in the database')</script>");
            popProfile.Show();
        }
        
        }
    

    protected void Upload_Click(object sender, EventArgs e)
    {
        try
        {
            HttpPostedFile postedFile = PictureUpload.PostedFile;
        string filename = Path.GetFileName(postedFile.FileName);
        string extension = Path.GetExtension(filename);
        if (extension.ToLower() == ".jpg" || extension.ToLower() == ".bmp" || extension.ToLower() == ".gif" || extension.ToLower() == ".png")
        {
            Stream stream = postedFile.InputStream;
            BinaryReader binaryReader = new BinaryReader(stream);
            byte[] bytes = binaryReader.ReadBytes((int)stream.Length);

            SqlConnection sc = new SqlConnection();

            sc.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
            sc.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = sc;
            cmd.CommandText = "UPDATE [dbo].[Person] SET [ProfilePicture] = @ProfilePicture WHERE username=@username";

            cmd.Parameters.AddWithValue("@ProfilePicture", bytes);
            cmd.Parameters.AddWithValue("@username", Session["loggedIn"].ToString());
            cmd.ExecuteNonQuery();
            sc.Close();
            ShowEmpImage(Session["loggedIn"].ToString());
                Response.Write("<script>alert('Your profile picture has been updated')</script>");
            }
        else
        {
            Response.Write("<script>alert('Only .jpg/.bmp/.gif/.png file can be upload')</script>");
                popPic.Show();
        }
    }
        catch
        {
            Response.Write("<script>alert('Picture Size is exceeding the database can take')</script>");
        }
        }


    public void ShowEmpImage(string empno)
    {
        SqlConnection sc = new SqlConnection();
        sc.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
        sc.Open();
        string sql = "SELECT profilepicture FROM person WHERE username = @username";
        SqlCommand cmd = new SqlCommand(sql, sc);
        cmd.Parameters.AddWithValue("@username", Session["loggedIn"]);
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.HasRows)
        {
            while (dr.Read())
            {
                if (!Convert.IsDBNull(dr["profilepicture"]))
                {
                    Byte[] imagedata = (byte[])dr["profilepicture"];
                    string img = Convert.ToBase64String(imagedata, 0, imagedata.Length);
                    ProfilePicture.ImageUrl = "data:image/png;base64," + img;
                }
                else
                {
                    ProfilePicture.ImageUrl = "https://micvadam.files.wordpress.com/2013/12/li-no-profile-photo.jpg";
                }
            }


        }
        sc.Close();

        //SqlConnection sc = new SqlConnection();
        //sc.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
        //sc.Open();
        //string sql = "SELECT profilepicture FROM person WHERE username = @username";
        //SqlCommand cmd = new SqlCommand(sql, sc);
        //cmd.Parameters.AddWithValue("@username", empno);
        //SqlDataReader dr = cmd.ExecuteReader();
        //if (dr.HasRows)
        //{
        //    while (dr.Read())
        //    {
        //        if (!Convert.IsDBNull(dr["profilepicture"]))
        //        {
        //            Byte[] imagedata = (byte[])dr["profilepicture"];
        //            string img = Convert.ToBase64String(imagedata, 0, imagedata.Length);
        //            ProfilePicture.ImageUrl = "data:image/png;base64," + img;
        //        }
        //        else
        //        {
        //            ProfilePicture.ImageUrl = "https://micvadam.files.wordpress.com/2013/12/li-no-profile-photo.jpg";
        //        }
        //    }
        //}
        //sc.Close();
    }
}