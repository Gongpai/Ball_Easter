using UnityEngine;
public class GrenadeLauncher : ProjectileLauncher
{
    // Start is called before the first frame update
    public override void LaunchProjectile()
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
        Vector3 dir = (m_LaunchPosition.forward + m_LaunchPosition.up).normalized;
        rb.AddForce(dir*m_Power,ForceMode.Impulse);
    }
}
