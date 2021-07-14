using SqlSugar;
namespace Model
{
    public class StuComp
    {
        public string id { get; set; }
        public string compname { get; set; }
        public string signstatus { get; set; }

        [SugarColumn(IsIgnore = true)]
        public CompInfo compInfo { get; set; }
    }
}
