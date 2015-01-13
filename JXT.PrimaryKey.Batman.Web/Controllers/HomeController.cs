using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JXT.PrimaryKey.Batman.Core.OAuth;
using JXT.PrimaryKey.Batman.Core.Extentions;
using JXT.PrimaryKey.Batman.Web.Models;
using JXT.PrimaryKey.Batman.Services.InterfaceServices;

namespace JXT.PrimaryKey.Batman.Web.Controllers
{
    using JXT.PrimaryKey.Batman.Core.ManagerConfig;
    using JXT.PrimaryKey.Batman.Model.DataModel;
    using JXT.PrimaryKey.Batman.Web.Comman;
    using JXT.PrimaryKey.Batman.Web.ViewModel;

    public class HomeController : BaseController
    {
        //
        // GET: /Home/

        private RestAuth oauth = null;
        private string _appKey = "XXXXXXX";
        private string _appSceret = "XXXXXXXXX";

        

        private static readonly object obj = new object();

        private static readonly string access_token = "access_token";

        private static readonly string refresh_token = "refresh_token";


        private static readonly string source_from = "aliexpress";

        
        public HomeController()
        {
            if(oauth == null)
            {
                oauth = new AliExpressAuth(_appKey, _appSceret);
            }
          
        }
      




        public ActionResult Index()
        {
            var entity = LoadServices<IUserInformationServices>().GetUserInformationBySourceFrom(source_from);
            if (entity == null)
            {
                Uri requestTokeUri = new Uri("http://gw.api.alibaba.com/auth/authorize.htm");
                string url = oauth.GetAuthorizeUri(requestTokeUri, null, null);
                return Redirect(url);
            }
           
            if (entity.UpdateTime.Value.AddHours(10) > DateTime.Now) //说明access_token还没有过期
            {
                return RedirectToAction("MainIndex", new{accessToken = entity.AccessToken});
            }
            if (entity.WriteDate.AddMonths(6) > DateTime.Now) //说明refreshtoke还没有过期
            {
                //使用refreshtoken交换accesstoken
                Uri changeAccessTokenUri = new Uri("https://gw.api.alibaba.com/openapi/param2/1/system.oauth2/getToken/{0}");
                var changeResult = oauth.ChangeAccessToken(changeAccessTokenUri, entity.RefreshToken);
                //更新数据库的accesstoken字段和updatetime字段
                var result = changeResult.ToJsonObject<ChangeTokenResult>();
                entity.UpdateTime = DateTime.Now;
                entity.AccessToken = result.Access_Token;
                LoadServices<IUserInformationServices>().UpdateUserInformation(entity);
                return RedirectToAction("MainIndex", new { accessToken = entity.AccessToken }); 
            }

            return RedirectToAction("CallBack");



        }

        public ActionResult CallBack(string code)
        {
            string result = oauth.GetRefreshTokenAndAccessToken(code);
            var authResult = result.ToJsonObject<AliAuthResult>();
            //更新数据库
            var entity = LoadServices<IUserInformationServices>().GetUserInformationBySourceFrom(source_from);
            if (entity == null)
            {
                var userItem = new UserInformation()
                {
                    AccessToken = authResult.Access_Token,
                    RefreshToken = authResult.Refresh_Token,
                    SourceFrom = source_from,
                    WriteDate = DateTime.Now,
                    UpdateTime = DateTime.Now,

                   
                };
                LoadServices<IUserInformationServices>().AddUserInformation(userItem);
                return RedirectToAction("MainIndex","Home", new{ accessToken = userItem.AccessToken});
            }
            entity.AccessToken = authResult.Access_Token;
            entity.RefreshToken = authResult.Refresh_Token;
            entity.WriteDate = DateTime.Now;
            entity.UpdateTime = DateTime.Now;
            
            LoadServices<IUserInformationServices>().UpdateUserInformation(entity);
            return RedirectToAction("MainIndex", new{accessToken = entity.AccessToken});

           
        }


        public ActionResult MainIndex(string accessToken)
        {
            ViewMainIndexModel viewModel = new ViewMainIndexModel();
            var menuModel = GetViewMenuModel(accessToken);
            viewModel.MenuModel = menuModel;
            ViewData.Model = viewModel;
            return View();
        }

        public ActionResult GetOrderBuyerInfo(string accessToken)
        {
            var menuModel = GetViewMenuModel(accessToken);
            Dictionary<string, string> dic = new Dictionary<string, string>();
            //dic.Add("orderId", "62536471964895");
            dic.Add("access_token", accessToken);
            dic.Add("page", "1");
            dic.Add("pageSize", "10");
            var resultData = oauth.GetDataFromApi("api.findOrderListQuery", true, dic,true);
            ViewOrderBuyerInfoModel viewModel = new ViewOrderBuyerInfoModel();
            viewModel.MenuModel = menuModel;
            viewModel.Result = resultData.Item2;
            ViewData.Model = viewModel;
            return View();
        }

    }
}
