using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.DataTransmission;
using WebApplication1.Models;

namespace WebApplication1.Pages.Employees
{
    public class EditModel : PageModel
    {
        private readonly RPCClient _rpcClient;
        public EditModel(RPCClient rpcClient)
        {
            _rpcClient = rpcClient;
        }

        [BindProperty]
        public Employee Employee { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //Employee = await _context.Employee.FirstOrDefaultAsync(m => m.ID == id);
            Employee = _rpcClient.Find<Employee>(id);
            if (Employee == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _rpcClient.update(Employee);
            //_context.Attach(Employee).State = EntityState.Modified;

            try
            {
                //await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(Employee.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool EmployeeExists(string id)
        {
            return true;
            //return _context.Employee.Any(e => e.ID == id);
        }
    }
}
