<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" 
    CodeBehind="BookDetails.aspx.cs" Inherits="Library_System.BookDetails" %>

<asp:Content ID="BookDetailsContent" ContentPlaceHolderID="MainContent" runat="server">

     <asp:Repeater ID="RepeaterBook" runat="server" ItemType="Library_System.Models.Book">
        <ItemTemplate>  
            <header>
                <h1>Book Details</h1>
            
                <p class="book-title"><%#: Item.Title %></p>
                <p class="book-author"><i>by <%#: Item.Author %></i></p>
                <p class="book-isbn"><i>ISBN: <%#: Item.ISBN %></i></p>
                <p class="book-isbn"><i>Web site: 
                    <a href="<%#: Item.WebSite %>"><%#: Item.WebSite %></a></i></p>
            </header>

             <div class="row-fluid">
                <div class="span12 book-description">
                    <p>
                        <%#: Item.Description %>
                    </p>
                </div>
            </div>
         </ItemTemplate>
    </asp:Repeater>

    <div class="back-link">
        <a href="Default.aspx">Back to books</a>
    </div>

</asp:Content>

