﻿
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

//
//   History.
//
//   10-April-2017                        Initial version.                                (nosa)    
//

using System.Text;
using System.IO;
using System.Web;

using TemplateWeb.Common;

public class clsGlobal
{

    public static string CompanyName = "Bank Sinarmas";
    public static string AppName = "Template Web";
    public static string Delimeter = " :: ";
    public static string Delimeter_Underscore = "_";

    public static string Delimeter_Des = "-";
    public static string txtFormatDate = "yyyy/MM/dd";
    public static string txtFormatDateMMDDYYYY = "MM/dd/yyyy";
    public static string txtMoneyFormatKoma = "##,##.#0";
    public static DateTime DATE_MINVALUE = DateTime.Parse("1/1/2000");

    public static DateTime DATE_MAXVALUE = DateTime.Parse("1/1/4000");

    public static string MODULE_NAME_GLOBAL = "GLOBAL";

    #region "Prefix"
    public class Prefix
    {
        public static string Delimeter_Desh = "-";
        public static string Delimeter_DOT = ".";
        public static int Length = 18;
    }
    #endregion

    #region "LOV"
    public class LOV
    {
        // Function Name
        public static string Fnc = "Fnc";
        // Module
        public static string Mdl = "Mdl";
        // Module
        public static string Query = "Query";

        public class Connector
        {
            public const string _AND = "AND";
            public const string _OR = "OR";
        }

        public class Constraint
        {
            public const string _IS = "IS";
            public const string _LIKE = "LIKE";
            public const string _MORE = "MORE";
            public const string _LESS = "LESS";
        }
    }
    #endregion

     
    public class PARAMETER
    {
        public const string DELIMETER = "|";
        public const string DELIMETER2 = ";";
    }

    public class CONSTANTS
    {
        public const string MESSAGE = "MESSAGE";
        public const string MAIN = "MAIN";
    }

    public class Options
    {
        public static string NO = "N";
        public static string YES = "Y";
    }

    public class DocStatus
    {
        public static string DRAFT = "DRAFT";
        public static string APPLY = "APPLY";
        public static string APPROVED = "APPROVED";
        public static string REJECTED = "REJECTED";
        public static string COMPLETED = "COMPLETED";
    }

    public class State
    {
        public static string CREATE = "CREATE";
        public static string UPDATE = "UPDATE";
    }

    public class Request
    { 
        public const string ID = "Id";
        public const string DTM = "dtm";
        public const string INT = "int";
        public const string BAP = "bap";
        public const string IDR = "idr";
        public const string SRC = "src";
        public const string K2SerialNumber = "sn";
        public const string K2ProcessID = "intProcessID";
        public const string Z = "Z";
        public const string W = "W";

        public const string H = "H";
        public const string Mdl = "Mdl";
        public const string Fnc = "Fnc";
        public const string Query = "Query";
    }

    public class SessionName
    {
        public const string objclsAccount = "objclsAccount"; 
    }

    public static string GetTitle(string txtModule)
    {
        return CompanyName + Delimeter + AppName + Delimeter + txtModule;
    }

    public class PRINTOUT
    {
        public const string PDF_TYPE = "PDF";
    }

    public class CONFIGURATION
    {
        public class Key
        {
            public const string SHOW_STACKTRACE = "Show Stack Trace";
            public const string ExceptionPublisherEmailSender = "Exception Sender";
            public const string ExceptionPublisherEmailSubject = "Exception Subject";
            public const string SenderEmail = "Sender Email";
            public const string DefaultFirstPassword = "Default FirstPassword";
        }
    }

    #region "DATE TIME MONTH YEAR"

    public class MONTH
    {
        public const string JANUARI = "JANUARI";
        public const string FEBRUARI = "FEBRUARI";
        public const string MARET = "MARET";
        public const string APRIL = "APRIL";
        public const string MEI = "MEI";
        public const string JUNI = "JUNI";
        public const string JULI = "JULI";
        public const string AGUSTUS = "AGUSTUS";
        public const string SEPTEMBER = "SEPTEMBER";
        public const string OKTOBER = "OKTOBER";
        public const string NOVEMBER = "NOVEMBER";
        public const string DESEMBER = "DESEMBER";
    }

