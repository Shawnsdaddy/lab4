<%@ Page Title="" Language="C#" MasterPageFile="~/EmployeeMasterPage.master" AutoEventWireup="true" CodeFile="EmployeeTeam.aspx.cs" Inherits="EmployeeTeam" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">

        <asp:Button ID="btnCreateT" runat="server" Text="Create A Team"  CssClass="btn-team" />
        <asp:Button ID="btnTSetting" runat="server" Text="Manage Your Team" CssClass="btn-team"/>
        <asp:Button ID="btnJoinT" runat="server" Text="Join A Team" CssClass="btn-team"/>
        <asp:Button ID="btnViewT" runat="server" Text="View Your Team" CssClass="btn-team" />
        <br />

    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="True"></asp:ScriptManager>
    <ajaxToolkit:ModalPopupExtender ID="popCreateT" runat="server" TargetControlID="btnCreateT" PopupControlID="divCreateT" CancelControlID="btnCancelCreate" BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>
    <ajaxToolkit:ModalPopupExtender ID="popTSetting" runat="server" TargetControlID="btnTSetting" PopupControlID="divTSetting" CancelControlID="btnCancelUpdate" BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>
    <ajaxToolkit:ModalPopupExtender ID="popJoin" runat="server" TargetControlID="btnJoinT" PopupControlID="divJoinT" CancelControlID="btnCancelJoin" BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>
    <ajaxToolkit:ModalPopupExtender ID="popView" runat="server" TargetControlID="btnViewT" PopupControlID="divViewT" CancelControlID="btnCancelView" BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>
    <div id="divCreateT" class="popup" style="width: 607px; height: 500px;">
        <h1>CREATE YOUROWN TEAM HERE</h1>
        <asp:Table ID="TblCreateT" runat="server" HorizontalAlign="Left" CssClass="table">
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="lblTeamName" runat="server" Text="Team Name:"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="txtTeamName" runat="server" MaxLength="20" CssClass="textbox"></asp:TextBox>
                </asp:TableCell><asp:TableCell>
                    <asp:RequiredFieldValidator ValidationGroup="Create" ID="RequiredFieldValidator3" runat="server" class="auto-style8" ForeColor="Red" ControlToValidate="txtTeamName" ErrorMessage="*"></asp:RequiredFieldValidator>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="lblStartDate" runat="server" Text="Start Date:"></asp:Label>
                </asp:TableCell><asp:TableCell>
                    <asp:TextBox ID="txtStartDate" runat="server" CssClass="textbox"></asp:TextBox>
                </asp:TableCell><asp:TableCell>
                    <asp:RequiredFieldValidator ValidationGroup="Create" ID="StartDateRequire" runat="server" class="auto-style8" ControlToValidate="txtStartDate" ForeColor="Red" ErrorMessage="*"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="StartDateMessage" ValidationGroup="Create" runat="server" ForeColor="Red" ControlToValidate="txtStartDate" Type="Date" Operator="DataTypeCheck" Text="Enter the date in the mm/dd/yyyy format"></asp:CompareValidator>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="lblED" runat="server" Text="End Date:"></asp:Label>
                </asp:TableCell><asp:TableCell>
                    <asp:TextBox ID="txtED" runat="server" CssClass="textbox"></asp:TextBox>
                </asp:TableCell><asp:TableCell>
                    <asp:CompareValidator ID="EDMessage" runat="server" ValidationGroup="Create" ControlToValidate="txtED" ControlToCompare="txtStartDate" Type="Date" Operator="GreaterThanEqual" ErrorMessage="End date must be greater than start date"></asp:CompareValidator>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="lblDescription" runat="server" Text="Team Description:"></asp:Label>
                </asp:TableCell><asp:TableCell RowSpan="1">
                    <asp:TextBox TextMode="MultiLine" ID="txtDescription" runat="server" Height="110px" MaxLength="250" CssClass="textbox"  style ="overflow:hidden"></asp:TextBox>
                </asp:TableCell><asp:TableCell>
                    <asp:RequiredFieldValidator ValidationGroup="Create" ID="RequiredFieldValidator2" runat="server" class="auto-style8" ControlToValidate="txtDescription" ForeColor="Red" ErrorMessage="*"></asp:RequiredFieldValidator>
                    <%--<asp:RegularExpressionValidator ValidationGroup="Create" ID="DescriptionMessage" runat="server" ControlToValidate="txtDescription" ValidationExpression="^.*(?=.*[a-zA-Z0-9\s])(?=.*\d)(?!=.*[\.@_-]).*$" Text="Description can not be more than 100 alphanumeric characters in length, can include special characters like ?!=.*\.@_-" />--%>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableFooterRow>
                <asp:TableCell> 
                    </asp:TableCell>
                <asp:TableCell> 
                    <div class ="btn-aligncenter"><asp:Button ID="btnCreate" runat="server" Text="CREATE NOW" ValidationGroup="Create" OnClick="btnCreate_Click" OnClientClick="if (!confirm('Please double check your entered data')) return false" CssClass="button"></asp:Button></div>
                    

                </asp:TableCell>
            </asp:TableFooterRow>
        </asp:Table>
        <asp:Button ID="btnCancelCreate" runat="server" Text="" CssClass="btn-close" Style="background-image: url('http://icons.iconarchive.com/icons/iconsmind/outline/24/Close-icon.png'); background-repeat: no-repeat" />
    </div>
    <div id="divTSetting" class="popup" style="width: 605px; height: 500px;">
        <h1>MANAGE YOUR OWN TEAM HERE</h1>
        <asp:Table ID="TblTSetting" runat="server" HorizontalAlign="Left">
            <asp:TableRow>
                <asp:TableCell>
                    <ajaxToolkit:ComboBox ID="cboUpdateTeam" runat="server" AutoCompleteMode="Suggest" DropDownStyle="DropDownList" AutoPostBack="true" ValidateRequestMode="Enabled" OnSelectedIndexChanged="cboUpdateTeam_SelectedIndexChanged">
                    </ajaxToolkit:ComboBox>
                    <%--                    <asp:RequiredFieldValidator ValidationGroup="Update" ID="RequiredFieldValidator4" runat="server" class="auto-style8" ControlToValidate="cboUpdateTeam" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="lblChangeName" runat="server" Text="Team Name:"></asp:Label>
                </asp:TableCell><asp:TableCell>
                    <asp:TextBox ID="txtChangeName" runat="server" MaxLength="20" CssClass="textbox"></asp:TextBox>
                </asp:TableCell><asp:TableCell>
                    <asp:RequiredFieldValidator ValidationGroup="Update" ID="RequiredFieldValidator4" runat="server" class="auto-style8" ForeColor="Red" ControlToValidate="txtChangeName" ErrorMessage="*"></asp:RequiredFieldValidator>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="lblChangeSD" runat="server" Text="Start Date:"></asp:Label>
                </asp:TableCell><asp:TableCell>
                    <asp:TextBox ID="txtChangeSD" runat="server" CssClass="textbox"></asp:TextBox>
                </asp:TableCell><asp:TableCell>
                    <asp:RequiredFieldValidator ValidationGroup="Update" ID="RequiredFieldValidator1" runat="server" class="auto-style8" ControlToValidate="txtChangeSD" ErrorMessage="*"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator1" ValidationGroup="Update" runat="server" ForeColor="Red" ControlToValidate="txtChangeSD" Type="Date" Operator="DataTypeCheck" Text="Enter the date in the mm/dd/yyyy format"></asp:CompareValidator>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="lblChangeED" runat="server" Text="End Date:"></asp:Label>
                </asp:TableCell><asp:TableCell>
                    <asp:TextBox ID="txtChangeED" runat="server" CssClass="textbox"></asp:TextBox>
                </asp:TableCell><asp:TableCell>
                    <asp:CompareValidator ID="CompareValidator2" runat="server" ValidationGroup="Update" ControlToValidate="txtChangeED" ControlToCompare="txtChangeSD" Type="Date" Operator="GreaterThanEqual" ErrorMessage="End date must be greater than start date"></asp:CompareValidator>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="lblChangeDesc" runat="server" Text="Team Description:"></asp:Label>
                </asp:TableCell><asp:TableCell>
                    <asp:TextBox TextMode="MultiLine" ID="txtChangeDesc" runat="server"  CssClass="textbox" style="overflow:auto"></asp:TextBox>
                </asp:TableCell><asp:TableCell>
                    <asp:RequiredFieldValidator ValidationGroup="Update" ID="RequiredFieldValidator5" runat="server" class="auto-style8" ControlToValidate="txtChangeDesc" ErrorMessage="*"></asp:RequiredFieldValidator>

                    <%--<asp:RegularExpressionValidator ValidationGroup="Update" ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtChangeDesc" ValidationExpression="^[a-zA-Z\s]{0,100}$" Text="Description can not be more than 100 alphabetic characters in length" />--%>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableFooterRow>
                <asp:TableCell>
                    <asp:Button ID="btnChange" runat="server" Text="Update Record" Width="150px" ValidationGroup="Update" OnClick="btnChange_Click" CssClass="button"></asp:Button>
                    </asp:TableCell>
                    <asp:TableCell>
                    <asp:Button ID="btnTerminate" runat="server" Text="Terminate Team" Width="150px" CausesValidation="false"  OnClick="btnTerminate_Click" OnClientClick="if (!confirm('Are you sure yo want to terminate this team?')) return false" CssClass="button-red"></asp:Button>
                </asp:TableCell>
            </asp:TableFooterRow>
        </asp:Table>
        <asp:Button ID="btnCancelUpdate" runat="server" Text="" CssClass="btn-close" Style="background-image: url('http://icons.iconarchive.com/icons/iconsmind/outline/24/Close-icon.png'); background-repeat: no-repeat" />
    </div>
    <div id="divJoinT" class="popup" style="width: 602px; height: 150px;">
        <h1>JOIN A TEAM HERE</h1>
        <asp:Table ID="TblTJoin" runat="server" HorizontalAlign="Left">
            <asp:TableRow>
                <asp:TableCell>
                    <ajaxToolkit:ComboBox ID="cboJoin" runat="server" AutoCompleteMode="Suggest" DropDownStyle="DropDownList" AutoPostBack="true" ValidateRequestMode="Enabled" OnSelectedIndexChanged="cboJoin_SelectedIndexChanged"></ajaxToolkit:ComboBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" class="auto-style8" ControlToValidate="cboJoin" ErrorMessage="*"></asp:RequiredFieldValidator>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableFooterRow>


                <asp:TableCell>
                    <asp:Button ID="btnJoin" runat="server" Text="Join Now" Width="150px" OnClick="btnJoin_Click" OnClientClick="if (!confirm('Are you sure yo want to join this team?')) return false" CssClass="button"></asp:Button>
                </asp:TableCell>
            </asp:TableFooterRow>
        </asp:Table>
        <asp:Button ID="btnCancelJoin" runat="server" Text="" CssClass="btn-close" Style="background-image: url('http://icons.iconarchive.com/icons/iconsmind/outline/24/Close-icon.png'); background-repeat: no-repeat" />
    </div>
    <div id="divViewT" class="popup" style="width: 606px; height: 226px;">
        <h1>VIEW YOUR TEAM HERE</h1>
        <asp:Table ID="TblTView" runat="server" HorizontalAlign="Left">
            <asp:TableRow>
                <asp:TableCell>
                    <ajaxToolkit:ComboBox ID="cboView" runat="server" AutoCompleteMode="Suggest" DropDownStyle="DropDownList" AutoPostBack="true" ValidateRequestMode="Enabled" OnSelectedIndexChanged="cboView_SelectedIndexChanged"></ajaxToolkit:ComboBox>

                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="lblManager" runat="server" Text="Team Creator:"></asp:Label>
                    <asp:Label ID="lblMName" runat="server"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <asp:DataList ID="dlTeamMembers" runat="server" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False">
            <ItemTemplate>
                <table class="stylePost" style="border: solid black">
                    <tr>
                        <td>
                            <%# DataBinder.Eval(Container.DataItem, "FirstName")%>  <%# DataBinder.Eval(Container.DataItem, "LastName")%> Joins At: <%# DataBinder.Eval(Container.DataItem, "[JoinDate]","{0:M/d/yyyy}")%></td>
                    </tr>
                </table>
            </ItemTemplate>
        </asp:DataList><asp:Button ID="btnCancelView" runat="server" Text="" CssClass="btn-close" Style="background-image: url('http://icons.iconarchive.com/icons/iconsmind/outline/24/Close-icon.png'); background-repeat: no-repeat" />
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">
</asp:Content>

