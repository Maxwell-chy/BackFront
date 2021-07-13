using SqlSugar;
namespace StuManage.Model
{
    public class Competition
    {
        [SugarColumn(IsPrimaryKey = true)]
        public string Id { get; set; }
        public string Studentid { get; set; }
        public string competitionname { get; set; }
        public string detailInfo { get; set; }
        public string type { get; set; }
        public string Comnumber { get; set; }
        public string ddl_date { get; set; }
        public string ddl_time { get; set; }
        public string mg_state { get; set; }



    }
}