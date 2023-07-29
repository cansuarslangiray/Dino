using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedScripts : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;

    private SpriteRenderer _spriteRenderer;
    private int _frame;
    private bool _isAnimating;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _isAnimating = true;
    }

    private void OnEnable()
    {
        _isAnimating = true;
        _frame = 0;
        Invoke(nameof(Animate), 0f);
    }

    private void OnDisable()
    {
        CancelInvoke();
        _isAnimating = false;
    }

    private void Animate()
    {
        if (_isAnimating)
        {
            _frame++;

            if (_frame >= sprites.Length)
            {
                Debug.Log("hehyy");

                _frame = 0;
            }

            if (_frame >= 0 && _frame < sprites.Length)
            {
                _spriteRenderer.sprite = sprites[_frame];
            }

            if (GameManager.IsDead)
            {
                _isAnimating = false;
            }
            else
            {
                Invoke(nameof(Animate), 1f / GameManager.Instance.GameSpeed);
            }
        }
    }

    public void SetIsAnimating(bool animate)
    {
        _isAnimating = animate;
    }
}