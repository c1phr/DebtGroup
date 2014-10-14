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
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

//        public ActionResult DoAuth()
//        {
//            string ClientId = Environment.GetEnvironmentVariable("Google_Client_Id");
//            string ClientSecret = Environment.GetEnvironmentVariable ("Google_Client_Secret");
//            string Scope = "https://spreadsheets.google.com/feeds https://www.googleapis.com/auth/plus.me";
//            string RedirectUri = "http://debtgroup.azurewebsites.net/oauth2callback";
//            OAuth2Parameters parameters = new OAuth2Parameters();
//            parameters.ClientId = ClientId;
//            parameters.ClientSecret = ClientSecret;
//            parameters.Scope = Scope;
//            parameters.RedirectUri = RedirectUri;
//            string OAuthUrl = OAuthUtil.CreateOAuth2AuthorizationUrl (parameters);
//            HttpContext.Application ["GoogleAuthParams"] = parameters;
//            return Redirect (OAuthUrl);
//        }

//        public ActionResult oauth2callback()
//        {
//            string qs = Request.QueryString ["code"];
//            OAuth2Parameters parameters = HttpContext.Application ["GoogleAuthParams"] as OAuth2Parameters;
//            parameters.AccessCode = qs;
//            OAuthUtil.GetAccessToken (parameters);
//            GOAuth2RequestFactory requestFactory = new GOAuth2RequestFactory (null, "MySpreadsheetIntegration-v1", parameters);
//            SpreadsheetsService service = new SpreadsheetsService ("MySpreadsheetIntegration-v1");
//            service.RequestFactory = requestFactory;
//            SpreadsheetQuery query = new SpreadsheetQuery ();
//            SpreadsheetFeed feed = service.Query (query);
//            int sheetIndex = 0;
//            //This is probably inefficient, but having issues with Linq under Mono. Deal with it later.
//            foreach (SpreadsheetEntry e in feed.Entries) {
//                if (feed.Entries [sheetIndex].Title.Text == "Ryan Dustin Cassie Payments") {
//                    break;
//                } else {
//                    sheetIndex++;
//                }
//            }
//            if (sheetIndex >= feed.Entries.Count) {
//                ViewBag.Message = "Could not find the required sheet in your Google Drive";
//                return View ();
//            }
//            SpreadsheetEntry sheet = (SpreadsheetEntry)feed.Entries[sheetIndex];
//            WorksheetFeed wsFeed = sheet.Worksheets;
//            WorksheetEntry WrkSht = (WorksheetEntry)wsFeed.Entries[0];
//            CellQuery cellQuery = new CellQuery (WrkSht.CellFeedLink);
//            cellQuery.MinimumRow = 1;
//            cellQuery.MaximumRow = 1;
//            cellQuery.MinimumColumn = 1;
//            cellQuery.MaximumColumn = 4;
//            CellFeed cellFeed = service.Query (cellQuery);
//            List<float> totalArray = new List<float>();
//            int countNegs = 0;
//            string negLocs = "";
//            foreach (CellEntry cell in cellFeed.Entries) 
//            {
//                float val = float.Parse(cell.Value.ToString (), System.Globalization.CultureInfo.InvariantCulture);
//                if (val < 0) {
//                    countNegs++;
//                    negLocs += "n";
//                } else {
//                    negLocs += "p";
//                }
//                totalArray.Add(val);
//            }
//            ViewBag.Message = ProcessSpreadsheetValues (totalArray, countNegs, negLocs);
////			foreach (float st in totalArray) {
////				ViewBag.Message += st.ToString(System.Globalization.CultureInfo.InvariantCulture);
////			}
//            return View ();
//        }

//        public ActionResult SheetApp(string token)
//        {
//            OAuth2Parameters parameters = HttpContext.Application ["GoogleAuthParams"] as OAuth2Parameters;
//            parameters.AccessCode = token;
//            OAuthUtil.GetAccessToken (parameters);
			
//            GOAuth2RequestFactory requestFactory = new GOAuth2RequestFactory (null, "MySpreadsheetIntegration-v1", parameters);
//            SpreadsheetsService service = new SpreadsheetsService ("MySpreadsheetIntegration-v1");
//            service.RequestFactory = requestFactory;
//            SpreadsheetQuery query = new SpreadsheetQuery ();
//            SpreadsheetFeed feed = service.Query (query);
//            foreach (SpreadsheetEntry entry in feed.Entries) 
//            {
//                ViewBag.Sheets += entry.Title.Text;
//            }
//            return View ();
//        }

//        public string ProcessSpreadsheetValues(List<float> values, int countNegs, string negativeLocs)
//        {
//            string outString = "";
//            if (countNegs == 1) 
//            {
//                if (negativeLocs [0].ToString() == "n") { //Dustin is getting money
//                    outString += "Ryan owes Dustin $" + values [1] + "<br />";
//                    outString += "Cassie owes Dustin $" + values [2] + "<br />";
//                } else if (negativeLocs [1].ToString() == "n") { //Ryan is getting money
//                    outString += "Dustin owes Ryan $" + values [0] + "<br />";
//                    outString += "Cassie owes Ryan $" + values [2] + "<br />";
//                } else if (negativeLocs [2].ToString() == "n") { //Cassie is getting money
//                    outString += "Dustin owes Cassie $" + values [0] + "<br />";
//                    outString += "Ryan owes Cassie $" + values [1] + "<br />";
//                }
//            }
//            else if (countNegs == 2) 
//            {
//                if (negativeLocs [0].ToString() == "p") { //Dustin owes people
//                    outString += "Dustin owes Ryan $" + values [1] * -1 + "<br />";
//                    outString += "Dustin owes Cassie $" + values [2] * -1 + "<br />";
//                } else if (negativeLocs [1].ToString() == "p") { //Ryan owes people
//                    outString += "Ryan owes Dustin $" + values [0] * -1 + "<br />";
//                    outString += "Ryan owes Cassie $" + values [2] * -1 + "<br />";
//                } else if (negativeLocs [2].ToString() == "p") { //Cassie owes people
//                    outString += "Cassie owes Dustin $" + values [0] * -1 + "<br />";
//                    outString += "Cassie owes Ryan $" + values [1] * -1 + "<br />";
//                }
//            }
//            return outString;
//        }
    }
}
