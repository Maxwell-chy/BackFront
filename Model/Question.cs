using SqlSugar;
using System;

namespace Model
{
    public class Question:IComparable<Question>
    {
        public string tilte { get; set; }
        public string issueDate { get; set; }
        public int CompareTo(Question other)
        {
            if (this.issueDate == other.issueDate)
                return 0;
            else if (this.issueDate.CompareTo(other.issueDate) < 0)
                return 1;
            else
                return -1;
        }
    }
}
