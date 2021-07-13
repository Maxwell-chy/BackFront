using SqlSugar;
namespace StuManage.Model
{
    public class Examinformation
    {
        public string Studentid { get; set; }
        public string Examname { get; set; }
        
        public string ExSubject { get; set; }
        public string ExDate { get; set; }
        public string ExTime { get; set; }
        public string Place { get; set; }
        public string Seat { get; set; }
        public string Remarks { get; set; }


    }
}