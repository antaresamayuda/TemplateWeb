using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using TemplateWeb.Common.Entity;
using TemplateWeb.Common;
using System.Data.Entity;
using System.Linq;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

public sealed class mRoleAccessCustomBL
{
    public mRoleAccessCustomBL()
    {
    }


    public static int SaveMRoleAccess(mRoleAccess dat, string txtUserID, string txtLangId)
    {
        DBEntities dObjContext = null;
        DbContextTransaction dObjTran = null;
        try
        {
            dObjContext = new DBEntities(EFClientUtility.GetConnectionString());
            dObjTran = dObjContext.Database.BeginTransaction();
            SaveMRoleAccess(dat, txtUserID, txtLangId, dObjContext, dObjTran);
            dObjTran.Commit();
            return dat.intRoleAccessID;
        }
        catch (Exception ex)
        {
            dObjTran.Rollback();
            throw ex;
        }
        finally
        {
            dObjContext.Dispose();
        }
    }

    public static int SaveMRoleAccess(mRoleAccess dat, string txtUserID, string txtLangId, DBEntities dObjContext, DbContextTransaction dObjTran)
    {
        ValidateInput(dat, txtUserID, txtLangId, dObjContext, dObjTran);

        dat.txtCreatedBy = txtUserID;
        dat.dtmCreatedDate = DateTime.Now;

        dObjContext.mRoleAccesses.Add(dat);
        dObjContext.SaveChanges();

        return dat.intRoleAccessID;
    }

    public static bool UpdateMRoleAccess(mRoleAccess dat, string txtUserID, string txtLangId)
    {
        DBEntities dObjContext = null;
        DbContextTransaction dObjTran = null;
        try
        {
            dObjContext = new DBEntities(EFClientUtility.GetConnectionString());
            dObjTran = dObjContext.Database.BeginTransaction();
            UpdateMRoleAccess(dat, txtUserID, txtLangId, dObjContext, dObjTran);
            dObjTran.Commit();
            return true;
        }
        catch (Exception ex)
        {
            dObjTran.Rollback();
            throw ex;
        }
        finally
        {
            dObjContext.Dispose();
        }
    }

    public static bool UpdateMRoleAccess(mRoleAccess dat, string txtUserID, string txtLangId, DBEntities dObjContext, DbContextTransaction dObjTran)
    {
        ValidateInput(dat, txtUserID, txtLangId, dObjContext, dObjTran);

        dat.txtUpdatedBy = txtUserID;
        dat.dtmUpdatedDate = DateTime.Now;

        dObjContext.Entry(dat).State = EntityState.Modified;
        dObjContext.SaveChanges();

        return true;
    }

    public static mRoleAccess GetMRoleAccess(int intRoleAccessID)
    {
        DBEntities dObjContext = null;
        DbContextTransaction dObjTran = null;
        try
        {
            dObjContext = new DBEntities(EFClientUtility.GetConnectionString());
            return GetMRoleAccess(intRoleAccessID, dObjContext, dObjTran);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            dObjContext.Dispose();
        }
    }

    public static mRoleAccess GetMRoleAccess(int intRoleAccessID, DBEntities dObjContext, DbContextTransaction dObjTran)
    {
        dObjContext.Configuration.ProxyCreationEnabled = false;
        return (from Item in dObjContext.mRoleAccesses
                where Item.intRoleAccessID.Equals(intRoleAccessID)
                select Item).FirstOrDefault();
    }

    public static bool DeleteMRoleAccess(int intRoleAccessID)
    {
        DBEntities dObjContext = null;
        DbContextTransaction dObjTran = null;
        try
        {
            dObjContext = new DBEntities(EFClientUtility.GetConnectionString());
            dObjTran = dObjContext.Database.BeginTransaction();
            DeleteMRoleAccess(intRoleAccessID, dObjContext, dObjTran);
            dObjTran.Commit();
            return true;
        }
        catch (Exception ex)
        {
            dObjTran.Rollback();
            throw ex;
        }
        finally
        {
            dObjContext.Dispose();
        }
    }

    public static bool DeleteMRoleAccess(int intRoleAccessID, DBEntities dObjContext, DbContextTransaction dObjTran)
    {
        mRoleAccess dat = GetMRoleAccess(intRoleAccessID, dObjContext, dObjTran);
        if (dat != null)
        {
            dObjContext.mRoleAccesses.Remove(dat);
            dObjContext.SaveChanges();
        }
        return true;
    }

