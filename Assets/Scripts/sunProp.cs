using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sunProp : MonoBehaviour
{
    public float mass;
    public float radius;
    public float surfaceTemp = 5778;
    public Text myMass;
    public Text myRadius;
    public Slider newMass;
    public float luminosity;
    public InputField newRadius;

    void Start()
    {
        
    }

    void Update()
    {
        myMass.text = mass.ToString();
        myRadius.text = radius.ToString();
    }

    public void newValues()
    {
        mass = newMass.value;
        if (newRadius.text.Length > 0)
        {
            radius = float.Parse(newRadius.text);
        }
        transform.localScale = 10*new Vector3(radius, radius, radius);
        newRadius.text = null;
    }
}
