using System;

namespace Model
{
    public class Score:IComparable<Score>
    {
        public string id { get; set; }
        public string examid { get; set; }
        public string name { get; set; }
        public float chinese { get; set; }
        public float math { get; set; }
        public float english { get; set; }
        public float physics { get; set; }
        public float chemistry { get; set; }
        public float biology { get; set; }
        public float politics { get; set; }
        public float history { get; set; }
        public float geography { get; set; }
        public int CompareTo(Score other)
        {
            if (this.examid == other.examid)
                return 0;
            else if (this.examid.CompareTo(other.examid) > 0)
                return -1;
            else
                return 1;
        }
    }
}