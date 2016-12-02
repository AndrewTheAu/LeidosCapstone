<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewGrant.aspx.cs" Inherits="WebApplication1.ViewGrant" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.1.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <link href="default.css" rel="stylesheet" />
    <link href="sticky-header.css" rel="stylesheet" />
    <title>View Grants</title>
</head>
<body>
    <form id="form1" runat="server">
        <header class="header">
            <div class="container">
                <p class="text-muted"><a class="headertext" href="HomePage.aspx">Home</a></p>
            </div>
        </header>
        <div class="container">
            <h1>Grants</h1>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
        </div>
        <div class="container">
            <ul class="nav nav-tabs">
                <li class="active"><a data-toggle="tab" href="#home">View</a></li>
                <li><a data-toggle="tab" href="#menu1">Add</a></li>
                <li><a data-toggle="tab" href="#menu2">Delete</a></li>
                <li><a data-toggle="tab" href="#menu3">Edit</a></li>
            </ul>
            <div class="tab-content">
                <div id="home" class="tab-pane fade in active">
                    <h3>HOME</h3>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:SqlDataSource ID="GrantSource" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT [Grant Number] AS Grant_Number, [Funded Research] AS Funded_Research, [Principal Investigator] AS Principal_Investigator FROM [Grants]"></asp:SqlDataSource>
                            <button type="button" class="btn btn-info" data-toggle="collapse" data-target="#filters">Filter</button>
                            <div id="filters" class="collapse">
                                Grant #:
                <asp:TextBox ID="grantnumber" runat="server"></asp:TextBox>
                                <br />
                                Research:
                <asp:TextBox ID="research" runat="server"></asp:TextBox>
                                <br />
                                Principal Investigator:
                <asp:TextBox ID="investigator" runat="server"></asp:TextBox>
                                <br />
                                <br />
                                <asp:Button ID="Button1" runat="server" Height="35px" OnClick="Button1_Click" Text="Search" Width="188px" />
                                <br />
                            </div>
                            <br />
                            <asp:GridView ID="GridView1" runat="server" ShowHeaderWhenEmpty="True" AllowSorting="True" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataSourceID="GrantSource" ForeColor="Black" GridLines="Horizontal" Width="746px">
                        <Columns>
                            <asp:BoundField DataField="Grant_Number" HeaderText="Grant_Number" SortExpression="Grant_Number" />
                            <asp:BoundField DataField="Funded_Research" HeaderText="Funded_Research" SortExpression="Funded_Research" />
                            <asp:BoundField DataField="Principal_Investigator" HeaderText="Principal_Investigator" SortExpression="Principal_Investigator" />
                        </Columns>
                        <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                        <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                        <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#F7F7F7" />
                        <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                        <SortedDescendingCellStyle BackColor="#E5E5E5" />
                        <SortedDescendingHeaderStyle BackColor="#242121" />
                    </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div id="menu1" class="tab-pane fade">
                    <h3>Add To Table</h3>
                    <div>
                        Grant #:
                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                        <br />
                        Research:
                <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                        <br />
                        Principal Investigator:
                        <asp:SqlDataSource ID="InvestigatorSource" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT [First Name] + ' ' + [Last Name] AS Investigator_Name FROM [Principal Investigator]"></asp:SqlDataSource>
                        <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="InvestigatorSource" DataTextField="Investigator_Name" DataValueField="Investigator_Name" AppendDataBoundItems="true">
                            <asp:ListItem Value="" Text=""></asp:ListItem>
                        </asp:DropDownList>
                        <%--<asp:RequiredFieldValidator ID="reqDDL1" runat="server" SetFocusOnError="true" InitialValue="0" ErrorMessage="*" ControlToValidate="DropDownList1" />--%>
                        <br />
                        <br />
                        <asp:Button ID="Button2" runat="server" Height="35px" OnClick="Button2_Click" Text="Insert" Width="188px" />
                        <br />
                    </div>
                </div>
                <div id="menu2" class="tab-pane fade">
                    <h3>Delete From Table</h3>
                    <asp:SqlDataSource ID="PrincipalInvestigatorData" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT [First Name] + ' ' + [Last Name] AS Investigator_Name FROM [Principal Investigator]"></asp:SqlDataSource>
                    <asp:SqlDataSource ID="GrantNumberData" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT [Grant Number] AS Grant_Number FROM [Grants]"></asp:SqlDataSource>
                    <asp:SqlDataSource ID="FundedResearchData" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT [Funded Research] AS Funded_Research FROM [Grants]"></asp:SqlDataSource>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>


                            <asp:DropDownList ID="DropDownList2" runat="server" AppendDataBoundItems="true" AutoPostBack="True" DataSourceID="GrantNumberData" DataTextField="Grant_Number" DataValueField="Grant_Number" OnSelectedIndexChanged="UpdateDropDown" EnableViewState="true">
                                <asp:ListItem Text="" Value=""></asp:ListItem>
                            </asp:DropDownList>
                            <asp:DropDownList ID="DropDownList3" runat="server" AppendDataBoundItems="true" AutoPostBack="True" DataSourceID="FundedResearchData" DataTextField="Funded_Research" DataValueField="Funded_Research" OnSelectedIndexChanged="UpdateDropDown" EnableViewState="true">
                                <asp:ListItem Text="" Value=""></asp:ListItem>
                            </asp:DropDownList>
                            <asp:DropDownList ID="DropDownList4" runat="server" AppendDataBoundItems="true" AutoPostBack="True" DataSourceID="PrincipalInvestigatorData" DataTextField="Investigator_Name" DataValueField="Investigator_Name" OnSelectedIndexChanged="UpdateDropDown" EnableViewState="true">
                                <asp:ListItem Text="" Value=""></asp:ListItem>
                            </asp:DropDownList>
                            <asp:Label ID="Label2" runat="server" ForeColor="Red"></asp:Label>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="DropDownList3" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                <div id="menu3" class="tab-pane fade">
                    <h3>Edit Entries</h3>
                    <p>Some content in menu 3.</p>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
