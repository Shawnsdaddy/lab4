using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//On my honor, I(Xubo Wu) pledge to the JMU Honor

public class Person
{
    private String FirstName;
    private String LastName;
    private String MI;
    private String Email;
    private String ManagerID;
    private String LastUpdatedBy = "Xubo Wu";
    private DateTime LastUpdated = DateTime.Today;
    private String Position = "EMPLOYEE";
    private int PointsBalance=0;
    private int PendingPoints = 0;
    private int BusinessEntityID = 1;

    public Person(String firstName, String lastName, String Email)
    {
        setFirstName(firstName);
        setLastName(lastName);
        setEmail(Email);
    }

    public void setManagerID(String ManagerID)
    {
        this.ManagerID = ManagerID;
    }

    public void setFirstName(String FirstName)
    {
        this.FirstName = FirstName;
    }

    public void setLastName(String LastName)
    {
        this.LastName = LastName;
    }

    public void setMI(String MI)
    {
        this.MI = MI;
    }

    public void setEmail(String Email)
    {
        this.Email = Email;
    }
    public void setLastUpdatedBy(String LastUpdatedBy)
    {
        this.LastUpdatedBy = LastUpdatedBy;
    }

    public void setLastUpdated(DateTime LastUpdated)
    {
        this.LastUpdated = LastUpdated;
    }

    public String getManagerID()
    {
        return this.ManagerID;
    }

    public String getFirstName()
    {
        return this.FirstName;
    }

    public string getLastName()
    {
        return this.LastName;
    }

    public string getMI()
    {
        return this.MI;
    }

    public string getEmail()
    {
        return this.Email;
    }

    public string getPosition()
    {
        return this.Position;
    }

    public string getPointsBalance()
    {
        return this.PointsBalance.ToString();
    }
    public string getPendingPoints()
    {
        return this.PendingPoints.ToString();
    }

    public string getBusinessEntityID()
    {
        return this.BusinessEntityID.ToString();
    }

    public String getLastUpdatedBy()
    {
        return this.LastUpdatedBy;
    }

    public String getLastUpdated()
    {
        return this.LastUpdated.ToShortDateString();
    }


}