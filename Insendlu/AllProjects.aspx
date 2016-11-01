<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="Site.Master" CodeBehind="AllProjects.aspx.cs" Inherits="Insendlu.AllProjects" %>

<asp:Content runat="server" ContentPlaceHolderID="MainContent">
     <div class="container">
        <div class="row">
            <div class="col-md-12 col-lg-12">
                <br />
                <h4>All Projects</h4>
                <br/>
                <asp:GridView runat="server" ID="datagridview" CssClass="mydatagrid" AllowPaging="True"
                    CellPadding="4" ForeColor="#333333" GridLines="None" BorderColor="#003300" BorderStyle="Solid"
                    BorderWidth="1px" Font-Size="11pt" Font-Names="Century" OnPageIndexChanging="datagridview_PageIndexChanging"
                    OnRowCommand="datagridview_RowCommand" AutoGenerateColumns="False" DataKeyNames="id">
                    <RowStyle BackColor="#EFF3FB" BorderColor="Black" BorderWidth="1px" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#2461BF" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
                    <AlternatingRowStyle BackColor="White" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
                    <Columns>
                        <asp:TemplateField HeaderText="ID">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblId" Text='<%# Eval("id") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Project Name">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblProjName" Text='<%# Eval("name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Created Date">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblDate" Text='<%# Eval("created_at","{0:D}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Status">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblStatus" Text='<%# Eval("status") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:ButtonField ButtonType="Button"  Text="ViewDetails" CommandName="ViewDetails"
                            ControlStyle-BackColor="#507CD1" ControlStyle-ForeColor="White" ControlStyle-Font-Size="15px" />
                    </Columns>
                </asp:GridView>
                <br />
                <asp:Label runat="server" ID="lblname" ForeColor="#507CD1" Font-Size="20px"></asp:Label>
                <br />
                <asp:Label runat="server" Text="" ID="lbldesc" ForeColor="#507CD1" Font-Size="20px"></asp:Label>
                <br />
                <asp:Label runat="server" Text="" ID="lblstore" ForeColor="#507CD1" Font-Size="20px"></asp:Label>
                <br />
                <asp:Label runat="server" Text="" ID="lblprice" ForeColor="#507CD1" Font-Size="20px"></asp:Label>


            </div>
        </div>
    </div>
</asp:Content>