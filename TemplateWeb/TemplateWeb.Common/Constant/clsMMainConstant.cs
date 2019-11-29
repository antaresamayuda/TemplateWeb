using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace TemplateWeb.Common
{
	public class clsMMainConstant
	{
		public const string MODULE_NAME = "MAIN";
		public const string Administrator = "Administrator";
		public const string MESSAGE = "MESSAGE";
		public const string TOKEN_API = "Sanghiang";

		public const string FormatDate = "yyyy/MM/dd";
	 
		public class CONFIGURATION
		{
			public class Key
			{
				public const string SHOW_STACKTRACE = "Show Stack Trace";
				public const string ExceptionPublisherEmailSender = "Exception Sender";
				public const string ExceptionPublisherEmailSubject = "Exception Subjet";
				public const string SenderEmail = "Sender Email";
				public const string JoinString = "JoinString";
				public const string SEND_ERROREMAIL = "Send Email Error";
				public const string DefaultLangID = "DefaultLangID";
                public const string bDebugEmail = "Debug Email";
                public const string SMTP = "SMTP"; 
                public const string Generate_Random_Parameter = "Generate Random Parameter";
                public const string byPassLogin = "byPassLogin";
            }

			public class Description
			{
				public const string SHOW_STACKTRACE = "Show Stack Trace";
				public const string ExceptionPublisherEmailSender = "Exception Sender";
				public const string ExceptionPublisherEmailSubject = "Exception Subjet";
				public const string SenderEmail = "Sender Email";
				public const string JoinString = "JoinString";
				public const string SEND_ERROREMAIL = "Send Email Error";
				public const string DefaultLangID = "DefaultLangID";
                public const string bDebugEmail = "Debug Email";
                public const string SMTP = "SMTP";
                public const string Generate_Random_Parameter = "Generate Random Parameter";
                public const string byPassLogin = "byPassLogin";
            }

			public class DefaultValue
			{
				public const string SHOW_STACKTRACE = "N";
				public const string ExceptionPublisherEmailSender = "mail@mail.com";
				public const string ExceptionPublisherEmailSubject = "BIP:ERROR";
				public const string SenderEmail = "antaresa.mayuda@gmail.com";
				public const string JoinString = ";";
				public const string SEND_ERROREMAIL = "Y";
				public const string DefaultLangID = "IN";
                public const string bDebugEmail = "Y";
                public const string SMTP = "172.31.254.246";
                public const string Generate_Random_Parameter = "12345";
                public const string byPassLogin = "N";
            }
		}
        		
		public class URL
		{
			public const string LOGIN = "/Account/Login";
			public const string LOGOUT = "/Account/Logout"; 
			public const string MAIN = "/Home/Index";
			public const string WARNING = "/Account/Warning"; 
		}

		public class ImageType
		{
			public const string JPEG = "JPEG";
			public const string JPG = "JPG";
			public const string PNG = "PNG";
			public const string GIF = "GIF";
		}
          
		public class StatusFlow
		{ 
			public const string COMPLETED = "COMPLETED";
			public const string PROCCESSING = "PROCCESSING";
			public const string PARTIAL_COMPLETED = "PARTIAL COMPLETED";
			public const string CLOSED = "CLOSED";
		}

        public class LOV
        {
            public const string LOV_EMPLOYEE = "LOV_EMPLOYEE";
            public const string LOV_EMPLOYEENAME = "LOV_EMPLOYEENAME";
            public const string LOV_EMPLOYEE_BYORGGROUP = "LOV_EMPLOYEE_BYORGGROUP";
            public const string LOV_ORGANIZATIONCODE = "LOV_ORGANIZATIONCODE"; 
        }

        public class Email
        {
            public const string EMAIL = "xxxxxxxx@gmail.com";
            public const string PASSWORD = "xxxxxxxx";
        }

    }
} 