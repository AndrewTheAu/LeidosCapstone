using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace WebApplication1 {
    public partial class ViewGrant : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {

        }

        protected void Button1_Click(object sender, EventArgs e) {
            GrantSource.SelectCommand = "SELECT [Grant Number] AS Grant_Number, [Funded Research] AS Funded_Research, [Principal Investigator] AS Principal_Investigator FROM [Grants]";

            if (!(string.IsNullOrEmpty(grantnumber.Text) && string.IsNullOrEmpty(research.Text) && string.IsNullOrEmpty(investigator.Text))) {
                GrantSource.SelectCommand += " WHERE(";

                List<String> clauses = new List<String>();
                if (!string.IsNullOrEmpty(grantnumber.Text)) {
                    clauses.Add("([Grant Number] like '%" + grantnumber.Text + "%')");
                }

                if (!string.IsNullOrEmpty(research.Text)) {
                    clauses.Add("([Funded Research] like '%" + research.Text + "%')");
                }

                if (!string.IsNullOrEmpty(investigator.Text)) {
                    clauses.Add("([Principal Investigator] like '%" + investigator.Text + "%')");
                }

                GrantSource.SelectCommand += (String.Join(" AND ", clauses) + ")");
            }

            GrantSource.DataBind();
            GridView1.DataBind();
        }

        protected void Button2_Click(object sender, EventArgs e) {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            {
                SqlCommand command = new SqlCommand("INSERT INTO Grants([Grant Number], [Funded Research], [Principal Investigator]) Values(@Number, @Research, @Investigator)", connection);
                command.Parameters.AddWithValue("@Number", TextBox1.Text);
                command.Parameters.AddWithValue("@Research", TextBox2.Text);
                command.Parameters.AddWithValue("@Investigator", DropDownList1.Text);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();

                if (IsPostBack) {
                    TextBox1.Text = "";
                    TextBox2.Text = "";
                    Page.Response.Redirect(Page.Request.Url.ToString(), true);
                }
            }
        }

        protected void NewGrantNumber(SqlConnection connection) {

        }

        protected void NewFundedResearch(SqlConnection connection) {
            string CMDString = "SELECT [Funded Research] AS Funded_Research FROM [Grants]";
            if (DropDownList2.SelectedValue != "" || DropDownList4.SelectedValue != "") {
                if (DropDownList2.SelectedValue != "" && DropDownList4.SelectedValue != "") {
                    CMDString += $" WHERE [Grant Number] = '{DropDownList2.SelectedValue}' AND [Principal Investigator] = '{DropDownList4.SelectedValue}'";
                } else if (DropDownList2.SelectedValue != "") {
                    CMDString += $" WHERE [Grant Number] = '{DropDownList2.SelectedValue}'";
                } else {
                    CMDString += $" WHERE [Principal Investigator] = '{DropDownList4.SelectedValue}'";
                }
            }

            SqlCommand CMD = new SqlCommand(CMDString, connection);
            DataTable table = new DataTable();

            SqlDataAdapter adapter = new SqlDataAdapter(CMD);

            adapter.Fill(table);

            Label2.Text = CMDString;
            DropDownList3.DataSourceID = null;
            DropDownList3.DataSource = table;
            DropDownList3.DataTextField = "Funded_Research";
            DropDownList3.DataBind();
        }

        protected void NewInvestigator(SqlConnection connection) {

        }

        protected void UpdateDropDown(object sender, EventArgs e) {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            {
                connection.Open();
                NewGrantNumber(connection);
                NewFundedResearch(connection);
                NewInvestigator(connection);
            }
        }


    }

}