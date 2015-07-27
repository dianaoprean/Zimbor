using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Data.Repositories;
using Domain;
using Infrastructure.UI.Helpers;
using Lib.Web.Mvc.JQuery.JqGrid;
using Microsoft.Practices.Unity;
using UI.Admin.Extensions;
using UI.Admin.Models;


namespace UI.Admin.Controllers
{
    public class StudiuPiataController : BaseController
    {
        [Dependency]
        public IZimborRepository<StudiuPiata> StudiuPiataRepository { get; set; }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetAll(JqGridRequest request)
        {
            var records = StudiuPiataRepository.Find(JqGridHelper.GetFilterExpression(request), JqGridHelper.GetSortExpression(request));

            return GetJqGridJsonResult(request, records);
        }

        [HttpGet]
        public ActionResult Get(int? id)
        {
            var model = (id > 0) ? StudiuPiataRepository.Select(x => x.ID == id).FirstOrDefault().ToModel() : new StudiuPiataModel();

            return PartialView("_Edit", model);
        }

        [HttpPost]
        public ActionResult Save(StudiuPiataModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = StudiuPiataRepository.Select(x => x.ID == model.Id).FirstOrDefault() ?? new StudiuPiata();

                model.ToEntity(ref entity);

                StudiuPiataRepository.Insert(entity);

                return Json(new { success = true });
            }
            return PartialView("_Edit", model);

        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var entity = StudiuPiataRepository.Select(x => x.ID == id).FirstOrDefault();

            StudiuPiataRepository.Delete(entity);

            return Json(new { success = true });
        }


        private JqGridJsonResult GetJqGridJsonResult(JqGridRequest request, IEnumerable<StudiuPiata> records)
        {
            int totalRecordsCount = records.Count();

            var result = GetJqGridResponse(totalRecordsCount, request);

            records = records.Skip((request.PageIndex) * request.RecordsCount).Take(request.RecordsCount).ToList();

            foreach (var item in records)
            {
                result.Records.Add(item.ToJqGridRecord());
            }

            return new JqGridJsonResult { Data = result };
        }

    }
}
