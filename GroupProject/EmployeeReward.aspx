<%@ Page Title="" Language="C#" MasterPageFile="~/EmployeeMasterPage.master" AutoEventWireup="true" CodeFile="EmployeeReward.aspx.cs" Inherits="EmployeeReward" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <div class="btn-aligncenter">
        <asp:Label ID="lblInfo" runat="server" Text="Points" CssClass="table"></asp:Label>

        <div style="height: 75px;">
        <asp:Label ID="lblPoints" runat="server" Text="Points" CssClass="reward-points"></asp:Label>
        <asp:Button ID="btnReward" runat="server" Text="Reward Peers" class="button"/>
        <br />
    </div>
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="True"></asp:ScriptManager>
    <ajaxToolkit:ModalPopupExtender ID="popReward" runat="server" TargetControlID="btnReward" PopupControlID="pnlReward" CancelControlID="btnCancel" BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="pnlReward" runat="server" CssClass="popup" Height="400px" Width="900px">

        <h1>Reward a Peer</h1>
   
            <asp:Table ID="Table1" runat="server" CssClass="table">
                <asp:TableRow runat="server">
                    <asp:TableCell runat="server">
                        <asp:Label ID="lblRName" runat="server" Text="Name:"></asp:Label>
                        <ajaxToolkit:ComboBox ID="cbName" OnSelectedIndexChanged="cbName_SelectedIndexChanged" runat="server" AutoCompleteMode="Suggest" DropDownStyle="DropDownList" AutoPostBack="false" ValidateRequestMode="Enabled"></ajaxToolkit:ComboBox>
                    </asp:TableCell>
                    <asp:TableCell runat="server">
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server">
                    <asp:TableCell runat="server">
                        <asp:Label ID="lblRValue" runat="server" Text="Company Value:"></asp:Label>

                        <asp:DropDownList DataSourceID="srcRValue" OnSelectedIndexChanged="ddlRValue_SelectedIndexChanged" DataTextField="ValueName" DataValueField="ValueID" ID="ddlRValue" AppendDataBoundItems="true" runat="server">
                            <asp:ListItem Value="-1" Text="--Select Value--"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="srcRValue" SelectCommand="SELECT [ValueID],[ValueName]FROM [dbo].[Value]" ConnectionString="<%$ ConnectionStrings:GroupProjectConnectionString %>" runat="server" />
                    </asp:TableCell>
                    <asp:TableCell runat="server">
                        <asp:Label ID="lblRCategory" runat="server" Text="Reward Category:"></asp:Label>
                        <asp:DropDownList ID="ddlRCategory" OnSelectedIndexChanged="ddlRCategory_SelectedIndexChanged" DataSourceID="srcRCategory" DataTextField="Title" DataValueField="CategoryID" AppendDataBoundItems="true" runat="server">
                            <asp:ListItem Value="-1" Text="--Select Category--"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="srcRCategory" SelectCommand="SELECT [CategoryID],[Title]FROM [dbo].[Category]" ConnectionString="<%$ ConnectionStrings:GroupProjectConnectionString %>" runat="server" />
                    </asp:TableCell>
                    <asp:TableCell runat="server">
                        <asp:Label ID="lblRDescription" runat="server" Text="Description:"></asp:Label>
                        <asp:TextBox ID="txtRDescription" runat="server" MaxLength="250" TextMode="MultiLine" CssClass="textbox" style="overflow:hidden" Height="150px"></asp:TextBox>
     <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" class="auto-style8" ControlToValidate="txtRDescription" ForeColor="Red" ErrorMessage="*"></asp:RequiredFieldValidator>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server">
                    <asp:TableCell runat="server">
                        <asp:Label ID="lblValue" runat="server" Text="Reward Value:"></asp:Label>

                        <asp:RadioButtonList ID="rblRewardPoints" OnSelectedIndexChanged="rblRewardPoints_SelectedIndexChanged" runat="server" RepeatDirection="Horizontal" Width="150px">
                            <asp:ListItem Value="10">10 Points</asp:ListItem>

                            <asp:ListItem Value="15">15 Points</asp:ListItem>

                            <asp:ListItem Value="25">25 Points</asp:ListItem>

                        </asp:RadioButtonList>
                    </asp:TableCell>
                    <asp:TableCell runat="server"></asp:TableCell>
                    <asp:TableCell runat="server"></asp:TableCell>
                </asp:TableRow>
            </asp:Table>
            <asp:Button ID="btnCommit" runat="server" Text="Reward" CssClass="button" OnClick="btnCommit_Click" OnClientClick="if (!confirm('Please double check your entered data')) return false" />
            <asp:Button ID="btnCancel" runat="server" Text="" CssClass="btn-close" Style="background-image: url('http://icons.iconarchive.com/icons/iconsmind/outline/24/Close-icon.png'); background-repeat: no-repeat" />


    </asp:Panel>
    </div>
    
    <asp:DataList ID="dlPosts" runat="server" HorizontalAlign="Center">
            <ItemTemplate>
                <article class="card">
                        <section class="card-content">

                            <div class="card-content">
                            <a href="#" class="card-image" style="background-image: url(https://image.flaticon.com/icons/png/128/201/201651.png);"></a>
                            </div>
                                    <h2 class="card-title"><%# DataBinder.Eval(Container.DataItem, "RewarderName")%> rewards <%# DataBinder.Eval(Container.DataItem, "ReceiverName")%> for <%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "PointsAmount"))%> Points</h2>
                                    <p class="card-subtitle">Our company values <%# DataBinder.Eval(Container.DataItem, "ReceiverName")%>'s <%# DataBinder.Eval(Container.DataItem, "Title")%> <%# DataBinder.Eval(Container.DataItem, "ValueName")%></p>

                                    <p class="card-body">Comments: <%# DataBinder.Eval(Container.DataItem, "EventDescription")%></p>

                                    
                                    <asp:Label runat="server" ID="lblPostedOn" ForeColor="Black" Font-Italic="True" Font-Size="Small" CssClass="card-body">
                                            Posted On: <%# DataBinder.Eval(Container.DataItem, "LastUpdated","{0:d/M/yyyy}")%>
                                    </asp:Label>
                            
                        </section>
                </article>

            </ItemTemplate>
        </asp:DataList>
      

    </asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">
</asp:Content>

