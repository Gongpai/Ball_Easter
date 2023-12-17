using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float m_ImpulsePower = 10;
    [SerializeField] float m_Radius = 3;
    [SerializeField] float m_TimeToExplode = 3;
    [SerializeField] private GameObject _bomb;
    [SerializeField] AudioClip _bombClip;
    [SerializeField] private GameObject bombEfface;

    bool _IsTimerStart;
    float _StartTimeStamp;
    float _EndTimeStamp;
    private bool Is_Expolde = false;
    private bool IsAddSphere;
    SphereCollider newSphere;
    private Collider _Other;

    private void OnEnable()
    {
        
    }

    void Start()
    {
        IsAddSphere = false;
        bombEfface.GetComponent<ParticleSystem>().Pause();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_IsTimerStart) return;
        if (Time.time >= _EndTimeStamp)
        {
            if (Is_Expolde == false)
            {
                bombEfface.GetComponent<ParticleSystem>().Play();
                Is_Expolde = true;
            }
            if (_Other.gameObject.GetComponent<HingeJoint>() != null)
            {
                Destroy(_Other.gameObject.GetComponent<HingeJoint>());
                print("Destroy " + gameObject.name);
                Destroy(_bomb, 0.5f);
            }
            Explode();
            gameObject.GetComponent<AudioSource>().PlayOneShot(_bombClip);
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
        Destroy(gameObject, 2);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Door")
        {
            _IsTimerStart = true;
            _StartTimeStamp = Time.time;
            _EndTimeStamp = Time.time + m_TimeToExplode;
            gameObject.GetComponent<FixedJoint>().connectedBody = null;
            Destroy(gameObject.GetComponent<Ball_FixJoint>());
            Destroy(gameObject.GetComponent<FixedJoint>());
            Destroy(gameObject.GetComponent<SphereCollider>());
            gameObject.AddComponent<FixedJoint>();
            gameObject.GetComponent<FixedJoint>().connectedBody = other.gameObject.GetComponent<Rigidbody>();
            print("Connect Fixed");
            gameObject.tag = "Untagged";
            _Other = other;
        }
    }
}