using PRDG11EOSV03WB.CommonFunction;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using G11EOSMW.EncyptionManager;
using G10CertificateValidation;
using System.Configuration;

namespace PRDG11EOSV03WB
{
    public partial class Login : System.Web.UI.Page
    {
        #region Declarations

        public DataSet objDatasetAppsVariables;
        public DataSet objDatasetlocaldeclaration;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            string stringLogMsg = "";
            try
            {
                if (ConfigurationManager.AppSettings["MaintenancePageFlag"] != null && ConfigurationManager.AppSettings["MaintenancePageFlag"].ToString().ToUpper() == "Y")
                {
                    if (Request.QueryString.Count == 1)
                    {
                        if (Request.QueryString["g11"] != null && Request.QueryString["g11"].ToString().ToUpper() == "DEPLOYMENT_TEST")
                        {

                        }
                        else
                            Response.Redirect("MaintenancePage.aspx", true);
                    }
                    else
                        Response.Redirect("MaintenancePage.aspx", true);
                }
                try
                {

                    if (!Page.IsPostBack)
                    {
                        //objDatasetlocaldeclaration = CommonFunctions.ConstructlocalStringInfoDetails(); //
                        //Session["objDatasetlocaldeclaration"] = objDatasetlocaldeclaration;
                        string stringUserLoginHistoryID = "";
                        string stringhBusinessID = "";
                        string boolLoginFlag = "N";
                        //if (Session["ssnLoggedInFlag"] != null)
                        //{
                        //    boolLoginFlag = Session["ssnLoggedInFlag"].ToString();
                        //    if (Session["ssnLoggedInFlag"] != null && Session["ssnLoggedInFlag"].ToString() == "Y")
                        //        Response.Redirect("about://blank");
                        //}
                        UpdatePanelModal6.Visible = false;
                        if (Request.QueryString["LogMsg"] != null)
                        { stringLogMsg = Request.QueryString["LogMsg"].ToString(); }
                        if (stringLogMsg.Length > 0)
                        {
                            if (Session["stringUserLoginHistoryID"] != null)
                            {
                                stringUserLoginHistoryID = Session["stringUserLoginHistoryID"].ToString();
                            }
                            else if (CommonFunctions.stringUserLoginHistoryID.ToString().Length > 0)
                            {
                                stringUserLoginHistoryID = CommonFunctions.stringUserLoginHistoryID.ToString();
                            }
                            if (stringLogMsg.Length > 0)
                            {
                                string stringMessage = "";
                                if (stringLogMsg == "Y")
                                {
                                    stringMessage = "LOGOUT SUCCESS";
                                }
                                else if (stringLogMsg == "SESSIONLOGOUT")
                                {
                                    stringMessage = "INACTIVE SESSION TIME OUT REACHED";
                                }

                                if (Session["objDatasetlocaldeclaration"] == null)
                                {
                                    objDatasetlocaldeclaration = CommonFunctions.ConstructlocalStringInfoDetails();
                                    Session["objDatasetlocaldeclaration"] = objDatasetlocaldeclaration;
                                }
                                if (HttpContext.Current.Session["BusinessID"] == null)
                                {
                                    stringhBusinessID = CommonFunctions.stringBOID.ToString();
                                }
                                else
                                {
                                    stringhBusinessID = HttpContext.Current.Session["BusinessID"].ToString();
                                }
                                DataSet objDatasetstringinfologout = (DataSet)HttpContext.Current.Session["objDatasetlocaldeclaration"];
                                objDatasetstringinfologout.Tables[0].Rows[0]["FORM_ID"] = "FAD6005R1V1";
                                objDatasetstringinfologout.Tables[0].Rows[0]["BE_ID"] = stringhBusinessID;
                                UpdateLoginHistory(objDatasetstringinfologout, stringMessage);

                            }
                        }
                        Session.Clear();
                        Session["ssnLoggedInFlag"] = boolLoginFlag;

                        txtUserName.Focus();
                        if (!Page.IsPostBack && Request.QueryString.Get("Connection") != null)
                        {
                            lblErrStatus.Text = Request.QueryString["Connection"];
                        }
                        Session["G11EOSUser_ID"] = null;
                    }
                }
                catch (Exception objException)
                {
                    CommonFunctions.ShowMessageboot02(objException);
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }

        }

