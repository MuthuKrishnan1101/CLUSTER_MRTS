<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MaintenancePage.aspx.cs" Inherits="PRDG11EOSV03WB.MaintenancePage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Maintenance Page</title>
    <style>
        .holder {
            width: 580px;
            text-align: center;
            margin: 0 auto;
            padding-top: 120px;
        }
        .tbl {
            font-size: 21px;
        }
        .footer {
            font-weight: bold;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="holder">
            <%--<img src="images/SSW_Logo.png" style="width: 250px;" />--%>
            <h1>G11 CUSTOMER PORTAL</h1>
            <br />
            <h1><span class="tbl">
                The ePortal is currently under-going a scheduled service maintenance to serve you better. All services will be unavailable during this maintenance window.                Please check back later. We apologize for any inconveniences caused.
            </span></h1>
            <br />
            <br />
            <br />
            <br />
        </div>

        <div style="width: 100%; height: auto;" align="center" class="footer">

            <span style="font-size: 12pt; color: #808080">Copyright © 2018-2022 Gurusoft PTE LTD. All rights reserved.</span>
            &nbsp;
           <img alt="" src="images/CompanyLogo.png" width="100" height="17">
        </div>
    </form>
</body>
</html>
