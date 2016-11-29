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
                <li><a data-toggle="tab" href="#menu2">Edit</a></li>
                <li><a data-toggle="tab" href="#menu3">Delete</a></li>
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
                            <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False" DataSourceID="GrantSource">
                                <Columns>
                                    <asp:BoundField DataField="Grant_Number" HeaderText="Grant_Number" SortExpression="Grant_Number" />
                                    <asp:BoundField DataField="Funded_Research" HeaderText="Funded_Research" SortExpression="Funded_Research" />
                                    <asp:BoundField DataField="Principal_Investigator" HeaderText="Principal_Investigator" SortExpression="Principal_Investigator" />
                                </Columns>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div id="menu1" class="tab-pane fade">
                    <h3>Add To Table</h3>
                </div>
                <div id="menu2" class="tab-pane fade">
                    <h3>Delete From Table</h3>
                    <p>Some content in menu 2.</p>
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
