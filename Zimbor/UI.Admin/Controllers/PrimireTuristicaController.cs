using System;
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
    public class PrimireTuristicaController : BaseController
    {
        [Dependency]
        public IZimborRepository<PrimireTuristica> PrimireTuristicaRepository { get; set; }

        [Dependency]
        public IZimborRepository<Zona> ZonaRepository { get; set; }

        [Dependency]
        public IZimborRepository<ImaginePrimireTuristica> ImaginePrimireTuristicaRepository { get; set; }


        public ActionResult Index()
        {


            return View();
        }

        public ActionResult GetAll(JqGridRequest request)
        {
            var records = PrimireTuristicaRepository.Find(JqGridHelper.GetFilterExpression(request), JqGridHelper.GetSortExpression(request));
            return GetJqGridJsonResult(request, records);
        }

        [HttpGet]
        public ActionResult Get(int? id)
        {
            var model = new PrimireTuristicaModel();
            if (id > 0)
            {
                model = PrimireTuristicaRepository.Select(x => x.ID == id).FirstOrDefault().ToModel();
            }

            var zone = ZonaRepository.Select();
            ViewBag.Zone = Utils.SelectList(zone, "ID", "Nume", "Selectati Zona");

            return PartialView("_Edit", model);
        }

        [HttpPost]
        public ActionResult Save(PrimireTuristicaModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = PrimireTuristicaRepository.Select(x => x.ID == model.Id).FirstOrDefault() ?? new PrimireTuristica();

                model.ToEntity(ref entity);

                try
                {
                    PrimireTuristicaRepository.Insert(entity);
                }
                catch (Exception ex)
                {
                    throw;
                }

                if (model.ImageIds != null && model.ImageIds.Any())
                {
                    foreach (var imageId in model.ImageIds)
                    {
                        var imgp = new ImaginePrimireTuristica { ImageID = imageId, PrimireTuristicaID = entity.ID };
                        ImaginePrimireTuristicaRepository.Insert(imgp);
                    }
                 
                }
                return Json(new { success = true });
            }

            var zone = ZonaRepository.Select();
            ViewBag.Zone = Utils.SelectList(zone, "ID", "Nume", "Selectati Zona");

            return PartialView("_Edit", model);

        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var PrimireTuristica = PrimireTuristicaRepository.Select(x => x.ID == id).FirstOrDefault();
            PrimireTuristicaRepository.Delete(PrimireTuristica);
            return Json(new { success = true });
        }


        private JqGridJsonResult GetJqGridJsonResult(JqGridRequest request, IEnumerable<PrimireTuristica> records)
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
