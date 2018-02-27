using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for EmployeeTeam
/// </summary>
public class TeamMember
{

    string StartDate= DateTime.Today.ToShortDateString();
    String LastUpdated = DateTime.Today.ToShortDateString();
    String LastUpdatedBy;
    int PersonID;
    int TeamID;
    public TeamMember(int TeamID, String LastUpdatedBy, int PersonID)
    {
        setTeamID(TeamID);
        setLUDB(LastUpdatedBy);
        setPersonID(PersonID);
    }
    public void setTeamID(int TeamID)
    {
        this.TeamID = TeamID;
    }
    public int getTeamID()
    {
        return this.TeamID;
    }
    public void setStartDate(string StartDate)
    {
        this.StartDate = StartDate;
    }


    public void setLUD(string LastUpdated)
    {
        this.LastUpdated = LastUpdated;
    }

    public void setLUDB(string LastUpdatedBy)
    {
        this.LastUpdatedBy = LastUpdatedBy;
    }

    public void setPersonID(int PersonID)
    {
        this.PersonID = PersonID;
    }

    public string getStartDate()
    {
        return this.StartDate;
    }
    public string getLUD()
    {
        return this.LastUpdated;
    }

    public string getLUDB()
    {
        return this.LastUpdatedBy;
    }

    public int getPersonID()
    {
        return this.PersonID;
    }
}