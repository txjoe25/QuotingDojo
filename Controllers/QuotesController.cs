using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using QuotingDojo.Factories;
using QuotingDojo.Models;

namespace QuotingDojo.Controllers
{
    public class QuotesController : Controller
    {
        private QuoteFactory _quoteFactory;

        public QuotesController()
        {
            _quoteFactory = new QuoteFactory();
        }

        [HttpGet]
        [Route("")]
        public IActionResult Landing()
        {
            ViewBag.Errors = new List<string>();
            return View();
        }
        
        [HttpGet]
        [Route("quotes")]
        public IActionResult Index()
        {
            IEnumerable<Quote> AllQuotes = _quoteFactory.FindAll();
            ViewBag.Quotes = AllQuotes;
            return View();
        }

        [HttpPost]
        [Route("quotes")]
        public IActionResult New(Quote model)
        {
            if(ModelState.IsValid)
            {
                _quoteFactory.Add(model);
                return RedirectToAction("Index");
            }
            ViewBag.Errors = ModelState.Values;
            return View("Landing");
        }

        [HttpGet]
        [Route("like/{quoteId}")]
        public IActionResult Like(int quoteId)
        {
            Quote LikedQuote = _quoteFactory.GetQuoteById(quoteId);
            LikedQuote.Likes += 1;
            _quoteFactory.UpdateQuote(LikedQuote);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("update/{quoteId}")]
        public IActionResult Update(int quoteId)
        {
            ViewBag.Quote = _quoteFactory.GetQuoteById(quoteId);
            ViewBag.Errors = new List<string>();
            return View();
        }

        [HttpPost]
        [Route("update/{quoteId}")]
        public IActionResult UpdateQuote(Quote UpdateQuote)
        {
            if(ModelState.IsValid)
            {
                _quoteFactory.UpdateQuote(UpdateQuote);
                return RedirectToAction("Index");
            }
            ViewBag.Errors = ModelState.Values;
            return View("Update");
        }

        [HttpDelete]
        [Route("delete/{quoteId}")]
        public IActionResult Delete(int quoteId)
        {
            _quoteFactory.DeleteQuote(quoteId);
            return RedirectToAction("index");
        }
    }
}