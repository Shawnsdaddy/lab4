<%@ Page Title="" Language="C#" MasterPageFile="~/employeeMasterPage.master" AutoEventWireup="true" CodeFile="employeeProfile.aspx.cs" Inherits="employeeProfile" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <div class="btn-aligncenter">
        <asp:Button ID="btnProfile" runat="server" Text="Change Profile" CssClass="btn-profile" />
        <asp:Button ID="btnPass" runat="server" Text="Change Password" CssClass="btn-profile" />
        <asp:Button ID="btnPic" runat="server" Text="Change Picture" CssClass="btn-profile"/>

        <br />

    </div>
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="True"></asp:ScriptManager>
    <ajaxToolkit:ModalPopupExtender ID="popProfile" runat="server" TargetControlID="btnProfile" PopupControlID="divInfomation" CancelControlID="btnCancel" BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>
    <ajaxToolkit:ModalPopupExtender ID="popPass" runat="server" TargetControlID="btnPass" PopupControlID="divPassword" CancelControlID="btnClose" BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>
    <ajaxToolkit:ModalPopupExtender ID="popPic" runat="server" TargetControlID="btnPic" PopupControlID="divPicture" CancelControlID="btnClosed" BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>


    <div id="divInfomation" class="popup" style="width: 605px; height: 500px;">
        <h1>UPDATE PROFILE INFORMATION</h1>

        <asp:Table ID="EditProfile" runat="server" HorizontalAlign="Left" CssClass="table">
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="lblFirstName" runat="server" Text="First Name:"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="txtFirstName" runat="server" CssClass="textbox"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="NameRequire" ValidationGroup="profile" runat="server" class="auto-style1" ForeColor="Red" ControlToValidate="txtFirstName" ErrorMessage="*"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ValidationGroup="profile" ID="FirstNameMessage" runat="server" ControlToValidate="txtFirstName" ValidationExpression="^[a-zA-Z\s']{1,20}$" Text="can not be more than 20 alphabetic characters" />
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="lblMI" runat="server" Text="Middle Initial:"></asp:Label>
                </asp:TableCell><asp:TableCell>
                    <asp:TextBox ID="txtMI" runat="server" CssClass="textbox"></asp:TextBox>
                    <asp:RegularExpressionValidator ValidationGroup="profile" ID="MIMessage" runat="server" ControlToValidate="txtMI" ValidationExpression="^[a-zA-Z\s]{0,1}$" Text="can not be more than 1 alphabetic character" />
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="lblLastName" runat="server" class="auto-style1" Text="Last Name:"></asp:Label>
                </asp:TableCell><asp:TableCell>
                    <asp:TextBox ID="txtLastName" runat="server" CssClass="textbox" ></asp:TextBox>
                </asp:TableCell><asp:TableCell>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="profile" runat="server" class="auto-style1" ForeColor="Red" ControlToValidate="txtLastName" ErrorMessage="*"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ValidationGroup="profile" ID="LastNameMessage" runat="server" ControlToValidate="txtLastName" ValidationExpression="^[a-zA-Z\s']{1,30}$" Text="Last Name can not be more than 30 alphabetic characters in length" />
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="lblEmail" runat="server" class="auto-style1" Text="Email Address:"></asp:Label>
                </asp:TableCell><asp:TableCell>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="textbox"></asp:TextBox>
                </asp:TableCell><asp:TableCell>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="profile" runat="server" class="auto-style1" ForeColor="Red" ControlToValidate="txtEmail" ErrorMessage="*"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="profile" runat="server" ControlToValidate="txtEmail" ErrorMessage="Please enter corect email. For example john@example.com"
                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                    <%--           DONE  email validation requries                
                <asp:RequiredFieldValidator ID="RequiredEmail" runat="server" class="auto-style1" ForeColor="Red" ControlToValidate="txtEmail" ErrorMessage="*"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="EmailMessage" runat="server" ControlToValidate="txtEmail" ValidationExpression="^[a-zA-Z\s']{0,30}$" Text="invalid input" />--%>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="lblManagerID" runat="server" Text="Manager ID:"></asp:Label>
                </asp:TableCell><asp:TableCell>
                    <asp:TextBox ID="txtManagerID" runat="server" CssClass="textbox"></asp:TextBox>
                </asp:TableCell><asp:TableCell>
                    <asp:CompareValidator ID="ManagerIDMessage" ValidationGroup="profile" runat="server" ControlToValidate="txtManagerID" class="auto-style7" Type="Integer" Operator="DataTypeCheck" ErrorMessage="ManagerID must be an integer" />
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableFooterRow>
                <asp:TableCell>
                    <asp:Button ID="btnChangeProfile" runat="server" class="button" Text="Change Profile" Width="150px" ValidationGroup="profile" OnClick="btnChangeProfile_Click"></asp:Button>
                </asp:TableCell>
            </asp:TableFooterRow>
        </asp:Table>
        <asp:Button ID="btnCancel" runat="server" CssClass="btn-close" Style="background-image: url('http://icons.iconarchive.com/icons/iconsmind/outline/24/Close-icon.png'); background-repeat: no-repeat" />
    </div>
    <div id="divPassword" class="popup">
        <h1>UPDATE PASSWORD</h1>
        <asp:Table ID="TablePassword" runat="server" Height="164px" CssClass="table">
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="lblOldPass" runat="server" Text="Old PassWord:"></asp:Label>
                </asp:TableCell><asp:TableCell>
                    <asp:TextBox ID="txtOldPass" runat="server" CssClass="textbox" Width="300"></asp:TextBox> 
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="password" runat="server" text-align="right" class="auto-style1" ForeColor="Red" ControlToValidate="txtOldPass" ErrorMessage="*"></asp:RequiredFieldValidator>
                                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="lblNewPass1" runat="server" Text="New PassWord:"></asp:Label>
                </asp:TableCell><asp:TableCell>
                    <asp:TextBox ID="txtNew1" runat="server" CssClass="textbox" Width="300"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="password" runat="server" class="auto-style1" ForeColor="Red" ControlToValidate="txtNew1" ErrorMessage="*"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ValidationGroup="password" ID="RegularNew1" runat="server" ControlToValidate="txtNew1" ValidationExpression="((?=.*\d)(?=.*[a-z])(?=.*[\W]).{8,12})" Text="Password must be 8-12 Characters long, contain one lowercase characters, one digit from 0-9,and at least one special character" />
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="lblNewPass2" runat="server" Text="Re-enter New Password:"></asp:Label>
                </asp:TableCell><asp:TableCell>
                    <asp:TextBox ID="txtNew2" runat="server" CssClass="textbox" Width="300"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ValidationGroup="password" runat="server" class="auto-style1" ForeColor="Red" ControlToValidate="txtNew2" ErrorMessage="*"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ValidationGroup="password" ID="RegularNew2" runat="server" ControlToValidate="txtNew2" ControlToCompare="txtNew1" Type="String" Operator="Equal" Text="Password must be same as new password" />
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableFooterRow>
                <asp:TableCell>
                    <div class="btn-aligncenter">
                        <asp:Button ID="btnChangePassWord" ValidationGroup="password" runat="server" Text="Change Password" OnClick="btnChangePassword_Click" CssClass="button"></asp:Button>
                    </div>
                </asp:TableCell>
            </asp:TableFooterRow>
        </asp:Table>
        <asp:Button ID="btnClose" runat="server" Text="" CssClass="btn-close" Style="background-image: url('http://icons.iconarchive.com/icons/iconsmind/outline/24/Close-icon.png'); background-repeat: no-repeat" />
    </div>
    <div id="divPicture" style="width: 500px;height:500px" class="popup">
        <h1>UPDATE PROFILE PICTURE</h1>
        <asp:FileUpload ID="PictureUpload" runat="server" />
        <asp:Button ID="Upload" runat="server" Text="Upload" OnClick="Upload_Click" />
        <asp:Image ID="ProfilePicture" runat="server" Width="480" />
        <asp:Button ID="btnClosed" runat="server"  CssClass="btn-close" Style="background-image: url('http://icons.iconarchive.com/icons/iconsmind/outline/24/Close-icon.png'); background-repeat: no-repeat" />
    </div>

</asp:Content>
