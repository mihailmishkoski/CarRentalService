using CarRentalService.Domain.Models;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalServiceAdmin.Controllers
{
    public class CarController : Controller
    {
        public IActionResult Index()
        {
            HttpClient client = new HttpClient();
            
            string URL = "api to the application hosted on azure ";

            HttpResponseMessage response = client.GetAsync(URL).Result;

            var data = response.Content.ReadAsAsync<List<Car>>().Result;
            return View(data);
        }

        [HttpGet]
        public FileContentResult ExportAllCars()
        {
            string fileName = "Cars.xlsx";
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            using (var workbook = new XLWorkbook())
            {
                
                IXLWorksheet worksheet = workbook.Worksheets.Add("Cars");

                
                worksheet.Cell(1, 1).Value = "Car Name";
                worksheet.Cell(1, 2).Value = "Description";
                worksheet.Cell(1, 3).Value = "Model";
                worksheet.Cell(1, 4).Value = "Date Manufactured";
                worksheet.Cell(1, 5).Value = "Kilometers Traveled";
                worksheet.Cell(1, 6).Value = "Color";
                worksheet.Cell(1, 7).Value = "License Plate";

                HttpClient client = new HttpClient();
                
                string URL = "api to the application hosted on azure ";


                HttpResponseMessage response = client.GetAsync(URL).Result;
                var data = response.Content.ReadAsAsync<List<Car>>().Result;  

                
                for (int i = 0; i < data.Count(); i++)
                {
                    var car = data[i];
                    worksheet.Cell(i + 2, 1).Value = car.Name;
                    worksheet.Cell(i + 2, 2).Value = car.Description;
                    worksheet.Cell(i + 2, 3).Value = car.Model;
                    worksheet.Cell(i + 2, 4).Value = car.DateManufactured.ToString("yyyy-MM-dd"); 
                    worksheet.Cell(i + 2, 5).Value = car.KilometersTraveled;
                    worksheet.Cell(i + 2, 6).Value = car.Color.ToString();  
                    worksheet.Cell(i + 2, 7).Value = car.LicensePlate;
                }

                
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, contentType, fileName);
                }
            }
        }

    }
}
