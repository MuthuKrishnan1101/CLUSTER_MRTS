<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="PRDG11EOSV03WB.Login" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <title></title>
    <style>
        .form {
            position: relative;
            z-index: 1;
            background: #FFFFFF;
            max-width: 300px;
            margin: 0 auto 100px;
            padding: 30px;
            border-top-left-radius: 3px;
            border-top-right-radius: 3px;
            border-bottom-left-radius: 3px;
            border-bottom-right-radius: 3px;
            text-align: center;
            box-shadow: 0px 0px 20px 11px rgba(0, 0, 0, 0.24);
        }

            .form .thumbnail {
                background: #ffffff;
                width: 150px;
                height: 150px;
                margin: 0 auto 30px;
                padding: 50px 30px;
                border-top-left-radius: 100%;
                border-top-right-radius: 100%;
                border-bottom-left-radius: 100%;
                border-bottom-right-radius: 100%;
                box-sizing: border-box;
                box-shadow: 7px 2px 3px rgba(28, 187, 157, 0.45);
            }

                .form .thumbnail img {
                    display: block;
                    width: 100%;
                }

            .form input {
                outline: 0;
                background: #f2f2f2;
                width: 100%;
                border: 1px solid #1cbb9d;
                margin: 0 0 15px;
                padding: 15px;
                border-top-left-radius: 3px;
                border-top-right-radius: 3px;
                border-bottom-left-radius: 3px;
                border-bottom-right-radius: 3px;
                box-sizing: border-box;
                font-size: 14px;
            }

            .form button {
                outline: 0;
                background: #005d63 !important;
                width: 100%;
                border: 0;
                padding: 15px;
                border-top-left-radius: 3px;
                border-top-right-radius: 3px;
                border-bottom-left-radius: 3px;
                border-bottom-right-radius: 3px;
                color: #FFFFFF;
                font-size: 14px;
                -webkit-transition: all 0.3 ease;
                transition: all 0.3 ease;
                cursor: pointer;
            }

        .btn-style {
            background-color: #005d63;
            color: white;
        }

        .form .message {
            margin: 15px 0 0;
            color: #b3b3b3;
            font-size: 12px;
        }

            .form .message a {
                color: #EF3B3A;
                text-decoration: none;
            }

        .form .register-form {
            display: none;
        }

        .container {
            position: relative;
            z-index: 1;
            /*max-width: 300px;*/
            margin: 0 auto;
        }

            .container:before, .container:after {
                content: "";
                display: block;
                clear: both;
            }

            .container .info {
                margin: 50px auto;
                text-align: center;
            }

                .container .info h1 {
                    margin: 0 0 15px;
                    padding: 0;
                    font-size: 36px;
                    font-weight: 300;
                    color: #005d63;
                }

                .container .info span {
                    color: #4d4d4d;
                    font-size: 12px;
                }

                    .container .info span a {
                        color: #000000;
                        text-decoration: none;
                    }

                    .container .info span .fa {
                        color: #EF3B3A;
                    }

        body {
            background: #eaeaea;
            font-family: "Roboto", sans-serif;
            -webkit-font-smoothing: antialiased;
            -moz-osx-font-smoothing: grayscale;
        }

            body:before {
                position: fixed;
                top: 0;
                left: 0;
                display: block;
                background: #eaeaea;
                width: 100%;
                height: 100%;
            }
    </style>
    <style>
        .buttonnew {
            background-color: red;
        }

        .auto-style2 {
            width: 60%;
            height: 30px;
        }

        .auto-style10 {
            height: 175px;
        }

        .auto-style11 {
            width: 70%;
            height: 70px;
        }

        .auto-style12 {
            width: 100%;
        }

        .auto-style13 {
            width: 70%;
            height: 23px;
        }

        .p {
            font-size: xx-small;
            font-weight: bold;
        }
    </style>
