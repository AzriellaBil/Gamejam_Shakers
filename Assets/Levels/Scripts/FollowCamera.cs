using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{

    [SerializeField] private Vector3 offset;
    [SerializeField] private float damping;

    public Transform target;

    private Vector3 vel = Vector3.zero;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        Vector3 targetPosition = target.position + offset;
        targetPosition.z = transform.position.z;

        transform.position = targetPosition;
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 targetPosition = target.position + offset;
        targetPosition.z = transform.position.z;
        targetPosition.y = transform.position.y;

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref vel, damping);
        
    }
}
