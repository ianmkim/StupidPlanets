using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class temperatureV2 : MonoBehaviour
{
    double sb = 5.670367 * System.Math.Pow(10, -8);
    double portionCO2 = 0.0019011407;
    double portionO2 = 0.9980988593;
    double lumiBase = 17270.65422;
    double co2Base = 0.25;
    double o2Base = 0.31;
    double h2oBase = 0.15;

    double luminosity = 1;
    double albedo = 0.50;
    double radius;

    public sunProp sun;
    public planetOrbit planet;

    public double temperature;

    bool won = false;

    void Start()
    {
        planet = this.GetComponent<planetOrbit>();
    }

    void Update()
    {
        updateValues();
        temperature = System.Math.Round(calcTemp() * 100) / 100;
        if (!won)
            checkWin();
    }

    void updateValues()
    {
        radius = (double)planet.orbitRadius / 100;
        luminosity = sun.luminosity;
        albedo = ((co2Base * (3 - planet.co2)) + (o2Base * (3 - planet.o2)) + (h2oBase * (3 - planet.h2o))) / 3;
    }
    double calcTemp()
    {
        return System.Math.Pow((luminosity * lumiBase * (1 - albedo)) / (16 * sb * System.Math.PI), 0.25) * (1 / System.Math.Sqrt(radius));
    }

    public void checkWin()
    {
        if (System.Math.Abs(temperature - 297) <= 5)
        {
            if (planet.h2o != 0 && planet.o2 != 0 && planet.co2 < 3)
            {
                GameMaster.GM.Win(planet.gameObject);
                won = true;
            }
        }
    }
}
