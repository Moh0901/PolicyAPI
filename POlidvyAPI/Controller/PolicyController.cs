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
        private object myList;

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
            var policyList = await _context.PolicyTbls.Include(x => x.PolicyType).Include(y => y.UserType).ToListAsync();

            return Ok(policyList);
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
        /*
                [HttpGet("Search")]

                public async Task<IActionResult> SearchPolicy([FromQuery] SearchViewModel searchViewModel)
                {
                    //PolicyTbl searchList2 = new PolicyTbl();

                    try
                    {
                        var searchlist = await _context.PolicyTbls.Include(y=>y.PolicyType)
                        .Where(
                             x => x.PolicyName.ToLower().Contains(searchViewModel.PolicyName.ToLower())
                          || x.PolicyId.ToString().Equals(searchViewModel.PolicyId.ToString())
                          || x.PolicyCompany.ToLower().Contains(searchViewModel.PolicyCompany.ToLower())
                          || x.PolicyDuration.ToString().Equals(searchViewModel.PolicyDuration.ToString())
                          || x.PolicyType.PolicyTypeName.ToLower().Contains(searchViewModel.PolicyTypeName.ToLower())
                             ).ToListAsync();
                        return Ok(searchlist);
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.ToString());
                    }
                }*/

        [HttpGet("Search")]

        public async Task<IActionResult> SearchPolicy(string name)
        {

            var list = await _context.PolicyTbls.Include(x => x.PolicyType)
                .Where(x => x.PolicyType.PolicyTypeName.ToLower().Contains(name.ToLower())).ToListAsync();
            return Ok(list);
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

                /*To get Policy Type Code*/
                string typeCode = await getPolicyTypeShortCode(poilicy.PolicyTypeId);
                int yearCode = poilicy.PolicyStartDate.Year;
                string idCode = poilicy.PolicyId.ToString("D4");

                string PolicyCode = typeCode + "-" + yearCode + "-" + idCode;

                poilicy.PolicyCode = PolicyCode;

                _context.PolicyTbls.Update(poilicy);

                await _context.SaveChangesAsync();

                /*To get Maturity Amount*/

                var maturityAmount = await getMaturityAmount(policyTbl);

                // await _context.SaveChangesAsync();

                Console.WriteLine("--------------------Maturity Amount--------------------------");
                Console.WriteLine(maturityAmount);
                Console.WriteLine("----------------------------------------------");
                //_maturityAmount.BuildMaturityAmount();
                //Console.WriteLine(policyTbl.MaturityAmount);
                //_context.PolicyTbls.Update(PolicyTbl);

                /*To get End Date of policy*/
                var endDate = await getEndDate(policyTbl);
                Console.WriteLine("--------------------End Date--------------------------");
                Console.WriteLine(endDate);
                Console.WriteLine("----------------------------------------------");

                Console.WriteLine("-------------------Policy Code---------------------------");
                Console.WriteLine(PolicyCode);
                Console.WriteLine("----------------------------------------------");

                string typeName = await getPolicyTypeName(poilicy.PolicyTypeId);

                Console.WriteLine("-------------------------------------------");
                Console.WriteLine(typeName);
                Console.WriteLine("-------------------------------------------");
            }
            return Ok(poilicy);
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

        private async Task<string> getPolicyTypeName(int typeid)
        {
            var name = await _context.PolicyTypeTbls.FirstOrDefaultAsync(z => z.PolicyTypeId == typeid);

            return name == null ? string.Empty : name.PolicyTypeName;
        }

        private async Task<double> getMaturityAmount(PolicyViewModel policyViewModel)
        {
            var MaturityAmount = (Convert.ToDouble(policyViewModel.PolicyInitialDeposit))
                + (Convert.ToDouble(policyViewModel.PolicyDuration) * Convert.ToDouble(policyViewModel.PolicyTermsPerYear) * Convert.ToDouble(policyViewModel.PolicyAmount))
                + ((Convert.ToDouble(policyViewModel.PolicyDuration) * Convert.ToDouble(policyViewModel.PolicyTermsPerYear) * Convert.ToDouble(policyViewModel.PolicyAmount))
                * (Convert.ToDouble(policyViewModel.PolicyInterest) / 100));


            Console.WriteLine("-------------------------------------------");
            Console.WriteLine($"({policyViewModel.PolicyInitialDeposit}) +  ({policyViewModel.PolicyDuration} * {policyViewModel.PolicyTermsPerYear} * {policyViewModel.PolicyAmount})  + (({policyViewModel.PolicyDuration} * {policyViewModel.PolicyTermsPerYear} * {policyViewModel.PolicyAmount}) * ({policyViewModel.PolicyInterest} /100))");
            Console.WriteLine("-------------------------------------------");

            return MaturityAmount;
        }

        private async Task<DateTime> getEndDate(PolicyViewModel policyViewModel)
        {
            /*var year = policyViewModel.PolicyStartDate.Year;
            var month = policyViewModel.PolicyStartDate.Month;
            var day = policyViewModel.PolicyStartDate.Day;*/
            var duration = policyViewModel.PolicyDuration;

            var startDate = DateTime.Now;
            //var startDate = policyViewModel.PolicyStartDate;

            var endDate = startDate.AddYears(duration);

            /*  var endyear = year + duration;

              var sendDate = endyear + "/" + month + "/" + day;*/

            Console.WriteLine("----------------------------------------------");
            /* Console.WriteLine($"{year} +{duration}");
             Console.WriteLine($"{endyear}/{month}/{day}");*/
            Console.WriteLine(endDate);
            Console.WriteLine("----------------------------------------------");

            return endDate;
        }
    }
}
