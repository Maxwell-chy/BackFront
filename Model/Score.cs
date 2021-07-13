using SqlSugar;
using System;

namespace StuManage.Model
{
    public class Score: IComparable<Score>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ExamName { get; set; }
        public float Chinese { get; set; }
        public float Math { get; set; }
        public float English { get; set; }
        public float Physics { get; set; }
        public float Chemistry { get; set; }
        public float Biology { get; set; }
        public float Politics { get; set; }
        public float History { get; set; }
        public float Geography { get; set; }
        public int CompareTo(Score other)
        {
            if (this.ExamName == other.ExamName)
                return 0;
            else if (this.ExamName.CompareTo(other.ExamName) > 0)
                return 1;
            else
                return -1;
        }
    }
}