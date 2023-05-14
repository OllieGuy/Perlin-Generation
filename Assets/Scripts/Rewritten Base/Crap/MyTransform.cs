using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MyTransform
{
    public MyVector3 position;
    public Quaternion rotation;
    public MyVector3 scale;

    public MyTransform(MyVector3 position, Quaternion rotation, MyVector3 scale)
    {
        this.position = position;
        this.rotation = rotation;
        this.scale = scale;
    }

    public void Translate(MyVector3 translation)
    {
        position += translation;
    }

    public void Rotate(MyVector3 axis, float angle)
    {
        //rotation *= Quaternion.AngleAxis(angle, axis);
    }

    public void Scale(MyVector3 scale)
    {
        this.scale.x *= scale.x;
        this.scale.y *= scale.y;
        this.scale.z *= scale.z;
    }

    //public void lookAt(MyTransform target)
    //{
    //    MyVector3 direction = target.position - position;
    //    //MyQuat r = MyQuat.LookRotation(direction);
    //    rotation = r;
    //}

    //public MyVector3 forward()
    //{
    //    MyVector3 rv = new MyVector3(0, 0, 0);
    //    rv = new MyVector3(0, 0, 1) * rotation;
    //}
}