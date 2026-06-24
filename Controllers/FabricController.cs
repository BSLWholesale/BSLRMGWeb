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
    public class FabricController : Controller
    {
        // GET: Fabric
        public ActionResult FabricInhouse()
        {
            return View();
        }

        public ActionResult ViewFabricRoll()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Fn_Upload_Fabirc_Inhouse(FabricInhouse objReq)
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

            List<FabricInhouseList> opList = new List<FabricInhouseList>();

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
                 "RollNo", "Quantity", "Unit", "Width(CM)",
                 "Shade", "GSM", "Shrinkage"
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

                            HttpResponseMessage responsePost = client.PostAsync("api/Fabric/Fn_Upload_Fabirc_Inhouse", content).Result;
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
                        FabricInhouseList ob = new FabricInhouseList
                        {
                            RollNo = Convert.ToDecimal(sheet.Cells[row, 1].Value),
                            Quantity = Convert.ToDecimal(sheet.Cells[row, 2].Value),
                            Unit = Convert.ToString(sheet.Cells[row, 3].Text),
                            Width = Convert.ToDecimal(sheet.Cells[row, 4].Text),
                            ShadeName = Convert.ToString(sheet.Cells[row, 5].Text),
                            GSM = Convert.ToDecimal(sheet.Cells[row, 6].Value),
                            Shrinkage = Convert.ToDecimal(sheet.Cells[row, 7].Value),
                            
                        };
                        opList.Add(ob);
                    }
                    objReq._oInhouseList = opList;
                }
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BSLRMGAPIURL"]);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                string DATA = Newtonsoft.Json.JsonConvert.SerializeObject(objReq);
                HttpContent content = new StringContent(DATA, UTF8Encoding.UTF8, "application/json");

                HttpResponseMessage responsePost = client.PostAsync("api/Fabric/Fn_Upload_Fabirc_Inhouse", content).Result;
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
        public JsonResult Fn_Get_Fabric_Roll(FabricInhouse objReq)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Convert.ToString(ConfigurationManager.AppSettings["BSLRMGAPIURL"]));
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                string DATA = Newtonsoft.Json.JsonConvert.SerializeObject(objReq);

                HttpContent content = new StringContent(DATA, UTF8Encoding.UTF8, "application/json");
                HttpResponseMessage responsePost = client.PostAsync("api/Fabric/Fn_Get_Fabric_Roll", content).Result;
                if (responsePost.IsSuccessStatusCode)
                {
                    return Json(new { success = true, message = responsePost.Content.ReadAsStringAsync().Result }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, message = "Fabric roll getting failed." }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        public JsonResult Fn_Get_Fabric_Order(clsFabricOrder objReq)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Convert.ToString(ConfigurationManager.AppSettings["BSLRMGAPIURL"]));
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                string DATA = Newtonsoft.Json.JsonConvert.SerializeObject(objReq);

                HttpContent content = new StringContent(DATA, UTF8Encoding.UTF8, "application/json");
                HttpResponseMessage responsePost = client.PostAsync("api/Fabric/Fn_Get_Fabric_Order", content).Result;
                if (responsePost.IsSuccessStatusCode)
                {
                    return Json(new { success = true, message = responsePost.Content.ReadAsStringAsync().Result }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, message = "Fabric roll getting failed." }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        [HttpPost]
        public JsonResult Fn_Update_Fabric_RollNo(FabricInhouseList objReq)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Convert.ToString(ConfigurationManager.AppSettings["BSLRMGAPIURL"]));
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                string DATA = Newtonsoft.Json.JsonConvert.SerializeObject(objReq);

                HttpContent content = new StringContent(DATA, UTF8Encoding.UTF8, "application/json");
                HttpResponseMessage responsePost = client.PostAsync("api/Fabric/Fn_Update_Fabric_RollNo", content).Result;
                if (responsePost.IsSuccessStatusCode)
                {
                    return Json(new { success = true, message = responsePost.Content.ReadAsStringAsync().Result }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, message = "Fabric Roll Updating failed." }, JsonRequestBehavior.AllowGet);
                }
            }
        }
    }
}