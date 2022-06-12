using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public struct SignalInfo
{
    public float signalTimer;
    public string signalState;
    public int signalCounter;
    public string signalName;

    public void GetInfo(float t, string s, int c, string n)
    {
        signalTimer = t;
        signalState = s;
        signalCounter = c;
        signalName = n;
    }
}

public class Signal : MonoBehaviour
{
    public List<int> direction;
    public bool available;
    public KeyCode switcher;
    public SignalInfo signalInfo;

    private float currentTimer;
    private string state;
    private int counter = 0;

    private void Start()
    {
        available = false;
        GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        currentTimer = 0;
        state = "OFF";
    }
    private void CheckStop() 
    {
        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, transform.forward, 20f);
        Debug.DrawRay(transform.position, transform.forward * 20f, Color.blue);
        counter = 0;

        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.GetComponent<Rigidbody>().velocity == Vector3.zero)
            {
                counter++;
            }
        }
    }

    public SignalInfo GetSignalInfo()
    {
        signalInfo.GetInfo(currentTimer, state, counter, name);
        return signalInfo;
    }

    void FixedUpdate()
    {
        if (Input.GetKeyDown(switcher))
        {
            available = !available;
            GetComponent<Renderer>().material.SetColor("_Color", available ? Color.green : Color.red);
            state = available ? "ON" : "OFF";
            currentTimer = 0;
        }

        // update timer
        currentTimer += Time.deltaTime;

        CheckStop();
    }
}
