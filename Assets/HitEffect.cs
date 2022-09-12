using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffect : MonoBehaviour
{
    public ParticleSystem particle;

    private void OnEnable()
    {
        AimTarget.onHit += ActiveParticle;
    }

    private void OnDestroy()
    {
        AimTarget.onHit -= ActiveParticle;
    }

    public void ActiveParticle(Vector3 pos)
    {
        particle.gameObject.transform.position = pos;
        particle.Play();

    }
}
