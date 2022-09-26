using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    #region field;
    [SerializeField]
    ParticleSystem particle;
    [SerializeField]
    AudioSource audiosource;
    public float maxDamage = 100f;
    public float explosionForce = 1000f;
    public float lifeTime = 10f;
    public float explosionRadius = 20f;
    public LayerMask whatIsProp;

    #endregion
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }
     
    private void OnTriggerEnter(Collider other)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position,explosionRadius,whatIsProp);

        foreach (Collider collider in colliders)
        {
            Rigidbody targetRigidbody = collider.GetComponent<Rigidbody>();
            targetRigidbody.AddExplosionForce(explosionForce,transform.position,explosionRadius);
            targetRigidbody.GetComponent<Prop>().TakeDamage(CalculateDamage(collider.transform.position));
        }

        particle.transform.parent = null;
        particle.Play();
        audiosource.Play();
        Destroy(particle.gameObject, particle.duration);
        Destroy(gameObject);

    }

    /// <summary>
    /// 원의 중심으로부터 거리에 따른 데미지 계산 
    /// </summary>
    /// <param name="targetPosition"></param>
    /// <returns></returns>
    float CalculateDamage(Vector3 targetPosition)
    {
        float distance = Vector3.Distance(targetPosition,transform.position);
        distance = Mathf.Min(distance,explosionRadius); // 콜라이더가 원 근처에 겹쳐 있을 경우
        float edgetToCenterDistance = explosionRadius - distance;
        float percentage = edgetToCenterDistance / explosionRadius;
        return (maxDamage * percentage);
    }


}
