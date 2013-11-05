<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" 
    CodeBehind="Default.aspx.cs" Inherits="Library_System._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="span1">
            <h1>Books</h1>
        </div>
        <div class="search-button text-right">
            <div class="form-search">
                <div class="input-append">
                    <asp:TextBox ID="q" runat="server" 
                        placeholder="Search by book title / author..." 
                        class="span3 search-query"/>
                    <asp:Button Text="Search" runat="server" 
                        OnClick="Search_Click" class="btn"/>
                </div>
            </div>
        </div>
    </div>

    <div class="row">
        <asp:Repeater ID="RepeaterMain" runat="server" ItemType="Library_System.Models.Book">
            <ItemTemplate> 
                <div class="span4">
                    <h2><%#: Item.Category.CategoryName %></h2>
                    <ul >
                        <ItemTemplate>
                            <li>
                                <a href="<%# string.Format("BookDetails.aspx?Id={0}", Item.BookId) %>" > 
                                    <%#: Item.Title %> <i> by <%# Item.Author %> </i>
                                </a>
                            </li>
                        </ItemTemplate>
                    </ul>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
    <br />
</asp:Content>
