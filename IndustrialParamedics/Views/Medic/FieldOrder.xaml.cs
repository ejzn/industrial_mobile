using System;
using System.Collections.Generic;


using Xamarin.Forms;

namespace IndustrialParamedics
{
	public partial class FieldOrder : ContentPage
	{
		public FieldOrder ()
		{
			InitializeComponent ();
			medicId.Text = App.currentUser.userName;
		}

		void OnSubmit (object sender, EventArgs e)
		{
			Form form = new Form (Form.FormType.Activity);
			fillForm (form);
			//int formId = form.saveSync ();

			App.Parse.sendEmail ("erik@erikjohnson.ca", 5);

			//Send out the Email, or at least trigger an event that sends it out
			this.Navigation.PopAsync ();
		}

		void fillForm (Form form) 
		{
			
		}
	}
}

