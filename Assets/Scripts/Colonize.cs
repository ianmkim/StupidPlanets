using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colonize : MonoBehaviour
{
	public cameraBehavior cb;
    public void buttonEvent(){
    	TextController.txt.killAll();

    	GameObject planet = cb.focus;
        float temp = (float)cb.focus.GetComponent<planetOrbit>().calcTemp();
        float radius = cb.focus.GetComponent<planetOrbit>().radius;
        float orbitRadius = cb.focus.GetComponent<planetOrbit>().orbitRadius;
        int co2 = cb.focus.GetComponent<planetOrbit>().co2;
        int o2 = cb.focus.GetComponent<planetOrbit>().o2;
        int h2o = cb.focus.GetComponent<planetOrbit>().h2o;
        string planetType = cb.focus.GetComponent<planetOrbit>().planetType;

        bool possible = colonizationPossible(temp, radius, orbitRadius, co2, o2, h2o, planetType);
        string reasons = colonizationPossibleReasons(temp,radius,orbitRadius,co2,o2,h2o,planetType);

        TextController.txt.killAll();
        if(possible){
        	Debug.Log("bruh");
        	TextController.txt.newText(reasons, 500,500);
        	GameMaster.GM.Win(planet);

    	}
    	else{
    		TextController.txt.newText(reasons, 500, 500);
    	}
    }

    public bool colonizationPossible(float temp, 
    	float radius, 
    	float orbitRadius,
    	int co2,
    	int o2,
    	int h2o, string planetType){
        if (planetType == "gasgiant" || temp < 273 || temp > 373 || o2 == 0){
        	return false;
        }
        if(planetType != "earth" && planetType != "water" && h2o <2){
        	return false;
        }
        return true;
    }

    public string colonizationPossibleReasons(float temp, float radius, float orbitRadius, int co2, int o2, int h2o, string planetType){
        if (planetType == "gasgiant")
        {
            return "Failure: Gas giant. You deploy your ship, and it falls straight through the planet!This is a gas giant; there's no solid ground to colonize. Unfortunately, you can't really change this one.Try another planet!";
        }
        if(temp <200)
        {
            return "Your colonies froze instantly! This planet is way too cold!";
        }
        if(temp < 273)
        {
            return "Your colonies were freezing, and they couldn't find any water, only ice. They need a warmer planet!";
        }
        if(temp > 373)
        {
            return "Your colonies were instantly vaporized from the heat! This planet is way too hot!";
        }
        if(o2 == 0){
        	return "Your colonies suffocated; there's no oxygen here.";
        }
        if(planetType != "earth" && planetType != "water" && h2o < 2){
        	return "Your colonies couldn't find enough water. Try adding more to the atmosphere.";
        }
        return "Your colonies have successfully established themselves on this planet! Congratulations! New life has evolved.";
    }
}
