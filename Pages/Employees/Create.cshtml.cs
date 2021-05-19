using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication1.Data;
using WebApplication1.DataTransmission;
using WebApplication1.Models;

namespace WebApplication1.Pages.Employees
{
    public class CreateModel : PageModel
    {
        private readonly RPCClient _rpcClient;

        public CreateModel(RPCClient rpcClient)
        {
            _rpcClient = rpcClient;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Employee Employee { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // _context.Employee.Add(Employee);
            // await _context.SaveChangesAsync();


            _rpcClient.Add(Employee);

            return RedirectToPage("./Index");
        }
    }
}
