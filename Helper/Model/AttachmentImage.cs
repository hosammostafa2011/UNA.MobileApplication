using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Helper.XamarinModel
{
    public class AttachmentImage
    {
        public string NAME { get; set; }
        public ImageSource image { get; set; }
        public string id { get; set; }
        public byte[] IMAGE_BYTES { get; set; }


        public AttachmentImage(string _NAME, ImageSource image, string id, byte[] IMAGE_BYTES)
        {
            this.NAME = _NAME;
            this.image = image;
            this.id = id;
            this.IMAGE_BYTES = IMAGE_BYTES;
        }

    }
}
