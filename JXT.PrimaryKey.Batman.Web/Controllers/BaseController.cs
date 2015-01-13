using System.Web.Mvc;

namespace JXT.PrimaryKey.Batman.Web.Controllers
{
    using JXT.PrimaryKey.Batman.Core.Container;
    using JXT.PrimaryKey.Batman.Web.Comman;
    using JXT.PrimaryKey.Batman.Web.ViewModel;

    public class BaseController : Controller
    {
       
        public TServices LoadServices<TServices>()
        {
            return ContainerManager.Service.Resolve<TServices>();
        }

        /// <summary>
        /// 获取菜单信息的model
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        protected virtual ViewMenuModel GetViewMenuModel(string accessToken)
        {
            var webMenu = MenuInformation.GetWebMenuElement();
            ViewMenuModel menuModel = new ViewMenuModel();
            menuModel.MenuInformation = webMenu;
            menuModel.AccessToken = accessToken;
            return menuModel;
        }

  
    }
}
