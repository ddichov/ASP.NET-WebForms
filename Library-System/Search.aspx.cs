using Library_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Library_System
{
    public partial class Search : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
          string queryString = (string) this.Session["search"];
      
            LibraryEntities db = new LibraryEntities();
            if (String.IsNullOrEmpty(queryString))
            {
                var books = db.Books
                    .OrderBy(b => b.Title)
                    .ThenBy(y => y.Author)
                    .Select(x => new
                    {
                        BookId = x.BookId,
                        Title = x.Title,
                        Author = x.Author,
                        ISBN = x.ISBN,
                        WebSite = x.WebSite,
                        CategoryName = x.Category.CategoryName
                    });

                this.RepeaterSearchResults.DataSource = books.ToList();
                this.RepeaterSearchResults.DataBind();
                this.LiteralQuery.Text = "all";
            }
            else
            {
                var bookList = db.Books.Include("Categories")
                    .Where(b => b.Title.Contains(queryString) || b.Author.Contains(queryString))
                    .OrderBy(x => x.Title).ThenBy(y => y.Author)
                    .Select(x => new
                    {
                        BookId = x.BookId,
                        Title = x.Title,
                        Author = x.Author,
                        ISBN = x.ISBN,
                        WebSite = x.WebSite,
                        CategoryName = x.Category.CategoryName
                    });

                this.RepeaterSearchResults.DataSource = bookList.ToList();
                this.RepeaterSearchResults.DataBind();
                this.LiteralQuery.Text = Server.HtmlEncode(queryString);
            }
        }
    }
}