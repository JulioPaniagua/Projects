using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictoryScreen : MonoBehaviour
{
    public Text texto;
    public float timer = 3f;
    // Start is called before the first frame update
    void Start()
    {
        switch (GameManager.Instance.actualLevel)
        {
            case 0:
                texto.text = (string)(GameManager.Instance.coleccionablesCogidosLvl1.Count + " / " + GameManager.Instance.maxColecc[GameManager.Instance.actualLevel]);
                break;
            case 1:
                texto.text = (string)(GameManager.Instance.coleccionablesCogidosLvl2.Count + " / " + GameManager.Instance.maxColecc[GameManager.Instance.actualLevel]);
                break;
            case 2:
                texto.text = (string)(GameManager.Instance.coleccionablesCogidosLvl3.Count + " / " + GameManager.Instance.maxColecc[GameManager.Instance.actualLevel]);
                break;
            case 3:
                texto.text = (string)(GameManager.Instance.coleccionablesCogidosLvl4.Count + " / " + GameManager.Instance.maxColecc[GameManager.Instance.actualLevel]);
                break;
            case 4:
                texto.text = (string)(GameManager.Instance.coleccionablesCogidosLvl5.Count + " / " + GameManager.Instance.maxColecc[GameManager.Instance.actualLevel]);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            GameManager.Instance.LoadScene("Map");
        }
    }
}
