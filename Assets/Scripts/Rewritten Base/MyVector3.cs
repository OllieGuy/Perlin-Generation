using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class MyVector3
{
    public float x, y, z;
    public MyVector3(float x, float y, float z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }
    public MyVector3(Vector3 a)
    {
        x = a.x;
        y = a.y;
        z = a.z;
    }
    public static MyVector3 addVector(MyVector3 a, MyVector3 b)
    {
        MyVector3 rv = new MyVector3(0, 0, 0);
        rv.x = a.x + b.x;
        rv.y = a.y + b.y;
        rv.z = a.z + b.z;
        return rv;
    }
    public static MyVector3 operator +(MyVector3 lhs, MyVector3 rhs)
    {
        return addVector(lhs, rhs);
    }
    public static MyVector3 subtractVector(MyVector3 a, MyVector3 b)
    {
        MyVector3 rv = new MyVector3(0, 0, 0);
        rv.x = a.x - b.x;
        rv.y = a.y - b.y;
        rv.z = a.z - b.z;
        return rv;
    }
    public static MyVector3 operator -(MyVector3 lhs, MyVector3 rhs)
    {
        return subtractVector(lhs, rhs);
    }
    public static MyVector3 scaleVectorUp(MyVector3 a, float s)
    {
        MyVector3 rv = new MyVector3(0, 0, 0);
        rv.x = a.x * s;
        rv.y = a.y * s;
        rv.z = a.z * s;
        return rv;
    }
    public static MyVector3 operator *(MyVector3 lhs, float rhs)
    {
        return scaleVectorUp(lhs, rhs);
    }
    public static MyVector3 scaleVectorDown(MyVector3 a, float s)
    {
        MyVector3 rv = new MyVector3(0, 0, 0);
        rv.x = a.x / s;
        rv.y = a.y / s;
        rv.z = a.z / s;
        return rv;
    }
    public static MyVector3 normalizeVector(MyVector3 a)
    {
        MyVector3 rv = new MyVector3(0, 0, 0);
        float s = a.returnMagnitude();
        rv.x = a.x / s;
        rv.y = a.y / s;
        rv.z = a.z / s;
        return rv;
    }
    public static float dotProduct(MyVector3 a, MyVector3 b)
    {
        float rv;
        MyVector3 normA = normalizeVector(a);
        MyVector3 normB = normalizeVector(b);
        rv = (normA.x * normB.x + normA.y * normB.y + normA.z * normB.z);
        return rv;
    }
    public static MyVector3 crossProduct(MyVector3 a, MyVector3 b)
    {
        MyVector3 rv = new MyVector3(0,0,0);
        rv.x = a.y * b.z - a.z * b.y;
        rv.y = a.z * b.x - a.x * b.z;
        rv.z = a.x * b.y - a.y * b.x;
        return rv;
    }
    public float returnMagnitude()
    {
        float rv = 0.0f;
        rv = Mathf.Sqrt(x * x + y * y + z * z);
        return rv;
    }
    public float returnMagnitudeSquared()
    {
        float rv = 0.0f;
        rv = (x * x + y * y + z * z);
        return rv;
    }
    public Vector3 convToVec3()
    {
        Vector3 rv = new Vector3();
        rv.x = x;
        rv.y = y;
        rv.z = z;
        return rv;
    }
}

