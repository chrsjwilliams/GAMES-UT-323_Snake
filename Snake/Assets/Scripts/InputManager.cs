using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;

[DefaultExecutionOrder(-1)]
public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    public delegate void StartTouchEvent(Finger position, float time);
    public event StartTouchEvent OnStartTouch;

    public delegate void EndTouchEvent(Finger position, float time);
    public event EndTouchEvent OnEndTouch;

    public delegate void TouchMovedEvent(Finger position, float time);
    public event TouchMovedEvent OnTouchMoved;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        TouchSimulation.Enable();
        EnhancedTouchSupport.Enable();
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerDown += FingerDown;
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerUp += FingerUp;
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerMove += FingerMoved;
    }

    

    private void OnDisable()
    {
        TouchSimulation.Disable();
        EnhancedTouchSupport.Disable();
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerDown -= FingerDown;
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerUp -= FingerUp;
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerMove -= FingerMoved;
    }


    private void FingerDown(Finger finger)
    {
        if (OnStartTouch != null)
        {
            OnStartTouch(finger, Time.time);
        }
    }

    private void FingerUp(Finger finger)
    {
        if (OnEndTouch != null)
        {
            OnEndTouch(finger, Time.time);
        }
    }

    private void FingerMoved(Finger finger)
    {
        if(OnTouchMoved != null)
        {
            OnTouchMoved(finger, Time.time);
        }
    }
}
