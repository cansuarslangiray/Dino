using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private float _leftEdge;

    private void Start()
    {
        _leftEdge = Camera.main.ScreenToViewportPoint(Vector3.zero).x - 8f;
    }

    private void Update()
    {
        transform.position += Vector3.left * GameManager.Instance.GameSpeed * Time.deltaTime;
        if (transform.position.x < _leftEdge)
        {
            Destroy(this.gameObject);
        }
    }
}
