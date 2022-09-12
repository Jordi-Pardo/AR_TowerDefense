using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using System;

public class Spawner : MonoBehaviour, ITrackableEventHandler
{

    public static Action<Vector3> onActiveTracker;
    private TrackableBehaviour mTrackableBehaviour;
    public int num_Units;
    public GameObject prefab;

    void Start()
    {
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();

        if (mTrackableBehaviour)
        {
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
        }
        num_Units = 1;
    }

    protected virtual void OnDestroy()
    {
        if (mTrackableBehaviour)
            mTrackableBehaviour.UnregisterTrackableEventHandler(this);
    }

    public void OnTrackableStateChanged(
        TrackableBehaviour.Status previousStatus,
        TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            if (num_Units > 0 && !GameManager.instance.gameOver && GameManager.instance.baseTarget.activeInHierarchy)
            {
                InvokeRepeating("Wave01", 0, 2f);
                Debug.Log("HeroFound");
                num_Units = 0;
            }

        }
        else
        {
            Debug.Log("HeroLostTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTT");
            num_Units = 1;
            CancelInvoke("Wave01");

        }
    }

    void SpawnUnit()
    {
        Vector3 spawnPosition = transform.position;
        spawnPosition.y = 0;
        Instantiate(prefab,spawnPosition + new Vector3(0, 0.5f),Quaternion.Euler(Vector3.forward));
    }
    public void Wave01()
    {
        SpawnUnit();
    }
}
