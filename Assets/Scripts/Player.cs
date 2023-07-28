using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController _character;
    private Vector3 _direction;
    [SerializeField] private float gravity = 9.81f * 2f;
    [SerializeField] private float jumpForce = 8f;
    [SerializeField] private Sprite deathSprite;

    private void Awake()
    {
        _character = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        _direction = Vector3.zero;
    }

    private void Update()
    {
        _direction += Vector3.down * gravity * Time.deltaTime;

        if (_character.isGrounded)
        {
            _direction = Vector3.down;

            if (Input.GetButton("Jump"))
            {
                _direction = Vector3.up * jumpForce;
            }
        }

        _character.Move(_direction * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            GameManager.IsDead = true;
            GetComponent<SpriteRenderer>().sprite = deathSprite;
            GameManager.Instance.GameOver();
        }
    }
}
