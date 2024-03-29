using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Newtonsoft.Json;
using SarayaAdmin.Entity.Model;

namespace SarayaAdmin.WebAdmin.Controllers {
    [Route ("[controller]")]
    public class SharedController : Controller {
        private static IHtmlLocalizer<HomeController> _localizer;
        public SharedController (IHtmlLocalizer<HomeController> localizer) {
            _localizer = localizer;
        }

        [Route ("localization")]
        public IActionResult Localization () {
            Response.ContentType = "application/javascript";

            var langCookie = HttpContext.Request.Cookies["lang"];
            if (langCookie == null) {
                langCookie = "id-ID";
            }

            CultureInfo.CurrentCulture = new CultureInfo (langCookie, true);
            CultureInfo.CurrentUICulture = new CultureInfo (langCookie, true);

            var localization = _localizer.WithCulture (new CultureInfo (langCookie, true)).GetAllStrings ();
            string lang = "Vue.prototype.$lang";

            StringBuilder result = new StringBuilder ();
            result.AppendFormat ("{0} = new Object();", lang);

            foreach (var item in localization) {
                string val = System.Web.HttpUtility.JavaScriptStringEncode (item.Value);
                result.AppendFormat ($"{lang}.{item.Name.Replace("-","")}='{val}';");
            }

            return Content (result.ToString ());
        }

        [Route ("menu")]
        public IActionResult Menu () {
            Response.ContentType = "application/javascript";
            string menu = "Vue.prototype.$menu";

            StringBuilder result = new StringBuilder ();
            result.Append ($"{menu} = new Array();");
            result.Append ("var menu = new Object();");
            result.Append ("var child = new Object();");

            if (HttpContext.User.Identity.IsAuthenticated) {
                // get information user from session and passing to vuejs
                var user = JsonConvert.DeserializeObject<Credentials> (HttpContext.User.Claims.Where (c => c.Type.Equals (ClaimTypes.UserData)).FirstOrDefault ().Value);
                var menuLists = user.RoleMap.FirstOrDefault ().Role.MenuRoleMap.Where (m => !m.Menu.Parent.HasValue).ToList ();
                foreach (var item in menuLists) {
                    StringBuilder menuObj = new StringBuilder ();
                    menuObj.Append ("menu = new Object();");
                    menuObj.Append ($"menu.title = '{item.Menu.Title}';");
                    menuObj.Append ($"menu.id = '{item.Menu.Id}';");
                    menuObj.Append ($"menu.url = '{item.Menu.Url}';");
                    menuObj.Append ($"menu.child = new Array();");
                    foreach (var child in item.Menu.InverseParentNavigation) {
                        menuObj.Append ("child = new Object();");
                        menuObj.Append ($"child.title = '{child.Title}';");
                        menuObj.Append ($"child.id = '{child.Id}';");
                        menuObj.Append ($"child.url = '{child.Url}';");
                        menuObj.Append ($"menu.child.push(child);");
                    }
                    result.Append (menuObj.ToString ());
                    result.Append ($"{menu}.push(menu);");
                }
            } else {
                result = result.Clear ();
                result.Append ($"{menu} = new Array();");
            }

            return Content (result.ToString ());
        }
    }
}