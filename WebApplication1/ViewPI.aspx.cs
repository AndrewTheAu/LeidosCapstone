using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1 {
    public partial class WebForm1 : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {

        }

        protected void Button1_Click(object sender, EventArgs e) {

            SqlDataSource1.SelectCommand = "SELECT * FROM[Principal Investigator]";

            if (!(string.IsNullOrEmpty(id.Text) && string.IsNullOrEmpty(firstname.Text) && string.IsNullOrEmpty(lastname.Text) && string.IsNullOrEmpty(institution.Text) && string.IsNullOrEmpty(research.Text))) {
                SqlDataSource1.SelectCommand += " WHERE(";

                List<String> clauses = new List<String>();
                if (!string.IsNullOrEmpty(id.Text)) {
                    clauses.Add("([Investigator ID] = '" + id.Text + "')");
                }

                if (!string.IsNullOrEmpty(firstname.Text)) {
                    clauses.Add("([First Name] = '" + firstname.Text + "')");
                }

                if (!string.IsNullOrEmpty(lastname.Text)) {
                    clauses.Add("([Last Name] = '" + lastname.Text + "')");
                }

                if (!string.IsNullOrEmpty(institution.Text)) {
                    clauses.Add("([Institution] = '" + institution.Text + "')");
                }

                if (!string.IsNullOrEmpty(research.Text)) {
                    clauses.Add("([Research] = '" + research.Text + "')");
                }

                SqlDataSource1.SelectCommand += (String.Join(" AND ", clauses) + ")");
            }
        }
    }
}