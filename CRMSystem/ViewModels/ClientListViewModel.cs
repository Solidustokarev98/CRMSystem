using CRMSystem.Models;

namespace CRMSystem.ViewModels
{
    public class ClientListViewModel
    {
        public IEnumerable<Client> Clients { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}