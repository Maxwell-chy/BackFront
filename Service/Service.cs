
using StuManage.Model;
using StuManage.Repository;

namespace StuManage.Service
{
    public class CustomerService : BaseService<Customer>
    {
        private readonly CustomerRepository _CustomerRepository;
        public CustomerService(CustomerRepository CustomerRepository)
        {
            base.baseRepository = CustomerRepository;
            _CustomerRepository = CustomerRepository;
        }
    }

}