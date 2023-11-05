using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _pushForce;
    [SerializeField] private float _cubeMaxPosX;
    [Space]
    [SerializeField] private TouchSlider _touchSlider;

    [SerializeField] private Cube _mainCube;

    private bool _isPointerDown;
    private Vector3 _cubePos;

    private void Start()
    {
        _touchSlider.OnPointerDownEvent += OnPointerDown;
        _touchSlider.OnPointerDragEvent += OnPointerDrag;
        _touchSlider.OnPointerUpEvent += OnPointerUp;
    }

    private void Update()
    {
        if (_isPointerDown)
        {
            _mainCube.transform.position =
                Vector3.Lerp(_mainCube.transform.position, _cubePos, _moveSpeed * Time.deltaTime);
        }
    }
    private void OnPointerDown()
    {
        _isPointerDown = true;
    }
    private void OnPointerDrag(float xMovemnt)
    {
        if (_isPointerDown)
        {
            _cubePos = _mainCube.transform.position;
            _cubePos.x = xMovemnt * _cubeMaxPosX;
        }
    }
    private void OnPointerUp()
    {
        if (_isPointerDown)
        {
            _isPointerDown = false;
            _mainCube.cubeRigidBody.AddForce(Vector3.forward*_pushForce, ForceMode.Impulse);
        }
    }

    private void OnDestroy()
    {
        _touchSlider.OnPointerDownEvent -= OnPointerDown;
        _touchSlider.OnPointerDragEvent -= OnPointerDrag;
        _touchSlider.OnPointerUpEvent -= OnPointerUp;
    }
}
