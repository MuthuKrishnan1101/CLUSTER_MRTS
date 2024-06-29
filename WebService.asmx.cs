using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;


namespace JGHR1V1
{
    /// <summary>
    /// Summary description for WebService
    /// </summary>
    /// 


    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ScriptService]
    public class WebService : System.Web.Services.WebService
    {

        public WebService()
        {

            //Uncomment the following line if using designed components 
            //InitializeComponent(); 
        }

        [WebMethod]
        public string GetContextKey(string contextKey)
        {
            return "";
        }

        [WebMethod]
        public string GetContextKeyGroupHistory(string contextKey)
        {
            return "";
        }

        [WebMethod]
        public string GetContextKeySaveProfile(string contextKey)
        {
            return "";
        }

        [WebMethod]
        public string GetContextKeySaveSpecialInfo(string contextKey)
        {
            return "";
        }

        [WebMethod]
        public string GetContextKeySaveDailySchedule(string contextKey)
        {
            return "";
        }

        [WebMethod]
        public string GetContextKeySoftDelete(string contextKey)
        {
            return "";
        }

        [WebMethod]
        public string GetContextKeyReactivate(string contextKey)
        {
            return "";
        }

        [WebMethod]
        public string HelloWorld()
        {
            return "";
        }

        [WebMethod]
        public string GetContextKeyGroupHistoryOneHour(string contextKey)
        {
            return "";
        }

        [WebMethod]
        public string GetContextKeyPopupMembers(string contextKey)
        {
            return "";
        }

        [WebMethod]
        public string GetContextKeySearchGroup(string contextKey)
        {
            return "";
        }

        [WebMethod]
        public string GetContextKeyPopupMembers1(string contextKey)
        {
            return "";
        }

        [WebMethod]
        public string GetContextKeySaveTeam(string contextKey)
        {
            return "";
        }

        [WebMethod]
        public string GetContextKeySaveGroup(string contextKey)
        {
            return "";
        }
    }
}




