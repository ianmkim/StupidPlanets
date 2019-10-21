using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Message : MonoBehaviour
{
    public string text = "Sample Text";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DeleteSelf()
    {
        Destroy(gameObject);
    }
    public void SetText(string text)
    {
        transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = text;
    }
}
