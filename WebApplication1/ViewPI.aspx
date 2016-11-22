<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewPI.aspx.cs" Inherits="WebApplication1.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.1.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <link href="sticky-footer.css" rel="stylesheet">
    <title>Principal Investigators</title>
</head>
<body>
    <form id="form1" runat="server">
        <h1>Principal Investigators</h1>

        <div>
            <br />
            <button type="button" class="btn btn-info" data-toggle="collapse" data-target="#filters">Filter</button>
            <div id="filters" class="collapse">
                <br />
                Investigator ID:
                <asp:TextBox ID="id" runat="server"></asp:TextBox>
                <br />
                First Name:
                <asp:TextBox ID="firstname" runat="server"></asp:TextBox>
                &nbsp;Last Name:
                <asp:TextBox ID="lastname" runat="server"></asp:TextBox>
                <br />
                Institution:
                <asp:TextBox ID="institution" runat="server"></asp:TextBox>
                &nbsp;Research:
                <asp:TextBox ID="research" runat="server"></asp:TextBox>
                <br />
                <br />
                <asp:Button ID="Button1" runat="server" Height="35px" OnClick="Button1_Click" Text="Search" Width="188px" />
                <br />
            </div>
        </div>
        <br />
        <asp:GridView ID="GridView1" runat="server" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataSourceID="SqlDataSource1" ForeColor="Black" GridLines="Horizontal" DataKeyNames="Investigator ID" AllowSorting="True" Width="746px">
            <Columns>
                <asp:BoundField DataField="Investigator ID" HeaderText="Investigator ID" SortExpression="Investigator ID" ReadOnly="True" />
                <asp:BoundField DataField="First Name" HeaderText="First Name" SortExpression="First Name" />
                <asp:BoundField DataField="Last Name" HeaderText="Last Name" SortExpression="Last Name" />
                <asp:BoundField DataField="Institution" HeaderText="Institution" SortExpression="Institution" />
                <asp:BoundField DataField="Research" HeaderText="Research" InsertVisible="False" ReadOnly="True" SortExpression="Research" />
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
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT * FROM [Principal Investigator]" OnSelecting="SqlDataSource1_Selecting"></asp:SqlDataSource>
    </form>
    <footer class="footer">
        <div class="container">
            <p class="text-muted"><a href="HomePage.aspx">Home</a></p>
        </div>
    </footer>
</body>
</html>
