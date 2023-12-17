using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Turret : MonoBehaviour
{
    // Start is called before the first frame update
    protected Turret_State currentstate;

    [SerializeField] public bool CurrenTurretState;

    public Transform Target { get; set; }

    [SerializeField]
    private float rotationSpeed;
    public float RotationSpeed { get => rotationSpeed; set => rotationSpeed = value; }

    [SerializeField] private Vector3 aimOffset;
    public Vector3 AinOffset { get => aimOffset; set => aimOffset = value; }

    [SerializeField]
    private Transform rotator;
    public Transform Rotator { get => rotator; set => rotator = value; }

    [SerializeField]
    private Transform ghostRotator;
    public Transform GhostRotator { get => ghostRotator; set => ghostRotator = value; }

    [SerializeField] private String AddCannonComponent;
    [SerializeField] private GameObject Cannon;
    [SerializeField] private GameObject Projecttile_Prefab;
    [SerializeField] private GameObject LaunchPoint;
    [SerializeField] private GameObject m_Effact;
    [SerializeField] public float Mass = 0.5f;
    [SerializeField] public float Power = 10;

    private void Start()
    {
        switch(AddCannonComponent)
        {
            case "Bullet":
                //Projecttile_Prefab.AddComponent<Bullet>();
                Cannon.gameObject.AddComponent<ProjectileLauncher>().m_ProjectilePrefab = Projecttile_Prefab;
                Cannon.gameObject.GetComponent<ProjectileLauncher>().MainTurret = gameObject;
                Cannon.gameObject.GetComponent<ProjectileLauncher>().m_LaunchPosition = LaunchPoint.transform;
                Cannon.gameObject.GetComponent<ProjectileLauncher>().m_CollisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
                Cannon.gameObject.GetComponent<ProjectileLauncher>().m_Effect = m_Effact;
                Cannon.gameObject.GetComponent<ProjectileLauncher>().m_Mass = Mass;
                Cannon.gameObject.GetComponent<ProjectileLauncher>().m_Power = Power;
                Cannon.gameObject.GetComponent<ProjectileLauncher>().M_SoundEffect = gameObject.GetComponent<AudioSource>();
                print(gameObject + "Bullet");
                break;
            case "Grenade":
                //Projecttile_Prefab.AddComponent<Grenade>();
                Cannon.gameObject.AddComponent<GrenadeLauncher>().m_ProjectilePrefab = Projecttile_Prefab;
                Cannon.gameObject.GetComponent<GrenadeLauncher>().MainTurret = gameObject;
                Cannon.gameObject.GetComponent<GrenadeLauncher>().m_LaunchPosition = LaunchPoint.transform;
                Cannon.gameObject.GetComponent<GrenadeLauncher>().m_CollisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
                Cannon.gameObject.GetComponent<GrenadeLauncher>().m_Effect = m_Effact;
                Cannon.gameObject.GetComponent<GrenadeLauncher>().m_Mass = Mass;
                Cannon.gameObject.GetComponent<GrenadeLauncher>().m_Power = Power;
                Cannon.gameObject.GetComponent<GrenadeLauncher>().M_SoundEffect = gameObject.GetComponent<AudioSource>();
                print(gameObject + "Grenade");
                break;
            default: break;
        }
        ChangeState(new Idle_State());
        CurrenTurretState = false;
    }

    private void Update()
    {
        currentstate.Update();
    }

    public void ChangeState(Turret_State newstate)
    {
        if (newstate != null)
        {
            newstate.Exit();
            CurrenTurretState = false;
        }
        this.currentstate = newstate;

        newstate.Enter(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        currentstate.OnTriggerEnter(other);
        if (other.tag == "Player")
        {
            CurrenTurretState = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        currentstate.OnTriggerExit(other);
        if (other.tag == "Player")
        {
            CurrenTurretState = false;
        }
    }
}
