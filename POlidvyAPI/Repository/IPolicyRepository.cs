using POlidvyAPI.Model;
using POlidvyAPI.ViewModels;

namespace POlidvyAPI.Repository
{
    public interface IPolicyRepository
    {
        List<PolicyTbl> GetAllPolicies();

        PolicyTbl GetPolicyById(int id);

        PolicyTbl AddNewPolicy(PolicyViewModel policyViewModel);

        List<PolicyTbl> SearchPolicy(SearchViewModel searchViewModel);
    }
}
