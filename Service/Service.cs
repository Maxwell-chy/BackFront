
using Model;
using Repository;

namespace Service
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
    public class ScoreService : BaseService<Score>
    {
        private readonly ScoreRepository _ScoreRepository;
        public ScoreService(ScoreRepository scoreRepository)
        {
            base.baseRepository = scoreRepository;
            _ScoreRepository = scoreRepository;
        }
    }

}