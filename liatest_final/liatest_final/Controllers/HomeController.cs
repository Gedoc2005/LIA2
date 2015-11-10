using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace liatest_final.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument document = web.Load("https://activetracing.dhl.com/DatPublic/search.do?search=consignmentId&autoSearch=true&l=sv&at=consignment&a=9545980527");
            //     HtmlNode: Gets a collection of flags that define specific behaviors for specific element nodes.
            var tdList = new List<HtmlNode>();
            // Select all tables and remove tags with HtmlDecode then add to the tdList.
            foreach (var cell in document.DocumentNode.SelectNodes("//table/tr/td"))
            {
                var decodedCell = WebUtility.HtmlDecode(cell.InnerHtml);
                if(!String.IsNullOrWhiteSpace(decodedCell))
                {
                tdList.Add(cell);
                }
            }
            // Debug, break point at ViewBag.Message to see the values of tdList, then select which information to show using a Dictionarylist with (Key,Value) as string
            // little editing with replace
            var returnDictionary = new Dictionary<string, string>();
            for (int i = 8; i < 22; i=i+2)
            {
                returnDictionary.Add(tdList.ElementAt(i).InnerHtml, tdList.ElementAt(i+1).InnerHtml.Replace(" ", ""));
            }
            return View(returnDictionary);
        }
    }
}