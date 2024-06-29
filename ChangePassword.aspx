<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="PRDG11EOSV03WB.ChangePassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Change Password</title>
    <link href="dist/Login%20Vendor/bootstrap.min1.css" rel="stylesheet" />
    <!-- jQuery 3 -->
    <script src="bower_components/jquery/dist/jquery.min.js" type="text/javascript"></script>

    <script src="dist/Login%20Vendor/bootstrap.min1.js"></script>
    <!-- Bootbox-->
    <script src="bower_components/bootstrap/dist/js/Bootbox/bootbox.min.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table width="100%" style="height: 100%">
                <tr style="height: 40px">
                    <td style="width: 5%"></td>
                    <td style="width: 30%; color: #005d63; font-size: 18px">Old Password:</td>
                    <td style="width: 65%">
                        <asp:TextBox ID="txtOldPassword" runat="server" MaxLength="16" TextMode="Password" Width="80%" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" SetFocusOnError="true" ControlToValidate="txtOldPassword" ErrorMessage="Old Password" ValidationGroup="SaveValidate">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr style="height: 40px">
                    <td style="width: 5%"></td>
                    <td style="width: 30%; color: #005d63; font-size: 18px">New Password:</td>
                    <td style="width: 65%">
                        <asp:TextBox ID="txtNewPassword" runat="server" MaxLength="16" TextMode="Password" Width="80%" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" SetFocusOnError="true" ControlToValidate="txtNewPassword" ErrorMessage="New Password" ValidationGroup="SaveValidate">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr style="height: 40px">
                    <td style="width: 5%"></td>
                    <td style="width: 30%; color: #005d63; font-size: 18px">Confirm Password:</td>
                    <td style="width: 65%">
                        <asp:TextBox ID="txtConfirmPassword" runat="server" MaxLength="16" TextMode="Password" Width="80%" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtConfirmPassword" ErrorMessage="Confirm Password" SetFocusOnError="true" ValidationGroup="SaveValidate">*</asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="comparePasswords"
                            runat="server"
                            ControlToCompare="txtNewPassword"
                            ControlToValidate="txtConfirmPassword"
                            ErrorMessage="Your passwords do not match up!"
                            Display="Dynamic" />
                    </td>
                </tr>
                <tr style="height: 50px">

                    <td colspan="3" style="width: 65%; text-align: center;">
                        <asp:Button ID="btnChangePassword" runat="server" Text="Change Password" CssClass="btn btn-primary" OnClick="BtnChangePassword_Click" ValidationGroup="SaveValidate" Width="160px" Height="33px" />
                    </td>
                </tr>
              
            </table>

        </div>
    </form>
    <!-- jQuery 3 -->
    <link href="dist/Login%20Vendor/bootstrap.min1.css" rel="stylesheet" />
    <!-- jQuery 3 -->
    <script src="bower_components/jquery/dist/jquery.min.js" type="text/javascript"></script>

    <script src="dist/Login%20Vendor/bootstrap.min1.js"></script>
    <!-- Bootbox-->
    <script src="bower_components/bootstrap/dist/js/Bootbox/bootbox.min.js" type="text/javascript"></script>
</body>
</html>

