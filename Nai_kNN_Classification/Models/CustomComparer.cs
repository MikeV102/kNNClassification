using System.Collections;
using System.Collections.Generic;

namespace Nai_kNN_Classification.Models
{
    public class TestExampleComparer : IComparer<TestExample>
    {

        public int Compare(TestExample x, TestExample y)
        {
            return x.difference.CompareTo(y.difference);
        }
    }
}