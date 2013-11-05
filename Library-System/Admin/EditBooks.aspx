<%@ Page Title="Edit Books" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" 
    CodeBehind="EditBooks.aspx.cs" Inherits="Library_System.Admin.EditBooks" %>

<asp:Content ID="ContentEditBooks" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="span12">
            <h1>Edit Books</h1>
        </div>
        <div class="span12">
            <asp:GridView  ID="GridViewBooks" runat="server" 
                AllowPaging="true" 
                AllowSorting="true" 
                
                DataKeyNames="BookId" 
                SelectMethod="GridViewBooks_GetData"
                AutoGenerateColumns="false" 
                PageSize="5"
                CssClass="gridview" >
                <Columns>
                    <asp:TemplateField HeaderText="Title"  SortExpression="Title">
                        <ItemTemplate>
                            <%#: TrimString((string)Eval("Title"))%>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Author"  SortExpression="Author">
                        <ItemTemplate>
                            <%#: TrimString((string)Eval("Author"))%>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField HeaderText="ISBN" SortExpression="ISBN" DataField="ISBN" />

                    <asp:TemplateField HeaderText="Web Site"  SortExpression="WebSite">
                        <ItemTemplate>
                            <asp:HyperLink NavigateUrl='<%#: Eval("WebSite") %>' 
                                Text='<%#: TrimString((string)Eval("WebSite")) %>' 
                                runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>

                     <asp:TemplateField HeaderText="Category"  SortExpression="CategoryName">
                        <ItemTemplate>
                            <%#: TrimString((string)Eval("CategoryName"))%>
                        </ItemTemplate>
                    </asp:TemplateField>
         
                    <asp:TemplateField HeaderText ="Action">
                        <EditItemTemplate> </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Button ID="ButtonEditBook" runat="server" 
                                Text="Edit" 
                                OnCommand="ButtonEditBook_Command" 
                                CommandArgument='<%# Eval("BookId") %>'
                                CssClass="link-button" />
                            <asp:Button ID="ButtonDeleteBook" runat="server" 
                                Text="Delete" 
                                OnCommand="ButtonDeleteBook_Command" 
                                CommandArgument='<%# Eval("BookId") %>'
                                CssClass="link-button" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
       
            <div class="create-link">
                <asp:Button ID="ButtonShowCreatePanel" runat="server" 
                    Text="Create New" 
                    OnClick="ButtonShowCreatePanel_Click" 
                    CssClass="link-button" />
            </div>

            <asp:Panel ID="PanelCreate" runat="server" CssClass="panel" >
	            <h2>Create New Book</h2>
                <label><span>Title:</span>
                     <asp:TextBox ID="TextBoxBookTitleCreate" runat="server" placeholder="Enter book title ..."  /> 
                </label>
                <label><span>Author(s):</span>
                     <asp:TextBox ID="TextBoxAuthorCreate" runat="server" placeholder="Enter book author / authors ..." /> 
                </label>
                <label><span>ISBN:</span>
                     <asp:TextBox ID="TextBoxISBNCreate" runat="server" placeholder="Enter book ISBN ..." /> 
                </label>
                <label><span>Web site:</span>
                     <asp:TextBox ID="TextBoxWebSiteCreate" runat="server" placeholder="Enter book web site ..." /> 
                </label>
                <label><span>Description:</span>
                   <%-- textarea? --%>
                     <asp:TextBox ID="TextBoxDescriptionCreate" runat="server" placeholder="Enter book description ..."  /> 
                </label>
                <label><span>Category:</span>
                     <asp:DropDownList ID="DropDownCategoriesCreate" Runat="server" 
                        ItemType="Library_System.Models.Category" 
                        SelectMethod="DropDownCategoriesCreate_GetData" 
                        DataTextField="CategoryName" 
                        DataValueField="CategoryId" >
                    </asp:DropDownList> 
                </label>
                <asp:Button ID="ButtonCreate" Text="Create" CssClass="link-button" runat="server" OnClick="ButtonCreate_Click" />
                <asp:Button ID="ButtonCancelCreate" Text="Cancel" CssClass="link-button" runat="server" OnClick="ButtonCancelCreate_Click"/>
            </asp:Panel>
        
	        <asp:Panel ID="PanelDelete" runat="server" CssClass="panel" >
                <h2>Confirm Book Deletion?</h2>
                <label>Title:  
                    <asp:TextBox ID="TextBoxDelete" runat="server" ReadOnly="true"  /> 
                </label>
                <asp:Button ID="ButtonConfirmDelete" runat="server" Text="Yes" OnClick="ButtonConfirmDelete_Click" CssClass="link-button"/>
                <asp:Button ID="ButtonCancelDelete" runat="server" Text="No" OnClick="ButtonCancelDelete_Click"  CssClass="link-button"/>
            </asp:Panel>
    
	        <asp:Panel ID="PanelEdit" runat="server" CssClass="panel" >
	            <h2>Edit Book</h2>

               <label><span>Title:</span>
                     <asp:TextBox ID="TextBoxTitleEdit" runat="server" placeholder="Enter book title ..."  /> 
                </label>
                <label><span>Author(s):</span>
                     <asp:TextBox ID="TextBoxAuthorEdit" runat="server" placeholder="Enter book author / authors ..." /> 
                </label>
                <label><span>ISBN:</span>
                     <asp:TextBox ID="TextBoxISBNEdit" runat="server" placeholder="Enter book ISBN ..." /> 
                </label>
                <label><span>Web site:</span>
                     <asp:TextBox ID="TextBoxWebSiteEdit" runat="server" placeholder="Enter book web site ..." /> 
                </label>
                <label><span>Description:</span>
                   <%-- textarea? --%>
                     <asp:TextBox ID="TextBoxDescriptionEdit" runat="server" placeholder="Enter book description ..."  /> 
                </label>
                <label><span>Category:</span>
                     <asp:DropDownList ID="DropDownListCategoryEdit" Runat="server" 
                         ItemType="Library_System.Models.Category"
                         SelectMethod="DropDownListCategoryEdit_GetData" 
                         DataTextField="CategoryName" 
                         DataValueField="CategoryId"  >
                     </asp:DropDownList> 
                </label>
                <asp:Button ID="ButtonEditSave" runat="server" Text="Save" OnClick="ButtonEditSave_Click" 
                    CssClass="link-button" />
                <asp:Button ID="ButtonCancelEdit" runat="server" Text="Cancel" OnClick="ButtonCancelEdit_Click" 
                    CssClass="link-button" />
            </asp:Panel>
        </div>
    </div>
    <div class="back-link">
         <a href="Default.aspx">Back to books</a>
    </div>
</asp:Content>
