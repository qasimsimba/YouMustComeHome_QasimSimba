using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab; // Drag your bullet prefab here

    void Update()
    {
        // 0 is Left Mouse Button
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Spawns the bullet at the player's position and rotation
        Instantiate(bulletPrefab, transform.position, transform.rotation);
    }
}