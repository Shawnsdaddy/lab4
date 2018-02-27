<%@ Page Language="C#" AutoEventWireup="true" CodeFile="firsttime.aspx.cs" Inherits="firsttime" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Style/CEO.css" rel="stylesheet" type="text/css" />
    <link href="Style/login.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server" style="margin: auto">
        <div style="margin: auto">
            <table cellpadding="1" cellspacing="0" style="border-collapse: collapse;">
                <tr>
                    <td>
                        <table cellpadding="0">
                            <tr>
                                <td align="center" class="td" colspan="2">
                                    <img src="https://upload.wikimedia.org/wikipedia/commons/b/b1/Google_Opinion_Rewards_app_logo.png" class="rewardIcon" /></td>
                            </tr>
                            <tr>
                                <td align="center" class="auto-style2">&nbsp;</td>
                                <td>
                                    <div class="btn-aligncenter">
                                        <asp:TextBox ID="txtUsername" runat="server" CssClass="textbox" placeholder="Customize Name:"></asp:TextBox>
                                        <asp:RequiredFieldValidator ValidationGroup="Change" ID="RequiredFieldValidator3" runat="server" ForeColor="Red" ControlToValidate="txtUsername" ErrorMessage=" *"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="btn-aligncenter" style="font-size: smaller">
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="Change" runat="server" ControlToValidate="txtUsername" ValidationExpression="^[a-zA-Z0-9\-_]{0,20}$" Text="User Name can not be more than 20 characters in length" />
                                    </div>

                                </td>
                            </tr>
                            <tr>
                                <td align="center" class="auto-style2">&nbsp;</td>
                                <td>
                                    <div class="btn-aligncenter" style="text-align:center">
                                        <asp:TextBox ID="txtNew1" TextMode="Password" runat="server" CssClass="textbox" placeholder="New Password:"></asp:TextBox>

                                        <asp:RequiredFieldValidator ValidationGroup="Change" ID="RequiredFieldValidator7" runat="server" ForeColor="Red" ControlToValidate="txtNew1" ErrorMessage=" *"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="btn-aligncenter" style="font-size: smaller">
                                        <asp:RegularExpressionValidator ValidationGroup="Change" ID="RegularNew1" runat="server" ControlToValidate="txtNew1" ValidationExpression="((?=.*\d)(?=.*[a-z])(?=.*[\W]).{8,12})" Text="Password must be 8-12 Characters long" />
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" class="auto-style2">&nbsp;</td>
                                <td>
                                    <div class="btn-aligncenter">
                                        <asp:TextBox ID="txtNew2" CssClass="textbox" TextMode="Password" runat="server" placeholder="Verify Password:"></asp:TextBox>
                                    <asp:RequiredFieldValidator ValidationGroup="Change" ID="RequiredFieldValidator8" runat="server" class="auto-style8" ForeColor="Red" ControlToValidate="txtNew2" ErrorMessage=" *"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="btn-aligncenter" style="font-size: smaller">
                                        <asp:CompareValidator ValidationGroup="Change" ID="RegularNew2" runat="server" ControlToValidate="txtNew2" ControlToCompare="txtNew1" Type="String" Operator="Equal" Text="Password must be same as new password" />
                                    </div>
                                </td>
                            </tr>
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2">
                        <asp:Button runat="server" ValidationGroup="Change" CssClass="button" OnClick="btnSave_Click" ID="btnSave" Text="Save" OnClientClick="if (!confirm('You have only one chance to modify your username! Please double check')) return false" />
                    </td>
                </tr>
                <tr>
                    <td align="right" colspan="2">&nbsp;</td>
                </tr>
                <tr>
                    <td align="right" colspan="2"></td>
                    <td align="right" colspan="2"></td>
                </tr>
            </table>

            <asp:Table ID="firstimel" runat="server" Height="164px" CssClass="table">
                <%--              <asp:TableRow>
                    <asp:TableCell>
                        <asp:Label ID="lblUsername" runat="server" Text="Customize Username:"></asp:Label>
                    </asp:TableCell><asp:TableCell>
                        <asp:TextBox ID="txtUsername" runat="server" CssClass="textbox"></asp:TextBox>
                        <asp:RequiredFieldValidator ValidationGroup="Change" ID="RequiredFieldValidator3" runat="server" class="auto-style1" ForeColor="Red" ControlToValidate="txtUsername" ErrorMessage="*" Display="Dynamic"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="Change" runat="server" ControlToValidate="txtUsername" ValidationExpression="^[a-zA-Z0-9\-_]{0,20}$" Text="User Name can not be more than 20 characters in length" />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>
                        <asp:Label ID="lblNewPass1" runat="server" Text="New PassWord:"></asp:Label>
                    </asp:TableCell><asp:TableCell>
                        <asp:TextBox ID="txtNew1" TextMode="Password" runat="server" CssClass="textbox"></asp:TextBox>
                    </asp:TableCell><asp:TableCell>
                        <asp:RequiredFieldValidator ValidationGroup="Change" ID="RequiredFieldValidator7" runat="server" class="auto-style8" ForeColor="Red" ControlToValidate="txtNew1" ErrorMessage="*"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ValidationGroup="Change" ID="RegularNew1" runat="server" ControlToValidate="txtNew1" ValidationExpression="((?=.*\d)(?=.*[a-z])(?=.*[\W]).{8,12})" Text="Password must be 8-12 Characters long, contain one lowercase characters, one digit from 0-9,and at least one special character" />
                    </asp:TableCell>
                </asp:TableRow>--%>
                <%--                <asp:TableRow>
                    <asp:TableCell>
                        <asp:Label ID="lblNewPass2" runat="server" Text="Re-enter New Password:"></asp:Label>
                    </asp:TableCell><asp:TableCell>
                        <asp:TextBox ID="txtNew2" CssClass="textbox" TextMode="Password" runat="server"></asp:TextBox>
                    </asp:TableCell><asp:TableCell>
                        <asp:RequiredFieldValidator ValidationGroup="Change" ID="RequiredFieldValidator8" runat="server" class="auto-style8" ForeColor="Red" ControlToValidate="txtNew2" ErrorMessage="*"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ValidationGroup="Change" ID="RegularNew2" runat="server" ControlToValidate="txtNew2" ControlToCompare="txtNew1" Type="String" Operator="Equal" Text="Password must be same as new password" />
                    </asp:TableCell>
                </asp:TableRow>--%>
            </asp:Table>
            <%--            <asp:Button runat="server" ValidationGroup="Change" CssClass="button" OnClick="btnSave_Click" ID="btnSave" Text="Save" OnClientClick="if (!confirm('You have only one chance to modify your username! Please double check')) return false" />--%>
        </div>
    </form>
</body>
</html>
