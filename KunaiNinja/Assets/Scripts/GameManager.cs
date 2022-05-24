using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public GameObject panelFade;
    public bool deadPlayer = false;
    public float volumeMultiplierFX = 1f;
    public float volumeMultiplierMusic = 1f;
    public List<string> coleccionablesCogidosLvl1 = new List<string>();
    public List<string> coleccionablesCogidosLvl2 = new List<string>();
    public List<string> coleccionablesCogidosLvl3 = new List<string>();
    public List<string> coleccionablesCogidosLvl4 = new List<string>();
    public List<string> coleccionablesCogidosLvl5 = new List<string>();
    public int[] maxColecc;
    public int actualLevel = -1;
    public bool recienCogido = false;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GameManager>();
            }

            return _instance;
        }
    }
    public GameObject respawnPos;
    public void CloseGame()
    {
        Application.Quit();
    }
    public void CheckColecc()
    {
        switch (actualLevel)
        {
            case 0:
                foreach (Coleccionable go in GameObject.FindObjectsOfType<Coleccionable>())
                {
                    foreach (string st in coleccionablesCogidosLvl1)
                    {
                        if (go.gameObject.name == st)
                        {
                            go.gameObject.GetComponent<SpriteRenderer>().color = Color.green;
                        }
                    }

                }

                break;
            case 1:
                foreach (Coleccionable go in GameObject.FindObjectsOfType<Coleccionable>())
                {
                    foreach (string st in coleccionablesCogidosLvl2)
                    {
                        if (go.gameObject.name == st)
                        {
                            go.gameObject.GetComponent<SpriteRenderer>().color = Color.green;
                        }
                    }

                }
                break;
            case 2:
                foreach (Coleccionable go in GameObject.FindObjectsOfType<Coleccionable>())
                {
                    foreach (string st in coleccionablesCogidosLvl3)
                    {
                        if (go.gameObject.name == st)
                        {
                            go.gameObject.GetComponent<SpriteRenderer>().color = Color.green;
                        }
                    }

                }
                break;
            case 3:
                foreach (Coleccionable go in GameObject.FindObjectsOfType<Coleccionable>())
                {
                    foreach (string st in coleccionablesCogidosLvl4)
                    {
                        if (go.gameObject.name == st)
                        {
                            go.gameObject.GetComponent<SpriteRenderer>().color = Color.green;
                        }
                    }

                }
                break;
            case 4:
                foreach (Coleccionable go in GameObject.FindObjectsOfType<Coleccionable>())
                {
                    foreach (string st in coleccionablesCogidosLvl5)
                    {
                        if (go.gameObject.name == st)
                        {
                            go.gameObject.GetComponent<SpriteRenderer>().color = Color.green;
                        }
                    }

                }
                break;
        }

    }
    public void CogerColeccionable(string nombre)
    {
        AudioManager.instance.Play("Coleccionable");
      
        switch (actualLevel)
        {
            case 0:
                bool canTake = true;
                foreach (Coleccionable go in GameObject.FindObjectsOfType<Coleccionable>())
                {
                    foreach (string st in coleccionablesCogidosLvl1)
                    {
                        if (go.gameObject.name == st)
                        {
                            canTake = false;
                        }
                    }
                  
 
                }  if (canTake) coleccionablesCogidosLvl1.Add(nombre);
                recienCogido = true;
               
                break;
            case 1:
                bool canTake1 = true;
                foreach (Coleccionable go in GameObject.FindObjectsOfType<Coleccionable>())
                {
                    foreach (string st in coleccionablesCogidosLvl2)
                    {
                        if (go.gameObject.name == st)
                        {
                            canTake1 = false;
                        }
                    }

                }
                if (canTake1) coleccionablesCogidosLvl2.Add(nombre);
                recienCogido = true;

                break;
            case 2:
                bool canTake2 = true;

                foreach (Coleccionable go in GameObject.FindObjectsOfType<Coleccionable>())
                {
                    foreach (string st in coleccionablesCogidosLvl3)
                    {
                        if (go.gameObject.name == st)
                        {
                            canTake2 = false;
                        }
                    }

                }if (canTake2) coleccionablesCogidosLvl3.Add(nombre); 
                recienCogido = true;
                break;
            case 3:
                bool canTake3 = true;

                foreach (Coleccionable go in GameObject.FindObjectsOfType<Coleccionable>())
                {
                    foreach (string st in coleccionablesCogidosLvl4)
                    {
                        if (go.gameObject.name == st)
                        {
                            canTake3 = false;
                        }
                    }

                }if (canTake3) coleccionablesCogidosLvl4.Add(nombre);
                recienCogido = true; 
                break;
            case 4:
                bool canTake4 = true;
                foreach (Coleccionable go in GameObject.FindObjectsOfType<Coleccionable>())
                {
                    foreach (string st in coleccionablesCogidosLvl5)
                    {
                        if (go.gameObject.name == st)
                        {
                            canTake4 = false;
                        }
                    }

                }if (canTake4) coleccionablesCogidosLvl5.Add(nombre);
               recienCogido = true;  
               
                break;
              
        }
     
    }
    public void LoadScene(string scene)
    {
        switch (scene)
        {
            case "Level1":
                actualLevel = 0;
                break;
            case "Level2":
                actualLevel = 1;
                break;
            case "Level3":
                actualLevel = 2;
                break;
            case "Level4":
                actualLevel = 3;
                break;
            case "Level5":
                actualLevel = 4;
                break;
        }
        SceneManager.LoadScene(scene);

    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (respawnPos == null)
        {
            if (GameObject.FindGameObjectWithTag("Respawn") != null) respawnPos = GameObject.FindGameObjectWithTag("Respawn");
        }
        if (panelFade == null)
        {
            if (GameObject.FindGameObjectWithTag("PanelFade") != null) panelFade = GameObject.FindGameObjectWithTag("PanelFade");
        }
    }
}
