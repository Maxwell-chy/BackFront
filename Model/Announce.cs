using SqlSugar;
namespace StuManage.Model
{
    public class Announce
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}