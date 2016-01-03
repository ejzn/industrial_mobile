using System;
using System.Collections.Generic;

namespace IndustrialParamedics
{
	public class Form
	{
		
		public Dictionary<string,string> elements = new Dictionary<string,string>();
		public enum FormType { Activity, CustomerRequest, ServiceRequest};
		public static Dictionary<FormType,string> ParseRelation = new Dictionary<FormType, string> {
			{FormType.Activity, "Activity"}, {FormType.CustomerRequest, "CustomerRequest"}, {FormType.ServiceRequest, "ServiceRequest"}
		};

		public FormType formType;

		public Form (FormType formType)
		{
			this.formType = formType;
		}
			

		public void addElement (string key, string value) 
		{
			elements.Add (key, value);
		}

		public void removeElement(string key)
		{
			elements.Remove (key);
		}
	}
}