        private void Sessionvalue()
        {
            try
            {
                objDatasetlocaldeclaration = CommonFunctions.ConstructlocalStringInfoDetails(); //
                Session["objDatasetlocaldeclaration"] = objDatasetlocaldeclaration;
                string stringuser = "";
                objDatasetAppsVariables = (DataSet)Session["objDatasetlocaldeclaration"];
                if (Session["G11EOSUser_ID"] != null)
                {
                    stringuser = Session["G11EOSUser_ID"].ToString();
                    if (stringuser.Length > 0)
                    {
                        stringuser = Session["G11EOSUser_ID"].ToString();
                    }
                    else
                    {
                        stringuser = "GUEST";
                    }

                }
                else
                {
                    stringuser = "GUEST";

                }

                objDatasetAppsVariables.Tables[0].Rows[0]["USER_ID"] = stringuser;
                objDatasetAppsVariables.Tables[0].Rows[0]["USER_NAME"] = stringuser;
                if (objDatasetAppsVariables != null && objDatasetAppsVariables.Tables.Count > 0 && objDatasetAppsVariables.Tables[0].Rows.Count > 0)
                {
                    if (txtUserName.Text.Trim().Length > 0 && txtUserName.Text.Trim().Contains(@"\"))
                    {
                        string[] stringdomain = txtUserName.Text.Trim().Split('\\');
                        if (stringdomain != null && stringdomain.Length >= 2)
                        {
                            objDatasetAppsVariables.Tables[0].Rows[0]["DOMAIN_NAME"] = stringdomain[0].ToString();
                        }
                    }
                }
                Session["objDatasetlocaldeclaration"] = objDatasetAppsVariables;

            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessage(objException);
            }
            finally
            {

            }

        }


        protected void btnLogin_Click(object sender, EventArgs e)
        {
            int interrorcount = 0;
            DataSet objDataSet = null;
            string stringPassword;
            clsEncrypt objclsEncrypt;
            string[] stringOutputResult = null;
            lblErrStatus.Text = "";
            objDatasetlocaldeclaration = CommonFunctions.ConstructlocalStringInfoDetails(); //
            Session["objDatasetlocaldeclaration"] = objDatasetlocaldeclaration;

            objDatasetAppsVariables = (DataSet)Session["objDatasetlocaldeclaration"];
            objDatasetAppsVariables.Tables[0].Rows[0]["FORM_ID"] = "FAD1011R1V1";

            DataSet objDatasetStringInfoLog = null;
            string stringRollID = "";
            string status = "ACTIVE";
            string stringsession = "";
            try
            {
                Session.Clear();
                if (txtUserName.Text.Trim().Length == 0)
                {
                    lblErrStatus.Text = "User name should not be empty.";
                    txtUserName.Focus();
                }
                else if (txtPassword.Text.Trim().Length == 0)
                {
                    lblErrStatus.Text = "Password should not be empty.";
                    txtUserName.Focus();
                }
                else
                {
                    objDatasetStringInfoLog = CommonFunctions.ConstructlocalStringInfoDetails();
                    objDatasetStringInfoLog.Tables[0].Rows[0]["FORM_ID"] = "FAD6005R1V1";
                    objDatasetStringInfoLog.Tables[0].Rows[0]["USER_ID"] = txtUserName.Text.Trim();
                    objDatasetStringInfoLog.Tables[0].Rows[0]["USER_NAME"] = txtUserName.Text.Trim();

                    stringOutputResult = new string[3];

                    clsCertificateValidation.EnableTrustedHosts();
                    using (G11EISMWSelectionService.SelectionServiceClient objSelectionServiceClient = new G11EISMWSelectionService.SelectionServiceClient(CommonFunctions.CreateServiceBasicHttpBinding(), CommonFunctions.objEndpointAddressSelectionService))
                    {
                        objclsEncrypt = new clsEncrypt(enumEncryptionAlgorithm.DES, "GSSLOGIN");
                        stringPassword = objclsEncrypt.Encrypt(txtPassword.Text.Trim()) + "ivector" + Convert.ToBase64String(objclsEncrypt.byteIV) + "end" + "DES";

                        objDataSet = objSelectionServiceClient.ValidateUserLoginFromDBR1V1(txtUserName.Text.Trim(), stringPassword, objDatasetAppsVariables, out interrorcount, out stringOutputResult);
                        if (objSelectionServiceClient != null)
                            objSelectionServiceClient.Close();
                    }
                    if (interrorcount == 0)
                    {
                        if (objDataSet.Tables[0].Select("USER_ID='" + txtUserName.Text.Trim() + "'").Length > 0)
                        {
                            if (objDataSet.Tables[0].Select("USER_ID='" + txtUserName.Text.Trim() + "' AND STATUS_REASON='" + status + "'").Length > 0)
                            {
                                Session["G11EOSUser_Name"] = "";
                                Session["BusinessID"] = "";
                                Session["ssnUserRole"] = "";
                                Session["Cus_Name"] = "";
                                Session["Cus_ID"] = "";
                                Session["G11EOSUser_ID"] = txtUserName.Text.Trim().ToUpper();
                                Session["G11EOS_Logon_DateTime"] = DateTime.Now;
                                DataTable objdatatableBEID = null;
                                DataTable objdatatableAPPLICATION = null;
                                DataTable objdatatableRoles = null;
                                DataRow[] objdatarow = null;

                                HttpRequest objHTTPRequest = base.Request;
                                string stringUserHostName = objHTTPRequest.UserHostName;

                                Session["G11EOSUser_Name"] = objDataSet.Tables[0].Rows[0]["USER_NAME"].ToString();
                                HttpContext.Current.Session["ssnboolUserValidation"] = true;//used in site master

                                if (Session["G11EOSUser_ID"] != null) { Session.Remove("G11EOSUser_ID"); }
                                Session.Add("G11EOSUser_ID", txtUserName.Text.Trim().ToUpper());

                                if (Session["stringComputerName"] != null) { Session.Remove("stringComputerName"); }
                                Session.Add("stringComputerName", stringUserHostName);

                                Session["password="] = stringUserHostName;
                                Sessionvalue();

                                if (interrorcount == 0)
                                {

                                    //for BEID selection

                                    //t1 - user
                                    //t2 - user source(customer access)
                                    //t3 - business entity
                                    //t4 - user access group
                                    //t5 - APPLICATION

                                    objdatatableBEID = objDataSet.Tables["t3"];
                                    objdatatableAPPLICATION = objDataSet.Tables["t5"];
                                    objdatatableRoles = objDataSet.Tables["t4"];
                                    Session["G11EOSUser_Name"] = objDataSet.Tables[0].Rows[0]["USER_NAME"].ToString();

                                    if (objdatatableBEID != null && objdatatableBEID.Rows.Count > 0)
                                    {
                                        if (objdatatableBEID != null && objdatatableBEID.Rows.Count == 1)
                                        { 
                                            string stringBusinessID = objdatatableBEID.Rows[0]["be_id"].ToString(); 
                                            Session["BusinessID"] = stringBusinessID;
                                            Session["BusinessIDName"] = objdatatableBEID.Rows[0]["reference_1"].ToString();
                                            Session["BusinessIDAddress"] = objdatatableBEID.Rows[0]["reference_2"].ToString();
                                            stringsession = "TRUE";
                                            if (Session["BusinessID"] != null)
                                            {
                                                objDatasetStringInfoLog.Tables[0].Rows[0]["BE_ID"] = Session["BusinessID"].ToString().Trim();
                                            }
                                            InsertLoginHistoryR1V1(txtUserName.Text.Trim(), "LOGIN SUCCESS", "", objDatasetStringInfoLog);


                                        }
                                        else
                                        {
                                            gvOrderUpload.DataSource = objdatatableBEID;
                                            gvOrderUpload.DataBind();
                                            if (Session["G11EOSUser_Name"] != null)
                                            {
                                                string   stringusrname = (string)Session["G11EOSUser_Name"].ToString();
                                                lblheader.Text = "Welcome  " + stringusrname;
                                            }
                                            Modelpopuperror.Show();
                                            UpdatePanelModal6.Visible = true;
                                        } 
                                        //for Application table

                                        if (objdatatableAPPLICATION != null && objdatatableAPPLICATION.Rows.Count > 0)
                                        {


                                        }
                                        else
                                        {
                                            stringsession = "FALSE";
                                            string stringmessage = "No Application attached to this User Account[ " + txtUserName.Text.Trim() + " ]. Please contact Administrator";
                                            lblErrStatus.Text = stringmessage;
                                            InsertLoginHistoryR1V1(txtUserName.Text.Trim(), "LOGIN FAILED", stringmessage, objDatasetStringInfoLog);
                                        }
                                        //for role table
                                        if (objdatatableRoles != null && objdatatableRoles.Rows.Count > 0)
                                        {
                                            Session["UserRolestable"] = objdatatableRoles;
                                            if (objdatatableRoles.Rows.Count == 1)
                                            {
                                                stringRollID = objDataSet.Tables["t4"].Rows[0]["Group_ID"].ToString();
                                                Session["ssnUserRole"] = stringRollID;
                                            }
                                            else
                                            {
                                                objDataSet.Tables["t4"].DefaultView.Sort = "Group_ID asc";
                                                objDataSet.Tables["t4"].DefaultView.ToTable();
                                                stringRollID = objDataSet.Tables["t4"].Rows[0]["Group_ID"].ToString();
                                                Session["ssnUserRole"] = stringRollID;
                                            }
                                        }

                                    }
                                    else
                                    {
                                        stringsession = "FALSE";
                                        string stringmessage = "No Business Organization is attached to this User Account[ " + txtUserName.Text.Trim() + " ]. Please contact Administrator";
                                        lblErrStatus.Text = stringmessage;
                                        InsertLoginHistoryR1V1(txtUserName.Text.Trim(), "LOGIN FAILED", stringmessage, objDatasetStringInfoLog);
                                    }


                                    if (stringsession == "TRUE")
                                    {
                                        string stringRedirect = "UserHistory.aspx";
                                        Response.Redirect(stringRedirect, true);

                                        //Session["ssnLoggedInFlag"] = "Y";
                                        //string stringPage = "UserHistory.aspx";
                                        //string redirectScript = "<script language='JavaScript'>" +
                                        //        "sessionStorage.setItem('ssnKey', '" + Session["G11EOSUser_ID"].ToString() + "');" +
                                        //        "localStorage.setItem('lsKey', '" + Session["G11EOSUser_ID"].ToString() + "');" +
                                        //        "window.location = '" + stringPage + "'" +
                                        //    "</script>";

                                        //ClientScript.RegisterStartupScript(typeof(string), "RedirectScript", redirectScript, false); 
                                    }
                                }


                            }

                            else
                            {
                                UpdatePanelModal6.Visible = false;
                                Modelpopuperror.Hide();
                                lblErrStatus.Text = "Account is Disabled, please contact Department Administrator.";
                                InsertLoginHistoryR1V1(txtUserName.Text.Trim(), "LOGIN FAILED", lblErrStatus.Text, objDatasetStringInfoLog);
                            }
                        }
                        else
                        {
                            UpdatePanelModal6.Visible = false;
                            Modelpopuperror.Hide();
                            lblErrStatus.Text = "Invalid User ID.";
                            InsertLoginHistoryR1V1(txtUserName.Text.Trim(), "LOGIN FAILED", lblErrStatus.Text, objDatasetStringInfoLog);
                        }
                    }
                    else
                    {
                        UpdatePanelModal6.Visible = false;
                        Modelpopuperror.Hide();
                        lblErrStatus.Text = stringOutputResult[2];
                        InsertLoginHistoryR1V1(txtUserName.Text.Trim(), "LOGIN FAILED", stringOutputResult[2].ToString(), objDatasetStringInfoLog);
                    }

                }
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
                lblErrStatus.Text = objException.Message;
            }
        }
        private void UpdateLoginHistory(DataSet objDatasetAppsVariables01, string stringMessage)
        {
            int interrorcount = 0;
            string[] stringresult = null; string stringUserLoginHistoryID = "";
            try
            {
                if (Session["stringUserLoginHistoryID"] != null)
                {
                    stringUserLoginHistoryID = Session["stringUserLoginHistoryID"].ToString();
                }
                else if (CommonFunctions.stringUserLoginHistoryID.ToString().Length > 0)
                {
                    stringUserLoginHistoryID = CommonFunctions.stringUserLoginHistoryID.ToString();
                }

                CommonFunctions.UpdateLoginHistory(new string[] { stringUserLoginHistoryID, stringMessage }, objDatasetAppsVariables01, out interrorcount, out stringresult);
                if (interrorcount == 0)
                {
                    txtUserName.Focus();
                }
                else
                {
                    UpdatePanelModal6.Visible = false;
                    Modelpopuperror.Hide();
                    lblErrStatus.Text = stringresult[2];
                }

            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }

        }
        private void InsertLoginHistoryR1V1(string stringUSERID, string stringSTATUS, string stringMessage, DataSet objDatasetAppsVariables01)
        {
            int interrorcount = 0;
            string[] stringresult = null;
            try
            {
                if(objDatasetAppsVariables01 != null && objDatasetAppsVariables01.Tables.Count > 0 && objDatasetAppsVariables01.Tables[0].Rows.Count > 0)
                {
                    if (txtUserName.Text.Trim().Length > 0 && txtUserName.Text.Trim().Contains(@"\"))
                    {
                        string[] stringdomain = txtUserName.Text.Trim().Split('\\');
                        if (stringdomain != null && stringdomain.Length >= 2)
                        {
                            objDatasetAppsVariables01.Tables[0].Rows[0]["DOMAIN_NAME"] = stringdomain[0].ToString();
                        }
                    }
                }
              
                string stringPortalNAME = ConfigurationManager.AppSettings["AUDITLOGNAME"] != null ? ConfigurationManager.AppSettings["AUDITLOGNAME"].ToString() : "ADMIN PORTAL";
                string stringIPaddress = ConfigurationManager.AppSettings["stringISPIPAddress"] != null ? ConfigurationManager.AppSettings["stringISPIPAddress"].ToString() : "";
                CommonFunctions.DataManipulationLoginHistoryR1V1(new string[] { stringSTATUS, stringMessage, stringIPaddress, "", stringPortalNAME }, objDatasetAppsVariables01, out interrorcount, out stringresult);
                if (interrorcount == 0)
                {

                }
                else
                {
                    UpdatePanelModal6.Visible = false;
                    Modelpopuperror.Hide();
                    lblErrStatus.Text = stringresult[2];
                }

            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }

        }
        private void LoadBussinessentity1(string stringBuss)
        {
            DataSet objDatasetResult = null;
            int interrorcount = 0;
            int intTotalRecord = 0;
            string[] stringOutputResult = null;
            string stringformid = "FA0034R1V1";
            string stringExpression = "";
            string stringOrderBy = "INST.INS_NAME asc";
            int intFromRecord = 0;
            int intToRecord = int.MaxValue;
            try
            {

                stringOutputResult = new string[3];
                objDatasetAppsVariables = (DataSet)Session["objDatasetlocaldeclaration"];
                objDatasetAppsVariables.Tables[0].Rows[0]["FORM_ID"] = "COMMONLOVBUSINESSENTITIES";

                clsCertificateValidation.EnableTrustedHosts();

                stringExpression = "WHERE be_id IN (" + stringBuss + ")";
                string stringServiceType = "List1R1V1";
                string stringexp012 = "And INST.ins_id IN (" + stringBuss + ") And INST.delmark= 'N'";

                objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, stringexp012, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);
                if (interrorcount == 0)
                {
                    if (objDatasetResult != null && objDatasetResult.Tables.Count > 0 && objDatasetResult.Tables[0] != null && objDatasetResult.Tables[0].Rows.Count > 0)
                    {
                        gvOrderUpload.DataSource = objDatasetResult.Tables[0];
                        gvOrderUpload.DataBind();

                        string stringusrname = (string)Session["G11EOSUser_ID"].ToString();
                        lblheader.Text = "Welcome  " + stringusrname;
                        UpdatePanelModal6.Visible = true;
                        Modelpopuperror.Show();
                    }
                    else
                    {
                        UpdatePanelModal6.Visible = false;
                        Modelpopuperror.Hide();
                        gvOrderUpload.DataSource = objDatasetResult;
                        gvOrderUpload.DataBind();
                    }
                }
                else
                {
                    CommonFunctions.ShowMessageboot(this, stringOutputResult[0]); 
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
            finally
            {
                interrorcount = 0;
                objDatasetResult = null;
                stringOutputResult = null;
                stringExpression = "";
            }


        }


        private bool ValidateCheckedRecords()
        {
            bool boolSelected = false;
            for (int intCount = 0; intCount < gvOrderUpload.Rows.Count; intCount++)
            {
                RadioButton objradiobutton = (RadioButton)gvOrderUpload.Rows[intCount].FindControl("RowSelector");
                if (objradiobutton != null && objradiobutton.Checked)
                {
                    boolSelected = true;
                    break;
                }
            }

            return boolSelected;
        }

        protected void btnclose_Click(object sender, EventArgs e)
        {
            Modelpopuperror.Hide();
        }


        protected void gvOrderUpload_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string stringSort = string.Empty;
            DataRow objDataRow = null;
            string stringIntStaffID = "";
            try
            {
                GridViewRow objGridViewRow = e.Row;
                if (objGridViewRow.DataItem == null) { return; }

                objDataRow = ((DataRowView)e.Row.DataItem).Row;
                stringIntStaffID = objDataRow["BE_ID"].ToString();


                if (objGridViewRow.FindControl("hfIDs") != null)
                {
                    ((HiddenField)objGridViewRow.FindControl("hfIDs")).Value = stringIntStaffID;
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
            finally
            {
                stringSort = string.Empty;
                objDataRow = null;
                stringIntStaffID = "";
            }
        }

        protected void btnok_Click(object sender, EventArgs e)
        {
            string stringBusinessID = "";
            string stringTooltip = "";
            string[] stringIDs = null;
            string stringID = "";
            DataSet objDatasetStringInfoLog = null;
            try
            {
                if (ValidateCheckedRecords())
                {
                    for (int intCount = 0; intCount < gvOrderUpload.Rows.Count; intCount++)
                    {
                        RadioButton objradiobutton = (RadioButton)gvOrderUpload.Rows[intCount].FindControl("RowSelector");
                        HiddenField objHiddenField = (HiddenField)gvOrderUpload.Rows[intCount].FindControl("hfIDs");

                        if (objradiobutton != null && objradiobutton.Checked)
                        {
                            stringBusinessID = "";
                            stringTooltip = objHiddenField.Value;

                            if (stringTooltip != null && stringTooltip.Trim().Length > 0)
                            {
                                stringIDs = stringTooltip.Split('~');
                                if (stringIDs != null && stringIDs.Length > 0)
                                {
                                    stringBusinessID = stringIDs[0];
                                     
                                    Session["BusinessID"] = stringBusinessID;

                                }
                            }
                        }
                    }
                    objDatasetStringInfoLog = CommonFunctions.ConstructlocalStringInfoDetails();
                    objDatasetStringInfoLog.Tables[0].Rows[0]["FORM_ID"] = "FAD6005R1V1";
                    objDatasetStringInfoLog.Tables[0].Rows[0]["USER_ID"] = txtUserName.Text.Trim();
                    objDatasetStringInfoLog.Tables[0].Rows[0]["USER_NAME"] = txtUserName.Text.Trim();
                    if (Session["BusinessID"] != null)
                    {
                        objDatasetStringInfoLog.Tables[0].Rows[0]["BE_ID"] = Session["BusinessID"].ToString().Trim();
                    }
                    InsertLoginHistoryR1V1(txtUserName.Text.Trim(), "LOGIN SUCCESS", "", objDatasetStringInfoLog);


                    stringID = txtUserName.Text.Trim().ToUpper();
                    string stringRedirect = "UserHistory.aspx";
                    Response.Redirect(stringRedirect, true);
                }
                else
                {
                    lblcheckboxvalidate.Visible = true;
                    Modelpopuperror.Show();
                    UpdatePanelModal6.Visible = true;
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
            finally
            {
                stringBusinessID = "";
                stringTooltip = "";
                stringIDs = null;
                stringID = "";
            }
        }
    

        protected void lbtnForgot_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtUserName.Text.Trim().Length > 0)
                {
                    ResetPassword();
                }
                else
                {
                    CommonFunctions.ShowMessageboot(this, "Please enter your USERNAME");
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
        }
        private void ResetPassword()
        {
            System.Text.StringBuilder sb = null;
            string stringStatus = string.Empty;
            string[] stringOutputResult = null;

            objDatasetAppsVariables = (DataSet)Session["objDatasetlocaldeclaration"];
            objDatasetAppsVariables.Tables[0].Rows[0]["FORM_ID"] = "FAD1011R1V1";


            string stringOutputResultr = null;
            int interrorcount = 0;
            try
            {
                stringOutputResult = new string[3];
                if (txtUserName.Text.Trim().Length > 0)
                {
                    if (IsvalidUser())
                    {
                        using (G11EISMWSelectionService.SelectionServiceClient objSelectionServiceClient = new G11EISMWSelectionService.SelectionServiceClient(CommonFunctions.CreateServiceBasicHttpBinding(), CommonFunctions.objEndpointAddressSelectionService))
                        {
                            //(txtUserName.Text.Trim().ToUpper()
                            //  stringOutputResultr = objSelectionServiceClient.ResetPasswordR1V1(stringoldPassword, stringnewPassword, objDatasetAppsVariables, out interrorcount, out stringOutputResult);
                            // if (objSelectionServiceClient != null)
                            //     objSelectionServiceClient.Close();
                        }

                        if (interrorcount == 0)
                        {
                            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "ShowSuccessMsg", "ShowSuccessMsg();", true);
                        }
                        else
                        {

                            CommonFunctions.ShowMessageboot(this, stringOutputResult[2]);
                        }

                    }
                    else
                    {
                        CommonFunctions.ShowMessageboot(this, "Please enter valid USERNAME");
                    }

                }
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
        }

        public bool IsvalidUser()
        {
            DataSet objDataSet = null;
            int interrorcount = 0;
            int intTotalRecord = 0;
            string[] stringOutputResult = null;
            string stringformid = "FAD1011R1V1";
            string stringOrderBy = "";
            int intFromRecord = 0;
            int intToRecord = int.MaxValue;
            DataTable objdatatableroles = null;
            try
            {
                string stringexp = "";
                string stringServiceType = "List1R1v1AdminServiceClient";
                objDataSet = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, stringexp, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);

                if (interrorcount == 0)
                {
                    if (objDataSet != null && objDataSet.Tables.Count > 0 && objDataSet.Tables["t1"] != null && objDataSet.Tables["t1"].Rows.Count > 0)
                    {
                        objdatatableroles = objDataSet.Tables["t1"];
                        if (objDataSet.Tables[0].Select("USER_Id='" + txtUserName.Text.Trim().ToUpper() + "'").Length > 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    CommonFunctions.ShowMessageboot(this, stringOutputResult[0]);
                }

            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
            finally
            {
                interrorcount = 0;
                intTotalRecord = 0;
                stringOutputResult = null;
                stringformid = "";
                stringOrderBy = "";
                intFromRecord = 0;
                intToRecord = 0;
            }


            return false;
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {

        }
    }
}