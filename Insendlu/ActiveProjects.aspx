<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="Site.Master" CodeBehind="ActiveProjects.aspx.cs" Inherits="Insendlu.ActiveProjects" %>

<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit.HtmlEditor" Assembly="AjaxControlToolkit, Version=16.1.1.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <script type="text/javascript">
        $(document).ready(function () {
            $("#viewModal").modal({ backdrop: "static", keyboard: false });
        })
    </script>
    <div class="modal fade" id="viewModal" visible="False" runat="server" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <br>
                    <h4>Hellow There</h4>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-info" data-dismiss="modal">Close</button>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <script type="text/javascript">
        function Confirm() {
            var confirmValue = document.createElement("INPUT");
            confirmValue.type = "hidden";
            confirmValue.name = "confirm_value";

            if (confirm("Are you sure you want to delete the Project ?")) {
                confirmValue.value = "Yes";
            }
            else {
                confirmValue.value = "No";
            }

            document.forms[0].appendChild(confirmValue);
        }
    </script>

    <div class="container">
        <div class="row">
            <div class="col-md-12 col-lg-12">
                <br />
                <h4>Active Projects</h4>
                <br />
                <asp:GridView runat="server" ID="datagridview" CssClass="mydatagrid" AllowPaging="True"
                    CellPadding="4" ForeColor="#333333" GridLines="None" BorderColor="#003300" BorderStyle="Solid"
                    BorderWidth="1px" Font-Size="11pt" PageSize="10" Font-Names="Century" OnPageIndexChanging="datagridview_PageIndexChanging"
                    OnRowCommand="datagridview_RowCommand" AutoGenerateColumns="False" OnRowEditing="datagridview_OnRowEditing" DataKeyNames="id">
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
                        
                        <asp:ButtonField ButtonType="Button" Text="ViewDetails" CommandName="ViewDetails"
                            ControlStyle-BackColor="#507CD1" ControlStyle-ForeColor="White" ControlStyle-Font-Size="15px" />
                        <asp:ButtonField ButtonType="Button" Text="Download" CommandName="Download"
                            ControlStyle-BackColor="#507CD1" ControlStyle-ForeColor="White" ControlStyle-Font-Size="15px" />
                        <asp:ButtonField ButtonType="Button" Text="Edit" CommandName="Edit"
                            ControlStyle-BackColor="#507CD1" ControlStyle-ForeColor="White" ControlStyle-Font-Size="15px" />
                    </Columns>
                </asp:GridView>
                <br />
                <asp:Label runat="server" ID="lblDownload" Visible="False" ForeColor="#507CD1" Font-Size="20px"></asp:Label>
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
        <div class="row">
            <div class="col-md-12 col-lg-12">
            </div>
        </div>
    </div>

</asp:Content>
