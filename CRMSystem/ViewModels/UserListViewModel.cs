using CRMSystem.Models;

namespace CRMSystem.ViewModels
{
    public class UserListViewModel
    {
        public IEnumerable<User> Users { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}