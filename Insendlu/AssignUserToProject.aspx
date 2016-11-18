<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AssignUserToProject.aspx.cs" Inherits="Insendlu.AssignUserToProject" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container">
        <div class="row">
            <div class="col-md-12">
                <br/>
                <br/>

                <asp:DropDownList runat="server" ID="projectList" ToolTip="Select Project to assign" CssClass="dropdown" />
                
                <asp:DropDownList runat="server" ID="selectUsers" DataTextField="name" DataValueField="id" CssClass="dropdown" />
             
              
                <asp:Button runat="server" ID="btnChosen" Text="ADD" CssClass="btn btn-primary" OnClick="btnChosen_OnClick"/><br/>
                    <asp:TextBox runat="server" ID="txtUsers" ReadOnly="True"></asp:TextBox>
                <br/>
              <br/>
                <asp:Button runat="server" Text="Assign" ID="Assign" CssClass="btn btn-success" OnClick="Assign_OnClick" />
            </div>
        </div>

    </div>

</asp:Content>
