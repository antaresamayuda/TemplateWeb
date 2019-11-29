using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TemplateWeb.Common;
using AutoMapper;
using TemplateWeb.Common.Entity;
using TemplateWeb.Common.ViewModels;

namespace TemplateWeb.MVC.App_Start
{
    public class AutoMapperConfiguration
    {
        public static void RegisterMaps()
        {

            #region SystemConfiguration

            Mapper.CreateMap<mRole, mRoleVW>().ReverseMap();
            Mapper.CreateMap<mMenu, mMenuVW>().ReverseMap();
            Mapper.CreateMap<mUser, mUserVW>().ReverseMap();
            Mapper.CreateMap<mUserRole, mUserRoleVW>().ReverseMap();

            #endregion

            #region Transaction

            //Mapper.CreateMap<ViewModels.Transaction.TrialFormula.TrialFormulaHeaderViewModels, Common.Transaction.clsTrTrialFormula0>().ReverseMap();
            //Mapper.CreateMap<ViewModels.Transaction.TrialFormula.TrialFormulaDetailViewModels, Common.Transaction.clsTrTrialFormula1>().ReverseMap();

            #endregion

        }
    }
}