    public static List<mRoleAccess> GetAllMRoleAccess()
    {
        DBEntities dObjContext = null;
        DbContextTransaction dObjTran = null;
        try
        {
            dObjContext = new DBEntities(EFClientUtility.GetConnectionString());
            return GetAllMRoleAccess(dObjContext, dObjTran);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            dObjContext.Dispose();
        }
    }

    public static List<mRoleAccess> GetAllMRoleAccess(DBEntities dObjContext, DbContextTransaction dObjTran)
    {
        dObjContext.Configuration.ProxyCreationEnabled = false;
        return (from Item in dObjContext.mRoleAccesses
                select Item).ToList();
    }

    public static bool IsExistMRoleAccess(int intRoleAccessID)
    {
        DBEntities dObjContext = null;
        DbContextTransaction dObjTran = null;
        try
        {
            dObjContext = new DBEntities(EFClientUtility.GetConnectionString());
            return IsExistMRoleAccess(intRoleAccessID, dObjContext, dObjTran);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            dObjContext.Dispose();

        }
    }

    public static bool IsExistMRoleAccess(int intRoleAccessID, DBEntities dObjContext, DbContextTransaction dObjTran)
    {
        mRoleAccess dat = new mRoleAccess();
        dat = GetMRoleAccess(intRoleAccessID, dObjContext, dObjTran);
        if (dat == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public static mRoleAccess GetPrivilegeUser(int intRoleID, int intModuleID)
    {
        DBEntities dObjContext = null;
        DbContextTransaction dObjTran = null;
        try
        {
            dObjContext = new DBEntities(EFClientUtility.GetConnectionString());
            return GetPrivilegeUser(intRoleID, intModuleID, dObjContext, dObjTran);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            dObjContext.Dispose();
        }
    }

    public static mRoleAccess GetPrivilegeUser(int intRoleID, int intModuleID, DBEntities dObjContext, DbContextTransaction dObjTran)
    {
        dObjContext.Configuration.ProxyCreationEnabled = false;
        return (from Item in dObjContext.mRoleAccesses
                where Item.intRoleID == intRoleID && Item.intModuleID == intModuleID
                select Item).FirstOrDefault();
    }


    public static List<clsMLOV> GetListLOVmRoleAccessLOVPaging(string txtUserId, int intStart, int intEnd, string txtSearch, ref decimal decTotalRow)
    {
        DBEntities dObjContext = null;
        DbContextTransaction dObjTran = null;
        try
        {
            dObjContext = new DBEntities(EFClientUtility.GetConnectionString());
            return GetListLOVmRoleAccessLOVPaging(txtUserId, intStart, intEnd, txtSearch, ref decTotalRow, dObjContext, dObjTran);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            dObjContext.Dispose();
        }
    }

    public static List<clsMLOV> GetListLOVmRoleAccessLOVPaging(string txtUserId, int intStart, int intEnd, string txtSearch, ref decimal decTotalRow, DBEntities dObjContext, DbContextTransaction dObjTran)
    {
        var retList = (from Item in dObjContext.mRoleAccesses
                       join role in dObjContext.mRoles.DefaultIfEmpty() on Item.intRoleID equals role.intRoleID
                       join module in dObjContext.mModules.DefaultIfEmpty() on Item.intModuleID equals module.intModuleID
                       where 1 == 1
                              && (txtSearch == ""
                              || Item.intRoleAccessID.ToString().Contains(txtSearch)
                              || Item.intModuleID.ToString().Contains(txtSearch)
                              || Item.intRoleID.ToString().Contains(txtSearch)
                              || role.txtRoleName.ToString().Contains(txtSearch)
                              || module.txtModuleName.ToString().Contains(txtSearch)
                              )
                       select new clsMLOV
                       {
                           txtColumn1 = Item.intRoleAccessID.ToString(),
                           txtColumn2 = role.txtRoleName.ToString(),
                           txtColumn3 = module.txtModuleName.ToString(),
                           txtColumnName1 = "User ROle ID",
                           txtColumnName2 = "Role Name",
                           txtColumnName3 = "Module Name"
                       }
              ).ToList();

        decTotalRow = retList.Count();
        return retList.Skip(intStart - 1).Take(intEnd).ToList();
    }

    public static bool ValidateInput(mRoleAccess dat, string txtRoleAccessID, string txtlangId)
    {
        DBEntities dObjContext = null;
        DbContextTransaction dObjTran = null;
        try
        {
            dObjContext = new DBEntities(EFClientUtility.GetConnectionString());
            return ValidateInput(dat, txtRoleAccessID, txtlangId, dObjContext, dObjTran);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            dObjContext.Dispose();
        }
    }

    private static bool ValidateInput(mRoleAccess dat, string txtRoleAccessID, string txtLangId, DBEntities dObjContext, DbContextTransaction dObjTran)
    {

        if (dat.intRoleID == 0)
        {
            throw new Exception("Role is required!");
        }
        if (dat.intModuleID == 0)
        {
            throw new Exception("Module is required!");
        }

        //Check if Name Already Exist. 
        mRoleAccess prevDat = mRoleAccessCustomBL.GetPrivilegeUser(clsGlobal.ParseToInteger(dat.intRoleID), clsGlobal.ParseToInteger(dat.intModuleID));
        if ((prevDat != null))
        {
            if (!(prevDat.intRoleID == dat.intRoleID))
            {
                throw new Exception("Role Access is already exist!");
            }
        }
        return true;
    }

    public static mRoleAccess GetPrivilegeUserUrl(int intRoleID, string txtUrl)
    {
        DBEntities dObjContext = null;
        DbContextTransaction dObjTran = null;
        try
        {
            dObjContext = new DBEntities(EFClientUtility.GetConnectionString());
            return GetPrivilegeUserUrl(intRoleID, txtUrl, dObjContext, dObjTran);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            dObjContext.Dispose();
        }
    }

    public static mRoleAccess GetPrivilegeUserUrl(int intRoleID, string txtUrl, DBEntities dObjContext, DbContextTransaction dObjTran)
    {
        //string txtReplaceUrl = txtUrl.Replace("/Index", "");
        //txtReplaceUrl = txtUrl.Replace("http://", "");
        //txtReplaceUrl = txtUrl.Replace("https://", "");
        
        dObjContext.Configuration.ProxyCreationEnabled = false;

        //return (from Item in dObjContext.mRoleAccesses
        //        join module in dObjContext.mModules on Item.intModuleID equals module.intModuleID
        //        join menu in dObjContext.mMenus on module.intModuleID equals menu.intModuleID
        //        where 1 == 1
        //        && Item.intRoleID == intRoleID
        //        && menu.txtLink.ToLower().Contains(txtUrl.ToLower())
        //        select Item).AsNoTracking().FirstOrDefault();

        return (from Item in dObjContext.mRoleAccesses
                join module in dObjContext.mModules on Item.intModuleID equals module.intModuleID
                join menu in dObjContext.mMenus on module.intModuleID equals menu.intModuleID
                where 1 == 1
                && Item.intRoleID == intRoleID
                && txtUrl.ToLower().Contains(menu.txtLink.ToLower())
                select Item).AsNoTracking().FirstOrDefault();

    }

    public static mRoleAccess CreateBlankmRoleAccess()
    {
        mRoleAccess dat = new mRoleAccess();
        dat.bitEdit = false;
        dat.bitView = false;
        dat.bitDelete = false;
        dat.bitPrint = false;
        dat.dtmCreatedDate = DateTime.Now;
        dat.dtmUpdatedDate = DateTime.Now;
        dat.intRoleAccessID = 0;
        dat.intModuleID = 0;
        dat.intRoleID = 0;
        dat.txtCreatedBy = string.Empty;
        dat.txtUpdatedBy = string.Empty;

        return dat;
    }

    public static mRoleAccess parseFromJSON(JObject jsonDat)
    {
        mRoleAccess dat = new mRoleAccess();

        dat.bitEdit = clsGlobal.ParseToBoolean(jsonDat["bitEdit"].ToString());
        dat.bitView = clsGlobal.ParseToBoolean(jsonDat["bitView"].ToString());
        dat.bitDelete = clsGlobal.ParseToBoolean(jsonDat["bitDelete"].ToString());
        dat.bitPrint = clsGlobal.ParseToBoolean(jsonDat["bitPrint"].ToString());
        dat.dtmCreatedDate = clsGlobal.ParseToDateTime(jsonDat["dtmCreatedDate"].ToString());
        dat.dtmUpdatedDate = DateTime.Now;
        dat.intRoleAccessID = clsGlobal.ParseToInteger(jsonDat["intRoleAccessID"].ToString());
        dat.intModuleID = clsGlobal.ParseToInteger(jsonDat["intModuleID"].ToString());
        dat.intRoleID = clsGlobal.ParseToInteger(jsonDat["intRoleID"].ToString());
        dat.txtCreatedBy = clsGlobal.ParseToString(jsonDat["txtCreatedBy"].ToString());
        dat.txtUpdatedBy = clsGlobal.ParseToString(jsonDat["txtUpdatedBy"].ToString());

        dat.dtmCreatedDate = DateTime.Parse(dat.dtmCreatedDate.ToString()).AddHours(TimeZone.CurrentTimeZone.GetUtcOffset(new DateTime()).Hours);

        return dat;
    }

}
//}
