using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SubscriptionPortal.Models;

namespace SubscriptionPortal.Controllers
{
    public class SubscriptionController : Controller
    {
        private readonly SubscriptionPortalContext _context;
        public string ApplicationName { get; set; }
        public SubscriptionController(SubscriptionPortalContext context)
        {
            _context = context;
        }
       
        // GET: Subscription
        public async Task<IActionResult> Index()
        {
            List<SelectListItem> ApplicationNames = new List<SelectListItem>
            {
                new SelectListItem { Value = "SelectApplication", Text = "Select Application" },
                new SelectListItem { Value = "GiftRegister", Text = "Gift Register" },
                new SelectListItem { Value = "MeetingExpanse", Text = "Meeting Expanse" },
                new SelectListItem { Value = "ManPower", Text = "Man Power" },
                new SelectListItem { Value = "NewHire", Text = "New Hire"},
            };

            //assigning SelectListItem to view Bag
            ViewBag.ApplicationName = ApplicationNames;

            var subscription = from m in _context.Subscription
                               select m;

            if (!String.IsNullOrEmpty(HttpContext.Session.GetString("userid")))
            {
                subscription = subscription.Where(s => s.Userid.Contains(HttpContext.Session.GetString("userid")));
            }

            return View(await subscription.ToListAsync());

            // return View(await _context.Subscription.ToListAsync());
            //List<Subscription> subscription = new List<Subscription>();
            //subscription = await _context.Subscription.AsNoTracking()
            //    .Include(m => m.Userid == HttpContext.Session.GetString("userid"))
            //    //.Include(x => x.Region)
            //    .ToListAsync();

            //return View(await _context.Subscription.FirstOrDefaultAsync(m => m.Userid == HttpContext.Session.GetString("userid")));
            ////var  subscription = await _context.Subscription
            ////    .FirstOrDefaultAsync(m => m.Userid == HttpContext.Session.GetString("userid"));

            ////if (subscription == null)
            ////{
            ////    return View(await subscription.ToListAsync());
            ////}

            ////return View(subscription);

        }

        // GET: Subscription/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subscription = await _context.Subscription
                .FirstOrDefaultAsync(m => m.SubscriptionId == id || m.Userid == HttpContext.Session.GetString("userid") );
            if (subscription == null)
            {
                return NotFound();
            }

            return View(subscription);
        }

        // GET: Subscription/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Subscription/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SubscriptionId,ApplicationName,CompanyName,DBName,DBUserId,DBUserPassword,EmailId,Location,Userid")] Subscription subscription)
        {
            if (ModelState.IsValid)
            {
                subscription.Userid = ViewBag.userid;
                //subscription.Userid = TempData["Userid"].ToString();
                subscription.Userid = HttpContext.Session.GetString("userid");

                _context.Add(subscription);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(subscription);
        }

        // GET: Subscription/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subscription = await _context.Subscription.FindAsync(id);
            if (subscription == null)
            {
                return NotFound();
            }
            return View(subscription);
        }

        // POST: Subscription/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SubscriptionId,ApplicationName,CompanyName,DBName,DBUserId,DBUserPassword,EmailId,Location")] Subscription subscription)
        {
            if (id != subscription.SubscriptionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subscription);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubscriptionExists(subscription.SubscriptionId))
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
            return View(subscription);
        }

        // GET: Subscription/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subscription = await _context.Subscription
                .FirstOrDefaultAsync(m => m.SubscriptionId == id);
            if (subscription == null)
            {
                return NotFound();
            }

            return View(subscription);
        }

        // POST: Subscription/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var subscription = await _context.Subscription.FindAsync(id);
            _context.Subscription.Remove(subscription);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubscriptionExists(int id)
        {
            return _context.Subscription.Any(e => e.SubscriptionId == id);
        }
    }
}
