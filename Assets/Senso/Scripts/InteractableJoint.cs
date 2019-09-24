using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableJoint : MonoBehaviour
{
    FixedJoint joint;
    Rigidbody rb;
    Rigidbody thisrb;
    Vector3 oldPos;
    public bool Grabbed;
    public bool Pinched;
    [HideInInspector] public Gestures gesture;
    [HideInInspector] public SensoHandExample SensoHandExample;

    [Header("Type of Interaction")]
    public bool Grab;
    public bool Pinch;

    void Update()
    {
        if (gesture != null && ((!gesture.grab && Grabbed) || (!gesture.pinch && Pinched)))
        {
            Clear();
        }

        else
        {
            oldPos = transform.position;
        }
    }

    void Start()
    {
        if (thisrb == null)
            thisrb = gameObject.GetComponent<Rigidbody>();
    }


    public void OnCollisionStay(Collision col)
    {
        if (col.gameObject.GetComponentInParent<SensoHandExample>())
        {
            if (!Grabbed && !Pinched)
            {
                SensoHandExample = col.gameObject.GetComponentInParent<SensoHandExample>();
                gesture = SensoHandExample.gameObject.GetComponent<Gestures>();
            }
            print("tag " + col.gameObject.tag);
            if (!gesture.PinchedOrGrabbed)
            {
                if (((SensoHandExample.HandType == Senso.EPositionType.RightHand) || (SensoHandExample.HandType == Senso.EPositionType.LeftHand)) && gesture.grab && Grab && col.gameObject.tag == "InteractableHand")
                {
                    if (Grabbed == false)
                    {
                        print("grab");
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
                        gesture.PinchedOrGrabbed = true;
                    }
                }

                else if (col.gameObject.tag == "InteractableFinger" && gesture.pinch && Pinch)
                {
                    if (Pinched == false)
                    {
                        print("pinch");
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
                        Pinched = true;
                        gesture.PinchedOrGrabbed = true;
                    }
                }

            }


        }
    }

    void CreateJoint(GameObject col)
    {
        if (!gameObject.TryGetComponent(out joint))
        {
            gameObject.GetComponent<Rigidbody>().useGravity = false;
            joint = gameObject.AddComponent<FixedJoint>();
            joint.connectedBody = col.GetComponent<Rigidbody>();
            joint.transform.position = col.transform.position;
        }

    }

    void Clear()
    {
        Destroy(joint);
        thisrb.useGravity = true;
        //thisrb.AddForce((oldPos - transform.position) * Time.deltaTime, ForceMode.Force); Throwing in progress
        Grabbed = false;
        Pinched = false;
        rb = null;
        gesture.PinchedOrGrabbed = false;
    }
}
