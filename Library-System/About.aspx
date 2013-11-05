<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" 
    CodeBehind="About.aspx.cs" Inherits="Library_System.About" %>

<asp:Content ID="ContentAbout" ContentPlaceHolderID="MainContent" runat="server">

    <h1>About Library System</h1>

    <h3>1. Library System Data Layer</h3>
    
    <p>
        We have tables for the categories and books (one-to-many). 
        Category name is mandatory, book title, author and category is mandatory. 
        All other fields are optional. 
    </p>
    <p>
   ASP.NET Identity - We have tables for the users / passwords / etc
    </p>
     <p>
   Copy / paste deployment -  
    The application runs with LocalDB and starts correctly without a need of installation / configuration / change. 
    </p>

     <h3>2. Library System Web Forms App</h3>
     <p>
   The project have master page holding the site header + footer + navigation (menu) + login / logout / register links. 
   The ASP.NET Identity system is  configured. Users can register. In case of duplicated username / incorect data, 
         an error message is shown (not a crash screen). After registration, the registered user is stored in the database.
          Users can login. In case of incorect login, an error message is shown (not a crash screen).
    </p>
     <p>
        Authorization (unauthorized access is not permitted). 
         The pages Admin/EditCategories.aspx and Admin/EditBooks canot be accessed annonymously. 
         If there is no logged-in user, these pages redirect to the login form.
    </p>
     <p>
   All categories and the books in each category are shown at the start page of the application. 
         The title and author are shown for each book. HTML escaping is done.
    </p>
     <p>
   View book details - Clicking on a book from the start page displays the book details 
         (title, author, ISBN, web site and description) in a separate page. HTML escaping is done.
    </p>
     <h4> Search</h4>
     <p>
        A search box is available at the start page and clicking on it opens 
         a new page and passes the search arguments to it.
         Search results - matches all the books by the passed search phrase. 
         Matching is done by substring matching (by title or author). 
         Matching is case-sensitive.
         Search results include the book title, author and category. 
         Each book entry has a link to the book details page.
         Search by empty string matches all available books.
         Search results are sorted alphabetically (by title as first criteria and by author as second criteria).
         In case of too long query the search box shows an error (does not crash). HTML escaping is done.
    </p>

    <h4>Administration Area - Edit Categories</h4>
     <p>
   Categories are listed in a table with [Edit] and [Delete] button for each category.
         Categories table is sortable (by category name).
         Categories table is pageable (page size = 5).
    </p>
     <p>
   Create category - When the [Create New] button is clicked the create category form is shown. 
         It displays a new empty category form. 
         The [Create] button creates the new category or shows an error message in case of a problem. 
         The [Cancel] button hides the form and shows back the categories table. 
         Duplicated categories may be allowed or not. 
         The form is on the same page.
    </p>
     <p>
   Edit category - When the [Edit] button is clicked the edit category form is shown. 
         It displays the currently selected category from the categories table. 
         The [Save] button modifies the category in the database or shows an error message in case of a problem. 
         The [Cancel] button hides the form and shows back the categories table. 
    </p>
     <p>
   Delete category - When the [Delete] button is clicked the delete category form is shown. 
         It asks for delete confirmation. The [Yes] button deletes the category from the database or 
         shows an error message in case of a problem. The form is shown on the same page. 
         Deleting a non-empty category deletes all books inside it.
    </p>
     <p>
         HTML special characters are escaped correctly everywhere in the page
    </p>

   <h4>Administration Area - Edit Books</h4> 
     <p>
   Books are listed in a table with [Edit] and [Delete] buttons for each book. 
         For each book display the following columns: Title, Author, ISBN, Web Site, Category.
         Long values (more than 20 characters) in all columns are cut to 20 characters (ending with ...).
         Books table is sortable (by all columns, including by category).
         Books table is pageable (page size = 5, Server-Side Table Paging).
    </p>
     <p> 
    Books Table: N+1 Query Problem - when the books table is shown only one SQL query is executed 
         (that joins the books with the categories).
    </p>
     <p>
    Create book form - When the [Create New] button is clicked the create book form is shown. 
         It displays a new empty book form. The category is choosable through a drop-down list. 
         The [Create] button creates the new book or shows an error message in case of a problem. 
         The [Cancel] button hides the form and shows back the books table. 
         The form is located on the same page.
         Edit book form - When the [Edit] button is clicked the edit book form is shown. 
         It displays the currently selected book from the books table. 
         The category is choosable through a drop-down list. 
         The [Save] button modifies the book in the database or shows an error message in case of a problem. 
         The [Cancel] button hides the form and shows back the books table. 
         The form is on the same page.
         Delete book - When the [Delete] button is clicked the delete book form is shown. 
         It asks for delete confirmation. The [Yes] button deletes the book from the database or shows an error message 
         in case of a problem. The form is shown on the same page.
         HTML special characters are escaped everywhere in the page.
    </p>
</asp:Content>
