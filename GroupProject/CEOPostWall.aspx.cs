using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class CEOLogin : System.Web.UI.Page
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
        else
        {
            lblInfo.Text = "Welcome to Peer Review System! " + Session["FirstName"].ToString();
            LatestUpdates();
            //lblPoints.Text = Session["PointsBalance"].ToString();
            rewardpool();
        }
            
    }
    private void LatestUpdates()
    {
        //if (Session["Name"] != null)
        //{
        //    lblName.Text = Session["Name"].ToString();
        //}
        SqlConnection sc = new SqlConnection();
        sc.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
        sc.Open();
        DataTable dt = new DataTable();
        //string query = "SELECT u.RegisterId,u.Name,s.FromId,s.ToId,s.Post,s.PostId,s.PostDate FROM [Register] as u, Posts as s WHERE u.RegisterId=s.FromId AND s.ToId='" + Session["CurrentProfileId"] + "' order by s.PostId desc";
        //dt = Database.GetData(query);
        //string getpost = "SELECT [TransactionID],[PointsAmount],[Date],[EventDescription],[LastUpdated],[LastUpdatedBy],[ReceiverID],[RewarderID],[CategoryID],[ValueID] FROM [dbo].[PeerTransaction] order by TransactionID desc";
        string getpost = "SELECT PeerTransaction.Date, PeerTransaction.EventDescription, PeerTransaction.LastUpdated, PeerTransaction.PointsAmount, Person.FirstName AS RewarderName, Category.Title, Value.ValueName, Person_1.FirstName AS ReceiverName " +
            "FROM PeerTransaction INNER JOIN  Person ON PeerTransaction.ReceiverID = Person.PersonID INNER JOIN Value ON PeerTransaction.ValueID = Value.ValueID INNER JOIN Category ON PeerTransaction.CategoryID = Category.CategoryID INNER JOIN " +
            "Person AS Person_1 ON PeerTransaction.RewarderID = Person_1.PersonID ORDER BY PeerTransaction.TransactionID DESC";
        SqlCommand cmd = new SqlCommand(getpost, sc);
        SqlDataReader sdr = cmd.ExecuteReader();
        dlPosts.DataSource = sdr;
        dlPosts.DataBind();
        sc.Close();
    }

    protected void btnCommit_Click(object sender, EventArgs e)
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
            int transactionAmount = Convert.ToInt32(txtFrontLoad.Text);
            reader.Close();

                MoneyTransaction newTransaction = new MoneyTransaction(totalPoints, DateTime.Today.ToShortDateString(), transactionAmount, DateTime.Today.ToShortDateString(), Session["loggedIn"].ToString(), Convert.ToInt32(Session["ID"]));
                insert.CommandText = "INSERT INTO [dbo].[MoneyTransaction] ([Date],[TotalAmount],[TransactionAmount],[LastUpdated],[LastUpdatedBy],[PersonID])" +
                "VALUES (@Date,@TotalAmount,@TransactionAmount,@LastUpdated,@LastUpdatedBy,@PersonID)";
                insert.Parameters.AddWithValue("@TotalAmount", totalPoints + transactionAmount);
                insert.Parameters.AddWithValue("@Date", newTransaction.getDate());
                insert.Parameters.AddWithValue("@TransactionAmount", transactionAmount);
                insert.Parameters.AddWithValue("@LastUpdated", newTransaction.getLUD());
                insert.Parameters.AddWithValue("@LastUpdatedBy", newTransaction.getLUDB());
                insert.Parameters.AddWithValue("@PersonID", newTransaction.getPersonID());
                insert.ExecuteNonQuery();
            
            //insert.CommandText = "Update person set [PointsBalance]=[PointsBalance]+@TransactionAmount where personid=PersonID";
            //insert.ExecuteNonQuery();
            sc.Close();
            rewardpool();
        }

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
        lblPoints.Text = "Pool Balance:"+Session["PointsBalance"].ToString();
    }
}