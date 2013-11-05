using Library_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Library_System.Admin
{
    public partial class EditCategories : System.Web.UI.Page
    {
        private int CatId
        {
            get
            {
                return (int)this.Session["CatId"];
            }
            set
            {
                this.Session["CatId"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            HidePanels();
        }

        protected void ButtonCancelCreate_Click(object sender, EventArgs e)
        {
            HidePanels();
        }

        protected void ButtonCreate_Click(object sender, EventArgs e)
        {
            string text = this.TextBoxCategoryCreate.Text;
            if (!string.IsNullOrWhiteSpace(text) && !string.IsNullOrEmpty(text) && text.Length < 150)
            {
                Category newCategiry = new Category();
                newCategiry.CategoryName = text;

                LibraryEntities db = new LibraryEntities();
                db.Categories.Add(newCategiry);
                try
                {
                    db.SaveChanges();
                    Error_Handler_Control.ErrorSuccessNotifier.AddSuccessMessage("Category created!");
                }
                catch (Exception ex)
                {
                    Error_Handler_Control.ErrorSuccessNotifier.AddErrorMessage(ex.Message);
                    return;
                }
            }
            else
            {
                Error_Handler_Control.ErrorSuccessNotifier.AddErrorMessage(
                    "Name mast be Max.150 chars and can not be null or empty string!");
            }
            this.PanelCreate.Visible = false;
            Response.Redirect("EditCategories.aspx");
        }

        protected void ButtonEditSave_Click(object sender, EventArgs e)
        {
            string text = this.TextBoxEdit.Text;
            if (!string.IsNullOrWhiteSpace(text) && !string.IsNullOrEmpty(text) && text.Length < 150)
            {
                if (CatId < 1)
                {
                    Error_Handler_Control.ErrorSuccessNotifier.AddErrorMessage("Wrong Category Id!");
                }
                else
                {
                    LibraryEntities db = new LibraryEntities();
                    Category categiry = db.Categories.Find(CatId);
                    categiry.CategoryName = text;
                    try
                    {
                        db.SaveChanges();
                        Error_Handler_Control.ErrorSuccessNotifier.AddSuccessMessage("Category Updated!");
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
                Error_Handler_Control.ErrorSuccessNotifier.AddErrorMessage("Name mast be Max.150 chars and can not be null or empty string!");
            }

            HidePanels();
            this.CatId = 0;
            Response.Redirect("EditCategories.aspx");
        }

        protected void ButtonCancelEdit_Click(object sender, EventArgs e)
        {
            HidePanels();
            this.CatId = 0;
        }

        protected void ButtonConfirmDelete_Click(object sender, EventArgs e)
        {
            if (CatId < 1)
            {
                Error_Handler_Control.ErrorSuccessNotifier.AddErrorMessage("Wrong Category Id!");
            }
            else
            {
                LibraryEntities db = new LibraryEntities();
                Category categiry = db.Categories.Find(CatId);
                List<Book> children = categiry.Books.ToList();

                try
                {
                    if (children.Count > 0)
                    {
                        for (int i = 0; i < children.Count; i++)
                        {
                            db.Books.Remove(children[i]);
                        }
                        db.SaveChanges();
                    }
                    db.Categories.Remove(categiry);
                    db.SaveChanges();
                    Error_Handler_Control.ErrorSuccessNotifier.AddSuccessMessage("Category Removed!");
                }
                catch (Exception ex)
                {
                    Error_Handler_Control.ErrorSuccessNotifier.AddErrorMessage(ex.Message);
                    return;
                }
            }

            this.PanelDelete.Visible = false;
            this.CatId = 0;
            Response.Redirect("EditCategories.aspx");
        }

        protected void ButtonCancelDelete_Click(object sender, EventArgs e)
        {
            HidePanels();
            this.CatId = 0;
        }

        protected void ButtonShowCreatePanel_Click(object sender, EventArgs e)
        {
            HidePanels();
            this.PanelCreate.Visible = true;
        }

        // The return type can be changed to IEnumerable, however to support
        // paging and sorting, the following parameters must be added:
        //     int maximumRows
        //     int startRowIndex
        //     out int totalRowCount
        //     string sortByExpression
        public IQueryable<Category> GridViewCategories_GetData()
        {
            LibraryEntities db = new LibraryEntities();
            var result = db.Categories;
            return result;
        }

        protected void ButtonEdit_Click(object sender, CommandEventArgs e)
        {
            this.PanelEdit.Visible = true;
            var CategoryId = e.CommandArgument;
            CatId = Convert.ToInt32(CategoryId);
            using (LibraryEntities db = new LibraryEntities())
            {
                Category categiry = db.Categories.Find(CatId);
                this.TextBoxEdit.Text = categiry.CategoryName;
            }
        }

        protected void ButtonDelete_Click(object sender, CommandEventArgs e)
        {
            HidePanels();
            this.PanelDelete.Visible = true;
            var CategoryId = e.CommandArgument;
            CatId = Convert.ToInt32(CategoryId);
            using (LibraryEntities db = new LibraryEntities())
            {
                Category categiry = db.Categories.Find(CatId);
                this.TextBoxDelete.Text = Server.HtmlEncode(categiry.CategoryName);
            }
        }

        protected void HidePanels()
        {
            this.PanelEdit.Visible = false;
            this.PanelCreate.Visible = false;
            this.PanelDelete.Visible = false;
        }

    }
}