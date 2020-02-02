using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBehaviour : MonoBehaviour
{
    public bool isRotate = false;
    public bool isWiggle = false;
    public Vector3 axis = Vector3.up;

    public float rotateSpeed = 50;

    public float wiggleSpeed = 20;
    public float wiggleHeight = 1;
    Vector3 orign;

    private void Start()
    {
        orign = transform.position;

    }
    void Update()
    {
        if (isWiggle)
        {
            WiggleObject();
        }
        if (isRotate)
        {
            RotateObject();
        }
        
    }

    void RotateObject()
    {
        transform.Rotate(transform.InverseTransformVector(axis) * rotateSpeed * Time.deltaTime );
    }

    void WiggleObject()
    {
        transform.position = new Vector3(transform.position.x, (orign + Vector3.up * wiggleHeight * Mathf.Sin(Time.time * wiggleSpeed)).y,transform.position.z);
    }

}
