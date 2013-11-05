using Library_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Library_System
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LibraryEntities db = new LibraryEntities();
            this.RepeaterMain.DataSource = db.Books.ToList();
            this.RepeaterMain.DataBind();
        }

        protected void Search_Click(object sender, EventArgs e)
        {
            string query = this.q.Text;
            this.Session["search"] = query;
            if (query.Length>1000)
            {
                 Error_Handler_Control.ErrorSuccessNotifier.AddErrorMessage(
                        "Query string is too long!Use less than 1000 letters.");
                 return;
            }
            string url = string.Format("Search.aspx?q={0}", query);
            Response.Redirect(url);
        }
    }
}