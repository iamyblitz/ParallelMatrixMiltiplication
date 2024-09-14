namespace ParallelMM;

public class MatrixMultiplication
{
    public static int[,] SequentialMultiply(Matrix matrixA, Matrix matrixB)
    {
        int[,] matrixC = new int[matrixA.NumOfRows,matrixA.NumOfRows];
        for (int i = 0; i < matrixA.NumOfRows; i++)
        {
            for (int j = 0; j < matrixB.NumOfCols; i++)
            {
                matrixC[i, j] = 0;
                for (int k = 0; k < matrixA.NumOfRows; k++)
                {
                    matrixC[i, j] = matrixC[i, j] + matrixA[i, j] * matrixB[i, j];
                }
            }
        }


        return matrixC;
    }

    public static int[,] ParallelBlockMatrixMultiplication(Matrix matrixA, Matrix matrixB)
    {
        int[,] matrixC = new int[matrixA.NumOfRows, matrixA.NumOfRows];
        
    }
    
    
    
    
}