using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    public Vector3 offSet;
    public Transform target;
    public float speed;

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + offSet, speed * Time.deltaTime);
    }
}
