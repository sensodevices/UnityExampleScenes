using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gestures : MonoBehaviour
{

    public SensoHandExample SensoHandExample;
    public Senso.HandData HandData;
    public byte hard;
    public GameObject SphereCollider;
    float hardness;
    float hardnesschar;
    Rigidbody rb;
    [HideInInspector]public bool PinchedOrGrabbed = false;
    [Header("Gesture")]
    public bool grab;
    public bool pinch;
    public bool Vgesture;

    void Start()
    {
        SphereCollider.SetActive(false);
        if(SensoHandExample == null)
            SensoHandExample = this.gameObject.GetComponent<SensoHandExample>();        
    }

    void Update()
    {
        GrabGesture();
        PinchGesture();
        VGesture();
    }

    public void GrabGesture()
    {
        if (SensoHandExample.indexBones[2].localRotation.x <= -0.21 && SensoHandExample.thirdBones[2].localRotation.x <= -0.21 && SensoHandExample.middleBones[2].localRotation.x <= -0.21 && SensoHandExample.littleBones[2].localRotation.x <= 0.21)
        {
            SphereCollider.SetActive(true);
            grab = true;
            hardness = Mathf.Abs((SensoHandExample.indexBones[2].localRotation.x + SensoHandExample.thirdBones[2].localRotation.x + SensoHandExample.middleBones[2].localRotation.x + SensoHandExample.littleBones[2].localRotation.x) / 4);
        }

        else
        {
            if (SphereCollider.TryGetComponent<Rigidbody>(out rb))
                Destroy(rb);
            SphereCollider.SetActive(false);
            grab = false;
        }

        hardnesschar = hardness * 10;

        switch ((int)hardnesschar) //convert hardness value to byte for vibration
        {
            case 2:
                hard = 2; break;
            case 3:
                hard = 3; break;
            case 4:
                hard = 4; break;
            case 5:
                hard = 5; break;
            case 6:
                hard = 6; break;
            case 7:
                hard = 7; break;
            case 8:
                hard = 8; break;
            case 9:
                hard = 9; break;
            default:
                hard = 2; break;
        }
    }

    public void PinchGesture()
    {
        if (SensoHandExample.thumbBones[2].localRotation.x >= 0.07 && SensoHandExample.indexBones[2].localRotation.x <= -0.4)
            pinch = true;
        
        else
            pinch = false;
    }

    public void VGesture()
    {
        if (SensoHandExample.thirdBones[2].localRotation.x <= -0.2 &&  SensoHandExample.littleBones[2].localRotation.x <= 0.2 && SensoHandExample.indexBones[2].localRotation.x >= -0.2 && SensoHandExample.middleBones[2].localRotation.x >= -0.2)
            Vgesture = true;

        else
            Vgesture = false;
    }

    


}
