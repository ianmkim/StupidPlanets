using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cameraBehavior : MonoBehaviour
{
    public enum cameraType {solar, planet};
    public cameraType currentView;
    public GameObject sun;
    public GameObject focus;
    public float moveSpeed = 0.5f;

    public GameObject planetMenu;
    public GameObject sunMenu;

    public Text myMass;
    public Text myRadius;
    public Text myOrbitRadius;
    public Text myTemp;
    public Text myco2;
    public Text myo2;
    public Text myh2o;

    void Start()
    {
        initializePos(false);
    }

    void Update()
    {
        move();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentView == cameraType.planet)
            {
                focus = sun;
                currentView = cameraType.solar;
                initializePos(false);
            }
        }
        if (Input.GetMouseButton(1))
        {
            if (currentView == cameraType.solar)
                onClick();
        }

        if (currentView == cameraType.solar)
        {
            sunMenu.SetActive(true);
            planetMenu.SetActive(false);
        }
        else
        {
            sunMenu.SetActive(false);
            planetMenu.SetActive(true);
            changePlanetText();
        }
    }

    void move()
    {
        if (currentView == cameraType.solar)
        {
            this.transform.parent = null;
            if (Input.GetAxis("Mouse X") < 0 && Input.GetMouseButton(0))
                this.transform.Translate(Vector3.left * moveSpeed);
            if (Input.GetAxis("Mouse X") > 0 && Input.GetMouseButton(0))
                this.transform.Translate(Vector3.right * moveSpeed);
            if (Input.GetAxis("Mouse Y") < 0 && Input.GetMouseButton(0))
                this.transform.Translate(Vector3.down * moveSpeed);
            if (Input.GetAxis("Mouse Y") > 0 && Input.GetMouseButton(0))
                this.transform.Translate(Vector3.up * moveSpeed);
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
                this.transform.Translate(Vector3.forward * moveSpeed);
            if (Input.GetAxis("Mouse ScrollWheel") < 0)
                this.transform.Translate(Vector3.back * moveSpeed);
        }
        else if (currentView == cameraType.planet)
        {
            this.transform.parent = focus.transform;
            if (Input.GetAxis("Mouse X") < 0 && Input.GetMouseButton(0))
                this.transform.RotateAround(focus.transform.position, Vector3.left, moveSpeed);
            if (Input.GetAxis("Mouse X") > 0 && Input.GetMouseButton(0))
                this.transform.RotateAround(focus.transform.position, Vector3.right, moveSpeed);
            if (Input.GetAxis("Mouse Y") < 0 && Input.GetMouseButton(0))
                this.transform.RotateAround(focus.transform.position, Vector3.down, moveSpeed);
            if (Input.GetAxis("Mouse Y") > 0 && Input.GetMouseButton(0))
                this.transform.RotateAround(focus.transform.position, Vector3.up, moveSpeed);
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
                this.transform.Translate(Vector3.forward * moveSpeed);
            if (Input.GetAxis("Mouse ScrollWheel") < 0)
                this.transform.Translate(Vector3.back * moveSpeed);
        }
    }

    public void initializePos(bool planet)
    {
        if (!planet)
        {
            this.transform.position = focus.transform.position + new Vector3(0, 100, 0);
            this.transform.rotation = Quaternion.Euler(90, 0, 0);
        }
        else
        {
            this.transform.position = focus.transform.position + new Vector3(0, 40, 0);
            this.transform.rotation = Quaternion.Euler(90, 0, 0);
        }
    }

    void onClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.gameObject.tag == "Planet")
            {
                focus = hit.transform.gameObject;
                currentView = cameraType.planet;
                initializePos(true);
            }
        }
    }

    public void changeTargetValues()
    {
        focus.GetComponent<planetOrbit>().newValues();
    }

    public void changePlanetText()
    {
        myTemp.text = focus.GetComponent<temperatureV2>().temperature.ToString();
        myMass.text = focus.GetComponent<planetOrbit>().mass.ToString();
        myRadius.text = focus.GetComponent<planetOrbit>().radius.ToString();
        myOrbitRadius.text = focus.GetComponent<planetOrbit>().orbitRadius.ToString();
        myco2.text = focus.GetComponent<planetOrbit>().co2.ToString();
        myo2.text = focus.GetComponent<planetOrbit>().o2.ToString();
        myh2o.text = focus.GetComponent<planetOrbit>().h2o.ToString();
    }
}
