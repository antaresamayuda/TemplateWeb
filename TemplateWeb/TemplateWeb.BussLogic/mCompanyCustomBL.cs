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

public sealed class mCompanyCustomBL
{
    public mCompanyCustomBL()
    {
    }

    public static int SaveCompany(mCompany dat, string txtUserName)
    {
        DBEntities dObjContext = null;
        DbContextTransaction dObjTran = null;
        try
        {
            dObjContext = new DBEntities(EFClientUtility.GetConnectionString());
            dObjTran = dObjContext.Database.BeginTransaction();
            SaveCompany(dat, txtUserName, dObjContext, dObjTran);
            dObjTran.Commit();
            return dat.intCompanyID;
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

    public static int SaveCompany(mCompany dat, string txtUserName, DBEntities dObjContext, DbContextTransaction dObjTran)
    {
        dat.txtCreatedBy = txtUserName;
        dat.dtmCreatedDate = DateTime.Now;
        dObjContext.mCompanies.Add(dat);
        dObjContext.SaveChanges();

        return dat.intCompanyID;
    }

    public static void UpdateCompany(mCompany dat, string txtUserName)
    {
        DBEntities dObjContext = null;
        DbContextTransaction dObjTran = null;
        try
        {
            dObjContext = new DBEntities(EFClientUtility.GetConnectionString());
            dObjTran = dObjContext.Database.BeginTransaction();
            UpdateCompany(dat, txtUserName, dObjContext, dObjTran);
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

    public static void UpdateCompany(mCompany dat, string txtUserName, DBEntities dObjContext, DbContextTransaction dObjTran)
    {
        dat.txtUpdatedBy = txtUserName;
        dat.dtmUpdatedDate = DateTime.Now;
        dObjContext.Entry(dat).State = EntityState.Modified;
        dObjContext.SaveChanges();
    }

    public static mCompany GetCompany(int intCompanyID)
    {
        DBEntities dObjContext = null;
        DbContextTransaction dObjTran = null;
        try
        {
            dObjContext = new DBEntities(EFClientUtility.GetConnectionString());
            return GetCompany(intCompanyID, dObjContext, dObjTran);
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

    public static mCompany GetCompany(int intCompanyID, DBEntities dObjContext, DbContextTransaction dObjTran)
    {
        dObjContext.Configuration.ProxyCreationEnabled = false;
        var retDat = (from Company in dObjContext.mCompanies
                      where Company.intCompanyID.Equals(intCompanyID)
                      select Company).FirstOrDefault();
        return retDat;
    }

    public static bool DeleteCompany(mCompany dat, string txtUserName)
    {
        DBEntities dObjContext = null;
        DbContextTransaction dObjTran = null;
        try
        {
            dObjContext = new DBEntities(EFClientUtility.GetConnectionString());
            dObjTran = dObjContext.Database.BeginTransaction();
            DeleteCompany(dat, txtUserName, dObjContext, dObjTran);
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

    public static bool DeleteCompany(mCompany dat, string txtUserName, DBEntities dObjContext, DbContextTransaction dObjTran)
    {
        mCompany mCompany = mCompanyCustomBL.GetCompany(dat.intCompanyID, dObjContext, dObjTran);

        if (mCompany != null)
        {
            dObjContext.mCompanies.Remove(mCompany);
            dObjContext.SaveChanges();
        }

        return true;
    }

    public static List<mCompany> GetAllCompanies()
    {
        DBEntities dObjContext = null;
        DbContextTransaction dObjTran = null;
        try
        {
            dObjContext = new DBEntities(EFClientUtility.GetConnectionString());
            return GetAllCompanies(dObjContext, dObjTran);
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

    public static List<mCompany> GetAllCompanies(DBEntities dObjContext, DbContextTransaction dObjTran)
    {
        dObjContext.Configuration.ProxyCreationEnabled = false;

        List<mCompany> listDat = new List<mCompany>();
        listDat = dObjContext.mCompanies.ToList();

        return listDat;
    }

    public static List<mCompany> GetAllIndex()
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

    public static List<mCompany> GetAllIndex(DBEntities dObjContext, DbContextTransaction dObjTran)
    {
        dObjContext.Configuration.ProxyCreationEnabled = false;

        List<mCompany> listDat = new List<mCompany>();
        listDat = dObjContext.mCompanies.ToList();

        return listDat;
    }

    public static List<mBidangUsaha> GetAllBidangUsaha()
    {
        DBEntities dObjContext = null;
        DbContextTransaction dObjTran = null;
        try
        {
            dObjContext = new DBEntities(EFClientUtility.GetConnectionString());
            return GetAllBidangUsaha(dObjContext, dObjTran);
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

    public static List<mBidangUsaha> GetAllBidangUsaha(DBEntities dObjContext, DbContextTransaction dObjTran)
    {
        dObjContext.Configuration.ProxyCreationEnabled = false;

        List<mBidangUsaha> listDat = new List<mBidangUsaha>();
        listDat = dObjContext.mBidangUsahas.ToList();

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
        var retDat = (from Company in dObjContext.mCompanies
                      where Company.txtGUID.Equals(txtGUID)
                      select Company).FirstOrDefault();

        if(retDat == null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}