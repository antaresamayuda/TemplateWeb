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

public sealed class mModuleCustomBL
{
    public mModuleCustomBL()
    {
    }

    public static int SaveModule(mModule dat, string txtUserName)
    {
        DBEntities dObjContext = null;
        DbContextTransaction dObjTran = null;
        try
        {
            dObjContext = new DBEntities(EFClientUtility.GetConnectionString());
            dObjTran = dObjContext.Database.BeginTransaction();
            SaveModule(dat, txtUserName, dObjContext, dObjTran);
            dObjTran.Commit();
            return dat.intModuleID;
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

    public static int SaveModule(mModule dat, string txtUserName, DBEntities dObjContext, DbContextTransaction dObjTran)
    {
        if (checkGUID(dat.txtGUID, dObjContext, dObjTran))
        {
            if(GetModuleByName(dat.txtModuleName) == null)
            {
                dat.txtCreatedBy = txtUserName;
                dat.dtmCreatedDate = DateTime.Now;
                dObjContext.mModules.Add(dat);
                dObjContext.SaveChanges();
            }
            else
            {
                throw new Exception("Data is already exist!");
            }           
        }      

        return dat.intModuleID;
    }

    public static void UpdateModule(mModule dat, string txtUserName)
    {
        DBEntities dObjContext = null;
        DbContextTransaction dObjTran = null;
        try
        {
            dObjContext = new DBEntities(EFClientUtility.GetConnectionString());
            dObjTran = dObjContext.Database.BeginTransaction();
            UpdateModule(dat, txtUserName, dObjContext, dObjTran);
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

    public static void UpdateModule(mModule dat, string txtUserName, DBEntities dObjContext, DbContextTransaction dObjTran)
    {
        if (checkGUID(dat.txtGUID, dObjContext, dObjTran))
        {
            dat.txtUpdatedBy = txtUserName;
            dat.dtmUpdatedDate = DateTime.Now;
            dObjContext.Entry(dat).State = EntityState.Modified;
            dObjContext.SaveChanges();
        }       
    }

    public static mModule GetModule(int intModuleID)
    {
        DBEntities dObjContext = null;
        DbContextTransaction dObjTran = null;
        try
        {
            dObjContext = new DBEntities(EFClientUtility.GetConnectionString());
            return GetModule(intModuleID, dObjContext, dObjTran);
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

    public static mModule GetModule(int intModuleID, DBEntities dObjContext, DbContextTransaction dObjTran)
    {
        dObjContext.Configuration.ProxyCreationEnabled = false;
        var retDat = (from Module in dObjContext.mModules
                      where Module.intModuleID.Equals(intModuleID)
                      select Module).FirstOrDefault();
        return retDat;
    }

    public static mModule GetModuleByName(string txtModuleName)
    {
        DBEntities dObjContext = null;
        DbContextTransaction dObjTran = null;
        try
        {
            dObjContext = new DBEntities(EFClientUtility.GetConnectionString());
            return GetModuleByName(txtModuleName, dObjContext, dObjTran);
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

    public static mModule GetModuleByName(string txtModuleName, DBEntities dObjContext, DbContextTransaction dObjTran)
    {
        dObjContext.Configuration.ProxyCreationEnabled = false;
        var retDat = (from Module in dObjContext.mModules
                      where Module.txtModuleName.Equals(txtModuleName)
                      select Module).FirstOrDefault();
        return retDat;
    }

    public static bool DeleteModule(mModule dat, string txtUserName)
    {
        DBEntities dObjContext = null;
        DbContextTransaction dObjTran = null;
        try
        {
            dObjContext = new DBEntities(EFClientUtility.GetConnectionString());
            dObjTran = dObjContext.Database.BeginTransaction();
            DeleteModule(dat, txtUserName, dObjContext, dObjTran);
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

    public static bool DeleteModule(mModule dat, string txtUserName, DBEntities dObjContext, DbContextTransaction dObjTran)
    {
        mModule mModule = GetModule(dat.intModuleID, dObjContext, dObjTran);

        if (mModule != null)
        {
            dObjContext.mModules.Remove(mModule);
            dObjContext.SaveChanges();
        }

        return true;
    }

    public static List<mModule> GetAllModules()
    {
        DBEntities dObjContext = null;
        DbContextTransaction dObjTran = null;
        try
        {
            dObjContext = new DBEntities(EFClientUtility.GetConnectionString());
            return GetAllModules(dObjContext, dObjTran);
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

    public static List<mModule> GetAllModules(DBEntities dObjContext, DbContextTransaction dObjTran)
    {
        dObjContext.Configuration.ProxyCreationEnabled = false;

        List<mModule> listDat = new List<mModule>();
        listDat = dObjContext.mModules.ToList();

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
        var retDat = (from Module in dObjContext.mModules
                      where Module.txtGUID.Equals(txtGUID)
                      select Module).FirstOrDefault();

        if (retDat == null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}