using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServiceAutomation.Model.XmlModel
{
    public class XmlModelBuilder
    {
        private string _BrandName { get; set; }
        private Features _Features { get; set; }
        private string _Id { get; set; }
        private string _LaptopName { get; set; }

        public Laptop Build()
        {
            return new Laptop()
            {
                BrandName = _BrandName,
                Features = _Features,
                Id = _Id,
                LaptopName = _LaptopName
            };
        }

        public XmlModelBuilder WithBrandName(string name)
        {
            _BrandName = name;
            return this;
        }

        public XmlModelBuilder WithId(int id)
        {
            _Id = id.ToString();
            return this;
        }

        public XmlModelBuilder WithLaptopName(string lName)
        {
            _LaptopName = lName;
            return this;
        }


        public XmlModelBuilder WithFeatures(List<string> features)
        {
            _Features = new Features()
            {
                Feature = features
            };
            return this;
        }
    }

}
