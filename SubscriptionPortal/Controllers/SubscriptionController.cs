using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.PowerShell;
using SubscriptionPortal.Models;

namespace SubscriptionPortal.Controllers
{
    public class SubscriptionController : Controller
    {
        private readonly SubscriptionPortalContext _context;
        //public string ApplicationName { get; set; }
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
                subscription.RequestStatus = "Pending";
                subscription.Userid = ViewBag.userid;
                subscription.Userid = TempData["Userid"].ToString();
                subscription.Userid = HttpContext.Session.GetString("userid");

                _context.Add(subscription);
                await _context.SaveChangesAsync();

                ////Thread.Sleep(5000);

                //string containername = "mysqldb" + subscription.CompanyName.Trim();
                //string podname = "mysqldb" + subscription.CompanyName.Trim();
                //string dbname = "mysqldb" + subscription.CompanyName.Trim();
                //string dbusername = "dbusername";
                //string dbpassword = "dbusername";
                //string rootuser = "root";
                //string rootpassword = "dbpassword";

               // string appname = subscription.ApplicationName.Trim().ToString().ToLower().Replace(' ', '-') + '-' + subscription.CompanyName.Trim();

               // List<object> output =  RunPowershellScript(containername, podname, dbname, dbusername, dbpassword, rootuser, rootpassword, appname);
              //  ViewBag.error = output[0];
               // ViewBag.results = output[1];
              return RedirectToAction(nameof(Index));
            }
            
