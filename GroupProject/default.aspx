<%@ Page Language="C#" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="loginScreen" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Style/login.css" rel="stylesheet" type="text/css" />


    <style type="text/css">
        .auto-style1 {
            text-align: center;
        }

        .auto-style2 {
            width: 5px;
        }

        .auto-style3 {
            color: #fff;
            background-color: #28a745;
            margin-left: auto;
            margin-right: auto;
            display: block;
            width: 25%;
            height: 15%;
            margin-bottom: 5px;
            font-weight: normal;
            text-align: center;
            white-space: nowrap;
            vertical-align: middle;
            -webkit-user-select: none;
            -moz-user-select: none;
            -ms-user-select: none;
            user-select: none;
            border: 1px solid transparent;
            font-size: 1rem;
            line-height: 1.25;
            border-radius: 0.25rem;
            transition: all 0.15s ease-in-out;
            position: Relative;
            bottom: -32px;
            left: -97px;
        }

        .auto-style4 {
            color: #fff;
            background-color: #28a745;
            margin-left: auto;
            margin-right: auto;
            display: block;
            width: 25%;
            height: 15%;
            margin-bottom: 5px;
            font-weight: normal;
            text-align: center;
            white-space: nowrap;
            vertical-align: middle;
            -webkit-user-select: none;
            -moz-user-select: none;
            -ms-user-select: none;
            user-select: none;
            border: 1px solid transparent;
            font-size: 1rem;
            line-height: 1.25;
            border-radius: 0.25rem;
            transition: all 0.15s ease-in-out;
            position: Relative;
            bottom: -4px;
            left: 115px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" margin-left="auto" margin-right="auto">
        <div class="auto-style1">
            <asp:Login ID="Login1" runat="server" OnAuthenticate="Login1_Authenticate" DestinationPageUrl="CEOPostWall.aspx">
                <LayoutTemplate>
                    <table cellpadding="1" cellspacing="0" style="border-collapse: collapse;">
                        <tr>
                            <td>
                                <table cellpadding="0">
                                    <tr>
                                        <td align="center" class="td" colspan="2">
                                            <img src="https://upload.wikimedia.org/wikipedia/commons/b/b1/Google_Opinion_Rewards_app_logo.png" class="rewardIcon" /></td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="auto-style2">&nbsp;</td>
                                        <td>
                                            <asp:TextBox ID="UserName" runat="server" CssClass="textbox" placeholder="User Name"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" ErrorMessage="User Name is required." ToolTip="User Name is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="auto-style2">&nbsp;</td>
                                        <td>
                                            <asp:TextBox ID="Password" runat="server" TextMode="Password" CssClass="textbox" placeholder="Password"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:CheckBox ID="RememberMe" runat="server" CssClass="td" Text="Remember me next time." />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="2" style="color: Red;">
                                            <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" colspan="2">
                                            <asp:Button ID="LoginButton" runat="server" CommandName="Login" CssClass="btn-login" Text="Log In" ValidationGroup="Login1" />
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

                            </td>
                        </tr>
                    </table>
                </LayoutTemplate>
            </asp:Login>
        </div>
        <div>
            <asp:Button ID="btnForgotUsername" runat="server" CssClass="btn-forgot" Text="Forgot Username" Width="210px" />
            <asp:Button ID="btnForgotPassword" runat="server" CssClass="btn-forgot" Text="Forgot Password"  Width="210px" />
        </div>


        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="True"></asp:ScriptManager>
        <ajaxToolkit:ModalPopupExtender ID="popResendUserName" runat="server" TargetControlID="btnForgotUsername" PopupControlID="divResendUserName" CancelControlID="cancelEmail" BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>
        <ajaxToolkit:ModalPopupExtender ID="popResendPassword" runat="server" TargetControlID="btnForgotPassword" PopupControlID="divResendPass" CancelControlID="cancelPass" BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>
        <div id="divResendUserName" class="popup" style="width: 280px" >
            <asp:Table ID="tblResetEmail" runat="server" HorizontalAlign="Left" CssClass="table">

                <asp:TableRow>
                    <asp:TableCell>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="textbox" Width="230px" placeholder="Enter Email to receive UserName"></asp:TextBox>
                    </asp:TableCell><asp:TableCell>
                        <asp:RequiredFieldValidator ValidationGroup="UserName" ID="RequiredFieldValidator2" runat="server" class="auto-style8" ControlToValidate="txtEmail" ErrorMessage="*"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ValidationGroup="UserName" ID="RegularExpressionValidator2" runat="server"
                            ControlToValidate="txtEmail" ErrorMessage="Please enter corect email"
                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
            <asp:Button  id="resendEmail" ValidationGroup="UserName" runat="server" Text="Send Your UserName to Your Email" CssClass="button" OnClick="resendEmail_Click" />
            <asp:Button ID="cancelEmail" runat="server" CssClass="btn-close" Style="background-image: url('http://icons.iconarchive.com/icons/iconsmind/outline/24/Close-icon.png'); background-repeat: no-repeat" />
        </div>
        <div id="divResendPass" class="popup" style="width: 300px">
            <asp:Table ID="tblResetUserName" runat="server" HorizontalAlign="Left" CssClass="table">
                <asp:TableRow>
                    <asp:TableCell>
                        <asp:TextBox ID="txtResetUserName" runat="server" CssClass="textbox" Width="250" placeholder="Enter UserName to receive default Password"></asp:TextBox>
                    </asp:TableCell><asp:TableCell>
                        <asp:RequiredFieldValidator ValidationGroup="Pass" ID="RequiredFieldValidator1" runat="server" class="auto-style8" ControlToValidate="txtResetUserName" ErrorMessage="*"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ValidationGroup="Pass" ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtResetUserName" ValidationExpression="^[a-zA-Z\s']{0,20}$" Text="User Name can not be more than 20 alphabetic characters in length" />
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
            <asp:Button ID="btnResetPass" ValidationGroup="Pass" runat="server" Text="Send Your default Password to Email" CssClass="button" OnClick="btnResetPass_Click" />
            <asp:Button ID="cancelPass" runat="server" CssClass="btn-close" Style="background-image: url('http://icons.iconarchive.com/icons/iconsmind/outline/24/Close-icon.png'); background-repeat: no-repeat" />
        </div>
        
    </form>

</body>
</html>
