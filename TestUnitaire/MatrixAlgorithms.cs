using System.Numerics;

namespace Maths_Matrices.Tests;

// Opérations élémentaires sur les matrices
public static class MatrixElementaryOperations
{
    public static void SwapLines<T>(Matrix<T> m, int line1, int line2) where T : INumber<T>, IMinMaxValue<T>
    {
        for (int j = 0; j < m.NbColumns; j++)
        {
            (m[line1, j], m[line2, j]) = (m[line2, j], m[line1, j]);
        }
    }
    
    public static void SwapColumns<T>(Matrix<T> m, int column1, int column2) where T : INumber<T>, IMinMaxValue<T>
    {
        for (int j = 0; j < m.NbLines; j++)
        {
            (m[j, column1], m[j, column2]) = (m[j, column2], m[j, column1]);
        }
    }
    
    public static void MultiplyLine<T>(Matrix<T> m, int line, T scalar) where T : INumber<T>, IMinMaxValue<T>
    {
        if (scalar == T.Zero)
        {
            throw new MatrixScalarZeroException();
        }
        for (int j = 0; j < m.NbColumns; j++)
        {
            m[line, j] = m[line, j] *  scalar;
        }
    }

    public static void MultiplyColumn<T>(Matrix<T> m, int column, T scalar) where T : INumber<T>, IMinMaxValue<T>
    {
        if (scalar == T.Zero)
        {
            throw new MatrixScalarZeroException();
        }

        for (int j = 0; j < m.NbLines; j++)
        {
            m[j, column] = m[j, column] * scalar;
        }
    }

    public static void AddLineToAnother<T>(Matrix<T> m, int lineToModify, int applyLine, T factor) where T : INumber<T>, IMinMaxValue<T>
    {
        for (int j = 0; j < m.NbColumns; j++)
        {
            m[applyLine, j] = m[applyLine, j] + m[lineToModify, j] * factor;
        }
    }

    public static void AddColumnToAnother<T>(Matrix<T> m, int columnToModify, int applyColumn, T factor) where T : INumber<T>, IMinMaxValue<T>
    {
        for (int j = 0; j < m.NbLines; j++)
        {
            m[j, applyColumn] = m[j, applyColumn] + m[j, columnToModify] * factor;
        }
    }
}

// Algorithme de réduction de lignes
public static class MatrixRowReductionAlgorithm
{
    public static (Matrix<T>, Matrix<T>) Apply<T>(Matrix<T> m1, Matrix<T> m2, bool throwOnZeroColumn = false) where T : INumber<T>, IMinMaxValue<T>
    {
        Matrix<T> augmented = Matrix<T>.GenerateAugmentedMatrix(m1, m2);

        int i = 0;
        for (int j = 0; j < m1.NbColumns; j++)
        {
            T maxValue = T.MinValue;
            int k = i;
            
            for (int r = i; r < augmented.NbLines; r++)
            {
                if (T.Abs(augmented[r, j]) > maxValue)
                {
                    maxValue =  T.Abs(augmented[r, j]);
                    k = r;
                }
            }

            if (maxValue == T.Zero)
            {
                if (throwOnZeroColumn)
                {
                    throw new MatrixRowReductionException();
                }
                continue;
            }
            
            if(k != i)
            {
                MatrixElementaryOperations.SwapLines(augmented, i, k);
            }
            
            MatrixElementaryOperations.MultiplyLine(augmented, i, T.One / augmented[i, j]);
            
            for (int r = 0; r < augmented.NbLines; r++)
            {
                if (r != i)
                {
                    MatrixElementaryOperations.AddLineToAnother(augmented,i, r, -augmented[r, j]);
                }
            }
            i++;
        }
        return augmented.Split(m1.NbColumns - 1);
    }
}

// Exceptions
public class MatrixRowReductionException : Exception { }

public class MatrixSumException : Exception { }

public class MatrixMultiplyException : Exception { }

public class MatrixInvertException : Exception { }

public class MatrixScalarZeroException : Exception { }