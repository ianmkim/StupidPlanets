using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sunProp : MonoBehaviour
{
    public float mass;
    public float radius;
    public float surfaceTemp = 5778;
    public double luminosity = 1;

    public Text myMass;
    public Text myRadius;
    public Text myLuminosity;
    public InputField newMass;
    public InputField newRadius;
    public InputField newLuminosity;

    void Start()
    {
        transform.localScale = 10 * new Vector3(radius, radius, radius);
    }

    void Update()
    {
        myMass.text = mass.ToString();
        myRadius.text = radius.ToString();
        myLuminosity.text = luminosity.ToString();
    }

    public void newValues()
    {
        if (newMass.text.Length > 0)
        {
            mass = float.Parse(newMass.text);
        }
        if (newRadius.text.Length > 0)
        {
            radius = float.Parse(newRadius.text);
        }
        if (newLuminosity.text.Length > 0)
            luminosity = double.Parse(newLuminosity.text);
        transform.localScale = 10*new Vector3(radius, radius, radius);
        newMass.text = null;
        newRadius.text = null;
        newLuminosity.text = null;
    }
}
