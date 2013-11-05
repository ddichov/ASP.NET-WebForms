using Library_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Library_System.Admin
{
    public partial class EditBooks : System.Web.UI.Page
    {
        private const int MaxTitleLength = 20;
        private int BookId
        {
            get
            {
                return (int)this.Session["bookId"];
            }
            set
            {
                this.Session["bookId"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            LibraryEntities db = new LibraryEntities();
            HidePanels();
        }

        // The return type can be changed to IEnumerable, however to support
        // paging and sorting, the following parameters must be added:
        //     int maximumRows
        //     int startRowIndex
        //     out int totalRowCount
        //     string sortByExpression
        public IQueryable<Object> GridViewBooks_GetData()
        {
            LibraryEntities db = new LibraryEntities();
            var result = db.Books.Include("Categories").Select(x => new
            {
                BookId = x.BookId,
                Title = x.Title,
                Author = x.Author,
                ISBN = x.ISBN,
                WebSite = x.WebSite,
                CategoryName = x.Category.CategoryName
            });
            return result.OrderBy(x => x.Title);
        }

        public IEnumerable<Library_System.Models.Category> DropDownCategoriesCreate_GetData()
        {
            LibraryEntities db = new LibraryEntities();
            var result = db.Categories.ToList();
            return result;
        }
        public IEnumerable<Library_System.Models.Category> DropDownListCategoryEdit_GetData()
        {
            LibraryEntities db = new LibraryEntities();
            var result = db.Categories.ToList();
            return result;
        }

        protected void ButtonDeleteBook_Command(object sender, CommandEventArgs e)
        {
            HidePanels();
            this.PanelDelete.Visible = true;
            var CategoryId = e.CommandArgument;
            BookId = Convert.ToInt32(CategoryId);
            using (LibraryEntities db = new LibraryEntities())
            {
                Book book = db.Books.Find(BookId);
                this.TextBoxDelete.Text = Server.HtmlEncode(book.Title);
            }
        }

        protected void ButtonEditBook_Command(object sender, CommandEventArgs e)
        {
            HidePanels();
            this.PanelEdit.Visible = true;
            var arg = e.CommandArgument;
            BookId = Convert.ToInt32(arg);
            using (LibraryEntities db = new LibraryEntities())
            {
                Book book = db.Books.Find(BookId);
                this.TextBoxTitleEdit.Text = book.Title;
                this.TextBoxAuthorEdit.Text = book.Author;
                this.TextBoxISBNEdit.Text = book.ISBN;
                this.TextBoxWebSiteEdit.Text = book.WebSite;
                this.DropDownListCategoryEdit.SelectedValue = book.CategoryId.ToString();
                this.TextBoxDescriptionEdit.Text = book.Description;
            }
        }

        protected void ButtonShowCreatePanel_Click(object sender, EventArgs e)
        {
            HidePanels();
            this.PanelCreate.Visible = true;
        }

        protected void ButtonCreate_Click(object sender, EventArgs e)
        {
            bool isInputValid = true;
            string author = this.TextBoxAuthorCreate.Text;
            string title = this.TextBoxBookTitleCreate.Text;
            string isbn = this.TextBoxISBNCreate.Text;
            string web = this.TextBoxWebSiteCreate.Text;
            string descr = this.TextBoxDescriptionCreate.Text;
            int categId = Convert.ToInt32(this.DropDownCategoriesCreate.SelectedValue);
            ////Book title, author and category are mandatory.
            if (!string.IsNullOrWhiteSpace(title) && !string.IsNullOrEmpty(title) && title.Length < 250 && categId > 0)
            {
                LibraryEntities db = new LibraryEntities();
                Book newBook = new Book();
                newBook.Title = title;

                if (!string.IsNullOrWhiteSpace(author) && !string.IsNullOrEmpty(author) && author.Length < 250)
                {
                    newBook.Author = author;
                }
                else
                {
                    isInputValid = false;
                    Error_Handler_Control.ErrorSuccessNotifier.AddErrorMessage(
                        "Author is not corect!");
                }

                if (db.Categories.FirstOrDefault(c => c.CategoryId == categId) != null)
                {
                    newBook.CategoryId = categId;
                }
                else
                {
                    isInputValid = false;
                    Error_Handler_Control.ErrorSuccessNotifier.AddErrorMessage(
                        "Category is not corect!");
                }

                if (!string.IsNullOrWhiteSpace(isbn) && !string.IsNullOrEmpty(isbn))
                {
                    if (isbn.Length < 19)
                    {
                        newBook.ISBN = isbn;
                    }
                    else
                    {
                        isInputValid = false;
                        Error_Handler_Control.ErrorSuccessNotifier.AddErrorMessage(
                            "ISBN is not corect!");
                    }
                }

                if (!string.IsNullOrWhiteSpace(web) && !string.IsNullOrEmpty(web))
                {
                    if (web.Contains('<') || web.Contains('>'))
                    {
                        isInputValid = false;
                        Error_Handler_Control.ErrorSuccessNotifier.AddErrorMessage(
                            "WebSite Field contains invalid characters!");
                    }
                    else
                    {
                        newBook.WebSite = web;
                    }
                }

                if (!string.IsNullOrWhiteSpace(descr) && !string.IsNullOrEmpty(descr))
                {
                    newBook.Description = descr;
                }
                
                if (isInputValid)
                {
                    db.Books.Add(newBook);
                    try
                    {
                        db.SaveChanges();
                        Error_Handler_Control.ErrorSuccessNotifier.AddSuccessMessage(
                            "Book created!");
                    }
                    catch (Exception ex)
                    {
                        Error_Handler_Control.ErrorSuccessNotifier.AddErrorMessage(ex.Message);
                        return;
                    }
                }
            }
            else
            {
                Error_Handler_Control.ErrorSuccessNotifier.AddErrorMessage(
                    "Title mast be Max.250 chars and can not be null or empty string!");
                return;
            }

            this.PanelCreate.Visible = false;
            Response.Redirect("EditBooks.aspx");
        }

        protected void ButtonCancelCreate_Click(object sender, EventArgs e)
        {
            HidePanels();
        }

        protected void ButtonConfirmDelete_Click(object sender, EventArgs e)
        {
            if (BookId < 1)
            {
                Error_Handler_Control.ErrorSuccessNotifier.AddErrorMessage(
                    "Wrong Category Id!");
            }
            else
            {
                LibraryEntities db = new LibraryEntities();
                Book book = db.Books.Find(BookId);

                try
                {
                    db.Books.Remove(book);
                    db.SaveChanges();
                    Error_Handler_Control.ErrorSuccessNotifier.AddSuccessMessage(
                        "Book Removed!");
                }
                catch (Exception ex)
                {
                    Error_Handler_Control.ErrorSuccessNotifier.AddErrorMessage(ex.Message);
                    return;
                }
            }

            this.PanelDelete.Visible = false;
            this.BookId = 0;
            Response.Redirect("EditBooks.aspx");
        }

        protected void ButtonCancelDelete_Click(object sender, EventArgs e)
        {
            HidePanels();
            this.BookId = 0;
        }

        protected void ButtonEditSave_Click(object sender, EventArgs e)
        {
            bool isInputValid = true;
            string author = this.TextBoxAuthorEdit.Text;
            string title = this.TextBoxTitleEdit.Text;
            string isbn = this.TextBoxISBNEdit.Text;
            string web = this.TextBoxWebSiteEdit.Text;
            string descr = this.TextBoxDescriptionEdit.Text;
            int categId = Convert.ToInt32(this.DropDownListCategoryEdit.SelectedValue);
            LibraryEntities db = new LibraryEntities();
            Book currentBook = db.Books.Find(BookId);
            ////Book title, author and category are mandatory.
            if (!string.IsNullOrWhiteSpace(title) && !string.IsNullOrEmpty(title) && title.Length < 250 && categId > 0)
            {
                currentBook.Title = title;
                if (!string.IsNullOrWhiteSpace(author) && !string.IsNullOrEmpty(author) && author.Length < 250)
                {
                    currentBook.Author = author;
                }
                else
                {
                    isInputValid = false;
                    Error_Handler_Control.ErrorSuccessNotifier.AddErrorMessage(
                        "Author is not corect!");
                }

                if (db.Categories.FirstOrDefault(c => c.CategoryId == categId) != null)
                {
                    currentBook.CategoryId = categId;
                }
                else
                {
                    isInputValid = false;
                    Error_Handler_Control.ErrorSuccessNotifier.AddErrorMessage(
                        "Category is not corect!");
                }

                if (!string.IsNullOrWhiteSpace(isbn) && !string.IsNullOrEmpty(isbn))
                {
                    if (isbn.Length < 19)
                    {
                        currentBook.ISBN = isbn;
                    }
                    else
                    {
                        isInputValid = false;
                        Error_Handler_Control.ErrorSuccessNotifier.AddErrorMessage(
                            "ISBN is not corect!");
                    }
                }
                if (!string.IsNullOrWhiteSpace(web) && !string.IsNullOrEmpty(web))
                {
                    if (web.Contains('<') || web.Contains('>'))
                    {
                        isInputValid = false;
                        Error_Handler_Control.ErrorSuccessNotifier.AddErrorMessage(
                            "WebSite field contains invalid characters!");
                    }
                    else
                    {
                        currentBook.WebSite = web;
                    }
                }
                if (!string.IsNullOrWhiteSpace(descr) && !string.IsNullOrEmpty(descr))
                {
                    currentBook.Description = descr;
                }
               
                if (isInputValid)
                {
                    try
                    {
                        db.SaveChanges();
                        Error_Handler_Control.ErrorSuccessNotifier.AddSuccessMessage(
                            "Changes Saved!");
                    }
                    catch (Exception ex)
                    {
                        Error_Handler_Control.ErrorSuccessNotifier.AddErrorMessage(ex.Message);
                        return;
                    }
                }
            }
            else
            {
                Error_Handler_Control.ErrorSuccessNotifier.AddErrorMessage(
                    "Title mast be Max.250 chars and can not be null or empty string!");
                return;
            }
            this.PanelCreate.Visible = false;
            Response.Redirect("EditBooks.aspx");
        }

        protected void ButtonCancelEdit_Click(object sender, EventArgs e)
        {
            HidePanels();
            this.BookId = 0;
        }

        protected void HidePanels()
        {
            this.PanelEdit.Visible = false;
            this.PanelCreate.Visible = false;
            this.PanelDelete.Visible = false;
        }
        protected string TrimString(string str)
        {
            if (str == null || str.Length <= MaxTitleLength)
            {
                return str;
            }
            else
            {
                return str.Substring(0, (MaxTitleLength - 3)) + "...";
            }
        }
    }
}