using UnityEngine;
using System.Collections;

public class SmoothCamera : MonoBehaviour
{

    public float dampTime = 15f;
    private Vector3 velocity = Vector3.zero;
    public Transform target;
    public Camera myCamera;



    void Awake()
    {
        myCamera = GetComponent<Camera>();
    }
    void Update()
    {
        if (target)
        {
            Vector3 point = myCamera.WorldToViewportPoint(target.position);
            Vector3 delta = target.position - myCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
            Vector3 destination = transform.position + delta;
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime * Time.deltaTime);
        }

    }
}
