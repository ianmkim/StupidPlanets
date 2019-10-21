using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarGenerator : MonoBehaviour
{
    public int starCount = 100;
    public float starDist = 500;
    public GameObject star;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < starCount; i++)
        {
            GameObject thisStar = Instantiate(star);
            thisStar.transform.parent = transform;
            thisStar.transform.position= Random.onUnitSphere * starDist;
            thisStar.transform.rotation = Quaternion.LookRotation(new Vector3(0, 0, 0));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
