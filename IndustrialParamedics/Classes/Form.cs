using System;
using System.Collections.Generic;

namespace IndustrialParamedics
{
	public class Form
	{
		Dictionary<string,string> elements = new Dictionary<string,string>();
		public enum FormType { Activity, CustomerRequest, ServiceRequest};
		public FormType formType;

		public Form (FormType formType)
		{
			this.formType = formType;
		}

		void addElement (string key, string value) 
		{
			elements.Add (key, value);
		}

		void removeElement(string key)
		{
			elements.Remove (key);
		}
	}
}

