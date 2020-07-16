using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    public Transform target;
    void LateUpdate()
    {
        transform.LookAt(target);
    }
}
