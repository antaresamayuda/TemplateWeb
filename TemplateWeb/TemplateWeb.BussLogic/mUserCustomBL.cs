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
using AutoMapper;
using TemplateWeb.Common.ViewModels;

public sealed class mUserCustomBL
{
    public mUserCustomBL()
    {
    }

    public static int SaveUser(mUser dat, List<mUserRole> listMenu, string txtUserName)
    {
        DBEntities dObjContext = null;
        DbContextTransaction dObjTran = null;
        try
        {
            dObjContext = new DBEntities(EFClientUtility.GetConnectionString());
            dObjTran = dObjContext.Database.BeginTransaction();
            SaveUser(dat, listMenu, txtUserName, dObjContext, dObjTran);
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

    public static int SaveUser(mUser dat, List<mUserRole> listMenu, string txtUserName, DBEntities dObjContext, DbContextTransaction dObjTran)
    {
        dat.txtCreatedBy = txtUserName;
        dat.dtmCreatedDate = DateTime.Now;

        List<mUserRole> listUserRole = new List<mUserRole>();

        foreach (var a in listMenu)
        {
            mUserRole b = new mUserRole();

            b.txtCreatedBy = txtUserName;
            b.dtmCreatedDate = DateTime.Now;
            b.intRoleID = a.intRoleID;
            b.intUserRoleID = 0;
            b.intUserID = dat.intUserID;

            listUserRole.Add(b);
        }

        dat.mUserRoles = listUserRole;
        dObjContext.mUsers.Add(dat);
        dObjContext.SaveChanges();

        //Create records
        mUserRecord record = new mUserRecord();
        record.bitLock = false;
        record.dtmLastLogin = DateTime.Now;
        record.intUserID = dat.intUserID;
        record.intUserRecordID = 0;
        record.intWrongPassCount = 0;
        record.txtPreviousPass1 = dat.txtPassword;
        dObjContext.mUserRecords.Add(record);
        dObjContext.SaveChanges();

        return dat.intUserID;
    }

    public static void UpdateUser(mUser dat, List<mUserRole> listMenu, string txtUserName)
    {
        DBEntities dObjContext = null;
        DbContextTransaction dObjTran = null;
        try
        {
            dObjContext = new DBEntities(EFClientUtility.GetConnectionString());
            dObjTran = dObjContext.Database.BeginTransaction();
            UpdateUser(dat, listMenu, txtUserName, dObjContext, dObjTran);
            dObjTran.Commit();
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

    public static void UpdateUser(mUser dat, List<mUserRole> listMenu, string txtUserName, DBEntities dObjContext, DbContextTransaction dObjTran)
    {
        dat.txtUpdatedBy = txtUserName;
        dat.dtmUpdatedDate = DateTime.Now;
        dat.mUserRoles = null;

        foreach (var a in listMenu)
        {
            mUserRole b = new mUserRole();

            b.txtCreatedBy = txtUserName;
            b.dtmCreatedDate = DateTime.Now;
            b.intRoleID = a.intRoleID;
            b.intUserRoleID = 0;
            b.intUserID = dat.intUserID;

            dObjContext.mUserRoles.Add(b);
        }

        dObjContext.mUserRoles.RemoveRange(dObjContext.mUserRoles.Where(x => x.intUserID == dat.intUserID).ToList());

        dObjContext.Entry(dat).State = EntityState.Modified;
        dObjContext.SaveChanges();
    }

    public static mUser GetUser(int intUserID)
    {
        DBEntities dObjContext = null;
        DbContextTransaction dObjTran = null;
        try
        {
            dObjContext = new DBEntities(EFClientUtility.GetConnectionString());
            return GetUser(intUserID, dObjContext, dObjTran);
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

    public static mUser GetUser(int intUserID, DBEntities dObjContext, DbContextTransaction dObjTran)
    {
        dObjContext.Configuration.ProxyCreationEnabled = false;
        var retDat = (from User in dObjContext.mUsers
                      where User.intUserID.Equals(intUserID)
                      select User).FirstOrDefault();
        return retDat;
    }

    public static bool DeleteUser(mUser dat, List<mUserRole> listMenu, string txtUserName)
    {
        DBEntities dObjContext = null;
        DbContextTransaction dObjTran = null;
        try
        {
            dObjContext = new DBEntities(EFClientUtility.GetConnectionString());
            dObjTran = dObjContext.Database.BeginTransaction();
            DeleteUser(dat, listMenu, txtUserName, dObjContext, dObjTran);
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

    public static bool DeleteUser(mUser dat, List<mUserRole> listMenu, string txtUserName, DBEntities dObjContext, DbContextTransaction dObjTran)
    {
        mUser mUser = GetUser(dat.intUserID, dObjContext, dObjTran);

        if (mUser != null)
        {
            dObjContext.mUserRoles.RemoveRange(dObjContext.mUserRoles.Where(x => x.intUserID == mUser.intUserID));
            dObjContext.mUsers.Remove(mUser);
            dObjContext.SaveChanges();
        }

        return true;
    }

    public static List<mUser> GetAllUsers()
    {
        DBEntities dObjContext = null;
        DbContextTransaction dObjTran = null;
        try
        {
            dObjContext = new DBEntities(EFClientUtility.GetConnectionString());
            return GetAllUsers(dObjContext, dObjTran);
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

    public static List<mUser> GetAllUsers(DBEntities dObjContext, DbContextTransaction dObjTran)
    {
        dObjContext.Configuration.ProxyCreationEnabled = false;

        List<mUser> listDat = new List<mUser>();
        listDat = dObjContext.mUsers.ToList();

        return listDat;
    }

    public static List<mUser> GetAllIndex()
    {
        DBEntities dObjContext = null;
        DbContextTransaction dObjTran = null;
        try
        {
            dObjContext = new DBEntities(EFClientUtility.GetConnectionString());
            return GetAllIndex(dObjContext, dObjTran);
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

    public static List<mUser> GetAllIndex(DBEntities dObjContext, DbContextTransaction dObjTran)
    {
        dObjContext.Configuration.ProxyCreationEnabled = false;

        List<mUser> listDat = new List<mUser>();
        listDat = dObjContext.mUsers.ToList();

        foreach(var a in listDat)
        {
            a.mUserRoles = dObjContext.mUserRoles.Where(x => x.intUserID == a.intUserID).ToList();

            foreach(var b in a.mUserRoles)
            {
                b.mRole = dObjContext.mRoles.Where(x => x.intRoleID == b.intRoleID).FirstOrDefault();
            }

            a.txtCompanyName = dObjContext.mCompanies.Where(x => x.intCompanyID == a.intCompanyID).FirstOrDefault().txtCompanyName;
        }

        return listDat;
    }

    public static List<mUser> GetAllUnlockIndex()
    {
        DBEntities dObjContext = null;
        DbContextTransaction dObjTran = null;
        try
        {
            dObjContext = new DBEntities(EFClientUtility.GetConnectionString());
            return GetAllUnlockIndex(dObjContext, dObjTran);
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

    public static List<mUser> GetAllUnlockIndex(DBEntities dObjContext, DbContextTransaction dObjTran)
    {
        dObjContext.Configuration.ProxyCreationEnabled = false;

        List<mUser> listDat = new List<mUser>();
        listDat = dObjContext.mUsers.ToList();

        foreach (var a in listDat)
        {
            a.mUserRoles = dObjContext.mUserRoles.Where(x => x.intUserID == a.intUserID).ToList();

            foreach (var b in a.mUserRoles)
            {
                b.mRole = dObjContext.mRoles.Where(x => x.intRoleID == b.intRoleID).FirstOrDefault();
            }

            a.txtCompanyName = dObjContext.mCompanies.Where(x => x.intCompanyID == a.intCompanyID).FirstOrDefault().txtCompanyName;
        }

        listDat = (from a in listDat
                   join b in dObjContext.mUserRecords
                   on a.intUserID equals b.intUserID
                   where b.bitLock == true
                   select a).ToList();

        return listDat;
    }


    public static bool checkGUID(string txtGUID)
    {
        DBEntities dObjContext = null;
        DbContextTransaction dObjTran = null;
        try
        {
            dObjContext = new DBEntities(EFClientUtility.GetConnectionString());
            return checkGUID(txtGUID, dObjContext, dObjTran);
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

    public static bool checkGUID(string txtGUID, DBEntities dObjContext, DbContextTransaction dObjTran)
    {
        dObjContext.Configuration.ProxyCreationEnabled = false;
        var retDat = (from User in dObjContext.mUsers
                      where User.txtGUID.Equals(txtGUID)
                      select User).FirstOrDefault();

        if (retDat == null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static List<mUserRole> GetUserRole(int intUserID)
    {
        DBEntities dObjContext = null;
        DbContextTransaction dObjTran = null;
        try
        {
            dObjContext = new DBEntities(EFClientUtility.GetConnectionString());
            return GetUserRole(intUserID, dObjContext, dObjTran);
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

    public static List<mUserRole> GetUserRole(int intUserID, DBEntities dObjContext, DbContextTransaction dObjTran)
    {
        dObjContext.Configuration.ProxyCreationEnabled = false;

        return dObjContext.mUserRoles.Where(x => x.intUserID == intUserID).ToList();
    }

    public static mUser GetMUserbyTxtUserName(string txtUserName)
    {
        DBEntities dObjContext = null;
        DbContextTransaction dObjTran = null;
        try
        {
            dObjContext = new DBEntities(EFClientUtility.GetConnectionString());
            return GetMUserbyTxtUserName(txtUserName, dObjContext, dObjTran);
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

    public static mUser GetMUserbyTxtUserName(string txtUserName, DBEntities dObjContext, DbContextTransaction dObjTran)
    {
        dObjContext.Configuration.ProxyCreationEnabled = false;
        var retDat = (from User in dObjContext.mUsers
                      where User.txtUserName.Equals(txtUserName)
                      select User).FirstOrDefault();
        return retDat;
    }

    public static bool Login(string txtUserName, string txtPassword, string txtLangId)
    {
        DBEntities dObjContext = null;
        DbContextTransaction dObjTran = null;
        try
        {
            dObjContext = new DBEntities(EFClientUtility.GetConnectionString());
            return Login(txtUserName, txtPassword, txtLangId, dObjContext, dObjTran);
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

    public static bool Login(string txtUserName, string txtPassword, string txtLangId, DBEntities dObjContext, DbContextTransaction dObjTran)
    {
        mUser userDat = mUserCustomBL.GetMUserbyTxtUserName(txtUserName, dObjContext, dObjTran);

        if (userDat != null)
        {
            if (userDat.txtPassword == txtPassword)
            {
                return true;
            }
            else
            {
                mUserRecord record = GetUserRecord(userDat.intUserID);
                record.intWrongPassCount = record.intWrongPassCount + 1;

                mUserCustomBL.UpdateRecord(record);

                return false;
            }
        }

        return false;
    }

    public static void ChangePassword(mUserChangePasswordVW dat, string txtUserName)
    {
        DBEntities dObjContext = null;
        DbContextTransaction dObjTran = null;
        try
        {
            dObjContext = new DBEntities(EFClientUtility.GetConnectionString());
            dObjTran = dObjContext.Database.BeginTransaction();
            ChangePassword(dat, txtUserName, dObjContext, dObjTran);
            dObjTran.Commit();
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

    public static void ChangePassword(mUserChangePasswordVW dat, string txtUserName, DBEntities dObjContext, DbContextTransaction dObjTran)
    {
        mUser userDat = GetUser(dat.intUserID);

        userDat.txtPassword = dat.txtNewPassword;
        userDat.txtUpdatedBy = txtUserName;
        userDat.dtmUpdatedDate = DateTime.Now;

        dObjContext.Entry(userDat).State = EntityState.Modified;
        dObjContext.SaveChanges();
    }

    public static void UnlockUser(int intUserID)
    {
        DBEntities dObjContext = null;
        DbContextTransaction dObjTran = null;
        try
        {
            dObjContext = new DBEntities(EFClientUtility.GetConnectionString());
            dObjTran = dObjContext.Database.BeginTransaction();
            UnlockUser(intUserID, dObjContext, dObjTran);
            dObjTran.Commit();
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

    public static void UnlockUser(int intUserID, DBEntities dObjContext, DbContextTransaction dObjTran)
    {
        mUser user = GetUser(intUserID, dObjContext, dObjTran);
        user.txtPassword = "1234";
        dObjContext.Entry(user).State = EntityState.Modified;

        mUserRecord record = mUserCustomBL.GetUserRecord(intUserID, dObjContext, dObjTran);
        record.dtmLastLogin = DateTime.Now;
        record.intWrongPassCount = 0;
        record.dtmLastWrongPass = null;
        record.bitLock = false;
        record.txtPreviousPass3 = record.txtPreviousPass2;
        record.txtPreviousPass2 = record.txtPreviousPass1;
        record.txtPreviousPass1 = "1234";
        dObjContext.Entry(record).State = EntityState.Modified;

        dObjContext.SaveChanges();
    }

    public static mUserRecord GetUserRecord(int intUserID)
    {
        DBEntities dObjContext = null;
        DbContextTransaction dObjTran = null;
        try
        {
            dObjContext = new DBEntities(EFClientUtility.GetConnectionString());
            return GetUserRecord(intUserID, dObjContext, dObjTran);
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

    public static mUserRecord GetUserRecord(int intUserID, DBEntities dObjContext, DbContextTransaction dObjTran)
    {
        dObjContext.Configuration.ProxyCreationEnabled = false;
        var retDat = new mUserRecord();
        retDat = (from User in dObjContext.mUserRecords
                      where User.intUserID.Equals(intUserID)
                      select User).FirstOrDefault();
        return retDat;
    }

    public static void UpdateRecord(mUserRecord dat)
    {
        DBEntities dObjContext = null;
        DbContextTransaction dObjTran = null;
        try
        {
            dObjContext = new DBEntities(EFClientUtility.GetConnectionString());
            dObjTran = dObjContext.Database.BeginTransaction();
            UpdateRecord(dat, dObjContext, dObjTran);
            dObjTran.Commit();
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

    public static void UpdateRecord(mUserRecord dat, DBEntities dObjContext, DbContextTransaction dObjTran)
    {
        dObjContext.Entry(dat).State = EntityState.Modified;
        dObjContext.SaveChanges();
    }

    public static void SaveLog(mUserLog dat)
    {
        DBEntities dObjContext = null;
        DbContextTransaction dObjTran = null;
        try
        {
            dObjContext = new DBEntities(EFClientUtility.GetConnectionString());
            dObjTran = dObjContext.Database.BeginTransaction();
            SaveLog(dat, dObjContext, dObjTran);
            dObjTran.Commit();
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

    public static void SaveLog(mUserLog dat, DBEntities dObjContext, DbContextTransaction dObjTran)
    {
        dObjContext.mUserLogs.Add(dat);
        dObjContext.SaveChanges();
    }
}