using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float m_ImpulsePower = 10;
    [SerializeField] float m_Radius = 3;
    [SerializeField] float m_TimeToExplode = 3;
    [SerializeField] public GameObject m_Effect;

    bool _IsTimerStart;
    float _StartTimeStamp;
    float _EndTimeStamp;
    private bool Is_Explode = false;
    private bool IsAddSphere;
    SphereCollider newSphere;

    private void OnEnable()
    {
        _IsTimerStart = true;
        _StartTimeStamp = Time.time;
        _EndTimeStamp = Time.time + m_TimeToExplode;
    }

    void Start()
    {
        IsAddSphere = false;
        m_Effect.GetComponent<ParticleSystem>().Pause();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_IsTimerStart) return;
        if (Time.time >= _EndTimeStamp)
        {
            Explode();
            m_Effect.GetComponent<ParticleSystem>().Play();
        }


    }

    protected void Explode()
    {
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, m_Radius);
        
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(m_ImpulsePower, explosionPos, m_Radius, 3.0f, ForceMode.Impulse);
            }
        }

        if (IsAddSphere == false)
        {
            newSphere = gameObject.AddComponent<SphereCollider>();
            newSphere.isTrigger = true;
            newSphere.radius = 10f;
            IsAddSphere = true;
        }

        if (Is_Explode == false)
        {
            gameObject.GetComponent<AudioSource>().volume = 0.5f;
            gameObject.GetComponent<AudioSource>().PlayOneShot(gameObject.GetComponent<AudioSource>().clip);
            Is_Explode = true;
        }
        
        Destroy(gameObject, 1);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<FixedJoint>() != null && other.gameObject.GetComponent<Ball_FixJoint>().Is_Joint == true)
        {
            Explode();
            Destroy(other.gameObject.GetComponent<FixedJoint>());
            print("Destroy" + newSphere.radius);
            if (other.tag != "Player")
            {
                Destroy(other, 1f);
            }
        }
    }
}