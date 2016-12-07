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
			parameters.display = "popup";
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

		public IActionResult retornofb()
		{
			var _fb = new FacebookClient();
			FacebookOAuthResult oauthResult;
			Uri url = new Uri($"http://localhost:50748/home/retornofb{Request.QueryString.ToString()}");
			if (_fb.TryParseOAuthCallbackUrl(url, out oauthResult))
			{
				//Pega o Access Token "permanente"
				dynamic parameters = new ExpandoObject();
				parameters.client_id = "1165085896873778";
				parameters.redirect_uri = "http://localhost:50748/home/retornofb";
				parameters.client_secret = "0800d5de424d04dafed1e15c8f11cfad";
				parameters.code = oauthResult.Code;
				//dynamic result = _fb.Get("/oauth/access_token", parameters);	
				//var accessToken = result.access_token;

				//TODO: Guardar no banco
				//Session.Add("FbUserToken", accessToken);
				ViewBag.Username = "Warleson";
				return RedirectToAction("Index", "Chat",new { });
			}
			else
			{
				// tratar
				return RedirectToAction("Index");
			}

		}
		public IActionResult Chat()
		{
			return View();
		}

	}
}
