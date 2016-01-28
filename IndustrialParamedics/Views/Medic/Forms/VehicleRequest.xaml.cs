using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Syncfusion.XlsIO;
using Xamarin.Forms;

namespace IndustrialParamedics
{

	public class VehicleForm {

		public string Job { get; set; }
		public string Customer { get; set; }
		public string Name { get; set; }
		public string Date { get; set; }
		public string Phone { get; set; }
		public string Note { get; set; }
		public string VehicleType { get; set; }
		public string FuelType { get; set; }
		public string MTC { get; set; }
		public string Unit { get; set; }
		public string IncidentNum { get; set; }


	}

	public partial class VehicleRequest : ContentPage
	{
		public VehicleForm vehicleForm;

		public VehicleRequest ()
		{
			InitializeComponent ();

			App.Parse.query ("Client", delegate (IDictionary<string,string> results) {
				foreach (var client in results) 
				{
					customerId.Items.Add(client.Key);
				}
				if(customerId.Items.Count > 0) {
					customerId.SelectedIndex = 0;
				}
			});

			fuelType.SelectedIndex = 0;
			vehicleType.SelectedIndex = 0;
		}

		private void initializeValues ()
		{
			this.vehicleForm = new VehicleForm();
			vehicleForm.Job = Job.Text;
			vehicleForm.Customer = customerId.Items[customerId.SelectedIndex].ToString();
			vehicleForm.Date = DateTime.Now.ToString();
			vehicleForm.Name = App.currentUser.userName;
			vehicleForm.Note = note.Text;

			vehicleForm.FuelType = fuelType.Items[fuelType.SelectedIndex].ToString();
			vehicleForm.VehicleType = vehicleType.Items[vehicleType.SelectedIndex].ToString();
			vehicleForm.Unit = unit.Text;
			vehicleForm.MTC = MTC.Text;
			vehicleForm.IncidentNum = incident.Text;

		}

		async void OnSubmit (object sender, EventArgs e)
		{
			// Setup all excel variables
			initializeValues();
			generateExcelFromTemplate ();
			//Go back
			await this.Navigation.PopAsync();
		}

		private void sendEmail (string excelFileURL)
		{
			App.Parse.sendEmail(App.Parse.getVehicleRequestEmail(), excelFileURL);
		}

		public void generateExcelFromTemplate ()
		{
			Assembly executingAssembly = typeof(App).GetTypeInfo ().Assembly;
			Stream inputStream = null;

			inputStream = executingAssembly.GetManifestResourceStream ("IndustrialParamedics.Equipment_Template.xlsx");

			IWorkbook book = null;
			ExcelEngine excelEngine = new ExcelEngine ();

			book = excelEngine.Excel.Workbooks.Open (inputStream);
			inputStream.Dispose ();


			IWorksheet sheet = book.Worksheets [0];

			//Create Template Marker Processor
			ITemplateMarkersProcessor marker = book.CreateTemplateMarkersProcessor();
			marker.AddVariable("VehicleForm", this.vehicleForm);

			//Applies the marker.
			marker.ApplyMarkers(UnknownVariableAction.Skip);

			Stream data = new MemoryStream ();
			book.SaveAs(data);
			book.Close ();
			data.Seek(0, SeekOrigin.Begin);

			App.Parse.saveFile ("VehicleRequest.xlsx", data, this.sendEmail);

		}
	}
}

