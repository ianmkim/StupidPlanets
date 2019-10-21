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
            /* TUTORIAL TEXT WHEN mass is changed */
            if(!TextController.txt.sunMassChanged){
                TextController.txt.killAll();
                TextController.txt.newText("Looks like you've changed the mass of the star. In accordance to Newton's laws of gravitation, the mass of the star will increase the force felt by the planets.", 500, 500);
                
                TextController.txt.sunMassChanged = true;
            }
        }
        if (newRadius.text.Length > 0)
        {
            radius = float.Parse(newRadius.text);
            /* TUTORIAL TEXT WHEN mass is changed */
            if(!TextController.txt.sunRadiusChanged){
                TextController.txt.killAll();
                TextController.txt.newText("Looks like you've changed the radius of the star. The properties of a star can change the solar system dramatically", 500, 500);
                
                TextController.txt.sunRadiusChanged = true;
            }
        }
        if (newLuminosity.text.Length > 0){
            luminosity = double.Parse(newLuminosity.text);
            /* TUTORIAL TEXT WHEN mass is changed */
            if(!TextController.txt.sunLumChanged){
                TextController.txt.killAll();
                TextController.txt.newText("Looks like you've changed the luminosity of the star. Luminosity will change the heat received by the planets among many others properties, see if you can find out what else it changes", 500, 500);
                
                TextController.txt.sunLumChanged = true;
            }
        }
        transform.localScale = 10*new Vector3(radius, radius, radius);
        newMass.text = null;
        newRadius.text = null;
        newLuminosity.text = null;

        
    }
}
    