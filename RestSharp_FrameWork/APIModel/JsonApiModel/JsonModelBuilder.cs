using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharp_FrameWork.APIModel.JsonApiModel
{
    public class JsonModelBuilder
    {
        private string _brandName { get; set; }
        private Features _features { get; set; }
        private int _id { get; set; }
        private string _laptopName { get; set; }

        public JsonModel Build()
        {
            return new JsonModel()
            {
                BrandName = _brandName,
                Features = _features,
                Id = _id,
                LaptopName = _laptopName
            };
        }

        public JsonModelBuilder WithBrandName(string brandname)
        {
            _brandName = brandname;
            return this;
        }

        public JsonModelBuilder WithId(int id)
        {
            _id = id;
            return this;
        }

        public JsonModelBuilder WithLaptopName(string laptopName)
        {
            _laptopName = laptopName;
            return this;
        }

        public JsonModelBuilder WithFeatures(List<string> features)
        {
            _features = new Features()
            {
                Feature = features
            };
            return this;
        }

    }
}
