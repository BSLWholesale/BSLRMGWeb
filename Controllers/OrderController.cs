using BSLRMGWEB.Models;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace BSLRMGWEB.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult CreateOrder()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Fn_Insert_Order_Master(clsOrderMaster objReq)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Convert.ToString(ConfigurationManager.AppSettings["BSLRMGAPIURL"]));
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                string DATA = Newtonsoft.Json.JsonConvert.SerializeObject(objReq);

                HttpContent content = new StringContent(DATA, UTF8Encoding.UTF8, "application/json");
                HttpResponseMessage responsePost = client.PostAsync("api/Order/Fn_Insert_Order_Master", content).Result;
                if (responsePost.IsSuccessStatusCode)
                {
                    return Json(new { success = true, message = responsePost.Content.ReadAsStringAsync().Result }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, message = "Category inserting failed." }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        [HttpPost]
        public JsonResult Fn_Get_Order_Master(clsOrderMaster objReq)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Convert.ToString(ConfigurationManager.AppSettings["BSLRMGAPIURL"]));
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                string DATA = Newtonsoft.Json.JsonConvert.SerializeObject(objReq);

                HttpContent content = new StringContent(DATA, UTF8Encoding.UTF8, "application/json");
                HttpResponseMessage responsePost = client.PostAsync("api/Order/Fn_Get_Order_Master", content).Result;
                if (responsePost.IsSuccessStatusCode)
                {
                    return Json(new { success = true, message = responsePost.Content.ReadAsStringAsync().Result }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, message = "Category inserting failed." }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        [HttpPost]
        public JsonResult Fn_Get_Order_Detail(clsOrderDetail objReq)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Convert.ToString(ConfigurationManager.AppSettings["BSLRMGAPIURL"]));
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                string DATA = Newtonsoft.Json.JsonConvert.SerializeObject(objReq);

                HttpContent content = new StringContent(DATA, UTF8Encoding.UTF8, "application/json");
                HttpResponseMessage responsePost = client.PostAsync("api/Order/Fn_Get_Order_Detail", content).Result;
                if (responsePost.IsSuccessStatusCode)
                {
                    return Json(new { success = true, message = responsePost.Content.ReadAsStringAsync().Result }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, message = "Category inserting failed." }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        [HttpPost]
        public JsonResult Fn_Upload_Operation_BreackdownFile(clsOPBreackDownMaster objReq)
        {
            HttpPostedFileBase file = Request.Files[0];

            if (file == null || file.ContentLength == 0)
            {
                return Json(new { status = "error", message = "Please select Excel file" });
            }

            string fileExtension = Path.GetExtension(file.FileName).ToLower();

            if (fileExtension != ".xls" && fileExtension != ".xlsx")
            {
                return Json(new { success = false, message = "Invalid file format. Only .xls or .xlsx are allowed." }, JsonRequestBehavior.AllowGet);
            }

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            List<clsOPBreackDownDetail> opList = new List<clsOPBreackDownDetail>();

            using (ExcelPackage package = new ExcelPackage(file.InputStream))
            {
                ExcelWorksheet sheet = package.Workbook.Worksheets[0];
                int rowCount = sheet.Dimension.End.Row;

                for (int row = 2; row <= rowCount; row++)
                {
                    

                    clsOPBreackDownDetail emp = new clsOPBreackDownDetail
                    {
                        SeqNo = Convert.ToInt32(sheet.Cells[row, 1].Value),
                        OpNo = Convert.ToInt32(sheet.Cells[row, 2].Value),
                        Descriptions = Convert.ToString(sheet.Cells[row, 3].Text),
                        Machine = Convert.ToString(sheet.Cells[row, 4].Text),
                        SubSection = Convert.ToString(sheet.Cells[row, 5].Text),
                        StdMin = Convert.ToDecimal(sheet.Cells[row, 6].Value),
                        Rate = Convert.ToDecimal(sheet.Cells[row, 7].Value),
                        Product = Convert.ToString(sheet.Cells[row, 8].Text),
                        Skill = Convert.ToString(sheet.Cells[row, 9].Text),
                        Grade = Convert.ToString(sheet.Cells[row, 10].Text),
                        Folder = Convert.ToString(sheet.Cells[row, 11].Text),
                        Seamlength = Convert.ToString(sheet.Cells[row, 12].Text),
                        IsDirect = Convert.ToBoolean(sheet.Cells[row, 13].Value),
                        IsDispatch = false,
                        IsDS = false
                    };

                    opList.Add(emp);
                }
                objReq.oList = opList;
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BSLRMGAPIURL"]);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                string DATA = Newtonsoft.Json.JsonConvert.SerializeObject(objReq);
                HttpContent content = new StringContent(DATA, UTF8Encoding.UTF8, "application/json");

                HttpResponseMessage responsePost = client.PostAsync("api/Order/Fn_Upload_Operation_BreackdownFile", content).Result;
                if (responsePost.IsSuccessStatusCode)
                {
                    return Json(new { success = true, message = responsePost.Content.ReadAsStringAsync().Result }, JsonRequestBehavior.AllowGet);                   
                }
                else
                {
                    return Json(new { success = false, message = "File Importing failed" }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        [HttpPost]
        public JsonResult Fn_Get_Operation_BreackdownFile(clsOPBreackDownMaster objReq)
        {            

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BSLRMGAPIURL"]);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                string DATA = Newtonsoft.Json.JsonConvert.SerializeObject(objReq);
                HttpContent content = new StringContent(DATA, UTF8Encoding.UTF8, "application/json");

                HttpResponseMessage responsePost = client.PostAsync("api/Order/Fn_Get_Operation_BreackdownFile", content).Result;
                if (responsePost.IsSuccessStatusCode)
                {
                    return Json(new { success = true, message = responsePost.Content.ReadAsStringAsync().Result }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, message = "Operation Breackdown getting failed" }, JsonRequestBehavior.AllowGet);
                }
            }
        }
    }
}