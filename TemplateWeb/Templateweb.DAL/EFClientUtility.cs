using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using TemplateWeb.Common;

public class EFClientUtility
{
    private const int COMMAND_TIMEOUT = 0;

    public static string GetConnectionString()
    {
        return new clsSystemConfiguration().ConnectionStringSql;
    }
        
}
