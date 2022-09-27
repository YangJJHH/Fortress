using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BallShooter : MonoBehaviour
{
    #region field
    public Rigidbody ball;
    public Transform firePosition;
    public Slider powerSlider;
    public AudioSource shootingAudio;

    public AudioClip[] audioClips;

    public float minForce = 15f;
    public float maxForce = 30f;
    public float chargingTime = 0.75f;

    float currentForce;
    float chargeSpeed;
    bool fired;
    public CamFollow cam;
    #endregion
    private void OnEnable()
    {
        currentForce = minForce;
        powerSlider.value = minForce;
        fired = false;
    }

    private void Start()
    {
        chargeSpeed = (maxForce - minForce) / chargingTime;
    }

    private void Update()
    {
        checkState();
    }

    void checkState()
    {
        if (fired) return;
        powerSlider.value = minForce;
        if (currentForce >= maxForce && !fired)
        {
            currentForce = maxForce;
            //惯荤贸府
            Fire();
        }
        else if (Input.GetButtonDown("Fire1") && !fired)
        {
            currentForce = minForce;
            //瞒隆家府
            shootingAudio.clip = audioClips[1];
            shootingAudio.Play();
        }
        else if (Input.GetButton("Fire1") && !fired)
        {
            currentForce += chargeSpeed * Time.deltaTime;
            powerSlider.value = currentForce;
        }
        else if (Input.GetButtonUp("Fire1") && !fired)
        {
            //惯荤贸府
            Fire();
        }
    }

    void Fire()
    {
        fired = true;
        Rigidbody ballInstance = Instantiate(ball,firePosition.position,Quaternion.identity);
        ballInstance.velocity = currentForce * firePosition.forward;
        shootingAudio.clip = audioClips[0];
        shootingAudio.Play();
        cam.SetTarget(ballInstance.transform, CamFollow.State.Tracking);
        currentForce = minForce;
    }
}
