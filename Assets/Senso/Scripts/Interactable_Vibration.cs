using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_Vibration : MonoBehaviour
{
    public byte Hardness = 2;
    public ushort duration = 500;
    public bool IsVibrate;
    public bool VibrateOnlyOnGrab;
    public bool FixedVibration = true;
    [HideInInspector]
    public bool Vibrated;
    [HideInInspector]
    public GameObject InteractableObject;
    [HideInInspector]
    public Interactable Interactable;
    [HideInInspector]
    public Gestures Gesture;
    [HideInInspector]
    public GameObject[] Hands;

    public void Start()
    {
        byte[] Hardness = new byte[1];
        InteractableObject = this.gameObject;
        Interactable = InteractableObject.GetComponent<Interactable>();

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
        Interactable.SensoHandExample.VibrateFinger(Senso.EFingerType.Thumb, duration, Hardness);
        Interactable.SensoHandExample.VibrateFinger(Senso.EFingerType.Index, duration, Hardness);
        Interactable.SensoHandExample.VibrateFinger(Senso.EFingerType.Third, duration, Hardness);
        Interactable.SensoHandExample.VibrateFinger(Senso.EFingerType.Middle, duration, Hardness);
        Interactable.SensoHandExample.VibrateFinger(Senso.EFingerType.Little, duration, Hardness);
    }

    void Update()
    {
        if (FixedVibration == false)
            Hardness = Gesture.hard;

        if (Interactable.Grabbed && VibrateOnlyOnGrab && IsVibrate && Vibrated == false)
        {
            Vibrate(duration, Hardness);
            Vibrated = true;
        }

        else if (Interactable.Grabbed && IsVibrate && VibrateOnlyOnGrab == false)
        {
            Vibrate(duration, Hardness);
        }

        else if(Interactable.Grabbed == false)
        {
            Vibrated = false;
        }
             
    }

}
