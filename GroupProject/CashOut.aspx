<%@ Page Title="" Language="C#" MasterPageFile="~/EmployeeMasterPage.master" AutoEventWireup="true" CodeFile="CashOut.aspx.cs" Inherits="CashOut" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <div class="btn-aligncenter">
        <asp:Label runat="server" ID="lblPoints" Font-Size="34px" margin-Bottom="15px" ForeColor="Black" Font-Bold="True" CssClass="table"></asp:Label>
        <div class="card">
        <asp:RadioButtonList ID="rblcashout" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="rblcashout_SelectedIndexChanged" Style="text-align:center; width:100%; height:20%; margin-top:15px;" BackColor="#FDDF68" BorderColor="White" BorderStyle="Solid" BorderWidth="2px" Font-Bold="True" Font-Size="24px">
                                <asp:ListItem Value="10">Cash Out $10</asp:ListItem>

                                <asp:ListItem Value="15">Cash Out $15</asp:ListItem>

                                <asp:ListItem Value="25">Cash Out $25</asp:ListItem>

                            </asp:RadioButtonList>
    </div>
    <asp:Button runat="server" ID="cashout" Text="Get the Reward!" onclick="cashout_Click" width="50%" CssClass="button"/>
            

</div>

</asp:Content>





<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
</asp:Content>

