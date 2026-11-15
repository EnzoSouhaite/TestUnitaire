namespace Maths_Matrices.Tests;

public class Transform
{
    private Vector3 _localPosition;
    private Matrix<float> _localTranslationMatrix;
    
    private Vector3 _localScale;
    private Matrix<float> _localScaleMatrix;
    
    private Vector3 _localRotation;
    private Matrix<float> _localRotationXMatrix;
    private Matrix<float> _localRotationYMatrix;
    private Matrix<float> _localRotationZMatrix;
    private Matrix<float> _localRotationMatrix;
    
    private Transform _parent;
    
    public Transform()
    {
        LocalPosition = new Vector3(0f, 0f, 0f);
        LocalScale = new Vector3(1f, 1f, 1f);
        LocalRotation = new Vector3(0f, 0f, 0f); 
    }
    
    private Matrix<float> BuildTranslationMatrix(Vector3 position)
    {
        Matrix<float> result = Matrix<float>.Identity(4);
        result[0, 3] = position.x;
        result[1, 3] = position.y;
        result[2, 3] = position.z;
        return result;
    }
    
    // position locale
    public Vector3 LocalPosition
    {
        get { return _localPosition; }
        set
        {
            _localPosition = value;
            _localTranslationMatrix = BuildTranslationMatrix(value);
        }
    }
    
    // échelle locale
    public Vector3 LocalScale
    {
        get { return _localScale; }
        set
        {
            _localScale = value;
            _localScaleMatrix = BuildScaleMatrix(value);
        }
    }
    public Matrix<float> LocalTranslationMatrix
    {
        get { return _localTranslationMatrix; }
    }
    
    // Tests18 : Matrices de rotation X, Y, Z
    private Matrix<float> BuildRotationXMatrix(float degrees)
    {
        float radians = degrees * MathF.PI / 180f;
        float cos = MathF.Cos(radians);
        float sin = MathF.Sin(radians);
    
        Matrix<float> result = Matrix<float>.Identity(4);
        result[1, 1] = cos;
        result[1, 2] = -sin;
        result[2, 1] = sin;
        result[2, 2] = cos;
        return result;
    }
    
    private Matrix<float> BuildRotationYMatrix(float degrees)
    {
        float radians = degrees * MathF.PI / 180f;
        float cos = MathF.Cos(radians);
        float sin = MathF.Sin(radians);
    
        Matrix<float> result = Matrix<float>.Identity(4);
        result[0, 0] = cos;
        result[2, 0] = -sin;
        result[0, 2] = sin;
        result[2, 2] = cos;
        return result;
    }
    
    private Matrix<float> BuildRotationZMatrix(float degrees)
    {
        float radians = degrees * MathF.PI / 180f;
        float cos = MathF.Cos(radians);
        float sin = MathF.Sin(radians);
    
        Matrix<float> result = Matrix<float>.Identity(4);
        result[0, 0] = cos;
        result[0, 1] = -sin;
        result[1, 0] = sin;
        result[1, 1] = cos;
        return result;
    }
    
    private Matrix<float> BuildScaleMatrix(Vector3 position)
    {
        Matrix<float> result = Matrix<float>.Identity(4);
        result[0, 0] = position.x;
        result[1, 1] = position.y;
        result[2, 2] = position.z;
        return result;
    }
    
    // Tests18 : LocalRotation
    public Vector3 LocalRotation
    {
        get { return _localRotation; }
        set
        {
            _localRotation = value;
            _localRotationXMatrix = BuildRotationXMatrix(value.x);
            _localRotationYMatrix = BuildRotationYMatrix(value.y);
            _localRotationZMatrix = BuildRotationZMatrix(value.z);
            _localRotationMatrix = _localRotationYMatrix * _localRotationXMatrix * _localRotationZMatrix;
        }
    }
    
    // Tests29 : LocalRotation sous forme de quaternion
    public Quaternion LocalRotationQuaternion
    {
        get
        {
            return Quaternion.Euler(LocalRotation.x, LocalRotation.y, LocalRotation.z);
        }
        set
        {
            LocalRotation = value.EulerAngles;
        }
    }
    
    // Tests21 et Tests22 : WorldPosition
    public Vector3 WorldPosition
    {
        get 
        { 
            Matrix<float> m = LocalToWorldMatrix;
            return new Vector3(m[0, 3], m[1, 3], m[2, 3]);
        }
        set
        {
            if (_parent == null)
            {
                LocalPosition = value;
            }
            else
            {
                LocalPosition = _parent.WorldToLocalMatrix * value;
            }
        }
    }
    
    // Tests21 : SetParent
    public void SetParent(Transform parent)
    {
        _parent = parent;
    }
    
    public Matrix<float> LocalRotationMatrix
    {
        get { return _localRotationMatrix; }
    }
    public Matrix<float> LocalRotationXMatrix
    {
        get { return _localRotationXMatrix; }
    }
    public Matrix<float> LocalRotationYMatrix
    {
        get { return _localRotationYMatrix; }
    }
    public Matrix<float> LocalRotationZMatrix
    {
        get { return _localRotationZMatrix; }
    }
    public Matrix<float> LocalScaleMatrix
    {
        get { return _localScaleMatrix; }
    }
    
    // Tests20 : LocalToWorld / WorldToLocal
    public Matrix<float> LocalToWorldMatrix
    {
        get 
        {
            Matrix<float> localMatrix = _localTranslationMatrix * _localRotationMatrix * _localScaleMatrix;
        
            if (_parent != null)
            {
                return _parent.LocalToWorldMatrix * localMatrix;
            }
        
            return localMatrix;
        }
    }
    
    public Matrix<float> WorldToLocalMatrix
    {
        get { return LocalToWorldMatrix.InvertByRowReduction(); }
    }
}