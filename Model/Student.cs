using SqlSugar;
namespace StuManage.Model
{
    public class Student
    {
        [SugarColumn(IsPrimaryKey = true)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Year { get; set; }
        public int ClassId { get; set; }
    }
}
