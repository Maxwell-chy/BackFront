using SqlSugar;
namespace StuManage.Model
{
    public class Customer
    {
        [SugarColumn(IsPrimaryKey = true)]
        public string id { get; set; }
        public string password { get; set; }
    }
}
