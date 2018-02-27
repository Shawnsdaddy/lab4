using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
public partial class EmployeeTeam : System.Web.UI.Page
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
            
            if (!IsPostBack)
            {
                fillcboUpdateTeam();
                Session["cboUpdate"] = null;
                Session["cboView"] = null;
                Session["cboJoin"] = null;
            }
           
        }
    }
    [System.Web.Services.WebMethod()]
    [System.Web.Script.Services.ScriptMethod()]

    protected void reset()
    {
        txtTeamName.Text = "";
        txtStartDate.Text = "";
        txtED.Text = "";
        txtDescription.Text = "";
        txtChangeName.Text = "";
        txtChangeSD.Text = "";
        txtChangeED.Text = "";
        txtChangeDesc.Text = "";

        Session["cboUpdate"] = null;
        Session["cboView"] = null;
        Session["cboJoin"] = null;

    }

    protected void btnCreate_Click(object sender, EventArgs e)
    {
        SqlConnection sc = new SqlConnection();
        sc.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
        sc.Open();
        Team team = new Team(txtTeamName.Text, txtStartDate.Text, txtDescription.Text, Session["loggedIn"].ToString(), Convert.ToInt32(Session["ID"]));
        string sqlString = "INSERT INTO [dbo].[Team]([CreatorID],[StartDate],[TeamDescription],[TeamName],[LastUpdated],[LastUpdatedBy],[EndDate],[Status]) VALUES " +
            "(@CreatorID,@StartDate,@TeamDescription,@TeamName,@LastUpdated,@LastUpdatedBy,@EndDate,@Status)";
        SqlCommand insert = new SqlCommand(sqlString);
        insert.Connection = sc;
        insert.Parameters.AddWithValue("@CreatorID", team.getPersonID());
        insert.Parameters.AddWithValue("@StartDate", team.getStartDate());
        insert.Parameters.AddWithValue("@TeamDescription", team.getTeamDescription());
        insert.Parameters.AddWithValue("@LastUpdated", team.getLUD());
        insert.Parameters.AddWithValue("@LastUpdatedBy", Session["loggedIn"].ToString());
        insert.Parameters.AddWithValue("@TeamName", team.getTeamName());
        insert.Parameters.AddWithValue("@Status", team.getStatus());
        if (txtED.Text.Trim() != "")
        {
            insert.Parameters.AddWithValue("@EndDate", txtED.Text);
        }
        else
        {
            insert.Parameters.AddWithValue("@EndDate", DBNull.Value);
        }
        insert.ExecuteNonQuery();
        Response.Write("<script>alert('Your Team has been created')</script>");
        sc.Close();
        reset();
        fillcboUpdateTeam();
    }
    private void fillcboUpdateTeam()
    {
        SqlConnection sc = new SqlConnection();
        sc.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
        sc.Open();
        DataTable managementTable = new DataTable();
        string getName = "SELECT [TeamID], [TeamName],[TeamDescription] FROM[dbo].[Team] Where [CreatorID] =" + Session["ID"]+ "and Status = 1";
        SqlDataAdapter management = new SqlDataAdapter(getName, sc);
        management.Fill(managementTable);
        cboUpdateTeam.DataSource = managementTable;
        cboUpdateTeam.DataTextField = "TeamName";
        cboUpdateTeam.DataValueField = "TeamID";
        cboUpdateTeam.DataBind();

        DataTable joinTeam = new DataTable();
        String getjoin = "SELECT Team.TeamID, Team.TeamName FROM Team where Team.TeamID not in (select TeamID from EmployeeTeam where PersonID = "+ Session["ID"]+ ") and team.Status = 1" ;
        SqlDataAdapter jointable = new SqlDataAdapter(getjoin, sc);
        jointable.Fill(joinTeam);
        cboJoin.DataSource = joinTeam;
        cboJoin.DataTextField = "TeamName";
        cboJoin.DataValueField = "TeamID";
        cboJoin.DataBind();

        DataTable viewTeam = new DataTable();
        String getview = "SELECT Team.TeamID, Team.TeamName FROM Team where Team.TeamID in (select teamid from EmployeeTeam where PersonID = "+ Session["ID"]+ ") and team.Status = 1" ;
        SqlDataAdapter teamviewtable = new SqlDataAdapter(getview, sc);
        teamviewtable.Fill(viewTeam);
        cboView.DataSource = viewTeam;
        cboView.DataTextField = "TeamName";
        cboView.DataValueField = "TeamID";
        cboView.DataBind();

        sc.Close();
    }
    protected void cboUpdateTeam_SelectedIndexChanged(object sender, EventArgs e)
    {   
            SqlConnection sc = new SqlConnection();
            sc.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
            sc.Open();
            SqlCommand insert = new SqlCommand();
            int id = Convert.ToInt32(Session["id"]);
            insert.Connection = sc;
            Session["cboUpdate"] = cboUpdateTeam.SelectedValue;
            insert.CommandText = "select [TeamName],[TeamDescription],[StartDate],[EndDate] from [Team] where [TeamID] = @TeamID and Status=1 and [CreatorID] =@id";
            insert.Parameters.AddWithValue("@TeamID", Session["cboUpdate"]);
            insert.Parameters.AddWithValue("@id", id);
            SqlDataReader reader = insert.ExecuteReader();

            if (reader.HasRows)
            {
                reader.Read();
                txtChangeName.Text = reader["TeamName"].ToString();
                txtChangeSD.Text = Convert.ToDateTime(reader["StartDate"]).ToShortDateString();

            if (!Convert.IsDBNull(reader["EndDate"]))
            {
                txtChangeED.Text = Convert.ToDateTime(reader["EndDate"]).ToShortDateString();
            }
            else
            {
                txtChangeED.Text = "";
            }
            
            txtChangeDesc.Text = reader["TeamDescription"].ToString();
            }
            sc.Close();
            popTSetting.Show();
    }
    protected void btnTerminate_Click(object sender, EventArgs e)
    {
        if (Session["cboUpdate"] == null)
        {
            Response.Write("<script>alert('Please select one team to terminate')</script>");
            popTSetting.Show();
        }
        else
        {   
            SqlConnection sc = new SqlConnection();
            sc.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
            sc.Open();
            SqlCommand insert = new SqlCommand();
            insert.Connection = sc;
            insert.CommandText = "update team set status=0 where teamID = @TeamID ";
            insert.Parameters.AddWithValue("@TeamID", Session["cboUpdate"]);
            insert.ExecuteNonQuery();
            sc.Close();
            fillcboUpdateTeam();
            popTSetting.Show();
            txtChangeName.Text = "";
            txtChangeSD.Text = "";
            txtChangeED.Text = "";
            txtChangeDesc.Text = "";
            reset();
            Response.Write("<script>alert('Your team has been terminate and will not show up in the list')</script>");
        }
        

    }

    protected void btnChange_Click(object sender, EventArgs e)
    {
        if (Session["cboUpdate"] == null)
        {
            Response.Write("<script>alert('Please select one team to change information')</script>");
            popTSetting.Show();
        }
        else
        {
            Team team = new Team(txtChangeName.Text, txtChangeSD.Text, txtChangeDesc.Text, Session["loggedIn"].ToString(), Convert.ToInt32(Session["ID"]));
            SqlConnection sc = new SqlConnection();
            sc.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
            sc.Open();
            SqlCommand insert = new SqlCommand();
            insert.Connection = sc;
            insert.CommandText = "UPDATE [dbo].[Team] SET [TeamName] = @TeamName,[TeamDescription] = @TeamDescription,[StartDate] = @StartDate,[EndDate] = @EndDate," +
                "[LastUpdated] = @LastUpdated,[LastUpdatedBy] = @LastUpdatedBy WHERE teamid=@id";
            insert.Parameters.AddWithValue("@TeamName", team.getTeamName());
            insert.Parameters.AddWithValue("@TeamDescription", team.getTeamDescription());
            insert.Parameters.AddWithValue("@StartDate", team.getStartDate());
            insert.Parameters.AddWithValue("@LastUpdated", team.getLUD());
            insert.Parameters.AddWithValue("@LastUpdatedBy", team.getLUDB());
            insert.Parameters.AddWithValue("@id", Session["cboUpdate"]);
            if (txtChangeED.Text.Trim() != "")
            {
                insert.Parameters.AddWithValue("@EndDate", txtChangeED.Text);
            }
            else
            {
                insert.Parameters.AddWithValue("@EndDate", DBNull.Value);
            }
            insert.ExecuteNonQuery();
            sc.Close();
            Response.Write("<script>alert('Information has been updated')</script>");
            fillcboUpdateTeam();
            reset();
        }
    }

    protected void cboJoin_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["cboJoin"] = cboJoin.SelectedValue;
        popJoin.Show();
    }

    protected void cboView_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["cboView"] = cboView.SelectedValue;

        SqlConnection sc = new SqlConnection();
        sc.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
        sc.Open();
        SqlCommand insert = new SqlCommand();
        insert.Connection = sc;
        DataTable dt = new DataTable();
        insert.CommandText = "SELECT Person.FirstName, Person.LastName, EmployeeTeam.JoinDate FROM EmployeeTeam INNER JOIN Person ON EmployeeTeam.PersonID = Person.PersonID INNER JOIN " +
            "Team ON EmployeeTeam.teamid = Team.TeamID where Team.TeamID = @id";
        insert.Parameters.AddWithValue("@id", Session["cboView"]);
        SqlDataReader sdr = insert.ExecuteReader();
        dlTeamMembers.DataSource = sdr;
        dlTeamMembers.DataBind();
        sdr.Close();
        insert.CommandText = "SELECT Person.FirstName, Person.LastName FROM Person CROSS JOIN Team where Team.CreatorID = Person.PersonID and Team.TeamID = @id";
        //insert.Parameters.AddWithValue("@id", Session["cboView"]);
        SqlDataReader reader = insert.ExecuteReader();
        if (reader.HasRows)
        {
            reader.Read();
            lblMName.Text = reader[0].ToString()+" "+ reader[1].ToString();
        }
        sc.Close();
        popView.Show();
    }


    protected void btnJoin_Click(object sender, EventArgs e)
    {
        if (Session["cboJoin"] == null)
        {
            Response.Write("<script>alert('Please select one team to join')</script>");
            popJoin.Show();
        }
        else
        {
            SqlConnection sc = new SqlConnection();
            sc.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
            sc.Open();
            TeamMember team = new TeamMember(Convert.ToInt32(Session["cboJoin"]), Session["loggedIn"].ToString(), Convert.ToInt32(Session["ID"]));
            string sqlString = "INSERT INTO [dbo].[EmployeeTeam]([PersonID],[teamid],[JoinDate],[LeaveDate],[LastUpdated],[LastUpdatedBy])VALUES" +
                "(@PersonID,@teamid,@JoinDate,@LeaveDate,@LastUpdated,@LastUpdatedBy)";
            SqlCommand insert = new SqlCommand(sqlString);
            insert.Connection = sc;
            insert.Parameters.AddWithValue("@PersonID", team.getPersonID());
            insert.Parameters.AddWithValue("@teamid", team.getTeamID());
            insert.Parameters.AddWithValue("@JoinDate", team.getStartDate());
            insert.Parameters.AddWithValue("@LastUpdated", team.getLUD());
            insert.Parameters.AddWithValue("@LastUpdatedBy", team.getLUDB());
            insert.Parameters.AddWithValue("@LeaveDate", DBNull.Value);
            insert.ExecuteNonQuery();
            sc.Close();
            fillcboUpdateTeam();
            reset();
            Response.Write("<script>alert('Congratulation! You Joined a team!')</script>");
        }
    }
}