            return View(subscription);
        }
        public List<object> RunPowershellScript(string containername, string podname, string dbname, string dbusername, string dbpassword, string rootuser, string rootpassword, string appname)
        {
            string fullPath = Path.GetFullPath("Files/0.mysqlscript.ps1");
            //string fileName = Path.GetFileName("Files/0.mysqlscript.ps1");

            //using (RunspaceInvoke invoker = new RunspaceInvoke())
            //{
            //    invoker.Invoke("Set-ExecutionPolicy Unrestricted");
            //}

            //StringBuilder OSScript = new StringBuilder("Set-ExecutionPolicy -Scope Process -ExecutionPolicy Unrestricted;");
            //OSScript.Append(@"other really exciting stuff");

            string cmdArg = fullPath;
            Runspace runspace = RunspaceFactory.CreateRunspace();
            //runspace.ApartmentState = System.Threading.ApartmentState.STA;
            runspace.ThreadOptions = PSThreadOptions.UseCurrentThread;

            runspace.Open();

            Pipeline pipeline = runspace.CreatePipeline();
            //pipeline.Commands.Add("Set-ExecutionPolicy");

            Command command = new Command(cmdArg);

            CommandParameter usernameParm = new CommandParameter("username", "mujoshi1");
            command.Parameters.Add(usernameParm);
            CommandParameter userpasswordParm = new CommandParameter("userpassword", "Welcome@IBM9535084436s");
            command.Parameters.Add(userpasswordParm);
            CommandParameter serverlinkParm = new CommandParameter("serverlink", "https://api.sandbox.x8i5.p1.openshiftapps.com:6443");
            command.Parameters.Add(serverlinkParm);
            CommandParameter servertokenParm = new CommandParameter("servertoken", "sha256~dCDKlZ5DVXxVub8nYlQGTn3pX5cLFHnDOWNXph7mmIo");
            command.Parameters.Add(servertokenParm);
            CommandParameter containernameParm = new CommandParameter("containername", containername);
            command.Parameters.Add(containernameParm);
            CommandParameter podnameParm = new CommandParameter("podname", podname);
            command.Parameters.Add(podnameParm);
            CommandParameter dbnameParm = new CommandParameter("dbname", dbname);
            command.Parameters.Add(dbnameParm);
            CommandParameter dbusernameParm = new CommandParameter("dbusername", dbusername);
            command.Parameters.Add(dbusernameParm);
            CommandParameter dbpasswordParm = new CommandParameter("dbpassword", dbpassword);
            command.Parameters.Add(dbpasswordParm);
            CommandParameter rootuserParm = new CommandParameter("rootuser", rootuser);
            command.Parameters.Add(rootuserParm);
            CommandParameter rootpasswordParm = new CommandParameter("rootpassword", rootpassword);
            command.Parameters.Add(rootpasswordParm);
            CommandParameter envnameParm = new CommandParameter("envname", "Development");
            command.Parameters.Add(envnameParm);
            CommandParameter appnameParm = new CommandParameter("appname", appname);
            command.Parameters.Add(appnameParm);

            pipeline.Commands.Add(command);
            //pipeline.Commands.AddScript(cmdArg);

            pipeline.Commands[0].MergeMyResults(PipelineResultTypes.Error, PipelineResultTypes.Output);
            Collection<PSObject> results = pipeline.Invoke("Set-ExecutionPolicy -Scope CurrentUser -ExecutionPolicy RemoteSigned");
            var error = pipeline.Error.ReadToEnd();
            runspace.Close();

            if (error.Count >= 1)
            {
                string errors = "";
                foreach (var Error in error)
                {
                    errors = errors + " " + Error.ToString();
                }
            }
            if (results != null)
            {
            }


            ////Execute PS1(PowerShell script) file
            //using (PowerShell PowerShellInst = PowerShell.Create())
            //{
            //    //string path = Path.GetFullPath("~/0.mysqlscript.ps1");
            //    //string path = System.IO.Path.GetDirectoryName(@"C:\Temp\") + "\\Get-EventLog.ps1";
            //   // path = System.IO.Path.GetDirectoryName(@"C:\Temp\") + "\\0.mysqlscript.ps1";
            //    if (!string.IsNullOrEmpty(fullPath))
            //    {
            //        PowerShellInst.AddScript(System.IO.File.ReadAllText(fullPath));
            //        PowerShellInst.AddParameter("username", "mujoshi1");
            //        PowerShellInst.AddParameter("userpassword", "Welcome@IBM9535084436s");
            //        PowerShellInst.AddParameter("serverlink", "https://api.sandbox.x8i5.p1.openshiftapps.com:6443");
            //        PowerShellInst.AddParameter("servertoken", "sha256~dCDKlZ5DVXxVub8nYlQGTn3pX5cLFHnDOWNXph7mmIo");

            //        PowerShellInst.AddParameter("containername", containername);
            //        PowerShellInst.AddParameter("podname", podname);
            //        PowerShellInst.AddParameter("dbname", dbname);
            //        PowerShellInst.AddParameter("dbusername", dbusername);
            //        PowerShellInst.AddParameter("dbpassword", dbpassword);
            //        PowerShellInst.AddParameter("rootuser", rootuser);
            //        PowerShellInst.AddParameter("rootpassword", rootpassword);

            //        PowerShellInst.AddParameter("envname", "Development");
            //        PowerShellInst.AddParameter("appname", appname);

            //        //PowerShellInst.AddParameter("ErrorAction", "Stop");                    
            //        PowerShellInst.AddCommand("Set-ExecutionPolicy");
            //        PowerShellInst.AddParameter("ExecutionPolicy", "unrestricted");
            //    }

            //    Collection<PSObject> PSOutput = PowerShellInst.Invoke("Set-ExecutionPolicy -Scope CurrentUser -ExecutionPolicy RemoteSigned");
            //    foreach (PSObject obj in PSOutput)
            //    {                   
            //        if (obj != null)
            //        {
            //        }

            //        if (obj != null)
            //        {
            //            Console.Write(obj.Properties["EntryType"].Value.ToString() + " - ");
            //            Console.Write(obj.Properties["Source"].Value.ToString() + " - ");
            //            Console.WriteLine(obj.Properties["Message"].Value.ToString() + " - ");
            //        }
            //    }
            //    Console.WriteLine("Done");
            //  Console.Read();
            //}
            List<object> output = new List<object>();
            output.Add(error);
            output.Add(results);
            return output;
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
