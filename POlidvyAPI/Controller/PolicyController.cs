using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POlidvyAPI.Model;
using POlidvyAPI.Model.ViewModel;

namespace POlidvyAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class PolicyController : ControllerBase
    {
        private readonly PolicyDBContext _context;

        public PolicyController(PolicyDBContext context)
        {
            _context = context;
        }

        // GET: api/Policy
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PolicyTbl>>> GetPolicyTbls()
        {
          if (_context.PolicyTbls == null)
          {
              return NotFound();
          }
            return await _context.PolicyTbls.Include(x=>x.PolicyType).Include(y=>y.UserType).ToListAsync();
        }

        // GET: api/Policy/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PolicyTbl>> GetPolicyTbl(int id)
        {
          if (_context.PolicyTbls == null)
          {
              return NotFound();
          }
            var policyTbl = await _context.PolicyTbls.Include(x => x.PolicyType).Include(y => y.UserType).FirstOrDefaultAsync(z => z.PolicyId == id);

            if (policyTbl == null)
            {
                return NotFound();
            }

            return policyTbl;
        }

        // PUT: api/Policy/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPolicyTbl(int id, PolicyTbl policyTbl)
        {
            if (id != policyTbl.PolicyId)
            {
                return BadRequest();
            }

            _context.Entry(policyTbl).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PolicyTblExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Policy
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostPolicyTbl(PolicyViewModel policyTbl)
        {
            PolicyTbl poilicy = new PolicyTbl();

            if (ModelState.IsValid)
            {
               
                poilicy.PolicyId = policyTbl.PolicyId;
                poilicy.PolicyName = policyTbl.PolicyName;
                poilicy.PolicyDuration = policyTbl.PolicyDuration;
                poilicy.PolicyStartDate = policyTbl.PolicyStartDate;
                poilicy.UserTypeId = policyTbl.UserTypeId;
                poilicy.PolicyTypeId = policyTbl.PolicyTypeId;
                poilicy.PolicyCompany = policyTbl.PolicyCompany;
                poilicy.PolicyAmount = policyTbl.PolicyAmount;
                poilicy.PolicyInitialDeposit = policyTbl.PolicyInitialDeposit;
                poilicy.PolicyInterest = policyTbl.PolicyInterest;
                poilicy.PolicyTermsPerYear = policyTbl.PolicyTermsPerYear;
                poilicy.PolicyCode = policyTbl.PolicyCode;

                await _context.PolicyTbls.AddAsync(poilicy);

                await _context.SaveChangesAsync();

                string CODE = await getPolicyTypeShortCode(poilicy.PolicyTypeId);

                string PolicyCode = CODE+"-" + poilicy.PolicyStartDate.Year + "-"+ poilicy.PolicyId.ToString("D4");

                poilicy.PolicyCode = PolicyCode;

                 _context.PolicyTbls.Update(poilicy);

                await _context.SaveChangesAsync();


                Console.WriteLine("----------------------------------------------");
                Console.WriteLine(PolicyCode);
                Console.WriteLine("----------------------------------------------");
                
            }

            return Ok(poilicy);
           // return CreatedAtAction("GetPolicyTbl", new { id = policyTbl.PolicyId }, policyTbl);
        }

        // DELETE: api/Policy/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePolicyTbl(int id)
        {
            if (_context.PolicyTbls == null)
            {
                return NotFound();
            }
            var policyTbl = await _context.PolicyTbls.FindAsync(id);
            if (policyTbl == null)
            {
                return NotFound();
            }

            _context.PolicyTbls.Remove(policyTbl);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PolicyTblExists(int id)
        {
            return (_context.PolicyTbls?.Any(e => e.PolicyId == id)).GetValueOrDefault();
        }

        private async Task<string> getPolicyTypeShortCode(int typeId)
        {
             var PolicyType = await _context.PolicyTypeTbls.FirstOrDefaultAsync(e => e.PolicyTypeId == typeId);

            return PolicyType.PolicyTypeCode;
        }
    }
}