    public class MONTH_SHORT
    {
        public const string JANUARI = "JAN";
        public const string FEBRUARI = "FEB";
        public const string MARET = "MAR";
        public const string APRIL = "APR";
        public const string MEI = "MEI";
        public const string JUNI = "JUN";
        public const string JULI = "JUL";
        public const string AGUSTUS = "AGU";
        public const string SEPTEMBER = "SEP";
        public const string OKTOBER = "OKT";
        public const string NOVEMBER = "NOV";
        public const string DESEMBER = "DES";
    }

    public class DAYNAME
    {
        public const string MONDAY = "MONDAY";
        public const string TUESDAY = "TUESDAY";
        public const string WEDNESDAY = "WEDNESDAY";
        public const string THURSDAY = "THURSDAY";
        public const string FRIDAY = "FRIDAY";
        public const string SATURDAY = "SATURDAY";
        public const string SUNDAY = "SUNDAY";
    }

    public class DAYNAME_INDO
    {
        public const string MONDAY = "SENIN";
        public const string TUESDAY = "SELASA";
        public const string WEDNESDAY = "RABU";
        public const string THURSDAY = "KAMIS";
        public const string FRIDAY = "JUMAT";
        public const string SATURDAY = "SABTU";
        public const string SUNDAY = "MINGGU";
    }

    public static string GetThisMonth(bool bShort)
    {
        if (DateTime.Now.Month == 1)
        {
            if (bShort)
            {
                return clsGlobal.MONTH_SHORT.JANUARI;
            }
            else
            {
                return clsGlobal.MONTH.JANUARI;
            }
        }
        else if (DateTime.Now.Month == 2)
        {
            if (bShort)
            {
                return clsGlobal.MONTH_SHORT.FEBRUARI;
            }
            else
            {
                return clsGlobal.MONTH.FEBRUARI;
            }
        }
        else if (DateTime.Now.Month == 3)
        {
            if (bShort)
            {
                return clsGlobal.MONTH_SHORT.MARET;
            }
            else
            {
                return clsGlobal.MONTH.MARET;
            }
        }
        else if (DateTime.Now.Month == 4)
        {
            if (bShort)
            {
                return clsGlobal.MONTH_SHORT.APRIL;
            }
            else
            {
                return clsGlobal.MONTH.APRIL;
            }
        }
        else if (DateTime.Now.Month == 5)
        {
            if (bShort)
            {
                return clsGlobal.MONTH_SHORT.MEI;
            }
            else
            {
                return clsGlobal.MONTH.MEI;
            }
        }
        else if (DateTime.Now.Month == 6)
        {
            if (bShort)
            {
                return clsGlobal.MONTH_SHORT.JUNI;
            }
            else
            {
                return clsGlobal.MONTH.JUNI;
            }
        }
        else if (DateTime.Now.Month == 7)
        {
            if (bShort)
            {
                return clsGlobal.MONTH_SHORT.JULI;
            }
            else
            {
                return clsGlobal.MONTH.JULI;
            }
        }
        else if (DateTime.Now.Month == 8)
        {
            if (bShort)
            {
                return clsGlobal.MONTH_SHORT.AGUSTUS;
            }
            else
            {
                return clsGlobal.MONTH.AGUSTUS;
            }
        }
        else if (DateTime.Now.Month == 9)
        {
            if (bShort)
            {
                return clsGlobal.MONTH_SHORT.SEPTEMBER;
            }
            else
            {
                return clsGlobal.MONTH.SEPTEMBER;
            }
        }
        else if (DateTime.Now.Month == 10)
        {
            if (bShort)
            {
                return clsGlobal.MONTH_SHORT.OKTOBER;
            }
            else
            {
                return clsGlobal.MONTH.OKTOBER;
            }
        }
        else if (DateTime.Now.Month == 11)
        {
            if (bShort)
            {
                return clsGlobal.MONTH_SHORT.NOVEMBER;
            }
            else
            {
                return clsGlobal.MONTH.NOVEMBER;
            }
        }
        else if (DateTime.Now.Month == 12)
        {
            if (bShort)
            {
                return clsGlobal.MONTH_SHORT.DESEMBER;
            }
            else
            {
                return clsGlobal.MONTH.DESEMBER;
            }
        }
        else
        {
            throw new Exception("DEV: ERROR in GetThisMonth()");

        }
    }

