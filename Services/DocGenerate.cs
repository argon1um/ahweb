using ah4cClientApp.DTO;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;

namespace ah4cClientApp.Services
{
	public class DocGenerate : Controller
	{
		public IActionResult CreateDoc(OrderAddDTO order)
		{
			string filepath = $"C:/Users/ItzArg/Documents/paydoc_{order.orderId}.xlsx";
			using (var workbook = new XLWorkbook(filepath))
			{
				foreach (var worksheet in workbook.Worksheets)
				{
					foreach (var cell in worksheet.CellsUsed())
					{
						if (cell.Value.ToString().Contains("CLIENTNAME"))
						{
							cell.Value = cell.Value.ToString().Replace("CLIENTNAME", order.clientName);
						}
						else if (cell.Value.ToString().Contains("ORDERID"))
						{
							cell.Value.ToString().Replace("ORDERID", order.orderId.ToString());
						}
						else if (cell.Value.ToString().Contains("CLIENTPHONE"))
						{
							cell.Value.ToString().Replace("CLIENTPHONE", order.clientPhone.ToString());
						}
						else if (cell.Value.ToString().Contains("ADMDATE"))
						{
							cell.Value.ToString().Replace("ADMDATE", order.admDate.ToString());
						}
						else if (cell.Value.ToString().Contains("ISSUEDATE"))
						{
							cell.Value.ToString().Replace("ISSUEDATE", order.issueDate.ToString());
						}
						else if (cell.Value.ToString().Contains("CLIENTMAIL"))
						{
							cell.Value.ToString().Replace("CLIENTMAIL", "");
						}
						else if (cell.Value.ToString().Contains("ANIMALNAME"))
						{
							cell.Value.ToString().Replace("ANIMALNAME", order.animalName);
						}
						else if (cell.Value.ToString().Contains("ANIMALGEN"))
						{
							cell.Value.ToString().Replace("ANIMALGEN", order.animalGen);
						}
						else if (cell.Value.ToString().Contains("ANIMALTYPE"))
						{
							cell.Value.ToString().Replace("ANIMALTYPE", order.animalType);
						}
						else if (cell.Value.ToString().Contains("ANIMALBREED"))
						{
							cell.Value.ToString().Replace("ANIMALBREED", order.animalBreed);
						}
						else if (cell.Value.ToString().Contains("TOTALDAYS"))
						{
							cell.Value.ToString().Replace("TOTALDAYS", (order.issueDate.Day - order.admDate.Day).ToString());
						}
						else if (cell.Value.ToString().Contains("TOTALPRICE"))
						{
							cell.Value.ToString().Replace("TOTALPRICE", order.Totalprice.ToString());
						}
					}
				}
				workbook.SaveAs($"C:/Users/ItzArg/Documents/paydoc_{order.orderId}.xlsx");
			}
			return File(System.IO.File.ReadAllBytes($"C:/Users/ItzArg/Documents/paydoc_{order.orderId}.xlsx"), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ModifiedFile.xlsx");
		}
	}
}
