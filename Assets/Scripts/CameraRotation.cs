using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CameraRotation : MonoBehaviour
{
    //MyTransform mt;
    public bool staticCam = true;
    bool switchedCam = false;
    public GameObject target;
    public float radius = 50f;
    public float speed = 25f;
    public float yOffset = 50f;
    private float angle = 0f;
    GroundMeshSetup gms;

    private void Start()
    {
        gms = target.GetComponent<GroundMeshSetup>();
    }

    void Update()
    {
        if (!staticCam && gms.centreOfMesh != null)
        {
            switchedCam = true;
            //MyVector3 pos = gms.centreOfMesh + new MyVector3(0, yOffset, radius);
            Vector3 pos = gms.centreOfMesh.convToVec3() + Quaternion.Euler(0, angle, 0) * new Vector3(0, yOffset, radius);

            //mt.position = pos;
            transform.position = pos;

            transform.LookAt(gms.centreOfMesh.convToVec3());
            //Vector3 angleToLook = MyMath.EulerAngToDir(gms.centreOfMesh - new MyVector3(transform.position)).convToVec3();
            //transform.rotation = Quaternion.Euler(angleToLook.x, angleToLook.y, angleToLook.z);

            angle += speed * Time.deltaTime;
        }
        else if (staticCam && switchedCam)
        {
            switchedCam = false;
            resetToStatic();
        }
    }

    public void updateCamValues()
    {
        Slider radiusSlider = GameObject.Find("radius Slider").GetComponent<Slider>();
        radius = radiusSlider.value;
        Slider speedSlider = GameObject.Find("speed Slider").GetComponent<Slider>();
        speed = speedSlider.value;
        Slider offsetSlider = GameObject.Find("offset Slider").GetComponent<Slider>();
        yOffset = offsetSlider.value;
    }
    public void resetToStatic()
    {
        transform.position = new Vector3(0,10,0);
        transform.rotation = new Quaternion(0.70711f,0,0,0.70711f);
    }

    //void camRot(float ang)
    //{
    //    MyMatrix4by4 rollMatrix = new MyMatrix4by4(
    //        new MyVector3(Mathf.Cos(ang), Mathf.Sin(ang), 0),
    //        new MyVector3(-Mathf.Sin(ang), Mathf.Cos(ang), 0),
    //        new MyVector3(0, 0, 1),
    //        new MyVector3(0, 0, 0));
    //    MyMatrix4by4 pitchMatrix = new MyMatrix4by4(
    //        new MyVector3(1, 0, 0),
    //        new MyVector3(Mathf.Cos(ang), Mathf.Sin(ang), 0),
    //        new MyVector3(-Mathf.Sin(ang), Mathf.Cos(ang), 0),
    //        new MyVector3(0, 0, 0));
    //    MyMatrix4by4 yawMatrix = new MyMatrix4by4(
    //        new MyVector3(Mathf.Cos(ang), Mathf.Sin(ang), 0),
    //        new MyVector3(0, 1, 0),
    //        new MyVector3(-Mathf.Sin(ang), Mathf.Cos(ang), 0),
    //        new MyVector3(0, 0, 0));
    //    MyVector3 rolledVert = rollMatrix * new MyVector3(0, 0, 0);
    //}
}
