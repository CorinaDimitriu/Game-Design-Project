using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class FallingManager : MonoBehaviour
{
    public GameObject[] waypoints;
    public GameObject coinSilverPrefab;

    private void Start()
    {
        waypoints = GameObject.FindGameObjectsWithTag("coin");
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag[..4] != "icy_ceiling")
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            //GetComponent<Rigidbody>().AddForce(Vector3.up * 3000f, ForceMode.Force);
            transform.SetPositionAndRotation(waypoints[Int32.Parse(tag[4..])].transform.position,
                                                coinSilverPrefab.transform.rotation);
            GetComponent<Rigidbody>().AddForce(Vector3.down * 3000f, ForceMode.Force);
        }
    }
}
