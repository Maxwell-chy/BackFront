﻿
using Model;
using Repository;

namespace Service
{
    public class AnnounceService : BaseService<Announce>
    {
        private readonly AnnounceRepository _AnnounceRepository;
        public AnnounceService(AnnounceRepository AnnounceRepository)
        {
            base.baseRepository = AnnounceRepository;
            _AnnounceRepository = AnnounceRepository;
        }
    }
    public class CustomerService : BaseService<Customer>
    {
        private readonly CustomerRepository _CustomerRepository;
        public CustomerService(CustomerRepository CustomerRepository)
        {
            base.baseRepository = CustomerRepository;
            _CustomerRepository = CustomerRepository;
        }
    }
    public class ExamInfoService : BaseService<ExamInfo>
    {
        private readonly ExamInfoRepository _ExamInfoRepository;
        public ExamInfoService(ExamInfoRepository ExamInfoRepository)
        {
            base.baseRepository = ExamInfoRepository;
            _ExamInfoRepository = ExamInfoRepository;
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
    public class StuExamService : BaseService<StuExam>
    {
        private readonly StuExamRepository _StuExamRepository;
        public StuExamService(StuExamRepository StuExamRepository)
        {
            base.baseRepository = StuExamRepository;
            _StuExamRepository = StuExamRepository;
        }
    }
}