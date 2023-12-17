using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEditor;
using System.Drawing;

public class Ball_FixJoint : MonoBehaviour
{
    // Start is called before the first frame update
    public bool Is_Joint;
    public bool Is_Shoot = false;
    private AudioSource[] audio_source = new AudioSource[4];
    [SerializeField]private AudioClip[] audio_clip = new AudioClip[4];
    void Start()
    {
        Is_Joint = false;
        audio_source[0] = gameObject.AddComponent<AudioSource>();
        audio_source[1] = gameObject.AddComponent<AudioSource>();
        audio_source[2] = gameObject.AddComponent<AudioSource>();
        audio_source[3] = gameObject.AddComponent<AudioSource>();
        audio_source[0].clip = audio_clip[0];
        audio_source[1].clip = audio_clip[1];
        audio_source[2].clip = audio_clip[2];
        audio_source[3].clip = audio_clip[3];
    }
    void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.GetComponent<Rigidbody>() != null) && (other.gameObject.tag == "Rock" || other.gameObject.tag == "Player" || other.gameObject.tag == "Cow" || other.gameObject.tag == "Car" || other.gameObject.tag == "Human") && Is_Joint == false)
        {
            if (gameObject.GetComponent<Rigidbody>() != null)
            {
                gameObject.GetComponent<Rigidbody>().useGravity = true;
                gameObject.GetComponent<Rigidbody>().isKinematic = false;
            }

            if (gameObject.GetComponent<FixedJoint>() == null)
            {
                gameObject.AddComponent<FixedJoint>().connectedBody = other.gameObject.GetComponent<Rigidbody>();
                print(gameObject.GetComponent<FixedJoint>());
            }

            // Object Add Point
            print("ADD " + other.gameObject.name + "Tag Is : " + other.gameObject.tag);
            switch (gameObject.tag)
            {
                case "Human":
                    Point.point -= 50;
                    Point.AddPoint?.Invoke();
                    audio_source[0].Play();
                    break;
                case "Player":
                    Point.point += 50;
                    Point.AddPoint?.Invoke();
                    break;
                case "Rock":
                    Point.point += 50;
                    Point.AddPoint?.Invoke();
                    audio_source[1].Play();
                    break;
                case "Cow":
                    Point.point += 150;
                    Point.AddPoint?.Invoke();
                    audio_source[2].Play();
                    break;
                case "Car":
                    Point.point += 100;
                    Point.AddPoint?.Invoke();
                    audio_source[3].Play();
                    break;
                default: break;
            }
            
            Is_Joint = true;
        }

        if (other.gameObject.tag == "Bullet" && Is_Shoot == false && Is_Joint == true && gameObject.tag != "Player")
        {
            Point.point -= 25;
            Point.AddPoint?.Invoke();
            Is_Shoot = true;
        }

        print("ADD " + other.gameObject.name + "Tag Is : " + other.gameObject.tag);
    }

    // Update is called once per frame
    void Update()
    {
        if (Is_Joint == true && gameObject.GetComponent<FixedJoint>() == null && gameObject.tag != "Player")
        {
            Destroy(gameObject, 1);
        }
    }
}
