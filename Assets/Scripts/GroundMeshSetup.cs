using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

[RequireComponent(typeof(MeshFilter))]
public class GroundMeshSetup : MonoBehaviour
{
    public MyVector3 centreOfMesh;
    MyVector3[] vertices;
    int[] tris;
    Vector2[] uvs;
    Mesh mesh;
    [SerializeField] GameObject PGO;
    [SerializeField] GameObject heightPoint;
    PerlinGeneration PG;
    Texture2D texture;
    public bool drawNow;
    MyVector3 curLargestHeightPos;

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
            Destroy(GameObject.Find("High Point(Clone)"));
            draw();
            UpdateMesh();
            texture = PG.returnTexture();
            GetComponent<Renderer>().material.mainTexture = texture;
            texture.Apply();
            Instantiate(heightPoint,new Vector3(curLargestHeightPos.x,curLargestHeightPos.y * 1.2f,curLargestHeightPos.z), new Quaternion(0,0,0,0)); //replace hard coded number
            drawNow = false;
        }
    }
    void draw()
    {
        float[,] heightMap = PG.returnHeightMap();
        int xSize = heightMap.GetLength(0);
        int zSize = heightMap.GetLength(1);
        int i = 0;
        vertices = new MyVector3[xSize * zSize];
        float curLargestHeight = 0;
        for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                vertices[i] = new MyVector3(x, heightMap[x, z], z);
                if(heightMap[x, z] > curLargestHeight)
                {
                    curLargestHeight = heightMap[x, z];
                    curLargestHeightPos = new MyVector3(x,curLargestHeight,z);
                    Debug.Log(curLargestHeight);
                }
                i++;
            }
        }
        tris = calcTris(heightMap);
        mesh.RecalculateNormals();
        int size = xSize * zSize;
        uvs = new Vector2[size];
        int uvScale = xSize;
        for (int u = 0; u < size; u++)
        {
            Vector3 vertex = vertices[u].convToVec3();
            uvs[u] = (new Vector2(vertex.x, vertex.z) / uvScale);
        }
        centreOfMesh = new MyVector3(xSize / 2, 0, zSize / 2);
    }

    int[] calcTris(float[,] heightMap)
    {
        int rows = heightMap.GetLength(0);
        int cols = heightMap.GetLength(1);
        int numTris = (rows - 1) * (cols - 1) * 6;

        int[] triReturn = new int[numTris];

        int triIndex = 0;
        for (int row = 0; row < rows - 1; row++)
        {
            for (int col = 0; col < cols - 1; col++)
            {
                int tl = row * cols + col;
                int tr = tl + 1;
                int bl = (row + 1) * cols + col;
                int br = bl + 1;

                triReturn[triIndex++] = tl;
                triReturn[triIndex++] = bl;
                triReturn[triIndex++] = tr;

                triReturn[triIndex++] = tr;
                triReturn[triIndex++] = bl;
                triReturn[triIndex++] = br;
            }
        }
        return triReturn;
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
        mesh.triangles = tris;
        mesh.uv = uvs;
    }
}