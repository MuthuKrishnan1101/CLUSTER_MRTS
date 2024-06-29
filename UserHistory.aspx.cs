using PRDG11EOSV03WB.CommonFunction;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace PRDG11EOSV03WB
{
    public partial class UserHistory : System.Web.UI.Page
    {
        public int recFrom = 0;
        public int recTo = 0;
        public int pageIndex = 0;
        public string stringformIdPaging = "userhistoryPopupPaging";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (CommonFunctions.IsActive())
            {
                try
                {
                    HtmlGenericControl home = (HtmlGenericControl)this.Page.Master.FindControl("hdrPageTitle");
                    home.InnerText = "User Summary";
                     
                    recTo = CommonFunctions.GridViewPagesize(stringformIdPaging);

                    if (!IsPostBack)
                    {
                        txtID.Text = "";
                        txtFilename.Text = "";
                        txtID.Attributes.Add("autocomplete", "off");
                        txtFilename.Attributes.Add("autocomplete", "off");
                        Session["exportcondition"] = null;
                        InitializeValues();
                        loaddata();
                        LoadDepartment();
                        Session["stringtransid"] = null;
                        Session["orderprocess"] = null;
                    }
                }
                catch (Exception objException)
                {
                    CommonFunctions.ShowMessageboot02(objException);
                }
            }


        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (hdnClickEvent.Value == "true")
            {
                LoadRecord(PrepareSearchExpression());
            }
        }

        #region Gridview
        private void InitializeValues()
        {
            try
            {
                ViewState["vsSortDirection"] = " ASC";
                ViewState["vsSortExpression"] = "";
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
        }

        #endregion

        private void loaddata()
        {
            //string stringexp = "And BATCH_SEQ= '" + Session["stringtransid"].ToString() + "'";
            // LoadRecord("", "BECONG_NAME asc");
            LoadRecord(" and USR.STATUS_REASON = 'ACTIVE' ", "USR.USER_ID asc");
        }
         
        private void LoadRecord(string Condition = null, string SortExpression = null, int? RecordFrom = null, int? RecordTo = null)
        {
            DataSet objDataSet = null;
            try
            {
                ViewState["vsSearchCondition"] = Condition;
                int intRecordCount = 0;
                objDataSet = GetRecords(out intRecordCount, Condition, SortExpression, RecordFrom, RecordTo);
                if (objDataSet != null && objDataSet.Tables.Count > 0 && objDataSet.Tables[0] != null && objDataSet.Tables[0].Rows.Count > 0)
                {
                    lblTotalRecords.InnerText = intRecordCount.ToString();
                    gvUserHistory.DataSource = objDataSet.Tables["t1"];
                    gvUserHistory.DataBind();
                    Session["excelprofile"] = objDataSet.Tables["t1"]; 
                }

            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
        }

        public DataSet GetRecords(out int intRecordCount, string Condition = null, string SortExpression = null, int? RecordFrom = null, int? RecordTo = null)
        {

            DataSet objDatasetResult = null;
            int interrorcount = 0;
            string[] stringOutputResult = null;
            string stringformid = "FAD1011R1V3";
            intRecordCount = 0;
            string stringBeID = "";
            string stringuserid = "";
            string stringServiceType = "List1R1v1AdminServiceClient";

            string stringbeid = CommonFunctions.GETBussinessEntity();
            try
            {
                stringBeID = Session["BusinessID"].ToString();
                int intRecordFrom = recFrom;
                int intRecordTo = recTo; 
                ViewState["vsSearchCondition"] = Condition;
                if (Session["G11EOSUser_ID"] != null)
                {
                    stringuserid = Session["G11EOSUser_ID"].ToString();
                }
                Condition += "and u1.USER_ID = '" + stringuserid + "'"; 
                Session["exportcondition"] = Condition;
                objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, Condition, SortExpression, intRecordFrom, intRecordTo, out intRecordCount, out interrorcount, out stringOutputResult);
                 PopulatePager(intRecordCount, pageIndex);

                if (interrorcount == 0)
                {
                    if (objDatasetResult != null && objDatasetResult.Tables.Count > 0 && objDatasetResult.Tables[0] != null && objDatasetResult.Tables[0].Rows.Count > 0)
                    {

                        return (objDatasetResult != null && objDatasetResult.Tables.Count > 0 && objDatasetResult.Tables[0].Rows.Count > 0) ? objDatasetResult : null;

                    }
                    else
                    {
                        string stringmessage = "No Records Found. ";
                        CommonFunctions.ShowMessageboot(this, stringmessage);
                        lblTotalRecords.InnerText = intRecordCount.ToString();
                        gvUserHistory.DataSource = objDatasetResult;
                        gvUserHistory.DataBind(); 
                    }
                }
                else
                {
                    Errorpopup(stringOutputResult);
                }

            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }

            return null;
        }

        protected void LnkbtnSort_Click(object sender, EventArgs e)
        {
            DataSet objDataSetSort1 = null;
            objDataSetSort1 = new DataSet();
            try
            {
                if (sender != null)
                {
                    string stringColumnName = ((LinkButton)sender).CommandArgument;
                    if (stringColumnName != null && stringColumnName.Trim().Length > 0)
                    {

                        ViewState["vsSortDirection"] = (ViewState["vsSortDirection"] != null && ViewState["vsSortDirection"].ToString().Trim() == "ASC") ? " DESC" : " ASC";
                        ViewState["vsSortExpression"] = stringColumnName + ViewState["vsSortDirection"].ToString();
                        LoadRecord((string)ViewState["vsSearchCondition"], ViewState["vsSortExpression"].ToString());
                    }
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }

        }
        private void Errorpopup(string[] stringOutputResult)
        {
            try
            {
                lblModalTile5.Text = "Error Message Summary";
                lblErrorType.Text = stringOutputResult[1];
                lblErrorCode.Text = stringOutputResult[0];
                lblsysseqno.Text = stringOutputResult[3];
                txterrormsg.Text = stringOutputResult[2];
                Modelpopuperror.Show();
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
        }

        protected void lnkbtnUserID_Click(object sender, EventArgs e)
        {
            try
            {
                if (sender != null)
                {
                    string stringCmdArgument = ((LinkButton)sender).CommandArgument;
                    if (stringCmdArgument != null && stringCmdArgument.Trim().Length > 0)
                    {
                        string[] stringValues = stringCmdArgument.Split(',');
                        if (stringValues != null && stringValues.Length > 0)
                        {
                            string stringusrid = stringValues[0];
                            string stringBEid = stringValues[1];

                            string stringPROJECTNAME = ConfigurationManager.AppSettings["PROJECTNAME"] != null ? ConfigurationManager.AppSettings["PROJECTNAME"].ToString() : "EDIR";

                            Response.Redirect("UserScreenCluster.aspx?ID=" + stringusrid);
                        }
                    }
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
        }

        private string PrepareSearchExpression()
        {
            string stringexp = null;
            try
            {

                if (txtID.Text.Length > 0 && txtID.Text.Trim() != "%")
                {
                    stringexp += "AND USR.USER_ID like '%" + txtID.Text.Trim().ToUpper() + "%'";
                }
                if (txtFilename.Text.Length > 0 && txtFilename.Text.Trim() != "%")
                {
                    stringexp += "AND USR.USER_NAME like '%" + txtFilename.Text.Trim() + "%'";
                }
                if (txtMNo.Text.Length > 0 && txtMNo.Text.Trim() != "%")
                {
                    stringexp += "AND USR.USER_TELEPHONE like '%" + txtMNo.Text.Trim().ToUpper() + "%'";
                }
                if (txtEmail.Text.Length > 0 && txtEmail.Text.Trim() != "%")
                {
                    stringexp += "AND USR.USER_EMAIL_ID like '%" + txtEmail.Text.Trim().ToUpper() + "%'";
                }
                if (ddlStatus.SelectedItem != null && ddlStatus.SelectedItem.Value.Length > 0 && ddlStatus.SelectedItem.Value.Trim() != "%")
                {
                    stringexp += "AND USR.STATUS_REASON= '" + ddlStatus.SelectedItem.Value.ToString() + "'";
                }
                else
                {
                    stringexp += "and USR.STATUS_REASON = 'ACTIVE'";
                }
                if (ddltDepartment.SelectedItem != null && ddltDepartment.SelectedItem.Value.Length > 0 && ddltDepartment.SelectedItem.Value.Trim() != "%")
                {
                    stringexp += "AND USR.DEPARTMENTS= '" + ddltDepartment.SelectedValue.ToString() + "'";
                }
                
                if (txtFrmDate.Text.Trim().Length > 0 && txtToDate.Text.Trim().Length > 0)
                {
                    DateTime objDateTo = CommonFunctions.ConvertToDateTime(txtToDate.Text.Trim(), "dd-MM-yyyy");

                    DateTime objDateFrom = CommonFunctions.ConvertToDateTime(txtFrmDate.Text.Trim(), "dd-MM-yyyy");
                    stringexp += "and CONVERT(date,USR.CREATED_ON)  between  CONVERT(date,'" + Convert.ToDateTime(objDateFrom).ToString("yyyy-MM-dd") + "') and CONVERT(date,'" + Convert.ToDateTime(objDateTo).ToString("yyyy-MM-dd") + "')";

                }
                else if (txtFrmDate.Text.Length > 0 && txtFrmDate.Text.Trim() != "%")
                { 
                    DateTime objDateFrom = CommonFunctions.ConvertToDateTime(txtFrmDate.Text.Trim(), "dd-MM-yyyy");
                    stringexp += "AND USR.CREATED_ON= '" + Convert.ToDateTime(objDateFrom).ToString("yyyy-MM-dd") + "'";
                }
                else if (txtToDate.Text.Length > 0 && txtToDate.Text.Trim() != "%")
                {
                    DateTime objDateFrom = CommonFunctions.ConvertToDateTime(txtToDate.Text.Trim(), "dd-MM-yyyy");
                    stringexp += "AND USR.CREATED_ON= '" + Convert.ToDateTime(objDateFrom).ToString("yyyy-MM-dd") + "'";
                }

                return stringexp;
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
                return null;
            }
        }

        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                LoadRecord(PrepareSearchExpression());
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
        }

        protected void lbtnClear_Click(object sender, EventArgs e)
        {
            try
            {
                txtID.Text = txtMNo.Text = txtFilename.Text = txtEmail.Text = "";
                ddltDepartment.ClearSelection();
                ddlStatus.ClearSelection();
                txtFrmDate.Text = "";
                txtToDate.Text = "";
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }

        }

        //Paging
        private void PopulatePager(int recordCount, int currentPage)
        {
            try
            {
                List<ListItem> pages = new List<ListItem>();
                int startIndex, endIndex;
                int pagerSpan = 5;
                int intpaging = CommonFunctions.GridViewPagesize(stringformIdPaging);
                double dblPageCount = (double)((decimal)recordCount / Convert.ToDecimal(intpaging));
                int pageCount = (int)Math.Ceiling(dblPageCount);
                var ssn = dblPageCount.ToString(CultureInfo.InvariantCulture).Split('.');
                if (ssn[0] == "0")
                {
                    pageCount = (int)Math.Round(dblPageCount);
                }
                if (currentPage == 0)
                {
                    currentPage = 1;
                    startIndex = 1;
                }
                else
                {
                    startIndex = currentPage > 1 && currentPage + pagerSpan - 1 < pagerSpan ? currentPage : 1;
                }

                endIndex = pageCount > pagerSpan ? pagerSpan : pageCount;
                if (currentPage > pagerSpan % 2)
                {
                    if (currentPage == 2)
                    {
                        endIndex = 5;
                    }
                    else
                    {
                        endIndex = currentPage + 2;
                    }
                }
                else
                {
                    endIndex = (pagerSpan - currentPage) + 1;
                }
                if (currentPage != 0)
                {
                    if (endIndex - (pagerSpan - 1) > startIndex)
                    {
                        startIndex = endIndex - (pagerSpan - 1);
                    }
                }
                if (endIndex > pageCount)
                {
                    endIndex = pageCount;
                    startIndex = ((endIndex - pagerSpan) + 1) > 0 ? (endIndex - pagerSpan) + 1 : 1;
                }
                //Add the First Page Button.
                if (currentPage > 1)
                {
                    pages.Add(new ListItem("First", "1"));
                }
                //Add the Previous Button.
                if (currentPage > 1)
                {
                    pages.Add(new ListItem("<<", (currentPage - 1).ToString()));
                }
                for (int i = startIndex; i <= endIndex; i++)
                {
                    pages.Add(new ListItem(i.ToString(), i.ToString(), i != currentPage));
                }
                //Add the Next Button.
                if (currentPage < pageCount)
                {
                    pages.Add(new ListItem(">>", (currentPage + 1).ToString()));
                }
                //Add the Last Button.
                if (currentPage != pageCount)
                {
                    if (currentPage < pageCount)
                    {
                        pages.Add(new ListItem("Last", pageCount.ToString()));
                    }
                    else
                    {

                    }
                    //pages.Add(new ListItem("Last", pageCount.ToString()));
                }
                rptPager.DataSource = pages;
                rptPager.DataBind();
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
        }

        protected void lnkPage_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["PageIndex1"] != null)
                {
                    pageIndex = Convert.ToInt32(Session["PageIndex1"].ToString());
                    Session["PageIndex"] = pageIndex;
                }
                else
                {
                    pageIndex = int.Parse((sender as LinkButton).CommandArgument);
                    if (pageIndex != 0)
                    {
                        Session["PageIndex"] = pageIndex;
                    }
                }

                if (pageIndex == 1)
                {
                    recFrom = 0;
                    recTo = CommonFunctions.GridViewPagesize(stringformIdPaging);
                }
                else
                {
                    int recFrom1 = (pageIndex * recTo) - recTo;
                    recFrom = recFrom1 + 1;
                    recTo = recFrom1 + CommonFunctions.GridViewPagesize(stringformIdPaging);
                }

                LoadRecord((string)ViewState["vsSearchCondition"], ViewState["vsSortExpression"].ToString());
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
        }

        protected void lbtnExport_Click(object sender, EventArgs e)
        {

            int interrorcount = 0;
            string[] stringOutputResult = null;
            string stringformid = "FAD1011R1V3";
            string stringOrderBy = "";
            byte[] byteArray;
            string stringServerPath = "";
            string stringsource = "";
            string stringFunctionName = "List1R1V1";
            string stringExportName = "User Summary";
            string stringexp1 = "";
            try
            {
                if (Session["exportcondition"] != null)
                {
                    stringexp1 = Session["exportcondition"].ToString();
                }
                else
                {
                    stringexp1 = "";
                }
                stringServerPath = CommonFunctions.Export(stringFunctionName, stringformid, stringexp1, stringOrderBy, stringExportName, out byteArray, out interrorcount, out stringOutputResult);

                if (interrorcount == 0)
                {

                    if (stringOutputResult != null && stringOutputResult.Length > 0)
                    { 
                        CommonFunctions.OpenExportedFileR1V1(this, byteArray, stringExportName.ToString(), "EXCEL");
                    }
                    else
                    {
                        CommonFunctions.ShowMessageboot(this, "Report Not Found.");
                    }
                }
                else
                {
                    Errorpopup(stringOutputResult);
                }

            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessage(objException);
            }
        }
        private void LoadDepartment()
        {
            DataSet objDatasetResult = null;
            int interrorcount = 0;
            int intTotalRecord = 0;
            string[] stringOutputResult = null;
            string stringformid = "FA0010R1V1";
            string stringOrderBy = "mrdep.short_name asc";
            int intFromRecord = 0;
            int intToRecord = 5000;
            DataTable objDataTable = null;
            string stringbeid = CommonFunctions.GETBussinessEntity();
            try
            {
                ddltDepartment.Items.Clear();

                string stringexp01 = "And mrdep.be_id= '" + stringbeid + "' And mrdep.delmark='N' ";
                string stringServiceType = "List1R1V1";

                objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, stringexp01, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);
                if (interrorcount == 0)
                {
                    if (objDatasetResult != null && objDatasetResult.Tables.Count > 0 && objDatasetResult.Tables["t1"] != null && objDatasetResult.Tables["t1"].Rows.Count > 0)
                    {
                        objDataTable = objDatasetResult.Tables["t1"];
                    }
                    if (objDataTable != null && objDataTable.Rows.Count > 0)
                    {
                        ddltDepartment.DataTextField = "short_name";
                        ddltDepartment.DataValueField = "DEPT_ID";
                        ddltDepartment.DataSource = objDataTable;
                        ddltDepartment.DataBind();
                        ddltDepartment.Items.Insert(0, new ListItem("", ""));
                    }
                    else
                    {
                        ddltDepartment.DataSource = null;
                        ddltDepartment.DataBind();
                    }
                }
                else
                {
                    CommonFunctions.ShowMessageboot(this, stringOutputResult[0]);
                }

            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
            }
        }
    }
}