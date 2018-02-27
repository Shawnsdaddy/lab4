using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class EmployeeReward : System.Web.UI.Page
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
            rewardpool();
            LatestUpdates();
            updatecombox();
            if (!IsPostBack)
            {
                reset();
            }
            lblInfo.Text = "Welcome to Peer Review System! " + Session["FirstName"].ToString();
        }
    }

    [System.Web.Services.WebMethod()]
    [System.Web.Script.Services.ScriptMethod()]
    public static List<string> SearchName(string prefixText, int count)
    {
        //Connect to Database
        SqlConnection sc = new SqlConnection();
        sc.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
        sc.Open();
        //Send Command
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = sc;
        cmd.CommandText = "SELECT ([FirstName] + isnull([MI],'')+[LastName]) as RewardName FROM [dbo].[Person] where FirstName like '%' + @SearchText + '%' or LastName like '%' + @SearchText + '%'";
        //cmd.CommandText = "SELECT [FirstName] FROM[RewardSystemLab4].[dbo].[Person]  where FirstName like '%' + @SearchText + '%'";
        cmd.Parameters.AddWithValue("@SearchText", prefixText);


        List<string> NameLists = new List<string>();
        SqlDataReader sdr = cmd.ExecuteReader();

        while (sdr.Read())
        {
            NameLists.Add(sdr["RewardName"].ToString());
        }

        sc.Close();
        return NameLists;
    }

    protected void btnCommit_Click(object sender, EventArgs e)
    {
        if (Session["ReceiverID"] == null||Session["ValueID"] == null|| Session["CategoryID"] == null  || Session["RewardAmount"]==null)
        {
            Response.Write("<script>alert('Please select Receiver Name,Value and Category')</script>");
            popReward.Show();
        }
        else if (Session["ValueID"].ToString() == "-1"|| Session["CategoryID"].ToString() == "-1")
        {
            Response.Write("<script>alert('Please select Receiver Name,Value and Category')</script>");
            popReward.Show();
        }
        else{
            double pointsAmount = Convert.ToDouble(rblRewardPoints.SelectedValue);
            string EventDate = "2/17/2018"; // add textbox to enter
            string EventDescription = txtRDescription.Text;
            string LastUpdated = DateTime.Now.ToShortDateString();
            string LastUpdatedBy = Session["loggedIn"].ToString();
            int ReceiverID = Convert.ToInt32(Session["ReceiverID"]);
            int RewarderID = Convert.ToInt32(Session["ID"]);
            int CategoryID = Convert.ToInt32(ddlRCategory.SelectedValue);
            int ValueID = Convert.ToInt32(ddlRValue.SelectedValue);

            //try
            //{
            //Connect to Database
            SqlConnection sc = new SqlConnection();
            sc.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
            sc.Open();
            PeerTranscation emp = new PeerTranscation(pointsAmount, EventDate, EventDescription, LastUpdated, LastUpdatedBy, ReceiverID, RewarderID, CategoryID, ValueID);

            string sqlString = "INSERT INTO [dbo].[PeerTransaction]([PointsAmount],[Date],[EventDescription],[LastUpdated],[LastUpdatedBy],[ReceiverID],[RewarderID],[CategoryID],[ValueID]) VALUES (@PointsAmount,@Date,@EventDescription,@LastUpdated,@LastUpdatedBy,@ReceiverID,@RewarderID,@CategoryID,@ValueID)";

            SqlCommand insert = new SqlCommand(sqlString);
            insert.Connection = sc;

            insert.Parameters.AddWithValue("@PointsAmount", emp.getPoints());
            insert.Parameters.AddWithValue("@Date", emp.getDate());
            insert.Parameters.AddWithValue("@EventDescription", emp.getDescription());
            insert.Parameters.AddWithValue("@LastUpdated", emp.getLUD());
            insert.Parameters.AddWithValue("@LastUpdatedBy", emp.getLUDB());
            insert.Parameters.AddWithValue("@ReceiverID", emp.getReceiverID());
            insert.Parameters.AddWithValue("@RewarderID", emp.getRewarderID());
            insert.Parameters.AddWithValue("@CategoryID", emp.getCategoryID());
            insert.Parameters.AddWithValue("@ValueID", emp.getValueID());


            insert.ExecuteNonQuery();
            sc.Close();
            //}


            //catch
            //{
            //    lblOutput.Text = "Fail add to database";
            //}
            LatestUpdates();
            reset();
            Response.Write("<script>alert('Peer reward Successful')</script>");
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
        string getpost = "SELECT PeerTransaction.Date, PeerTransaction.EventDescription, PeerTransaction.LastUpdated, PeerTransaction.PointsAmount, Person.FirstName AS ReceiverName, Category.Title, Value.ValueName, Person_1.FirstName AS RewarderName " +
            "FROM PeerTransaction INNER JOIN  Person ON PeerTransaction.ReceiverID = Person.PersonID INNER JOIN Value ON PeerTransaction.ValueID = Value.ValueID INNER JOIN Category ON PeerTransaction.CategoryID = Category.CategoryID INNER JOIN " +
            "Person AS Person_1 ON PeerTransaction.RewarderID = Person_1.PersonID ORDER BY PeerTransaction.TransactionID DESC";
        SqlCommand cmd = new SqlCommand(getpost, sc);
        SqlDataReader sdr = cmd.ExecuteReader();
        dlPosts.DataSource = sdr;
        dlPosts.DataBind();
        sc.Close();

    }

    private void updatecombox()
    {
        SqlConnection sc = new SqlConnection();
        sc.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
        sc.Open();

        DataTable dt = new DataTable();
        string getName = "SELECT PersonID, ([FirstName] + isnull([MI], '') +[LastName]) as RewardName FROM[dbo].[Person] Where Position != 'CEO' and PersonID !=" + Session["ID"];
        SqlDataAdapter da = new SqlDataAdapter(getName, sc);
        da.Fill(dt);
        cbName.DataSource = dt;
        cbName.DataTextField = "RewardName";
        cbName.DataValueField = "PersonID";
        cbName.DataBind();
        sc.Close();
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
        lblPoints.Text = "Current Balance: "+Session["PointsBalance"].ToString();
    }

    protected void cbName_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["ReceiverID"] = cbName.SelectedValue;
    }

    protected void ddlRValue_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["ValueID"] = ddlRValue.SelectedValue;
    }

    protected void ddlRCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["CategoryID"] = ddlRCategory.SelectedValue;
    }

    protected void rblRewardPoints_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["RewardAmount"] = rblRewardPoints.SelectedValue;
    }

    protected void reset()
    {
        Session["ReceiverID"] = null;
        Session["ValueID"] = null;
        Session["CategoryID"] = null;
        Session["RewardAmount"]=null;
    }
}