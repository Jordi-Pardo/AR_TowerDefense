using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Base : MonoBehaviour
{

    public float health;
    public float maxHealth = 1f;

    public Spawner spawner;
    public Image healthBar;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            health -= 0.1f;
            healthBar.fillAmount = health;
            if (health <= 0)
            {
                GameManager.instance.gameOver = true;
                GameManager.instance.gameOverPanel.SetActive(true);
                spawner.CancelInvoke();

            }
        }
    }
}
