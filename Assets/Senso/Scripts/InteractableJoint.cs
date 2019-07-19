using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableJoint : MonoBehaviour
{
    GameObject InteractableObject;    
    FixedJoint joint;
    Rigidbody rb;
    [HideInInspector] public bool Grabbed;
    [HideInInspector] public bool Pinched;
    [HideInInspector] public Gestures gesture;
    [HideInInspector] public SensoHandExample SensoHandExample;

    [Header("Type of Interaction")]
    public bool Grab;
    public bool Pinch;


    public void Start()
    {
        InteractableObject = this.gameObject;
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
                    if (col.gameObject.TryGetComponent(out rb))
                    {
                        rb.isKinematic = true;
                        rb.useGravity = false;
                    }

                    else
                    {
                        rb = col.gameObject.AddComponent<Rigidbody>();
                        rb.isKinematic = true;
                        rb.useGravity = false;
                    }
                    CreateJoint(col.gameObject);
                    Grabbed = true;
                }
            }

            else if (col.gameObject.tag == "InteractableFinger" && gesture.pinch && Pinch)
            {
                if (Pinched == false)
                {
                    rb = col.gameObject.AddComponent<Rigidbody>();
                    rb.isKinematic = true;
                    rb.useGravity = false;
                    CreateJoint(col.gameObject);
                    Pinched = true;
                }
            }

            
        }

        else if(gesture != null && ((!gesture.grab && Grabbed) || (!gesture.pinch && Pinched)))
            {
                Clear(col.gameObject);
            }
    }

    void CreateJoint(GameObject col)
    {
        if (!InteractableObject.TryGetComponent(out joint))
        {
            joint = InteractableObject.AddComponent<FixedJoint>();
            joint.connectedBody = col.GetComponent<Rigidbody>();
        }

    }

    void Clear(GameObject col)
    {
        Destroy(joint);
        Grabbed = false;
        Pinched = false;
        rb = null;
    }






}
