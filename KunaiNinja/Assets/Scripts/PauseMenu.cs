using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject textoColeccionable;
    MovimientoPersonaje mp;
    // Start is called before the first frame update
    void Start()
    {
        mp = GameObject.FindObjectOfType<MovimientoPersonaje>();
    }
    public void Salir()
    {
        GameManager.Instance.CloseGame();
    }
    public void Map()
    {
        GameManager.Instance.LoadScene("Map");
    }
    public void OpenClose()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        if (pauseMenu.activeSelf)
        {
            textoColeccionable.GetComponent<TextoColeccionable>().UpdateScore();
            Time.timeScale = 0;
            mp.enabled = false;
            textoColeccionable.GetComponent<TextoColeccionable>().texto.color = new Color(textoColeccionable.GetComponent<TextoColeccionable>().texto.color.r, textoColeccionable.GetComponent<TextoColeccionable>().texto.color.g, textoColeccionable.GetComponent<TextoColeccionable>().texto.color.b, 1);
        }
        else
        {
            mp.enabled = true;
            Time.timeScale = 1;
            textoColeccionable.GetComponent<TextoColeccionable>().texto.color = new Color(textoColeccionable.GetComponent<TextoColeccionable>().texto.color.r, textoColeccionable.GetComponent<TextoColeccionable>().texto.color.g, textoColeccionable.GetComponent<TextoColeccionable>().texto.color.b,0);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)){
            OpenClose();
        }
    }
}
