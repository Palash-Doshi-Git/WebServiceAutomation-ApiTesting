using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServiceAutomation.Model.JsonModel
{
    public class JsonRootObjectBuilder
    {

        private string _brandName { get; set; }
        private Features _features { get; set; }
        private int _id { get; set; }
        private string _laptopName { get; set; }

        public JsonRootObject Build()
        {
            return new JsonRootObject()
            {
                BrandName = _brandName,
                Features = _features,
                Id = _id,
                LaptopName = _laptopName
            };
        }

        public JsonRootObjectBuilder WithBrandName(string brandname)
        {
            _brandName = brandname;
            return this;
        }

        public JsonRootObjectBuilder WithId(int id)
        {
            _id = id;
            return this;
        }

        public JsonRootObjectBuilder WithLaptopName(string laptopName)
        {
            _laptopName = laptopName;
            return this;
        }

        public JsonRootObjectBuilder WithFeatures(List<string> features)
        {
            _features = new Features()
            {
                Feature = features
            };
            return this;
        }


    }
}
