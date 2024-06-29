using PRDG11EOSV03WB.CommonFunction;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace PRDG11EOSV03WB
{
    public partial class WriteBinaryAsFile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string stringScreenName;
            byte[] byteArray;
            string stringFileName;
            try
            {
                if (Session["ssnWriteBinaryAsFile_Page"] != null)
                {
                    stringScreenName = Session["ssnWriteBinaryAsFile_Page"].ToString();

                    if (stringScreenName == "OrderTracking")
                    {
                        if (Session["ssnWriteBinaryAsFile_ByteArray"] != null && Session["ssnWriteBinaryAsFile_FileName"] != null)
                        {
                            byteArray = (byte[])Session["ssnWriteBinaryAsFile_ByteArray"];
                            stringFileName = Session["ssnWriteBinaryAsFile_FileName"].ToString();
                            ExportExcel(byteArray, stringFileName);
                        }
                    }
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
        }

        private void ExportExcel(byte[] bytes, string stringFileName)
        {
            try
            {
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.ClearContent();
                HttpContext.Current.Response.ClearHeaders();
                HttpContext.Current.Response.Charset = "";
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.ContentType = "application/vnd.xls";
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + stringFileName);
                HttpContext.Current.Response.BinaryWrite(bytes);
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.Close();
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
        }
    }
}