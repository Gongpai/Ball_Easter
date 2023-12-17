using System.Collections;
using System.Collections.Generic;
using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ProjectileLauncher : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public Transform m_LaunchPosition;
    [SerializeField] public GameObject m_ProjectilePrefab;

    [SerializeField] public float m_Mass = 0.5f;

    [SerializeField] public float m_Power = 10;

    [SerializeField] private float Speed_Fire = 1;

    [SerializeField] public CollisionDetectionMode m_CollisionDetectionMode = CollisionDetectionMode.Discrete;

    [SerializeField] public GameObject m_Effect;

    public AudioSource M_SoundEffect;

    public GameObject MainTurret;

    float timer = 0;

    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if (MainTurret.gameObject.GetComponent<Turret>().CurrenTurretState == true)
        {
            if (timer < Speed_Fire)
            {
                timer += Time.deltaTime;
                m_Effect.GetComponent<ParticleSystem>().Stop();
            }
            else
            {
                LaunchProjectile();
                timer = 0;
                m_Effect.GetComponent<ParticleSystem>().Play();
                M_SoundEffect.Play();
            }
        }
        else
        {
            timer = 0;
            m_Effect.GetComponent<ParticleSystem>().Stop();
        }
        
    }

    public virtual void LaunchProjectile()
    {
        var g = Instantiate(m_ProjectilePrefab);
        g.transform.position = m_LaunchPosition.position;
        
        var rb = g.GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = g.AddComponent<Rigidbody>();
        }
        
        rb.mass = m_Mass;
        rb.collisionDetectionMode = m_CollisionDetectionMode;
        rb.AddForce(m_LaunchPosition.forward*m_Power,ForceMode.Impulse);
    }
}
