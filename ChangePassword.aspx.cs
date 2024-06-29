using PRDG11EOSV03WB.CommonFunction;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using G11EOSMW.EncyptionManager;

namespace PRDG11EOSV03WB
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        #region Declarations

        public DataSet objDatasetAppsVariables;
        public DataSet objDatasetlocaldeclaration;
        #endregion

        #region Page


        protected void Page_Load(object sender, EventArgs e)
        {
            objDatasetlocaldeclaration = CommonFunctions.ConstructlocalStringInfoDetails(); //
            Session["objDatasetlocaldeclaration"] = objDatasetlocaldeclaration;

           

                txtOldPassword.Focus();
            
        }

        #endregion


        private void ResetPassword()
        {
            int interrorcount = 0;          
            string[] stringOutputResult = null;
            clsEncrypt objclsEncrypt;
            objDatasetAppsVariables = (DataSet)Session["objDatasetlocaldeclaration"];
            objDatasetAppsVariables.Tables[0].Rows[0]["FORM_ID"] = "FAD1011R1V1";

            string stringoldPassword,    stringnewPassword;
            try
            {
                if (Page.IsValid)
                {
                    if (txtOldPassword.Text.Length >0 && txtNewPassword.Text.Length>0  && txtOldPassword.Text.Length>0)
                    {
                     
                        objclsEncrypt = new clsEncrypt(enumEncryptionAlgorithm.DES, "GSSLOGIN");
                        stringoldPassword = objclsEncrypt.Encrypt(txtOldPassword.Text) + "ivector" + Convert.ToBase64String(objclsEncrypt.byteIV) + "end" + "DES";
                         stringnewPassword = objclsEncrypt.Encrypt(txtNewPassword.Text) + "ivector" + Convert.ToBase64String(objclsEncrypt.byteIV) + "end" + "DES";

                        using (G11EISMWSelectionService.SelectionServiceClient objSelectionServiceClient = new G11EISMWSelectionService.SelectionServiceClient(CommonFunctions.CreateServiceBasicHttpBinding(), CommonFunctions.objEndpointAddressSelectionService))
                        {
                           // stringOutputResultr = objSelectionServiceClient.ChangePasswordR1V2(stringoldPassword, stringnewPassword, objDatasetAppsVariables, out interrorcount, out stringOutputResult);
                            if (objSelectionServiceClient != null)
                                objSelectionServiceClient.Close();
                        }

                        if (interrorcount == 0)
                        {
                            CommonFunctions.ShowMessageboot(this, "Password Successfully Changed.");
                        }
                        else
                        {
                            CommonFunctions.ShowMessageboot(this, stringOutputResult[2].ToString());
                        }
                    }



                }
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
        }
        #region Button

        protected void BtnChangePassword_Click(object sender, EventArgs e)
        {
            string stringErrorMsg = "";
            string[] stringResult = new string[3];

            try
            {
                if (txtOldPassword.Text.Length > 0 )
                {
                    if (txtNewPassword.Text.Length > 0 )
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
                                CommonFunctions.ShowMessageboot(this, stringErrorMsg);
                            }
                        }
                        else
                        {
                            CommonFunctions.ShowMessageboot(this, "Please Enter Old Password");
                        }
                    }
                    else
                    {
                        CommonFunctions.ShowMessageboot(this, "Please Enter New Password");
                    }
                }
                else
                {
                    CommonFunctions.ShowMessageboot(this, "Please Enter Confirm Password");
                }


            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
            finally
            {

            }
        }

        #endregion

        #region Methods / Functions

        private void ClearText()
        {
            txtConfirmPassword.Text = "";
            txtNewPassword.Text = "";
            txtOldPassword.Text = "";
        }

        #endregion
    }
}