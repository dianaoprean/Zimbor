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
    public class ClientiController : BaseController
    {
        [Dependency]
        public IZimborRepository<Client> ClientRepository { get; set; }

        [Dependency]
        public IZimborRepository<Judet> JudetRepository { get; set; }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetAll(JqGridRequest request)
        {
            var records = ClientRepository.Find(JqGridHelper.GetFilterExpression(request), JqGridHelper.GetSortExpression(request));
            return GetJqGridJsonResult(request, records);
        }

        [HttpGet]
        public ActionResult Get(int? id)
        {
            var model = new ClientModel();
            if (id > 0)
            {
                model = ClientRepository.Select(x => x.ID == id).FirstOrDefault().ToModel();
            }

            var judete = JudetRepository.Select();
            ViewBag.Judete = Utils.SelectList(judete, "ID", "Nume", "Selectati Judetul");

            return PartialView("_Edit", model);
        }

        [HttpPost]
        public ActionResult Save(ClientModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = ClientRepository.Select(x => x.ID == model.Id).FirstOrDefault() ?? new Client();

                model.ToEntity(ref entity);

                ClientRepository.Insert(entity);

                return Json(new { success = true });
            }

            var judete = JudetRepository.Select();
            ViewBag.Judete = Utils.SelectList(judete, "ID", "Nume", "Selectati Judetul");

            return PartialView("_Edit", model);

        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var client = ClientRepository.Select(x => x.ID == id).FirstOrDefault();
            ClientRepository.Delete(client);
            return Json(new { success = true });
        }


        private JqGridJsonResult GetJqGridJsonResult(JqGridRequest request, IEnumerable<Client> records)
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
