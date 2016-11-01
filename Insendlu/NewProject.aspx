<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewProject.aspx.cs" Inherits="Insendlu.NewProject" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HtmlEditor" TagPrefix="cc1" %>
<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.1.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>New Project</title>
    <!-- Bootstrap -->
    <link href="assets/css/bootstrap.min.css" rel="stylesheet">
    <link href="assets/css/prettify.min.css" rel="stylesheet">
</head>
<body>
    <div class="modal fade" id="projectModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <br>
                    <i class="fa fa-exclamation-triangle fa-7x"></i>
                    <h4 class="semi-bold">Please enter Project Name</h4>
                    <label for="projectName">Project Name</label>
                    <input type="text" runat="server" class="form-control" id="projectName" />
                    <label for="projDescription"></label>
                    <input type="text" runat="server" class="form-control" id="projDescription" />
                    <label for=""></label>
                    </>
                <br>
                </div>

                <div class="modal-footer">
                    <button type="button" class="btn btn-info" data-dismiss="modal">Close</button>
                    <button runat="server" id="projSpec" type="button" class="btn btn-info" data-dismiss="modal">Confirm</button>
                </div>

            </div>
        </div>
    </div>
    <form runat="server">
        <asp:ScriptManager runat="server"></asp:ScriptManager>
        <div class="container">
            <section id="wizard">
                <div class="page-header">
                    <h1>Developing New Project</h1>
                </div>
                <div id="rootwizard">
                    <div class="navbar">
                        <div class="navbar-inner">
                            <div class="container">
                                <ul>
                                    <li><a href="#tab0" data-toggle="tab">Project</a></li>
                                    <li><a href="#tab1" data-toggle="tab">Executive Summary</a></li>
                                    <li><a href="#tab2" data-toggle="tab">Company Background</a></li>
                                   <%-- <li><a href="#tab3" data-toggle="tab">DPW Property</a></li>--%>
                                    <li><a href="#tab4" data-toggle="tab">Proposed Methodology</a></li>
                                    <%--<li><a href="#tab5" data-toggle="tab">Project Team</a></li>--%>
                                   <%-- <li><a href="#tab6" data-toggle="tab">Project References</a></li>--%>
                                    <li><a href="#tab7" data-toggle="tab">Cost Plan</a></li>
                                    <%--<li><a href="#tab8" data-toggle="tab">Why Choose Insedlu</a></li>--%>
                                    <%--<li><a href="#tab9" data-toggle="tab">BEE</a></li>--%>
                                    <li><a href="#tab10" data-toggle="tab">Risk Analysis</a></li>
                                    <%--<li><a href="#tab11" data-toggle="tab">Resource Capabilities</a></li>--%>
                                    <li><a href="#tab12" data-toggle="tab">Conclusion</a></li>
                                </ul>
                            </div>
                        </div>
                    </div>
                    <div id="bar" class="progress">
                        <div class="progress-bar" role="progressbar" aria-valuenow="0" aria-valuemin="0" aria-valuemax="100" style="width: 0%;"></div>
                    </div>
                    <div class="tab-content">
                        <div class="tab-pane" id="tab0">
                            <div class="container">
                                <div class="row">
                                    <div class="col-lg-12">
                                        <label for="projectName">Project Name</label>
                                        <input type="text" runat="server" class="form-control" id="ProjName" />
                                        <label>Project Description</label>
                                        <asp:TextBox runat="server" Height="150" TextMode="MultiLine" CssClass="form-control" ID="descriptionPro"></asp:TextBox>
                                        <%--<label runat="server">Project Team</label>--%>
                                        <br />
                                       <%-- <select runat="server" class="select2 dropdown-caret-right" id="ddlUsers"></select>
                                        <input type="button" id="addUser" value="Add" class="btn btn-primary" />
                                        <br />
                                        <label for="selectedUsers"></label>
                                        <input type="text" id="selectedUsers" class="form-control" runat="server" />--%>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="tab-pane" id="tab1">
                            <cc1:Editor ID="execSummary" Content="" runat="server" Height="250"></cc1:Editor>
                        </div>

                        <div class="tab-pane" id="tab2">
                            <cc1:Editor ID="companyBackground" runat="server" Height="250" TargetControlID="txtEditor"></cc1:Editor>
                        </div>
                        <div class="tab-pane" id="tab3">
                            <cc1:Editor ID="dpw" runat="server" Height="250" TargetControlID="txtEditor"></cc1:Editor>
                        </div>
                        <div class="tab-pane" id="tab4">
                            <cc1:Editor ID="propMethodology" runat="server" Height="250" TargetControlID="txtEditor"></cc1:Editor>
                        </div>
                        <%--<div class="tab-pane" id="tab5">
                <cc1:Editor ID="editor5" runat="server" Height="250" TargetControlID="txtEditor"></cc1:Editor>
            </div>--%>
                       <%-- <div class="tab-pane" id="tab6">
                            <cc1:Editor ID="projReference" runat="server" Height="250" TargetControlID="txtEditor"></cc1:Editor>
                        </div>--%>
                        <div class="tab-pane" id="tab7">
                            <cc1:Editor ID="costPlan" runat="server" Height="250" TargetControlID="txtEditor"></cc1:Editor>
                        </div>
                       <%-- <div class="tab-pane" id="tab8">

                            <cc1:Editor ID="whyChoose" runat="server" Height="250" TargetControlID="txtEditor"></cc1:Editor>
                        </div>--%>
                        <div class="tab-pane" id="tab9">
                            <cc1:Editor ID="blackEmpower" runat="server" Height="250" TargetControlID="txtEditor"></cc1:Editor>
                        </div>
                        <div class="tab-pane" id="tab10">
                            <cc1:Editor ID="riskanalysis" runat="server" Height="250" TargetControlID="txtEditor"></cc1:Editor>
                        </div>
                        <div class="tab-pane" id="tab11">
                            <cc1:Editor ID="resource" runat="server" Height="250" TargetControlID="txtEditor"></cc1:Editor>
                        </div>
                        <div class="tab-pane" id="tab12">
                            <cc1:Editor ID="conclusion" runat="server" Height="250" TargetControlID="txtEditor"></cc1:Editor>
                        </div>
                        <ul class="pager wizard">
                            <li class="previous first" style="display: none;"><a href="#">First</a></li>
                            <li class="previous"><a href="#">Previous</a></li>
                            <li class="next last" style="display: none;"><a href="#">Last</a></li>
                            <li class="next"><a href="#">Next</a></li>
                           <%-- <li class="finish"><a href="javascript:;">Finish</a></li>--%>
                        </ul>
                        <div class="pull-right">
                            <asp:Button CssClass="btn btn-primary" ID="SubmitButton" OnClick="SubmitButton_OnClick" Text="Submit" runat="server"></asp:Button>
                            <%--OnClick="SubmitButton_OnClick"--%>
                        </div>
                    </div>
                </div>
            </section>
            <div id="insendluChat"></div>
        </div>
        <script src="http://code.jquery.com/jquery-latest.js"></script>
        <script src="assets/js/bootstrap.min.js"></script>
        <script src="assets/js/jquery.bootstrap.wizard.js"></script>
        <script src="assets/js/prettify.min.js"></script>

        <script type="text/javascript">
            $("#btnSubmit").hide();
            //alert($("#projectName").val());
            $(document).ready(function () {

                $("#SubmitButton").hide();
                $('#rootwizard').bootstrapWizard({
                    'onNext': function (tab, navigation, index) {
                        console.log(index);
                        switch (index) {
                            case 1:

                                //alert(content);
                                break;
                            case 2:
                                $("#SubmitButton").hide();;
                                break;
                            case 3:
                                break;
                            case 4:
                                break;
                            case 5:
                                $("#SubmitButton").show();
                                break;
                            case 6:
                                break;
                            case 9:
                               
                                //$("#projectModal").modal({ backdrop: "static", keyboard: false });
                                //alert("am here");
                                break;

                            default:
                        }

                    },
                    'onTabShow': function (tab, navigation, index) {
                        console.log(index);
                        var $total = navigation.find('li').length;
                        var $current = index + 1;
                        if (index == 6) {
                            $("#SubmitButton").show();
                        } else {
                            $("#SubmitButton").hide();
                        }
                        var $percent = ($current / $total) * 100;
                        $('#rootwizard .progress-bar').css({ width: $percent + '%' });
                    }
                });
                $('#rootwizard .finish').click(function () {

                    //alert('Finished!, Starting over!');
                    $('#rootwizard').find("a[href*='tab1']").trigger('click');
                });

                window.prettyPrint && window.prettyPrint();
            });
        </script>
    </form>

</body>
</html>
