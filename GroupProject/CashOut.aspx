<%@ Page Title="" Language="C#" MasterPageFile="~/EmployeeMasterPage.master" AutoEventWireup="true" CodeFile="CashOut.aspx.cs" Inherits="CashOut" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">

        <asp:Label runat="server" ID="lblPoints"></asp:Label>
        <div>    <asp:RadioButtonList ID="rblcashout" runat="server" RepeatDirection="Horizontal" Width="150px" OnSelectedIndexChanged="rblcashout_SelectedIndexChanged">
                                <asp:ListItem Value="10">Cash Out $10</asp:ListItem>

                                <asp:ListItem Value="15">Cash Out $15</asp:ListItem>

                                <asp:ListItem Value="25">Cash Out $25</asp:ListItem>

                            </asp:RadioButtonList>
    <asp:Button runat="server" ID="cashout" Text="Get the Reward!" onclick="cashout_Click" />
   </div>

</asp:Content>





<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
</asp:Content>

