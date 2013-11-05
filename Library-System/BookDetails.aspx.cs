using Library_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Library_System
{
    public partial class BookDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LibraryEntities db = new LibraryEntities();
            if (Convert.ToInt32(Request.Params["Id"]) != 0)
            {
                IList<Book> bookList = new List<Book>();
                var book = db.Books.Find(Convert.ToInt32(Request.Params["Id"]));
                bookList.Add(book);
                this.RepeaterBook.DataSource = bookList;
                this.RepeaterBook.DataBind();
            }
        }
    }
}