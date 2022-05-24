using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    public float tiempoDesact = 3f;
    float auxTiempo = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void DesactivarSprites()
    {
        this.GetComponent<SpriteRenderer>().enabled = false;
        foreach (SpriteRenderer rend in this.transform.GetComponentsInChildren<SpriteRenderer>())
        {
            rend.enabled = false;
        }
    }
    void ActivarSprites()
    {
        this.GetComponent<SpriteRenderer>().enabled = true;
        foreach (SpriteRenderer rend in this.transform.GetComponentsInChildren<SpriteRenderer>())
        {
            rend.enabled = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (auxTiempo > 0)
        {
            DesactivarSprites();
            this.GetComponent<Collider2D>().enabled = false;
            auxTiempo -= Time.deltaTime;
            if (auxTiempo <= 0)
            {
                ActivarSprites();
                this.GetComponent<Collider2D>().enabled = true;
                auxTiempo = 0;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<MovimientoPersonaje>() != null)
        {
            AudioManager.instance.Play("PickUp");

            auxTiempo = tiempoDesact;
            collision.GetComponent<MovimientoPersonaje>().puedoKunai = true;
            collision.GetComponent<MovimientoPersonaje>().jumpCount = 1;
            collision.GetComponent<MovimientoPersonaje>().auxrecentPw = collision.GetComponent<MovimientoPersonaje>().recentPWtime;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<MovimientoPersonaje>() != null)
        {
            AudioManager.instance.Play("PickUp");

            auxTiempo = tiempoDesact;
            collision.GetComponent<MovimientoPersonaje>().puedoKunai = true;
            collision.GetComponent<MovimientoPersonaje>().jumpCount = 1;
            collision.GetComponent<MovimientoPersonaje>().auxrecentPw = collision.GetComponent<MovimientoPersonaje>().recentPWtime;
        }
    }
}
