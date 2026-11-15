//using System.Numerics;
//using System.Security.Authentication;
//
//namespace Maths_Matrices.Tests;
//public class Matrix<T> where T : INumber<T>, IMinMaxValue<T>
//{
//    
//    private T[,] _data;
//    public int NbLines { get; }
//    public int NbColumns { get; }
//    
//    // ===== Tests01 : Création de matrices =====
//    public Matrix(int nbLines, int nbColumns)
//    {
//        NbLines = nbLines;
//        NbColumns = nbColumns;
//        _data = new T[nbLines, nbColumns];
//    }
//    
//    public T[,] ToArray2D()
//    {
//        return _data;
//    }
//
//    public Matrix(T[,] data)
//    {
//        _data = data;
//        NbLines = data.GetLength(0);
//        NbColumns = data.GetLength(1);
//    }
//
//    public T this[int i, int j]
//    {
//        get { return _data[i, j]; }
//        set { _data[i, j] = value; }
//    }
//    
//    public Matrix(Matrix<T> other)
//    {
//        NbLines = other.NbLines;
//        NbColumns = other.NbColumns;
//        _data = new T[NbLines, NbColumns];
//        for (int i = 0; i < NbLines; i++)
//        {
//            for (int j = 0; j < NbColumns; j++)
//            {
//                _data[i,j] = other[i, j];
//            }
//        }
//    }
//
//    // ===== Matrice identité =====
//    public static Matrix<T> Identity(int size)
//    {
//        Matrix<T> result = new Matrix<T>(size, size);
//
//        for (int i = 0; i < size; i++)
//        {
//            for (int j = 0; j < size; j++)
//            {
//                if(i == j)
//                {
//                    result[i,j] =T.One;
//                }   
//            }
//        }
//
//        return result;
//    }
//
//    public bool IsIdentity()
//    {
//        if (NbLines != NbColumns)
//        {
//            return false;
//        }
//        
//        for (int i = 0; i < NbLines; i++)
//        {
//            for (int j = 0; j < NbColumns; j++)
//            {
//                if (i == j && _data[i, j] != T.One) return false;
//                if (i != j && _data[i, j] != T.Zero) return false;
//            }
//        }
//        
//        return true;
//    }
//
//    // ===== Multiplication par un scalaire =====
//    public void Multiply(T scalar)
//    {
//        for (int i = 0; i < NbLines; i++)
//        {
//            for (int j = 0; j < NbColumns; j++)
//            {
//                _data [i, j] = _data[i, j] * scalar;
//            }
//        }
//    }
//
//    public static Matrix<T> Multiply(Matrix<T> m, T scalar)
//    {
//        Matrix<T> result = new Matrix<T>(m); 
//        result.Multiply(scalar);
//        return result;
//    }
//
//    public static Matrix<T> operator *(Matrix<T> m, T scalar)
//    {
//        Matrix<T> result = Matrix<T>.Multiply(m , scalar);
//        return result;
//    }
//
//    public static Matrix<T> operator *(T scalar, Matrix<T> m)
//    {
//        Matrix<T> result = Matrix<T>.Multiply(m, scalar);
//        return result;
//    }
//
//    public static Matrix<T> operator -(Matrix<T> m)
//    {
//        Matrix<T> result = Matrix<T>.Multiply(m, -T.One);
//        return result;
//    }
//
//    // ===== Addition et soustraction =====
//    public void Add(Matrix<T> other)
//    {
//        if (NbLines != other.NbLines || other.NbColumns != NbColumns)
//        {
//            throw new MatrixSumException();
//        }
//        
//        for (int i = 0; i < NbLines; i++)
//        {
//            for (int j = 0; j < NbColumns; j++)
//            {
//                _data[i,j] = _data[i, j] + other[i, j];
//            }
//        }
//    }
//    
//    public static Matrix<T> Add(Matrix<T> m1, Matrix<T> m2)
//    {
//        Matrix<T> result = new Matrix<T>(m1); 
//        result.Add(m2);
//        return result;
//    }
//
//    public static Matrix<T> operator +(Matrix<T> m1, Matrix<T> m2)
//    {
//        Matrix<T> result = Matrix<T>.Add(m1 , m2);
//        return result;
//    }
//
//    public static Matrix<T> operator -(Matrix<T> m1, Matrix<T> m2)
//    {
//        Matrix<T> result = Matrix<T>.Add(m1, -m2);
//        return result;
//    }
//    
//    // ===== Multiplication de deux matrices =====
//    public Matrix<T> Multiply(Matrix<T> other)
//    {
//        if (this.NbColumns != other.NbLines)
//        {
//            throw new MatrixMultiplyException();
//        }
//        
//        Matrix<T> result = new Matrix<T>(NbLines, other.NbColumns); 
//        for (int i = 0; i < NbLines; i++)
//        {
//            for (int j = 0; j < other.NbColumns; j++)
//            {
//                T sum = T.Zero;
//                for (int k = 0; k < NbColumns; k++)
//                {
//                    sum = sum + (this[i, k] * other[k, j]);
//                }
//                result[i, j] = sum;
//            }
//        }
//        return result;
//    }
//
//    public static Matrix<T> Multiply(Matrix<T> m1, Matrix<T> other)
//    {
//        Matrix<T> result = m1.Multiply(other);
//        return result;
//    }
//
//    public static Matrix<T> operator *(Matrix<T> m1, Matrix<T> m2)
//    {
//        Matrix<T> result = Matrix<T>.Multiply(m1, m2);
//        return result;
//    }
//
//    // ===== Transposée =====
//    public Matrix<T> Transpose()
//    {
//        Matrix<T> result = new Matrix<T>(NbColumns, NbLines);
//        
//        for (int i = 0; i < NbLines; i++)
//        {
//            for (int j = 0; j < NbColumns; j++)
//            {
//                result[j, i] = _data[i, j];
//            }
//        }
//        
//        return result;
//    }
//
//    public static Matrix<T> Transpose(Matrix<T> m)
//    {
//        Matrix<T> result = m.Transpose();
//        return result;
//    }
//
//    // ===== Matrice augmentée =====
//    public static Matrix<T> GenerateAugmentedMatrix(Matrix<T> m1, Matrix<T> m2)
//    {
//        Matrix<T> result = new Matrix<T>(m1.NbLines, m1.NbColumns + m2.NbColumns);
//        for (int i = 0; i < m1.NbLines; i++)
//        {
//            for (int j = 0; j < m1.NbColumns; j++)
//            {
//                result[i, j] = m1[i, j];
//            }
//        }
//        
//        for (int i = 0; i < m2.NbLines; i++)
//        {
//            for (int j = 0; j < m2.NbColumns; j++)
//            {
//                result[i, j +  m1.NbColumns] = m2[i, j];
//            }
//        }
//        return result;
//    }
//    
//    // ===== Découpage de matrice =====    
//        public (Matrix<T>, Matrix<T>) Split(int columnIndex)
//        {
//            Matrix<T> m1 = new Matrix<T>(NbLines, columnIndex + 1);
//            Matrix<T> m2 = new Matrix<T>(NbLines, NbColumns - (columnIndex + 1));
//            for (int i = 0; i < NbLines; i++)
//            {
//                for (int j = 0; j < columnIndex + 1; j++)
//                {
//                    m1[i, j] = this[i, j];
//                }
//            }
//            
//            for (int i = 0; i < NbLines; i++)
//            {
//                for (int j = 0; j < m2.NbColumns; j++)
//                {
//                    m2[i, j] = this[i, j + columnIndex + 1];
//                }
//            }
//            return (m1, m2);
//        }
//
//        // ===== Inversion par réduction de lignes =====
//        public Matrix<T> InvertByRowReduction()
//        {
//            Matrix<T> identity = Matrix<T>.Identity(NbColumns);
//            try
//            {
//                (Matrix<T> reducedM, Matrix<T> inverted) = MatrixRowReductionAlgorithm.Apply(this, identity, true);
//                return inverted;
//            }
//            catch (MatrixRowReductionException)
//            {
//                throw new MatrixInvertException();
//            }
//        }
//
//        public static Matrix<T> InvertByRowReduction(Matrix<T> m)
//        {
//            Matrix<T> result = m.InvertByRowReduction();
//            return result;
//        }
//        
//        // ===== Sous-matrice =====
//        public Matrix<T> SubMatrix(int lineToRemove, int columnToRemove)
//        {
//            Matrix<T> result = new Matrix<T>(NbLines-1, NbColumns-1);
//
//            int resultI = 0;
//            for (int i = 0; i < NbLines; i++)
//            {
//                if(i == lineToRemove) continue;
//
//                int resultJ = 0;
//                for (int j = 0; j < NbColumns; j++)
//                {
//                    if(j ==  columnToRemove) continue; 
//                    result[resultI, resultJ] = this[i, j];
//                    resultJ++;
//                }
//                resultI++;
//            }
//            
//            return result;
//        }
//
//        public static Matrix<T> SubMatrix(Matrix<T> m,  int lineToRemove, int columnToRemove)
//        {
//            Matrix<T> result = m.SubMatrix(lineToRemove, columnToRemove);
//            return result;
//        }
//
//        // ===== Tests13 : Déterminants =====
//        public static T Determinant(Matrix<T> m)
//        {
//            if (m.NbLines == 1)
//            {
//                return m[0, 0];
//            }
//            
//            if (m.NbLines == 2)
//            {
//                return m[0, 0] * m[1, 1] - m[0, 1] * m[1, 0];
//            }
//            
//            T sum = T.Zero;
//                
//            for (int i = 0; i < m.NbLines; i++)
//            {
//                T sign = (i % 2 == 0) ? T.One : -T.One;
//                
//                sum = sum +  m[i, 0] * sign * Determinant(m.SubMatrix(i, 0));
//            }
//            
//            return sum;
//        }
//
//        // ===== Matrice adjointe =====
//        public Matrix<T> Adjugate()
//        {
//            Matrix<T> result = new Matrix<T>(NbLines, NbColumns);
//            
//            for (int i = 0; i < NbLines; i++)
//            {
//                for (int j = 0; j < NbColumns; j++)
//                {
//                    T sign = ((i + j) % 2 == 0) ? T.One : -T.One;
//
//                    result[j, i] = sign * Determinant(this.SubMatrix(i, j));
//                }
//            }
//            return result;
//        }
//
//        public static Matrix<T> Adjugate(Matrix<T> m)
//        {
//            Matrix<T> result = m.Adjugate();
//            return result;
//        }
//
//        // ===== Inversion par déterminant =====
//        public Matrix<T> InvertByDeterminant()
//        {
//            T det =  Determinant(this);
//
//            if (det == T.Zero)
//            {
//                throw new MatrixInvertException();
//            }
//
//            Matrix<T> adjugate = this.Adjugate();
//            return adjugate * (T.One / det);
//        }
//
//        public static Matrix<T> InvertByDeterminant(Matrix<T> m)
//        {
//            Matrix<T> result = m.InvertByDeterminant();
//            return result;
//        }
//}
//
//// ===== Opérations élémentaires sur les matrices =====
//public static class MatrixElementaryOperations
//{
//    public static void SwapLines<T>(Matrix<T> m, int line1, int line2) where T : INumber<T>, IMinMaxValue<T>
//    {
//        for (int j = 0; j < m.NbColumns; j++)
//        {
//            T temp = m[line1, j];
//            m[line1, j] = m[line2, j];
//            m[line2, j] = temp;
//        }
//    }
//    
//    public static void SwapColumns<T>(Matrix<T> m, int column1, int column2) where T : INumber<T>, IMinMaxValue<T>
//    {
//        for (int j = 0; j < m.NbLines; j++)
//        {
//            T temp = m[j, column1];
//            m[j, column1] = m[j, column2];
//            m[j, column2] = temp;
//        }
//    }
//    
//    public static void MultiplyLine<T>(Matrix<T> m, int line, T scalar) where T : INumber<T>, IMinMaxValue<T>
//    {
//        if (scalar == T.Zero)
//        {
//            throw new MatrixScalarZeroException();
//        }
//        for (int j = 0; j < m.NbColumns; j++)
//        {
//            m[line, j] = m[line, j] *  scalar;
//        }
//    }
//
//    public static void MultiplyColumn<T>(Matrix<T> m, int column, T scalar) where T : INumber<T>, IMinMaxValue<T>
//    {
//        if (scalar == T.Zero)
//        {
//            throw new MatrixScalarZeroException();
//        }
//
//        for (int j = 0; j < m.NbLines; j++)
//        {
//            m[j, column] = m[j, column] * scalar;
//        }
//    }
//
//    public static void AddLineToAnother<T>(Matrix<T> m, int lineToModify, int applyLine, T factor) where T : INumber<T>, IMinMaxValue<T>
//    {
//        for (int j = 0; j < m.NbColumns; j++)
//        {
//            m[applyLine, j] = m[applyLine, j] + m[lineToModify, j] * factor;
//        }
//    }
//
//    public static void AddColumnToAnother<T>(Matrix<T> m, int columnToModify, int applyColumn, T factor) where T : INumber<T>, IMinMaxValue<T>
//    {
//        for (int j = 0; j < m.NbLines; j++)
//        {
//            m[j, applyColumn] = m[j, applyColumn] + m[j, columnToModify] * factor;
//        }
//    }
//}
//
//// ===== Algorithme de réduction de lignes =====
//public static class MatrixRowReductionAlgorithm
//{
//    public static (Matrix<T>, Matrix<T>) Apply<T>(Matrix<T> m1, Matrix<T> m2, bool throwOnZeroColumn = false) where T : INumber<T>, IMinMaxValue<T>
//    {
//        Matrix<T> augmented = Matrix<T>.GenerateAugmentedMatrix(m1, m2);
//
//        int i = 0;
//        for (int j = 0; j < m1.NbColumns; j++)
//        {
//            T maxValue = T.MinValue;
//            int k = i;
//            
//            for (int r = i; r < augmented.NbLines; r++)
//            {
//                if (T.Abs(augmented[r, j]) > maxValue)
//                {
//                    maxValue =  T.Abs(augmented[r, j]);
//                    k = r;
//                }
//            }
//
//            if (maxValue == T.Zero)
//            {
//                if (throwOnZeroColumn)
//                {
//                    throw new MatrixRowReductionException();
//                }
//                continue;
//            }
//            
//            if(k != i)
//            {
//                MatrixElementaryOperations.SwapLines(augmented, i, k);
//            }
//            
//            MatrixElementaryOperations.MultiplyLine(augmented, i, T.One / augmented[i, j]);
//            
//            for (int r = 0; r < augmented.NbLines; r++)
//            {
//                if (r != i)
//                {
//                    MatrixElementaryOperations.AddLineToAnother(augmented,i, r, -augmented[r, j]);
//                }
//            }
//            i++;
//        }
//        return augmented.Split(m1.NbColumns - 1);
//    }
//}
//
//// ===== Exceptions =====
//public class MatrixRowReductionException : Exception
//{
//    
//}
//
//public class MatrixSumException : Exception
//{
//        
//}
//
//public class MatrixMultiplyException : Exception
//{
//    
//}
//
//public class MatrixInvertException : Exception
//{
//    
//}
//
//public class MatrixScalarZeroException : Exception
//{
//    
//}
//
//// ===== Vector4 =====
//public class Vector4
//{
//    public float x, y, z, w;
//    
//    public Vector4(float x, float y, float z, float w)
//    {
//        this.x = x;
//        this.y = y;
//        this.z = z;
//        this.w = w;
//    }
//    
//    public static implicit operator Matrix<float>(Vector4 v)
//    {
//        return new Matrix<float>(new float[,]
//        {
//            { v.x },
//            { v.y },
//            { v.z },
//            { v.w }
//        });
//    }
//
//    public static implicit operator Vector4(Matrix<float> m)
//    {
//        return new Vector4(m[0, 0], m[1, 0], m[2,0], m[3, 0]);
//    }
//
//    public static Vector4 operator *(Matrix<float> m, Vector4 v)
//    {
//        return m * (Matrix<float>) v;
//    }
//}
//
//// ===== Vector3 =====
//public class Vector3
//{
//    public float x, y, z;
//    
//    public Vector3(float x, float y, float z)
//    {
//        this.x = x;
//        this.y = y;
//        this.z = z;
//    }
//    
//    public static implicit operator Matrix<float>(Vector3 v)
//    {
//        return new Matrix<float>(new float[,]
//        {
//            { v.x },
//            { v.y },
//            { v.z },
//            {1f},
//        });
//    }
//
//    public static implicit operator Vector3(Matrix<float> m)
//    {
//        return new Vector3(m[0, 0], m[1, 0], m[2,0]);
//    }
//
//    public static Vector3 operator *(Matrix<float> m, Vector3 v)
//    {
//        return m * (Matrix<float>) v;
//    }
//    
//    public Vector3 Normalized()
//    {
//        float length = MathF.Sqrt(x * x + y * y + z * z);
//        return new Vector3(x / length, y / length, z / length);
//    }
//}
//
//// ===== Transform =====
//public class Transform
//{
//    private Vector3 _localPosition;
//    private Matrix<float> _localTranslationMatrix;
//    
//    private Vector3 _localScale;
//    private Matrix<float> _localScaleMatrix;
//    
//    private Vector3 _localRotation;
//    private Matrix<float> _localRotationXMatrix;
//    private Matrix<float> _localRotationYMatrix;
//    private Matrix<float> _localRotationZMatrix;
//    private Matrix<float> _localRotationMatrix;
//    
//    private Transform _parent;
//    
//    public Transform()
//    {
//        LocalPosition = new Vector3(0f, 0f, 0f);
//        LocalScale = new Vector3(1f, 1f, 1f);
//        LocalRotation = new Vector3(0f, 0f, 0f); 
//    }
//    
//    private Matrix<float> BuildTranslationMatrix(Vector3 position)
//    {
//        Matrix<float> result = Matrix<float>.Identity(4);
//        result[0, 3] = position.x;
//        result[1, 3] = position.y;
//        result[2, 3] = position.z;
//        return result;
//    }
//    
//    // ===== Transform : position locale =====
//    public Vector3 LocalPosition
//    {
//        get { return _localPosition; }
//        set
//        {
//            _localPosition = value;
//            _localTranslationMatrix = BuildTranslationMatrix(value);
//        }
//    }
//    
//    // ===== Transform : échelle locale =====
//    public Vector3 LocalScale
//    {
//        get { return _localScale; }
//        set
//        {
//            _localScale = value;
//            _localScaleMatrix = BuildScaleMatrix(value);
//        }
//    }
//    public Matrix<float> LocalTranslationMatrix
//    {
//        get { return _localTranslationMatrix; }
//    }
//    
//    // ===== Tests18 : Matrices de rotation X, Y, Z =====
//    private Matrix<float> BuildRotationXMatrix(float degrees)
//    {
//        float radians = degrees * MathF.PI / 180f;
//        float cos = MathF.Cos(radians);
//        float sin = MathF.Sin(radians);
//    
//        Matrix<float> result = Matrix<float>.Identity(4);
//        result[1, 1] = cos;
//        result[1, 2] = -sin;
//        result[2, 1] = sin;
//        result[2, 2] = cos;
//        return result;
//    }
//    
//    private Matrix<float> BuildRotationYMatrix(float degrees)
//    {
//        float radians = degrees * MathF.PI / 180f;
//        float cos = MathF.Cos(radians);
//        float sin = MathF.Sin(radians);
//    
//        Matrix<float> result = Matrix<float>.Identity(4);
//        result[0, 0] = cos;
//        result[2, 0] = -sin;
//        result[0, 2] = sin;
//        result[2, 2] = cos;
//        return result;
//    }
//    
//    private Matrix<float> BuildRotationZMatrix(float degrees)
//    {
//        float radians = degrees * MathF.PI / 180f;
//        float cos = MathF.Cos(radians);
//        float sin = MathF.Sin(radians);
//    
//        Matrix<float> result = Matrix<float>.Identity(4);
//        result[0, 0] = cos;
//        result[0, 1] = -sin;
//        result[1, 0] = sin;
//        result[1, 1] = cos;
//        return result;
//    }
//    
//    private Matrix<float> BuildScaleMatrix(Vector3 position)
//    {
//        Matrix<float> result = Matrix<float>.Identity(4);
//        result[0, 0] = position.x;
//        result[1, 1] = position.y;
//        result[2, 2] = position.z;
//        return result;
//    }
//    
//    // ===== Tests18 : LocalRotation =====
//    public Vector3 LocalRotation
//    {
//        get { return _localRotation; }
//        set
//        {
//            _localRotation = value;
//            _localRotationXMatrix = BuildRotationXMatrix(value.x);
//            _localRotationYMatrix = BuildRotationYMatrix(value.y);
//            _localRotationZMatrix = BuildRotationZMatrix(value.z);
//            _localRotationMatrix = _localRotationYMatrix * _localRotationXMatrix * _localRotationZMatrix;
//        }
//    }
//    
//    // ===== Tests29 : LocalRotation sous forme de quaternion =====
//    public Quaternion LocalRotationQuaternion
//    {
//        get
//        {
//            return Quaternion.Euler(LocalRotation.x, LocalRotation.y, LocalRotation.z);
//        }
//        set
//        {
//            LocalRotation = value.EulerAngles;
//        }
//    }
//    
//    // ===== Tests21 et Tests22 : WorldPosition =====
//    public Vector3 WorldPosition
//    {
//        get 
//        { 
//            Matrix<float> m = LocalToWorldMatrix;
//            return new Vector3(m[0, 3], m[1, 3], m[2, 3]);
//        }
//        set
//        {
//            if (_parent == null)
//            {
//                LocalPosition = value;
//            }
//            else
//            {
//                LocalPosition = _parent.WorldToLocalMatrix * value;
//            }
//        }
//    }
//    
//    // ===== Tests21 : SetParent =====
//    public void SetParent(Transform parent)
//    {
//        _parent = parent;
//    }
//    
//    public Matrix<float> LocalRotationMatrix
//    {
//        get { return _localRotationMatrix; }
//    }
//    public Matrix<float> LocalRotationXMatrix
//    {
//        get { return _localRotationXMatrix; }
//    }
//    public Matrix<float> LocalRotationYMatrix
//    {
//        get { return _localRotationYMatrix; }
//    }
//    public Matrix<float> LocalRotationZMatrix
//    {
//        get { return _localRotationZMatrix; }
//    }
//    public Matrix<float> LocalScaleMatrix
//    {
//        get { return _localScaleMatrix; }
//    }
//    
//    // ===== Tests20 : LocalToWorld / WorldToLocal =====
//    public Matrix<float> LocalToWorldMatrix
//    {
//        get 
//        {
//            Matrix<float> localMatrix = _localTranslationMatrix * _localRotationMatrix * _localScaleMatrix;
//        
//            if (_parent != null)
//            {
//                return _parent.LocalToWorldMatrix * localMatrix;
//            }
//        
//            return localMatrix;
//        }
//    }
//    
//    public Matrix<float> WorldToLocalMatrix
//    {
//        get { return LocalToWorldMatrix.InvertByRowReduction(); }
//    }
//}
//
//// ===== Quaternion : constructeur et Identity =====
//public struct Quaternion
//{
//    public float x, y, z, w;
//
//    public Quaternion(float x, float y, float z, float w)
//    {
//        this.x = x;
//        this.y = y;
//        this.z = z;
//        this.w = w;
//    }
//    
//    public static Quaternion Identity
//    {
//        get { return new Quaternion(0, 0, 0, 1); }
//    }
//
//    // ===== Quaternion : AngleAxis =====
//    public static Quaternion AngleAxis(float degrees, Vector3 axis)
//    {
//        Vector3 normalizedAxis = axis.Normalized();
//        float radians = degrees * MathF.PI / 180f;
//        float halfAngle = radians / 2;
//
//        float cos = MathF.Cos(halfAngle);
//        float sin = MathF.Sin(halfAngle);
//        
//        return new Quaternion(normalizedAxis.x * sin, normalizedAxis.y* sin, normalizedAxis.z* sin, cos);
//    }
//    
//    // ===== Quaternion : multiplication de deux quaternions =====
//    public static Quaternion operator *(Quaternion q1, Quaternion q2)
//    {
//        float resultW = q1.w*q2.w - q1.x*q2.x - q1.y*q2.y - q1.z*q2.z;
//        float resultX = q1.w * q2.x + q1.x * q2.w + q1.y * q2.z - q1.z * q2.y;
//        float resultY = q1.w * q2.y - q1.x * q2.z + q1.y * q2.w + q1.z * q2.x;
//        float resultZ = q1.w * q2.z + q1.x * q2.y - q1.y * q2.x + q1.z * q2.w;;
//    
//        return new Quaternion(resultX, resultY, resultZ, resultW);
//    }
//
//    // ===== Tests26 : Conjugate / Magnitude / Inverse =====
//    public Quaternion Conjugate()
//    {
//        return new Quaternion(-x, -y, -z, w);
//    }
//
//    public float Magnitude()
//    {
//        return MathF.Sqrt(w * w + x * x + y * y + z * z);
//    }
//
//    public Quaternion Inverse()
//    {
//        Quaternion conjugate = this.Conjugate();
//        float magnitudeSquared = Magnitude() * Magnitude();
//
//        return new Quaternion(conjugate.x / magnitudeSquared, conjugate.y / magnitudeSquared,  conjugate.z / magnitudeSquared, conjugate.w / magnitudeSquared);
//    }
//    
//    public static Vector3 operator *(Quaternion q, Vector3 point)
//    {
//        Quaternion pointAsQuaternion = new Quaternion(point.x, point.y, point.z, 0f);
//        Quaternion qInverse = q.Inverse();
//    
//        Quaternion result = q * pointAsQuaternion * qInverse;
//    
//        return new Vector3(result.x, result.y, result.z);
//    }
//    
//    // ===== Tests27 Quaternion :  Matrice de rotation =====
//    
//    public Matrix<float> Matrix
//    {
//        get
//        {
//            Matrix<float> result = Matrix<float>.Identity(4);
//        
//            result[0, 0] = 1 - 2 * (y * y + z * z);
//            result[0, 1] = 2 * (x * y - w * z);
//            result[0, 2] = 2 * (x * z + w * y);
//        
//            result[1, 0] = 2 * (x * y + w * z);
//            result[1, 1] = 1 - 2 * (x * x + z * z);
//            result[1, 2] = 2 * (y * z - w * x);
//        
//            result[2, 0] = 2 * (x * z - w * y);
//            result[2, 1] = 2 * (y * z + w * x);
//            result[2, 2] = 1 - 2 * (x * x + y * y);
//        
//            return result;
//        }
//    }
//    
//    // ===== Tests28 : Quaternion -> angles d'Euler =====
//    public Vector3 EulerAngles
//    {
//        get
//        {
//            Matrix<float> m = this.Matrix;
//
//            float xRadians = MathF.Asin(-m[1, 2]);
//            float x = xRadians * 180 / MathF.PI;
//
//            float yRadians = MathF.Atan2(m[0, 2], m[2, 2]);
//            float y = yRadians * 180 / MathF.PI;
//            
//            float zRadians = MathF.Atan2(m[1, 0], m[1, 1]);
//            float z = zRadians * 180 / MathF.PI; 
//
//            return new Vector3(x, y, z);
//        }
//    }
//    
//    public static Quaternion Euler(float x, float y, float z)
//    {
//        Quaternion qRY = Quaternion.AngleAxis(y, new Vector3(0, 1, 0));
//        Quaternion qRX = Quaternion.AngleAxis(x, new Vector3(1, 0, 0));
//        Quaternion qRZ = Quaternion.AngleAxis(z, new Vector3(0, 0, 1));
//        
//        return qRY * qRX * qRZ;
//    }
//}