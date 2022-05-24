using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMenus : MonoBehaviour
{
    public GameObject panelNormal;
    public GameObject panelOpciones;
    public Slider sliderFX;
    public Slider sliderMusica;
    public void OpenClose()
    {
        AudioManager.instance.Play("Hud");

        panelNormal.SetActive(!panelNormal.activeSelf);
        panelOpciones.SetActive(!panelOpciones.activeSelf);
        if (panelOpciones.activeSelf)
        {
            sliderFX.value = GameManager.Instance.volumeMultiplierFX;
            sliderMusica.value = GameManager.Instance.volumeMultiplierMusic;
        }
      
    }
    public void CloseGame()
    {
        AudioManager.instance.Play("Hud");
        GameManager.Instance.CloseGame();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void SetFX()
    {
        AudioManager.instance.Play("Hud");

        GameManager.Instance.volumeMultiplierFX = sliderFX.value;
    }
    public void SetMusic()
    {
        AudioManager.instance.Play("Hud");

        GameManager.Instance.volumeMultiplierMusic = sliderMusica.value;
    }
    public void Play(string scene)
    {
        AudioManager.instance.Play("Hud");

        GameManager.Instance.LoadScene(scene);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
