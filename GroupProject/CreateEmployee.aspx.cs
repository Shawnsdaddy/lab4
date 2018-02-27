using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Windows.Forms;
using System.Configuration;

public partial class CreateEmployee : System.Web.UI.Page
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

    protected void BtnCommit_Click(object sender, EventArgs e)
    {
        try
        {
            Person employee = new Person(txtFirstName.Text, txtLastName.Text, txtEmail.Text);
            employee.setLastUpdatedBy((string)(Session["loggedIn"]));
            SqlConnection sc = new SqlConnection();
            sc.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
            sc.Open();
            SqlCommand insert = new SqlCommand();
            insert.Connection = sc;
            insert.CommandText = "select [E-mail] from [Person] where [E-mail] = @Email";
            insert.Parameters.AddWithValue("@Email", employee.getEmail());
            SqlDataReader reader = insert.ExecuteReader();

            if (reader.HasRows)
            {
                Response.Write("<script>alert('Email record has already existed in Database')</script>");
                reader.Close();
                sc.Close();
            }
            else
            {
                reader.Close();
                insert.CommandText = "INSERT INTO [dbo].[Person] ([FirstName],[LastName],[MI],[E-mail],[Position],[Password],[UserName],[PointsBalance],[PendingPoints],[LastUpdated],[LastUpdatedBy],[BusinessEntityID],[ManagerID],[loginCount]) VALUES" +
               "(@FirstName,@LastName,@MI,@Email,@Position,@Password,@UserName,@PointsBalance,@PendingPoints,@LastUpdated,@LastUpdatedBy,@BusinessEntityID,@ManagerID,0)";
                insert.Parameters.AddWithValue("@FirstName", employee.getFirstName());
                insert.Parameters.AddWithValue("@LastName", employee.getLastName());
                insert.Parameters.AddWithValue("@Position", employee.getPosition());
                insert.Parameters.AddWithValue("@PointsBalance", employee.getPointsBalance());
                insert.Parameters.AddWithValue("@PendingPoints", employee.getPendingPoints());
                insert.Parameters.AddWithValue("@BusinessEntityID", employee.getBusinessEntityID());
                insert.Parameters.AddWithValue("@LastUpdatedBy", employee.getLastUpdatedBy());
                insert.Parameters.AddWithValue("@LastUpdated", employee.getLastUpdated());

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
                string password = System.Web.Security.Membership.GeneratePassword(8, 6);
                string passwordHashNew = SimpleHash.ComputeHash(password, "MD5", null);

                insert.Parameters.AddWithValue("@Password", passwordHashNew);
                insert.Parameters.AddWithValue("@UserName", employee.getEmail());
                insert.ExecuteNonQuery();
                sc.Close();
                Send_Mail(employee.getEmail(), employee.getEmail(), password);

                Response.Write("<script>alert('Employee Account: " + employee.getFirstName() + "" + employee.getMI() + " " + employee.getLastName() + " is created')</script>");
                txtFirstName.Text = string.Empty;
                txtMI.Text = string.Empty;
                txtLastName.Text = string.Empty;
                txtEmail.Text = string.Empty;
                txtManagerID.Text = string.Empty;
            }
    }
        catch
        {
            Response.Write("<script>alert('ManagerID not found in Database')</script>");
        }
        
    }
    public void Send_Mail(String email,String Name,String Password)
    {
        String message = "Dear Employee: \n";
        message += "Your CEO has Created an Account for you!!\n";
        message += "Please login with UserName and Password provides below:\n";
        message += "UserName:  "+Name + "\n PassWord: " + Password+"\n";
        MailMessage mail = new MailMessage("elkmessage@gmail.com",email, "Your Account Has been Created(DO NOT REPLY)", message);
        SmtpClient client = new SmtpClient();
        client.EnableSsl = true;
        client.Port = 587;
        client.DeliveryMethod = SmtpDeliveryMethod.Network;
        client.Credentials = new System.Net.NetworkCredential("elkmessage@gmail.com", "javapass");
        client.Host = "smtp.gmail.com";        
        client.Send(mail);
    }
}