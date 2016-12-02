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
            ToDelete();
        }

        protected void AddBlank(object sender, EventArgs e) {
            ((DropDownList)sender).Items.Insert(0, new ListItem("", ""));
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

        protected void Button3_Click(object sender, EventArgs e) {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            {
                String command = "DELETE FROM [Grants]";

                if ((DropDownList2.SelectedValue != "") || (DropDownList3.SelectedValue != "") || (DropDownList4.SelectedValue != "")) {
                    command += " WHERE (";

                    List<String> clauses = new List<String>();
                    if (DropDownList2.SelectedValue != "") {
                        clauses.Add($"([Grant Number] = '{DropDownList2.SelectedValue}')");
                    }

                    if (DropDownList3.SelectedValue != "") {
                        clauses.Add($"([Funded Research] = '{DropDownList3.SelectedValue}')");
                    }

                    if (DropDownList4.SelectedValue != "") {
                        clauses.Add($"([Principal Investigator] = '{DropDownList4.SelectedValue}')");
                    }

                    command += (String.Join(" AND ", clauses) + ")");
                }

                SqlCommand SQLCommand = new SqlCommand(command, connection);
                connection.Open();
                SQLCommand.ExecuteNonQuery();
                connection.Close();

                GridView1.DataBind();

                DropDownList2.SelectedIndex = 0;
                DropDownList3.SelectedIndex = 0;
                DropDownList4.SelectedIndex = 0;
                GridView2.DataBind();
            }

            ToDelete();
        }

        protected void ToDelete() {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            {
                String command = "SELECT [Grant Number] AS Grant_Number, [Funded Research] AS Funded_Research, [Principal Investigator] AS Principal_Investigator FROM [Grants]";

                if ((DropDownList2.SelectedValue != "") || (DropDownList3.SelectedValue != "") || (DropDownList4.SelectedValue != "")) {
                    command += " WHERE (";

                    List<String> clauses = new List<String>();
                    if (DropDownList2.SelectedValue != "") {
                        clauses.Add($"([Grant Number] = '{DropDownList2.SelectedValue}')");
                    }

                    if (DropDownList3.SelectedValue != "") {
                        clauses.Add($"([Funded Research] = '{DropDownList3.SelectedValue}')");
                    }

                    if (DropDownList4.SelectedValue != "") {
                        clauses.Add($"([Principal Investigator] = '{DropDownList4.SelectedValue}')");
                    }

                    command += (String.Join(" AND ", clauses) + ")");
                }

                //Label1.Text = command;
                SqlCommand SQLCommand = new SqlCommand(command, connection);

                DataTable table = new DataTable();

                SqlDataAdapter adapter = new SqlDataAdapter(SQLCommand);

                adapter.Fill(table);

                GridView2.DataSourceID = null;
                GridView2.DataSource = table;
                GridView2.DataBind();
            }
        }

        protected void UpdateDDL(SqlConnection connection,
                DropDownList update, DropDownList dependancy1, DropDownList dependancy2,
                String updateName, String dependancy1Name, String dependancy2Name) {
            string CMDString = $"SELECT DISTINCT [{updateName.Replace("_", " ")}] AS {updateName} FROM [Grants]";
            if (dependancy1.SelectedValue != "" || dependancy2.SelectedValue != "") {
                if (dependancy1.SelectedValue != "" && dependancy2.SelectedValue != "") {
                    CMDString += $" WHERE [{dependancy1Name}] = '{dependancy1.SelectedValue}' AND [{dependancy2Name}] = '{dependancy2.SelectedValue}'";
                } else if (dependancy1.SelectedValue != "") {
                    CMDString += $" WHERE [{dependancy1Name}] = '{dependancy1.SelectedValue}'";
                } else {
                    CMDString += $" WHERE [{dependancy2Name}] = '{dependancy2.SelectedValue}'";
                }
            }

            SqlCommand CMD = new SqlCommand(CMDString, connection);
            DataTable table = new DataTable();

            SqlDataAdapter adapter = new SqlDataAdapter(CMD);

            adapter.Fill(table);

            update.DataSourceID = null;
            update.DataSource = table;
            update.DataTextField = updateName;
            String temp = update.SelectedValue;
            update.DataBind();
            if (update.Items.FindByValue(temp) != null) {
                update.Items.FindByValue(temp).Selected = true;
            }
            ToDelete();
        }

        protected void UpdateDropDown(object sender, EventArgs e) {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            {
                connection.Open();
                UpdateDDL(connection, DropDownList2, DropDownList3, DropDownList4, "Grant_Number", "Funded Research", "Principal Investigator");
                UpdateDDL(connection, DropDownList3, DropDownList2, DropDownList4, "Funded_Research", "Grant Number", "Principal Investigator");
                UpdateDDL(connection, DropDownList4, DropDownList2, DropDownList3, "Principal_Investigator", "Grant Number", "Funded Research");
            }
        }


    }

}