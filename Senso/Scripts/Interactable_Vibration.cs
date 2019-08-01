using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_Vibration : MonoBehaviour
{
    public byte Hardness = 2;
    public ushort duration = 500;
    public bool VibrateOnce;
    public bool FixedVibration = true;

    bool Vibrated;
    GameObject InteractableObject;
    InteractableJoint InteractableJoint;

    public void Start()
    {
        InteractableObject = this.gameObject;
        InteractableJoint = InteractableObject.GetComponent<InteractableJoint>();
    }

    public void GrabVibrate(ushort duration, byte Hardness)
    {
        InteractableJoint.SensoHandExample.VibrateFinger(Senso.EFingerType.Thumb, duration, Hardness);
        InteractableJoint.SensoHandExample.VibrateFinger(Senso.EFingerType.Index, duration, Hardness);
        InteractableJoint.SensoHandExample.VibrateFinger(Senso.EFingerType.Third, duration, Hardness);
        InteractableJoint.SensoHandExample.VibrateFinger(Senso.EFingerType.Middle, duration, Hardness);
        InteractableJoint.SensoHandExample.VibrateFinger(Senso.EFingerType.Little, duration, Hardness);
    }

    public void PinchVibrate(ushort duration, byte Hardness)
    {
        InteractableJoint.SensoHandExample.VibrateFinger(Senso.EFingerType.Thumb, duration, Hardness);
        InteractableJoint.SensoHandExample.VibrateFinger(Senso.EFingerType.Index, duration, Hardness);
    }

    void Update()
    {
        if (InteractableJoint.gesture != null)
        {
            if (InteractableJoint.Grab)
            {
                if (FixedVibration == false)
                    Hardness = InteractableJoint.gesture.hard;

                if (InteractableJoint.Grabbed && VibrateOnce && !Vibrated)
                {
                    GrabVibrate(duration, Hardness);
                    Vibrated = true;
                }

                else if (InteractableJoint.Grabbed && !VibrateOnce)
                    GrabVibrate(duration, Hardness);

                else if (!InteractableJoint.Grabbed)
                    Vibrated = false;
            }

            else if (InteractableJoint.Pinch)
            {
                if (InteractableJoint.Pinched && VibrateOnce && !Vibrated)
                {
                    PinchVibrate(duration, Hardness);
                    Vibrated = true;
                }

                else if (InteractableJoint.Pinched && !VibrateOnce)
                    PinchVibrate(duration, Hardness);

                else if (!InteractableJoint.Pinched)
                    Vibrated = false;
            }
        }
    }

}
