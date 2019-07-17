using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_Vibration : MonoBehaviour
{
    public byte Hardness = 2;
    public ushort duration = 500;
    public bool VibrateOnlyOnGrab;
    public bool FixedVibration = true;

    bool Vibrated;
    GameObject InteractableObject;
    InteractableJoint InteractableJoint;
    Gestures Gesture;
    GameObject[] Hands;

    public void Start()
    {
        byte[] Hardness = new byte[1];
        InteractableObject = this.gameObject;
        InteractableJoint = InteractableObject.GetComponent<InteractableJoint>();

        if (Gesture == null)
        {
            Hands = GameObject.FindGameObjectsWithTag("Hand Container");
            for (int i = 0; i < 2; i++)
            {
                Gesture = Hands[i].GetComponent<Gestures>();
            }
        }
    }

    public void Vibrate(ushort duration, byte Hardness)
    {
        InteractableJoint.SensoHandExample.VibrateFinger(Senso.EFingerType.Thumb, duration, Hardness);
        InteractableJoint.SensoHandExample.VibrateFinger(Senso.EFingerType.Index, duration, Hardness);
        InteractableJoint.SensoHandExample.VibrateFinger(Senso.EFingerType.Third, duration, Hardness);
        InteractableJoint.SensoHandExample.VibrateFinger(Senso.EFingerType.Middle, duration, Hardness);
        InteractableJoint.SensoHandExample.VibrateFinger(Senso.EFingerType.Little, duration, Hardness);
    }

    void Update()
    {
        if (FixedVibration == false)
            Hardness = Gesture.hard;

        if (InteractableJoint.Grabbed && VibrateOnlyOnGrab && !Vibrated)
        {
            Vibrate(duration, Hardness);
            Vibrated = true;
        }

        else if (InteractableJoint.Grabbed && !VibrateOnlyOnGrab)
        {
            Vibrate(duration, Hardness);
        }

        else if(!InteractableJoint.Grabbed)
        {
            Vibrated = false;
        }
             
    }

}
