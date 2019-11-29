 
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
//
//   History.
//
//   12-Oktober-2015                        Initial version.                                (nosa)  
//

using System.Text;
using System.Web.UI;
using System.Web;
using System.IO;
using System.Text.RegularExpressions;
 
public class clsWebUtility
{
          

    #region " Server SSL (SubDomain)"
    public static string GetDomainUrl(string txtoriginalUrl, string subDomain)
    {
        //Create Uri
        Uri originalUrl = new Uri(txtoriginalUrl);

        if (subDomain.Substring(subDomain.Length - 1).Equals("/"))
        {
            //Hilangkan '/' dibelakang!
            subDomain = subDomain.Substring(0, subDomain.Length - 1);
        }

        string domainUrl = string.Empty;
        if (originalUrl.Scheme.Equals("http"))
        {
            //Jika HTTP, default Port (80)
            if (originalUrl.Port != 80)
            {
                domainUrl = string.Concat(originalUrl.Scheme, Uri.SchemeDelimiter, originalUrl.Host, ":", originalUrl.Port) + subDomain;
            }
            else
            {
                domainUrl = string.Concat(originalUrl.Scheme, Uri.SchemeDelimiter, originalUrl.Host) + subDomain;
            }
        }
        else if (originalUrl.Scheme.Equals("https"))
        {
            //Jika HTTPS, default Port (443)
            if (originalUrl.Port != 443)
            {
                domainUrl = string.Concat(originalUrl.Scheme, Uri.SchemeDelimiter, originalUrl.Host, ":", originalUrl.Port) + subDomain;
            }
            else
            {
                domainUrl = string.Concat(originalUrl.Scheme, Uri.SchemeDelimiter, originalUrl.Host) + subDomain;
            }
        }

        return domainUrl;
    }

    public static string GetRequestPath(string RequestUrl, string subDomain)
    {
        return RequestUrl.ToString().ToLower().Replace(clsWebUtility.GetDomainUrl(RequestUrl, subDomain.ToLower()), "");
    }

    public static string ResolveServerUrl(string txtoriginalUrl, string subDomain, string txtResolveUrl)
    {
        //domain + page => https://10.171.80.17/HRESS + /Logout.aspx.
        return GetDomainUrl(txtoriginalUrl, subDomain) + txtResolveUrl;
    }

    #endregion

      
}
 