    public static string GetMonthName(int intMonth, bool bShort)
    {
        if (intMonth == 1)
        {
            if (bShort)
            {
                return clsGlobal.MONTH_SHORT.JANUARI;
            }
            else
            {
                return clsGlobal.MONTH.JANUARI;
            }
        }
        else if (intMonth == 2)
        {
            if (bShort)
            {
                return clsGlobal.MONTH_SHORT.FEBRUARI;
            }
            else
            {
                return clsGlobal.MONTH.FEBRUARI;
            }
        }
        else if (intMonth == 3)
        {
            if (bShort)
            {
                return clsGlobal.MONTH_SHORT.MARET;
            }
            else
            {
                return clsGlobal.MONTH.MARET;
            }
        }
        else if (intMonth == 4)
        {
            if (bShort)
            {
                return clsGlobal.MONTH_SHORT.APRIL;
            }
            else
            {
                return clsGlobal.MONTH.APRIL;
            }
        }
        else if (intMonth == 5)
        {
            if (bShort)
            {
                return clsGlobal.MONTH_SHORT.MEI;
            }
            else
            {
                return clsGlobal.MONTH.MEI;
            }
        }
        else if (intMonth == 6)
        {
            if (bShort)
            {
                return clsGlobal.MONTH_SHORT.JUNI;
            }
            else
            {
                return clsGlobal.MONTH.JUNI;
            }
        }
        else if (intMonth == 7)
        {
            if (bShort)
            {
                return clsGlobal.MONTH_SHORT.JULI;
            }
            else
            {
                return clsGlobal.MONTH.JULI;
            }
        }
        else if (intMonth == 8)
        {
            if (bShort)
            {
                return clsGlobal.MONTH_SHORT.AGUSTUS;
            }
            else
            {
                return clsGlobal.MONTH.AGUSTUS;
            }
        }
        else if (intMonth == 9)
        {
            if (bShort)
            {
                return clsGlobal.MONTH_SHORT.SEPTEMBER;
            }
            else
            {
                return clsGlobal.MONTH.SEPTEMBER;
            }
        }
        else if (intMonth == 10)
        {
            if (bShort)
            {
                return clsGlobal.MONTH_SHORT.OKTOBER;
            }
            else
            {
                return clsGlobal.MONTH.OKTOBER;
            }
        }
        else if (intMonth == 11)
        {
            if (bShort)
            {
                return clsGlobal.MONTH_SHORT.NOVEMBER;
            }
            else
            {
                return clsGlobal.MONTH.NOVEMBER;
            }
        }
        else if (intMonth == 12)
        {
            if (bShort)
            {
                return clsGlobal.MONTH_SHORT.DESEMBER;
            }
            else
            {
                return clsGlobal.MONTH.DESEMBER;
            }
        }
        else
        {
            throw new Exception("DEV: ERROR in GetThisMonth()");

        }
    }

    public static int GetMonth(string txtPeriod, bool bShort)
    {
        if (bShort)
        {
            switch (txtPeriod)
            {
                case clsGlobal.MONTH_SHORT.JANUARI:
                    return 1;
                case clsGlobal.MONTH_SHORT.FEBRUARI:
                    return 2;
                case clsGlobal.MONTH_SHORT.MARET:
                    return 3;
                case clsGlobal.MONTH_SHORT.APRIL:
                    return 4;
                case clsGlobal.MONTH_SHORT.MEI:
                    return 5;
                case clsGlobal.MONTH_SHORT.JUNI:
                    return 6;
                case clsGlobal.MONTH_SHORT.JULI:
                    return 7;
                case clsGlobal.MONTH_SHORT.AGUSTUS:
                    return 8;
                case clsGlobal.MONTH_SHORT.SEPTEMBER:
                    return 9;
                case clsGlobal.MONTH_SHORT.OKTOBER:
                    return 10;
                case clsGlobal.MONTH_SHORT.NOVEMBER:
                    return 11;
                case clsGlobal.MONTH_SHORT.DESEMBER:
                    return 12;
                default:
                    throw new Exception("DEV: ERROR in GetMonth(Jan-Des)");
            }
        }
        else
        {
            switch (txtPeriod)
            {
                case clsGlobal.MONTH.JANUARI:
                    return 1;
                case clsGlobal.MONTH.FEBRUARI:
                    return 2;
                case clsGlobal.MONTH.MARET:
                    return 3;
                case clsGlobal.MONTH.APRIL:
                    return 4;
                case clsGlobal.MONTH.MEI:
                    return 5;
                case clsGlobal.MONTH.JUNI:
                    return 6;
                case clsGlobal.MONTH.JULI:
                    return 7;
                case clsGlobal.MONTH.AGUSTUS:
                    return 8;
                case clsGlobal.MONTH.SEPTEMBER:
                    return 9;
                case clsGlobal.MONTH.OKTOBER:
                    return 10;
                case clsGlobal.MONTH.NOVEMBER:
                    return 11;
                case clsGlobal.MONTH.DESEMBER:
                    return 12;
                default:
                    throw new Exception("DEV: ERROR in GetMonth(Januari-Desember)");
            }
        }
    }

