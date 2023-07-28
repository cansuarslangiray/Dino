using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedScripts : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;

    private SpriteRenderer _spriteRenderer;
    private int _frame;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        Invoke(nameof(Animate), 0f);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void Animate()
    {
        if (!GameManager.IsDead)
        {
            _frame++;

            if (_frame >= sprites.Length)
            {
                _frame = 0;
            }

            if (_frame >= 0 && _frame < sprites.Length)
            {
                _spriteRenderer.sprite = sprites[_frame];
            }

            Invoke(nameof(Animate), 1f / GameManager.Instance.GameSpeed);
        }
    }
}