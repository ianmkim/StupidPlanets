using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour
{
    public static TextController txt;
    public GameObject tBox;
    public GameObject canvas;
    // Start is called before the first frame update
    void Start()
    {
        txt = this;
        newText("Welcome to Stupid Planets! Drag the mouse to look around, scroll to zoom, and right click a Stupid Planet to view it!", 500, 300);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void newText(string text, float x, float y)
    {
        GameObject textBox = Instantiate(tBox);
        textBox.transform.parent = canvas.transform;
        textBox.GetComponent<RectTransform>().position = new Vector3(x, y, 0);
        textBox.GetComponent<Message>().SetText(text);
    }
    public void killAll()
    {
        for(int i = 0; i < canvas.transform.childCount; i++)
        {
            if(canvas.transform.GetChild(i).gameObject.GetComponent<Message>() != null)
            {
                Destroy(canvas.transform.GetChild(i).gameObject);
            }
        }
    }
}
