using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
public class MoneyTransaction
{
    double TotalAmount;
    double TransactionAmount;
    string Date;
    String LastUpdated;
    String LastUpdatedBy;
    int PersonID;
    public MoneyTransaction(double TotalAmount, string Date, double TransactionAmount, String LastUpdated, String LastUpdatedBy,int PersonID)
    {
        setPoints(TotalAmount);
        setDate(Date);
        setDescription(TransactionAmount);
        setLUD(LastUpdated);
        setLUDB(LastUpdatedBy);
        setPersonID(PersonID);
    }

    public void setPoints(double TotalAmount)
    {
        this.TotalAmount = TotalAmount;
    }

    public void setDate(string Date)
    {
        this.Date = Date;
    }

    public void setDescription(double TransactionAmount)
    {
        this.TransactionAmount = 0-TransactionAmount;
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
    public double getPoints()
    {
        return this.TotalAmount;
    }

    public string getDate()
    {
        return this.Date;
    }

    public string getTransactionAmount()
    {

        return this.TransactionAmount.ToString();
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

