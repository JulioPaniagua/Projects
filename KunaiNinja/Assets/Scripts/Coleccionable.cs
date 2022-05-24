using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coleccionable : MonoBehaviour
{
    GameObject player;
    Vector2 originalPos;
    bool started = false;
    bool grounded = false;
    public float speed = 10;
    public GameObject particulas;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.CheckColecc();
        player = GameObject.FindObjectOfType<MovimientoPersonaje>().gameObject;
        originalPos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (started)
        {
            this.transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
            if (player.GetComponent<MovimientoPersonaje>().grounded == true&&player.GetComponent<MovimientoPersonaje>().alltrueGroundcheck == true&&!player.GetComponent<MovimientoPersonaje>().touchingwall&& !player.GetComponent<MovimientoPersonaje>().dead)
            {
                GameManager.Instance.CogerColeccionable(this.gameObject.name);
                GameObject part = Instantiate(particulas, this.transform.position, Quaternion.identity) as GameObject;
                Destroy(part, 2f);
                this.gameObject.SetActive(false);
            }
            else
            {
                if (player.GetComponent<MovimientoPersonaje>().dead == true)
                {
                    this.transform.position = originalPos;
                    started = false;
                    this.GetComponent<Collider2D>().enabled = true;
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<MovimientoPersonaje>() != null)
        {
            started = true;
            this.GetComponent<Collider2D>().enabled = false;

        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<MovimientoPersonaje>() != null)
        {
            started = true;
            this.GetComponent<Collider2D>().enabled = false;

        }
    }
}
