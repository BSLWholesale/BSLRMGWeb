using BSLRMGWEB.Models;
using Newtonsoft.Json;
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
            objReq.vErrorMsg = "";
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
                int colCount = sheet.Dimension.End.Column;

                // Create dictionary: ColumnName -> ColumnNumber
                Dictionary<string, int> columnMap = new Dictionary<string, int>();

                // Read header row (Row 1 ONLY)
                for (int col = 1; col <= colCount; col++)
                {
                    string header = sheet.Cells[1, col].Text.Trim();

                    if (!string.IsNullOrEmpty(header) && !columnMap.ContainsKey(header))
                    {
                        columnMap.Add(header, col);
                    }
                }

                // Required columns list
                string[] requiredColumns = {
                 "SeqNo", "OpNo", "Description", "Machine",
                 "SubSection", "StdMin", "Rate", "Product"
                  };

                // Validate required columns
                foreach (var colName in requiredColumns)
                {
                    if (!columnMap.ContainsKey(colName))
                    {
                        objReq.vErrorMsg = $"{colName} column not mapped";
                        objReq.vErrorCode = 400;

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
                    else
                    {
                        objReq.vErrorMsg = "";
                        objReq.vErrorCode = 200;
                    }
                }

                if (objReq.vErrorMsg == "")
                {
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

        [HttpPost]
        public JsonResult Fn_Check_Exist_style_In_Master(clsOPBreackDownMaster objReq)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BSLRMGAPIURL"]);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                string DATA = Newtonsoft.Json.JsonConvert.SerializeObject(objReq);
                HttpContent content = new StringContent(DATA, UTF8Encoding.UTF8, "application/json");

                HttpResponseMessage responsePost = client.PostAsync("api/Order/Fn_Check_Exist_style_In_Master", content).Result;
                if (responsePost.IsSuccessStatusCode)
                {
                    return Json(new { success = true, message = responsePost.Content.ReadAsStringAsync().Result }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, message = "Check existing style getting failed" }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        [HttpPost]
        public JsonResult Fn_Get_OB_BY_Product(clsOPBreackDownDetail objReq)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BSLRMGAPIURL"]);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                string DATA = Newtonsoft.Json.JsonConvert.SerializeObject(objReq);
                HttpContent content = new StringContent(DATA, UTF8Encoding.UTF8, "application/json");

                HttpResponseMessage responsePost = client.PostAsync("api/Order/Fn_Get_OB_BY_Product", content).Result;
                if (responsePost.IsSuccessStatusCode)
                {
                    return Json(new { success = true, message = responsePost.Content.ReadAsStringAsync().Result }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, message = "OB getting failed" }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        [HttpPost]
        public JsonResult Fn_Insert_OB_BY_Product(clsOPBreackDownMaster objReq)
        {

            using (var client = new HttpClient())
            {
                var oListJson = Request.Form["oList"];

                var _oList = JsonConvert.DeserializeObject<List<clsOPBreackDownDetail>>(oListJson);
                objReq.oList = _oList;

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
        public JsonResult Fn_Update_Rate_In_OB_Master(clsOPBreackDownDetail objReq)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BSLRMGAPIURL"]);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                string DATA = Newtonsoft.Json.JsonConvert.SerializeObject(objReq);
                HttpContent content = new StringContent(DATA, UTF8Encoding.UTF8, "application/json");

                HttpResponseMessage responsePost = client.PostAsync("api/Order/Fn_Update_Rate_In_OB_Master", content).Result;
                if (responsePost.IsSuccessStatusCode)
                {
                    return Json(new { success = true, message = responsePost.Content.ReadAsStringAsync().Result }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, message = "Rate updating failed" }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        #region Start Fn_Add_New_OpNo 06-APR-2026

        [HttpPost]
        public JsonResult Fn_Add_New_OpNo(clsOPBreackDownDetail objReq)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BSLRMGAPIURL"]);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                string DATA = Newtonsoft.Json.JsonConvert.SerializeObject(objReq);
                HttpContent content = new StringContent(DATA, UTF8Encoding.UTF8, "application/json");

                HttpResponseMessage responsePost = client.PostAsync("api/Order/Fn_Add_New_OpNo", content).Result;
                if (responsePost.IsSuccessStatusCode)
                {
                    return Json(new { success = true, message = responsePost.Content.ReadAsStringAsync().Result }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, message = "OB getting failed" }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        #endregion End Fn_Add_New_OpNo 06-APR-2026

        #region Start Fn_Get_Order_Chart 13-APR-2026

        [HttpPost]
        public JsonResult Fn_Get_Order_Chart(clsOrderMaster objReq)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BSLRMGAPIURL"]);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                string DATA = Newtonsoft.Json.JsonConvert.SerializeObject(objReq);
                HttpContent content = new StringContent(DATA, UTF8Encoding.UTF8, "application/json");

                HttpResponseMessage responsePost = client.PostAsync("api/Order/Fn_Get_Order_Chart", content).Result;
                if (responsePost.IsSuccessStatusCode)
                {
                    return Json(new { success = true, message = responsePost.Content.ReadAsStringAsync().Result }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, message = "OB getting failed" }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        #endregion End Fn_Get_Order_Chart 13-APR-2026

        #region End Fn_Filter_OP_Detail 10-JUN-2026

        [HttpPost]
        public JsonResult Fn_Filter_OP_Detail(clsOPBreackDownDetail objReq)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BSLRMGAPIURL"]);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                string DATA = Newtonsoft.Json.JsonConvert.SerializeObject(objReq);
                HttpContent content = new StringContent(DATA, UTF8Encoding.UTF8, "application/json");

                HttpResponseMessage responsePost = client.PostAsync("api/Order/Fn_Filter_OP_Detail", content).Result;
                if (responsePost.IsSuccessStatusCode)
                {
                    return Json(new { success = true, message = responsePost.Content.ReadAsStringAsync().Result }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, message = "OB getting failed" }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        #endregion End Fn_Filter_OP_Detail 10-JUN-2026

        #region Start Fn_Append_New_OpNo 11-JUN-2026

        [HttpPost]
        public JsonResult Fn_Append_New_OpNo(clsOPBreackDownDetail objReq)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BSLRMGAPIURL"]);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                string DATA = Newtonsoft.Json.JsonConvert.SerializeObject(objReq);
                HttpContent content = new StringContent(DATA, UTF8Encoding.UTF8, "application/json");

                HttpResponseMessage responsePost = client.PostAsync("api/Order/Fn_Append_New_OpNo", content).Result;
                if (responsePost.IsSuccessStatusCode)
                {
                    return Json(new { success = true, message = responsePost.Content.ReadAsStringAsync().Result }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, message = "Append_New_OpNo failed" }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        #endregion Start Fn_Append_New_OpNo 11-JUN-2026

        #region Start Fn_Delete_OpNo_IN_OBD 15-JUN-2026

        [HttpPost]
        public JsonResult Fn_Delete_OpNo_IN_OBD(clsOPBreackDownDetail objReq)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BSLRMGAPIURL"]);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                string DATA = Newtonsoft.Json.JsonConvert.SerializeObject(objReq);
                HttpContent content = new StringContent(DATA, UTF8Encoding.UTF8, "application/json");

                HttpResponseMessage responsePost = client.PostAsync("api/Order/Fn_Delete_OpNo_IN_OBD", content).Result;
                if (responsePost.IsSuccessStatusCode)
                {
                    return Json(new { success = true, message = responsePost.Content.ReadAsStringAsync().Result }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, message = "OpNo deleting failed" }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        #endregion End Fn_Delete_OpNo_IN_OBD 15-JUN-2026

        #region Start Fn_Add_New_OpNo_IN_OBD 15-JUN-2026

        [HttpPost]
        public JsonResult Fn_Add_New_OpNo_IN_OBD(clsOPBreackDownDetail objReq)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BSLRMGAPIURL"]);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                string DATA = Newtonsoft.Json.JsonConvert.SerializeObject(objReq);
                HttpContent content = new StringContent(DATA, UTF8Encoding.UTF8, "application/json");

                HttpResponseMessage responsePost = client.PostAsync("api/Order/Fn_Add_New_OpNo_IN_OBD", content).Result;
                if (responsePost.IsSuccessStatusCode)
                {
                    return Json(new { success = true, message = responsePost.Content.ReadAsStringAsync().Result }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, message = "OpNo deleting failed" }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        #endregion End Fn_Add_New_OpNo_IN_OBD 15-JUN-2026
    }
}