using G11EOSMW.EncyptionManager;
using PRDG11EOSV03WB.CommonFunction;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;


namespace PRDG11EOSV03WB
{
    public partial class UserScreenCluster : System.Web.UI.Page
    {

        public DataSet objDatasetAppsVariables;
        public DataSet objDatasetlocaldeclaration;
        protected void Page_Load(object sender, EventArgs e)
        {
            string stringACCESS_USER = "";
            bool boolInsertPatientConsent = true;
            if (CommonFunctions.IsActive())
            {
                try
                {
                    string Password = txtPwd.Text;
                    txtPwd.Attributes.Add("value", Password);
                    string Password1 = txtCPwd.Text;
                    txtCPwd.Attributes.Add("value", Password1);

                    if (!IsPostBack)
                    {
                        HtmlGenericControl home = (HtmlGenericControl)this.Page.Master.FindControl("hdrPageTitle");
                        home.InnerText = "User Creation";
                        Session["ssnECom_UID"] = CommonFunctions.UserID;//?
                        LoadDepartment();
                        if (Request.QueryString["ID"] != null && Request.QueryString["ID"].Trim().Length > 0)
                        {
                            string StringUserid = Request.QueryString["ID"]; 
                            Session["ssnUserId"] = StringUserid;
                            ClearValues();
                            LoadUserRoles();
                            LoadRecord();
                            LoadBussinessEntity();
                            LoadApplication();
                            // VerifyAccessRights();//now hide after unhide this
                            Loadusers(StringUserid, Session["BusinessID"].ToString());
                            lnkDelete.Visible = true;
                        }
                        else
                        {
                            if (Session["ssnECom_UID"] != null)
                            {
                                ViewState["vsSort"] = " ASC";
                                ClearValues();
                                LoadUserRoles();
                                LoadBussinessEntity();
                                LoadApplication();
                                //VerifyAccessRights();//now hide after unhide this

                            }
                        }

                        if (ConfigurationManager.AppSettings["ACCESS_USER"] != null && ConfigurationManager.AppSettings["ACCESS_USER"].ToString().Trim().Length > 0)
                        {
                            stringACCESS_USER = ConfigurationManager.AppSettings["ACCESS_USER"].ToString();
                            string[] stringarrayPendingItems = stringACCESS_USER.Split(',');
                            string stringID1 = "";
                            if (Session["ssnECom_UID"] != null)
                            {
                                stringID1 = Session["ssnECom_UID"].ToString().Trim();

                            }
                            foreach (string value in stringarrayPendingItems)
                            {
                                if (stringID1.ToUpper() == value.Trim().ToUpper())
                                {
                                    boolInsertPatientConsent = false;
                                }
                            }
                        }
                        if (!boolInsertPatientConsent)
                        {
                            string stringINSTITUTIONadmin = ConfigurationManager.AppSettings["INSTITUTION_ADMIN"] != null ? ConfigurationManager.AppSettings["INSTITUTION_ADMIN"].ToString() : "INSTITUTION ADMIN";
                            string stringSUPERUSER = ConfigurationManager.AppSettings["SUPER_USER"] != null ? ConfigurationManager.AppSettings["SUPER_USER"].ToString() : "SUPER USER";

                            if (cbRole.Items.FindByValue(stringINSTITUTIONadmin) != null)
                            {
                                cbRole.ClearSelection();
                                cbRole.Items.FindByValue(stringINSTITUTIONadmin).Enabled = false;
                                //cbRole.Items.FindByValue(stringINSTITUTIONadmin).Selected = true;
                            }
                            if (cbRole.Items.FindByValue(stringSUPERUSER) != null)
                            {
                                cbRole.ClearSelection();
                                cbRole.Items.FindByValue(stringSUPERUSER).Enabled = false;
                                //cbRole.Items.FindByValue(stringSUPERUSER).Selected = true;
                            }
                        }



                    }

                }
                catch (Exception objException)
                {
                    CommonFunctions.ShowMessageboot02(objException);
                }
            }
        }

        #region Loaddata
        //for save

        //t1 - user---------------------------------------PSVR0100a0_user
        //t2 - user source-no need now--------------------noo
        //t3 - business entity-----------------------------PSVR0103a0_user_business_entity
        //t4 - user access group---------------------------PSVR0102a0_user_access_group
        //t5 - APPLICATION---------------------------------PSVR0107a0_user_applications

        //for LOAD DATA

        //t1 - user---------------------------------------PSVR0108a0_get_user_search
        //t2 - user source-no need now--------------------noo
        //t3 - business entity-----------------------------PSVR0110a0_get_user_business_entity_search
        //t4 - user access group---------------------------PSVR0109a0_get_user_access_group_search
        //t5 - APPLICATION---------------------------------PSVR0111a0_get_user_applications_search

