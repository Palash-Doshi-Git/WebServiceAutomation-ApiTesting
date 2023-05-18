using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace WebServiceAutomation.Model.XmlModel
{
	[XmlRoot(ElementName = "Features")]
	public class Features
	{
		[XmlElement(ElementName = "Feature")]
		public List<string> Feature { get; set; }
	}
}
