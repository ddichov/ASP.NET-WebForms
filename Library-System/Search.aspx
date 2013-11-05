<%@ Page Title="Search Results" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" 
    CodeBehind="Search.aspx.cs" Inherits="Library_System.Search" %>
<asp:Content ID="ContentSearchResults" ContentPlaceHolderID="MainContent" runat="server">

     <h1>Search Results for “<asp:Literal ID="LiteralQuery" runat="server"/>”:</h1>
    <div class="row">
          <ul >
            <asp:Repeater ID="RepeaterSearchResults" runat="server">
                <ItemTemplate>
                    <li>
                        <a href="<%# string.Format("BookDetails.aspx?Id={0}", Eval("BookId")) %>" >
                           <%#: Eval("Title") %> <i> by <%#:Eval("Author") %></i>
                        </a> 
                        (Category: <%#: Eval("CategoryName") %>)
                    </li>
                </ItemTemplate>
            </asp:Repeater>    
          </ul>
      </div>
</asp:Content>
