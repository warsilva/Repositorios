using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Facebook;
using System.Dynamic;
using System.Net.Http;




namespace Sistema.Web.Controllers
{
	public class HomeController : Controller
	{

		//http://www.rodolfofadino.com.br/2013/12/api-do-facebook-com-c-facebook-sdk-for-net/#comment-312
		public string GetFacebookLoginUrl()
		{
			dynamic parameters = new ExpandoObject();
			parameters.client_id = "1165085896873778";
			parameters.redirect_uri = "http://localhost:50748/home/retornofb";
			parameters.response_type = "code";
			parameters.display = "page";
			//var extendedPermissions = "user_about_me,read_stream,publish_stream";
			//parameters.scope = extendedPermissions;
			var _fb = new FacebookClient();
			var url = _fb.GetLoginUrl(parameters);
			return url.ToString();
		}
		public IActionResult Index()
		{
			ViewBag.UrlFb = GetFacebookLoginUrl();
			return View();
		}

		public ActionResult RetornoFb()
		{
			return RedirectToAction("About");
		}





		public IActionResult About()
		{
			ViewData["Message"] = "Your application description page.";

			return View();
		}

		public IActionResult Contact()
		{
			ViewData["Message"] = "Your contact page.";

			return View();
		}

		public IActionResult Error()
		{
			return View();
		}
	}
}
