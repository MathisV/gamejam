using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SwitchController : MonoBehaviour
{
    public float maxActivationDistance = 5f;

    public Transform player;

    public UnityEvent OnSwitchActivated;

    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    public bool isNearPlayer()
    {
        return Vector3.Distance(player.position, transform.position) <=
        maxActivationDistance;
    }

    public bool ActivateSwitch()
    {
        if (
            Vector3.Distance(player.position, transform.position) <=
            maxActivationDistance
        )
        {
            OnSwitchActivated?.Invoke();
            return true;
        }
        return false;
    }
}
