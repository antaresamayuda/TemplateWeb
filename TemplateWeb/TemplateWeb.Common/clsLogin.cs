using System; 
using TemplateWeb.Common.Entity;

namespace TemplateWeb.Common
{
    [Serializable]
    public class clsLogin
    {
        public mUser userDat;
        public int intRoleID;
        public string txtLangID;

        public void New()
        {
            userDat = new mUser();
        }
    } 
}
