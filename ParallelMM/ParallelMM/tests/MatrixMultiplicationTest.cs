using System;
using System.Diagnostics;
using System.IO;
using NUnit.Framework;

namespace ParallelMM.test;

public class MatrixMultiplicationTest
{
      [TestFixture]
    public class MatrixMultiplicationTests
    {
        const string testDataPath = "../../../tests/testData/";
        const string testResultsPath = "../../../tests/test_result/";
        
        private string testSmallMatrixAPath = testDataPath + "matrix02_50*50";
        private string testSmallMatrixBPath = testDataPath + "matrix022_50*50";
        
        private string testBigMatrixAPath = testDataPath + "matrix01_500*500";
        private string testBigMatrixBPath = testDataPath + "matrix02_500*500";
        
        private string testUnevenMatrixAPath = testDataPath + "matrix03_500*667";
        private string testUnevenMatrixBPath = testDataPath + "matrix03_667*500";
        

        public void SequentialAndParallelMultiplication_ShouldReturnSameResult(string testMatrixAPath,string testMatrixBPath)
        {
            // Arrange
            Matrix matrixA = new Matrix(testMatrixAPath);
            Matrix matrixB = new Matrix(testMatrixBPath);

            // Act
            int[,] sequentialResult = MatrixMultiplication.SequentialMultiply(matrixA, matrixB);
            int[,] parallelResult = MatrixMultiplication.ParallelBlockMatrixMultiplication(matrixA, matrixB);

            // Assert: compare two matrices element by element
            for (int i = 0; i < sequentialResult.GetLength(0); i++)
            {
                for (int j = 0; j < sequentialResult.GetLength(1); j++)
                {
                        Assert.That(parallelResult[i, j], Is.EqualTo(sequentialResult[i, j]), 
                            $"Matrices differ at element [{i},{j}]");
                }
            }
        }

        public void SequentialMultiplication_TimeMeasurement(string testMatrixAPath,string testMatrixBPath)
        {
            // Arrange
            Matrix matrixA = new Matrix(testMatrixAPath);
            Matrix matrixB = new Matrix(testMatrixBPath);

            // Act
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            int[,] result = MatrixMultiplication.SequentialMultiply(matrixA, matrixB);

            stopwatch.Stop();
            TimeSpan sequentialTime = stopwatch.Elapsed;

            // Assert: store time result
            string logPath = testResultsPath + "sequential_times.txt";
            File.AppendAllText(logPath, $"Sequential execution time: {sequentialTime.TotalMilliseconds} ms{Environment.NewLine}");
        }

        public void ParallelMultiplication_TimeMeasurement(string testMatrixAPath,string testMatrixBPath)
        {
            // Arrange
            Matrix matrixA = new Matrix(testMatrixAPath);
            Matrix matrixB = new Matrix(testMatrixBPath);

            // Act
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            int[,] result = MatrixMultiplication.ParallelBlockMatrixMultiplication(matrixA, matrixB);

            stopwatch.Stop();
            TimeSpan parallelTime = stopwatch.Elapsed;

            // Assert: store time result
            string logPath = testResultsPath + "parallel_times.txt";
            File.AppendAllText(logPath, $"Parallel execution time: {parallelTime.TotalMilliseconds} ms{Environment.NewLine}");
        }


        [Test]
        public void TestLittleMatrixMultiplication()
        {
            SequentialMultiplication_TimeMeasurement(testSmallMatrixAPath, testSmallMatrixBPath);
            SequentialAndParallelMultiplication_ShouldReturnSameResult(testSmallMatrixAPath, testSmallMatrixBPath);
            ParallelMultiplication_TimeMeasurement(testSmallMatrixAPath, testSmallMatrixBPath);
        }
        
        [Test]
        public void TestBigMatrixMultiplication()
        {
            SequentialMultiplication_TimeMeasurement(testBigMatrixAPath, testBigMatrixBPath);
            SequentialAndParallelMultiplication_ShouldReturnSameResult(testBigMatrixAPath, testBigMatrixBPath);
            ParallelMultiplication_TimeMeasurement(testBigMatrixAPath, testBigMatrixBPath);
        }
        
        [Test]
        public void TestUnevenMatrixMultiplication()
        {
            SequentialMultiplication_TimeMeasurement(testUnevenMatrixAPath, testUnevenMatrixBPath);
            SequentialAndParallelMultiplication_ShouldReturnSameResult(testUnevenMatrixAPath, testUnevenMatrixBPath);
            ParallelMultiplication_TimeMeasurement(testUnevenMatrixAPath, testUnevenMatrixBPath);
        }
    }
    
}