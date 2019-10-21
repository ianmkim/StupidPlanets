using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reestimate : MonoBehaviour
{
    public double x1;
    public double x2;
    public double x3;
    public double co2;
    public double h2o;
    public double o2;
    // Start is called before the first frame update
    void Start()
    {
        if (co2 == 0)
            co2 = 1;
        else if (co2 == 1)
            co2 = 1.08;
        else if (co2 == 2)
            co2 = 1.25;
        else if (co2 == 3)
            co2 = 1.5;
        if (h2o == 0)
            h2o = 1;
        else if (h2o == 1)
            h2o = 1.1;
        else if (h2o == 2)
            h2o = 1.3;
        else if (h2o == 3)
            h2o = 1.6;
        if (o2 == 0)
            o2 = 1;
        else if (o2 == 1)
            o2 = 1.01;
        else if (o2 == 2)
            o2 = 1.02;
        else if (o2 == 3)
            o2 = 1.04;
        double temp = 290 * System.Math.Sqrt(x2 / x1) * x3 * (co2 * h2o * o2) / (1.25 * 1.3 * 1.02);
        Debug.Log("Re-estimate value: " + temp);
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
