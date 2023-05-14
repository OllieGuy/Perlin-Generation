using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MyMatrix3by3
{
    public float[,] values;
    public static MyMatrix3by3 Identity
    {
        get
        {
            return new MyMatrix3by3(new MyVector3(1, 0, 0), new MyVector3(0, 1, 0), new MyVector3(0, 0, 1));
        }
    }
    public MyMatrix3by3(MyVector3 col1, MyVector3 col2, MyVector3 col3)
    {
        values = new float[4, 4];

        values[0, 0] = col1.x;
        values[1, 0] = col1.y;
        values[2, 0] = col1.z;

        values[0, 1] = col2.x;
        values[1, 1] = col2.y;
        values[2, 1] = col2.z;

        values[0, 2] = col3.x;
        values[1, 2] = col3.y;
        values[2, 2] = col3.z;
    }

    public static MyVector3 operator *(MyMatrix3by3 lhs, MyVector3 rhs)
    {
        MyVector3 rv = new MyVector3(0, 0, 0);

        rv.x = lhs.values[0, 0] * rhs.x + lhs.values[0, 1] * rhs.y + lhs.values[0, 2] * rhs.z;
        rv.y = lhs.values[1, 0] * rhs.x + lhs.values[1, 1] * rhs.y + lhs.values[1, 2] * rhs.z;
        rv.z = lhs.values[2, 0] * rhs.x + lhs.values[2, 1] * rhs.y + lhs.values[2, 2] * rhs.z;

        return rv;
    }
    public static MyMatrix3by3 operator *(MyMatrix3by3 lhs, MyMatrix3by3 rhs)
    {
        MyMatrix3by3 rm = Identity;

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

    public MyMatrix3by3 TranslationInverse()
    {
        MyMatrix3by3 rm = Identity;

        for (int i = 0; i < 3; i++)
        {
            rm.values[i, 3] = -values[i, 3];
        }
        return rm;
    }

    public MyMatrix3by3 scale(MyVector3 s)
    {
        MyMatrix3by3 rm = Identity;
        rm.values[0, 0] = values[0, 0] * s.x;
        rm.values[1, 1] = values[1, 1] * s.y;
        rm.values[2, 2] = values[2, 2] * s.z;
        return rm;
    }

    public MyMatrix3by3 translate(MyVector3 s)
    {
        MyMatrix3by3 rm = Identity;
        rm.values[0, 0] = values[0, 0] * s.x;
        rm.values[1, 1] = values[1, 1] * s.y;
        rm.values[2, 2] = values[2, 2] * s.z;
        return rm;
    }
}

