using System.Configuration;

namespace TemplateWeb.Common
{
    public class clsSystemConfiguration
    {
        private string _DBEntities = ConfigurationManager.ConnectionStrings["DBEntities"].ToString(); 


        public string ConnectionStringSql
        {
            get { return _DBEntities; }
            //get { return clsRijndael.Decrypt(_DBEntities); }
        }
         
    }
}