        private void LoadUserRoles()//ok
        {
            DataSet objDataSet = null;
            int interrorcount = 0;
            int intTotalRecord = 0;
            string[] stringOutputResult = null;
            string stringformid = "FAD1012R1V1";
            string stringOrderBy = "";
            string stringBeID = "";
            int intFromRecord = 0;
            int intToRecord = int.MaxValue;
            DataTable objdatatableroles = null;
            try
            {
                if (Session["BusinessID"] != null)
                {
                    stringBeID = Session["BusinessID"].ToString();
                }
                string stringexp012 = "And (ags.TYPE='" + stringBeID + "' or ags.TYPE='ALL')  And ags.delmark= 'N'"; 
                string stringServiceType = "List1R1v1AdminServiceClient";
                objDataSet = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, stringexp012, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);

                if (interrorcount == 0)
                {
                    if (objDataSet != null && objDataSet.Tables.Count > 0 && objDataSet.Tables["t1"] != null && objDataSet.Tables["t1"].Rows.Count > 0)
                    {
                        objdatatableroles = objDataSet.Tables["t1"];
                        cbRole.DataTextField = "ACCESS_GRP_ID";
                        cbRole.DataValueField = "ACCESS_GRP_ID";
                        cbRole.DataSource = objdatatableroles;
                        Session["userroleforload"] = objdatatableroles;
                        cbRole.DataBind();
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
        }
        private void LoadBussinessEntity()//ok
        {
            DataSet objDatasetResult = null;
            int interrorcount = 0;
            int intTotalRecord = 0;
            string[] stringOutputResult = null;
            string stringformid = "FA0034R1V1";
            string stringOrderBy = "INST.INS_NAME asc";
            int intFromRecord = 0;
            int intToRecord = int.MaxValue;
            DataTable objDataTable = null;
            string stringBeID = "";
            try
            {
                if(Session["BusinessID"] != null)
                {
                    stringBeID = Session["BusinessID"].ToString();
                }
                string stringServiceType = "List1R1V1";
                //string stringexp012 = "And INST.BE_ID='"+stringBeID +"' And INST.delmark= 'N'";
                string stringexp012 = "And INST.delmark= 'N'";

                objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, stringexp012, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);
                if (interrorcount == 0)
                {
                    if (objDatasetResult != null && objDatasetResult.Tables.Count > 0 && objDatasetResult.Tables["t1"] != null && objDatasetResult.Tables["t1"].Rows.Count > 0)
                    {
                        objDataTable = objDatasetResult.Tables["t1"];
                    }
                    if (objDataTable != null && objDataTable.Rows.Count > 0)
                    {
                        cbbussinessent.DataValueField = "INS_ID";
                        cbbussinessent.DataTextField = "INS_NAME";
                        cbbussinessent.DataSource = objDataTable;
                        cbbussinessent.DataBind();
                        Session["beidforload"] = objDataTable;

                    }
                    else
                    {
                        cbbussinessent.DataSource = null;
                        cbbussinessent.DataBind();
                    }
                }
                else
                {
                    //Errorpopup(stringOutputResult);
                } 
            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
            }
             
        }

        private void LoadApplication()//ok
        {
            int intIndex = 0;
            try
            {
                DataTable objDataTable = new DataTable("t1");
                DataColumn objDataColumnprofileidID = new DataColumn("BE_ID");
                objDataTable.Columns.Add(objDataColumnprofileidID);

                DataTable objDataTableResultant = objDataTable;

                string stringProfileTypeID = ConfigurationManager.AppSettings["APPLICATION_ACCESS"] != null ? ConfigurationManager.AppSettings["APPLICATION_ACCESS"].ToString() : "CGH";


                DataRow objDataRowNew = objDataTableResultant.NewRow();
                objDataRowNew["BE_ID"] = stringProfileTypeID;

                objDataTableResultant.Rows.Add(objDataRowNew);

                cbapplicationId.DataValueField = "BE_ID";
                cbapplicationId.DataTextField = "BE_ID";
                cbapplicationId.DataSource = objDataTableResultant;
                cbapplicationId.DataBind();
                Session["Appidforload"] = objDataTableResultant;
                for (intIndex = 0; intIndex < cbapplicationId.Items.Count; intIndex++)
                {
                    cbapplicationId.Items[intIndex].Selected = true;
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }

        }


        #endregion


        #region link BUTTONS
        protected void lbtnNew_Click(object sender, EventArgs e)//ok
        {
            ClearValues();

        }
        protected void lbtnSave_Click(object sender, EventArgs e)//ok
        {
            string stringDomain = "";

            if (ddldom.SelectedItem != null)
            {
                stringDomain = ddldom.SelectedItem.Value;

                if (stringDomain.Length > 0 && stringDomain == "LOCAL")
                {
                    if (ValidateControlsDomain())
                    {
                        if (ValidateControlsPASSWORD())
                        {
                            SaveRecord1("SAVE");
                        }
                    }
                }
                else
                {
                    if (ValidateControlsDomain())
                    {
                        if (ValidateControlsPASSWORD())
                        {
                            SaveRecord1("SAVE");
                        }
                    }

                }
            }



        }
        public bool IsValidEmailAddress(string email)//ok
        {
            try
            {
                if (email.Length > 0)
                {
                    Regex regex = new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
                     RegexOptions.CultureInvariant | RegexOptions.Singleline);
                    bool isValidEmail = regex.IsMatch(email);
                    if (isValidEmail)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
                else
                {
                    return true;
                }

            }
            catch
            {
                return false;
            }
        }

        private void SaveRecord1(string stringProcessStatus)
        {
            DataSet objDatasetResult = null;
            int interrorcount = 0;
            int intTotalRecord = 0;
            string[] stringOutputResult = null;
            string stringformid = "FAD1011R1V1";
            string stringOrderBy = "";
            int intFromRecord = 0;
            int intToRecord = int.MaxValue;
            objDatasetResult = new DataSet();
            DataRow[] DataRowCollection;
            DataSet objUsersDataSet = null;
            DataTable objDataTableRoles = null;
            DataTable objDataTableBessinessentity = null;
            clsEncrypt objclsEncrypt;
            string stringPassword = "";
            string stringErrorMsg = "";
            string stringUSERID = "";
            string stringEXP = "";
            int intIndex = 0;
            try
            {
                if (ValidateControls())
                {
                    if (IsValidEmailAddress(txtMail.Text))
                    {

                        Session["ssnECom_UID"] = txtID.Text.Trim().ToUpper();
                        string stringDMLIndicator = "", stringID = "";

                        if (Session["ssnUserId"] != null)
                        {
                            stringID = Session["ssnUserId"].ToString();
                        }
                        else
                        {
                            stringID = txtID.Text.Trim().ToUpper();
                        }

                        string stringServiceType = "DEFAULT";
                        string stringexp = "";
                        objUsersDataSet = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, stringexp, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);

                        if (interrorcount == 0)
                        {

                            stringDMLIndicator = ViewState["vsDML"].ToString();

                            if (objUsersDataSet != null && objUsersDataSet.Tables.Count > 0)
                            {
                                stringEXP = "AND USR.BE_ID = '" + Session["BusinessID"].ToString() + "' AND USR.USER_ID = '" + txtID.Text.Trim().ToUpper() + "'";
                                DataSet objUsersDataSet1 = GetUsers(stringEXP);
                                if (objUsersDataSet1 != null && objUsersDataSet1.Tables.Count > 0 && objUsersDataSet1.Tables[0].Rows.Count > 0)
                                {
                                    stringDMLIndicator = "U";
                                    stringID = txtID.Text.Trim().ToUpper();

                                }

                                if (stringID.Trim().Contains(@"\"))
                                {
                                    string[] stringValues = stringID.Trim().Split('\\');
                                    if (stringValues != null && stringValues.Length > 0)
                                    {
                                        if (stringValues != null && stringValues.Length == 2)
                                        {
                                            string string1 = stringValues[0];
                                            string string2 = stringValues[1];
                                            stringUSERID = string2;

                                        }
                                    }
                                }
                                else
                                {
                                    stringUSERID = txtID.Text.Trim().ToUpper();
                                }



                                DataRow objDataRow = null;

                                /// <User Table>
                                /// 
                                if (Session["ssnUserId"] == null)
                                {


                                    objDataRow = objUsersDataSet.Tables["t1"].NewRow();

                                    objDataRow["USER_ID"] = stringID.ToUpper();
                                    objDataRow["USER_BE_ID"] = Session["BusinessID"].ToString();//need to insert not hardcore
                                    objDataRow["USER_NAME"] = txtName.Text.Trim();
                                    objDataRow["REFERENCE_1"] = stringUSERID;
                                    objDataRow["GENDER"] = "M";
                                    objclsEncrypt = new clsEncrypt(enumEncryptionAlgorithm.DES, "GSSLOGIN");
                                    stringPassword = objclsEncrypt.Encrypt(txtPwd.Text) + "ivector" + Convert.ToBase64String(objclsEncrypt.byteIV) + "end" + "DES";

                                    if (txtPwd.Text.Trim().Length > 0)
                                    {
                                        objDataRow["PASSWORD"] = stringPassword;
                                    }

                                    if (hfvPwd.Value.Trim().Length > 0)
                                    {
                                        objDataRow["PASSWORD"] = hfvPwd.Value.ToString();
                                    }

                                    objDataRow["ACCOUNT_ACTIVE_FLAG"] = "Y";
                                    if (ddlStatus.SelectedValue.Trim().Length == 0)
                                    {
                                        objDataRow["STATUS_REASON"] = ddlStatus.SelectedValue.Trim();
                                    }
                                    else
                                    {
                                        objDataRow["STATUS_REASON"] = "ACTIVE";
                                    }


                                    objDataRow["USER_TELEPHONE"] = txtTelNo.Text;
                                    objDataRow["USER_EMAIL_ID"] = txtMail.Text;


                                    objDataRow["GENDER"] = "MALE";//new
                                    objDataRow["DOMAIN"] = ddldom.SelectedValue;//new
                                    objDataRow["DESIGNATION"] = txtDesignation.Text.Trim();
                                    if (ddltDepartment.SelectedItem != null && ddltDepartment.SelectedItem.Text != "")
                                    {
                                        objDataRow["DEPARTMENTS"] = ddltDepartment.SelectedValue.ToString();
                                    } 
                                    objDataRow["DOMAIN_NAME"] = txtDomainName.Text.Trim(); 
                                    objDataRow["reference_6"] = stringDMLIndicator;
                                    objDataRow["REFERENCE_7"] = "ECOM";
                                    objDataRow["REFERENCE_5"] = "I";
                                    objDataRow["reference_8"] = "ECOMADMINPORTAL";
                                    if (ddlBctrl.SelectedValue.Trim().Length == 0)
                                    {
                                        objDataRow["reference_9"] = "NA";
                                    }
                                    else
                                    {
                                        objDataRow["reference_9"] = ddlBctrl.SelectedValue.Trim();
                                    }
                                    objDataRow["DELMARK"] = "N";
                                    objDataRow["GW_STATUS"] = "UNSEND";

                                    if (Session["stringComputerName"] != null)
                                        objDataRow["CREATED_AT"] = Session["stringComputerName"].ToString();
                                    if (Session["G11EOSUser_ID"] != null)
                                        objDataRow["CREATED_BY"] = Session["G11EOSUser_ID"].ToString();
                                    objDataRow["CREATED_ON"] = DateTime.Now;
                                    if (Session["stringComputerName"] != null)
                                        objDataRow["MODIFIED_AT"] = Session["stringComputerName"].ToString();
                                    if (Session["G11EOSUser_ID"] != null)
                                        objDataRow["MODIFIED_BY"] = Session["G11EOSUser_ID"].ToString();
                                    objDataRow["MODIFIED_ON"] = DateTime.Now;



                                    objUsersDataSet.Tables["t1"].Rows.Add(objDataRow);
                                }
                                if (Session["ssnUserId"] != null && stringProcessStatus == "SAVE")
                                {
                                    string stringexp012 = "AND USR.BE_ID = '" + Session["BusinessID"].ToString() + "' AND USR.USER_ID= '" + Session["ssnUserId"].ToString().ToUpper() + "'";
                                    objUsersDataSet = GetUsers(stringexp012);
                                    if (objUsersDataSet != null && objUsersDataSet.Tables.Count > 0 && objUsersDataSet.Tables["t1"].Rows.Count > 0)
                                    { 
                                        objUsersDataSet.Tables["t1"].Rows[0]["USER_ID"] = Session["ssnUserId"].ToString().ToUpper();
                                        objUsersDataSet.Tables["t1"].Rows[0]["USER_BE_ID"] = Session["BusinessID"].ToString();//need to insert not hardcore
                                        objUsersDataSet.Tables["t1"].Rows[0]["USER_NAME"] = txtName.Text;
                                        objUsersDataSet.Tables["t1"].Rows[0]["GENDER"] = "M";

                                        if (objUsersDataSet.Tables["t1"].Rows[0]["PASSWORD"].ToString().Length > 0)
                                        {
                                            objUsersDataSet.Tables["t1"].Rows[0]["PASSWORD"] = objUsersDataSet.Tables["t1"].Rows[0]["PASSWORD"].ToString();
                                        }
                                         
                                        objUsersDataSet.Tables["t1"].Rows[0]["ACCOUNT_ACTIVE_FLAG"] = "Y";
                                        objUsersDataSet.Tables["t1"].Rows[0]["STATUS_REASON"] = ddlStatus.SelectedValue.Trim();
                                        objUsersDataSet.Tables["t1"].Rows[0]["USER_TELEPHONE"] = txtTelNo.Text;
                                        objUsersDataSet.Tables["t1"].Rows[0]["USER_EMAIL_ID"] = txtMail.Text;
                                        objUsersDataSet.Tables["t1"].Rows[0]["REFERENCE_5"] = "U";


                                        objUsersDataSet.Tables["t1"].Rows[0]["DOMAIN"] = ddldom.SelectedValue;//new
                                        objUsersDataSet.Tables["t1"].Rows[0]["DESIGNATION"] = txtDesignation.Text.Trim();
                                        if (ddltDepartment.SelectedItem != null && ddltDepartment.SelectedItem.Text != "")
                                        {
                                            objUsersDataSet.Tables["t1"].Rows[0]["DEPARTMENTS"] = ddltDepartment.SelectedValue.ToString();
                                        }
                                        objUsersDataSet.Tables["t1"].Rows[0]["DOMAIN_NAME"] = txtDomainName.Text.Trim();//new
                                        objUsersDataSet.Tables["t1"].Rows[0]["GENDER"] = "MALE";//new 
                                        objUsersDataSet.Tables["t1"].Rows[0]["reference_6"] = stringDMLIndicator;
                                        objUsersDataSet.Tables["t1"].Rows[0]["REFERENCE_7"] = "ECOM";
                                        objUsersDataSet.Tables["t1"].Rows[0]["reference_8"] = "ECOMADMINPORTAL";
                                        if (ddlBctrl.SelectedValue.Trim().Length == 0)
                                        {
                                            objUsersDataSet.Tables["t1"].Rows[0]["reference_9"] = "NA";
                                        }
                                        else
                                        {
                                            objUsersDataSet.Tables["t1"].Rows[0]["reference_9"] = ddlBctrl.SelectedValue.Trim();
                                        }
                                        objUsersDataSet.Tables["t1"].Rows[0]["DELMARK"] = "N";
                                        objUsersDataSet.Tables["t1"].Rows[0]["GW_STATUS"] = "UNSEND";

                                        if (Session["stringComputerName"] != null)
                                            objUsersDataSet.Tables["t1"].Rows[0]["MODIFIED_AT"] = Session["stringComputerName"].ToString();
                                        if (Session["G11EOSUser_ID"] != null)
                                            objUsersDataSet.Tables["t1"].Rows[0]["MODIFIED_BY"] = Session["G11EOSUser_ID"].ToString();
                                        objUsersDataSet.Tables["t1"].Rows[0]["MODIFIED_ON"] = DateTime.Now;

                                        objUsersDataSet.Tables["t1"].Rows[0].RowState.ToString();
                                    }
                                }
                                else if (stringProcessStatus == "DELETE")
                                {
                                    string stringexp012 = "AND USR.BE_ID = '" + Session["BusinessID"].ToString() + "' AND USR.USER_ID= '" + Session["ssnUserId"].ToString().ToUpper() + "'";
                                    objUsersDataSet = GetUsers(stringexp012);
                                    if (objUsersDataSet != null && objUsersDataSet.Tables.Count > 0 && objUsersDataSet.Tables["t1"].Rows.Count > 0)
                                    {
                                        objUsersDataSet.Tables["t1"].Rows[0]["USER_ID"] = Session["ssnUserId"].ToString().ToUpper();
                                        objUsersDataSet.Tables["t1"].Rows[0]["USER_BE_ID"] = Session["BusinessID"].ToString();//need to insert not hardcore
                                        objUsersDataSet.Tables["t1"].Rows[0]["USER_NAME"] = txtName.Text;
                                        objUsersDataSet.Tables["t1"].Rows[0]["GENDER"] = "M";

                                        if (objUsersDataSet.Tables["t1"].Rows[0]["PASSWORD"].ToString().Length > 0)
                                        {
                                            objUsersDataSet.Tables["t1"].Rows[0]["PASSWORD"] = objUsersDataSet.Tables["t1"].Rows[0]["PASSWORD"].ToString();
                                        } 
                                        objUsersDataSet.Tables["t1"].Rows[0]["ACCOUNT_ACTIVE_FLAG"] = "Y";
                                        objUsersDataSet.Tables["t1"].Rows[0]["STATUS_REASON"] = ddlStatus.SelectedValue.Trim();
                                        objUsersDataSet.Tables["t1"].Rows[0]["USER_TELEPHONE"] = txtTelNo.Text;
                                        objUsersDataSet.Tables["t1"].Rows[0]["USER_EMAIL_ID"] = txtMail.Text;

                                        objUsersDataSet.Tables["t1"].Rows[0]["DOMAIN"] = ddldom.SelectedValue;//new
                                        objUsersDataSet.Tables["t1"].Rows[0]["DESIGNATION"] = txtDesignation.Text.Trim();//new
                                        if (ddltDepartment.SelectedItem != null && ddltDepartment.SelectedItem.Text != "")
                                        {
                                            objUsersDataSet.Tables["t1"].Rows[0]["DEPARTMENTS"] = ddltDepartment.SelectedValue.ToString();
                                        }
                                        objUsersDataSet.Tables["t1"].Rows[0]["DOMAIN_NAME"] = txtDomainName.Text.Trim();//new
                                        objUsersDataSet.Tables["t1"].Rows[0]["GENDER"] = "MALE";//new 
                                        objUsersDataSet.Tables["t1"].Rows[0]["reference_6"] = stringDMLIndicator;
                                        objUsersDataSet.Tables["t1"].Rows[0]["REFERENCE_7"] = "ECOM";
                                        objUsersDataSet.Tables["t1"].Rows[0]["reference_8"] = "ECOMADMINPORTAL";
                                        if (ddlBctrl.SelectedValue.Trim().Length == 0)
                                        {
                                            objUsersDataSet.Tables["t1"].Rows[0]["reference_9"] = "NA";
                                        }
                                        else
                                        {
                                            objUsersDataSet.Tables["t1"].Rows[0]["reference_9"] = ddlBctrl.SelectedValue.Trim();
                                        }
                                        objUsersDataSet.Tables["t1"].Rows[0]["DELMARK"] = "N";
                                        objUsersDataSet.Tables["t1"].Rows[0]["GW_STATUS"] = "UNSEND";
                                        objUsersDataSet.Tables["t1"].Rows[0]["REFERENCE_5"] = "D";

                                        if (Session["stringComputerName"] != null)
                                            objUsersDataSet.Tables["t1"].Rows[0]["MODIFIED_AT"] = Session["stringComputerName"].ToString();
                                        if (Session["G11EOSUser_ID"] != null)
                                            objUsersDataSet.Tables["t1"].Rows[0]["MODIFIED_BY"] = Session["G11EOSUser_ID"].ToString();
                                        objUsersDataSet.Tables["t1"].Rows[0]["MODIFIED_ON"] = DateTime.Now;

                                        objUsersDataSet.Tables["t1"].Rows[0].RowState.ToString();

                                        objUsersDataSet.AcceptChanges();
                                        objUsersDataSet.Tables["t1"].AcceptChanges();
                                        
                                    }
                                } 
                                //objUsersDataSet.Tables["t1"].Rows[0].RowState.ToString(); 
                                /// roles 
                                objDataTableRoles = (DataTable)Session["ssnRoles"];
                                if (objDataTableRoles != null && objDataTableRoles.Rows.Count > 0)
                                {
                                    //objUsersDataSet.Tables["t4"].Merge(objDataTableRoles);
                                    if (stringProcessStatus == "SAVE")
                                    {
                                        for (intIndex = 0; intIndex < cbRole.Items.Count; intIndex++)
                                        {
                                            if (cbRole.Items[intIndex].Selected && objDataTableRoles.Select("BE_ID = '" + Session["BusinessID"].ToString() + "' AND GROUP_ID='" + cbRole.Items[intIndex].Value + "'").Length == 0)
                                            {
                                                objDataRow = objUsersDataSet.Tables["t4"].NewRow();
                                                objDataRow["BE_ID"] = Session["BusinessID"].ToString();
                                                objDataRow["USER_ID"] = stringID.ToUpper();
                                                objDataRow["GROUP_ID"] = cbRole.Items[intIndex].Value;
                                                objDataRow["DELMARK"] = "N";
                                                objDataRow["REFERENCE_5"] = "I";
                                                objDataRow["GW_STATUS"] = "UNSEND";
                                                if (Session["stringComputerName"] != null)
                                                    objDataRow["CREATED_AT"] = Session["stringComputerName"].ToString();
                                                if (Session["G11EOSUser_ID"] != null)
                                                    objDataRow["CREATED_BY"] = Session["G11EOSUser_ID"].ToString();
                                                objDataRow["CREATED_ON"] = DateTime.Now;
                                                if (Session["stringComputerName"] != null)
                                                    objDataRow["MODIFIED_AT"] = Session["stringComputerName"].ToString();
                                                if (Session["G11EOSUser_ID"] != null)
                                                    objDataRow["MODIFIED_BY"] = Session["G11EOSUser_ID"].ToString();
                                                objDataRow["MODIFIED_ON"] = DateTime.Now;
                                                objUsersDataSet.Tables["t4"].Rows.Add(objDataRow);
                                            }
                                            else if (cbRole.Items[intIndex].Selected && objDataTableRoles.Select("BE_ID = '" + Session["BusinessID"].ToString() + "' AND GROUP_ID='" + cbRole.Items[intIndex].Value + "'").Length > 0)
                                            {
                                                objDataRow = objUsersDataSet.Tables["t4"].NewRow();
                                                objDataRow["BE_ID"] = Session["BusinessID"].ToString();
                                                objDataRow["USER_ID"] = stringID.ToUpper();
                                                objDataRow["GROUP_ID"] = cbRole.Items[intIndex].Value;
                                                objDataRow["DELMARK"] = "N";
                                                objDataRow["REFERENCE_5"] = "U";
                                                objDataRow["GW_STATUS"] = "UNSEND";
                                                if (Session["stringComputerName"] != null)
                                                    objDataRow["CREATED_AT"] = Session["stringComputerName"].ToString();
                                                if (Session["G11EOSUser_ID"] != null)
                                                    objDataRow["CREATED_BY"] = Session["G11EOSUser_ID"].ToString();
                                                objDataRow["CREATED_ON"] = DateTime.Now;
                                                if (Session["stringComputerName"] != null)
                                                    objDataRow["MODIFIED_AT"] = Session["stringComputerName"].ToString();
                                                if (Session["G11EOSUser_ID"] != null)
                                                    objDataRow["MODIFIED_BY"] = Session["G11EOSUser_ID"].ToString();
                                                objDataRow["MODIFIED_ON"] = DateTime.Now;
                                                objUsersDataSet.Tables["t4"].Rows.Add(objDataRow);
                                            }
                                            else if (cbRole.Items[intIndex].Selected == false && objDataTableRoles.Select("BE_ID = '" + Session["BusinessID"].ToString() + "' AND GROUP_ID='" + cbRole.Items[intIndex].Value + "'").Length > 0)
                                            {
                                                DataRowCollection = objDataTableRoles.Select("BE_ID = '" + Session["BusinessID"].ToString() + "' AND GROUP_ID='" + cbRole.Items[intIndex].Value + "'");
                                                if (DataRowCollection != null && DataRowCollection.Length > 0)
                                                {
                                                    objDataRow = objUsersDataSet.Tables["t4"].NewRow();
                                                    objDataRow["BE_ID"] = DataRowCollection[0]["BE_ID"].ToString();
                                                    objDataRow["USER_ID"] = DataRowCollection[0]["USER_ID"].ToString();
                                                    objDataRow["GROUP_ID"] = DataRowCollection[0]["GROUP_ID"].ToString();
                                                    objDataRow["DELMARK"] = "N";
                                                    objDataRow["REFERENCE_5"] = "D";
                                                    objDataRow["GW_STATUS"] = "UNSEND";
                                                    if (Session["stringComputerName"] != null)
                                                        objDataRow["CREATED_AT"] = Session["stringComputerName"].ToString();
                                                    if (Session["G11EOSUser_ID"] != null)
                                                        objDataRow["CREATED_BY"] = Session["G11EOSUser_ID"].ToString();
                                                    objDataRow["CREATED_ON"] = DateTime.Now;
                                                    if (Session["stringComputerName"] != null)
                                                        objDataRow["MODIFIED_AT"] = Session["stringComputerName"].ToString();
                                                    if (Session["G11EOSUser_ID"] != null)
                                                        objDataRow["MODIFIED_BY"] = Session["G11EOSUser_ID"].ToString();
                                                    objDataRow["MODIFIED_ON"] = DateTime.Now;
                                                    objUsersDataSet.Tables["t4"].Rows.Add(objDataRow);
                                                } 
                                            } 
                                        }
                                    }
                                    if (stringProcessStatus == "DELETE")
                                    {
                                        objUsersDataSet.Tables["t4"].AcceptChanges();
                                        foreach (DataRow row in objUsersDataSet.Tables["t4"].Rows)
                                        {
                                            objDataRow = objUsersDataSet.Tables["t4"].NewRow();
                                            objDataRow["BE_ID"] = row["BE_ID"].ToString();
                                            objDataRow["USER_ID"] = row["USER_ID"].ToString();
                                            objDataRow["GROUP_ID"] = row["GROUP_ID"].ToString();
                                            objDataRow["DELMARK"] = "N";
                                            objDataRow["REFERENCE_5"] = "D";
                                            objDataRow["GW_STATUS"] = "UNSEND";
                                            if (Session["stringComputerName"] != null)
                                                objDataRow["CREATED_AT"] = Session["stringComputerName"].ToString();
                                            if (Session["G11EOSUser_ID"] != null)
                                                objDataRow["CREATED_BY"] = Session["G11EOSUser_ID"].ToString();
                                            objDataRow["CREATED_ON"] = DateTime.Now;
                                            if (Session["stringComputerName"] != null)
                                                objDataRow["MODIFIED_AT"] = Session["stringComputerName"].ToString();
                                            if (Session["G11EOSUser_ID"] != null)
                                                objDataRow["MODIFIED_BY"] = Session["G11EOSUser_ID"].ToString();
                                            objDataRow["MODIFIED_ON"] = DateTime.Now;
                                            objUsersDataSet.Tables["t4"].Rows.Add(objDataRow);
                                        }
                                    }
                                }
                                else
                                {
                                    for (intIndex = 0; intIndex < cbRole.Items.Count; intIndex++)
                                    {
                                        if (cbRole.Items[intIndex].Selected)
                                        {
                                            objDataRow = objUsersDataSet.Tables["t4"].NewRow();
                                            objDataRow["BE_ID"] = Session["BusinessID"].ToString();
                                            objDataRow["USER_ID"] = stringID.ToUpper();
                                            objDataRow["GROUP_ID"] = cbRole.Items[intIndex].Value;
                                            objDataRow["DELMARK"] = "N";
                                            objDataRow["GW_STATUS"] = "UNSEND";
                                            objDataRow["REFERENCE_5"] = "I";
                                            if (Session["stringComputerName"] != null)
                                                objDataRow["CREATED_AT"] = Session["stringComputerName"].ToString();
                                            if (Session["G11EOSUser_ID"] != null)
                                                objDataRow["CREATED_BY"] = Session["G11EOSUser_ID"].ToString();
                                            objDataRow["CREATED_ON"] = DateTime.Now;
                                            if (Session["stringComputerName"] != null)
                                                objDataRow["MODIFIED_AT"] = Session["stringComputerName"].ToString();
                                            if (Session["G11EOSUser_ID"] != null)
                                                objDataRow["MODIFIED_BY"] = Session["G11EOSUser_ID"].ToString();
                                            objDataRow["MODIFIED_ON"] = DateTime.Now;
                                            objUsersDataSet.Tables["t4"].Rows.Add(objDataRow);
                                        }
                                    }
                                }

                                //bessentity

                                objDataTableBessinessentity = (DataTable)Session["ssnUbussinessent"];
                                if (objDataTableBessinessentity != null && objDataTableBessinessentity.Rows.Count > 0)
                                {
                                    if (stringProcessStatus == "SAVE")
                                    {
                                        for (intIndex = 0; intIndex < cbbussinessent.Items.Count; intIndex++)
                                        {
                                            string stringexp03 = "BE_ID='" + cbbussinessent.Items[intIndex].Value + "'";
                                            if (cbbussinessent.Items[intIndex].Selected && objDataTableBessinessentity.Select(stringexp03).Length > 0)
                                            {
                                                objDataRow = objUsersDataSet.Tables["t3"].NewRow();
                                                objDataRow["BE_ID"] = cbbussinessent.Items[intIndex].Value;
                                                objDataRow["USER_ID"] = stringID.ToUpper();
                                                objDataRow["DELMARK"] = "N";
                                                objDataRow["REFERENCE_5"] = "U";
                                                objDataRow["REFERENCE_1"] = cbbussinessent.Items[intIndex].Value;
                                                objDataRow["REFERENCE_2"] = cbbussinessent.Items[intIndex].Text;
                                                 
                                                objDataRow["GW_STATUS"] = "UNSEND";
                                                if (Session["stringComputerName"] != null)
                                                    objDataRow["CREATED_AT"] = Session["stringComputerName"].ToString();
                                                if (Session["G11EOSUser_ID"] != null)
                                                    objDataRow["CREATED_BY"] = Session["G11EOSUser_ID"].ToString();
                                                objDataRow["CREATED_ON"] = DateTime.Now;
                                                if (Session["stringComputerName"] != null)
                                                    objDataRow["MODIFIED_AT"] = Session["stringComputerName"].ToString();
                                                if (Session["G11EOSUser_ID"] != null)
                                                    objDataRow["MODIFIED_BY"] = Session["G11EOSUser_ID"].ToString();
                                                objDataRow["MODIFIED_ON"] = DateTime.Now;
                                                objUsersDataSet.Tables["t3"].Rows.Add(objDataRow);
                                            }
                                            else if (cbbussinessent.Items[intIndex].Selected && objDataTableBessinessentity.Select(stringexp03).Length == 0)
                                            {
                                                objDataRow = objUsersDataSet.Tables["t3"].NewRow();
                                                objDataRow["BE_ID"] = cbbussinessent.Items[intIndex].Value;
                                                objDataRow["USER_ID"] = stringID.ToUpper();
                                                objDataRow["DELMARK"] = "N";
                                                objDataRow["REFERENCE_5"] = "I";
                                                objDataRow["REFERENCE_1"] = cbbussinessent.Items[intIndex].Value;
                                                objDataRow["REFERENCE_2"] = cbbussinessent.Items[intIndex].Text;

                                                objDataRow["GW_STATUS"] = "UNSEND";
                                                if (Session["stringComputerName"] != null)
                                                    objDataRow["CREATED_AT"] = Session["stringComputerName"].ToString();
                                                if (Session["G11EOSUser_ID"] != null)
                                                    objDataRow["CREATED_BY"] = Session["G11EOSUser_ID"].ToString();
                                                objDataRow["CREATED_ON"] = DateTime.Now;
                                                if (Session["stringComputerName"] != null)
                                                    objDataRow["MODIFIED_AT"] = Session["stringComputerName"].ToString();
                                                if (Session["G11EOSUser_ID"] != null)
                                                    objDataRow["MODIFIED_BY"] = Session["G11EOSUser_ID"].ToString();
                                                objDataRow["MODIFIED_ON"] = DateTime.Now;
                                                objUsersDataSet.Tables["t3"].Rows.Add(objDataRow);
                                            }
                                            else if (cbbussinessent.Items[intIndex].Selected == false && objDataTableBessinessentity.Select("BE_ID='" + cbbussinessent.Items[intIndex].Value + "'").Length > 0)
                                            { 
                                                DataRowCollection = objDataTableBessinessentity.Select(" BE_ID='" + cbbussinessent.Items[intIndex].Value + "'");
                                                if (DataRowCollection != null && DataRowCollection.Length > 0)
                                                {
                                                    objDataRow = objUsersDataSet.Tables["t3"].NewRow();
                                                    objDataRow["BE_ID"] = DataRowCollection[0]["BE_ID"].ToString();
                                                    objDataRow["USER_ID"] = DataRowCollection[0]["USER_ID"].ToString();
                                                    objDataRow["DELMARK"] = DataRowCollection[0]["DELMARK"].ToString();
                                                    objDataRow["REFERENCE_5"] = "D";
                                                    objDataRow["REFERENCE_1"] = DataRowCollection[0]["REFERENCE_1"].ToString();
                                                    objDataRow["REFERENCE_2"] = DataRowCollection[0]["REFERENCE_2"].ToString();

                                                    objDataRow["GW_STATUS"] = "UNSEND";
                                                    if (Session["stringComputerName"] != null)
                                                        objDataRow["CREATED_AT"] = Session["stringComputerName"].ToString();
                                                    if (Session["G11EOSUser_ID"] != null)
                                                        objDataRow["CREATED_BY"] = Session["G11EOSUser_ID"].ToString();
                                                    objDataRow["CREATED_ON"] = DateTime.Now;
                                                    if (Session["stringComputerName"] != null)
                                                        objDataRow["MODIFIED_AT"] = Session["stringComputerName"].ToString();
                                                    if (Session["G11EOSUser_ID"] != null)
                                                        objDataRow["MODIFIED_BY"] = Session["G11EOSUser_ID"].ToString();
                                                    objDataRow["MODIFIED_ON"] = DateTime.Now;
                                                    objUsersDataSet.Tables["t3"].Rows.Add(objDataRow);
                                                }
                                            }
                                            
                                        }
                                    }
                                    if (stringProcessStatus == "DELETE")
                                    {
                                        objUsersDataSet.Tables["t3"].AcceptChanges();
                                        foreach (DataRow row in objUsersDataSet.Tables["t3"].Rows)
                                        {
                                            objDataRow = objUsersDataSet.Tables["t3"].NewRow();
                                            objDataRow["BE_ID"] = row["BE_ID"].ToString();
                                            objDataRow["USER_ID"] = row["USER_ID"].ToString();
                                            objDataRow["DELMARK"] = row["DELMARK"].ToString();
                                            objDataRow["REFERENCE_5"] = "D";
                                            objDataRow["REFERENCE_1"] = row["REFERENCE_1"].ToString();
                                            objDataRow["REFERENCE_2"] = row["REFERENCE_2"].ToString(); 

                                            objDataRow["GW_STATUS"] = "UNSEND";
                                            if (Session["stringComputerName"] != null)
                                                objDataRow["CREATED_AT"] = Session["stringComputerName"].ToString();
                                            if (Session["G11EOSUser_ID"] != null)
                                                objDataRow["CREATED_BY"] = Session["G11EOSUser_ID"].ToString();
                                            objDataRow["CREATED_ON"] = DateTime.Now;
                                            if (Session["stringComputerName"] != null)
                                                objDataRow["MODIFIED_AT"] = Session["stringComputerName"].ToString();
                                            if (Session["G11EOSUser_ID"] != null)
                                                objDataRow["MODIFIED_BY"] = Session["G11EOSUser_ID"].ToString();
                                            objDataRow["MODIFIED_ON"] = DateTime.Now;
                                            objUsersDataSet.Tables["t3"].Rows.Add(objDataRow);
                                        }
                                    }
                                }
                                else
                                {
                                    for (intIndex = 0; intIndex < cbbussinessent.Items.Count; intIndex++)
                                    {
                                        if (cbbussinessent.Items[intIndex].Selected)
                                        {
                                            objDataRow = objUsersDataSet.Tables["t3"].NewRow();
                                            objDataRow["BE_ID"] = cbbussinessent.Items[intIndex].Value;
                                            objDataRow["USER_ID"] = stringID.ToUpper();
                                            objDataRow["REFERENCE_1"] = cbbussinessent.Items[intIndex].Value;
                                            objDataRow["REFERENCE_2"] = cbbussinessent.Items[intIndex].Text;
                                            objDataRow["DELMARK"] = "N";
                                            objDataRow["REFERENCE_5"] = "I";
                                            objDataRow["GW_STATUS"] = "UNSEND";
                                            if (Session["stringComputerName"] != null)
                                                objDataRow["CREATED_AT"] = Session["stringComputerName"].ToString();
                                            if (Session["G11EOSUser_ID"] != null)
                                                objDataRow["CREATED_BY"] = Session["G11EOSUser_ID"].ToString();
                                            objDataRow["CREATED_ON"] = DateTime.Now;
                                            if (Session["stringComputerName"] != null)
                                                objDataRow["MODIFIED_AT"] = Session["stringComputerName"].ToString();
                                            if (Session["G11EOSUser_ID"] != null)
                                                objDataRow["MODIFIED_BY"] = Session["G11EOSUser_ID"].ToString();
                                            objDataRow["MODIFIED_ON"] = DateTime.Now;
                                            objUsersDataSet.Tables["t3"].Rows.Add(objDataRow);
                                        }
                                    }
                                }

                                //Application


                                DataTable objdatatablecustumersession = (DataTable)Session["Applicationtable"];
                                if (objdatatablecustumersession != null && objdatatablecustumersession.Rows.Count > 0)
                                {
                                    //objUsersDataSet.Tables["t5"].Merge(objdatatablecustumersession);
                                    if (stringProcessStatus == "SAVE")
                                    {
                                        for (intIndex = 0; intIndex < cbapplicationId.Items.Count; intIndex++)
                                        {
                                            if (cbapplicationId.Items[intIndex].Selected && objdatatablecustumersession.Select("BE_ID = '" + Session["BusinessID"].ToString() + "' AND APP_ID='" + cbapplicationId.Items[intIndex].Text + "'").Length == 0)
                                            {
                                                objDataRow = objUsersDataSet.Tables["t5"].NewRow(); 
                                                objDataRow["BE_ID"] = Session["BusinessID"].ToString();
                                                objDataRow["USER_ID"] = stringID.ToUpper();
                                                objDataRow["DELMARK"] = "N";
                                                objDataRow["REFERENCE_1"] = cbapplicationId.Items[intIndex].Value;
                                                objDataRow["APP_ID"] = cbapplicationId.Items[intIndex].Value;
                                                objDataRow["REFERENCE_5"] = "I";

                                                objDataRow["GW_STATUS"] = "UNSEND";
                                                if (Session["stringComputerName"] != null)
                                                    objDataRow["CREATED_AT"] = Session["stringComputerName"].ToString();
                                                if (Session["G11EOSUser_ID"] != null)
                                                    objDataRow["CREATED_BY"] = Session["G11EOSUser_ID"].ToString();
                                                objDataRow["CREATED_ON"] = DateTime.Now;
                                                if (Session["stringComputerName"] != null)
                                                    objDataRow["MODIFIED_AT"] = Session["stringComputerName"].ToString();
                                                if (Session["G11EOSUser_ID"] != null)
                                                    objDataRow["MODIFIED_BY"] = Session["G11EOSUser_ID"].ToString();
                                                objDataRow["MODIFIED_ON"] = DateTime.Now;
                                                objUsersDataSet.Tables["t5"].Rows.Add(objDataRow);
                                            }
                                            else if (cbapplicationId.Items[intIndex].Selected && objdatatablecustumersession.Select("BE_ID = '" + Session["BusinessID"].ToString() + "' AND APP_ID='" + cbapplicationId.Items[intIndex].Text + "'").Length > 0)
                                            {
                                                objDataRow = objUsersDataSet.Tables["t5"].NewRow();
                                                objDataRow["BE_ID"] = Session["BusinessID"].ToString();
                                                objDataRow["USER_ID"] = stringID.ToUpper();
                                                objDataRow["DELMARK"] = "N";
                                                objDataRow["REFERENCE_1"] = cbapplicationId.Items[intIndex].Value;
                                                objDataRow["APP_ID"] = cbapplicationId.Items[intIndex].Value;
                                                objDataRow["REFERENCE_5"] = "U";

                                                objDataRow["GW_STATUS"] = "UNSEND";
                                                if (Session["stringComputerName"] != null)
                                                    objDataRow["CREATED_AT"] = Session["stringComputerName"].ToString();
                                                if (Session["G11EOSUser_ID"] != null)
                                                    objDataRow["CREATED_BY"] = Session["G11EOSUser_ID"].ToString();
                                                objDataRow["CREATED_ON"] = DateTime.Now;
                                                if (Session["stringComputerName"] != null)
                                                    objDataRow["MODIFIED_AT"] = Session["stringComputerName"].ToString();
                                                if (Session["G11EOSUser_ID"] != null)
                                                    objDataRow["MODIFIED_BY"] = Session["G11EOSUser_ID"].ToString();
                                                objDataRow["MODIFIED_ON"] = DateTime.Now;
                                                objUsersDataSet.Tables["t5"].Rows.Add(objDataRow);
                                            }
                                            else if (cbapplicationId.Items[intIndex].Selected == false && objdatatablecustumersession.Select("BE_ID = '" + Session["BusinessID"].ToString() + "' AND APP_ID='" + cbapplicationId.Items[intIndex].Text + "'").Length > 0)
                                            { 
                                                DataRowCollection = objdatatablecustumersession.Select("BE_ID = '" + Session["BusinessID"].ToString() + "' AND APP_ID='" + cbapplicationId.Items[intIndex].Text + "'");
                                                if (DataRowCollection != null && DataRowCollection.Length > 0)
                                                {
                                                    objDataRow = objUsersDataSet.Tables["t5"].NewRow();
                                                    objDataRow["BE_ID"] = DataRowCollection[0]["BE_ID"].ToString();
                                                    objDataRow["USER_ID"] = DataRowCollection[0]["USER_ID"].ToString();
                                                    objDataRow["DELMARK"] = "N";
                                                    objDataRow["REFERENCE_1"] = DataRowCollection[0]["REFERENCE_1"].ToString();
                                                    objDataRow["APP_ID"] = DataRowCollection[0]["APP_ID"].ToString();
                                                    objDataRow["REFERENCE_5"] = "U";

                                                    objDataRow["GW_STATUS"] = "UNSEND";
                                                    if (Session["stringComputerName"] != null)
                                                        objDataRow["CREATED_AT"] = Session["stringComputerName"].ToString();
                                                    if (Session["G11EOSUser_ID"] != null)
                                                        objDataRow["CREATED_BY"] = Session["G11EOSUser_ID"].ToString();
                                                    objDataRow["CREATED_ON"] = DateTime.Now;
                                                    if (Session["stringComputerName"] != null)
                                                        objDataRow["MODIFIED_AT"] = Session["stringComputerName"].ToString();
                                                    if (Session["G11EOSUser_ID"] != null)
                                                        objDataRow["MODIFIED_BY"] = Session["G11EOSUser_ID"].ToString();
                                                    objDataRow["MODIFIED_ON"] = DateTime.Now;
                                                    objUsersDataSet.Tables["t5"].Rows.Add(objDataRow);
                                                }
                                            }
                                             
                                        }
                                    }
                                    if (stringProcessStatus == "DELETE")
                                    {
                                        objUsersDataSet.Tables["t5"].AcceptChanges();
                                        foreach (DataRow row in objUsersDataSet.Tables["t5"].Rows)
                                        {
                                            objDataRow = objUsersDataSet.Tables["t5"].NewRow();
                                            objDataRow["BE_ID"] = row["BE_ID"].ToString();
                                            objDataRow["USER_ID"] = row["USER_ID"].ToString();
                                            objDataRow["DELMARK"] = "N";
                                            objDataRow["REFERENCE_1"] = row["REFERENCE_1"].ToString();
                                            objDataRow["APP_ID"] = row["APP_ID"].ToString();
                                            objDataRow["REFERENCE_5"] = "D";

                                            objDataRow["GW_STATUS"] = "UNSEND";
                                            if (Session["stringComputerName"] != null)
                                                objDataRow["CREATED_AT"] = Session["stringComputerName"].ToString();
                                            if (Session["G11EOSUser_ID"] != null)
                                                objDataRow["CREATED_BY"] = Session["G11EOSUser_ID"].ToString();
                                            objDataRow["CREATED_ON"] = DateTime.Now;
                                            if (Session["stringComputerName"] != null)
                                                objDataRow["MODIFIED_AT"] = Session["stringComputerName"].ToString();
                                            if (Session["G11EOSUser_ID"] != null)
                                                objDataRow["MODIFIED_BY"] = Session["G11EOSUser_ID"].ToString();
                                            objDataRow["MODIFIED_ON"] = DateTime.Now;
                                            objUsersDataSet.Tables["t5"].Rows.Add(objDataRow);
                                        }
                                    }
                                }
                                else
                                {
                                    for (intIndex = 0; intIndex < cbapplicationId.Items.Count; intIndex++)
                                    {
                                        if (cbapplicationId.Items[intIndex].Selected)
                                        {
                                            objDataRow = objUsersDataSet.Tables["t5"].NewRow(); 
                                            objDataRow["BE_ID"] = Session["BusinessID"].ToString();
                                            objDataRow["USER_ID"] = stringID.ToUpper();
                                            objDataRow["APP_ID"] = cbapplicationId.Items[intIndex].Value;  
                                            objDataRow["REFERENCE_5"] = "I";
                                            objDataRow["REFERENCE_1"] = cbapplicationId.Items[intIndex].Value;
                                            objDataRow["DELMARK"] = "N";
                                            objDataRow["GW_STATUS"] = "UNSEND";
                                            if (Session["stringComputerName"] != null)
                                                objDataRow["CREATED_AT"] = Session["stringComputerName"].ToString();
                                            if (Session["G11EOSUser_ID"] != null)
                                                objDataRow["CREATED_BY"] = Session["G11EOSUser_ID"].ToString();
                                            objDataRow["CREATED_ON"] = DateTime.Now;
                                            if (Session["stringComputerName"] != null)
                                                objDataRow["MODIFIED_AT"] = Session["stringComputerName"].ToString();
                                            if (Session["G11EOSUser_ID"] != null)
                                                objDataRow["MODIFIED_BY"] = Session["G11EOSUser_ID"].ToString();
                                            objDataRow["MODIFIED_ON"] = DateTime.Now;
                                            objUsersDataSet.Tables["t5"].Rows.Add(objDataRow);
                                        }
                                    }
                                }


                                objUsersDataSet.AcceptChanges();
                                for (int intIndex01 = 0; intIndex01 < objUsersDataSet.Tables.Count; intIndex01++)
                                {
                                    DataRow[] objdatarowState = objUsersDataSet.Tables[intIndex01].Select("REFERENCE_5 = 'D'");
                                    if (objdatarowState != null && objdatarowState.Length > 0)
                                    {
                                        for (int intIndex1 = 0; intIndex1 < objdatarowState.Length; intIndex1++)
                                        {
                                            objdatarowState[intIndex1].Delete();
                                        }
                                    }

                                    DataRow[] objdatarowState1 = objUsersDataSet.Tables[intIndex01].Select("REFERENCE_5 = 'U'");
                                    if (objdatarowState1 != null && objdatarowState1.Length > 0)
                                    {
                                        for (int intIndex2 = 0; intIndex2 < objdatarowState1.Length; intIndex2++)
                                        {
                                            objdatarowState1[intIndex2].SetModified();
                                        }
                                    }
                                    DataRow[] objdatarowState2 = objUsersDataSet.Tables[intIndex01].Select("REFERENCE_5 = 'I'");
                                    if (objdatarowState2 != null && objdatarowState2.Length > 0)
                                    {
                                        for (int intIndex3 = 0; intIndex3 < objdatarowState2.Length; intIndex3++)
                                        {

                                            objdatarowState2[intIndex3].SetAdded();
                                        }
                                    }
                                } 
                                objDatasetResult = objUsersDataSet.GetChanges();
                                stringServiceType = "InterfaceService_DataManipulationR1V1";
                                objDatasetResult = CommonFunctions.DataManipulationR1V1(stringServiceType, objUsersDataSet.GetChanges(), stringformid, out int intErrorCount, out stringOutputResult);



                                if (intErrorCount != 0)
                                {
                                    CommonFunctions.ShowMessageboot(this, (stringOutputResult != null && stringOutputResult.Length > 2) ? stringOutputResult[1] : null);
                                }
                                else
                                {
                                    if (Session["ssnUserId"] == null && stringProcessStatus == "SAVE")
                                    {
                                        CommonFunctions.ShowMessageboot(this, "Record Created Successfully.");
                                        lnkDelete.Visible = true;
                                    }
                                    else if (Session["ssnUserId"] != null && stringProcessStatus == "SAVE")
                                    {
                                        CommonFunctions.ShowMessageboot(this, "Record Updated Successfully.");
                                    }
                                    else if (stringProcessStatus == "DELETE")
                                    {
                                        CommonFunctions.ShowMessageboot(this, "Record Deleted Successfully.");
                                        lbtnNew_Click(null, null);
                                    }
                                    ClearData();
                                    Session["Applicationtable"] = null;
                                    Loadusers(stringID, Session["BusinessID"].ToString());
                                }
                            }
                        }
                        else
                        {
                            CommonFunctions.ShowMessageboot(this, stringOutputResult[0]);
                        }

                    }
                    else
                    {
                        stringErrorMsg = "Please Enter the Valid Email";
                        CommonFunctions.ShowMessageboot(this, stringErrorMsg);
                    }

                }
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
        }


        private bool ValidateControls()
        {
            bool boolStatus = true;
            string stringOverallMsg = "You must enter the value for the following fields:- " + "\\r\\n";

            if (txtID.Text.Trim().Length == 0)
            {
                stringOverallMsg += "- User ID" + "\\r\\n";
                boolStatus = false;
            }

            if (txtName.Text.Trim().Length == 0)
            {
                stringOverallMsg += "- User Name" + "\\r\\n";
                boolStatus = false;
            }
            if(ddldom.SelectedItem != null && ddldom.SelectedValue.Length > 0)
            {
                string stringDomain = ddldom.SelectedValue;
                if(stringDomain == "DOMAIN" && txtDomainName.Text.Length == 0)
                {
                    stringOverallMsg += "- Domain Name" + "\\r\\n";
                    boolStatus = false;
                }
            } 
            if (txtPwd.Text.Trim().Length == 0)
            {
                stringOverallMsg += "- New Pasword" + "\\r\\n";
                boolStatus = false;
            }

            if (txtCPwd.Text.Trim().Length == 0)
            {
                stringOverallMsg += "- Confirm Pasword" + "\\r\\n";
                boolStatus = false;
            }
            if (ddlStatus.Text.Trim().Length == 0)
            {
                stringOverallMsg += "- Status" + "\\r\\n";
                boolStatus = false;
            }
            //if (ddltDepartment.Text.Trim().Length == 0)
            //{
            //    stringOverallMsg += "- Department" + "\\r\\n";
            //    boolStatus = false;
            //}

            if (!boolStatus)
            {
                if (stringOverallMsg.Trim().Length > 0)
                {
                    stringOverallMsg = stringOverallMsg.Trim();
                    CommonFunctions.ShowMessageboot(this, stringOverallMsg);
                    return false;
                }
            }

            return true;
        }
        private bool ValidateControlsDomain()
        {
            bool boolStatus = true;
            string stringOverallMsg = "You must enter the value for the following fields:- " + "\\r\\n";


            if (txtPwd.Text.Trim().Length == 0)
            {
                stringOverallMsg += "- New Pasword" + "\\r\\n";
                boolStatus = false;
            }

            if (txtCPwd.Text.Trim().Length == 0)
            {
                stringOverallMsg += "- Confirm Pasword" + "\\r\\n";
                boolStatus = false;
            }

            if (!boolStatus)
            {
                if (stringOverallMsg.Trim().Length > 0)
                {
                    stringOverallMsg = stringOverallMsg.Trim();
                    CommonFunctions.ShowMessageboot(this, stringOverallMsg);
                    return false;
                }
            }

            return true;
        }

        private bool ValidateControlsPASSWORD()
        {
            string stringOverallMsg = "New Password and Confirm Password Not Matched";


            if (txtPwd.Text.Trim().Length >= 0 && txtCPwd.Text.Trim().Length >= 0)
            {
                if (txtCPwd.Text.Trim() != txtPwd.Text.Trim())
                {
                    CommonFunctions.ShowMessageboot(this, stringOverallMsg);
                    return false;
                }

            }


            return true;
        }
        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserSummery.aspx");
        }
        protected void lbtnClear_Click(object sender, EventArgs e)
        {
            ClearValues();
        }

        #endregion
        #region Footer
        protected void lnNew_Click(object sender, EventArgs e)
        {
            ClearValues();
        }
        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            string stringDomain = "";

            if (ddldom.SelectedItem != null)
            {
                stringDomain = ddldom.SelectedItem.Value;

                if (stringDomain.Length > 0 && stringDomain == "LOCAL")
                {
                    if (ValidateControlsDomain())
                    {
                        if (ValidateControlsPASSWORD())
                        {
                            SaveRecord1("DELETE");
                        }
                    }
                }
                else
                {

                }
            }
            else
            {
                SaveRecord1("DELETE");

            }
        }

        #endregion
        #region CLEARVALUES
        private void ClearValues()//fixed
        {
            try
            {
                ViewState["vsDML"] = "I";
                Session["Applicationtable"] = null;
                Session["ssnRoles"] = null;
                Session["ssnUbussinessent"] = null;
                Session["ssnUserId"] = null;


                txtID.Text = txtName.Text = txtMail.Text = txtTelNo.Text = "";
                hfvPwd.Value = "";
                hfvCPwd.Value = "";
                txtPwd.Attributes.Add("value", "");
                txtCPwd.Attributes.Add("value", "");
                cbRole.ClearSelection();
                cbbussinessent.ClearSelection();
                ddlStatus.ClearSelection();
                chkbxAReqd.Checked = false;
                ScriptManager.RegisterStartupScript(this, GetType(), "function", "activatetab('#roles');", true);
                txtID.Enabled = true;
                txtID.Focus();
                ddlBctrl.ClearSelection();
                txtPwd.Enabled = true;
                txtCPwd.Enabled = true;

                //newly added columns
                ddldom.ClearSelection();
                ddltDepartment.ClearSelection();
                txtDesignation.Text = ""; 
                txtDomainName.Text = "";

            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
        }

        private void ClearData()//fixed
        {
            ViewState["vsDML"] = "I";



            Session["ssnRoles"] = null;
            Session["ssnUbussinessent"] = null;
            Session["Applicationtable"] = null;
        }
        #endregion
        private void Loadusers(string StringUserid,string stringBEid)//ok
        {
            DataSet objDataSet = null;
            int interrorcount = 0;
            int intTotalRecord = 0;
            string[] stringOutputResult = null;
            string stringformid = "FAD1011R1V1";
            string stringOrderBy = "";
            int intFromRecord = 0;
            int intToRecord = int.MaxValue;
            string stringexp = "";
            DataRow objdatarowcollection = null;
            try
            {
                stringexp = "AND USR.BE_ID='" + stringBEid.ToString().ToUpper() + "' AND USR.USER_ID='" + StringUserid.ToString().ToUpper() + "'";
                //stringexp = "AND USER_ID='" + StringUserid.ToString().ToUpper() + "'";
                DataSet objdatasetdtusers = GetUsers(stringexp);
                if (objdatasetdtusers != null && objdatasetdtusers.Tables.Count > 0 && objdatasetdtusers.Tables[0] != null && objdatasetdtusers.Tables[0].Rows.Count > 0)
                {
                    objdatarowcollection = objdatasetdtusers.Tables[0].Rows[0];
                    if (objdatarowcollection != null)
                    {
                        txtID.Text = objdatarowcollection["USER_ID"].ToString().ToUpper();
                        txtID.Enabled = false;

                        txtName.Text = objdatarowcollection["USER_NAME"].ToString();
                        txtMail.Text = objdatarowcollection["USER_EMAIL_ID"].ToString();

                        txtPwd.Attributes.Add("value", objdatarowcollection["PASSWORD"].ToString());
                        txtCPwd.Attributes.Add("value", objdatarowcollection["PASSWORD"].ToString());

                        txtTelNo.Text = objdatarowcollection["USER_TELEPHONE"].ToString();
                        if (objdatarowcollection["STATUS_REASON"].ToString() != "")
                        {
                            ddlStatus.SelectedValue = objdatarowcollection["STATUS_REASON"].ToString();

                        }

                        if (objdatarowcollection["DESIGNATION"].ToString() != "")
                        {
                            txtDesignation.Text = objdatarowcollection["DESIGNATION"].ToString();//new
                        }
                        string stringDEPARTMENTS = objdatarowcollection["DEPARTMENTS"].ToString();
                        if (ddltDepartment.Items.FindByValue(stringDEPARTMENTS) != null)
                        {
                            ddltDepartment.ClearSelection();
                            ddltDepartment.Items.FindByValue(stringDEPARTMENTS).Selected = true;
                        } 
                        if (objdatarowcollection["DOMAIN_NAME"].ToString() != "")
                        {
                            txtDomainName.Text = objdatarowcollection["DOMAIN_NAME"].ToString();//new
                        }

                        if (objdatarowcollection["DOMAIN"].ToString() != "")
                        {
                            ddldom.SelectedValue = objdatarowcollection["DOMAIN"].ToString();//new
                        }


                    }
                    //roles

                    DataTable dtroles = null;
                    string stringServiceType = "List4R1v1AdminServiceClient";
                    string stringexp01 = " AND USER_ID = '" + StringUserid.ToString().ToUpper() + "'";


                    objDataSet = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, stringexp01, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);

                    if (interrorcount == 0)
                    {
                        if (objDataSet != null && objDataSet.Tables.Count > 0 && objDataSet.Tables["t4"] != null && objDataSet.Tables["t4"].Rows.Count > 0)
                        {
                            dtroles = objDataSet.Tables["t4"];
                            Session["ssnRoles"] = objDataSet.Tables["t4"];
                        }


                        if (dtroles != null)
                        {
                            DataTable objdatatablerole1 = (DataTable)Session["userroleforload"];

                            cbRole.DataTextField = "ACCESS_GRP_ID";
                            cbRole.DataValueField = "ACCESS_GRP_ID";
                            cbRole.DataSource = objdatatablerole1;
                            cbRole.DataBind();

                            int intIndex = 0;


                            cbRole.ClearSelection();
                            for (intIndex = 0; intIndex < dtroles.Rows.Count; intIndex++)
                            {
                                string stringroleid = dtroles.Rows[intIndex]["GROUP_ID"].ToString().Trim().ToUpper();


                                if (cbRole.Items.FindByValue(stringroleid) != null)
                                    cbRole.Items.FindByValue(stringroleid).Selected = true;
                            }

                            ftUserRoles.Visible = true;
                            ftUserDetail.Visible = true;

                        }

                        //application

                        DataTable dtapplication = null;
                        string stringServiceType1 = "List5R1v1AdminServiceClient";
                        string stringexp02 = "  AND USER_ID = '" + StringUserid.ToString().ToUpper() + "'";


                        objDataSet = CommonFunctions.SelectionServiceClient(stringServiceType1, stringformid, stringexp02, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);

                        if (interrorcount == 0)
                        {

                            if (objDataSet != null && objDataSet.Tables.Count > 0 && objDataSet.Tables["t5"] != null && objDataSet.Tables["t5"].Rows.Count > 0)
                            {
                                dtapplication = objDataSet.Tables["t5"];
                                Session["Applicationtable"] = objDataSet.Tables["t5"];
                            }


                            if (dtapplication != null)
                            {
                                DataTable objdatatableapp = (DataTable)Session["Appidforload"];
                                cbapplicationId.DataValueField = "BE_ID";
                                cbapplicationId.DataTextField = "BE_ID";
                                cbapplicationId.DataSource = objdatatableapp;
                                cbapplicationId.DataBind();

                                int intIndex = 0;

                                for (intIndex = 0; intIndex < dtapplication.Rows.Count; intIndex++)
                                {
                                    if (cbapplicationId.Items.FindByValue(dtapplication.Rows[intIndex]["APP_ID"].ToString()) != null)
                                    {
                                        cbapplicationId.Items.FindByValue(dtapplication.Rows[intIndex]["APP_ID"].ToString()).Selected = true;
                                    }
                                }

                            }




                            //bussinessentity AND BE_ID='" + stringBEid.ToString().ToUpper() + "'

                            DataTable dtbeid = null;
                            string stringServiceType2 = "List3R1v1AdminServiceClient";
                            string stringexp03 = " AND USER_ID = '" + StringUserid.ToString().ToUpper() + "'";


                            objDataSet = CommonFunctions.SelectionServiceClient(stringServiceType2, stringformid, stringexp03, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);

                            if (interrorcount == 0)
                            {
                                if (objDataSet != null && objDataSet.Tables.Count > 0 && objDataSet.Tables["t3"] != null && objDataSet.Tables["t3"].Rows.Count > 0)
                                {
                                    dtbeid = objDataSet.Tables["t3"];
                                    Session["ssnUbussinessent"] = objDataSet.Tables["t3"];
                                }


                                if (dtbeid != null)
                                {
                                    DataTable objdatatablebe = (DataTable)Session["beidforload"];
                                    cbbussinessent.DataValueField = "INS_ID";
                                    cbbussinessent.DataTextField = "INS_NAME";
                                    cbbussinessent.DataSource = objdatatablebe;
                                    cbbussinessent.DataBind();

                                    int intIndex = 0;


                                    for (intIndex = 0; intIndex < dtbeid.Rows.Count; intIndex++)
                                    {
                                        if (cbbussinessent.Items.FindByValue(dtbeid.Rows[intIndex]["BE_ID"].ToString()) != null)
                                        {
                                            cbbussinessent.Items.FindByValue(dtbeid.Rows[intIndex]["BE_ID"].ToString()).Selected = true;
                                        }
                                    }
                                }


                                Session["ssnUserId"] = txtID.Text.Trim().ToUpper();
                            }
                            else
                            {
                                CommonFunctions.ShowMessageboot(this, stringOutputResult[0]);
                            }
                        }
                        else
                        {
                            CommonFunctions.ShowMessageboot(this, stringOutputResult[0]);
                        }
                    }
                    else
                    {
                        CommonFunctions.ShowMessageboot(this, stringOutputResult[0]);
                    }

                }
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessage(objException);
            }

        }
        private void LoadRecord(string Condition = "", string SortExpression = null)//need fn
        {
            try
            {
                Condition = "";
                Condition = " and Upper(CREATED_BY) = Upper('" + Session["ssnECom_UID"].ToString() + "') or Upper(USER_ID) = Upper('" + Session["ssnECom_UID"].ToString() + "')";

                ClearValues();

            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
        }

        public DataSet GetUsers(string stringCondition)//need fn
        {
            DataSet objDataSet = null;
            int interrorcount = 0;
            int intTotalRecord = 0;
            string[] stringOutputResult = null;
            string stringformid = "FAD1011R1V1";
            string stringOrderBy = "";
            int intFromRecord = 0;
            int intToRecord = int.MaxValue;
            try
            {

                string stringServiceType = "List1R1v1AdminServiceClient";
                objDataSet = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, stringCondition, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);

                if (interrorcount == 0)
                {
                    if (objDataSet != null && objDataSet.Tables.Count > 0)
                    {
                        return objDataSet;
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



            return null;
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