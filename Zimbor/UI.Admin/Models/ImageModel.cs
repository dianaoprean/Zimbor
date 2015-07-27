using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UI.Admin.Models
{
    public class ImageModel
    {
    }

 
    public class UploadAdArrayResult
    {
        public UploadAdArrayResult()
        {
            this.files = new List<UploadFileResult>();
        }

        public List<UploadFileResult> files { get; set; }
    }

    public class UploadFileResult
    {
        public string name { get; set; }
        public int size { get; set; }
        public string type { get; set; }
        public bool isVideo { get; set; }
        public string url { get; set; }
        public int imageID { get; set; }
        public string deleteUrl { get; set; }
        public string thumbnailUrl { get; set; }
        public string delete_type { get; set; }
        public string error { get; set; }
    }

}