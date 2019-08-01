using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable: MonoBehaviour
{
    [HideInInspector]public GameObject InteractableObject;
    [HideInInspector]public string HandType;
    [HideInInspector]public SensoHandExample SensoHandExample;
    [HideInInspector]public Gestures gesture;
    [HideInInspector]public Interactable_Vibration vibration;
    [HideInInspector]public bool Grabbed;
    [HideInInspector]public bool Pinched;
    [Header("Type of Interaction")]
    public bool Grab;
    public bool Pinch;
    
   public void Start()
    {
        InteractableObject = this.gameObject;

        if(vibration == null)
            vibration = InteractableObject.GetComponent<Interactable_Vibration>();

    }

    public void OnTriggerStay(Collider col)
    {
        if (col.gameObject.GetComponentInParent<SensoHandExample>())
        {
            SensoHandExample = col.gameObject.GetComponentInParent<SensoHandExample>();
            gesture = SensoHandExample.gameObject.GetComponent<Gestures>();
            if (((SensoHandExample.HandType == Senso.EPositionType.RightHand) || (SensoHandExample.HandType == Senso.EPositionType.LeftHand)) && gesture.grab && Grab)
            {
                InteractableObject.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                InteractableObject.transform.position = col.transform.position;
                InteractableObject.transform.rotation = col.transform.rotation;
                Grabbed = true;
            }

            if (col.gameObject.tag == "InteractableFinger" && Pinch)
            {
                InteractableObject.transform.position = Vector3.Lerp(SensoHandExample.indexBones[2].position, SensoHandExample.thumbBones[2].position, 0.5f);
                InteractableObject.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                Pinched = true;
            }

            else if (gesture.pinch == false)
            {
                InteractableObject.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                Pinched = false;
            }
        }


        else
        {
            InteractableObject.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            Grabbed = false;
            Pinched = false;
        }
    }





 

}
