using StuManage.Model;
using StuManage.Repository;

namespace StuManage.Service
{
    public class AnnounceService : BaseService<Announce>
    {
        private readonly AnnounceRepository _announceRepository;
        public AnnounceService(AnnounceRepository announceRepository)
        {
            base.baseRepository = announceRepository;
            _announceRepository = announceRepository;
        }
    }
    public class CompetitionService : BaseService<Competition>
    {
        private readonly CompetitionRepository _competitionRepository;
        public CompetitionService(CompetitionRepository competitionRepository)
        {
            base.baseRepository = competitionRepository;
            _competitionRepository = competitionRepository;
        }
    }
    public class StudentService : BaseService<Student>
    {
        private readonly StudentRepository _studentRepository;
        public StudentService(StudentRepository studentRepository)
        {
            base.baseRepository = studentRepository;
            _studentRepository = studentRepository;
        }
    }
    public class ScoreService : BaseService<Score>
    {
        private readonly ScoreRepository _scoreRepository;
        public ScoreService(ScoreRepository scoreRepository)
        {
            base.baseRepository = scoreRepository;
            _scoreRepository = scoreRepository;
        }
    }
    public class ExaminformationService : BaseService<Examinformation>
    {
        private readonly ExaminformationRepository _examinformationRepository;
        public ExaminformationService(ExaminformationRepository examinformationRepository)
        {
            base.baseRepository = examinformationRepository;
            _examinformationRepository = examinformationRepository;
        }
    }

    public class UserService : BaseService<User>
    {
        private readonly UserRepository _UserRepository;
        public UserService(UserRepository UserRepository)
        {
            base.baseRepository = UserRepository;
            _UserRepository = UserRepository;
        }
    }

}