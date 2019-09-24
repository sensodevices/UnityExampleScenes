using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTo : MonoBehaviour
{
    public float maxDistance;
    public LayerMask layerMask;

    private Vector3 origin;
    private Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(Raycast());
    }

    IEnumerator Raycast()
    {
        origin = transform.position;
        direction = transform.TransformDirection(Vector3.down);
        RaycastHit hit;
        if (Physics.Raycast(origin, direction, out hit, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal))
        {
            Debug.DrawRay(origin, direction * hit.distance, Color.yellow);
            print(hit.distance);
            if (hit.distance > 0.01)
            {
                hit.transform.position = transform.position;
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
