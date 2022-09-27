using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public enum State
    {
        Idle,
        Ready,
        Tracking
    }
    State state
    {
        set
        {
            switch (value)
            {
                case State.Idle:
                    targetZoomSize = roundReadyZoomSize;
                    break;
                case State.Ready:
                    targetZoomSize = readyShotZoomSize;
                    break;
                case State.Tracking:
                    targetZoomSize = trackingZoomSize;
                    break;
            }
        }
    }

    Transform target;

    public float smoothTime = 0.2f;

    Vector3 lastMovingVelocity;
    Vector3 targetPosition;

    Camera cam;

    float targetZoomSize = 5f;
    const float roundReadyZoomSize = 14.5f;
    const float readyShotZoomSize = 5f;
    const float trackingZoomSize = 10f;

    float lastZoomSpeed;


    private void Awake()
    {
        cam = GetComponentInChildren<Camera>();
        state = State.Idle;
    }

    void Move()
    {
        targetPosition = target.transform.position;

        Vector3 smoothPosiotion = Vector3.SmoothDamp(transform.position,targetPosition,ref lastMovingVelocity,smoothTime);

        transform.position = smoothPosiotion;
    }

    void Zoom()
    {
        float smoothZoomSize = Mathf.SmoothDamp(cam.orthographicSize, targetZoomSize,ref lastZoomSpeed,smoothTime);
        cam.orthographicSize = smoothZoomSize;
    }

    private void FixedUpdate()
    {
        if (target != null)
        {
            Move();
            Zoom();
        }
    }

    public void Reset()
    {
        state = State.Idle;
    }
    
    public void SetTarget(Transform newTarget, State newState)
    {
        target = newTarget;
        state = newState;
    }
}
