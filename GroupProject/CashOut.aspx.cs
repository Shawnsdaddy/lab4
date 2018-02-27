using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Windows.Forms;
using System.Net.Mail;


public partial class CashOut : System.Web.UI.Page
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
        if (!IsPostBack)
        {
            Session["CashoutAmount"] = null;
            rewardpool();
        }
    }

    protected void cashout_Click(object sender, EventArgs e)
    {
        if(Session["CashoutAmount"] == null)
        {
            Response.Write("<script>alert('Please select Reward Amount you want to get')</script>");

        }
        else
        {
            SqlConnection sc = new SqlConnection();
            sc.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
            sc.Open();
            SqlCommand insert = new SqlCommand();
            insert.Connection = sc;

            insert.CommandText = "SELECT [TotalAmount] FROM [MoneyTransaction] where MoneyTransactionID=(select max(MoneyTransactionID) from MoneyTransaction)";
            SqlDataReader reader = insert.ExecuteReader();

            if (reader.HasRows)
            {
                reader.Read();
                int totalPoints = Convert.ToInt32(reader["TotalAmount"]);
                int transactionAmount = Convert.ToInt32(rblcashout.SelectedValue);
                reader.Close();
                if (totalPoints >= transactionAmount && (Convert.ToInt32(Session["PointsBalance"]) > transactionAmount))
                {
                    MoneyTransaction newTransaction = new MoneyTransaction(totalPoints, DateTime.Today.ToShortDateString(), transactionAmount, DateTime.Today.ToShortDateString(), Session["loggedIn"].ToString(), Convert.ToInt32(Session["ID"]));
                    insert.CommandText = "INSERT INTO [dbo].[MoneyTransaction] ([Date],[TotalAmount],[TransactionAmount],[LastUpdated],[LastUpdatedBy],[PersonID])" +
                    "VALUES (@Date,@TotalAmount,@TransactionAmount,@LastUpdated,@LastUpdatedBy,@PersonID)";
                    insert.Parameters.AddWithValue("@TotalAmount", totalPoints - transactionAmount);
                    insert.Parameters.AddWithValue("@Date", newTransaction.getDate());
                    insert.Parameters.AddWithValue("@TransactionAmount", newTransaction.getTransactionAmount());
                    insert.Parameters.AddWithValue("@LastUpdated", newTransaction.getLUD());
                    insert.Parameters.AddWithValue("@LastUpdatedBy", newTransaction.getLUDB());
                    insert.Parameters.AddWithValue("@PersonID", newTransaction.getPersonID());
                    insert.ExecuteNonQuery();
                    sc.Close();
                    Response.Write("<script>alert('Transaction Submited')</script>");
                    Send_Mail(Session["E-mail"].ToString(), newTransaction.getTransactionAmount());
                    rewardpool();
                }
                else
                {
                    Response.Write("<script>alert('personal points not enough or Bank balance low')</script>");
                }
            }


        }
    }

    protected void rblcashout_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["CashoutAmount"] = rblcashout.SelectedValue;
    }


    private void rewardpool()
    {
        SqlConnection sc = new SqlConnection();
        sc.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
        sc.Open();
        SqlCommand insert = new SqlCommand();
        insert.Connection = sc;
        insert.CommandText = "select pointsbalance from person where personID=@id";
        insert.Parameters.AddWithValue("@id", Session["ID"]);
        SqlDataReader reader = insert.ExecuteReader();
        reader.Read();
        Session["PointsBalance"] = Convert.ToInt32(reader["pointsbalance"]);
        lblPoints.Text = "Your Current Points Balance: " + Session["PointsBalance"].ToString();
    }

    public void Send_Mail(String email, String Amount)
    {
        String message = "Dear Employee: \n";
        message += "Your Tranaction of Cashing out"+Amount+" has been submited!!\n";
        MailMessage mail = new MailMessage("elkmessage@gmail.com", email, "Your Transaction has been submitted (DO NOT REPLY)", message);
        SmtpClient client = new SmtpClient();
        client.EnableSsl = true;
        client.Port = 587;
        client.DeliveryMethod = SmtpDeliveryMethod.Network;
        client.Credentials = new System.Net.NetworkCredential("elkmessage@gmail.com", "javapass");
        client.Host = "smtp.gmail.com";
        client.Send(mail);
    }
}