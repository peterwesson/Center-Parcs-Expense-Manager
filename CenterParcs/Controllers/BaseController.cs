using System.Web.Mvc;

using Newtonsoft.Json;

namespace CenterParcs.Controllers
{
    public abstract class BaseController : Controller
    {
        protected ActionResult JSONCircular(object obj)
        {
            var json = JsonConvert.SerializeObject(
                obj,
                Formatting.Indented,
                new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects });

            return this.Content(json, "application/json");
        }
    }
}