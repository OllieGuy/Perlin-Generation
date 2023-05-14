using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Flag : MonoBehaviour
{
    GameObject cam;
    MyVector3[] msv;
    MeshFilter mf;
    void Start()
    {
        cam = GameObject.Find("Main Camera");
        mf = GetComponent<MeshFilter>();
        msv = new MyVector3[mf.mesh.vertices.Length];
        int i = 0;
        foreach (Vector3 v in mf.mesh.vertices)
        {
            msv[i] = new MyVector3(v);
            i++;
        }
    }

    void Update()
    {
        MyVector3 F = new MyVector3(transform.position) - new MyVector3(cam.transform.position);

        MyVector3 R = MyVector3.crossProduct(new MyVector3(0, 1, 0), F);

        MyVector3 U = MyVector3.crossProduct(F, R);

        MyVector3 fNorm = MyVector3.normalizeVector(F);
        MyVector3 rNorm = MyVector3.normalizeVector(R);
        MyVector3 uNorm = MyVector3.normalizeVector(U);
        MyVector3[] transformedVerts = new MyVector3[msv.Length];
        MyMatrix3by3 rotMatrix = new MyMatrix3by3(rNorm * -1, fNorm * -1, uNorm * -1);
        for (int t = 0; t < transformedVerts.Length; t++)
        {
            transformedVerts[t] = rotMatrix * msv[t];
        }
        Vector3[] mvToV3verts = new Vector3[transformedVerts.Length];
        int i = 0;
        foreach (MyVector3 v in transformedVerts)
        {
            mvToV3verts[i] = v.convToVec3();
            i++;
        }
        mf.mesh.vertices = mvToV3verts;
        mf.mesh.RecalculateNormals();
        mf.mesh.RecalculateBounds();
    }
}
