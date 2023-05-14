using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyVector4
{
    public float x, y, z, w;
    public MyVector4(float x, float y, float z, float w)
    {
        this.x = x;
        this.y = y;
        this.z = z;
        this.w = w;
    }
    public static MyVector4 addVector(MyVector4 a, MyVector4 b)
    {
        MyVector4 rv = new MyVector4(0, 0, 0, 0);
        rv.x = a.x + b.x;
        rv.y = a.y + b.y;
        rv.z = a.z + b.z;
        rv.w = a.w + b.w;
        return rv;
    }
    public static MyVector4 operator +(MyVector4 lhs, MyVector4 rhs)
    {
        return addVector(lhs, rhs);
    }
    public static MyVector4 subtractVector(MyVector4 a, MyVector4 b)
    {
        MyVector4 rv = new MyVector4(0, 0, 0, 0);
        rv.x = a.x - b.x;
        rv.y = a.y - b.y;
        rv.z = a.z - b.z;
        rv.w = a.w - b.w;
        return rv;
    }
    public static MyVector4 operator -(MyVector4 lhs, MyVector4 rhs)
    {
        return subtractVector(lhs, rhs);
    }
}
