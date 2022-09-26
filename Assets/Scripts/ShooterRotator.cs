using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterRotator : MonoBehaviour
{
    #region field
    enum RotatateState { 
        Idel,
        Vertical,
        Horizontal,
        Ready
    }
    RotatateState state = RotatateState.Idel;

    public float verticalRotateSpeed = 360f;
    public float horizontalRotateSpeed = 360f;
    public BallShooter ballShooter;

    #endregion

    void Update()
    {
        stateCheck();

    }

    void stateCheck()
    {
        switch (state) {
            case RotatateState.Idel:
                if (Input.GetButtonDown("Fire1"))
                {
                    state = RotatateState.Horizontal;
                }
                break;

            case RotatateState.Horizontal:
                if (Input.GetButton("Fire1"))
                {
                    transform.Rotate(Vector3.up * horizontalRotateSpeed * Time.deltaTime);
                }
                else if (Input.GetButtonUp("Fire1"))
                {
                    state = RotatateState.Vertical;
                }
                break;

            case RotatateState.Vertical:
                if (Input.GetButton("Fire1"))
                    transform.Rotate(Vector3.left * verticalRotateSpeed * Time.deltaTime);
                else if (Input.GetButtonUp("Fire1"))
                {
                    state = RotatateState.Ready;
                    ballShooter.enabled = true;
                }
                break;

            case RotatateState.Ready:
                break;
        }

    }

    private void OnEnable()
    {
        transform.rotation = Quaternion.identity;
        state = RotatateState.Idel;
        ballShooter.enabled = false;
    }



}
