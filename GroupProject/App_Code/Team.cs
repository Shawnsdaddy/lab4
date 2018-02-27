using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Team
/// </summary>
public class Team
{
    String TeamName;
    String TeamDescription;
    string StartDate;
    String LastUpdated=DateTime.Today.ToShortDateString();
    String LastUpdatedBy;
    String TerminadationDate;
    int PersonID;
    int Status = 1;
    public Team(String TeamName, string StartDate, String TeamDescription,String LastUpdatedBy, int CreatorID)
    {
        setName(TeamName);
        setStartDate(StartDate);
        setDescription(TeamDescription);
        setLUDB(LastUpdatedBy);
        setPersonID(CreatorID);
    }

    public void setStatus(int Status)
    {
        this.Status = Status;
    }

    public int getStatus()
    {
        return this.Status;
    }
    public void setName(String TeamName)
    {
        this.TeamName = TeamName;
    }

    public void setStartDate(string StartDate)
    {
        this.StartDate = StartDate;
    }

    public void setDescription(String TeamDescription)
    {
        this.TeamDescription = TeamDescription;
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
    public String getTeamName()
    {
        return this.TeamName;
    }

    public string getStartDate()
    {
        return this.StartDate;
    }

    public string getTeamDescription()
    {
        return this.TeamDescription;
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