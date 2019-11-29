
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Web;
using System.Text;
using System.Text.RegularExpressions;

public class clsPathHelper
{
	//		private static readonly string _port;
	//		private static readonly HttpRequest _request;
	//		private static readonly string _path;
	//		private static readonly string _domain;

	private static string _port;
	private static HttpRequest _request;
	private static string _path;

	private static string _domain;
	/// <summary>
	/// Static constructor, to initialize static data
	/// </summary> 
	static clsPathHelper()
	{ 
		Init();
	}

	private static void Init()
	{
		_request = HttpContext.Current.Request;
		_port = ((_request.Url.Port == 80) ? "" : ":" + _request.Url.Port.ToString());

		StringBuilder temp = new StringBuilder(_request.Url.Scheme + "://");
		temp.Append(_request.Url.Host);
		temp.Append(_port);
		_domain = temp.ToString();
		if (!_domain.EndsWith("/")) {
			_domain += "";
		}
		if (!(_request.ApplicationPath == "/")) {
			temp.Append(_request.ApplicationPath);
		}
		if (!_request.ApplicationPath.EndsWith("/")) {
			temp.Append("");
		}
		_path = temp.ToString();
	}

	/// <summary>
	/// Private constructor, prevent client from creating instance of this class
	/// </summary>
	private clsPathHelper()
	{
	}

	/// <summary>
	/// Return absolute URL of current web application. Ex: http://server/appvirtualdir/
	/// </summary>
	public static string AppVirtualDirectory {
		get {
			//if ((_path == null) || (_path == "")) 
			//                {

			//#if NOT_STATIC_CACHE
			Init();
			//#endif
			//}
			return _path;
		}
	}

	/// <summary>
	/// Return the domain (website). Ex: http://www.binuscareer.com/
	/// </summary>
	public static string Domain {
		get {
			if ((_domain == null) || (string.IsNullOrEmpty(_domain))) {
				//#if NOT_STATIC_CACHE
				//#endif
				Init();
			}
			return _domain;
		}
	}

	/// <summary>
	/// Convert physical path into web application path.:
	/// </summary>
	/// <param name="physicalPath"></param>
	/// <returns></returns>
	/// <example>
	/// string path = PathHelper.TranslatePath(@"c:\inetpub\wwwroot\BiNusCareerWeb\Images\banners\"); // return "http://localhost/BiNusCareerWeb/Images\banners"
	/// </example>
	public static string TranslatePath(string physicalPath)
	{
		//#if NOT_STATIC_CACHE
		Init();
		//#endif
		return physicalPath.Substring(physicalPath.IndexOf(HttpContext.Current.Request.ApplicationPath.Substring(1)) + HttpContext.Current.Request.ApplicationPath.Length).Replace('\\', '/');
	}

	/// <summary>
	/// Return absolute URL (ex: http://server/appvirtualdir/sample.aspx)
	/// </summary>
	public static string CurrentUrl {
		get {
			string URL = HttpContext.Current.Request.Url.AbsoluteUri;

			int index = URL.LastIndexOf("?");

			if (index == -1) {
				index = URL.Length;
			}


			return URL.Substring(0, index);
		}
	}

	/// <summary>
	/// Method ini gunanya utk ngambil FolderName ex : localhost/BiNusCareerweb/Jobseeker/blabla.aspx  
	/// maka yg diambil adalah JobSeeker
	/// </summary>
	public static string CurrentFolder {
		get {
			string FolderName = "";
			Regex r = new Regex("[a-zA-Z0-9_]+\\/[a-zA-Z0-9_]+\\.aspx$");
			Match m = r.Match(HttpContext.Current.Request.Url.LocalPath);
			//Ini gunanya buat matching antara local path dengan regex

			if (m.Success) {
				//klo match, ambil FolderNamenya ex : /FolderName/blabla.aspx
				FolderName = m.Captures[0].Value.Substring(0, m.Captures[0].Value.LastIndexOf("/"));
			}
			if (HttpContext.Current.Request.ApplicationPath.Substring(1) == FolderName) {
				FolderName = "";
			} else {
				//klo utk public folder maka foldername = "/";
				FolderName = FolderName + "/";
			}
			return FolderName;
		}
	}

}
 