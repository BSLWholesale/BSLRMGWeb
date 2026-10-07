using BSLRMGWEB.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace BSLRMGWEB.Controllers
{
    public class EfficiencyController : Controller
    {
        // GET: Efficiency
        public ActionResult EmployeeWiseEff()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Fn_Get_EmployeeWiseEfficiency(clsEfficiencyReq objReq)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Convert.ToString(ConfigurationManager.AppSettings["BSLRMGAPIURL"]));
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                string DATA = Newtonsoft.Json.JsonConvert.SerializeObject(objReq);

                HttpContent content = new StringContent(DATA, UTF8Encoding.UTF8, "application/json");
                HttpResponseMessage responsePost = client.PostAsync("api/Efficiency/Fn_Get_EmployeeWiseEfficiency", content).Result;
                if (responsePost.IsSuccessStatusCode)
                {
                    return Json(new { success = true, message = responsePost.Content.ReadAsStringAsync().Result }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var errorObj = new[] { new { vErrorMsg = objReq.vErrorMsg, vErrorCode = 400 } };
                    return Json(new { success = false, message = Newtonsoft.Json.JsonConvert.SerializeObject(errorObj) }, JsonRequestBehavior.AllowGet);
                }
            }
        }
    }
}