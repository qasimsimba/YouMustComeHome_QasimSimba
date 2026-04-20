using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int health = 10;
    public Slider healthBar; // Drag your Slider here

    public void TakeDamage(int amount)
    {
        health -= amount;
        healthBar.value = health;
    }
}