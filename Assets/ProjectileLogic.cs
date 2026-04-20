using System;
using UnityEngine;

public class ProjectileLogic : MonoBehaviour
{
    public float speed = 25f;
    public float lifetime = 3f;

    void Start()
    {
        // Destroy the bullet after 3 seconds so it doesn't fly forever
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Spot"))
        {
            // Fix: Added "UnityEngine." before Object
            DarknessManager dm = UnityEngine.Object.FindFirstObjectByType<DarknessManager>();

            if (dm != null)
            {
                dm.ResetDarkness();
            }

            Destroy(gameObject);
        }
    }
}