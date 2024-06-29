using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;
using System.IO;
using System.Configuration;
using System.Globalization;
using System.Text;
using System.Data.OleDb;
using System.Diagnostics;
using System.Windows;
using System.Net;
using Gurusoft.ExceptionManager;
using System.Collections.Specialized;
using System.ServiceModel;
using System.Xml;
using System;
using PRDG11EOSV03WB.CommonFunction;
using G11EOSMW.EncyptionManager;

namespace PRDG11EOSV03WB
{
    public partial class SiteMaster : MasterPage
    { 
        public DataSet objDatasetAppsVariables;
         
        protected void master_Page_PreLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               
            }
           
        }
        //  For loader enable during report download
        public string assignByteArrayString
        {
            get { return txtByteArray.Text; }
            set { txtByteArray.Text = value; }
        }
        public string assignDownloadFilename
        {
            get { return txtFilename.Text; }
            set { txtFilename.Text = value; }
        }
        public string assignDownloadFileExtension
        {
            get { return txtExtension.Text; }
            set { txtExtension.Text = value; }
        } 
        protected void Page_Load(object sender, EventArgs e)
        {
            if (CommonFunctions.IsActive())
            {
                try
                { 
                    if (!IsPostBack)
                    {
                        Session["boolIsChangeCustomer"] = false;

                        if (Session["G11EOSUser_ID"] != null && Session["BusinessID"] != null)
                        { 
                            string stringusername = Session["G11EOSUser_Name"].ToString();
                            spnUserName.InnerText = stringusername + "  " + "(" + Session["BusinessID"].ToString() + ")";
                        }
                         
                    }
                    else
                    {

                    }
                }
                catch (Exception objException)
                {
                    CommonFunctions.ShowMessage(objException);
                }
            }

        }

      
        protected void Page_PreRender(object sender, EventArgs e)
        {
            
            if (CommonFunctions.IsActive())
            {
                DataTable objdatatablerole = null;
                try
                {
                    if (Session["ssnboolUserValidation"] != null && !(bool)Session["ssnboolUserValidation"])
                    {
                        string stringusername = Session["G11EOSUser_ID"].ToString();

                        objdatatablerole = (DataTable)Session["UserRolestable"];
                        if (objdatatablerole != null && objdatatablerole.Rows.Count > 0)
                        {
                            if (objdatatablerole.Select("Group_ID='ADMIN'").Length > 0 || objdatatablerole.Select("Group_ID='IDADMIN'").Length > 0)
                            {
                                Session["ssnIsAdmin"] = "Y";
                                User.Visible = true;
                            }
                            else
                            {
                                Session["ssnIsAdmin"] = "N";
                                User.Visible = false;
                            }

                        }
                        else
                        {
                            Session["ssnIsAdmin"] = "N";
                            User.Visible = false;
                          
                        }
                    }
                    else
                    {
                        Session["ssnIsAdmin"] = "Y";
                    }
                    if (Session["ssnUserRole"] != null && Session["ssnUserRole"].ToString().ToUpper() == "USER")
                    {
                        User.Visible = false;                       
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "Func()", true);
                    }
                    else if (Session["ssnUserRole"] != null &&  Session["ssnUserRole"].ToString().ToUpper() == "POWER USER")
                    {
                        User.Visible = false;
                        Li1.Visible = false;
                    }
                }
                catch (Exception objException)
                {
                    CommonFunctions.ShowMessage(objException);
                }
            }
        }
        public bool test
        {
            get
            {
                return up.Visible;
            }
            set
            {
                up.Visible = value;
            }
        }
       
        protected void btnChangePassword_Click(object sender, EventArgs e)
        {
            string stringErrorMsg = "";
            string[] stringResult = new string[3];

            try
            {
                if (txtOldPassword.Text.Length > 0)
                {
                    if (txtNewPassword.Text.Length > 0)
                    {
                        if (txtOldPassword.Text.Length > 0)
                        {
                            if (txtConfirmPassword.Text.Trim() == txtNewPassword.Text.Trim())
                            {
                                ResetPassword();

                            }
                            else
                            {
                                stringErrorMsg = " New Password and Confirm Password Not Matched";
                                CommonFunctions.ShowMessage(stringErrorMsg);
                                // CommonFunctions.ShowMessageboot(this, stringErrorMsg);
                            }
                        }
                        else
                        {
                            CommonFunctions.ShowMessage("Please Enter Old Password");
                            //CommonFunctions.ShowMessageboot(this, "Please Enter Old Password");
                        }
                    }
                    else
                    {
                        CommonFunctions.ShowMessage("Please Enter New Password");
                        //CommonFunctions.ShowMessageboot(this, "Please Enter New Password");
                    }
                }
                else
                {
                    CommonFunctions.ShowMessage("Please Enter Confirm Password");
                   // CommonFunctions.ShowMessageboot(this, "Please Enter Confirm Password");
                }


            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessage(objException);
            }
            finally
            {

            }
        }

        private void ResetPassword()
        {
            int interrorcount = 0;
            string[] stringOutputResult = null;
            clsEncrypt objclsEncrypt;
            objDatasetAppsVariables = (DataSet)Session["objDatasetlocaldeclaration"];
            objDatasetAppsVariables.Tables[0].Rows[0]["FORM_ID"] = "FAD1011R1V1";

            string stringoldPassword, stringnewPassword;
            try
            {
                if (Page.IsValid)
                {
                    if (txtOldPassword.Text.Length > 0 && txtNewPassword.Text.Length > 0 && txtOldPassword.Text.Length > 0)
                    {

                        objclsEncrypt = new clsEncrypt(enumEncryptionAlgorithm.DES, "GSSLOGIN");
                        stringoldPassword = objclsEncrypt.Encrypt(txtOldPassword.Text) + "ivector" + Convert.ToBase64String(objclsEncrypt.byteIV) + "end" + "DES";
                        stringnewPassword = objclsEncrypt.Encrypt(txtNewPassword.Text) + "ivector" + Convert.ToBase64String(objclsEncrypt.byteIV) + "end" + "DES";

                        using (G11EISMWSelectionService.SelectionServiceClient objSelectionServiceClient = new G11EISMWSelectionService.SelectionServiceClient(CommonFunctions.CreateServiceBasicHttpBinding(), CommonFunctions.objEndpointAddressSelectionService))
                        {
                          //  stringOutputResultr = objSelectionServiceClient.ChangePasswordR1V2(stringoldPassword, stringnewPassword, objDatasetAppsVariables, out interrorcount, out stringOutputResult);
                            if (objSelectionServiceClient != null)
                                objSelectionServiceClient.Close();
                        }

                        if (interrorcount == 0)
                        {
                            CommonFunctions.ShowMessage("Password Successfully Changed.");
                           // CommonFunctions.ShowMessageboot(this, "Password Successfully Changed.");
                        }
                        else
                        {
                            CommonFunctions.ShowMessage(stringOutputResult[2]);
                            //CommonFunctions.ShowMessageboot(this, stringOutputResult[2].ToString());
                        }
                    }



                }
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessage(objException);
               // CommonFunctions.ShowMessageboot02(objException);
            }
        }
        protected void lnkbtnusercreation_Click(object sender, EventArgs e)
        {
            try
            { 
                    Response.Redirect("UserScreenCluster.aspx");
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }

        }

        protected void lnkbtnUSerRole_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserRoleHistory.aspx"); 
        }
         

        protected void lnkbtnlogout_Click(object sender, EventArgs e)
        {
            Session["ssnLoggedInFlag"] = "N";
            Response.Redirect("Login.aspx");

        }
    }

}