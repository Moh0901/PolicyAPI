using Microsoft.EntityFrameworkCore;
using POlidvyAPI.Model;
using POlidvyAPI.Services;
using POlidvyAPI.ViewModels;

namespace POlidvyAPI.Repository
{
    public class PolicyRepository : IPolicyRepository
    {
        private readonly PolicyMDBContext _context;
        private readonly IEmailService emailService;

        public PolicyRepository(PolicyMDBContext context, IEmailService emailService)
        {
            _context = context;
            this.emailService = emailService;
        }

        public PolicyTbl AddNewPolicy(PolicyViewModel policyTbl)
        {
           PolicyTbl poilicy = new PolicyTbl();

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

            _context.PolicyTbls.Add(poilicy);

              _context.SaveChanges();

            /*To get Policy Type Code*/
            string typeCode = getPolicyTypeShortCode(poilicy.PolicyTypeId);
            int yearCode = poilicy.PolicyStartDate.Year;
            string idCode = poilicy.PolicyId.ToString("D4");

            string PolicyCode = typeCode + "-" + yearCode + "-" + idCode;

            poilicy.PolicyCode = PolicyCode;

            _context.PolicyTbls.Update(poilicy);

             _context.SaveChangesAsync();

            /*To get Maturity Amount*/

            var maturityAmount =  getMaturityAmount(policyTbl);


            Console.WriteLine("--------------------Maturity Amount--------------------------");
            Console.WriteLine(maturityAmount);
            Console.WriteLine("----------------------------------------------");
            //_maturityAmount.BuildMaturityAmount();
            //Console.WriteLine(policyTbl.MaturityAmount);
            //_context.PolicyTbls.Update(PolicyTbl);

            /*To get End Date of policy*/
            var endDate =  getEndDate(policyTbl);
            Console.WriteLine("--------------------End Date--------------------------");
            Console.WriteLine(endDate);
            Console.WriteLine("----------------------------------------------");

            Console.WriteLine("-------------------Policy Code---------------------------");
            Console.WriteLine(PolicyCode);
            Console.WriteLine("----------------------------------------------");

            /* string typeName = getPolicyTypeName(poilicy.PolicyTypeId);

             Console.WriteLine("-------------------------------------------");
             Console.WriteLine(typeName);
             Console.WriteLine("-------------------------------------------");*/

            EmailViewModel request = new EmailViewModel();
            request.ToEmailId = "mohitverma.540.mv@gmail.com";
            request.EmailSubject = " Policy Registered Successfully";
            request.EmailBody = "<html><body> <p> Hi Admin, </p> " +
                 "<p> The policy is successfully registered. </p>" +
                $"<p> The policy {PolicyCode} is available to the users from {poilicy.PolicyStartDate} to {endDate}. </p>" +
                $"<p> This is the {poilicy.PolicyId}<sup>th</sup> policy in the {poilicy.PolicyType.PolicyTypeName}. </p>" +
                 "<p> To add more Click Policy Registration. </p>  </body></html>";

             emailService.SendEmail(request);

            return poilicy;
        }

        public List<PolicyTbl> GetAllPolicies()
        {
            var policyList =  _context.PolicyTbls.Include(x => x.PolicyType).Include(y => y.UserType).ToList();

            return policyList;
        }

        public List<PolicyTbl> SearchPolicy(SearchViewModel searchViewModel)
        {
            //PolicyTbl searchList2 = new PolicyTbl();
            List<PolicyTbl> myListSearch = new List<PolicyTbl>();

            if (!string.IsNullOrWhiteSpace(searchViewModel.PolicyName))
            {
                myListSearch =  _context.PolicyTbls.Include(y => y.PolicyType).Where(x => x.PolicyName.ToLower().Contains(searchViewModel.PolicyName.ToLower())).ToList();
            }

            if (searchViewModel.PolicyId > 0 && myListSearch != null && myListSearch.Any())
            {
                myListSearch = myListSearch.Where(x => x.PolicyId == searchViewModel.PolicyId).ToList();
            }
            else if (searchViewModel.PolicyId > 0 && myListSearch != null && myListSearch.Count() == 0)
            {
                myListSearch =  _context.PolicyTbls.Include(y => y.PolicyType).Where(x => x.PolicyId == searchViewModel.PolicyId).ToList();
            }

            if (searchViewModel.NumberOfYears > 0 && myListSearch != null && myListSearch.Any())
            {
                myListSearch = myListSearch.Where(x => x.PolicyDuration == searchViewModel.NumberOfYears).ToList();
            }
            else if (searchViewModel.NumberOfYears > 0 && myListSearch != null && myListSearch.Count() == 0)
            {
                myListSearch =  _context.PolicyTbls.Include(y => y.PolicyType).Where(x => x.PolicyDuration == searchViewModel.NumberOfYears).ToList();
            }

            if (!string.IsNullOrWhiteSpace(searchViewModel.PolicyCompany) && myListSearch.Any())
            {
                myListSearch = myListSearch.Where(x => x.PolicyCompany.ToLower().Contains(searchViewModel.PolicyCompany.ToLower())).ToList();
            }
            else if (searchViewModel.PolicyCompany != null && myListSearch.Count() == 0)
            {
                myListSearch =  _context.PolicyTbls.Include(y => y.PolicyType).Where(x => x.PolicyCompany.ToLower().Contains(searchViewModel.PolicyCompany.ToLower())).ToList();
            }

            if (!string.IsNullOrEmpty(searchViewModel.PolicyTypeName) && myListSearch.Any())
            {
                myListSearch = myListSearch.Where(x => x.PolicyType.PolicyTypeName.ToLower().Contains(searchViewModel.PolicyTypeName.ToLower())).ToList();
            }
            else if (searchViewModel.PolicyTypeName != null && myListSearch.Count() == 0)
            {
                
               myListSearch = _context.PolicyTbls.Include(y => y.PolicyType).Where(x =>
                                x.PolicyType.PolicyTypeName.ToLower().Contains(searchViewModel.PolicyTypeName.ToLower())
                               ).ToList();
            }

            var list = myListSearch;
            return list;
        }

        private DateTime getEndDate(PolicyViewModel policyViewModel)
        {
            /*var year = policyViewModel.PolicyStartDate.Year;
            var month = policyViewModel.PolicyStartDate.Month;
            var day = policyViewModel.PolicyStartDate.Day;*/
            var duration = policyViewModel.PolicyDuration;

            var startDate = DateTime.Now;
            //var startDate = policyViewModel.PolicyStartDate;

            var endDate = startDate.AddYears((int)duration);

            /*  var endyear = year + duration;

              var sendDate = endyear + "/" + month + "/" + day;*/

            Console.WriteLine("----------------------------------------------");
            /* Console.WriteLine($"{year} +{duration}");
             Console.WriteLine($"{endyear}/{month}/{day}");*/
            Console.WriteLine(endDate);
            Console.WriteLine("----------------------------------------------");

            return endDate;
        }

        private string getPolicyTypeShortCode(int? typeId)
        {
            var PolicyType =  _context.PolicyTypeTbls.FirstOrDefault(e => e.PolicyTypeId == typeId);

            return PolicyType.PolicyTypeCode;
        }

        private double getMaturityAmount(PolicyViewModel policyViewModel)
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

    }
}
