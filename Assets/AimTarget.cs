using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AimTarget : MonoBehaviour
{
    public static Action<Vector3> onHit;

    public static float damage = 0.3f;
    public Collider[] enemyInRange;
    public LayerMask enemy;
    public float range;
    public float rotationSpeed = 4;
    public Transform cannon;
    public float fireRate = 0.5f;
    float fireTime;
    public GameObject particleHit;
    public float firePower = 10;
    public Transform barrel;
    public LineRenderer line;
    public AudioSource source;
    public AudioClip shot;
    public float timeLine = 0.1f;

    public float currentTime;
    public float timeToDecreaseDamage = 10f;

    private void Start()
    {
        fireTime = fireRate;
        currentTime = timeToDecreaseDamage;
    }
    private void Update()
    {
        if (damage >= 0.2f)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
            {
                damage -= 0.05f;
                currentTime = timeToDecreaseDamage;
                Debug.Log("Damage Reduced");
            }
        }
        enemyInRange = Physics.OverlapSphere(this.transform.position, range,enemy);
        fireTime -= Time.deltaTime;

        if (enemyInRange.Length > 0)
        {
            cannon.transform.LookAt(enemyInRange[0].transform);
            if (fireTime <= 0)
            {
                line.enabled = true;
                line.SetPositions(new Vector3[] { barrel.position, enemyInRange[0].transform.position });
                RaycastHit hitInfo;
                Vector3 baseDirection = enemyInRange[0].transform.position - barrel.transform.position;
                if (Physics.Raycast(barrel.position, baseDirection, out hitInfo))
                {
                    onHit?.Invoke(hitInfo.point);
                    source.Play();
                    hitInfo.collider.gameObject.GetComponent<Move>().RecieveDamage(damage);
                }

                // ShootProjectile(cannon);
                fireTime = fireRate;
            }
            // CalculateAngle(enemyInRange[0].gameObject);
        }

        if (line.enabled)
        {
            timeLine -= Time.deltaTime;
            if (timeLine <= 0)
            {
                line.enabled = false;
                timeLine = 0.1f;
            }
        }
    }


    private void CalculateAngle(GameObject target)
    {
        Vector3 helicopterForward = cannon.transform.forward;
        Vector3 baseDirection = target.transform.position - cannon.transform.position;

        float angle = Vector3.SignedAngle(helicopterForward, baseDirection, Vector3.up);
        cannon.transform.Rotate(0, (angle * rotationSpeed * Time.deltaTime), 0);

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.transform.position, range);
    }



    public void ShootProjectile(Transform origin)
    {
        Vector3 helicopterForward = cannon.transform.forward;
        Vector3 baseDirection = enemyInRange[0].transform.position - cannon.transform.position;

        GameObject bullet = Instantiate(particleHit, origin.position, origin.rotation,cannon);
        bullet.GetComponent<Rigidbody>().AddForce(baseDirection, ForceMode.Impulse);
    }
    IEnumerator ReduceDamage()
    {
        yield return new WaitForSecondsRealtime(1);
        ReduceDamageFunction();


    }

    private void ReduceDamageFunction()
    {
        if (damage >= 0.1f)
            damage -= .05f;
        Debug.Log("Damage Reduced");
    }
}


