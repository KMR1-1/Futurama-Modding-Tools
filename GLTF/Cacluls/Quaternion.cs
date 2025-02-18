namespace FuturamaLib.GLTF.Calculs
{
public class Matrix4x4
{
    public float M11, M12, M13, M14;
    public float M21, M22, M23, M24;
    public float M31, M32, M33, M34;
    public float M41, M42, M43, M44;

    public Matrix4x4(float m11, float m12, float m13, float m14,
                     float m21, float m22, float m23, float m24,
                     float m31, float m32, float m33, float m34,
                     float m41, float m42, float m43, float m44)
    {
        M11 = m11; M12 = m12; M13 = m13; M14 = m14;
        M21 = m21; M22 = m22; M23 = m23; M24 = m24;
        M31 = m31; M32 = m32; M33 = m33; M34 = m34;
        M41 = m41; M42 = m42; M43 = m43; M44 = m44;
    }

    // Convert 3x3 rotation part of the matrix to quaternion
    public static Quaternion MatrixToQuaternionList(Matrix4x4 matrix)
    {

        // Extract the 3x3 rotation part of the matrix
        float m11 = matrix.M11, m12 = matrix.M12, m13 = matrix.M13;
        float m21 = matrix.M21, m22 = matrix.M22, m23 = matrix.M23;
        float m31 = matrix.M31, m32 = matrix.M32, m33 = matrix.M33;

        // Compute the trace (sum of diagonal elements)
        float trace = m11 + m22 + m33;

        Quaternion q = new Quaternion();

        if (trace > 0)
        {
            float s = (float)Math.Sqrt(trace + 1.0f) * 2; // S=4*qw
            q.W = 0.25f * s;
            q.X = (m32 - m23) / s;
            q.Y = (m13 - m31) / s;
            q.Z = (m21 - m12) / s;
        }
        else
        {
            int i = 0;
            if (m22 > m11)
                i = 1;
            if (m33 > (i == 0 ? m11 : m22))
                i = 2;

            switch (i)
            {
                case 0:
                    float s0 = (float)Math.Sqrt(m11 - (m22 + m33) + 1.0f) * 2; // S=4*qx
                    q.W = (m32 - m23) / s0;
                    q.X = 0.25f * s0;
                    q.Y = (m12 + m21) / s0;
                    q.Z = (m13 + m31) / s0;
                    break;
                case 1:
                    float s1 = (float)Math.Sqrt(m22 - (m33 + m11) + 1.0f) * 2; // S=4*qy
                    q.W = (m13 - m31) / s1;
                    q.X = (m12 + m21) / s1;
                    q.Y = 0.25f * s1;
                    q.Z = (m23 + m32) / s1;
                    break;
                case 2:
                    float s2 = (float)Math.Sqrt(m33 - (m11 + m22) + 1.0f) * 2; // S=4*qz
                    q.W = (m21 - m12) / s2;
                    q.X = (m13 + m31) / s2;
                    q.Y = (m23 + m32) / s2;
                    q.Z = 0.25f * s2;
                    break;
            }
        }

        // Add the quaternion to the list
        
        return q;
    }
}

public class Quaternion
{
    public float X, Y, Z, W;

    public Quaternion()
    {
        X = 0;
        Y = 0;
        Z = 0;
        W = 1;
    }

    public Quaternion(float x, float y, float z, float w)
    {
        X = x;
        Y = y;
        Z = z;
        W = w;
    }
}

}