    public static int GetYear(string txtPeriod)
    {
        // From OCT/2013 => 2013
        return ParseToInteger(txtPeriod.Substring(txtPeriod.IndexOf("/") + 1));

    }

    public static ArrayList GetPeriod(int intCOunt)
    {
        ArrayList retList = new ArrayList();

        int intMonth = DateTime.Now.Month;
        int intYear = DateTime.Now.Year;

        for (int i = 0; i <= intCOunt; i++)
        {
            if (intMonth == 0)
            {
                // Jika Mundur sampai tahun lalu
                // Jika intmonth sampai 0, maka intmonth tambah 12, supaya kembali ke desember lagi, dan intyear dikuraing 1 (tahun).
                intMonth = intMonth + 12;
                intYear = intYear - 1;
            }
            string szString = GetMonthName(intMonth, true).ToString().Trim() + "/" + intYear.ToString().Trim();
            retList.Add(szString);

            intMonth -= 1;
        }

        return retList;
    }

    //Get the first day of the month
    public static DateTime FirstDayOfMonth(DateTime sourceDate)
    {
        return new DateTime(sourceDate.Year, sourceDate.Month, 1);
    }

    //Get the last day of the month
    public static DateTime LastDayOfMonth(DateTime sourceDate)
    {
        DateTime lastDay = new DateTime(sourceDate.Year, sourceDate.Month, 1);
        return lastDay.AddMonths(1).AddDays(-1);
    }

    //Menghitung hari diantara 2 tanggal.
    public static int CalculateDaysBetweenDate(DateTime endTime, DateTime startTime)
    {
        TimeSpan span = endTime.Subtract(startTime);

        return span.Days;
    }

    //Rubah ke Format Oracle dd-MMM-yyyy
    public static string DateOracleFormat(DateTime pDate)
    {
        string strTgl = null;
        string strBulan = "";
        string strTahun = null;
        strTgl = String.Format(pDate.ToString(), "dd");
        strTahun = String.Format(pDate.ToString(), "yyyy");
        switch (String.Format(pDate.ToString(), "MM"))
        {
            case "01":
                strBulan = "JAN";
                break;
            case "02":
                strBulan = "FEB";
                break;
            case "03":
                strBulan = "MAR";
                break;
            case "04":
                strBulan = "APR";
                break;
            case "05":
                strBulan = "MAY";
                break;
            case "06":
                strBulan = "JUN";
                break;
            case "07":
                strBulan = "JUL";
                break;
            case "08":
                strBulan = "AUG";
                break;
            case "09":
                strBulan = "SEP";
                break;
            case "10":
                strBulan = "OCT";
                break;
            case "11":
                strBulan = "NOV";
                break;
            case "12":
                strBulan = "DEC";
                break;
        }
        return strTgl + "-" + strBulan + "-" + strTahun;
    }

    // Get Date and minutes
    public static string GetFormatHour(DateTime pDate, bool bitWithSecond)
    {

        string strHour = null;
        string strMinutes = null;
        string strSecond = null;

        strHour = String.Format(pDate.ToString(), "hh");
        strMinutes = String.Format(pDate.ToString(), "mm");
        strSecond = String.Format(pDate.ToString(), "ss");

        if (bitWithSecond)
        {
            return strHour + ":" + strMinutes + ":" + strSecond;
        }
        else
        {
            return strHour + ":" + strMinutes;
        }
    }

