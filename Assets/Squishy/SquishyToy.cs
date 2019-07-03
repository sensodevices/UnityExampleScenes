using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    
    public class SquishyToy : MonoBehaviour
    {
        public GameObject InteractableObject;
        public Interactable interactable;
        public new SkinnedMeshRenderer renderer;
        public Gestures gesture;
        public bool affectMaterial = true;
        



        private new Rigidbody rigidbody;

        private void Start()
        {
            InteractableObject = this.gameObject;
            interactable = this.gameObject.GetComponent<Interactable>();

            if (rigidbody == null)
                rigidbody = GetComponent<Rigidbody>();

            if (renderer == null)
                renderer = GetComponent<SkinnedMeshRenderer>();
        }

        private void Update()
        {
            if (interactable.Grabbed)
            {
                gesture = interactable.gesture;
                
                renderer.SetBlendShapeWeight(0, Mathf.Lerp(renderer.GetBlendShapeWeight(0), gesture.hard * 10, Time.deltaTime * 10));

                if (renderer.sharedMesh.blendShapeCount > 1) // make sure there's a pinch blend shape
                {
                    renderer.SetBlendShapeWeight(1, Mathf.Lerp(renderer.GetBlendShapeWeight(1), gesture.hard * 10, Time.deltaTime * 10));
                }

                if (affectMaterial)
                {
                    renderer.material.SetFloat("_Deform", Mathf.Pow(gesture.hard * 1f, 0.5f));
                    if (renderer.material.HasProperty("_PinchDeform"))
                    {
                        renderer.material.SetFloat("_PinchDeform", Mathf.Pow(gesture.hard * 1f, 0.5f));
                    }
                }
                        
                    
                
            }

        }

    }