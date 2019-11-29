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

public sealed class mRoleCustomBL
{
    public mRoleCustomBL()
    {
    }

    public static int SaveRole(mRole dat, List<mMenu> listMenu, string txtUserName)
    {
        DBEntities dObjContext = null;
        DbContextTransaction dObjTran = null;
        try
        {
            dObjContext = new DBEntities(EFClientUtility.GetConnectionString());
            dObjTran = dObjContext.Database.BeginTransaction();
            SaveRole(dat, listMenu, txtUserName, dObjContext, dObjTran);
            dObjTran.Commit();
            return dat.intRoleID;
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

    public static int SaveRole(mRole dat, List<mMenu> listMenu, string txtUserName, DBEntities dObjContext, DbContextTransaction dObjTran)
    {
        List<mRoleAccess> listRoleAccess = new List<mRoleAccess>();

        foreach (var a in listMenu)
        {
            mRoleAccess b = new mRoleAccess();

            b.bitDelete = true;
            b.bitEdit = true;
            b.bitPrint = true;
            b.bitView = true;
            b.txtCreatedBy = txtUserName;
            b.dtmCreatedDate = DateTime.Now;
            b.intModuleID = a.intModuleID.Value;
            b.intRoleAccessID = 0;
            b.intRoleID = dat.intRoleID;

            if(a.bitChecklist == true)
            {
                listRoleAccess.Add(b);
            }           
        }

        dat.mRoleAccesses = listRoleAccess;
        dat.txtCreatedBy = txtUserName;
        dat.dtmCreatedDate = DateTime.Now;

        dObjContext.mRoles.Add(dat);
        dObjContext.SaveChanges();

        return dat.intRoleID;
    }

    public static void UpdateRole(mRole dat, List<mMenu> listMenu, string txtUserName)
    {
        DBEntities dObjContext = null;
        DbContextTransaction dObjTran = null;
        try
        {
            dObjContext = new DBEntities(EFClientUtility.GetConnectionString());
            dObjTran = dObjContext.Database.BeginTransaction();
            UpdateRole(dat, listMenu, txtUserName, dObjContext, dObjTran);
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

    public static void UpdateRole(mRole dat, List<mMenu> listMenu, string txtUserName, DBEntities dObjContext, DbContextTransaction dObjTran)
    {               
        foreach (var a in listMenu)
        {
            mRoleAccess b = new mRoleAccess();

            b.bitDelete = true;
            b.bitEdit = true;
            b.bitPrint = true;
            b.bitView = true;
            b.txtCreatedBy = txtUserName;
            b.dtmCreatedDate = DateTime.Now;
            b.intModuleID = a.intModuleID.Value;
            b.intRoleAccessID = 0;
            b.intRoleID = dat.intRoleID;

            if (a.bitChecklist == true)
            {
                dObjContext.mRoleAccesses.Add(b);
            }
        }

        dat.txtUpdatedBy = txtUserName;
        dat.dtmUpdatedDate = DateTime.Now;

        dObjContext.mRoleAccesses.RemoveRange(dObjContext.mRoleAccesses.Where(x => x.intRoleID == dat.intRoleID).ToList());

        dObjContext.Entry(dat).State = EntityState.Modified;
        dObjContext.SaveChanges();
    }

    public static mRole GetRole(int intRoleID)
    {
        DBEntities dObjContext = null;
        DbContextTransaction dObjTran = null;
        try
        {
            dObjContext = new DBEntities(EFClientUtility.GetConnectionString());
            return GetRole(intRoleID, dObjContext, dObjTran);
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

    public static mRole GetRole(int intRoleID, DBEntities dObjContext, DbContextTransaction dObjTran)
    {
        dObjContext.Configuration.ProxyCreationEnabled = false;
        var retDat = (from Role in dObjContext.mRoles
                      where Role.intRoleID.Equals(intRoleID)
                      select Role).FirstOrDefault();
        return retDat;
    }

    public static bool DeleteRole(mRole dat, List<mMenu> listMenu, string txtUserName)
    {
        DBEntities dObjContext = null;
        DbContextTransaction dObjTran = null;
        try
        {
            dObjContext = new DBEntities(EFClientUtility.GetConnectionString());
            dObjTran = dObjContext.Database.BeginTransaction();
            DeleteRole(dat, listMenu, txtUserName, dObjContext, dObjTran);
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

    public static bool DeleteRole(mRole dat, List<mMenu> listMenu, string txtUserName, DBEntities dObjContext, DbContextTransaction dObjTran)
    {
        mRole mRole = GetRole(dat.intRoleID, dObjContext, dObjTran);

        if (mRole != null)
        {
            dObjContext.mRoleAccesses.RemoveRange(dObjContext.mRoleAccesses.Where(x => x.intRoleID == mRole.intRoleID));
            dObjContext.mRoles.Remove(mRole);
            dObjContext.SaveChanges();
        }

        return true;
    }

    public static List<mRole> GetAllRoles()
    {
        DBEntities dObjContext = null;
        DbContextTransaction dObjTran = null;
        try
        {
            dObjContext = new DBEntities(EFClientUtility.GetConnectionString());
            return GetAllRoles(dObjContext, dObjTran);
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

    public static List<mRole> GetAllRoles(DBEntities dObjContext, DbContextTransaction dObjTran)
    {
        dObjContext.Configuration.ProxyCreationEnabled = false;

        List<mRole> listDat = new List<mRole>();
        listDat = dObjContext.mRoles.ToList();

        return listDat;
    }

    public static List<mRole> GetAllIndex()
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

    public static List<mRole> GetAllIndex(DBEntities dObjContext, DbContextTransaction dObjTran)
    {
        dObjContext.Configuration.ProxyCreationEnabled = false;

        List<mRole> listDat = new List<mRole>();
        listDat = dObjContext.mRoles.ToList();

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
        var retDat = (from Role in dObjContext.mRoles
                      where Role.txtGUID.Equals(txtGUID)
                      select Role).FirstOrDefault();

        if (retDat == null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static List<mMenu> PopulateMenu()
    {
        DBEntities dObjContext = null;
        DbContextTransaction dObjTran = null;
        try
        {
            dObjContext = new DBEntities(EFClientUtility.GetConnectionString());
            return PopulateMenu(dObjContext, dObjTran);
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

    public static List<mMenu> PopulateMenu(DBEntities dObjContext, DbContextTransaction dObjTran)
    {
        dObjContext.Configuration.ProxyCreationEnabled = false;

        List<mMenu> listMenu = new List<mMenu>();
        List<mMenu> parent1 = dObjContext.mMenus.Where(x => x.intParentID == 0).OrderBy(x => x.intOrderID).ToList();     

        foreach(var a in parent1)
        {
            a.bitParent1 = true;
            listMenu.Add(a);
            List<mMenu> parent2 = dObjContext.mMenus.Where(x => x.intParentID == a.intMenuID).OrderBy(x => x.intOrderID).ToList();

            foreach(var b in parent2)
            {
                b.bitParent2 = true;
                listMenu.Add(b);
                List<mMenu> parent3 = dObjContext.mMenus.Where(x => x.intParentID == b.intMenuID).OrderBy(x => x.intOrderID).ToList();

                foreach(var c in parent3) 
                {
                    c.bitParent3 = true;
                    listMenu.Add(c);

                    List<mMenu> parent4 = dObjContext.mMenus.Where(x => x.intParentID == c.intMenuID).OrderBy(x => x.intOrderID).ToList();

                    foreach (var d in parent4) //parent berikutnya mustahil karena gak muat tampilannya
                    {
                        d.bitParent4 = true;
                        listMenu.Add(d);
                    }
                }
            }
        }

        return listMenu;
    }

    public static List<mMenu> PopulateExistedMenu(int intRoleID)
    {
        DBEntities dObjContext = null;
        DbContextTransaction dObjTran = null;
        try
        {
            dObjContext = new DBEntities(EFClientUtility.GetConnectionString());
            return PopulateExistedMenu(intRoleID, dObjContext, dObjTran);
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

    public static List<mMenu> PopulateExistedMenu(int intRoleID, DBEntities dObjContext, DbContextTransaction dObjTran)
    {
        dObjContext.Configuration.ProxyCreationEnabled = false;

        List<mMenu> listMenu = new List<mMenu>();
        List<mMenu> parent1 = dObjContext.mMenus.Where(x => x.intParentID == 0).OrderBy(x => x.intOrderID).ToList();

        foreach (var a in parent1)
        {
            a.bitParent1 = true;
            if(dObjContext.mRoleAccesses.Where(x => x.intRoleID == intRoleID && x.intModuleID == a.intModuleID).FirstOrDefault() != null)
            {
                a.bitChecklist = true;
            }

            listMenu.Add(a);
            List<mMenu> parent2 = dObjContext.mMenus.Where(x => x.intParentID == a.intMenuID).OrderBy(x => x.intOrderID).ToList();

            foreach (var b in parent2)
            {
                b.bitParent2 = true;
                if (dObjContext.mRoleAccesses.Where(x => x.intRoleID == intRoleID && x.intModuleID == b.intModuleID).FirstOrDefault() != null)
                {
                    b.bitChecklist = true;
                }

                listMenu.Add(b);
                List<mMenu> parent3 = dObjContext.mMenus.Where(x => x.intParentID == b.intMenuID).OrderBy(x => x.intOrderID).ToList();

                foreach (var c in parent3)
                {
                    c.bitParent3 = true;
                    if (dObjContext.mRoleAccesses.Where(x => x.intRoleID == intRoleID && x.intModuleID == c.intModuleID).FirstOrDefault() != null)
                    {
                        c.bitChecklist = true;
                    }

                    listMenu.Add(c);

                    List<mMenu> parent4 = dObjContext.mMenus.Where(x => x.intParentID == c.intMenuID).OrderBy(x => x.intOrderID).ToList();

                    foreach (var d in parent4) //parent berikutnya mustahil karena gak muat tampilannya
                    {
                        d.bitParent4 = true;
                        if (dObjContext.mRoleAccesses.Where(x => x.intRoleID == intRoleID && x.intModuleID == d.intModuleID).FirstOrDefault() != null)
                        {
                            d.bitChecklist = true;
                        }
                        listMenu.Add(d);
                    }
                }
            }
        }

        return listMenu;
    }
}