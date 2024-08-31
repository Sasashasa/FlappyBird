using System;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float _smoothSpeed = 2f;
    [SerializeField] private float _xOffset = 0.5f;
    
    private Transform _target;
    private bool _isGameOver;
    
    private void Start()
    {
        _target = Bird.Instance.transform;
        
        transform.position = new Vector3(_target.position.x + _xOffset, _target.position.y, transform.position.z);
        
        Bird.Instance.OnGameOver += Bird_OnGameOver;
    }

    private void Update()
    {
        if (_isGameOver)
            return;
        
        Vector3 targetPosition = new Vector3(_target.position.x + _xOffset, transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, _smoothSpeed * Time.deltaTime);
    }
    
    private void Bird_OnGameOver(object sender, EventArgs e)
    {
        _isGameOver = true;
    }
}