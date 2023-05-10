using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TDD_Api.Models
{
    
    public class ImageDto
    {
        public string Url { get; set; }
        public string Location { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Description { get; set; }
        public byte ImageBlobData { get; set; }
    }
}