using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<MovimientoPersonaje>() != null)
        {
            GetComponent<Animator>().ResetTrigger("Up");
            transform.parent.GetComponent<ButtonSystem>().CollidedChild(this.gameObject);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<MovimientoPersonaje>() != null )
        {
           GetComponent<Animator>().ResetTrigger("Up");
            transform.parent.GetComponent<ButtonSystem>().CollidedChild(this.gameObject);
        }
    }
}
