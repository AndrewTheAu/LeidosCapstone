using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1 {
    public partial class WebForm4 : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {

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
    }
}