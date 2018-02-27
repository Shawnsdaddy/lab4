<%@ Page Title="" Language="C#" MasterPageFile="~/CEOMasterPage.master" AutoEventWireup="true" CodeFile="CreateEmployee.aspx.cs" Inherits="CreateEmployee" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="btn-aligncenter">
        <h1>ADD A NEW EMPLOYEE</h1>

        <asp:Table ID="CreateEmployeefrom" runat="server" HorizontalAlign="Center">
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="lblFirstName" runat="server" class="auto-style1" Text="First Name:"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="txtFirstName" runat="server" CssClass="textbox"></asp:TextBox>
                </asp:TableCell><asp:TableCell>
                    <asp:RequiredFieldValidator ID="FirstNameRequire" runat="server" class="auto-style1" ForeColor="Red" ControlToValidate="txtFirstName" ErrorMessage="*"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="FirstNameMessage" runat="server" ControlToValidate="txtFirstName" ValidationExpression="^[a-zA-Z\s']{0,20}$" Text="First name can not be more than 20 alphabetic characters in length" CssClass="auto-style7" />
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="lblMI" runat="server" Text="Middle Initial:"></asp:Label>
                </asp:TableCell><asp:TableCell>
                    <asp:TextBox ID="txtMI" runat="server" CssClass="textbox"></asp:TextBox>
                </asp:TableCell><asp:TableCell>
                    <asp:RegularExpressionValidator ID="MIMessage" runat="server"  ControlToValidate="txtMI" ValidationExpression="^[a-zA-Z\s]{0,1}$" Text="Middle name can not be more than 1 alphabetic character in length" CssClass="auto-style7" />
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="lblLastName" runat="server" class="auto-style1" Text="Last Name:"></asp:Label>
                </asp:TableCell><asp:TableCell>
                    <asp:TextBox ID="txtLastName" runat="server" CssClass="textbox"></asp:TextBox>
                </asp:TableCell><asp:TableCell>
                    <asp:RequiredFieldValidator ID="LastNameRequire" runat="server" class="auto-style1" ForeColor="Red" ControlToValidate="txtLastName" ErrorMessage="*"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="LastNameMessage" runat="server" ControlToValidate="txtLastName" ValidationExpression="^[a-zA-Z\s']{0,30}$" Text="Last name can not be more than 30 alphabetic characters in length" CssClass="auto-style7" />
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="lblEmail" runat="server" class="auto-style1" Text="Email Address:"></asp:Label>
                </asp:TableCell><asp:TableCell>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="textbox"></asp:TextBox>
                </asp:TableCell><asp:TableCell>
                    <asp:RequiredFieldValidator ID="Required" runat="server" class="auto-style1" ForeColor="Red" ControlToValidate="txtEmail" ErrorMessage="*"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                        ControlToValidate="txtEmail" ErrorMessage="Please enter corect email. For example john@example.com"
                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="lblManagerID" runat="server" Text="Manager ID:"></asp:Label>
                </asp:TableCell><asp:TableCell>
                    <asp:TextBox ID="txtManagerID" runat="server" CssClass="textbox"></asp:TextBox>
                </asp:TableCell><asp:TableCell>
                    <asp:CompareValidator ID="ManagerIDMessage" runat="server" CssClass="auto-style7" ControlToValidate="txtManagerID" class="auto-style7" Type="Integer" Operator="DataTypeCheck" ErrorMessage="ManagerID must be an integer" />
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <div class="table">
            <div class="btn-aligncenter">
                <asp:Button ID="btnCommit" runat="server" Text="Add Employee " OnClick="BtnCommit_Click" CssClass="button" Width="25%" OnClientClick="if (!confirm('Please double check your entered data')) return false"></asp:Button>

            </div>
        </div>
    </div>
</asp:Content>

