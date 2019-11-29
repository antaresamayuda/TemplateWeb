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

public sealed class mMenuCustomBL
{
    public mMenuCustomBL()
    {
    }

    public static int SaveMenu(mMenu dat, string txtUserName)
    {
        DBEntities dObjContext = null;
        DbContextTransaction dObjTran = null;
        try
        {
            dObjContext = new DBEntities(EFClientUtility.GetConnectionString());
            dObjTran = dObjContext.Database.BeginTransaction();
            SaveMenu(dat, txtUserName, dObjContext, dObjTran);
            dObjTran.Commit();
            return dat.intMenuID;
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

    public static int SaveMenu(mMenu dat, string txtUserName, DBEntities dObjContext, DbContextTransaction dObjTran)
    {
        if (checkGUID(dat.txtGUID, dObjContext, dObjTran))
        {
            if (GetMenuByParentAndName(dat.intParentID.Value, dat.txtMenuName) == null)
            {
                dat.txtCreatedBy = txtUserName;
                dat.dtmCreatedDate = DateTime.Now;
                dObjContext.mMenus.Add(dat);
                dObjContext.SaveChanges();
            }
            else
            {
                throw new Exception("Menu name in the same parent is already exist!");
            }
        }

        return dat.intMenuID;
    }

    public static void UpdateMenu(mMenu dat, string txtUserName)
    {
        DBEntities dObjContext = null;
        DbContextTransaction dObjTran = null;
        try
        {
            dObjContext = new DBEntities(EFClientUtility.GetConnectionString());
            dObjTran = dObjContext.Database.BeginTransaction();
            UpdateMenu(dat, txtUserName, dObjContext, dObjTran);
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

    public static void UpdateMenu(mMenu dat, string txtUserName, DBEntities dObjContext, DbContextTransaction dObjTran)
    {
        if (checkGUID(dat.txtGUID, dObjContext, dObjTran))
        {
            dat.txtUpdatedBy = txtUserName;
            dat.dtmUpdatedDate = DateTime.Now;
            dObjContext.Entry(dat).State = EntityState.Modified;
            dObjContext.SaveChanges();
        }
    }

    public static mMenu GetMenu(int intMenuID)
    {
        DBEntities dObjContext = null;
        DbContextTransaction dObjTran = null;
        try
        {
            dObjContext = new DBEntities(EFClientUtility.GetConnectionString());
            return GetMenu(intMenuID, dObjContext, dObjTran);
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

    public static mMenu GetMenu(int intMenuID, DBEntities dObjContext, DbContextTransaction dObjTran)
    {
        dObjContext.Configuration.ProxyCreationEnabled = false;
        var retDat = (from Menu in dObjContext.mMenus
                      where Menu.intMenuID.Equals(intMenuID)
                      select Menu).FirstOrDefault();
        return retDat;
    }

    public static mMenu GetMenuByParentAndName(int intParentID, string txtMenuName)
    {
        DBEntities dObjContext = null;
        DbContextTransaction dObjTran = null;
        try
        {
            dObjContext = new DBEntities(EFClientUtility.GetConnectionString());
            return GetMenuByParentAndName(intParentID, txtMenuName, dObjContext, dObjTran);
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

    public static mMenu GetMenuByParentAndName(int intParentID, string txtMenuName, DBEntities dObjContext, DbContextTransaction dObjTran)
    {
        dObjContext.Configuration.ProxyCreationEnabled = false;
        var retDat = (from Menu in dObjContext.mMenus
                      where Menu.intParentID == intParentID && Menu.txtMenuName.Equals(txtMenuName)
                      select Menu).FirstOrDefault();

        return retDat;
    }

    public static bool DeleteMenu(mMenu dat, string txtUserName)
    {
        DBEntities dObjContext = null;
        DbContextTransaction dObjTran = null;
        try
        {
            dObjContext = new DBEntities(EFClientUtility.GetConnectionString());
            dObjTran = dObjContext.Database.BeginTransaction();
            DeleteMenu(dat, txtUserName, dObjContext, dObjTran);
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

    public static bool DeleteMenu(mMenu dat, string txtUserName, DBEntities dObjContext, DbContextTransaction dObjTran)
    {
        mMenu mMenu = mMenuCustomBL.GetMenu(dat.intMenuID, dObjContext, dObjTran);

        if (mMenu != null)
        {
            dObjContext.mMenus.Remove(mMenu);
            dObjContext.SaveChanges();
        }

        return true;
    }

    public static List<mMenu> GetAllMenus()
    {
        DBEntities dObjContext = null;
        DbContextTransaction dObjTran = null;
        try
        {
            dObjContext = new DBEntities(EFClientUtility.GetConnectionString());
            return GetAllMenus(dObjContext, dObjTran);
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

    public static List<mMenu> GetAllMenus(DBEntities dObjContext, DbContextTransaction dObjTran)
    {
        dObjContext.Configuration.ProxyCreationEnabled = false;

        List<mMenu> listDat = new List<mMenu>();
        listDat = dObjContext.mMenus.ToList();

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
        var retDat = (from Menu in dObjContext.mMenus
                      where Menu.txtGUID.Equals(txtGUID)
                      select Menu).FirstOrDefault();

        if (retDat == null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static List<mMenu> GetHierarchyOfMenu(int intRoleID)
    {
        DBEntities dObjContext = null;
        DbContextTransaction dObjTran = null;
        try
        {
            dObjContext = new DBEntities(EFClientUtility.GetConnectionString());
            return GetHierarchyOfMenu(intRoleID, dObjContext, dObjTran);
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

    private static List<mMenu> SelectAllWithParent(int intParentId, int intRoleID, DBEntities dObjContext, DbContextTransaction dObjTran)
    {
        dObjContext.Configuration.ProxyCreationEnabled = false;
        return (from Item in dObjContext.mMenus
                join roleAccess in dObjContext.mRoleAccesses on Item.intModuleID equals roleAccess.intModuleID
                where 1 == 1
                && Item.intParentID == intParentId
                && (Item.intModuleID == 0 || roleAccess.bitView == true)
                && roleAccess.intRoleID == intRoleID
                && Item.bitActive == true
                && Item.bitMethod == false
                select Item).ToList();
    }

    public static List<mMenu> GetHierarchyOfMenu(int intRoleID, DBEntities dObjContext, DbContextTransaction dObjTran)
    {
        List<mMenu> List = new List<mMenu>();
        // Get Parent = 0
        List = SelectAllWithParent(0, intRoleID, dObjContext, dObjTran);
        // Recursive for get Child.
        foreach (mMenu dat in List)
        {
            dat.itemList = p_GetHierarchyOfMenu(dat.intMenuID, intRoleID, dObjContext, dObjTran);
        }
        return List;
    }

    public static List<mMenu> p_GetHierarchyOfMenu(int intParentID, int intRoleID, DBEntities dObjContext, DbContextTransaction dObjTran)
    {
        List<mMenu> List = new List<mMenu>();
        // Get Parent = 0
        List = SelectAllWithParent(intParentID, intRoleID, dObjContext, dObjTran);
        // Recursive for get Child.
        foreach (mMenu dat in List)
        {
            dat.itemList = p_GetHierarchyOfMenu(dat.intMenuID, intRoleID, dObjContext, dObjTran);
        }
        return List;
    }

    public static string GenerateMenuHierarchy(int intRoleID, string txtDomain)
    {
        DBEntities dObjContext = null;
        DbContextTransaction dObjTran = null;
        try
        {
            dObjContext = new DBEntities(EFClientUtility.GetConnectionString());
            return GenerateMenuHierarchy(intRoleID, txtDomain, dObjContext, dObjTran);
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

    public static string GenerateMenuHierarchy(int intRoleID, string txtDomain, DBEntities dObjContext, DbContextTransaction dObjTran)
    {
        List<mMenu> List = GetHierarchyOfMenu(intRoleID, dObjContext, dObjTran).OrderBy(x => x.intOrderID).ToList();
        string txtText = string.Empty;

        txtText += "  <ul class='sidebar-menu'>   ";

        foreach (mMenu dat in List)
        {
            List<mMenu> itemList = dat.itemList.OrderBy(x => x.intOrderID).ToList();
            if (itemList.Count == 0)
            {
                // No Child, so just print parent. 
                if (!(dat.intModuleID == 0))
                {
                    txtText += " <li> <a href='" + txtDomain + dat.txtLink.ToString().Replace("~", "") + "'> <i class='glyphicon glyphicon-hand-right'></i><span> " + dat.txtMenuName + "  </span>  </a> </li> ";
                }
            }
            else
            {
                // There childs, so print parent and child.
                txtText += "  <li class='treeview' > " + " <a href='#'> <i class='glyphicon glyphicon-inbox'></i> <span> " + dat.txtMenuName + " </span> <span class='pull-right-container'> <i class='fa fa-angle-left pull-right'> </i> </span> </a> <ul class='treeview-menu'> ";

                p_PrintChild(itemList, ref txtText, txtDomain, dObjContext, dObjTran);

                txtText += " </ul> " + " </li> ";
            }
        }

        txtText += " </ul> ";

        return txtText;
    }


    public static string p_PrintChild(List<mMenu> List, ref string txtText, string txtDomain, DBEntities dObjContext, DbContextTransaction dObjTran)
    {

        foreach (mMenu dat in List)
        {
            List<mMenu> itemList = dat.itemList;
            if (itemList.Count == 0)
            {
                // No Child, so just print parent.
                if (!(dat.intModuleID == 0))
                {
                    txtText += " <li> <a href='" + txtDomain + dat.txtLink.ToString().Replace("~", "") + "'> <i class='glyphicon glyphicon-hand-right'></i><span> " + dat.txtMenuName + "  </span>  </a> </li> ";
                }
            }
            else
            {
                // There childs, so print parent and child. 
                txtText += "  <li class='treeview' > " + " <a href='#'> <i class='glyphicon glyphicon-inbox'></i> <span> " + dat.txtMenuName + " </span> <span class='pull-right-container'> <i class='fa fa-angle-left pull-right'> </i> </span> </a> <ul class='treeview-menu'> ";

                p_PrintChild(itemList, ref txtText, txtDomain, dObjContext, dObjTran);

                txtText += " </ul> " + " </li> ";
            }
        }

        return txtText;
    }
}