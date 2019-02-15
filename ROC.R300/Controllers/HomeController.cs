using ROC.R300.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ROC.R300.Controllers
{
    public class HomeController : Controller
    {
        MyDbContext db = new MyDbContext();
        public ActionResult Index()
        {
            HttpCookie cook = Request.Cookies["_rocvip_id"];
            if (cook != null)
            {
                string rocvipId = cook.Value;
                var record = db.VisitRecords.Where(t => t.UserId.Equals(rocvipId)).FirstOrDefault();
                if (record != null)
                {
                    record.LastTime = DateTime.Now;
                    record.VisitTimes++;
                    db.SaveChanges();
                    return View();
                }
            }

            string guidId = Guid.NewGuid().ToString();
            //向用户浏览器 保存cookies
            cook = new HttpCookie("_rocvip_id");
            cook.Value = guidId;
            cook.Expires = DateTime.Now.AddYears(1);
            Response.Cookies.Add(cook);

            //保存访问记录到数据库
            HttpBrowserCapabilitiesBase bc = Request.Browser;
            string userAgent = Request.UserAgent;
            StringBuilder sb = new StringBuilder();
            sb.Append("Mobile:" + IsMobile(userAgent));
            sb.Append(" Type:" + bc.Type);
            //sb.Append(" Name:" + bc.Browser);
            //sb.Append(" Version:" + bc.Version);
            //sb.Append(" Major Version = " + bc.MajorVersion);
            //sb.Append(" Minor Version = " + bc.MinorVersion);
            //sb.Append(" Platform:" + bc.Platform);
            sb.Append(" OS:" + GetHoverTreeOSName(userAgent));
            //sb.Append(" Beta = " + bc.Beta);
            //sb.Append(" Crawler = " + bc.Crawler);
            //sb.Append(" AOL = " + bc.AOL);
            //sb.Append(" Win16:" + bc.Win16);
            //sb.Append(" Win32:" + bc.Win32);
            //sb.Append(" Frames:" + bc.Frames);
            //sb.Append(" Tables" + bc.Tables);
            sb.Append(" Cookies:" + bc.Cookies);
            //sb.Append(" VB Script" + bc.VBScript);
            //sb.Append(" Java Applets" + bc.JavaApplets);
            //sb.Append(" ActiveX Controls" + bc.ActiveXControls);
            //sb.Append(" CDF = " + bc.CDF);
            sb.Append(" UserAgent:" + userAgent);

            db.VisitRecords.Add(new VisitInfo
            {
                UserId = guidId,
                Ip = Request.UserHostAddress,
                UrlReferrer = Request.UrlReferrer == null ? "" : Request.UrlReferrer.ToString(),
                Browser = sb.ToString(),
                CreateTime = DateTime.Now,
                LastTime = DateTime.Now,
                VisitTimes = 1
            });
            db.SaveChanges();

            return View();
        }

        [HttpPost]
        public ActionResult Message()
        {
            var name = Request["name"];
            var tel = Request["tel"];
            var msg = Request["message"];
            if(!string.IsNullOrEmpty(name)&&!string.IsNullOrEmpty(tel)&&!string.IsNullOrEmpty(msg))
            {
                db.UserMessages.Add(new UserMessage
                {
                    Name = name,
                    Tel = tel,
                    Message = msg,
                    CreateTime=DateTime.Now
                });
                db.SaveChanges();
            }
            return RedirectToAction("index");
        }       

        /// <summary>
        /// 根据 User Agent 获取操作系统名称
        /// </summary>
        private string GetHoverTreeOSName(string userAgent)
        {
            string m_hvtOsVersion = "未知";
            if (userAgent.Contains("NT 6.4"))
            {
                m_hvtOsVersion = "Windows 10";
            }
            else
            if (userAgent.Contains("NT 6.3"))
            {
                m_hvtOsVersion = "Windows 8.1";
            }
            else
            if (userAgent.Contains("NT 6.2"))
            {
                m_hvtOsVersion = "Windows 8";
            }
            else
            if (userAgent.Contains("NT 6.1"))
            {
                m_hvtOsVersion = "Windows 7";
            }
            else
            if (userAgent.Contains("NT 6.0"))
            {
                m_hvtOsVersion = "Windows Vista/Server 2008";
            }
            else if (userAgent.Contains("NT 5.2"))
            {
                m_hvtOsVersion = "Windows Server 2003";
            }
            else if (userAgent.Contains("NT 5.1"))
            {
                m_hvtOsVersion = "Windows XP";
            }
            else if (userAgent.Contains("NT 5"))
            {
                m_hvtOsVersion = "Windows 2000";
            }
            else if (userAgent.Contains("NT 4"))
            {
                m_hvtOsVersion = "Windows NT4";
            }
            else if (userAgent.Contains("Me"))
            {
                m_hvtOsVersion = "Windows Me";
            }
            else if (userAgent.Contains("98"))
            {
                m_hvtOsVersion = "Windows 98";
            }
            else if (userAgent.Contains("95"))
            {
                m_hvtOsVersion = "Windows 95";
            }
            else if (userAgent.Contains("Mac"))
            {
                m_hvtOsVersion = "Mac";
            }
            else if (userAgent.Contains("Unix"))
            {
                m_hvtOsVersion = "UNIX";
            }
            else if (userAgent.Contains("Linux"))
            {
                m_hvtOsVersion = "Linux";
            }
            else if (userAgent.Contains("SunOS"))
            {
                m_hvtOsVersion = "SunOS";
            }
            return m_hvtOsVersion;
        }

        private bool IsMobile(string userAgent)
        {

            //string u = Request.ServerVariables["HTTP_USER_AGENT"];
            string u = userAgent;
            Regex b = new Regex(@"(android|bb\d+|meego).+mobile|avantgo|bada\/|blackberry|blazer|compal|elaine|fennec|hiptop|iemobile|ip(hone|od)|iris|kindle|lge |maemo|midp|mmp|mobile.+firefox|netfront|opera m(ob|in)i|palm( os)?|phone|p(ixi|re)\/|plucker|pocket|psp|series(4|6)0|symbian|treo|up\.(browser|link)|vodafone|wap|windows ce|xda|xiino", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            Regex v = new Regex(@"1207|6310|6590|3gso|4thp|50[1-6]i|770s|802s|a wa|abac|ac(er|oo|s\-)|ai(ko|rn)|al(av|ca|co)|amoi|an(ex|ny|yw)|aptu|ar(ch|go)|as(te|us)|attw|au(di|\-m|r |s )|avan|be(ck|ll|nq)|bi(lb|rd)|bl(ac|az)|br(e|v)w|bumb|bw\-(n|u)|c55\/|capi|ccwa|cdm\-|cell|chtm|cldc|cmd\-|co(mp|nd)|craw|da(it|ll|ng)|dbte|dc\-s|devi|dica|dmob|do(c|p)o|ds(12|\-d)|el(49|ai)|em(l2|ul)|er(ic|k0)|esl8|ez([4-7]0|os|wa|ze)|fetc|fly(\-|_)|g1 u|g560|gene|gf\-5|g\-mo|go(\.w|od)|gr(ad|un)|haie|hcit|hd\-(m|p|t)|hei\-|hi(pt|ta)|hp( i|ip)|hs\-c|ht(c(\-| |_|a|g|p|s|t)|tp)|hu(aw|tc)|i\-(20|go|ma)|i230|iac( |\-|\/)|ibro|idea|ig01|ikom|im1k|inno|ipaq|iris|ja(t|v)a|jbro|jemu|jigs|kddi|keji|kgt( |\/)|klon|kpt |kwc\-|kyo(c|k)|le(no|xi)|lg( g|\/(k|l|u)|50|54|\-[a-w])|libw|lynx|m1\-w|m3ga|m50\/|ma(te|ui|xo)|mc(01|21|ca)|m\-cr|me(rc|ri)|mi(o8|oa|ts)|mmef|mo(01|02|bi|de|do|t(\-| |o|v)|zz)|mt(50|p1|v )|mwbp|mywa|n10[0-2]|n20[2-3]|n30(0|2)|n50(0|2|5)|n7(0(0|1)|10)|ne((c|m)\-|on|tf|wf|wg|wt)|nok(6|i)|nzph|o2im|op(ti|wv)|oran|owg1|p800|pan(a|d|t)|pdxg|pg(13|\-([1-8]|c))|phil|pire|pl(ay|uc)|pn\-2|po(ck|rt|se)|prox|psio|pt\-g|qa\-a|qc(07|12|21|32|60|\-[2-7]|i\-)|qtek|r380|r600|raks|rim9|ro(ve|zo)|s55\/|sa(ge|ma|mm|ms|ny|va)|sc(01|h\-|oo|p\-)|sdk\/|se(c(\-|0|1)|47|mc|nd|ri)|sgh\-|shar|sie(\-|m)|sk\-0|sl(45|id)|sm(al|ar|b3|it|t5)|so(ft|ny)|sp(01|h\-|v\-|v )|sy(01|mb)|t2(18|50)|t6(00|10|18)|ta(gt|lk)|tcl\-|tdg\-|tel(i|m)|tim\-|t\-mo|to(pl|sh)|ts(70|m\-|m3|m5)|tx\-9|up(\.b|g1|si)|utst|v400|v750|veri|vi(rg|te)|vk(40|5[0-3]|\-v)|vm40|voda|vulc|vx(52|53|60|61|70|80|81|83|85|98)|w3c(\-| )|webc|whit|wi(g |nc|nw)|wmlb|wonu|x700|yas\-|your|zeto|zte\-", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            if ((b.IsMatch(u) || v.IsMatch(u.Substring(0, 4))))
            {
                return true;
            }
            return false;
        }

}
}