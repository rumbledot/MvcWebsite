using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcWebsite.Data;
using MvcWebsite.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

namespace MvcWebsite.Controllers
{
    public class BoardsController : Controller
    {
        public const string PEG_WHITE = "white";
        public const string PEG_YELLOW = "yellow";
        public const string PEG_GREEN = "green";
        public const string PEG_BLUE = "blue";
        public const string PEG_PINK = "pink";

        private readonly MvcWebsiteContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public BoardsController(MvcWebsiteContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: Boards
        public async Task<IActionResult> Index()
        {
            return View(await _context.Board.ToListAsync());
        }

        // GET: Boards/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var board = await _context.Board
                .FirstOrDefaultAsync(m => m.Id == id);
            if (board == null)
            {
                return NotFound();
            }

            IEnumerable<Stiky> stikies = from s in _context.Stiky
                          where s.BoardId==board.Id
                          select s;

            ViewData["board"] = board;
            ViewData["stikies"] = stikies;

            return View();
        }

        public async Task<IActionResult> GetStikies(int Id) {
            IEnumerable<Stiky> stikies = from s in _context.Stiky
                                         where s.BoardId == Id
                                         select s;

            JsonResult res = new JsonResult(stikies);

            return (res);
        }

        // POST: Boards/NewStiky
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NewStiky([FromBody] Stiky stiky)
        {
            string text = stiky.Text;
            if (!string.IsNullOrEmpty(text) && text.Length > 4 && text.Length < 255)
            {
                Stiky newS = new Stiky()
                {
                    Type = "text",
                    Text = stiky.Text,
                    BoardId = stiky.BoardId
                };

                _context.Add(newS);
                await _context.SaveChangesAsync();
            };

            IEnumerable<Stiky> stikies = from s in _context.Stiky
                                             where s.BoardId == stiky.BoardId
                                             select s;

            JsonResult res = new JsonResult(stikies);

            return (res);
        }

        // POST: Boards/NewStikyImage
        [HttpPost]
        public async Task<IActionResult> NewStikyImage(IFormFile newStikyPic)
        {
            string webRootPath = _hostingEnvironment.WebRootPath;
            string contentRootPath = _hostingEnvironment.ContentRootPath;
            Console.WriteLine("FILE : " + newStikyPic);
            Console.WriteLine("WebRootPath : " + webRootPath);
            Console.WriteLine("WebRootPath : " + contentRootPath);

            if (ModelState.IsValid)
            {
                string[] AllowedFileExtensions = new string[] { ".jpg", ".gif", ".png", ".jpeg" };

                if (!AllowedFileExtensions.Contains(newStikyPic.FileName.Substring(newStikyPic.FileName.LastIndexOf('.'))))
                {
                    ModelState.AddModelError("File", "Please file of type: " + string.Join(", ", AllowedFileExtensions));
                }
                Console.WriteLine("PAST THE EXTENSION IF BLOCK");

                //string webRootPath = _hostingEnvironment.WebRootPath;
                //string contentRootPath = _hostingEnvironment.ContentRootPath;

                var fileName = Path.GetFileName(newStikyPic.FileName);
                var path = Path.Combine(webRootPath, "uploaded", fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await newStikyPic.CopyToAsync(fileStream);
                }
                ModelState.Clear();
                ViewBag.Message = "File uploaded successfully";

            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Boards/Create
        public IActionResult Create()
        {
            var vm = new BoardColorSelectorViewModel();
            vm.NewBoard = new Board()
            {
                BoardColor = "white"
            };
            return View(vm);
        }

        // POST: Boards/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NewBoard.Id,NewBoard.Title,NewBoard.Text,NewBoard.Tags,NewBoard.BoardColor")] BoardColorSelectorViewModel vm)
        {
            if (ModelState.IsValid)
            {
                Board board = new Board()
                {
                    Title = HttpContext.Request.Form["NewBoard.Title"].ToString(),
                    Text = HttpContext.Request.Form["NewBoard.Text"].ToString(),
                    Tags = HttpContext.Request.Form["NewBoard.Tags"].ToString(),
                    BoardColor = HttpContext.Request.Form["NewBoard.BoardColor"].ToString(),
                };

                _context.Add(board);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        // GET: Boards/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var board = await _context.Board.FindAsync(id);
            if (board == null)
            {
                return NotFound();
            }

            var vm = new BoardColorSelectorViewModel();
            vm.NewBoard = board;

            return View(vm);
        }

        // POST: Boards/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NewBoard.Id,NewBoard.Title,NewBoard.Text,NewBoard.Tags,NewBoard.BoardColor")] BoardColorSelectorViewModel vm)
        {
            var editedID = Convert.ToInt32(HttpContext.Request.Form["NewBoard.Id"]);

            if (id != editedID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Board board = new Board()
                    {
                        Id = editedID,
                        Title = HttpContext.Request.Form["NewBoard.Title"].ToString(),
                        Text = HttpContext.Request.Form["NewBoard.Text"].ToString(),
                        Tags = HttpContext.Request.Form["NewBoard.Tags"].ToString(),
                        BoardColor = HttpContext.Request.Form["NewBoard.BoardColor"].ToString(),
                    };

                    _context.Update(board);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BoardExists(editedID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        // GET: Boards/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var board = await _context.Board
                .FirstOrDefaultAsync(m => m.Id == id);
            if (board == null)
            {
                return NotFound();
            }

            return View(board);
        }

        // POST: Boards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var board = await _context.Board.FindAsync(id);
            _context.Board.Remove(board);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BoardExists(int id)
        {
            return _context.Board.Any(e => e.Id == id);
        }
    }
}
