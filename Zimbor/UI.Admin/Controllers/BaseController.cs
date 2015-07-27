using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data.Repositories;
using Domain;
using Infrastructure.UI.Helpers;
using Lib.Web.Mvc.JQuery.JqGrid;
using Microsoft.Practices.Unity;
using UI.Admin.Models;

namespace UI.Admin.Controllers
{
    public class BaseController : Controller
    {
        [Dependency]
        public IZimborRepository<Image> ImageRepository { get; set; }


        public JqGridResponse GetJqGridResponse(int totalRecordsCount, JqGridRequest request)
        {
            return new JqGridResponse
            {
                TotalPagesCount = (int)Math.Ceiling((float)totalRecordsCount / (float)request.RecordsCount),
                PageIndex = request.PageIndex,
                TotalRecordsCount = totalRecordsCount,
                UserData = new { sortexp = request.SortingName, sortdir = request.SortingOrder.ToString().ToLower() }
            };
        }

        [HttpPost]
        public ActionResult UploadImages()
        {
            var uploadResult = new UploadAdArrayResult();
            try
            {
                if (Request.Files != null && Request.Files.Count > 0)
                {
                    for (var i = 0; i < Request.Files.Count; i++)
                    {
                        var file = Request.Files[i];

                        if (file != null)
                        {
                            int ifg = 0;
                            //file.SaveAs("C:\\"+ file.FileName);
                            var image = new Image { Path = file.FileName, Name= file.FileName, Type = file.ContentType, ImageContent = ConvertToBytes(file) };
                            ImageRepository.Insert(image);


                            uploadResult.files.Add(new UploadFileResult
                            {
                                name = file.FileName,
                                size = file.ContentLength,
                                type = file.ContentType,
                                imageID = image.ID,
                                url = Url.Action("GetImage", new { image.ID }),
                                deleteUrl = Url.Action("DeleteAd", new { image.ID }),
                                thumbnailUrl = Url.Action("GetImage", new { image.ID }),
                                delete_type = "GET"
                            });
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return Json(uploadResult, "text/plain");
        }

        private byte[] ConvertToBytes(HttpPostedFileBase image)
        {
            byte[] imageBytes = null;
            BinaryReader reader = new BinaryReader(image.InputStream);
            imageBytes = reader.ReadBytes((int)image.ContentLength);
            return imageBytes;
        }

        public ActionResult GetImage(int id)
        {
            var img = GetImageFromDataBase(id);
            byte[] cover = img.ImageContent;
            if (cover != null)
            {
                return File(cover, img.Type);
            }
            else
            {
                return null;
            }
        }

        private Image GetImageFromDataBase(int imageId)
        {
            return ImageRepository.Select(x => x.ID == imageId).FirstOrDefault();
        }

    }
}
