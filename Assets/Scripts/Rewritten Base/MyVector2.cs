using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MyVector2
{
    public float x, y;
    public MyVector2(float x, float y)
    {
        this.x = x;
        this.y = y;
    }
    public MyVector2(MyVector2 a)
    {
        this.x = a.x;
        this.y = a.y;
    }
    public static MyVector2 Subtract(MyVector2 a, MyVector2 b)
    {
        MyVector2 rv = new MyVector2(0, 0);
        rv.x = a.x - b.x;
        rv.y = a.y - b.y;
        return rv;
    }
    public static MyVector2 operator -(MyVector2 lhs, MyVector2 rhs)
    {
        return Subtract(lhs, rhs);
    }
    public static MyVector2 Scale(MyVector2 a, float scalar)
    {
        MyVector2 rv = new MyVector2(0, 0);
        rv.x = a.x * scalar;
        rv.y = a.y * scalar;
        return rv;
    }
    public static MyVector2 operator*(MyVector2 lhs, float rhs)
    {
        return Scale(lhs,rhs);
    }
    public static MyVector2 Divide(MyVector2 a, float divisor)
    {
        MyVector2 rv = new MyVector2(0, 0);
        rv.x = a.x / divisor;
        rv.y = a.y / divisor;
        return rv;
    }
    public static MyVector2 operator/(MyVector2 lhs, float rhs)
    {
        return Divide(lhs, rhs);
    }
    public static MyVector2 Normalize(MyVector2 a)
    {
        MyVector2 rv = new MyVector2(0, 0);
        rv.x = a.x;
        rv.y = a.y;
        rv = rv / Magnitude(rv);
        return rv;
    }
    public static float Magnitude(MyVector2 a)
    {
        float rv = Mathf.Sqrt(a.x * a.x + a.y * a.y);
        return rv;
    }
    public static MyVector2 LERP(MyVector2 a, MyVector2 b, float LERPVal)
    {
        MyVector2 rv = new MyVector2(0, 0);

        rv.x = a.x * (1 - LERPVal) + b.x * LERPVal;
        rv.y = a.y * (1 - LERPVal) + b.y * LERPVal;

        return rv;
    }
    public static float DotProduct(MyVector2 a, MyVector2 b, bool normalize)
    {
        if (normalize)
        {
            a = Normalize(a);
            b = Normalize(b);
        }
        float rv = a.x * b.x + a.y *b.y;
        return rv;
    }
}
