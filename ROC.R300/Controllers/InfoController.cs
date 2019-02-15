using ROC.R300.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ROC.R300.Controllers
{
    public class InfoController : Controller
    {
        MyDbContext db = new MyDbContext();
        // GET: Info
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetReport(int type)
        {
            var reportInfos = db.Database.SqlQuery<ReportInfo>("exec GetReport @p0", type).ToList();
            var chartModel = new ChartModel()
            {
                Labels = reportInfos.Select(t => t.Lables).ToArray(),
                Data = reportInfos.Select(t => t.Qty).ToArray()
            };
            return Json(chartModel);
        }
    }
}