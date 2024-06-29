using System;
using System.Data;
using PRDG11EOSV03WB.CommonFunction;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI.DataVisualization.Charting;
using System.Drawing;

namespace PRDG11EOSV03WB
{
    public partial class RedirectPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                LoadCoustomes();
                Redirect();
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessage(objException);
            }
        }
        private void Redirect()
        {
            string stringScreen = "";
            try
            {
                stringScreen = Request.QueryString["name"];
                Response.Redirect(stringScreen, true);
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessage(objException);
            }
        }
        private void LoadCoustomes()
        {
            int interrorcount = 0;
            int intTotalRecord = 0;
            DataSet objDataSet = null;
            string[] stringOutputResult = null;
            string stringServiceType = "RETRIVELOV";
            string stringFormID = "COMMONLOVCUSTOMERS";
            string stringExpression = "";
            string stringOrderBy = "";
            string stringCustomerID = "";
            int intFromRecord = 0;
            string stringcustomer = "";
            int intToRecord = int.MaxValue;
            try
            {
                stringOutputResult = new string[3];
                if (Session["SitemasterCustomerDDL"] != null)
                    stringcustomer = Session["SitemasterCustomerDDL"].ToString();

                stringExpression = "AND INV_CUS_ID IN (" + stringcustomer + ")";


                objDataSet = CommonFunctions.SelectionServiceClient(stringServiceType, stringFormID, stringExpression, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);


                if (interrorcount == 0)
                {
                    if (objDataSet != null && objDataSet.Tables.Count > 0 && objDataSet.Tables[0] != null && objDataSet.Tables[0].Rows.Count > 0)
                    {
                        Session["ssnCustomertable"] = objDataSet.Tables[0];
                        stringCustomerID = objDataSet.Tables[0].Rows[0]["INV_CUS_ID"].ToString();
                        Session["Cus_ID"] = stringCustomerID;
                        Session["ssnFirstCustomer"] = stringCustomerID;
                    }
                    else
                    {
                        
                    }
                }
                else
                {
                    CommonFunctions.ShowMessage(stringOutputResult[0]);

                }
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessage(objException);
            }
            finally
            {
                interrorcount = 0;
                intTotalRecord = 0;
                objDataSet = null;
                stringOutputResult = null;
                stringServiceType = "";
                stringFormID = "";
                stringExpression = "";
                stringOrderBy = "";
                intFromRecord = 0;
                intToRecord = 0;
            }
        }
    }
}