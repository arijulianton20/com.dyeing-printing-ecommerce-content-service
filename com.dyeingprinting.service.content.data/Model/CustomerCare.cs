using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace com.dyeingprinting.service.content.data.Model
{
    public class CustomerCare : StandardEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl1 { get; set; }
        public string ContentTitle1 { get; set; }
        public string ContentDescription1 { get; set; }
        public string TextButton1 { get; set; }
        public string ContentUrl1 { get; set; }

        public string ImageUrl2 { get; set; }
        public string ContentTitle2 { get; set; }
        public string ContentDescription2 { get; set; }
        public string TextButton2 { get; set; }
        public string ContentUrl2 { get; set; }

    }
}