    //Rubah ke Format Oracle dd-MMM-yyyy hh:mm
    public static string DateOracleFormatWithHour(DateTime pDate)
    {
        string strTgl = null;
        string strBulan = "";
        string strTahun = null;
        string strHour = null;
        string strMinutes = null;
        string strAMPM = null;

        strHour = String.Format(pDate.ToString(), "hh");
        strMinutes = String.Format(pDate.ToString(), "mm");
        strTgl = String.Format(pDate.ToString(), "dd");
        strTahun = String.Format(pDate.ToString(), "yyyy");
        strAMPM = String.Format(pDate.ToString(), "tt");
        switch (String.Format(pDate.ToString(), "MM"))
        {
            case "01":
                strBulan = "JAN";
                break;
            case "02":
                strBulan = "FEB";
                break;
            case "03":
                strBulan = "MAR";
                break;
            case "04":
                strBulan = "APR";
                break;
            case "05":
                strBulan = "MAY";
                break;
            case "06":
                strBulan = "JUN";
                break;
            case "07":
                strBulan = "JUL";
                break;
            case "08":
                strBulan = "AUG";
                break;
            case "09":
                strBulan = "SEP";
                break;
            case "10":
                strBulan = "OCT";
                break;
            case "11":
                strBulan = "NOV";
                break;
            case "12":
                strBulan = "DEC";
                break;
        }
        return strTgl + "-" + strBulan + "-" + strTahun + " " + strHour + ":" + strMinutes + " " + strAMPM;
    }

    //Rubah ke Format Oracle MM/ddM/yyyy hh:mm:ss AM
    public static string DateOracleFormatWithHourSecond(DateTime pDate)
    {
        string strTgl = null;
        string strBulan = "";
        string strTahun = null;
        string strHour = null;
        string strMinutes = null;
        string strAMPM = null;
        string strSecond = null;

        strHour = String.Format(pDate.ToString(), "hh");
        strMinutes = String.Format(pDate.ToString(), "mm");
        strTgl = String.Format(pDate.ToString(), "dd");
        strTahun = String.Format(pDate.ToString(), "yyyy");
        strAMPM = String.Format(pDate.ToString(), "tt");
        strSecond = String.Format(pDate.ToString(), "ss");
        switch (String.Format(pDate.ToString(), "MM"))
        {
            case "01":
                strBulan = "JAN";
                break;
            case "02":
                strBulan = "FEB";
                break;
            case "03":
                strBulan = "MAR";
                break;
            case "04":
                strBulan = "APR";
                break;
            case "05":
                strBulan = "MAY";
                break;
            case "06":
                strBulan = "JUN";
                break;
            case "07":
                strBulan = "JUL";
                break;
            case "08":
                strBulan = "AUG";
                break;
            case "09":
                strBulan = "SEP";
                break;
            case "10":
                strBulan = "OCT";
                break;
            case "11":
                strBulan = "NOV";
                break;
            case "12":
                strBulan = "DEC";
                break;
        }
        return strTgl + "/" + strBulan + "/" + strTahun + " " + strHour + ":" + strMinutes + ":" + strSecond + " " + strAMPM;
    }

    //Rubah ke Format Oracle DD-MON-RR
    public static string DateOracleFormat2(DateTime pDate)
    {
        string strTgl = null;
        string strBulan = "";
        string strTahun = null;
        strTgl = String.Format(pDate.ToString(), "dd");
        strTahun = String.Format(pDate.ToString(), "yy");
        switch (String.Format(pDate.ToString(), "MM"))
        {
            case "01":
                strBulan = "JAN";
                break;
            case "02":
                strBulan = "FEB";
                break;
            case "03":
                strBulan = "MAR";
                break;
            case "04":
                strBulan = "APR";
                break;
            case "05":
                strBulan = "MAY";
                break;
            case "06":
                strBulan = "JUN";
                break;
            case "07":
                strBulan = "JUL";
                break;
            case "08":
                strBulan = "AUG";
                break;
            case "09":
                strBulan = "SEP";
                break;
            case "10":
                strBulan = "OCT";
                break;
            case "11":
                strBulan = "NOV";
                break;
            case "12":
                strBulan = "DEC";
                break;
        }
        return strTgl + "-" + strBulan + "-" + strTahun;
    }

