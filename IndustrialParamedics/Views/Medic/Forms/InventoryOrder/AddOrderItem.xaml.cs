using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace IndustrialParamedics
{
	public partial class AddOrderItem : ContentPage
	{

		public AddOrderItem ()
		{
			InitializeComponent ();
		}

		async void OnSubmit (object sender, EventArgs e)
		{
			OrderLine line = new OrderLine ();
			if (Item.Text != null) {
				line.item = Item.Text;
			}
			if (Order.Text != null) {
				line.order = Order.Text;
			}
			if (PCR.Text != null) {
				line.pcr = PCR.Text;
			}
			if (QTY.SelectedIndex > 0 && QTY.Items [QTY.SelectedIndex] != null) {
				line.qty = QTY.Items [QTY.SelectedIndex].ToString ();
			}

			if (Reason.Text != null) {
				line.reasonCode = Reason.Text;
			}
			if (line != null) {
				MessagingCenter.Send<AddOrderItem, OrderLine> (this, "addItem", line);
			}
			await this.Navigation.PopModalAsync();
		}

		async void OnCancel (object sender, EventArgs e)
		{
			await this.Navigation.PopModalAsync ();
		}

	}
}

