using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolverMapa : MonoBehaviour
{
    public string scene = "Victory";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public IEnumerator Cargar(float time)
    {
        yield return new WaitForSeconds(time);
        GameManager.Instance.LoadScene(scene);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<MovimientoPersonaje>() != null)
        {
            AudioManager.instance.Play("Respawn");
            GameManager.Instance.panelFade.GetComponent<Animator>().SetTrigger("Fade");
            this.GetComponent<Collider2D>().enabled = false;
            StartCoroutine(Cargar(0.2f));
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<MovimientoPersonaje>() != null)
        {
            AudioManager.instance.Play("Respawn");
            GameManager.Instance.panelFade.GetComponent<Animator>().SetTrigger("Fade");
            this.GetComponent<Collider2D>().enabled = false;
            StartCoroutine(Cargar(0.2f));
        }
    }
}
