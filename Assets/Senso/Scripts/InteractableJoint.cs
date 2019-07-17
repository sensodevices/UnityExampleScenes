using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableJoint : MonoBehaviour
{
    GameObject InteractableObject;
    Gestures gesture;
    FixedJoint joint;
    Interactable_Vibration vibration;
    [HideInInspector] public bool Grabbed;
    [HideInInspector] public bool Pinched;
    [HideInInspector] public SensoHandExample SensoHandExample;
    [Header("Type of Interaction")]
    public bool Grab;
    public bool Pinch;


    public void Start()
    {
        InteractableObject = this.gameObject;

        if (vibration == null)
            vibration = InteractableObject.GetComponent<Interactable_Vibration>();

    }


    public void OnCollisionStay(Collision col)
    {
        if (col.gameObject.GetComponentInParent<SensoHandExample>())
        {
            SensoHandExample = col.gameObject.GetComponentInParent<SensoHandExample>();
            gesture = SensoHandExample.gameObject.GetComponent<Gestures>();
            if (((SensoHandExample.HandType == Senso.EPositionType.RightHand) || (SensoHandExample.HandType == Senso.EPositionType.LeftHand)) && gesture.grab && Grab)
            {
                if (Grabbed == false)
                {                             
                    Rigidbody rb = col.gameObject.AddComponent<Rigidbody>();
                    rb.isKinematic = true;
                    rb.useGravity = false;
                    CreateJoint(col.gameObject);
                    Grabbed = true;
                }
            }

            if (col.gameObject.tag == "InteractableFinger" && gesture.pinch && Pinch)
            {
                if (Pinched == false)
                {
                    Rigidbody rb = col.gameObject.AddComponent<Rigidbody>();
                    rb.isKinematic = true;
                    rb.useGravity = false;
                    CreateJoint(col.gameObject);
                    Pinched = true;
                }
            }
        }

        else if(gesture != null && ((!gesture.grab && Grabbed) || (!gesture.pinch && Pinched)))
        {
            Destroy(joint);
            Grabbed = false;
            Pinched = false;
            Destroy(col.rigidbody);
        }
    }

    void CreateJoint(GameObject col)
    {
        if (!InteractableObject.TryGetComponent<FixedJoint>(out joint))
        {
            joint = InteractableObject.AddComponent<FixedJoint>();
            joint.connectedBody = col.GetComponent<Rigidbody>();
        }

    }






}
