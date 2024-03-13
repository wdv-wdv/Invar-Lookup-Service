using backend.Models;
using System.Threading.Tasks;

namespace backend.Interfaces
{
    public interface IMergeService
    {
        public Task<CreditData> MergeCreditData(string ssn);
    }
}