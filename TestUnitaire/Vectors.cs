namespace Maths_Matrices.Tests;

// Vector4
public class Vector4
{
    public float x, y, z, w;
    
    public Vector4(float x, float y, float z, float w)
    {
        this.x = x;
        this.y = y;
        this.z = z;
        this.w = w;
    }
    
    public static implicit operator Matrix<float>(Vector4 v)
    {
        return new Matrix<float>(new float[,]
        {
            { v.x },
            { v.y },
            { v.z },
            { v.w }
        });
    }

    public static implicit operator Vector4(Matrix<float> m)
    {
        return new Vector4(m[0, 0], m[1, 0], m[2,0], m[3, 0]);
    }

    public static Vector4 operator *(Matrix<float> m, Vector4 v)
    {
        return m * (Matrix<float>) v;
    }
}

// Vector3
public class Vector3
{
    public float x, y, z;
    
    public Vector3(float x, float y, float z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }
    
    public static implicit operator Matrix<float>(Vector3 v)
    {
        return new Matrix<float>(new float[,]
        {
            { v.x },
            { v.y },
            { v.z },
            {1f},
        });
    }

    public static implicit operator Vector3(Matrix<float> m)
    {
        return new Vector3(m[0, 0], m[1, 0], m[2,0]);
    }

    public static Vector3 operator *(Matrix<float> m, Vector3 v)
    {
        return m * (Matrix<float>) v;
    }
    
    public Vector3 Normalized()
    {
        float length = MathF.Sqrt(x * x + y * y + z * z);
        return new Vector3(x / length, y / length, z / length);
    }
}