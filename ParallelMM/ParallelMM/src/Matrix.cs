namespace ParallelMM;

public class Matrix
{
    private readonly int[,] _matrixArray;
    private readonly int _numOfRows;
    private readonly int _numOfCols;

    //constructor
    public Matrix(string filepath)
    {
        _matrixArray = convertFileToMatrix(filepath);
        _numOfRows = _matrixArray.GetLength(0);
        _numOfCols = _matrixArray.GetLength(1);
        
    }
    
    //indexer
    public int this[int _numOfRows, int _numOfCols ]
    {
        get { return _matrixArray[_numOfRows, _numOfCols]; }
    }
    
    //properties
    public int NumOfRows => _numOfRows;
    public int NumOfCols => _numOfCols;
    
    private int[,] convertFileToMatrix(string filepath)
    {
        //realization
        return new int[,]{ { 0, 1, 2 }, { 3, 4, 5 } };
    }

}