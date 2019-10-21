using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    public static GameMaster GM;
    public GameObject planet;
    public int maxPlanets;
    public float minRadius;
    public float maxRadius;
    public GameObject[] planets;
    public GameObject myCamera;
    public GameObject spider;

    void Start()
    {
        GM = this;
        makePlanets();
    }

    void Update()
    {

    }

    void makePlanets()
    {
        int planetNum = Random.Range(3, maxPlanets);
        float[] radii = generateRadii(planetNum);
        for (int i = 0; i < planetNum; i++)
        {
            Vector3 planetPos = new Vector3(0, 0, Random.Range(1, 100));
            GameObject newPlanet = Instantiate(planets[Random.Range(0, planets.Length)], planetPos, Quaternion.identity);
            newPlanet.SetActive(true);
            //newPlanet.transform.parent = planet.transform;
            //newPlanet.GetComponent<Planet>().GeneratePlanet();
            newPlanet.GetComponent<planetOrbit>().orbitRadius = radii[i];
        }
    }

    float[] generateRadii(int planetNum)
    {
        float[] radii = new float[planetNum];
        for (int i = 0; i < planetNum; i++)
        {
            int flag = 0;
            bool canNext = false;
            while (!canNext)
            {
                float temp = Random.Range(minRadius, maxRadius);
                for (int j = 0; j < planetNum; j++)
                {
                    if (Mathf.Abs(radii[j] - temp) <= 10 || temp <= 10)
                        flag++;
                }
                if (flag > 0)
                    flag = 0;
                else
                {
                    radii[i] = temp;
                    canNext = true;
                }
            }
        }
        return radii;
    }

    public void quitGame()
    {
        SceneManager.LoadScene("menu");
    }

    public void Win(GameObject planet)
    {
        myCamera.GetComponent<cameraBehavior>().focus = planet;
        myCamera.GetComponent<cameraBehavior>().currentView = cameraBehavior.cameraType.planet;
        GameObject newSpider = Instantiate(spider, planet.transform.position + new Vector3(0, 5, 0), Quaternion.identity);
        newSpider.transform.localScale = new Vector3(20,20,20);
        newSpider.GetComponent<PlayerMovementScript>().target = planet.transform;
        newSpider.SetActive(true);
        StartCoroutine(spooder());
        Debug.Log("won");
    }
    IEnumerator spooder(){

        yield return new WaitForSeconds(5);
        quitGame();
    }

}