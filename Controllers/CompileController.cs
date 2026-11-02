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
    public class CompileController : Controller
    {
        // GET: Compile
        public ActionResult FinishingCompile()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Fn_Insert_Bundle_Size(clsBundleSize objReq)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Convert.ToString(ConfigurationManager.AppSettings["BSLRMGAPIURL"]));
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                string DATA = Newtonsoft.Json.JsonConvert.SerializeObject(objReq);

                HttpContent content = new StringContent(DATA, UTF8Encoding.UTF8, "application/json");
                HttpResponseMessage responsePost = client.PostAsync("api/Compile/Fn_Insert_Bundle_Size", content).Result;
                if (responsePost.IsSuccessStatusCode)
                {
                    return Json(new { success = true, message = responsePost.Content.ReadAsStringAsync().Result }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, message = "Bundle Size Inserting failed." }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        [HttpPost]
        public JsonResult Fn_Insert_Bundle_Color(clsBundleColor objReq)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Convert.ToString(ConfigurationManager.AppSettings["BSLRMGAPIURL"]));
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                string DATA = Newtonsoft.Json.JsonConvert.SerializeObject(objReq);

                HttpContent content = new StringContent(DATA, UTF8Encoding.UTF8, "application/json");
                HttpResponseMessage responsePost = client.PostAsync("api/Compile/Fn_Insert_Bundle_Color", content).Result;
                if (responsePost.IsSuccessStatusCode)
                {
                    return Json(new { success = true, message = responsePost.Content.ReadAsStringAsync().Result }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, message = "Color Inserting failed." }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        [HttpPost]
        public JsonResult Fn_Insert_Bundle_Layer(clsBundleLayerMaster objReq)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Convert.ToString(ConfigurationManager.AppSettings["BSLRMGAPIURL"]));
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                string DATA = Newtonsoft.Json.JsonConvert.SerializeObject(objReq);

                HttpContent content = new StringContent(DATA, UTF8Encoding.UTF8, "application/json");
                HttpResponseMessage responsePost = client.PostAsync("api/Compile/Fn_Insert_Bundle_Layer", content).Result;
                if (responsePost.IsSuccessStatusCode)
                {
                    return Json(new { success = true, message = responsePost.Content.ReadAsStringAsync().Result }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, message = "Bundle Layer Inserting failed." }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        [HttpPost]
        public JsonResult Fn_Delete_Bundle_Layer(clsBundleLayerMaster objReq)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Convert.ToString(ConfigurationManager.AppSettings["BSLRMGAPIURL"]));
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                string DATA = Newtonsoft.Json.JsonConvert.SerializeObject(objReq);

                HttpContent content = new StringContent(DATA, UTF8Encoding.UTF8, "application/json");
                HttpResponseMessage responsePost = client.PostAsync("api/Compile/Fn_Delete_Bundle_Layer", content).Result;
                if (responsePost.IsSuccessStatusCode)
                {
                    return Json(new { success = true, message = responsePost.Content.ReadAsStringAsync().Result }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, message = "Bundle Layer Deleting failed." }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        [HttpPost]
        public JsonResult Fn_Get_Bundle_Layer(clsBundleLayerMaster objReq)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Convert.ToString(ConfigurationManager.AppSettings["BSLRMGAPIURL"]));
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                string DATA = Newtonsoft.Json.JsonConvert.SerializeObject(objReq);

                HttpContent content = new StringContent(DATA, UTF8Encoding.UTF8, "application/json");
                HttpResponseMessage responsePost = client.PostAsync("api/Compile/Fn_Get_Bundle_Layer", content).Result;
                if (responsePost.IsSuccessStatusCode)
                {
                    return Json(new { success = true, message = responsePost.Content.ReadAsStringAsync().Result }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, message = "Bundle Layer Getting failed." }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        [HttpPost]
        public JsonResult Fn_Delete_Bundle_Size(clsBundleSize objReq)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Convert.ToString(ConfigurationManager.AppSettings["BSLRMGAPIURL"]));
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                string DATA = Newtonsoft.Json.JsonConvert.SerializeObject(objReq);

                HttpContent content = new StringContent(DATA, UTF8Encoding.UTF8, "application/json");
                HttpResponseMessage responsePost = client.PostAsync("api/Compile/Fn_Delete_Bundle_Size", content).Result;
                if (responsePost.IsSuccessStatusCode)
                {
                    return Json(new { success = true, message = responsePost.Content.ReadAsStringAsync().Result }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, message = "Bundle Size Deleting failed." }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        [HttpPost]
        public JsonResult Fn_Get_Bundle_Size(clsBundleSize objReq)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Convert.ToString(ConfigurationManager.AppSettings["BSLRMGAPIURL"]));
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                string DATA = Newtonsoft.Json.JsonConvert.SerializeObject(objReq);

                HttpContent content = new StringContent(DATA, UTF8Encoding.UTF8, "application/json");
                HttpResponseMessage responsePost = client.PostAsync("api/Compile/Fn_Get_Bundle_Size", content).Result;
                if (responsePost.IsSuccessStatusCode)
                {
                    return Json(new { success = true, message = responsePost.Content.ReadAsStringAsync().Result }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, message = "Bundle size getting failed." }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        [HttpPost]
        public JsonResult Fn_Get_Bundle_Color(clsBundleColor objReq)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Convert.ToString(ConfigurationManager.AppSettings["BSLRMGAPIURL"]));
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                string DATA = Newtonsoft.Json.JsonConvert.SerializeObject(objReq);

                HttpContent content = new StringContent(DATA, UTF8Encoding.UTF8, "application/json");
                HttpResponseMessage responsePost = client.PostAsync("api/Compile/Fn_Get_Bundle_Color", content).Result;
                if (responsePost.IsSuccessStatusCode)
                {
                    return Json(new { success = true, message = responsePost.Content.ReadAsStringAsync().Result }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, message = "Color Fetching failed." }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        [HttpPost]
        public JsonResult Fn_Delete_Bundle_Color(clsBundleColor objReq)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Convert.ToString(ConfigurationManager.AppSettings["BSLRMGAPIURL"]));
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                string DATA = Newtonsoft.Json.JsonConvert.SerializeObject(objReq);

                HttpContent content = new StringContent(DATA, UTF8Encoding.UTF8, "application/json");
                HttpResponseMessage responsePost = client.PostAsync("api/Compile/Fn_Delete_Bundle_Color", content).Result;
                if (responsePost.IsSuccessStatusCode)
                {
                    return Json(new { success = true, message = responsePost.Content.ReadAsStringAsync().Result }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, message = "Color deleteing failed." }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        [HttpPost]
        public JsonResult Fn_Insert_Bundle_Shade(clsBundleShade objReq)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Convert.ToString(ConfigurationManager.AppSettings["BSLRMGAPIURL"]));
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                string DATA = Newtonsoft.Json.JsonConvert.SerializeObject(objReq);

                HttpContent content = new StringContent(DATA, UTF8Encoding.UTF8, "application/json");
                HttpResponseMessage responsePost = client.PostAsync("api/Compile/Fn_Insert_Bundle_Shade", content).Result;
                if (responsePost.IsSuccessStatusCode)
                {
                    return Json(new { success = true, message = responsePost.Content.ReadAsStringAsync().Result }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, message = "Shade Inserting failed." }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        [HttpPost]
        public JsonResult Fn_Get_Bundle_Shade(clsBundleShade objReq)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Convert.ToString(ConfigurationManager.AppSettings["BSLRMGAPIURL"]));
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                string DATA = Newtonsoft.Json.JsonConvert.SerializeObject(objReq);

                HttpContent content = new StringContent(DATA, UTF8Encoding.UTF8, "application/json");
                HttpResponseMessage responsePost = client.PostAsync("api/Compile/Fn_Get_Bundle_Shade", content).Result;
                if (responsePost.IsSuccessStatusCode)
                {
                    return Json(new { success = true, message = responsePost.Content.ReadAsStringAsync().Result }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, message = "Shade Fetching failed." }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        [HttpPost]
        public JsonResult Fn_Delete_Bundle_Shade(clsBundleShade objReq)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Convert.ToString(ConfigurationManager.AppSettings["BSLRMGAPIURL"]));
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                string DATA = Newtonsoft.Json.JsonConvert.SerializeObject(objReq);

                HttpContent content = new StringContent(DATA, UTF8Encoding.UTF8, "application/json");
                HttpResponseMessage responsePost = client.PostAsync("api/Compile/Fn_Delete_Bundle_Shade", content).Result;
                if (responsePost.IsSuccessStatusCode)
                {
                    return Json(new { success = true, message = responsePost.Content.ReadAsStringAsync().Result }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, message = "Shade deleting failed." }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        [HttpPost]
        public JsonResult Fn_Insert_Bundle_Compile(clsBundleCompile objReq)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Convert.ToString(ConfigurationManager.AppSettings["BSLRMGAPIURL"]));
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                string DATA = Newtonsoft.Json.JsonConvert.SerializeObject(objReq);

                HttpContent content = new StringContent(DATA, UTF8Encoding.UTF8, "application/json");
                HttpResponseMessage responsePost = client.PostAsync("api/Compile/Fn_Insert_Bundle_Compile", content).Result;
                if (responsePost.IsSuccessStatusCode)
                {
                    return Json(new { success = true, message = responsePost.Content.ReadAsStringAsync().Result }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, message = "Shade Fetching failed." }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        [HttpPost]
        public JsonResult Fn_Get_Bundle_Compile(clsBundleCompile objReq)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Convert.ToString(ConfigurationManager.AppSettings["BSLRMGAPIURL"]));
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                string DATA = Newtonsoft.Json.JsonConvert.SerializeObject(objReq);

                HttpContent content = new StringContent(DATA, UTF8Encoding.UTF8, "application/json");
                HttpResponseMessage responsePost = client.PostAsync("api/Compile/Fn_Get_Bundle_Compile", content).Result;
                if (responsePost.IsSuccessStatusCode)
                {
                    return Json(new { success = true, message = responsePost.Content.ReadAsStringAsync().Result }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, message = "Shade Fetching failed." }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        [HttpPost]
        public JsonResult Fn_Get_SectionWis_Compile_QR_Data(clsBundleCompile objReq)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Convert.ToString(ConfigurationManager.AppSettings["BSLRMGAPIURL"]));
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                string DATA = Newtonsoft.Json.JsonConvert.SerializeObject(objReq);

                HttpContent content = new StringContent(DATA, UTF8Encoding.UTF8, "application/json");
                HttpResponseMessage responsePost = client.PostAsync("api/Compile/Fn_Get_SectionWis_Compile_QR_Data", content).Result;
                if (responsePost.IsSuccessStatusCode)
                {
                    return Json(new { success = true, message = responsePost.Content.ReadAsStringAsync().Result }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, message = "Shade Fetching failed." }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        [HttpPost]
        public JsonResult Fn_Get_Subsection_List(clsBundleCompile objReq)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Convert.ToString(ConfigurationManager.AppSettings["BSLRMGAPIURL"]));
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                string DATA = Newtonsoft.Json.JsonConvert.SerializeObject(objReq);

                HttpContent content = new StringContent(DATA, UTF8Encoding.UTF8, "application/json");
                HttpResponseMessage responsePost = client.PostAsync("api/Compile/Fn_Get_Subsection_List", content).Result;
                if (responsePost.IsSuccessStatusCode)
                {
                    return Json(new { success = true, message = responsePost.Content.ReadAsStringAsync().Result }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, message = "Updating AppEmpId failed." }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        [HttpPost]
        public JsonResult Fn_Get_Sum_Laywise_Plies(clsBundleShade objReq)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Convert.ToString(ConfigurationManager.AppSettings["BSLRMGAPIURL"]));
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                string DATA = Newtonsoft.Json.JsonConvert.SerializeObject(objReq);

                HttpContent content = new StringContent(DATA, UTF8Encoding.UTF8, "application/json");
                HttpResponseMessage responsePost = client.PostAsync("api/Compile/Fn_Get_Sum_Laywise_Plies", content).Result;
                if (responsePost.IsSuccessStatusCode)
                {
                    return Json(new { success = true, message = responsePost.Content.ReadAsStringAsync().Result }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, message = "Shade Fetching failed." }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        [HttpPost]
        public JsonResult Fn_Update_Print_layarDate(clsBundleLayerMaster objReq)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Convert.ToString(ConfigurationManager.AppSettings["BSLRMGAPIURL"]));
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                string DATA = Newtonsoft.Json.JsonConvert.SerializeObject(objReq);

                HttpContent content = new StringContent(DATA, UTF8Encoding.UTF8, "application/json");
                HttpResponseMessage responsePost = client.PostAsync("api/Compile/Fn_Update_Print_layarDate", content).Result;
                if (responsePost.IsSuccessStatusCode)
                {
                    return Json(new { success = true, message = responsePost.Content.ReadAsStringAsync().Result }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, message = "Shade Fetching failed." }, JsonRequestBehavior.AllowGet);
                }
            }
        }
    }
}