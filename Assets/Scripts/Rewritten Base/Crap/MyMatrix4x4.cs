using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MyMatrix4by4
{
    public float[,] values;
    public static MyMatrix4by4 Identity
    {
        get
        {
            return new MyMatrix4by4(new MyVector4(1, 0, 0, 0), new MyVector4(0, 1, 0, 0), new MyVector4(0, 0, 1, 0), new MyVector4(0, 0, 0, 1));
        }
    }
    public MyMatrix4by4(MyVector4 col1, MyVector4 col2, MyVector4 col3, MyVector4 col4)
    {
        values = new float[4, 4];

        values[0, 0] = col1.x;
        values[1, 0] = col1.y;
        values[2, 0] = col1.z;
        values[3, 0] = col1.w;

        values[0, 1] = col2.x;
        values[1, 1] = col2.y;
        values[2, 1] = col2.z;
        values[3, 1] = col2.w;

        values[0, 2] = col3.x;
        values[1, 2] = col3.y;
        values[2, 2] = col3.z;
        values[3, 2] = col3.w;

        values[0, 3] = col4.x;
        values[1, 3] = col4.y;
        values[2, 3] = col4.z;
        values[3, 3] = col4.w;
    }
    public MyMatrix4by4(MyVector3 col1, MyVector3 col2, MyVector3 col3, MyVector3 col4)
    {
        values = new float[4, 4];

        values[0, 0] = col1.x;
        values[1, 0] = col1.y;
        values[2, 0] = col1.z;
        values[3, 0] = 0;

        values[0, 1] = col2.x;
        values[1, 1] = col2.y;
        values[2, 1] = col2.z;
        values[3, 1] = 0;

        values[0, 2] = col3.x;
        values[1, 2] = col3.y;
        values[2, 2] = col3.z;
        values[3, 2] = 0;

        values[0, 3] = col4.x;
        values[1, 3] = col4.y;
        values[2, 3] = col4.z;
        values[3, 3] = 1;
    }

    public static MyVector4 operator *(MyMatrix4by4 lhs, MyVector4 rhs)
    {
        MyVector4 rv = new MyVector4(0, 0, 0, 0);

        rv.x = lhs.values[0, 0] * rhs.x + lhs.values[0, 1] * rhs.y + lhs.values[0, 2] * rhs.z + lhs.values[0, 3] * rhs.w;
        rv.y = lhs.values[1, 0] * rhs.x + lhs.values[1, 1] * rhs.y + lhs.values[1, 2] * rhs.z + lhs.values[1, 3] * rhs.w;
        rv.z = lhs.values[2, 0] * rhs.x + lhs.values[2, 1] * rhs.y + lhs.values[2, 2] * rhs.z + lhs.values[2, 3] * rhs.w;
        rv.w = lhs.values[3, 0] * rhs.x + lhs.values[3, 1] * rhs.y + lhs.values[3, 2] * rhs.z + lhs.values[3, 3] * rhs.w;

        return rv;
    }
    public static MyMatrix4by4 operator *(MyMatrix4by4 lhs, MyMatrix4by4 rhs)
    {
        MyMatrix4by4 rm = Identity;

        float temp = 0;
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                for (int k = 0; k < 4; k++)
                {
                    temp += lhs.values[i, k] * rhs.values[k, j];
                }
                rm.values[i, j] = temp;
                temp = 0;
            }
        }

        return rm;
    }

    public MyMatrix4by4 TranslationInverse()
    {
        MyMatrix4by4 rm = Identity;

        for (int i = 0; i < 3; i++)
        {
            rm.values[i, 3] = -values[i, 3];
        }
        return rm;
    }

    public MyMatrix4by4 scale(MyVector3 s)
    {
        MyMatrix4by4 rm = Identity;
        rm.values[0, 0] = values[0, 0] * s.x;
        rm.values[1, 1] = values[1, 1] * s.y;
        rm.values[2, 2] = values[2, 2] * s.z;
        return rm;
    }

    public MyMatrix4by4 translate(MyVector3 s)
    {
        MyMatrix4by4 rm = Identity;
        rm.values[0, 0] = values[0, 0] * s.x;
        rm.values[1, 1] = values[1, 1] * s.y;
        rm.values[2, 2] = values[2, 2] * s.z;
        return rm;
    }
}

