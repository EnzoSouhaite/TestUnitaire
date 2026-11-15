namespace Maths_Matrices.Tests;

// constructeur et Identity
public struct Quaternion
{
    public float x, y, z, w;

    public Quaternion(float x, float y, float z, float w)
    {
        this.x = x;
        this.y = y;
        this.z = z;
        this.w = w;
    }
    
    public static Quaternion Identity
    {
        get { return new Quaternion(0, 0, 0, 1); }
    }

    // AngleAxis
    public static Quaternion AngleAxis(float degrees, Vector3 axis)
    {
        Vector3 normalizedAxis = axis.Normalized();
        float radians = degrees * MathF.PI / 180f;
        float halfAngle = radians / 2;

        float cos = MathF.Cos(halfAngle);
        float sin = MathF.Sin(halfAngle);
        
        return new Quaternion(normalizedAxis.x * sin, normalizedAxis.y* sin, normalizedAxis.z* sin, cos);
    }
    
    // multiplication de deux quaternions
    public static Quaternion operator *(Quaternion q1, Quaternion q2)
    {
        float resultW = q1.w*q2.w - q1.x*q2.x - q1.y*q2.y - q1.z*q2.z;
        float resultX = q1.w * q2.x + q1.x * q2.w + q1.y * q2.z - q1.z * q2.y;
        float resultY = q1.w * q2.y - q1.x * q2.z + q1.y * q2.w + q1.z * q2.x;
        float resultZ = q1.w * q2.z + q1.x * q2.y - q1.y * q2.x + q1.z * q2.w;;
    
        return new Quaternion(resultX, resultY, resultZ, resultW);
    }

    // Tests26 : Conjugate / Magnitude / Inverse
    public Quaternion Conjugate()
    {
        return new Quaternion(-x, -y, -z, w);
    }

    public float Magnitude()
    {
        return MathF.Sqrt(w * w + x * x + y * y + z * z);
    }

    public Quaternion Inverse()
    {
        Quaternion conjugate = this.Conjugate();
        float magnitudeSquared = Magnitude() * Magnitude();

        return new Quaternion(conjugate.x / magnitudeSquared, conjugate.y / magnitudeSquared,  conjugate.z / magnitudeSquared, conjugate.w / magnitudeSquared);
    }
    
    public static Vector3 operator *(Quaternion q, Vector3 point)
    {
        Quaternion pointAsQuaternion = new Quaternion(point.x, point.y, point.z, 0f);
        Quaternion qInverse = q.Inverse();
    
        Quaternion result = q * pointAsQuaternion * qInverse;
    
        return new Vector3(result.x, result.y, result.z);
    }
    
    // Tests27 :  Matrice de rotation
    
    public Matrix<float> Matrix
    {
        get
        {
            Matrix<float> result = Matrix<float>.Identity(4);
        
            result[0, 0] = 1 - 2 * (y * y + z * z);
            result[0, 1] = 2 * (x * y - w * z);
            result[0, 2] = 2 * (x * z + w * y);
        
            result[1, 0] = 2 * (x * y + w * z);
            result[1, 1] = 1 - 2 * (x * x + z * z);
            result[1, 2] = 2 * (y * z - w * x);
        
            result[2, 0] = 2 * (x * z - w * y);
            result[2, 1] = 2 * (y * z + w * x);
            result[2, 2] = 1 - 2 * (x * x + y * y);
        
            return result;
        }
    }
    
    // Tests28 : Quaternion -> angles d'Euler
    public Vector3 EulerAngles
    {
        get
        {
            Matrix<float> m = this.Matrix;

            float xRadians = MathF.Asin(-m[1, 2]);
            float x = xRadians * 180 / MathF.PI;

            float yRadians = MathF.Atan2(m[0, 2], m[2, 2]);
            float y = yRadians * 180 / MathF.PI;
            
            float zRadians = MathF.Atan2(m[1, 0], m[1, 1]);
            float z = zRadians * 180 / MathF.PI; 

            return new Vector3(x, y, z);
        }
    }
    
    public static Quaternion Euler(float x, float y, float z)
    {
        Quaternion qRY = Quaternion.AngleAxis(y, new Vector3(0, 1, 0));
        Quaternion qRX = Quaternion.AngleAxis(x, new Vector3(1, 0, 0));
        Quaternion qRZ = Quaternion.AngleAxis(z, new Vector3(0, 0, 1));
        
        return qRY * qRX * qRZ;
    }
}