using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// VR button event. Used for registering callbacks in the editor
/// </summary>
[System.Serializable]
public class VRButtonEvent : UnityEvent<VRButton> { }

/// <summary>
/// VR Button. Behaves like a UI button, but exists as a physical button for you to push in VR
/// </summary>
public class VRButton : VRInteractable
{

    /// <summary>
    /// Callbacks for button pressed event
    /// </summary>
    public VRButtonEvent ButtonListeners;

    /// <summary>
    /// Controllers currently interacting with the button
    /// </summary>
    public List<SensoHandExample> ActiveControllers = new List<SensoHandExample>();
    public List<string> ActiveFingers = new List<string>();
    public int ButtonNumber;

    void OnTriggerEnter(Collider _collider)
    {
        if (Interactable == true && _collider.name == "Switch")
        { // If the button hit's the contact switch it has been pressed
            TriggerButton();
        }
    }

    void OnCollisionEnter(Collision _collision)
    {

        if (Interactable == true && _collision.collider.name == "Switch")
        {
            TriggerButton(); // If the button hit's the contact switch it has been pressed
        }
        else if (_collision.rigidbody == null)
            return;

        var InteractionObj = _collision.rigidbody.GetComponentInParent<SensoHandExample>();
        string VibroFinger = _collision.gameObject.name;


        if (InteractionObj != null) // If we find a controller add it to our interacting list
        {
            try
            {
                if (InteractionObj.name != (ActiveControllers[ActiveControllers.Count - 1]).name)
                {
                    ActiveControllers.Add(InteractionObj);
                }
            }
            catch { ActiveControllers.Add(InteractionObj); }
            finally
            {
                ActiveFingers.Add(VibroFinger);
            }
        }
    }

    void OnCollisionExit(Collision _collision)
    {
        if (_collision.rigidbody == null)
            return;

        var InteractionObj = _collision.rigidbody.GetComponentInParent<SensoHandExample>();
        string VibroFinger = _collision.gameObject.name;

        if (InteractionObj != null)
        {
            ActiveControllers.Remove(InteractionObj);
            ActiveFingers.Remove(VibroFinger);
        }

    }

    public float TriggerHapticStrength = 0.5f;

    void TriggerButton()
    {
        if (Interactable == false)
            return;

        if (ButtonListeners != null)
        { // Trigger our callbacks
            ButtonListeners.Invoke(this);
        }

        foreach (SensoHandExample InteractionObj in ActiveControllers)
        { // Trigger a response on any active controllers
            foreach (string VibroFinger in ActiveFingers)
            {
                Senso.EFingerType finger = (Senso.EFingerType)Enum.Parse(typeof(Senso.EFingerType), VibroFinger);
                switch (ButtonNumber)
                {
                    case 1: InteractionObj.VibrateFinger(finger, 500, 5); break;
                    case 2: break;
                    case 3: break;
                    case 4: break;
                    case 5: break;
                    case 6: break;
                    case 7: break;
                    case 8: break;
                    case 9: break;
                    case 10: break;
                    default: InteractionObj.VibrateFinger(finger, 500, 5); break;
                }
            }

        }
    }

    //  This is just here as a reminder to call base. Will remove the need to do this in future versions
    //	void Update()
    //	{
    //		base.Update ();
    //	}
}
