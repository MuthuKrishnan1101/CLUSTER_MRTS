using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PRDG11EOSV03WB
{
    public partial class MaintenancePage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (ConfigurationManager.AppSettings["MaintenancePageFlag"].ToString().ToUpper() == "N")
                Response.Redirect("Login.aspx", true);
        }
    }
}