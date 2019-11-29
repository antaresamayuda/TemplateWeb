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

public sealed class mUserRoleCustomBL
{
    public mUserRoleCustomBL()
    {
    }


    public static int SaveMUserRole(mUserRole dat, string txtUserID, string txtLangId)
    {
        DBEntities dObjContext = null;
        DbContextTransaction dObjTran = null;
        try
        {
            dObjContext = new DBEntities(EFClientUtility.GetConnectionString());
            dObjTran = dObjContext.Database.BeginTransaction();
            SaveMUserRole(dat, txtUserID, txtLangId, dObjContext, dObjTran);
            dObjTran.Commit();
            return dat.intUserID;
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

    public static int SaveMUserRole(mUserRole dat, string txtUserID, string txtLangId, DBEntities dObjContext, DbContextTransaction dObjTran)
    {
        ValidateInput(dat, txtUserID, txtLangId, dObjContext, dObjTran);

        dat.txtCreatedBy = txtUserID;
        dat.dtmCreatedDate = DateTime.Now;

        dObjContext.mUserRoles.Add(dat);
        dObjContext.SaveChanges();

        return dat.intUserRoleID;
    }

    public static bool UpdateMUserRole(mUserRole dat, string txtUserID, string txtLangId)
    {
        DBEntities dObjContext = null;
        DbContextTransaction dObjTran = null;
        try
        {
            dObjContext = new DBEntities(EFClientUtility.GetConnectionString());
            dObjTran = dObjContext.Database.BeginTransaction();
            UpdateMUserRole(dat, txtUserID, txtLangId, dObjContext, dObjTran);
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

    public static bool UpdateMUserRole(mUserRole dat, string txtUserID, string txtLangId, DBEntities dObjContext, DbContextTransaction dObjTran)
    {
        ValidateInput(dat, txtUserID, txtLangId, dObjContext, dObjTran);

        dat.txtUpdatedBy = txtUserID;
        dat.dtmUpdatedDate = DateTime.Now;
         
        dObjContext.Entry(dat).State = EntityState.Modified;
        dObjContext.SaveChanges();

        return true;
    }

    public static mUserRole GetMUserRole(int intUserRoleID)
    {
        DBEntities dObjContext = null;
        DbContextTransaction dObjTran = null;
        try
        {
            dObjContext = new DBEntities(EFClientUtility.GetConnectionString());
            return GetMUserRole(intUserRoleID, dObjContext, dObjTran);
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

    public static mUserRole GetMUserRole(int intUserRoleID, DBEntities dObjContext, DbContextTransaction dObjTran)
    {
        dObjContext.Configuration.ProxyCreationEnabled = false;
        var retDat = (from Item in dObjContext.mUserRoles
                where Item.intUserRoleID.Equals(intUserRoleID)
                select Item).FirstOrDefault();
        return retDat;
    }

    public static bool DeleteMUserRole(int intUserRoleID)
    {
        DBEntities dObjContext = null;
        DbContextTransaction dObjTran = null;
        try
        {
            dObjContext = new DBEntities(EFClientUtility.GetConnectionString());
            dObjTran = dObjContext.Database.BeginTransaction();
            DeleteMUserRole(intUserRoleID, dObjContext, dObjTran);
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

    public static bool DeleteMUserRole(int intUserRoleID, DBEntities dObjContext, DbContextTransaction dObjTran)
    {
        mUserRole dat = GetMUserRole(intUserRoleID, dObjContext, dObjTran);
        if (dat != null)
        {
            dObjContext.mUserRoles.Remove(dat);
            dObjContext.SaveChanges();
        }
        return true;
    }

    public static List<mUserRole> GetAllMUserRole()
    {
        DBEntities dObjContext = null;
        DbContextTransaction dObjTran = null;
        try
        {
            dObjContext = new DBEntities(EFClientUtility.GetConnectionString());
            return GetAllMUserRole(dObjContext, dObjTran);
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

    public static List<mUserRole> GetAllMUserRole(DBEntities dObjContext, DbContextTransaction dObjTran)
    {
        dObjContext.Configuration.ProxyCreationEnabled = false;
        var retList = (from Item in dObjContext.mUserRoles
                select Item).ToList();
        return retList;
    }

    public static bool IsExistMUserRole(int intUserRoleID)
    {
        DBEntities dObjContext = null;
        DbContextTransaction dObjTran = null;
        try
        {
            dObjContext = new DBEntities(EFClientUtility.GetConnectionString());
            return IsExistMUserRole(intUserRoleID, dObjContext, dObjTran);
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

    public static bool IsExistMUserRole(int intUserRoleID, DBEntities dObjContext, DbContextTransaction dObjTran)
    {
        mUserRole dat = new mUserRole();
        dat = GetMUserRole(intUserRoleID, dObjContext, dObjTran);
        if (dat == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
     
    public static List<clsMLOV> GetListLOVmUserRoleLOVPaging(string txtUserId, int intStart, int intEnd, string txtSearch, ref decimal decTotalRow)
    {
        DBEntities dObjContext = null;
        DbContextTransaction dObjTran = null;
        try
        {
            dObjContext = new DBEntities(EFClientUtility.GetConnectionString());
            return GetListLOVmUserRoleLOVPaging(txtUserId, intStart, intEnd, txtSearch, ref decTotalRow, dObjContext, dObjTran);
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

    public static List<clsMLOV> GetListLOVmUserRoleLOVPaging(string txtUserId, int intStart, int intEnd, string txtSearch, ref decimal decTotalRow, DBEntities dObjContext, DbContextTransaction dObjTran)
    {
        var retList = (from Item in dObjContext.mUserRoles
                join role in dObjContext.mRoles.DefaultIfEmpty() on Item.intRoleID equals role.intRoleID
                join user in dObjContext.mUsers.DefaultIfEmpty() on Item.intUserID equals user.intUserID
                where 1 == 1
                          && (txtSearch == ""
                          || Item.intUserRoleID.ToString().Contains(txtSearch)
                          || user.txtUserName.ToString().Contains(txtSearch)
                          || role.txtRoleName.ToString().Contains(txtSearch)
                          )
                select new clsMLOV
                {
                    txtColumn1 = Item.intUserRoleID.ToString(),
                    txtColumn2 = user.txtUserName.ToString(),
                    txtColumn3 = role.txtRoleName.ToString(), 
                    txtColumnName1 = "User ROle ID",
                    txtColumnName2 = "User Name",
                    txtColumnName3 = "Role Name"
                }
              ).ToList();

        decTotalRow = retList.Count();
        return retList.Skip(intStart - 1).Take(intEnd).ToList();
    }


    public static List<mUserRole> GetAllmUserRoleByRoleAndUser(int intRoleID, int intUserID)
    {
        DBEntities dObjContext = null;
        DbContextTransaction dObjTran = null;
        try
        {
            dObjContext = new DBEntities(EFClientUtility.GetConnectionString());
            return GetAllmUserRoleByRoleAndUser(intRoleID, intUserID, dObjContext, dObjTran);
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

    public static List<mUserRole> GetAllmUserRoleByRoleAndUser(int intRoleID, int intUserID, DBEntities dObjContext, DbContextTransaction dObjTran)
    {
        var retList = new List<mUserRole> { };
        if (clsGlobal.ParseToInteger(intRoleID) != 0 && clsGlobal.ParseToInteger(intUserID) != 0)
        {
            retList = (from Item in dObjContext.mUserRoles
                       where Item.intUserID == intUserID && Item.intRoleID == intRoleID
                       select Item).ToList();
        }
        else if (clsGlobal.ParseToInteger(intRoleID) != 0)
        {
            retList = (from Item in dObjContext.mUserRoles
                       where Item.intRoleID == intRoleID
                       select Item).ToList();
        }
        else
        {
            retList = (from Item in dObjContext.mUserRoles
                       where Item.intUserID == intUserID
                       select Item).ToList();
        }
        return retList;
    }

    private static bool ValidateInput(mUserRole dat, string txtUserID, string txtLangId, DBEntities dObjContext, DbContextTransaction dObjTran)
    {
        return true;
    }

    public static mUserRole CreateBlankmUserRole()
    {
        mUserRole dat = new mUserRole();
        dat.intUserRoleID = 0;
        dat.intRoleID = 0;
        dat.intUserID = 0; 

        dat.dtmCreatedDate = DateTime.Now;
        dat.dtmUpdatedDate = DateTime.Now;
        dat.txtUpdatedBy = string.Empty;
        dat.txtCreatedBy = string.Empty;

        return dat;
    }

    public static mUserRole parseFromJSON(JObject jsonDat)
    {
        mUserRole dat = new mUserRole();
        dat.intRoleID = clsGlobal.ParseToInteger(jsonDat["intRoleID"].ToString());
        dat.intUserID = clsGlobal.ParseToInteger(jsonDat["intUserID"].ToString());
        dat.intUserRoleID = clsGlobal.ParseToInteger(jsonDat["intUserRoleID"].ToString());


        dat.dtmCreatedDate = clsGlobal.ParseToDateTime(jsonDat["dtmCreatedDate"].ToString());
        dat.dtmUpdatedDate = DateTime.Now;
        dat.txtCreatedBy = clsGlobal.ParseToString(jsonDat["txtCreatedBy"].ToString());
        dat.txtUpdatedBy = clsGlobal.ParseToString(jsonDat["txtUpdatedBy"].ToString());

        dat.dtmCreatedDate = DateTime.Parse(dat.dtmCreatedDate.ToString()).AddHours(TimeZone.CurrentTimeZone.GetUtcOffset(new DateTime()).Hours);

        return dat;
    }
}