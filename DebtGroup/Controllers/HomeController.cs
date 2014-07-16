using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Google.GData.Spreadsheets;
using Google.GData.Client;

namespace DebtGroup.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

		public ActionResult DoAuth()
		{
			string ClientId = Environment.GetEnvironmentVariable("Google_Client_Id");
			string ClientSecret = Environment.GetEnvironmentVariable ("Google_Client_Secret");
			string Scope = "https://spreadsheets.google.com/feeds https://docs.google.com/feeds";
			string RedirectUri = "http://debtgroup.azurewebsites.net/oauth2callback";
			OAuth2Parameters parameters = new OAuth2Parameters();
			parameters.ClientId = ClientId;
			parameters.ClientSecret = ClientSecret;
			parameters.Scope = Scope;
			parameters.RedirectUri = RedirectUri;
			string OAuthUrl = OAuthUtil.CreateOAuth2AuthorizationUrl (parameters);
			HttpContext.Application ["GoogleAuthParams"] = parameters;
			return Redirect (OAuthUrl);
		}

		public ActionResult oauth2callback()
		{
			return View ();
		}

		public ActionResult SheetApp(string token)
		{
			OAuth2Parameters parameters = HttpContext.Application ["GoogleAuthParams"] as OAuth2Parameters;
			parameters.AccessCode = token;
			OAuthUtil.GetAccessToken (parameters);
			GOAuth2RequestFactory requestFactory = new GOAuth2RequestFactory (null, "MySpreadsheetIntegration-v1", parameters);
			SpreadsheetsService service = new SpreadsheetsService ("MySpreadsheetIntegration-v1");
			service.RequestFactory = requestFactory;
			SpreadsheetQuery query = new SpreadsheetQuery ();
			SpreadsheetFeed feed = service.Query (query);
			foreach (SpreadsheetEntry entry in feed.Entries) 
			{
				ViewBag.Sheets += entry.Title.Text;
			}
			return View ();
		}
    }
}
