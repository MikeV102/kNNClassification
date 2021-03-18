using System;

namespace Nai_kNN_Classification
{
    class Program
    {
        static void Main(string[] args)
        {
            Knn.perform_kNN(@"Data\\iris_training.txt", @"Data\iris_test.txt");
        }
    }
}