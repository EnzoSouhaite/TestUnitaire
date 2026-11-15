using System.Numerics;

namespace Maths_Matrices.Tests;

public class Matrix<T> where T : INumber<T>, IMinMaxValue<T>
{
    
    private T[,] _data;
    public int NbLines { get; }
    public int NbColumns { get; }
    
    // Tests01 : Création de matrices
    public Matrix(int nbLines, int nbColumns)
    {
        NbLines = nbLines;
        NbColumns = nbColumns;
        _data = new T[nbLines, nbColumns];
    }
    
    public T[,] ToArray2D()
    {
        return _data;
    }

    public Matrix(T[,] data)
    {
        _data = data;
        NbLines = data.GetLength(0);
        NbColumns = data.GetLength(1);
    }

    public T this[int i, int j]
    {
        get { return _data[i, j]; }
        set { _data[i, j] = value; }
    }
    
    public Matrix(Matrix<T> other)
    {
        NbLines = other.NbLines;
        NbColumns = other.NbColumns;
        _data = new T[NbLines, NbColumns];
        for (int i = 0; i < NbLines; i++)
        {
            for (int j = 0; j < NbColumns; j++)
            {
                _data[i,j] = other[i, j];
            }
        }
    }

    // Matrice identité
    public static Matrix<T> Identity(int size)
    {
        Matrix<T> result = new Matrix<T>(size, size);

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if(i == j)
                {
                    result[i,j] =T.One;
                }   
            }
        }

        return result;
    }

    public bool IsIdentity()
    {
        if (NbLines != NbColumns)
        {
            return false;
        }
        
        for (int i = 0; i < NbLines; i++)
        {
            for (int j = 0; j < NbColumns; j++)
            {
                if (i == j && _data[i, j] != T.One) return false;
                if (i != j && _data[i, j] != T.Zero) return false;
            }
        }
        
        return true;
    }

    // Multiplication par un scalaire 
    public void Multiply(T scalar)
    {
        for (int i = 0; i < NbLines; i++)
        {
            for (int j = 0; j < NbColumns; j++)
            {
                _data [i, j] = _data[i, j] * scalar;
            }
        }
    }

    public static Matrix<T> Multiply(Matrix<T> m, T scalar)
    {
        Matrix<T> result = new Matrix<T>(m); 
        result.Multiply(scalar);
        return result;
    }

    public static Matrix<T> operator *(Matrix<T> m, T scalar)
    {
        Matrix<T> result = Matrix<T>.Multiply(m , scalar);
        return result;
    }

    public static Matrix<T> operator *(T scalar, Matrix<T> m)
    {
        Matrix<T> result = Matrix<T>.Multiply(m, scalar);
        return result;
    }

    public static Matrix<T> operator -(Matrix<T> m)
    {
        Matrix<T> result = Matrix<T>.Multiply(m, -T.One);
        return result;
    }

    // Addition et soustraction
    public void Add(Matrix<T> other)
    {
        if (NbLines != other.NbLines || other.NbColumns != NbColumns)
        {
            throw new MatrixSumException();
        }
        
        for (int i = 0; i < NbLines; i++)
        {
            for (int j = 0; j < NbColumns; j++)
            {
                _data[i,j] = _data[i, j] + other[i, j];
            }
        }
    }
    
    public static Matrix<T> Add(Matrix<T> m1, Matrix<T> m2)
    {
        Matrix<T> result = new Matrix<T>(m1); 
        result.Add(m2);
        return result;
    }

    public static Matrix<T> operator +(Matrix<T> m1, Matrix<T> m2)
    {
        Matrix<T> result = Matrix<T>.Add(m1 , m2);
        return result;
    }

    public static Matrix<T> operator -(Matrix<T> m1, Matrix<T> m2)
    {
        Matrix<T> result = Matrix<T>.Add(m1, -m2);
        return result;
    }
    
    // Multiplication de deux matrices
    public Matrix<T> Multiply(Matrix<T> other)
    {
        if (this.NbColumns != other.NbLines)
        {
            throw new MatrixMultiplyException();
        }
        
        Matrix<T> result = new Matrix<T>(NbLines, other.NbColumns); 
        for (int i = 0; i < NbLines; i++)
        {
            for (int j = 0; j < other.NbColumns; j++)
            {
                T sum = T.Zero;
                for (int k = 0; k < NbColumns; k++)
                {
                    sum = sum + (this[i, k] * other[k, j]);
                }
                result[i, j] = sum;
            }
        }
        return result;
    }

    public static Matrix<T> Multiply(Matrix<T> m1, Matrix<T> other)
    {
        Matrix<T> result = m1.Multiply(other);
        return result;
    }

    public static Matrix<T> operator *(Matrix<T> m1, Matrix<T> m2)
    {
        Matrix<T> result = Matrix<T>.Multiply(m1, m2);
        return result;
    }

    // Transposée
    public Matrix<T> Transpose()
    {
        Matrix<T> result = new Matrix<T>(NbColumns, NbLines);
        
        for (int i = 0; i < NbLines; i++)
        {
            for (int j = 0; j < NbColumns; j++)
            {
                result[j, i] = _data[i, j];
            }
        }
        
        return result;
    }

    public static Matrix<T> Transpose(Matrix<T> m)
    {
        Matrix<T> result = m.Transpose();
        return result;
    }

    // Matrice augmentée
    public static Matrix<T> GenerateAugmentedMatrix(Matrix<T> m1, Matrix<T> m2)
    {
        Matrix<T> result = new Matrix<T>(m1.NbLines, m1.NbColumns + m2.NbColumns);
        for (int i = 0; i < m1.NbLines; i++)
        {
            for (int j = 0; j < m1.NbColumns; j++)
            {
                result[i, j] = m1[i, j];
            }
        }
        
        for (int i = 0; i < m2.NbLines; i++)
        {
            for (int j = 0; j < m2.NbColumns; j++)
            {
                result[i, j +  m1.NbColumns] = m2[i, j];
            }
        }
        return result;
    }
    
    // Découpage de matrice  
        public (Matrix<T>, Matrix<T>) Split(int columnIndex)
        {
            Matrix<T> m1 = new Matrix<T>(NbLines, columnIndex + 1);
            Matrix<T> m2 = new Matrix<T>(NbLines, NbColumns - (columnIndex + 1));
            for (int i = 0; i < NbLines; i++)
            {
                for (int j = 0; j < columnIndex + 1; j++)
                {
                    m1[i, j] = this[i, j];
                }
            }
            
            for (int i = 0; i < NbLines; i++)
            {
                for (int j = 0; j < m2.NbColumns; j++)
                {
                    m2[i, j] = this[i, j + columnIndex + 1];
                }
            }
            return (m1, m2);
        }

        // Inversion par réduction de lignes
        public Matrix<T> InvertByRowReduction()
        {
            Matrix<T> identity = Matrix<T>.Identity(NbColumns);
            try
            {
                (Matrix<T> reducedM, Matrix<T> inverted) = MatrixRowReductionAlgorithm.Apply(this, identity, true);
                return inverted;
            }
            catch (MatrixRowReductionException)
            {
                throw new MatrixInvertException();
            }
        }

        public static Matrix<T> InvertByRowReduction(Matrix<T> m)
        {
            Matrix<T> result = m.InvertByRowReduction();
            return result;
        }
        
        // Sous-matrice
        public Matrix<T> SubMatrix(int lineToRemove, int columnToRemove)
        {
            Matrix<T> result = new Matrix<T>(NbLines-1, NbColumns-1);

            int resultI = 0;
            for (int i = 0; i < NbLines; i++)
            {
                if(i == lineToRemove) continue;

                int resultJ = 0;
                for (int j = 0; j < NbColumns; j++)
                {
                    if(j ==  columnToRemove) continue; 
                    result[resultI, resultJ] = this[i, j];
                    resultJ++;
                }
                resultI++;
            }
            
            return result;
        }

        public static Matrix<T> SubMatrix(Matrix<T> m,  int lineToRemove, int columnToRemove)
        {
            Matrix<T> result = m.SubMatrix(lineToRemove, columnToRemove);
            return result;
        }

        // Tests13 : Déterminants
        public static T Determinant(Matrix<T> m)
        {
            if (m.NbLines == 1)
            {
                return m[0, 0];
            }
            
            if (m.NbLines == 2)
            {
                return m[0, 0] * m[1, 1] - m[0, 1] * m[1, 0];
            }
            
            T sum = T.Zero;
                
            for (int i = 0; i < m.NbLines; i++)
            {
                T sign = (i % 2 == 0) ? T.One : -T.One;
                
                sum = sum +  m[i, 0] * sign * Determinant(m.SubMatrix(i, 0));
            }
            
            return sum;
        }

        // Matrice adjointe
        public Matrix<T> Adjugate()
        {
            Matrix<T> result = new Matrix<T>(NbLines, NbColumns);
            
            for (int i = 0; i < NbLines; i++)
            {
                for (int j = 0; j < NbColumns; j++)
                {
                    T sign = ((i + j) % 2 == 0) ? T.One : -T.One;

                    result[j, i] = sign * Determinant(this.SubMatrix(i, j));
                }
            }
            return result;
        }

        public static Matrix<T> Adjugate(Matrix<T> m)
        {
            Matrix<T> result = m.Adjugate();
            return result;
        }

        // Inversion par déterminant
        public Matrix<T> InvertByDeterminant()
        {
            T det =  Determinant(this);

            if (det == T.Zero)
            {
                throw new MatrixInvertException();
            }

            Matrix<T> adjugate = this.Adjugate();
            return adjugate * (T.One / det);
        }

        public static Matrix<T> InvertByDeterminant(Matrix<T> m)
        {
            Matrix<T> result = m.InvertByDeterminant();
            return result;
        }
}