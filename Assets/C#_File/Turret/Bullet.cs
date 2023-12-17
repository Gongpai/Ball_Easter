using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.AddComponent<SphereCollider>().isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<FixedJoint>() != null && other.gameObject.GetComponent<Ball_FixJoint>().Is_Joint == true)
        {
            Destroy(other.gameObject.GetComponent<FixedJoint>());
            if (other.gameObject.tag != "Player")
            {
                Destroy(other.gameObject, 1f);
            }
        }
    }
}
