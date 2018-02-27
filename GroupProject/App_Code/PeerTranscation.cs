using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PeerTranscation
/// </summary>
public class PeerTranscation
{
        double PointsAmount;
        string Date;
        string EventDescription;
        String LastUpdated;
        String LastUpdatedBy;
        int ReceiverID;
        int RewarderID;
        int CategoryID;
        int ValueID;
    public PeerTranscation(double PointsAmount,string Date,string EventDescription,String LastUpdated,String LastUpdatedBy, int ReceiverID, int RewarderID,int CategoryID,int ValueID)
    {
        setPoints(PointsAmount);
        setDate(Date);
        setDescription(EventDescription);
        setLUD(LastUpdated);
        setLUDB(LastUpdatedBy);
        setReceiverID(ReceiverID);
        setRewarderID(RewarderID);
        setValueID(ValueID);
        setCategoryID(CategoryID);


    }

    public void setPoints(double PointsAmount)
    {
        this.PointsAmount = PointsAmount;
    }

    public void setDate(string Date)
    {
        this.Date = Date;
    } 

    public void setDescription(string EventDescription)
    {
        this.EventDescription = EventDescription;
    }

    public void setLUD (string LastUpdated)
    {
        this.LastUpdated = LastUpdated;
    }

    public void setLUDB (string LastUpdatedBy)
    {
        this.LastUpdatedBy = LastUpdatedBy;
    }

    public void setReceiverID(int ReceiverID)
    {
        this.ReceiverID = ReceiverID;
    }

    public void setRewarderID(int RewarderID)
    {
        this.RewarderID = RewarderID;
    }
    public void setCategoryID(int CategoryID)
    {
        this.CategoryID = CategoryID;
    }
    public void setValueID(int ValueID)
    {
        this.ValueID = ValueID;
    }

    public double getPoints()
    {
        return this.PointsAmount;
    }

    public string getDate()
    {
        return this.Date;
    }

    public string getDescription()
    {
        return this.EventDescription;
    }

    public string getLUD()
    {
        return this.LastUpdated;
    }

    public string getLUDB()
    {
        return this.LastUpdatedBy;
    }

    public int getReceiverID()
    {
        return this.ReceiverID;
    }
    
    public int getRewarderID()
    {
        return this.RewarderID;
    }

    public int getCategoryID()
    {
        return this.CategoryID;
    }

    public int getValueID()
    {
        return this.ValueID;
    }
}