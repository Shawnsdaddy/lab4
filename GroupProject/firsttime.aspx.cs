using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Configuration;

public partial class firsttime : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        HttpContext.Current.Response.AddHeader("Cache-Control", "no-cache, no-store, must-revalidate");
        HttpContext.Current.Response.AddHeader("Pragma", "no-cache");
        HttpContext.Current.Response.AddHeader("Expires", "0");
        if (Session["loggedIn"] == null)
        {
            Response.Redirect("default.aspx");
        }
   }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        SqlConnection sc = new SqlConnection();
        sc.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
        sc.Open();
        SqlCommand insert = new SqlCommand();
        String userName = txtUsername.Text;
        insert.Connection = sc;
        insert.CommandText = "select user from person where person.username =@newUserName and PersonID!=@id";
        insert.Parameters.AddWithValue("@newUserName", userName);
        insert.Parameters.AddWithValue("@id", Convert.ToInt32(Session["ID"]));
        SqlDataReader reader = insert.ExecuteReader();  
            if (!reader.HasRows)
            {
                string passwordHashNew = SimpleHash.ComputeHash(txtNew1.Text, "MD5", null);
                //SqlConnection sc = new SqlConnection();
                //sc.ConnectionString = "Data Source=groupproject.clltaluyh8dp.us-east-1.rds.amazonaws.com;Initial Catalog=RewardSystemLab4;Persist Security Info=True;User ID=javauser;Password=javapass";
                //sc.Open();
                //SqlCommand insert = new SqlCommand();
                //insert.Connection = sc;
                //insert.CommandText = "UPDATE [dbo].[Person] SET [Password] = @Password WHERE username= @username";

                //insert.Parameters.AddWithValue("@username", username);
                //insert.ExecuteNonQuery();
                //sc.Close();
                reader.Close();
                insert.CommandText = "update person set username = @newUserName,[Password] = @Password  where username = @oldUserName";      
                insert.Parameters.AddWithValue("@oldUserName", Session["loggedIn"].ToString());
                insert.Parameters.AddWithValue("@Password", passwordHashNew);
                insert.ExecuteNonQuery();

                SqlCommand login = new SqlCommand("login_count", sc);
                login.CommandType = CommandType.StoredProcedure;
                login.Parameters.AddWithValue("@UserName", userName);
                login.ExecuteNonQuery();
                Session["loggedIn"] = userName;
            sc.Close();
            //bool verify = SimpleHash.VerifyHash(password, "MD5", pwHash);
            Response.Redirect("EmployeeReward.aspx");

            }
            else
            {
            Response.Write("<script>alert('Sorry, username has been taken')</script>");
            sc.Close();
        }
            
        
    }
}