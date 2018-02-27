using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;

public partial class loginScreen : System.Web.UI.Page
{
    [System.Web.Services.WebMethod()]
    [System.Web.Script.Services.ScriptMethod()]

    protected void Page_Load(object sender, EventArgs e)
    {
        HttpContext.Current.Response.AddHeader("Cache-Control", "no-cache, no-store, must-revalidate");
        HttpContext.Current.Response.AddHeader("Pragma", "no-cache");
        HttpContext.Current.Response.AddHeader("Expires", "0");
        //setAccount("admin", "admin");
        //setAccount("employee1", "employee1");
        //setAccount("javauser", "javapass");

    }

    protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
    {
        string userName = Login1.UserName;
        string password = Login1.Password;

        e.Authenticated = false;

        SqlConnection con = new SqlConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
        con.Open();
        SqlCommand command = new SqlCommand();
        command.Connection = con;

        command.CommandText = "select [E-mail],firstname,loginCount,PointsBalance,PersonID,Password,Position from person where person.username = '" + userName + "' ";
        SqlDataReader reader = command.ExecuteReader();

        if (reader.HasRows)
        {
            reader.Read();
            Session["E-mail"]=reader["E-mail"].ToString();
            Session["FirstName"] = reader["firstname"].ToString();
            Session["PointsBalance"] = Convert.ToInt32(reader["PointsBalance"]);
            Session["loginCount"] = reader["loginCount"].ToString();
            Session["ID"] = Convert.ToInt32(reader["PersonID"]);
            String pwHash = reader["Password"].ToString();  // retrieve the password hash
            String Position = reader["Position"].ToString();
            bool verify = SimpleHash.VerifyHash(password, "MD5", pwHash);
            e.Authenticated = verify;
            switch (Position)
            {
                case "CEO":
                    Login1.DestinationPageUrl = "CEOPostWall.aspx";
                    break;
                case "EMPLOYEE":
                    Login1.DestinationPageUrl = "employeeReward.aspx";
                    break;
            }
        }
        reader.Close();
        if (e.Authenticated)
        {
            Session["loggedIn"] = userName;
            Session["Password"] = password;
            if (Session["loginCount"].ToString() == "0")
            {
                Login1.DestinationPageUrl = "firsttime.aspx";
            }
            else
            {
                SqlCommand login = new SqlCommand("login_count", con);
                login.CommandType = CommandType.StoredProcedure;
                login.Parameters.AddWithValue("@UserName", userName);
                login.ExecuteNonQuery();
            }
        }
        con.Close();

    }
    public void setAccount(string username, string password)
    {
        SqlConnection con = new SqlConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
        con.Open();
        SqlCommand command = new SqlCommand();
        command.Connection = con;
        string passwordHashNew =
               SimpleHash.ComputeHash(password, "MD5", null);
        command.CommandText = "update person set Password  = '" + passwordHashNew + "' where username = '" + username + "'";
        command.ExecuteNonQuery();
        con.Close();

    }

    protected void resendEmail_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
        con.Open();
        SqlCommand command = new SqlCommand();
        command.Connection = con;

        command.CommandText = "select [UserName],[E-mail] from person where person.[E-mail] = @email ";
        command.Parameters.AddWithValue("@email", txtEmail.Text);
        SqlDataReader reader = command.ExecuteReader();

        if (reader.HasRows)
        {
            reader.Read();
            string username = reader["UserName"].ToString();
            string email = reader["E-mail"].ToString();
            Send_Mail(email, "Your login username is here: "+username);
            Response.Write("<script>alert('Your Username has been send to your email')</script>");
        }
        else {
            Response.Write("<script>alert('Your entered E-mail is not regristered in our Database')</script>");
        }
        con.Close();
    }
    protected void btnResetPass_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
        con.Open();
        SqlCommand command = new SqlCommand();
        command.Connection = con;

        command.CommandText = "select [UserName],[E-mail] from person where [UserName] = @username ";
        command.Parameters.AddWithValue("@username", txtResetUserName.Text);
        SqlDataReader reader = command.ExecuteReader();

        if (reader.HasRows)
        {
            reader.Read();
            string username = reader["UserName"].ToString();
            string email = reader["E-mail"].ToString();
            reader.Close();
            string password = System.Web.Security.Membership.GeneratePassword(8, 6);
            string passwordHashNew = SimpleHash.ComputeHash(password, "MD5", null);

            command.CommandText = "UPDATE [dbo].[Person] SET [Password] = @password WHERE username=@username";

            command.Parameters.AddWithValue("@password", passwordHashNew);
            command.ExecuteNonQuery();     
           Send_Mail(email, "Your login username is here: " + password);
            Response.Write("<script>alert('Your temporary password has been send to your email')</script>");
        }
        else
        {
            Response.Write("<script>alert('Your entered UserName is not regristered in our Database')</script>");
        }
        con.Close();
    }

    public void Send_Mail(String email, String Message)
    {
        MailMessage mail = new MailMessage("elkmessage@gmail.com", email, "Important Information(DO NOT REPLY)", Message);
        SmtpClient client = new SmtpClient();
        client.EnableSsl = true;
        client.Port = 587;
        client.DeliveryMethod = SmtpDeliveryMethod.Network;
        client.Credentials = new System.Net.NetworkCredential("elkmessage@gmail.com", "javapass");
        client.Host = "smtp.gmail.com";
        client.Send(mail);
    }
}


