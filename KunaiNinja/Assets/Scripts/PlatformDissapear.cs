using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDissapear : MonoBehaviour
{
    public float tiempoHastaCaer = 1f;
    public float tiempoRespawn = 2f;
    public float auxTiempoCaer = 0;
    public float auxTiempoRespawn = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (auxTiempoCaer > 0)
        {
            if (auxTiempoCaer < 0.2f * tiempoHastaCaer)
            {
                foreach (SpriteRenderer sp in this.GetComponentsInChildren<SpriteRenderer>())
                {
                    sp.GetComponent<SpriteRenderer>().color = new Color(sp.GetComponent<SpriteRenderer>().color.r, sp.GetComponent<SpriteRenderer>().color.g, sp.GetComponent<SpriteRenderer>().color.b, sp.GetComponent<SpriteRenderer>().color.a - 6f * Time.deltaTime);
                    if (sp.GetComponent<SpriteRenderer>().color.a < 0) sp.GetComponent<SpriteRenderer>().color = new Color(sp.GetComponent<SpriteRenderer>().color.r, sp.GetComponent<SpriteRenderer>().color.g, sp.GetComponent<SpriteRenderer>().color.b, 0);
                }
            }
            auxTiempoCaer -= Time.deltaTime;
            if (auxTiempoCaer <= 0)
            {
                AudioManager.instance.Play("FallPlat");

                GetComponent<Collider2D>().enabled = false;
                auxTiempoCaer = 0;
                auxTiempoRespawn = tiempoRespawn;
            }
        }
        if (auxTiempoRespawn > 0)
        {
            if (auxTiempoRespawn < 0.2f * tiempoRespawn)
            {
                foreach (SpriteRenderer sp in this.GetComponentsInChildren<SpriteRenderer>())
                {
                    sp.GetComponent<SpriteRenderer>().color = new Color(sp.GetComponent<SpriteRenderer>().color.r, sp.GetComponent<SpriteRenderer>().color.g, sp.GetComponent<SpriteRenderer>().color.b, sp.GetComponent<SpriteRenderer>().color.a + 3f * Time.deltaTime);
                    if (sp.GetComponent<SpriteRenderer>().color.a > 1) sp.GetComponent<SpriteRenderer>().color = new Color(sp.GetComponent<SpriteRenderer>().color.r, sp.GetComponent<SpriteRenderer>().color.g, sp.GetComponent<SpriteRenderer>().color.b, 1);
                }
            }
            auxTiempoRespawn -= Time.deltaTime;
            if (auxTiempoRespawn <= 0.2f)
            {       this.GetComponentInChildren<ParticleSystem>().Play();
                GetComponent<Collider2D>().enabled = true;
             
                if (auxTiempoRespawn <= 0.0f)
                {
                    AudioManager.instance.Play("PlatAppear");
                    auxTiempoRespawn = 0;
                }
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<MovimientoPersonaje>() != null)
        {
            if (collision.gameObject.transform.position.y > this.transform.position.y)
            {
                if (auxTiempoCaer <= 0)
                {
                    this.GetComponentInChildren<ParticleSystem>().Play();
                    auxTiempoCaer = tiempoHastaCaer;
                }
            }

        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<MovimientoPersonaje>() != null)
        {
            if (collision.gameObject.transform.position.y > this.transform.position.y)
            {
                if (auxTiempoCaer <= 0)
                {
                    auxTiempoCaer = tiempoHastaCaer;
                }
            }

        }
    }
}
