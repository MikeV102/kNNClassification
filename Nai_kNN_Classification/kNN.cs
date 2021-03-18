using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using Nai_kNN_Classification.Models;

namespace Nai_kNN_Classification
{
    public class Knn
    {
        public static double Count { get; set; }
        public static uint parameterK { get; set; }
        static List<TestExample> _examples = new List<TestExample>();
        static List<TestExample> _tests = new List<TestExample>();
        static private List<TestExample> _differences = new List<TestExample>();
        public static void perform_kNN(string pathTraining, string pathTest)
        {
            Console.WriteLine("input parameter k: ");
            parameterK = UInt32.Parse(Console.ReadLine());
            System.Console.WriteLine("this is the parameter" + parameterK);

            var fileTraining = new FileInfo(pathTraining);
            var fileTest = new FileInfo(pathTest);

            int[] arrInts = new int[parameterK];
            string[] arrStrings = new string[parameterK];

            using (var streamTraining = new StreamReader(fileTraining.OpenRead()))
            {
                string trainingLine = null;
                while ((trainingLine = streamTraining.ReadLine()) != null)
                {
                    //TODO: handling document spacing
                    string[] trainingColumns = trainingLine.Split("   \t", StringSplitOptions.RemoveEmptyEntries);
                    TestExample example = new TestExample();
                    for (int i = 0; i < trainingColumns.Length - 1; i++)
                    {
                        example.exampleList.Add(Double.Parse(trainingColumns[i]));
                    }
                    example.result = trainingColumns[trainingColumns.Length - 1];

                    _examples.Add(example);
                }
            }

            using (var streamTest = new StreamReader(fileTest.OpenRead()))
            {
                string testLine = null;
                while ((testLine = streamTest.ReadLine()) != null)
                {
                    string[] testColumns = testLine.Split("   \t", StringSplitOptions.RemoveEmptyEntries);
                    TestExample test = new TestExample();
                    for (int i = 0; i < testColumns.Length - 1; i++)
                    {
                        test.exampleList.Add(Double.Parse(testColumns[i]));
                    }
                    test.result = testColumns[testColumns.Length - 1];
                    _tests.Add(test);
                }
            }

            for (int i = 0; i < _tests.Count; i++)
            {
                for (int x = 0; x < _examples.Count; x++)
                {
                    _examples[x].difference =
                        CalculateEuclideanDistance(_examples[x].exampleList, _tests[i].exampleList);
                }

                _examples.Sort(new TestExampleComparer());
                for (int z = 0; z < parameterK; z++)
                {
                    arrStrings[z] = _examples[z].result;
                }

                string MostCommon = arrStrings.GroupBy(v => v)
                    .OrderByDescending(g => g.Count())
                    .First()
                    .Key;
                Console.WriteLine(MostCommon);
                if (MostCommon.Equals(_tests[i].result.Trim()))
                {
                    Count++;
                    Console.WriteLine("correct");
                }
                else
                {
                    Console.WriteLine($"Incorrect! The correct result is: {_tests[i].result}");
                }

            }

            Console.WriteLine($"percentage correctness: {(Count / _tests.Count) * 100}%");

            while (true)
            {
                Console.WriteLine("type \"finish\" to finish or \"example\" to give another example");
                string userInput = Console.ReadLine();
                if ("finish".Equals(userInput))
                {
                    break;
                }
                else if ("example".Equals(userInput))
                {
                    Console.WriteLine("input new parameter k, integer");
                    var newParameterK = Int32.Parse(Console.ReadLine());

                    string[] newArrStrings = new string[newParameterK];
                    double[] coefficients = new double[4];

                    for (int i = 0; i < coefficients.Length; i++)
                    {
                        Console.WriteLine($"input coefficient number: {i + 1} as a double example(2,5 or 0,3)");
                        coefficients[i] = Double.Parse(Console.ReadLine());
                    }

                    for (int x = 0; x < _examples.Count; x++)
                    {
                        _examples[x].difference =
                            CalculateEuclideanDistance(_examples[x].exampleList, coefficients);
                    }

                    _examples.Sort(new TestExampleComparer());
                    for (int z = 0; z < newParameterK; z++)
                    {
                        newArrStrings[z] = _examples[z].result;
                    }

                    string NewMostCommon = newArrStrings.GroupBy(v => v)
                        .OrderByDescending(g => g.Count())
                        .First()
                        .Key;
                    Console.WriteLine(NewMostCommon);
                }
                else
                {
                    Console.WriteLine("wrong input, try again...");
                }

            }



        }



        public static double CalculateEuclideanDistance(List<double> exampleValues, List<double> testValues)
        {
            double distance;
            double sumOfSquareDiffernces = 0.0;
            for (int i = 0; i < exampleValues.Count; i++)
            {
                sumOfSquareDiffernces += Math.Pow((exampleValues[i] - testValues[i]), 2);
            }

            return distance = Math.Sqrt(sumOfSquareDiffernces);
        }

        public static double CalculateEuclideanDistance(List<double> exampleValues, double[] testValues)
        {
            double distance;
            double sumOfSquareDiffernces = 0.0;
            for (int i = 0; i < exampleValues.Count; i++)
            {
                sumOfSquareDiffernces += Math.Pow((exampleValues[i] - testValues[i]), 2);
            }

            return distance = Math.Sqrt(sumOfSquareDiffernces);
        }
    }
}