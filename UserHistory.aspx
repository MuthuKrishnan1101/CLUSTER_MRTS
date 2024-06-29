<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserHistory.aspx.cs" Inherits="PRDG11EOSV03WB.UserHistory" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/GridViewPaging.ascx" TagPrefix="paging" TagName="Pager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <style>
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
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="upnl6" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hdnClickEvent" runat="server" />
            <div class="box box-primary" style="border-radius: 32px;">
                <!-- /.box-header -->
                <div class="box-body">
                    <div class="form">
                          <div class="row">
                        <div class="col-sm-5 col-lg-2">
                            <div class="form-group">
                                <label for="inputEmail3" class="control-label mb-1" style="color: black">User ID</label>
                                <asp:TextBox ID="txtID" AutoCompleteType="Disabled" runat="server" autocomplete="off" CssClass="form-control" onkeypress="return enter1_click(event);"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-sm-0">
                        </div>
                        <div class="col-sm-5 col-lg-2">
                            <div class="form-group">
                                <label for="inputEmail3" class="control-label mb-1" style="color: black">Name</label>
                                <asp:TextBox ID="txtFilename" AutoCompleteType="Disabled" runat="server" autocomplete="off" CssClass="form-control" onkeypress="return enter1_click(event);"></asp:TextBox>
                            </div>

                        </div>
                        <div class="col-sm-0">
                        </div>
                        <div class="col-sm-5 col-lg-2">
                            <div class="form-group">
                                <label for="inputEmail3" class="control-label mb-1" style="color: black">TelePhone No.</label>
                                <asp:TextBox ID="txtMNo" runat="server" CssClass="form-control" onkeypress="return enter1_click(event);"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-sm-0">
                        </div>
                        <div class="col-sm-5 col-lg-2">
                            <div class="form-group">
                                <label for="inputEmail3" class="control-label mb-1" style="color: black">E-Mail </label>
                                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" onkeypress="return enter1_click(event);"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-sm-0">
                        </div>
                        <div class="col-sm-5 col-lg-2">
                            <div class="form-group">
                                <label for="inputEmail3" class="control-label mb-1" style="color: black">Status </label>
                                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control input-sm optional">
                                    <asp:ListItem Value="" Text=""></asp:ListItem>
                                    <asp:ListItem Value="ACTIVE" Text="ACTIVE"></asp:ListItem>
                                    <asp:ListItem Value="DISABLED" Text="DISABLED"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        </div>
                        <div class="row">

                             <div class="col-sm-5 col-lg-2">
                            <div class="form-group">
                                <label for="inputEmail3" class="control-label mb-1" style="color: black">Created Date From </label>
                                <asp:TextBox ID="txtFrmDate" AutoCompleteType="Disabled" runat="server" autocomplete="off" CssClass="form-control" onkeypress="return enter1_click(event);"></asp:TextBox>
                                 <cc1:CalendarExtender ID="txtFrmDate_CalendarExtender" TargetControlID="txtFrmDate" runat="server"
                                        Format="dd-MM-yyyy"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-sm-0">
                        </div>
                        <div class="col-sm-5 col-lg-2">
                            <div class="form-group">
                                <label for="inputEmail3" class="control-label mb-1" style="color: black"> Created Date To</label>
                                <asp:TextBox ID="txtToDate" AutoCompleteType="Disabled" runat="server" autocomplete="off" CssClass="form-control" onkeypress="return enter1_click(event);"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtToDate_CalendarExtender" TargetControlID="txtToDate" runat="server"
                                        Format="dd-MM-yyyy"></cc1:CalendarExtender>
                            </div>

                        </div>
                            
                        <div class="col-sm-0">
                        </div>
                        <div class="col-sm-5 col-lg-2">
                            <div class="form-group">
                                <label for="inputEmail3" class="control-label mb-1" style="color: black">Department </label>
                                <asp:DropDownList ID="ddltDepartment" runat="server" CssClass="form-control input-sm optional"></asp:DropDownList>
                            </div>
                        </div>
                            </div>

                        <div class="row">
                            <div class="col-xs-12 col-sm-12 col-md-12 text-right">
                                <asp:LinkButton ID="lbtnExport" runat="server" CssClass="btn btns-search btn-info" Style="font-size: 16px; line-height: 42px; padding: 0px 15px" OnClick="lbtnExport_Click"  OnClientClick="showLoader()"><i class="fa fa-cloud-download" aria-hidden="true"></i> Export</asp:LinkButton>
                                <asp:LinkButton ID="lbtnSearch" runat="server" CssClass="btn btns-search btn-info" Style="font-size: 16px; line-height: 42px; padding: 0px 15px" OnClick="lbtnSearch_Click"><i class="fa fa-search" aria-hidden="true"></i> Search</asp:LinkButton>
                                <asp:LinkButton ID="lbtnClear" runat="server" CssClass="btn btns-save btn-danger" Style="font-size: 16px; line-height: 42px; padding: 0px 15px" OnClick="lbtnClear_Click"><i class="fa fa-times-circle" aria-hidden="true"></i> Clear</asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- /.box-body -->
            </div>

            <div class="box box-primary box-solid" style="border-radius: 33px; border: 1px dashed #00b7d4">
                <div class="box-header with-border" style="border-radius: 60px;">
                    <div class="box-title">
                        <label class="text-right" style="color: white">Total Records:<span id="lblTotalRecords" runat="server"></span></label>
                    </div>
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                    <div class="form">
                        <asp:HiddenField ID="hdnVisiblity" runat="server" />
                        <div class="table-responsive table--no-card m-b-30" id="divid">
                            <asp:GridView ID="gvUserHistory" runat="server" GridLines="Vertical" RowStyle-Wrap="false"
                                AutoGenerateColumns="False"
                                CellPadding="2"
                                ForeColor="#333333"
                                HorizontalAlign="Center" 
                                CssClass="table table-borderless table-striped table-earning">
                                <PagerSettings Mode="NumericFirstLast" PageButtonCount="4" FirstPageText="First" LastPageText="Last" />
                                <PagerStyle CssClass="GridviewPagerStyle gridview" ForeColor="White" HorizontalAlign="Center" />
                                <Columns>

                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkbtnUserID" runat="server" Text="<%#Bind('USER_ID')%>" CommandArgument='<%#Eval("USER_ID")+","+Eval("BE_ID")%>'  
                                                OnClick="lnkbtnUserID_Click"></asp:LinkButton>

                                        </ItemTemplate>
                                        <HeaderTemplate>
                                            <table style="padding: 0px; margin: 0px; width: 100%; background-color: #21BACF; border-collapse: collapse;">
                                                <tr>
                                                    <td style="background-color: #21BACF">
                                                        <asp:LinkButton ID="lnkbtnusrid" runat="server" Font-Underline="false" Font-Bold="true"
                                                            Text="User ID" Font-Names="Arial" Font-Size="12pt" ForeColor="White"
                                                            CommandArgument="USR.USER_ID" OnClick="LnkbtnSort_Click"></asp:LinkButton>
                                                    </td>
                                                </tr>
                                                <tr>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemStyle Width="1%" HorizontalAlign="Left" />
                                    </asp:TemplateField>


                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Label ID="lblUserName" runat="server" Text="<%#Bind('USER_NAME')%>"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderTemplate>
                                            <table style="padding: 0px; margin: 0px; width: 100%; background-color: #21BACF; border-collapse: collapse;">
                                                <tr>
                                                    <td style="background-color: #21BACF">
                                                        <asp:LinkButton ID="lnkbtnHeadershortName9" runat="server" Font-Underline="false" Font-Bold="true"
                                                            Text="User Name" Font-Names="Arial" Font-Size="12pt" ForeColor="White"
                                                            CommandArgument="USR.USER_NAME" OnClick="LnkbtnSort_Click"></asp:LinkButton>
                                                    </td>
                                                </tr>
                                                <tr>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemStyle Width="13%" HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Label ID="lbldept" runat="server" Text="<%#Bind('DEPARTMENTS')%>"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderTemplate>
                                            <table style="padding: 0px; margin: 0px; width: 100%; background-color: #21BACF; border-collapse: collapse;">
                                                <tr>
                                                    <td style="background-color: #21BACF">
                                                        <asp:LinkButton ID="lnkbtndept" runat="server" Font-Underline="false" Font-Bold="true"
                                                            Text="Dapartment" Font-Names="Arial" Font-Size="12pt" ForeColor="White"
                                                            CommandArgument="USR.DEPARTMENTS" OnClick="LnkbtnSort_Click"></asp:LinkButton>
                                                    </td>
                                                </tr>
                                                <tr>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemStyle Width="13%" HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                        <ItemTemplate>

                                            <asp:Label ID="lblmail" runat="server" Text="<%#Bind('USER_EMAIL_ID')%>"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderTemplate>
                                            <table style="padding: 0px; margin: 0px; width: 100%; background-color: #21BACF; border-collapse: collapse;">
                                                <tr>
                                                    <td style="background-color: #21BACF">
                                                        <asp:LinkButton ID="lnkbtnmail" runat="server" Font-Underline="false" Font-Bold="true"
                                                            Text="E-Mail" Font-Names="Arial" Font-Size="12pt" ForeColor="White"
                                                            CommandArgument="USR.USER_EMAIL_ID" OnClick="LnkbtnSort_Click"></asp:LinkButton>
                                                    </td>
                                                </tr>
                                                <tr>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemStyle Width="6%" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                   
                                    <asp:TemplateField>
                                        <ItemTemplate>

                                            <asp:Label ID="lblteleno" runat="server" Text="<%#Bind('USER_TELEPHONE')%>"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderTemplate>
                                            <table style="padding: 0px; margin: 0px; width: 100%; background-color: #21BACF; border-collapse: collapse;">
                                                <tr>
                                                    <td style="background-color: #21BACF">
                                                        <asp:LinkButton ID="lnkbtnteleno" runat="server" Font-Underline="false" Font-Bold="true"
                                                            Text="TelePhone No." Font-Names="Arial" Font-Size="12pt" ForeColor="White"
                                                            CommandArgument="USR.USER_TELEPHONE" OnClick="LnkbtnSort_Click"></asp:LinkButton>
                                                    </td>
                                                </tr>
                                                <tr>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemStyle Width="4%" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>

                                            <asp:Label ID="lblStatus" runat="server" Text="<%#Bind('STATUS_REASON')%>"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderTemplate>
                                            <table style="padding: 0px; margin: 0px; width: 100%; background-color: #21BACF; border-collapse: collapse;">
                                                <tr>
                                                    <td style="background-color: #21BACF">
                                                        <asp:LinkButton ID="lnkbtnpStatusts" runat="server" Font-Underline="false" Font-Bold="true"
                                                            Text="Status" Font-Names="Arial" Font-Size="12pt" ForeColor="White"
                                                            CommandArgument="USR.STATUS_REASON" OnClick="LnkbtnSort_Click"></asp:LinkButton>
                                                    </td>
                                                </tr>
                                                <tr>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemStyle Width="7%" HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                     
                                  <asp:TemplateField>
                                        <ItemTemplate>
                                             <asp:Label ID="lblLOGIN_DATE" runat="server"  Text='<%#Bind("LOGIN_DATE", "{0:dd-MM-yyyy HH:mm:ss}")%>'></asp:Label>
                                                                                       
                                        </ItemTemplate>
                                        <HeaderTemplate>
                                            <table style="padding: 0px; margin: 0px; width: 100%; background-color: #21BACF; border-collapse: collapse;">
                                                <tr>
                                                    <td style="background-color: #21BACF">
                                                        <asp:LinkButton ID="lnkbtnpGIN_DATE" runat="server" Font-Underline="false" Font-Bold="true"
                                                            Text="Last Login Date" Font-Names="Arial" Font-Size="12pt" ForeColor="White"
                                                            CommandArgument="USH.LOGIN_DATE" OnClick="LnkbtnSort_Click"></asp:LinkButton>
                                                    </td>
                                                </tr>
                                                <tr>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemStyle Width="7%"  HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                     
                                    <asp:TemplateField>
                                        <ItemTemplate>

                                            <asp:Label ID="lblNO_OF_DAYS" runat="server" Text="<%#Bind('NO_OF_DAYS')%>"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderTemplate>
                                            <table style="padding: 0px; margin: 0px; width: 100%; background-color: #21BACF; border-collapse: collapse;">
                                                <tr>
                                                    <td style="background-color: #21BACF">
                                                        <asp:LinkButton ID="lnkbtnpNO_OF_DAYS" runat="server" Font-Underline="false" Font-Bold="true"
                                                            Text="No of Days From Last Logon Date" Font-Names="Arial" Font-Size="12pt" ForeColor="White"
                                                            CommandArgument="USH.NO_OF_DAYS" OnClick="LnkbtnSort_Click"></asp:LinkButton>
                                                    </td>
                                                </tr>
                                                <tr>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemStyle  Width="7%" HorizontalAlign="Left" />
                                    </asp:TemplateField> 

                                </Columns>


                                <HeaderStyle CssClass="GridHeaderTextforScroll-UnText" />
                                <RowStyle CssClass="GridviewRowStyle" />
                                <AlternatingRowStyle CssClass="GridviewAlternatingRowStyle" />
                            </asp:GridView>
                        </div>
                        <div class="col-lg-12" hidden>
                            
                        </div>
                        <div class="col-lg-12">
                            <div class="row form-group">
                                <div class="col-sm-12" style="text-align:center">
                                    <asp:Repeater ID="rptPager" runat="server">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkPage" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>'
                                                CssClass='<%# Convert.ToBoolean(Eval("Enabled")) ? "page_enabled" : "page_disabled" %>' OnClick="lnkPage_Click" ></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div>
                        <tr>
                            <td>
                                <br/>
                                 <br/>
                                 <br/>
                                 <br/>
                                 <br/>
                                 <br/>
                                <br/>
                                 <br/>
                                 <br/>
                                 <br/>
                                 <br/>
                                 <br/>
                                <br/>
                                 <br/>
                                 <br/>
                                 <br/>
                                 <br/>
                                 <br/>
                                <br/>
                                 <br/>
                                 <br/>
                                 <br/>
                                 <br/>
                                 <br/>
                                <br/>
                                 <br/>
                                 <br/>
                                 <br/>
                                 <br/>
                                 <br/>
                                <asp:UpdatePanel ID="UpdatePanelModal6" runat="server">
                                    <ContentTemplate>
                                        <asp:LinkButton ID="btnerror" runat="server" />
                                        <cc1:ModalPopupExtender ID="Modelpopuperror" runat="server" BackgroundCssClass="modal-background"
                                            DynamicControlID="btnerror" PopupControlID="pnlpopuperror" DynamicServiceMethod="GetContextKeyGroupHistory" DynamicServicePath="~/WebService.asmx" RepositionMode="RepositionOnWindowResizeAndScroll"
                                            TargetControlID="btnerror" />
                                        <asp:Panel ID="pnlpopuperror" runat="server" BackColor="#FFFFCC" BorderStyle="Outset"
                                            EnableTheming="True" Style="text-align: center; resize: none;" Width="447px">
                                            <center>
                                                <table style="width: 98%; text-align: left">
                                                    <tr>
                                                        <td style="width: 18%"></td>
                                                        <td style="width: 70%; text-align: center; font-family: Arial, Helvetica, sans-serif; font-size: medium; font-weight: bold; color: #000080;">
                                                            <asp:Label ID="lblModalTile5" runat="server"></asp:Label>
                                                        </td>
                                                        <td style="width: 10%"></td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3">
                                                            <table style="width: 100%">
                                                                <tr>
                                                                    <td style="width: 28%; font-family: Arial, Helvetica, sans-serif; font-weight: bold; color: #000080; font-size: 9pt;">
                                                                        <asp:Label ID="lblErrorTypeheader" runat="server" Font-Names="Tahoma" Font-Size="9pt" Text="Error Type:"></asp:Label>

                                                                    </td>
                                                                    <td style="width: 2%; font-family: Arial, Helvetica, sans-serif; font-weight: bold; color: #000080; font-size: 9pt;">:
                                                                    </td>
                                                                    <td style="width: 70%; font-family: tahoma; font-size: 9pt;">
                                                                        <asp:Label ID="lblErrorType" runat="server" Font-Names="Tahoma" Font-Size="9pt"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <asp:Panel ID="PanelValidateFromTo" runat="server">
                                                                <table style="width: 100%">
                                                                    <tr>
                                                                        <td style="width: 28%; font-family: Arial, Helvetica, sans-serif; font-weight: bold; color: #000080; font-size: 9pt;">
                                                                            <asp:Label ID="lblErrorCodeheader" runat="server" Font-Names="Tahoma" Font-Size="9pt" Text="Error Code:"></asp:Label>

                                                                        </td>
                                                                        <td style="width: 2%; font-family: Arial, Helvetica, sans-serif; font-weight: bold; color: #000080; font-size: 9pt;">:
                                                                        </td>
                                                                        <td style="width: 70%; font-family: tahoma; font-size: 9pt;">
                                                                            <asp:Label ID="lblErrorCode" runat="server"></asp:Label>
                                                                        </td>
                                                                    </tr>

                                                                    <tr>
                                                                        <td style="width: 28%; font-family: Arial, Helvetica, sans-serif; font-weight: bold; color: #000080; font-size: 9pt;">
                                                                            <asp:Label ID="lblsysseqnoheader" runat="server" Font-Names="Tahoma" Font-Size="9pt" Text="System Sequence No:"></asp:Label>
                                                                        </td>
                                                                        <td style="width: 2%; font-family: Arial, Helvetica, sans-serif; font-weight: bold; color: #000080; font-size: 9pt;">:
                                                                        </td>
                                                                        <td style="width: 70%; font-family: tahoma; font-size: 9pt;">
                                                                            <asp:Label ID="lblsysseqno" runat="server"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 28%; font-family: Arial, Helvetica, sans-serif; font-weight: bold; color: #000080; font-size: 9pt;">Error Message:</td>
                                                                        <td style="width: 2%; font-family: Arial, Helvetica, sans-serif; font-weight: bold; color: #000080; font-size: 9pt;">:
                                                                        </td>
                                                                        <td style="width: 70%; font-family: tahoma; resize: none; font-size: 9pt;">
                                                                            <asp:TextBox ID="txterrormsg" runat="server" BorderColor="#CCCCCC" BorderStyle="Solid"
                                                                                BorderWidth="1px" Font-Names="Tahoma" Font-Size="9pt" Height="100px" MaxLength="500"
                                                                                TextMode="MultiLine" Width="99%">Message Content Sample....</asp:TextBox>
                                                                        </td>
                                                                    </tr>

                                                                </table>
                                                            </asp:Panel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3" style="text-align: center">
                                                            <asp:Button ID="btnErrorpopupclose" runat="server" align="right" CssClass="SubmitButtonStyle"
                                                                Text="Close" Width="100px" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </center>
                                        </asp:Panel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanelModal6success" runat="server">
                                    <ContentTemplate>
                                        <asp:LinkButton ID="btnerrorsuccess" runat="server" />
                                        <cc1:ModalPopupExtender ID="Modelpopuperrorsuccess" runat="server" BackgroundCssClass="modal-background"
                                            DynamicControlID="btnerrorsuccess" PopupControlID="pnlpopuperrorsuccess" DynamicServiceMethod="GetContextKeyGroupHistory" DynamicServicePath="~/WebService.asmx" RepositionMode="RepositionOnWindowResizeAndScroll"
                                            TargetControlID="btnerrorsuccess" />
                                        <asp:Panel ID="pnlpopuperrorsuccess" runat="server" BackColor="#F0F6F6" BorderStyle="Outset"
                                            EnableTheming="True" Style="text-align: center; resize: none; align-content: flex-start; align-items: flex-start;" Width="447px">
                                            <center>
                                                <table style="width: 98%; text-align: left" bgcolor="#F0F6F6">
                                                    <tr>

                                                        <td style="width: 70%; text-align: left; font-family: Arial, Helvetica, sans-serif; font-size: medium; font-weight: bold; color: #000080;"
                                                            height="30px">
                                                            <asp:Label ID="Label1" runat="server" Text="Info." ForeColor="#212529" Font-Bold="True" Font-Names="Arial" Font-Size="20pt"></asp:Label>
                                                        </td>
                                                        <td style="width: 10%"></td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3" style="height: 96px">
                                                            <table style="width: 100%">
                                                            </table>
                                                            <asp:Panel ID="Panel2" runat="server" Height="67px">
                                                                <table style="width: 100%">

                                                                    <tr>

                                                                        <td style="width: 28%; font-family: Arial, Helvetica, sans-serif; font-weight: lighter; color: #808080; font-size: 12pt;">------------------------------------------------------------------------------</td>
                                                                    </tr>
                                                                    <tr>

                                                                        <td style="width: 28%; font-family: Arial, Helvetica, sans-serif; font-weight: lighter; color: #212529; font-size: 12pt;">
                                                                            <asp:Label ID="Labelsuccesmsg" runat="server" Style="width: 28%; font-family: Arial, Helvetica, sans-serif; font-weight: lighter; color: #212529; font-size: 12pt;"></asp:Label>

                                                                        </td>


                                                                    </tr>
                                                                    <tr>

                                                                        <td style="width: 28%; font-family: Arial, Helvetica, sans-serif; font-weight: lighter; color: #808080; font-size: 12pt;">------------------------------------------------------------------------------</td>
                                                                    </tr>

                                                                </table>
                                                            </asp:Panel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3" style="text-align: center">
                                                            <asp:Button ID="btCloseSuccess" runat="server" align="right" CssClass="SubmitButtonStyle"
                                                                Text="OK" Width="100px" BorderColor="White" BackColor="#DC3545" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </center>
                                        </asp:Panel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </div>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script>
        var xPos, yPos;
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        function BeginRequestHandler(sender, args) {
            if ($get('divid') != null) {
                xPos = $get('divid').scrollLeft;
                yPos = $get('divid').scrollTop;
            }
        }
        function EndRequestHandler(sender, args) {
            if ($get('divid') != null) {
                $get('divid').scrollLeft = xPos;
                $get('divid').scrollTop = yPos;
            }
        }
        prm.add_beginRequest(BeginRequestHandler);
        prm.add_endRequest(EndRequestHandler);
        function enter1_click(event) {
            var keyCode = event.keyCode ? event.keyCode : event.which ? event.which : event.charCode;
            if (keyCode == 13) {
                document.getElementById("<%=lbtnSearch.ClientID%>").click();
                return false;
            }
        }

        setInterval("PageMethods.CallUnexpiredSessionMethod()", 10000);
    </script>
</asp:Content>
