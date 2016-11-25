using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

namespace WebApplication1 {
    public partial class WebForm4 : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            if (Page.IsPostBack == true) {
                Label1.Text = ("Successfully inserted");
            }
        }

        protected void Button1_Click(object sender, EventArgs e) {

            SqlDataSource1.SelectCommand = "SELECT * FROM[Grants]";

            if (!(string.IsNullOrEmpty(grantnumber.Text) && string.IsNullOrEmpty(research.Text) && string.IsNullOrEmpty(investigator.Text))) {
                SqlDataSource1.SelectCommand += " WHERE(";

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

                SqlDataSource1.SelectCommand += (String.Join(" AND ", clauses) + ")");
            }
        }

        protected void SqlDataSource1_Selecting(object sender, SqlDataSourceSelectingEventArgs e) {

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
    }
}