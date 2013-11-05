<%@ Page Title="Edit Categories" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" 
    CodeBehind="EditCategories.aspx.cs" Inherits="Library_System.Admin.EditCategories" %>

<asp:Content ID="ContentEditCategories" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="span12">
            <h1>Edit Categories</h1>
        </div>

        <div class="span12">
            <asp:GridView 
                ID="GridViewCategories"
                runat="server" 
                AllowPaging="true" 
                AllowSorting="true" 
                ItemType="Library_System.Models.Category"
                DataKeyNames="CategoryId" 
                SelectMethod="GridViewCategories_GetData"
                AutoGenerateColumns="false" 
                PageSize="5"
                CssClass="gridview" >
                <Columns>
                    <asp:BoundField HeaderText="Category Name" SortExpression="CategoryName" DataField="CategoryName" />
                    <asp:TemplateField HeaderText ="Action">
                        <EditItemTemplate> </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Button ID="ButtonEdit" runat="server" 
                                Text="Edit"
                                OnCommand="ButtonEdit_Click" 
                                CommandArgument="<%#: Item.CategoryId %>" 
                                CssClass="link-button" />
                            <asp:Button ID="ButtonDelete"  runat="server" 
                                Text="Delete" 
                                OnCommand="ButtonDelete_Click" 
                                CommandArgument="<%#: Item.CategoryId %>" 
                                CssClass="link-button" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        
            <div class="create-link">
                <asp:Button ID="ButtonShowCreatePanel" 
                    runat="server" 
                    Text="Create New" 
                    OnClick="ButtonShowCreatePanel_Click" 
                    CssClass="link-button"/>
            </div>
           
            <asp:Panel ID="PanelCreate" runat="server" CssClass="panel" >
                <h2>Create New Category</h2>
                <label>Category: 
                    <asp:TextBox ID="TextBoxCategoryCreate" runat="server" placeholder="Enter category name..." /> 
                </label>
                <asp:Button ID="ButtonCreate" runat="server"
                    Text="Create"  
                    OnClick="ButtonCreate_Click" 
                    CssClass="link-button"/>
                <asp:Button ID="ButtonCancelCreate" runat="server" 
                    Text="Cancel" 
                    OnClick="ButtonCancelCreate_Click" 
                    CssClass="link-button" />
            </asp:Panel>
           
	        <asp:Panel ID="PanelEdit" runat="server" CssClass="panel" >
                <h2>Edit Category</h2>
                <label>Category: 
                    <asp:TextBox ID="TextBoxEdit" runat="server" /> 
                </label>
                <asp:Button ID="ButtonEditSave" runat="server" 
                    Text="Save"  
                    OnClick="ButtonEditSave_Click" 
                    CssClass="link-button"/>
                <asp:Button ID="ButtonCancelEdit" runat="server" 
                    Text="Cancel" 
                    OnClick="ButtonCancelEdit_Click" 
                    CssClass="link-button"/>
            </asp:Panel>
              
	        <asp:Panel ID="PanelDelete" runat="server" CssClass="panel" >
                <h2>Confirm Category Deletion?</h2>
                <label>Category: 
                    <asp:TextBox ID="TextBoxDelete" runat="server" ReadOnly="true"  /> 
                </label>
                <asp:Button ID="ButtonConfirmDelete" runat="server" 
                    Text="Yes" 
                    OnClick="ButtonConfirmDelete_Click" 
                    CssClass="link-button"/>
                <asp:Button ID="ButtonCancelDelete" runat="server" 
                    Text="No" 
                    OnClick="ButtonCancelDelete_Click" 
                    CssClass="link-button"/>
            </asp:Panel>
        </div>
    </div>
    <div class="back-link">
         <a href="Default.aspx">Back to books</a>
    </div>
</asp:Content>
