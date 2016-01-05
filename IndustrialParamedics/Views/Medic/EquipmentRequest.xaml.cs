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

	public class EquipmentForm {

		public string Job { get; set; }
		public string Customer { get; set; }
		public string Name { get; set; }
		public string Date { get; set; }
		public string Phone { get; set; }
		public string Title { get; set; }
		public string Note { get; set; }

	}

	public partial class EquipmentRequest : ContentPage
	{
		public EquipmentForm equipmentForm;

		public EquipmentRequest ()
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
		}

		private void initializeValues ()
		{
			this.equipmentForm = new EquipmentForm();
			equipmentForm.Job = Job.Text;
			equipmentForm.Customer = customerId.Items[customerId.SelectedIndex].ToString();
			equipmentForm.Date = DateTime.Now.ToString();
			equipmentForm.Name = App.currentUser.userName;
			equipmentForm.Title = title.Text;
			equipmentForm.Note = note.Text;
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
			App.Parse.sendEmail("erikj54+operations@gmail.com", excelFileURL);
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
			marker.AddVariable("EquipmentForm", this.equipmentForm);

			//Applies the marker.
			marker.ApplyMarkers(UnknownVariableAction.Skip);

			Stream data = new MemoryStream ();
			book.SaveAs(data);
			book.Close ();
			data.Seek(0, SeekOrigin.Begin);

			App.Parse.saveFile ("EquipmentRequest.xlsx", data, this.sendEmail);

		}
	}
}

