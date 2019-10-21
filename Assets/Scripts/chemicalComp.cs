using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chemicalComp : MonoBehaviour
{
    double h = 6.6 * Mathf.Pow(10, -34);
    double c = 3 * Mathf.Pow(10, 8);
    double k = 1.38 * Mathf.Pow(10, -23);
    double[] L = { 150, 250, 350, 500, 700, 900, 1100, 1350, 1750, 2500.0, 5000.0, 30000.0 };
    float e = 2.718281828459045f;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 10; i++)
        {
            L[i] /= Mathf.Pow(10, 9);
        }
        Debug.Log("Final val: " + calcTemp(6000,2,2,2,700000,150000000));
    }
    public float calcTemp(float sunTemp, int co2, int h2o, int o2, float sunRad, float sunDist)
    {
        double[] S = new double[L.Length];
        for (int i = 0; i < L.Length; i++)
        {
            double numerator = 8 * Mathf.PI * h * c;
            double lambda_5 = Mathf.Pow((float)L[i], 5);
            double power = (h * c / (L[i] * k * sunTemp));
            double denominator = lambda_5 * (System.Math.Pow(e,power)-1);
            S[i] = numerator / denominator;
        }
        double K1 = co2;
        double K2 = h2o;
        double K3 = o2;
        double[] B1;
        double[] B2;
        double[] B3;
        B1 = new double[12];
        for (int i = 0; i < B1.Length; i++)
        {
            B1[i] = 0;
        }
        double area = Mathf.Abs(4 * Mathf.PI * Mathf.Pow(Mathf.Abs(sunRad) * 1000, 2));
        if(K1 == 1)
        {
            B1[9] = 0.3;
            B1[10] = 0.2;
            B1[11] = 0.3;
        }else if (K1 == 2)
        {
            B1[9] = 0.7;
            B1[10] = 0.5;
            B1[11] = 0.7;
        }else if (K1 == 3)
        {
            B1[9] = 1;
            B1[10] = 1;
            B1[11] = 1;
        }
        B2 = new double[12];
        for(int i = 0; i < B2.Length; i++)
        {
            B2[i] = 0;
        }
        if (K2 == 1)
        {
            B2[11] = 0.3;
            B2[10] = 0.4;
            B2[9] = 0.4;
            B2[8] = 0.4;
            B2[7] = 0.4;
            B2[6] = 0.4;
            B2[5] = 0.3;
            B2[4] = 0.1;
        }else if (K2 == 2)
        {
            B2[11] = 0.7;
            B2[10] = 0.8;
            B2[9] = 0.8;
            B2[8] = 0.8;
            B2[7] = 0.8;
            B2[6] = 0.8;
            B2[5] = 0.6;
            B2[4] = 0.2;
        }else if (K2 == 3)
        {
            B2[11] = 1;
            B2[10] = 1;
            B2[9] = 1;
            B2[8] = 1;
            B2[7] = 1;
            B2[6] = 1;
            B2[5] = 0.8;
            B2[4] = 0.5;
        }
        B3 = new double[12];
        for(int i = 0; i < B3.Length; i++)
        {
            B3[i] = 0;
        }
        if (K3 == 1)
        {
            B3[11] = 0.4;
            B3[4] = 0.2;
        }else if (K3 == 2)
        {
            B3[11] = 0.8;
            B3[4] = 0.6;
        }else if (K3 == 3)
        {
            B3[11] = 1;
            B3[4] = 1;
        }
        double[] T = new double[12];
        for(int i = 0; i < 12; i++)
        {
            T[i] = (1 - B1[i]) * (1 - B2[i]) * (1 - B3[i]);
        }
        double[] l = new double[12];
        for(int i=0; i < 12; i++)
        {
            l[i] = T[i] * S[i];
        }
        double[] intensity_constant = { 100, 100, 100, 200, 200, 200, 200, 300, 500, 500, 7000, 9000 };
        double intensity = 0;
        for (int i = 0; i < 12; i++)
        {
            intensity += (intensity_constant[i] * l[i]) / Mathf.Pow(10, 16);
        }
        intensity *= (area * 2 * Mathf.Pow(10, -9)) / Mathf.Pow(sunDist / 150000000, 2);
        double intensity2 = 0;
        int t2 = 150;
        while (intensity2 < intensity)
        {
            intensity2 = 0;
            t2++;
            double[] S2 = new double[12];
            double[] l2 = new double[12];
            for(int i = 0; i < L.Length; i++)
            {
                double numerator = 8 * Mathf.PI * h * c*Mathf.Pow(10,15);
                double lambda_5 = Mathf.Pow((float)L[i], 5);
                double power = (h * c / (L[i] * k * t2));
                double denominator = lambda_5 * (System.Math.Pow(e, power) - 1);
                S2[i] = numerator / denominator;
            }
            for(int i = 0; i < 12; i++)
            {
                l2[i] = T[i] * S2[i];
            }
            for(int i = 0; i < 12; i++)
            {
                intensity2 += Mathf.Pow(10,5) * intensity_constant[i] * l2[i];
            }
        }
        return t2;
    }
    public double calc_temp(string planetType, float sunMass, float distSun, int co2, int h2o)
    {
        double sigma = 5.6703 * System.Math.Pow(10, -5);
        double L = 3.846 * System.Math.Pow(10, 33) * System.Math.Pow(sunMass, 3);
        double D = distSun * 1.496 * Mathf.Pow(10, 13);
        int albedo = 1;
        if (planetType == "earth" || planetType == "water")
            albedo = 25;
        else if (planetType == "rock" || planetType == "sand")
            albedo = 5;
        else if (planetType == "ice"||planetType=="gasgiant")
            albedo = 50;
        if (h2o == 1)
            albedo += 5;
        else if (h2o == 2)
            albedo += 10;
        else if (h2o == 3)
            albedo += 15;
        double A = albedo / 100;
        double greenhouse = 1;
        if (co2 == 1)
            greenhouse *= 0.7;
        else if (co2 == 3)
            greenhouse *= 10;
        else if (co2 == 0)
            greenhouse *= 0.4;
        if (h2o == 1)
            greenhouse *= 0.5;
        else if (h2o == 3)
            greenhouse *= 25;
        else if (h2o == 0)
            greenhouse *= 0.3;
        if (co2 == 0 && h2o == 0)
            greenhouse = 0;
        double T = greenhouse * 0.5841;
        double X = System.Math.Sqrt((1 - A) * L / (16 * Mathf.PI * sigma));
        double T_eff = System.Math.Sqrt(X) * (1 / System.Math.Sqrt(D));
        double T_eq = (System.Math.Pow(T_eff, 4)) * (1 + (3 * T / 4));
        double T_sur = T_eq / 0.9;
        double T_kel = System.Math.Sqrt(System.Math.Sqrt(T_sur));
        return T_kel;
    }
    public float gameToAu(float gameDist)
    {
        return gameDist/100;
    }
    public float gameToSolar(float mass)
    {
        return mass / (1.33f * Mathf.Pow(10, 11));
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
