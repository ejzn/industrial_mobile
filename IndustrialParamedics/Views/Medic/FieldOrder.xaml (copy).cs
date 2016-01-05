using System;
using System.Collections.Generic;


using Xamarin.Forms;

namespace IndustrialParamedics
{
	public partial class FieldOrder : ContentPage
	{
		private IDictionary<string,string> clients;

		public static IDictionary<string, string> fields = new Dictionary<string, string> {
			{"DATE" , "activityDate"}, {"IPS_JOB_ID", "ipsJobId"}, {"CUST_JOB_ID", "custJobId"}, {"MEDIC_ID", "medicId"},{ "CLIENT_ID" , "clientId"},
			{"COMPANY_ORIENTATIONS" , "numCompanyOrientations"}, {"SITE_ORIENTATIONS" , "numSiteOrientations"},
			{"HAZARD_ORIENTATIONS" , "numHazardOrientations"}, {"ISP_JPA" , "numIPSPJAs"}, {"THIRD_PARTY", "numThirdPartys"},
			{"SAFETY_MEETINGS" , "numSafetyMeetings"}, {"WORK_PERMITS" , "numSafeWorkPermits"}};

		public FieldOrder ()
		{
			InitializeComponent ();
			InitializeFieldValues ();
		}
			


		void OnSubmit (object sender, EventArgs e)
		{
			
			Form form = fillForm ();
			App.Parse.saveForm (form);
			this.Navigation.PopAsync ();
		}

		Form fillForm () 
		{
			Form form = new Form (Form.FormType.Activity);
			form.addElement (fields ["DATE"], date.Date.ToString());
			form.addElement (fields ["MEDIC_ID"], App.currentUser.userId.ToString());
			form.addElement(fields["IPS_JOB_ID"], ipsJobId.Text);
			form.addElement(fields["CUST_JOB_ID"], custJobId.Text);

			if (clientId.Items[clientId.SelectedIndex] != null) {
				form.addElement (fields ["CLIENT_ID"], clients[clientId.Items[clientId.SelectedIndex]]);
			}

			if (companyOrientations.Items [companyOrientations.SelectedIndex] != null) {
				form.addElement (fields ["COMPANY_ORIENTATIONS"], companyOrientations.Items [companyOrientations.SelectedIndex]);
			}
			if (siteOrientations.Items [siteOrientations.SelectedIndex] != null) {
				form.addElement (fields ["SITE_ORIENTATIONS"], siteOrientations.Items [siteOrientations.SelectedIndex]);
			}
			if (hazardWorkIds.Items [hazardWorkIds.SelectedIndex] != null) {
				form.addElement (fields ["HAZARD_ORIENTATIONS"], hazardWorkIds.Items [hazardWorkIds.SelectedIndex]);
			}			
			if (ipsPjas.Items [ipsPjas.SelectedIndex] != null) {
				form.addElement (fields ["ISP_JPA"], ipsPjas.Items [ipsPjas.SelectedIndex]);
			}
			if (thirdPartyJSAs.Items [thirdPartyJSAs.SelectedIndex] != null) {
				form.addElement (fields ["THIRD_PARTY"], thirdPartyJSAs.Items [thirdPartyJSAs.SelectedIndex]);
			}
			if (safeWorkPermits.Items [safeWorkPermits.SelectedIndex] != null) {
				form.addElement (fields ["WORK_PERMITS"], safeWorkPermits.Items [safeWorkPermits.SelectedIndex]);
			
			}
			if (safetyMeetings.Items [safetyMeetings.SelectedIndex] != null) {
				form.addElement (fields ["SAFETY_MEETINGS"], safetyMeetings.Items [safetyMeetings.SelectedIndex]);
			}

			return form;
		}

		void custTextChanged (object sender, TextChangedEventArgs e) {
			if (custJobId.Text.Length > 0) {
				submitButton.IsEnabled = true;
			} else {
				submitButton.IsEnabled = false;
			}
		}

		 void InitializeFieldValues () {

			submitButton.IsEnabled = false;


			/* CLIENT Fields*/
			medicId.Text = App.currentUser.userName;
			App.Parse.query ("Client", delegate (IDictionary<string,string> results) {
				this.clients = results;

				foreach (var client in results) 
				{
					clientId.Items.Add(client.Key);
				}
				if(clientId.Items.Count > 0) {
					clientId.SelectedIndex = 0;
				}
			});

			if(companyOrientations.Items.Count > 0) {
				companyOrientations.SelectedIndex = 0;
			}

			if(siteOrientations.Items.Count > 0) {
				siteOrientations.SelectedIndex = 0;
			}

			if(hazardWorkIds.Items.Count > 0) {
				hazardWorkIds.SelectedIndex = 0;
			}

			if(ipsPjas.Items.Count > 0) {
				ipsPjas.SelectedIndex = 0;
			}

			if(thirdPartyJSAs.Items.Count > 0) {
				thirdPartyJSAs.SelectedIndex = 0;
			}

			if(safeWorkPermits.Items.Count > 0) {
				safeWorkPermits.SelectedIndex = 0;
			}

			if(safetyMeetings.Items.Count > 0) {
				safetyMeetings.SelectedIndex = 0;
			}
				
		}
	}
}

