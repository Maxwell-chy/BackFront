using SqlSugar;
namespace StuManage.Model
{
    public class User
    {
        [SugarColumn(IsPrimaryKey = true)]
        public string Id { get; set; }
        public string PassWord { get; set; }
    }
}
