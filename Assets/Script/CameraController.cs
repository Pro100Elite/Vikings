using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameObject player;

    private Vector3 offset;

    private float _rotY;
    private float rotSpeed = 10.5f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        _rotY = transform.eulerAngles.y;
        offset = player.transform.position - transform.position;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            _rotY += rotSpeed;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _rotY -= rotSpeed;
        }
    }

    void LateUpdate()
    {
        Quaternion rotation = Quaternion.Euler(0, _rotY, 0);
        transform.position = player.transform.position - (rotation * offset);
        transform.LookAt(player.transform.position);
    }
}
