using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyQuat : MonoBehaviour
{
    public float x, y, z, w;
    public MyQuat(MyVector4 a)
    {
        x = a.x;
        y = a.y;
        z = a.z;
        w = a.w;
    }
    public MyQuat(float x, float y, float z, float w)
    {
        this.x = x;
        this.y = y;
        this.z = z;
        this.w = w;
    }

    //public static MyQuat LookRotation(MyTransform mt, MyVector3 a)
    //{
    //    MyQuat rv = new MyQuat(0, 0, 0, 1);
    //    a = MyVector3.normalizeVector(a);
    //    float angle = Mathf.Acos(MyVector3.dotProduct(mt.forward(), a)) * Mathf.Rad2Deg;
    //    return rv;
    //}
}
