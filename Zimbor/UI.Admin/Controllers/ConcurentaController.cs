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
    public class ConcurentaController : BaseController
    {
        [Dependency]
        public IZimborRepository<Concurenta> ConcurentaRepository { get; set; }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetAll(JqGridRequest request)
        {
            var records = ConcurentaRepository.Find(JqGridHelper.GetFilterExpression(request), JqGridHelper.GetSortExpression(request));

            return GetJqGridJsonResult(request, records);
        }

        [HttpGet]
        public ActionResult Get(int? id)
        {
            var model = (id > 0) ? ConcurentaRepository.Select(x => x.ID == id).FirstOrDefault().ToModel() : new ConcurentaModel();

            return PartialView("_Edit", model);
        }

        [HttpPost]
        public ActionResult Save(ConcurentaModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = ConcurentaRepository.Select(x => x.ID == model.Id).FirstOrDefault() ?? new Concurenta();

                model.ToEntity(ref entity);

                ConcurentaRepository.Insert(entity);

                return Json(new { success = true });
            }
            return PartialView("_Edit", model);

        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var entity = ConcurentaRepository.Select(x => x.ID == id).FirstOrDefault();

            ConcurentaRepository.Delete(entity);

            return Json(new { success = true });
        }


        private JqGridJsonResult GetJqGridJsonResult(JqGridRequest request, IEnumerable<Concurenta> records)
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