    //Rubah ke Format Oracle yyyy/MM/dd hh:mm:ss 
    public static string DateOracleFormat_FND_STANDART_DATE(DateTime pDate)
    {
        string strTgl = null;
        string strBulan = "";
        string strTahun = null;
        string strHour = null;
        string strMinutes = null;
        string strAMPM = null;
        string strSecond = null;

        strHour = String.Format(pDate.ToString(), "hh");
        strMinutes = String.Format(pDate.ToString(), "mm");
        strTgl = String.Format(pDate.ToString(), "dd");
        strTahun = String.Format(pDate.ToString(), "yyyy");
        strAMPM = String.Format(pDate.ToString(), "tt");
        strSecond = String.Format(pDate.ToString(), "ss");
        strBulan = String.Format(pDate.ToString(), "MM");

        return strTahun + "/" + strBulan + "/" + strTgl + " " + strHour + ":" + strMinutes + ":" + strSecond;
    }

    //Check If [Date From] is bigger than [Date To]
    public static bool CheckIfDateFromBiggerThanTo(DateTime fromDate, DateTime toDate)
    {
        if (fromDate > toDate)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //Get Oracle Period
    public static string GetOracleGLPeriod(System.DateTime dtDate)
    {
        switch (dtDate.Month)
        {
            case 1:
                return "JAN-" + String.Format(dtDate.ToString(), "yy");
            case 2:
                return "FEB-" + String.Format(dtDate.ToString(), "yy");
            case 3:
                return "MAR-" + String.Format(dtDate.ToString(), "yy");
            case 4:
                return "APR-" + String.Format(dtDate.ToString(), "yy");
            case 5:
                return "MAY-" + String.Format(dtDate.ToString(), "yy");
            case 6:
                return "JUN-" + String.Format(dtDate.ToString(), "yy");
            case 7:
                return "JUL-" + String.Format(dtDate.ToString(), "yy");
            case 8:
                return "AUG-" + String.Format(dtDate.ToString(), "yy");
            case 9:
                return "SEP-" + String.Format(dtDate.ToString(), "yy");
            case 10:
                return "OCT-" + String.Format(dtDate.ToString(), "yy");
            case 11:
                return "NOV-" + String.Format(dtDate.ToString(), "yy");
            case 12:
                return "DEC-" + String.Format(dtDate.ToString(), "yy");
            default:
                throw new Exception("DEV: Out of range Month Oracle Period.");
        }
    }

    #endregion

    #region "Data Reader"

    public static string DataReaderGetString(object obj)
    {
        if (obj == null)
        {
            return string.Empty;
        }
        else if (obj.Equals(DBNull.Value))
        {
            return string.Empty;
        }
        else
        {
            return obj.ToString();
        }
    }

    public static string DataReaderGetStringTryCatch(object obj)
    {
        try
        {
            if (obj == null)
            {
                return string.Empty;
            }
            else if (obj.Equals(DBNull.Value))
            {
                return string.Empty;
            }
            else
            {
                return obj.ToString();
            }
        }
        catch (Exception)
        {
            return string.Empty;
        }
    }

    public static decimal DataReaderGetDecimal(object obj)
    {
        if (obj == null)
        {
            return decimal.Zero;
        }
        else if (obj.Equals(DBNull.Value))
        {
            return decimal.Zero;
        }
        else
        {
            return decimal.Parse(obj.ToString());
        }
    }

    public static int DataReaderGetInteger(object obj)
    {
        if (obj == null)
        {
            return 0;
        }
        else if (obj.Equals(DBNull.Value))
        {
            return 0;
        }
        else
        {
            return int.Parse(obj.ToString());
        }
    }

    public static DateTime DataReaderGetDateTime(object obj)
    {
        if (obj == null)
        {
            return clsGlobal.DATE_MINVALUE;
        }
        else if (obj.Equals(DBNull.Value))
        {
            return clsGlobal.DATE_MINVALUE;
        }
        else
        {
            return DateTime.Parse(obj.ToString());
        }
    }

    public static bool DataReaderGetBoolean(object obj)
    {
        if (obj == null)
        {
            return false;
        }
        else if (obj.Equals(DBNull.Value))
        {
            return false;
        }
        else
        {
            if ((int) obj == 1 | (bool) obj == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public static byte[] DataReaderGetByteOfArray(object obj)
	{
		if (obj == null) {
			return new byte[1];
		} else if (obj.Equals(DBNull.Value)) {
			return new byte[1];
		} else {
			return (byte[]) obj;
		}
	}

    #endregion

    #region "Math"
    public static int ParseToInteger(object obj)
    {
        if (obj == null)
        {
            return 0;
        }
        else
        {
            try
            {
                return int.Parse(obj.ToString());
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }


    public static decimal ParseToDecimal(object obj)
    {
        return ParseToDecimal(obj, 0, string.Empty, string.Empty);
    }

    
    public static decimal ParseToDecimal(object obj, int intCount, string txtCurrencyRounding, string txtCurrencyID)
    {
        if (obj == null)
        {
            return 0;
        }
        else
        {
            try
            {
                if (txtCurrencyID.Equals(string.Empty) | txtCurrencyRounding.Equals(string.Empty))
                {
                    //Jika salah satu parameternya adalah kosong.
                    return decimal.Parse(obj.ToString().Trim());
                }

                if (txtCurrencyID.ToLower().Equals(txtCurrencyRounding.ToLower()))
                {
                    //Jika matauang yang dipilih adalah matauang yang dibulatkan. 
                    return decimal.Parse(obj.ToString().Trim());
                }
                else
                {
                    return decimal.Parse(obj.ToString());//  String.FormatNumber(obj.ToString().Trim(), intCount);
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }

    public static bool ParseToBoolean(object obj)
    {
        try
        {
            if (obj == null)
            {
                return false;
            }
            else
            {
                try
                {
                    if (obj.Equals(DBNull.Value))
                    {
                        return false;
                    }

                    return bool.Parse(obj.ToString());
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
        catch (Exception)
        {
            return false;
        }
    }

    public static DateTime ParseToDateTime(object obj)
    {
        try
        {
            if (obj == null)
            {
                return DATE_MINVALUE;
            }
            else
            {
                return DateTime.Parse(obj.ToString());

            }
        }
        catch (Exception)
        {
            return DATE_MINVALUE;
        }
    }

    public static DateTime ParseToDateTimeTo(object obj)
    {
        try
        {
            if (obj == null)
            {
                return DATE_MAXVALUE;
            }
            else
            {
                return DateTime.Parse(obj.ToString());

            }
        }
        catch (Exception)
        {
            return DATE_MAXVALUE;
        }
    }

    public static string ParseToFormatNumber(object obj, int intCount)
    {
        return ParseToFormatNumber(obj, intCount, "1", "2");
    }

    public static string ParseToFormatNumber(object obj, int intCount, string txtCurrencyRounding, string txtCurrencyID)
    {
        if (obj == null)
        {
            return decimal.Zero.ToString();
        }
        else
        {
            try
            {
                if (txtCurrencyID.Equals(string.Empty) | txtCurrencyRounding.Equals(string.Empty))
                {
                    //Jika salah satu parameternya adalah kosong.
                    return obj.ToString(); // String.FormatNumber(obj, 0);
                }

                if (txtCurrencyID.ToLower().Equals(txtCurrencyRounding.ToLower()))
                {
                    //Jika matauang yang dipilih adalah matauang yang dibulatkan. 
                    return obj.ToString(); //String.FormatNumber(obj, 0);
                }
                else
                {
                    return obj.ToString(); //String.FormatNumber(obj, intCount);
                }

            }
            catch (Exception)
            {
                return decimal.Zero.ToString();
            }
        }
    }

    public static string ParseToString(object obj)
    {
        if (obj == null)
        {
            return string.Empty;
        }
        else
        {
            try
            {
                return obj.ToString().Trim();
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
    #endregion


    #region "Print HTML"
    public static object PrintWarningItem(string warn)
    {
        return "<li>" + warn + "</li>";
    }
    public static object PrintWarningError(string message)
    {
        return "<ul class='textred'>" + message + "</ul>";
    }
    public static object PrintWarningSuccess(string message)
    {
        return "<ul class='textblue'>" + message + "</ul>";
    }
    public static object PrintDivWarning(string message)
    {
        return "<div class='contentsectionbig warning'>" + message + "</div>";
    }
    public static object PrintWarning(string message)
    {
        return "<span class='warning'>" + message + "</span>";
    }
    #endregion

}
 