using System;
using System.Collections.Generic;

namespace Nai_kNN_Classification.Models
{
    public class TestExample
    {
        public TestExample()
        {
        }

        public TestExample(List<double> exampleList , string result)
        {
            this.exampleList = exampleList ?? throw new ArgumentNullException(nameof(exampleList));
            this.result = result ?? throw new ArgumentNullException(nameof(result));
        }

        public List<double> exampleList  = new List<double>();
        public string result { get; set; }

        public double difference { get; set; }
    }
}