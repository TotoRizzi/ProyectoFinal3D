using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    [SerializeField] float _parallaxMultiplier;
    [SerializeField] Transform _cameraTransform;

    Vector3 _previosCameraPos;
    float _spriteWidth, _startPos;
    void Start()
    {
        _previosCameraPos = _cameraTransform.position;
        _spriteWidth = GetComponent<SpriteRenderer>().bounds.size.x;
        _startPos = transform.position.x;
    }
    void LateUpdate()
    {
        float deltaX = (_cameraTransform.position.x - _previosCameraPos.x) * _parallaxMultiplier;
        float moveAmount = _cameraTransform.position.x * (1 - _parallaxMultiplier);
        transform.Translate(new Vector3(deltaX, 0, 0));
        _previosCameraPos = _cameraTransform.position;

        if (moveAmount > _startPos + _spriteWidth)
        {
            transform.Translate(new Vector3(_spriteWidth, 0, 0));
            _startPos += _spriteWidth;
        }
        else
        {
            transform.Translate(new Vector3(-_spriteWidth, 0, 0));
            _startPos -= _spriteWidth;
        }
    }
}
