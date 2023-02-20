using Ganss.Xss;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Quill.Delta;
using QuillDemo.Models;
using System.Diagnostics;

namespace QuillDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;

        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var model = _context.Documents.First();

            var delta = JArray.Parse(model.DeltaJson);

            var htmlConverter = new HtmlConverter(delta);
            var html = htmlConverter.Convert();

            // it is unlikely that Quill will produce unsafe html via a delta object
            // but we should sanitize it just to be sure.
            var sanitizer = new HtmlSanitizer();

            sanitizer.AllowedAttributes.Add("class"); // to preserve the classes used to style quill delta as html

            var safeHtml = sanitizer.Sanitize(html);

            ViewBag.DeltaHtml = safeHtml;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult Edit()
        {
            var @document = _context.Documents.First();
            if (@document == null)
            {
                return NotFound();
            }

            return View(@document);
        }


        [HttpPost]
        public async Task<IActionResult> Edit([Bind("Id,DeltaJson")] Document document)
        {
            if (ModelState.IsValid)
            {

                _context.Update(document);
                await _context.SaveChangesAsync();
                
            } else
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}