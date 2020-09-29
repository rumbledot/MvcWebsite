using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcWebsite.Data;
using MvcWebsite.Models;

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

        public BoardsController(MvcWebsiteContext context)
        {
            _context = context;
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
