using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace com.dyeingprinting.service.content.data.Model
{
    public class WebContent : StandardEntity
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string Link { get; set; }
        public int Order { get; set; }
        public string LinkYoutube { get; set; }
        public string TextButton { get; set; }

        public void UpdateData(WebContent model)
        {
            this.Name = model.Name;
            this.Title = model.Title;
            this.Description = model.Description;
            this.ImageUrl = model.ImageUrl;
            this.Link = model.Link;
            this.Order = model.Order;
            this.LinkYoutube = model.LinkYoutube;
            this.TextButton = model.TextButton;
        }
    }
}