</head>
<body style="background-image: url(images/JCSHeartsMonogram.jpg);">
    <link href="CSS/gridviewstyle.css" rel="stylesheet" type="text/css" />
    <div class="container">
        <div class="info">
            <h1 style="font-size: 25px; font-weight: bold;">ADMIN PORTAL</h1>
        </div>
    </div>
    <div class="form">
        <div class="thumbnail">
            <img src="images/GurusoftLogo.gif" /></div>
        <form class="register-form">

            <input type="text" placeholder="name" />
            <input type="password" placeholder="password" />
            <input type="text" placeholder="email address" />
            <button>create</button>
            <p class="message">Already registered? <a href="#">Sign In</a></p>
        </form>
        <form class="login-form" runat="server">
            <asp:ScriptManager ID="ScriptManager12" runat="server" EnablePageMethods="True">
            </asp:ScriptManager>
            <asp:TextBox ID="txtUserName" runat="server" Style="text-transform: uppercase" CssClass="form-control input-sm newborder upper-case" AutoCompleteType="Disabled" placeholder="USER ID" onblur="UpperCase(this);" onkeypress="return login(event);"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvUID" runat="server" ControlToValidate="txtUserName" Display="Dynamic" ValidationGroup="vgLogin" ForeColor="Red" SetFocusOnError="true" ErrorMessage="Please Enter User ID"></asp:RequiredFieldValidator>

            <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control input-sm newborder" TextMode="Password" placeholder="PASSWORD" onkeypress="return login(event);"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvPwd" runat="server" ControlToValidate="txtPassword" Display="Dynamic" ValidationGroup="vgLogin" ForeColor="Red" SetFocusOnError="true" ErrorMessage="Please Enter Password"></asp:RequiredFieldValidator>
            <%--<asp:Button ID="btnLogin" runat="server" CssClass="form button" Text="LOGIN" BackColor="#1cbb9d" OnClick="btnLogin_Click" ForeColor="White" ValidationGroup="vgLogin" style="left: 0px; top: 0px" />--%>
            <h4>
                <asp:Label ID="lblErrStatus" runat="server" CssClass="ErrorLabel" ForeColor="Red" Font-Size="14px"></asp:Label></h4>
            <%-- <br />--%>
            <br />
            <div>
                <asp:Button ID="btnLogin" runat="server" CssClass="submit" Text="LOGIN" BackColor="#1cbb9d" OnClick="btnLogin_Click" ForeColor="White" Style="left: 0px; top: 0px" ValidationGroup="vgLogin" />
                <a href="javascript:void(0);" onclick="forgot_click();" class="forgot-password">Forgot Password?</a>
                <br />
                <br />
                <asp:LinkButton ID="lbtnForgot" runat="server" OnClick="lbtnForgot_Click"></asp:LinkButton>
                <asp:LinkButton ID="btnReset" runat="server" OnClick="btnReset_Click"></asp:LinkButton>

                <br />

            </div>
            <div>

                <table width="100%" cellpadding="0" cellspacing="2" align="center" style="background-image: url('Images/TableBackground.png'); background-repeat: no-repeat;">
                    <tr>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanelModal6" runat="server" Visible="false">
                                <ContentTemplate>
                                    <asp:LinkButton ID="btnerror" runat="server" />
                                    <cc1:ModalPopupExtender ID="Modelpopuperror" runat="server" BackgroundCssClass="modal-background"
                                        DynamicControlID="btnerror" PopupControlID="pnlpopuperror" DynamicServiceMethod="GetContextKeyGroupHistory" DynamicServicePath="~/WebService.asmx" RepositionMode="RepositionOnWindowResizeAndScroll"
                                        TargetControlID="btnerror" />
                                    <asp:Panel ID="pnlpopuperror" runat="server" BackColor="#EAFFFF" BorderStyle="Outset"
                                        EnableTheming="True" Style="text-align: center; resize: none;" Width="790px">
                                        <center>
                                            <table style="width: 100%; text-align: center">
                                                <tr style="background-color: #7BEFA8; height: 60%; font-family: Arial; font-size: 16px; color: #000000">

                                                    <td style="width: 80%; height: 50%; text-align: center; font-family: Arial, Helvetica, sans-serif; font-size: medium; font-weight: bold; color: #000080;"
                                                        class="auto-style2">
                                                        <asp:Label ID="lblModalTile5" runat="server" Text="Business Unit"></asp:Label>
                                                    </td>

                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table width="100%" align="center">
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblheader" Style="font-weight: bold; color: black; font-size: 18px" runat="server"></asp:Label>
                                                                </td>

                                                            </tr>
                                                            <tr>
                                                                <td>&nbsp;</td>
                                                            </tr>
                                                        </table>
                                                        <asp:Panel ID="PanelValidateFromTo" runat="server" Style="height: 418px; overflow: auto">
                                                            <table style="border: thin ridge #2aa7ed; width: 85%;" align="center">
                                                                <tr>
                                                                    <td style="text-align: center; background-color: #e3f6fd" class="nav-justified">
                                                                        <asp:GridView ID="gvOrderUpload" runat="server" GridLines="Vertical" RowStyle-Wrap="false" OnRowDataBound="gvOrderUpload_RowDataBound"
                                                                            AutoGenerateColumns="False"
                                                                            CellPadding="2"
                                                                            ForeColor="#333333"
                                                                            HorizontalAlign="Center"
                                                                            CssClass="table table-borderless  table-earning">
                                                                            <PagerSettings Mode="NumericFirstLast" PageButtonCount="4" FirstPageText="First" LastPageText="Last" />
                                                                            <PagerStyle CssClass="GridviewPagerStyle gridview" ForeColor="White" HorizontalAlign="Center" />
                                                                            <Columns>



                                                                                <asp:TemplateField>
                                                                                    <ItemTemplate>
                                                                                        <asp:RadioButton ID="RowSelector" runat="server" onclick="checkRadioBtn(this);" />
                                                                                        <asp:HiddenField ID="hfIDs" runat="server" />
                                                                                    </ItemTemplate>

                                                                                    <HeaderStyle Width="2%" HorizontalAlign="Left" />
                                                                                    <ItemStyle Width="2%" HorizontalAlign="Left" />
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField>
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblBEID" runat="server" Text="<%#Bind('BE_ID')%>"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <HeaderTemplate>
                                                                                        <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4;">
                                                                                            <tr>
                                                                                                <td class="gridtabletd" width="50px">
                                                                                                    <asp:Label ID="lnkbtnMStatus" runat="server" Font-Bold="true" Font-Size="16px" ForeColor="White" Text="ID"></asp:Label>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td style="background-position: bottom; background-repeat: repeat-x; background-image: url('Images/GridviewHeader.png'); line-height: 11px; vertical-align: bottom;"></td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </HeaderTemplate>
                                                                                    <ItemStyle Width="10%" HorizontalAlign="Left" />
                                                                                </asp:TemplateField>




                                                                                <asp:TemplateField>
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblProductID" runat="server" Text="<%#Bind('reference_2')%>"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <HeaderTemplate>
                                                                                        <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4 !important; border-collapse: collapse;">
                                                                                            <tr>
                                                                                                <td class="gridtabletd" width="50px">
                                                                                                    <asp:Label ID="lnkbtnMStatus" runat="server" Font-Bold="true" Font-Size="16px" ForeColor="White" Text="Name"></asp:Label>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td style="background-position: bottom; background-repeat: repeat-x; background-image: url('Images/GridviewHeader.png'); line-height: 11px; vertical-align: bottom;"></td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </HeaderTemplate>
                                                                                    <ItemStyle Width="50%" HorizontalAlign="Left" />
                                                                                </asp:TemplateField>

                                                                            </Columns>
                                                                            <HeaderStyle CssClass="GridHeaderTextforScroll-UnText" />
                                                                            <RowStyle CssClass="GridviewRowStyle" />
                                                                            <AlternatingRowStyle CssClass="GridviewAlternatingRowStyle alternativeFontColor" />
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                                <tr style="background-color: #00FFFF; font-family: Arial; font-size: 16px; color: #000000">
                                                    <td style="text-align: center; font-family: Arial, Helvetica, sans-serif; font-size: medium; font-weight: bold; color: #000080; background-color: #EAFFFF"
                                                        class="auto-style13"></td>

                                                </tr>
                                                <tr style="background-color: #7BEFA8; font-family: Arial; font-size: 16px; color: #000000">
                                                    <td style="text-align: center; font-family: Arial, Helvetica, sans-serif; font-size: medium; font-weight: bold; color: #C6ECD9; background-color: #EAFFFF;"
                                                        class="auto-style11">

                                                        <asp:Button ID="btnok" runat="server" align="center" OnClick="btnok_Click"
                                                            Text="OK" Width="100px" Font-Bold="True" BorderColor="#00CC66" ForeColor="White" BackColor="#0066FF" Height="45px" />&nbsp;
                                                                     <asp:Button ID="btnclose" runat="server" align="center" OnClick="btnclose_Click"
                                                                         Text="Close" Width="100px" Font-Bold="True" BorderColor="#00CC66" ForeColor="Black" BackColor="#CC3300" Height="47px" />
                                                    </td>
                                                </tr>
                                                <tr style="background-color: #7BEFA8; font-family: Arial; font-size: 16px; color: #000000">
                                                    <td style="width: 70%; text-align: center; font-family: Arial, Helvetica, sans-serif; font-size: medium; font-weight: bold; color: #000080; background-color: #EAFFFF;">
                                                        <asp:Label ID="lblcheckboxvalidate" runat="server" CssClass="ErrorLabel" ForeColor="Red" Font-Size="14px" Text="Please select at least one ID to add." Visible="false"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </center>
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </div>
        </form>
    </div>

    <script>
        function UpperCase(txt) {
            txt.value = txt.value.toUpperCase();
        }
        function login(event) {
            var keyCode = event.keyCode ? event.keyCode : event.which ? event.which : event.charCode;
            if (keyCode == 13) {
                document.getElementById("<%=btnLogin.ClientID%>").click();
                return false;
            }
        }

        function checkRadioBtn(id) {
            var gv = document.getElementById('<%=gvOrderUpload.ClientID %>');

            for (var i = 1; i < gvOrderUpload.rows.length; i++) {
                var radioBtn = gvOrderUpload.rows[i].cells[0].getElementsByTagName("input");

                // Check if the id not same
                if (radioBtn[0].id != id.id) {
                    radioBtn[0].checked = false;
                }
            }
        }

        function forgot_click() {

            if (document.getElementById("<%=txtUserName.ClientID%>").value.trim() == "") {
                bootbox.alert("Please enter your USERNAME");
            }
            else {
                document.getElementById("<%=lbtnForgot.ClientID%>").click();

            }
        }
    </script>


</body>
</html>
