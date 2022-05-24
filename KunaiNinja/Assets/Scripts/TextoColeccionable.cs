using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextoColeccionable : MonoBehaviour
{
    bool cogido = false;
    float auxtimerColor = 0f;
    bool bajando = false;
   public Text texto;

    // Start is called before the first frame update
    void Start()
    {
        texto = this.GetComponent<Text>();
        texto.color = new Color(texto.color.r, texto.color.g, texto.color.b, 0);
    }
    public void UpdateScore()
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
        if (GameManager.Instance.recienCogido == true)
        {
            GameManager.Instance.recienCogido = false;
            cogido = true;
     
            UpdateScore();
           
        }
        if (cogido)
        {
          
            texto.color = new Color(texto.color.r, texto.color.g, texto.color.b, texto.color.a + 6 * Time.deltaTime);
            if (texto.color.a >= 1)
            {
                texto.color = new Color(texto.color.r, texto.color.g, texto.color.b, 1);
                cogido = false;
                auxtimerColor = 2f;
            }
        }
        else
        {
            if (bajando)
            {
                texto.color = new Color(texto.color.r, texto.color.g, texto.color.b, texto.color.a - 1 * Time.deltaTime);
                if (texto.color.a <= 0)
                {
                    texto.color = new Color(texto.color.r, texto.color.g, texto.color.b, 0);
                    bajando = false;
                }
            }
            if (auxtimerColor > 0)
            {
                auxtimerColor -= Time.deltaTime;

                if (auxtimerColor <= 0)
                {

                    auxtimerColor = 0;
                    bajando = true;
                }
            }
        }
    }
}
