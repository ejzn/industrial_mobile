using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Syncfusion.XlsIO;
using Xamarin.Forms;
using System.Collections.ObjectModel;

namespace IndustrialParamedics
{
	public class OrderLine {
		public string qty { get; set; }
		public string item { get; set; }
		public string pcr { get; set; }
		public string order { get; set; }
		public string reasonCode { get; set; }
	}

	public class OrderForm {

		public string Job { get; set; }
		public string Customer { get; set; }
		public string Name { get; set; }
		public string Date { get; set; }
		public string Phone { get; set; }
		public IList<OrderLine> orderLines { get; set; }
		public string Note { get; set; }

	}

	public partial class FieldOrder : ContentPage
	{
		public OrderForm orderForm;
		public ObservableCollection<OrderLine> orderlines { get; set; }

		public FieldOrder ()
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

			orderlines = new ObservableCollection<OrderLine> ();

			/*var line1 = new OrderLine ();
			line1.item = "Gloves";
			line1.order = "1234";
			line1.pcr = "XCD";
			line1.qty = "5";
			line1.reasonCode = "99";
			orderlines.Add (line1);*/

			listView.ItemsSource = orderlines;
			listView.RowHeight = 80;

			MessagingCenter.Subscribe<AddOrderItem, OrderLine> (this, "addItem", (sender, arg) => {
				if(arg != null) {
					orderlines.Add((OrderLine) arg);
					listView.ItemsSource = null;
					listView.ItemsSource = orderlines;
				}
			});
		}

		private void initializeValues ()
		{
			this.orderForm = new OrderForm();
			orderForm.Job = Job.Text;
			orderForm.Customer = customerId.Items[customerId.SelectedIndex].ToString();
			orderForm.Date = DateTime.Now.ToString();
			orderForm.Name = App.currentUser.userName;
			orderForm.Note = note.Text;
		}

		async void OnSubmit (object sender, EventArgs e)
		{
			// Setup all excel variables
			initializeValues();
			generateExcelFromTemplate ();
			//Go back
			await this.Navigation.PopAsync();
		}

		async void OnAddItem (object sender, EventArgs e)
		{
			await Navigation.PushModalAsync (new AddOrderItem ());
		}


		private void sendEmail (string excelFileURL)
		{
			App.Parse.sendFieldOrder("erikj54+orders@gmail.com", excelFileURL);
		}

		public void generateExcelFromTemplate ()
		{
			this.orderForm.orderLines = orderlines;
			Assembly executingAssembly = typeof(App).GetTypeInfo ().Assembly;
			Stream inputStream = null;

			inputStream = executingAssembly.GetManifestResourceStream ("IndustrialParamedics.Field_Order_Template.xlsx");

			IWorkbook book = null;
			ExcelEngine excelEngine = new ExcelEngine ();

			book = excelEngine.Excel.Workbooks.Open (inputStream);
			inputStream.Dispose ();


			IWorksheet sheet = book.Worksheets [0];

			//Create Template Marker Processor
			ITemplateMarkersProcessor marker = book.CreateTemplateMarkersProcessor();
			marker.AddVariable("OrderForm", this.orderForm);

			//Applies the marker.
			marker.ApplyMarkers(UnknownVariableAction.Skip);

			Stream data = new MemoryStream ();
			book.SaveAs(data);
			book.Close ();
			data.Seek(0, SeekOrigin.Begin);

			App.Parse.saveFile ("FieldOrder.xlsx", data, this.sendEmail);

		}


		public void OnDelete (object sender, EventArgs e) {
			var mi = ((MenuItem)sender);
			var t = mi.CommandParameter;
			this.orderlines.Remove ((OrderLine)t);
		}
	}
}

