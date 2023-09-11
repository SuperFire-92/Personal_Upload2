using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public Canvas rend;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Canvas>();
        rend.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        rend.enabled = false;
    }
}
