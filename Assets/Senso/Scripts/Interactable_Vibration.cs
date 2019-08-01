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

    public void Start()
    {
        InteractableObject = this.gameObject;
        InteractableJoint = InteractableObject.GetComponent<InteractableJoint>();
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
        if (InteractableJoint.gesture != null)
        {
            if (FixedVibration == false)
                Hardness = InteractableJoint.gesture.hard;

            if (InteractableJoint.Grabbed && VibrateOnlyOnGrab && !Vibrated)
            {
                Vibrate(duration, Hardness);
                Vibrated = true;
            }

            else if (InteractableJoint.Grabbed && !VibrateOnlyOnGrab)
                Vibrate(duration, Hardness);

            else if (!InteractableJoint.Grabbed)
                Vibrated = false;
        }             
    }

}
