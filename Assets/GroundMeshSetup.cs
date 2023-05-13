using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(MeshFilter))]
public class GroundMeshSetup : MonoBehaviour
{
    int col, row;
    int scale = 20;
    MyVector3[] vertices;
    int[] tris;
    Mesh mesh;
    [SerializeField] GameObject PGO;
    PerlinGeneration PG;
    Texture2D texture;
    public bool drawNow;
    public Material mat;

    void Start()
    {
        PG = PGO.GetComponent<PerlinGeneration>();
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
    }
    void Update()
    {
        if (drawNow)
        {
            draw();
            UpdateMesh();
            texture = PG.returnTexture();
            //GetComponent<Renderer>().material.mainTexture = texture;
            GetComponent<Renderer>().material = mat;
            texture.Apply();
            drawNow = false;
        }
    }
    void draw()
    {
        float[,] heightMap = PG.returnHeightMap();
        int xSize = heightMap.GetLength(0);
        int zSize = heightMap.GetLength(1);
        int i = 0;
        //vertices = new MyVector3[(heightMap.GetLength(0)+1) * (heightMap.GetLength(1)+1)];
        vertices = new MyVector3[xSize * zSize];
        for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                vertices[i] = new MyVector3(x * 0.2f, heightMap[x, z], z * 0.2f);
                i++;
            }
        }

        tris = new int[xSize * zSize * 6];
        int curVert = 0;
        int curTriSet = 0;
        for (int x = 0; x < (xSize - 1) * (zSize - 1); x++)
        {
            tris[curTriSet + 0] = curVert + 0;
            tris[curTriSet + 1] = curVert + xSize + 1;
            tris[curTriSet + 2] = curVert + 1;

            tris[curTriSet + 3] = curVert + 1;
            tris[curTriSet + 4] = curVert + xSize + 1;
            tris[curTriSet + 5] = curVert + xSize + 2;

            curVert++;
            curTriSet += 6;
        }
    }

    void UpdateMesh()
    {
        mesh.Clear();
        Vector3[] unityVertices = new Vector3[vertices.Length];
        int i = 0;
        foreach (MyVector3 v in vertices)
        {
            unityVertices[i] = v.convToVec3();
            i++;
        }
        mesh.vertices = unityVertices;
        //mesh.triangles = tris;
    }

    private void OnDrawGizmos()
    {
        if (vertices == null)
        {
            return;
        }
        for (int i = 0; i < vertices.Length; i++)
        {
            Gizmos.DrawSphere(vertices[i].convToVec3(), .1f);
        }
    }

}