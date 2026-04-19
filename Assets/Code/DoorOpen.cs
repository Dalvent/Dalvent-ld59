using System;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    public float openAngle = 90f;
    public float speed = 5f;

    public bool IsOpen { get; private set; }
    public bool IsMoving { get; private set; }

    private bool _isMovingStarted = false;

    public event Action StartMoving;
    public event Action StopMoving;

    public bool IsFullClosed => !IsMoving && !IsOpen;

    private Quaternion closedRotation;

    void Start()
    {
        closedRotation = transform.rotation;
    }

    void Update()
    {
        if (!IsMoving)
            _isMovingStarted = false;
        
        if (IsMoving)
        {
            if (!_isMovingStarted)
                StartMoving?.Invoke();
            
            _isMovingStarted = true;
            
            Quaternion target = IsOpen ? closedRotation * Quaternion.Euler(0f, 0f, openAngle) : closedRotation;

            transform.rotation = Quaternion.Lerp(
                transform.rotation,
                target,
                Time.deltaTime * speed
            );

            if (Quaternion.Angle(transform.rotation, target) < 0.5f)
            {
                transform.rotation = target;
                IsMoving = false;
                StopMoving?.Invoke();
            }
        }
    }

    public void Interact()
    {
        if (IsMoving) return;

        if (IsOpen)
            Close();
        else
            Open();
    }

    private void Open()
    {
        IsOpen = true;
        IsMoving = true;
    }

    private void Close()
    {
        IsOpen = false;
        IsMoving = true;
    }
}