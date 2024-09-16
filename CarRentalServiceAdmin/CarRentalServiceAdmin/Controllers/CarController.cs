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
            string URL = "https://localhost:7144/api/API/GetAllCars";

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
                // Create a worksheet
                IXLWorksheet worksheet = workbook.Worksheets.Add("Cars");

                // Add header row
                worksheet.Cell(1, 1).Value = "Car Name";
                worksheet.Cell(1, 2).Value = "Description";
                worksheet.Cell(1, 3).Value = "Model";
                worksheet.Cell(1, 4).Value = "Date Manufactured";
                worksheet.Cell(1, 5).Value = "Kilometers Traveled";
                worksheet.Cell(1, 6).Value = "Color";
                worksheet.Cell(1, 7).Value = "License Plate";

                // Fetch the list of cars (you can replace the URL with your actual API endpoint)
                HttpClient client = new HttpClient();
                string URL = "https://localhost:7144/api/API/GetAllCars";  // Change this to your actual API

                HttpResponseMessage response = client.GetAsync(URL).Result;
                var data = response.Content.ReadAsAsync<List<Car>>().Result;  // Assuming the API returns a List<Car>

                // Add data rows
                for (int i = 0; i < data.Count(); i++)
                {
                    var car = data[i];
                    worksheet.Cell(i + 2, 1).Value = car.Name;
                    worksheet.Cell(i + 2, 2).Value = car.Description;
                    worksheet.Cell(i + 2, 3).Value = car.Model;
                    worksheet.Cell(i + 2, 4).Value = car.DateManufactured.ToString("yyyy-MM-dd");  // Format date
                    worksheet.Cell(i + 2, 5).Value = car.KilometersTraveled;
                    worksheet.Cell(i + 2, 6).Value = car.Color.ToString();  // Assuming Color is an enum or class with ToString()
                    worksheet.Cell(i + 2, 7).Value = car.LicensePlate;
                }

                // Return the Excel file
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
