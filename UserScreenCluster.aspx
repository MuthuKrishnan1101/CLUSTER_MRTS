<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserScreenCluster.aspx.cs" Inherits="PRDG11EOSV03WB.UserScreenCluster" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/GridViewPaging.ascx" TagPrefix="paging" TagName="Pager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
     <script src="Scripts/Validation.js" type="text/javascript"></script>     
    <link href="js/1.8.9/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function callAlert(msg, page) {
            bootbox.alert({
                message: msg,
                callback: function () {
                    window.location = "" + page + "";
                }
            })
        }

        var Dol = $.noConflict(true);
       
    </script>
    <style type="text/css" media="screen">
           /* commented backslash hack for ie5mac \*/ 
       html, body{height:100%;} 
          /* end hack */
       .CenterPB
       {
          position: absolute;
          left: 50%;
          top: 50%;
          margin-top: -30px; /* make this half your image/element height */
          margin-left: -30px; /* make this half your image/element width */
       }
    </style>
    <style>
        .addCustomerPopup {
            border-radius: 30px;
            width: 40%;
            padding: 20px 0px;
        }
        .td-cellspacing {
            padding: 10px;
            background-color: none;
        }
        .GridHeaderTextforScroll-UnText {
            background: #00b7d4 !important;
            font-size: 17px !important;
            color: #fff !important;
            vertical-align: middle !important;
            font-weight: 400 !important;
            text-transform: capitalize !important;
            line-height: 2.5 !important;
            padding: 22px 40px !important;
            white-space: nowrap !important;
            font-family: "Poppins", sans-serif;
        }
        .modal-panel {
            display: none;
            overflow: auto
        }
        .display-block {
            display: block !important;
            overflow: auto
        }
        .modal-background {
            position: fixed !important;
            z-index: 1000 !important;
            background-color: Gray;
            opacity: .5
        }

        .disabledbutton {
            background-color: #ddd
        }

        .table-earning tbody td.text-right {
            padding-left: 9px !important;
            padding-right: 9px !important
        }

        .btn-group-lg > .btn, .btn-lg {
            padding: 11px 20px;
            font-size: 18px;
            line-height: 1.3333333;
            border-radius: 20px
        }

        .col-sm-0 {
            width: 3.33% !important;
        }

        .paraGraphtext {
            white-space: pre-wrap;
            text-wrap: normal;
            overflow-wrap: break-word;
            word-wrap: break-word;
            word-break: break-all;
        }

        .page_enabled, .page_disabled {
            display: inline-block !important;
            height: 40px !important;
            min-width: 40px !important;
            line-height: 40px !important;
            text-align: center !important;
            text-decoration: none !important;
            border: 1px solid #ccc !important;
            font-size: 16px !important;
        }

        .page_enabled {
            background-color: #565151 !important;
            color: #fff !important;
            border-radius: 20px !important;
        }

        .page_disabled {
            background-color: #17a2b8 !important;
            color: #fff !important;
            border-radius: 20px !important;
        }

        #USERLEGEND
        {
            width:95%;
            margin-left:22px;
        }
    </style>
  
    <style type="text/css">
        .optional{
            background-color:#ddf5ff !important;
        }
        .gridviewHeader
        {
            color:#304863 !Important;
        }
    </style>
    <style>
      legend.group-border {
  width: inherit;
  /* Or auto */
  padding: 0 10px;
  /* To give a bit of padding on the left and right */
  border-bottom: none;
  background-color: #304863;
  border-radius: 10px;
  color: white;
  box-shadow: 1px -3px 3px 0px #ccc2c2;
}
      fieldset.group-border {
  /*border: 0px groove rgba(204, 187, 10, 0.27) !important;*/
  padding: 0 1.4em 1.4em 1.4em !important;
  margin: 0 0 1.5em 0 !important;
  -webkit-box-shadow: 0px 0px 0px 0px #000;
  box-shadow: 0px 0px 0px 0px #000;
  /*background-color: #f5f3de;*/
  border-radius: 10px;
  box-shadow: -4px -2px 4px 0px #d8d0d0;
  border:2px solid lightslategray;
}
    </style>
    <style>
        .tab-content
        {
            /*background-color:#f5f3de;*/
        }
    </style>
    <style>
        .btn-area{
    padding-top:50px;
    padding-bottom:50px;
}
.btns-del{
    font-weight:500;
    text-transform:uppercase;
    color:#fff;
    background-color: #d21111;
    border-radius:5px;
    box-shadow: 0 2px 2px 0 rgba(0,0,0,.14), 0 3px 1px -2px rgba(0,0,0,.2), 0 1px 5px 0 rgba(0,0,0,.12);
}
.btns-new{
    font-weight:500;
    text-transform:uppercase;
    color:#fff;
    background-color: #ff7e40;
    border-radius:5px;
    box-shadow: 0 2px 2px 0 rgba(0,0,0,.14), 0 3px 1px -2px rgba(0,0,0,.2), 0 1px 5px 0 rgba(0,0,0,.12);
}
.btns-search{
    font-weight:500;
    text-transform:uppercase;
    color:#fff;
    background-color: #4bbac5;
    border-radius:5px;
    box-shadow: 0 2px 2px 0 rgba(0,0,0,.14), 0 3px 1px -2px rgba(0,0,0,.2), 0 1px 5px 0 rgba(0,0,0,.12);
}
.btns-save{
    font-weight:500;
    text-transform:uppercase;
    color:#fff;
    background-color: #4ea955;
    border-radius:5px;
    box-shadow: 0 2px 2px 0 rgba(0,0,0,.14), 0 3px 1px -2px rgba(0,0,0,.2), 0 1px 5px 0 rgba(0,0,0,.12);
}

    </style>
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        
            <div class="box box-primary" style="border-radius: 32px; height:200%;">
                <div class="row">
            <br />
             <!-- Ionicons-->
            <div class="col-xs-12 col-sm-12 col-md-12 text-right">
                <asp:LinkButton ID="lbtnNew" runat="server" CssClass="btn btns-new btn-warning" OnClick="lbtnNew_Click" Visible="false"><i class="fa fa-plus-circle" aria-hidden="true"></i> New</asp:LinkButton>
                <asp:LinkButton ID="lnNew" runat="server" CssClass="btn btns-new btn-warning" OnClick="lnNew_Click"><i class="fa fa-plus-circle" aria-hidden="true"></i> New</asp:LinkButton>
                <asp:LinkButton ID="lbtnSave" runat="server" CssClass="btn btns-save btn-success" OnClick="lbtnSave_Click"><i class="fa fa-floppy-o" aria-hidden="true"></i> Save</asp:LinkButton>
                <asp:LinkButton ID="lnkDelete" runat="server" CssClass="btn btns-del btn-danger" OnClick="lnkDelete_Click" Visible="false"><i class="fa fa-trash" aria-hidden="true"></i> Delete</asp:LinkButton>
                 <asp:LinkButton ID="lbtnDelete" runat="server" ></asp:LinkButton>
                <asp:LinkButton ID="lbtnClear" runat="server" CssClass="btn btns-save btn-success" OnClick="lbtnClear_Click"><i class="fa fa-times-circle" aria-hidden="true"></i> Clear</asp:LinkButton>
            </div>
            <br />
            <br />
            <br />

           <!-- Top User DIVISION -->
            <div class="form-horizontal" id="USERLEGEND">
                <!-- USERS MAIN TABLE Starts-->
                <fieldset class="group-border" runat="server" id="ftUserDetail">
                     <legend  class="group-border" style="background-color:#1ca5b8;">User</legend>
                <div class="row">


                     <!-- USERS MAIN TABLE Starts-->
                    <div class="col-xs-12 col-sm-12 col-md-12">

                         <!--ROW.1 Column UserID -->
                         <div class="col-xs-12 col-sm-12 col-md-6">
                            <div class="form-group">
                                <div class="col-sm-4 col-lg-4">
                                    <label class="control-label">User ID</label>
                                </div>
                                <div class="col-sm-8 col-lg-8">
                                    <asp:TextBox ID="txtID" runat="server" style="text-transform:uppercase" CssClass="form-control input-sm upper-case" ></asp:TextBox>
                                </div>
                            </div>
                        </div>
                         <!--ROW.1 Column UserName -->
                         <div class="col-xs-12 col-sm-12 col-md-6">
                            <div class="form-group">
                                <div class="col-sm-4 col-lg-4">
                                    <label class="control-label">User Name</label>
                                </div>
                                <div class="col-sm-8 col-lg-8">
                                    <asp:TextBox ID="txtName" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                </div>
                            </div>
                        </div>   

                         <!--ROW.2 Column Domain -->
                         <div class="col-xs-12 col-sm-12 col-md-6">
                            <div class="form-group">
                                <div class="col-sm-4 col-lg-4">
                                    <label class="control-label">Domain</label>
                                </div>
                                <div class="col-sm-8 col-lg-8">

                                     <asp:DropDownList ID="ddldom" runat="server" CssClass="form-control input-sm ">
                                        <asp:ListItem Value="LOCAL" Text="LOCAL"></asp:ListItem>
                                        <asp:ListItem Value="DOMAIN" Text="DOMAIN"></asp:ListItem>
                                    </asp:DropDownList>

                                    </div>
                            </div>
                        </div>
                         <!--ROW.2 Column Department -->
                         <div class="col-xs-12 col-sm-12 col-md-6">
                            <div class="form-group">
                                <div class="col-sm-4 col-lg-4">
                                    <label class="control-label">Domain Name</label>
                                </div>
                                <div class="col-sm-8 col-lg-8">
                                      <asp:TextBox ID="txtDomainName" runat="server" style="text-transform:uppercase" CssClass="form-control input-sm upper-case"></asp:TextBox>
                                  
                                      </div>
                            </div>
                        </div>   
                        

                         <!--ROW.3 Column NewPassword -->
                         <div class="col-xs-12 col-sm-12 col-md-6">
                            <div class="form-group">
                                <div class="col-sm-4 col-lg-4">
                                    <label class="control-label">New Password</label>
                                </div>
                                <div class="col-sm-8 col-lg-8">
                                    <asp:TextBox ID="txtPwd" runat="server" CssClass="form-control input-sm" TextMode="Password"></asp:TextBox>
                                    <asp:HiddenField id="hfvPwd" runat="server"/>

                                </div>
                            </div>
                        </div>
                         <!--ROW.3 Column Designation -->
                         
                          <div class="col-xs-12 col-sm-12 col-md-6">
                            <div class="form-group">
                                <div class="col-sm-4 col-lg-4">
                                    <label class="control-label">Confirm Password</label>
                                </div>
                                <div class="col-sm-8 col-lg-8">
                                    <asp:TextBox ID="txtCPwd" runat="server" CssClass="form-control input-sm" TextMode="Password"></asp:TextBox>
                                     <asp:HiddenField id="hfvCPwd" runat="server"/>
                                </div>

                            </div>
                        </div>


                         <!--ROW.4 Column ConfirmPassword -->
                         <div class="col-xs-12 col-sm-12 col-md-6">
                            <div class="form-group">                                
                               <div class="col-sm-4 col-lg-4">
                                    <label class="control-label">Department</label>
                                </div>
                                <div class="col-sm-8 col-lg-8"> 
                                     <asp:DropDownList ID="ddltDepartment" runat="server" CssClass="form-control optional">
                                    </asp:DropDownList>
                                      </div>
                            </div>
                        </div>
                                  
                         <!--ROW.4 Column Tel No -->

                           <div class="col-xs-12 col-sm-12 col-md-6">
                            <div class="form-group">
                                <div class="col-sm-4 col-lg-4">
                                    <label class="control-label">Designation</label>
                                </div>
                                <div class="col-sm-8 col-lg-8">
                                      <asp:TextBox ID="txtDesignation" runat="server" CssClass="form-control input-sm optional"></asp:TextBox>
                                  
                                </div>
                            </div>
                        </div>

                        
                         <!--ROW.5 Column E-Mail -->
                        <div class="col-xs-12 col-sm-12 col-md-6">
                            <div class="form-group">
                                 <div class="col-sm-4 col-lg-4">
                                    <label class="control-label">Tel No.</label>
                                </div>
                                <div class="col-sm-8 col-lg-8">
                                    <asp:TextBox ID="txtTelNo" runat="server" CssClass="form-control input-sm optional" MaxLength="15"  onkeypress="return isNumber(event)" OnPaste="JavaScript:return RestrictCopyPaste();"></asp:TextBox>
                                    <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtTelNo"  FilterType="Numbers,Custom" ValidChars="+,-" />
                                
                                </div>
                            </div>
                        </div>


                       
                         <!--ROW.5 Column no. Tel nO. -->
                     
                         
                       
                         <!--ROW.5 Column OrderBy. -->
                         <div class="col-xs-12 col-sm-12 col-md-6">
                            <div class="form-group">
                              <div class="col-sm-4 col-lg-4">
                                    <label class="control-label">E-Mail</label>
                                </div>
                                <div class="col-sm-8 col-lg-8">
                                    <asp:TextBox ID="txtMail" runat="server" CssClass="form-control input-sm optional"></asp:TextBox>
                                                      
                                </div>
                            </div>
                        </div>   

                         <div class="col-xs-12 col-sm-12 col-md-6">
                            <div class="form-group">
                                 <div class="col-sm-4 col-lg-4">
                                    <label class="control-label">Status</label>
                                </div>
                                <div class="col-sm-8 col-lg-8">
                                    
                                      <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control input-sm ">
                                        <asp:ListItem Value="ACTIVE" Text="ACTIVE"></asp:ListItem>
                                        <asp:ListItem Value="DISABLED" Text="DISABLED"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        
                     
                         <!--ROW.6 Column DefaultDeliveryDate. -->
                        <div class="col-xs-12 col-sm-12 col-md-6">
                            <div class="form-group">
                                <div class="col-sm-4 col-lg-4">
                                    <label class="control-label"> </label>
                                </div>
                                <div class="col-sm-8 col-lg-8">
                                  </div>
                            </div>
                        </div>

                        
                        
                         <!--HIDDEN COLUMN -->
                         <div class="col-xs-12 col-sm-12 col-md-6" hidden="hidden">
                            <div class="form-group">
                                <div class="col-sm-4 col-lg-4">
                                    <label class="control-label">Budget Control</label>
                                </div>
                                <div class="col-sm-8 col-lg-8">
                                    <asp:DropDownList ID="ddlBctrl" runat="server" CssClass="form-control input-sm optional">
                                         <asp:ListItem Value="" Text=""></asp:ListItem>
                                        <asp:ListItem Value="COST CENTRE" Text="COST CENTRE"></asp:ListItem>
                                        <asp:ListItem Value="NA" Text="NA"></asp:ListItem>
                                    </asp:DropDownList>
                                   
                                </div>
                            </div>
                        </div>
                         <!--HIDDEN COLUMN -->
                         <div class="col-xs-12 col-sm-12 col-md-6" hidden="hidden">
                            <div class="form-group">
                                <div class="col-sm-4 col-lg-4">
                                    <label class="control-label">Approval Required</label>
                                 </div>
                                <div class="col-sm-8 col-lg-8"  style="margin-top:5px">
                                    <asp:CheckBox id="chkbxAReqd" runat="server"  AutoPostBack="true"/>
                                </div>
                            </div>
                        </div>
                        </div>
                      <!-- USERS MAIN TABLE END-->


                    </div>
                </fieldset>


              </div>

            <!-- BOTTOM User DIVISION -->
            <div  class="form-horizontal" id="USERLEGEND">

                <!-- USERSDETAILS MAIN TABLE Starts-->

                  <fieldset class="group-border" runat="server" id="ftUserRoles" style="height:500%">
                <legend class="group-border" style="background-color:#1ca5b8;">User Details</legend>
                        <asp:HiddenField ID="hfTabID" runat="server" Value="#roles" />
                        <ul class="nav nav-tabs" id="myTabs">

                            <!--  HEADER ROLES,Customers,CostCenter  -->

                            <li class="active"><a href="#roles" onclick="activatetab('#roles');" aria-controls="roles" role="tab" data-toggle="tab">Roles</a></li>                         

                             <li id="lblbussid" runat="server"><a  href="#bussid" data-toggle="tab" style="font-weight: 100; font-size: 20px; border-color: rgba(221, 221, 221, 0.5490196078431373);" onclick="activatetab('#bussid');">Bussiness Entities</a></li>  
                               <li id="litab2" runat="server"><a id="tab2" href="#tab_2" data-toggle="tab" style="font-weight: 100; font-size: 20px; border-color: rgba(221, 221, 221, 0.5490196078431373);" onclick="activatetab('#tab_2');">Application</a></li>
                              
                           </ul>

                         
                        <div class="tab-content">

                               <!--  ROLES -->
                            <div id="roles" class="tab-pane active">
                                <div class="form-horizontal">
                                    <div class="row">
                                        <div class="col-xs-12 col-sm-12 col-md-12">
                                            <asp:CheckBoxList ID="cbRole" runat="server" CssClass="checkbox checkbox-inline" RepeatDirection="Vertical" ></asp:CheckBoxList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                              <!--  Bussinessentities -->
                            <div id="bussid" class="tab-pane">
                                <div class="form-horizontal">
                                    <div class="row">
                                        <div class="col-xs-12 col-sm-12 col-md-12">
                                      <asp:CheckBoxList ID="cbbussinessent" runat="server" CssClass="checkbox checkbox-inline" RepeatDirection="Vertical"  ></asp:CheckBoxList>  
                                        </div>
                                    </div>
                                </div>
                            </div>

                             <!--  Application  -->

                            <div id="tab_2" class="tab-pane">
                               <div class="form-horizontal">
                                    <div class="row">
                                        <div class="col-xs-12 col-sm-12 col-md-12">
                                         <asp:CheckBoxList ID="cbapplicationId" runat="server" CssClass="checkbox checkbox-inline" RepeatDirection="Vertical" ></asp:CheckBoxList>  
                                        </div>
                                    </div>
                                </div>
                               
                            </div>
                            

                            
                    </div>
                     </fieldset>

               
                
             </div>
            </div>
        </div>
   

   
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
            </div>
             

               


  

        <br />
      
              <script src="js/jquery.js" type="text/javascript"></script>
        <script src="js/bootstrap.js" type="text/javascript"></script>
        <script src="js/bootbox.min.js?v=1.0" type="text/javascript">



        </script>
        <script type="text/javascript" src="js/Common.js"></script>
            </div>
         
                
        </ContentTemplate>
    </asp:UpdatePanel>
     <script type="text/javascript">
        var flage = 0;

        function activatetab(tid) {
            document.getElementById("<%=hfTabID.ClientID%>").value = tid;
            SetTab(tid);
            ClickProductTab();
        }

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {

            prm.add_endRequest(function (sender, e) {
                SetTab(document.getElementById("<%=hfTabID.ClientID %>").value);
            });
        }
        function SetTab(tabid) {
            $('#myTabs a[href="' + tabid + '"]').tab('show');

        }

      

        function Clicktab2() {
            document.getElementById('tab2').click();
        }

        function ClickTab1() {
            document.getElementById('tab1').click();
        }

        function ClickTab3() {
            document.getElementById('tab3').click();
        }
               
        function ResetTarget() {
            document.forms[0].target = "_self";
        }       
     

        function SetSession() {

        }
      
     </script>
</asp:Content>
