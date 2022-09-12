using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Move : MonoBehaviour
{
    public float speed = 20f;
    public Transform target;
    public float rotationSpeed = 4f;
    public ParticleSystem explosion;
    public AudioSource source;
    public Image healthBar;
    public float health;
    public float maxHealth = 1f;

    public BoxCollider collider;
    // Update is called once per frame
    public void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        health = maxHealth;
    }
    void Update()
    {
        if (target != null)
        {
            if (target.gameObject.activeInHierarchy)
            {
                CalculateAngle();
                transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);
            }
        }
    }

    private void CalculateAngle()
    {
        Vector3 helicopterForward = this.transform.forward;
        Vector3 baseDirection = target.transform.position - this.transform.position;
        float angle = Vector3.SignedAngle(helicopterForward, baseDirection, Vector3.up);
        this.transform.Rotate(0, (angle * rotationSpeed * Time.deltaTime), 0);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Base"))
        {
            StartCoroutine(Deactivate());
            explosion.Play();
            source.Play();
            collider.enabled = false;
            healthBar.transform.parent.gameObject.SetActive(false);
            Destroy(this.gameObject, 2f);
        }
    }

    IEnumerator Deactivate()
    {
        yield return new WaitForSecondsRealtime(0.01f);
        transform.GetChild(0).gameObject.SetActive(false);
    }

    public void RecieveDamage(float num)
    {
        health -= num;
        healthBar.fillAmount = health;

        if (health <= 0)
        {
            StartCoroutine(Deactivate());
            explosion.Play();
            source.Play();
            collider.enabled = false;
            healthBar.transform.parent.gameObject.SetActive(false);
            Destroy(this.gameObject, 2f);
        }
    }


}
