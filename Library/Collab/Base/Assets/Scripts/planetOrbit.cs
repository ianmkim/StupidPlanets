﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class planetOrbit : MonoBehaviour
{
    public float mass;
    public float radius;
    public float orbitRadius;
    public Vector3 rotationAxis;
    public float rotationSpeed = 50;
    public int co2;
    public int h2o;
    public int o2;
    float lastRadius;

    public GameObject sunObject;
    sunProp sun;
    Transform center;

    public InputField newMass;
    public InputField newRadius;
    public InputField newOrbitRadius;
    public Slider newco2;
    public Slider newo2;
    public Slider newh2o;

    //Physics
    float G = 6.67430f * Mathf.Pow(10, -11);
    float velocity;
    float period;
    Vector3 currentPos;

    void Start()
    {
        radius += Random.Range(-5, 5);
        transform.localScale = new Vector3(radius, radius, radius);
        sun = sunObject.GetComponent<sunProp>();
        lastRadius = orbitRadius;
        rotationAxis = new Vector2(Random.Range(1, 4), Random.Range(-1, 1)).normalized;
        if (Random.Range(0, 2) < 0.5f)
        {
            rotationAxis *= -1;
        }
        correctRadius();
    }

    void Update()
    {
        updatePhysics();
        doRotation();
        if (lastRadius == orbitRadius)
            transform.RotateAround(center.position, Vector3.up, velocity);
        else
        {
            correctRadius();
            lastRadius = orbitRadius;
        }
    }
    public double calcTemp()
    {
        chemicalComp calculator = gameObject.GetComponent<chemicalComp>();
        return calculator.calcTemp(sun.surfaceTemp, co2, h2o, o2, calculator.gameToKm(sun.radius), calculator.gameToKm(orbitRadius));
    }
    void doRotation()
    {
        transform.Rotate(0, rotationAxis.x * rotationSpeed* Time.deltaTime, rotationAxis.y * rotationSpeed*Time.deltaTime);
    }
    public void correctRadius()
    {
        currentPos = new Vector3(Mathf.Abs(this.transform.position.x - sunObject.transform.position.x),
            Mathf.Abs(this.transform.position.y - sunObject.transform.position.y),
            Mathf.Abs(this.transform.position.z - sunObject.transform.position.z));
        currentPos.Normalize();
        currentPos *= orbitRadius;
        this.transform.position = sunObject.transform.position + currentPos;
    }

    void updatePhysics()
    {
        center = sunObject.transform;
        velocity = Mathf.Sqrt(G * sun.mass / orbitRadius);
        period = 2 * Mathf.PI * Mathf.Sqrt(Mathf.Pow(orbitRadius, 3) / G / mass);
    }  
    
    public void newValues()
    {
        if (newMass.text.Length > 0)
            mass = float.Parse(newMass.text);
        if (newRadius.text.Length > 0)
            radius = float.Parse(newRadius.text);
        if (newOrbitRadius.text.Length > 0)
            orbitRadius = float.Parse(newOrbitRadius.text);
        co2 = (int)newco2.value;
        o2 = (int)newo2.value;
        h2o = (int)newh2o.value;
        newMass.text = null;
        newRadius.text = null;
        newOrbitRadius.text = null;
        transform.localScale = new Vector3(radius, radius, radius);
        correctRadius();
    }
}
