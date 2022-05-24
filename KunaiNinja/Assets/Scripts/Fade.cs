using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void ResetChar()
    {

        GameObject.FindObjectOfType<MovimientoPersonaje>().dead = false;
        
        GameObject.FindObjectOfType<MovimientoPersonaje>().GetComponent<Animator>().SetBool("Dead",false);
        GameManager.Instance.deadPlayer = false;
        GameObject.FindObjectOfType<MovimientoPersonaje>().GetComponent<Rigidbody2D>().velocity = Vector2.zero;
   

    }
    public void TpChar()
    {     AudioManager.instance.Play("Respawn");
        GameManager.Instance.deadPlayer = true;
        GameObject.FindObjectOfType<MovimientoPersonaje>().gameObject.transform.position = GameManager.Instance.respawnPos.transform.position;
        GameObject.FindObjectOfType<MovimientoPersonaje>().GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
