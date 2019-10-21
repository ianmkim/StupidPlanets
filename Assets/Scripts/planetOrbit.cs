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
    public string planetType;
    float lastRadius;

    public GameObject sunObject;
    sunProp sun;
    Transform center;

    public InputField newRadius;
    public InputField newOrbitRadius;
    public Slider newMass;
    public Slider newO2;
    public Slider newH2o;
    public Slider newCo2;
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
        return calculator.calc_temp(planetType, calculator.gameToSolar(sun.mass), calculator.gameToAu(orbitRadius), co2, h2o);
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
        float oldMass = mass;
        mass = newMass.value;
        co2 = (int)newCo2.value;
        h2o = (int)newH2o.value;
        o2 = (int)newO2.value;
        if(mass != oldMass)
        {
            if (!TextController.txt.planetMassChanged)
            {
                TextController.txt.killAll();
                TextController.txt.newText("Looks like you've changed the mass of a planet. Mass of a planet can determine orbits, see if you can find what else mass changes", 500, 500);

                TextController.txt.planetMassChanged = true;
            }
        }
        if (newRadius.text.Length > 0)
        {
            radius = float.Parse(newRadius.text);
            /* TUTORIAL TEXT WHEN mass is changed */
            if (!TextController.txt.planetRadiusChanged)
            {
                TextController.txt.killAll();
                TextController.txt.newText("Looks like you've changed the planet radius. Consider what properties will make this planet suitable for life", 500, 500);

                TextController.txt.planetRadiusChanged = true;
            }
        }
        if (newOrbitRadius.text.Length > 0)
        {
            orbitRadius = float.Parse(newOrbitRadius.text);
            /* TUTORIAL TEXT WHEN mass is changed */
            if (!TextController.txt.planetOrbitRadiusChanged)
            {
                TextController.txt.killAll();
                TextController.txt.newText("A change in orbital raidus can put the planet in the \"Goldilocks\" or habitable zone.", 500, 500);

                TextController.txt.planetOrbitRadiusChanged = true;
            }
        }
        newRadius.text = null;
        transform.localScale = new Vector3(radius, radius, radius);
        newOrbitRadius.text = null;
        correctRadius();
    }
}
