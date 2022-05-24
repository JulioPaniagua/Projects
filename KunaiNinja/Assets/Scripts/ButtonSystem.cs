using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSystem : MonoBehaviour
{
    public GameObject[] buttonsList;
    public bool activa = false;
    public GameObject[] affectedObjects;
    public float timeBetweenButtons = 2.5f;
    public float auxtimeBetween = 0;
    public Sprite sinPulsar;
    public Sprite pulsado;
  
   public int count = 0;
    // Start is called before the first frame update
    void Start()
    {
   
    }
    public void CollidedChild(GameObject obj)
    {
        GameObject temp = null;
        foreach (GameObject go in buttonsList)
        {
            if (obj.name == go.name)
            {
                temp = go;
            }
        }
        if (temp != null)
        {
            if (temp.GetComponent<Collider2D>().enabled == true)
            {
               
                temp.GetComponent<Collider2D>().enabled = false;
                count++;
                auxtimeBetween = timeBetweenButtons;
            }
            temp.GetComponent<Animator>().SetTrigger("Down");
            AudioManager.instance.Play("ButtonPress");

        }
    }
    void EnableOrDisable()
    {
        count = 100;  auxtimeBetween = 0;
       
        foreach (GameObject go in affectedObjects)
        {
            go.gameObject.SetActive(!go.gameObject.activeSelf);
          
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.deadPlayer)
        {
            if (count == 100)
            {
                EnableOrDisable();
            }
            count = 0;
            auxtimeBetween = 0;
            foreach (GameObject go in buttonsList)
            {
                go.GetComponent<Animator>().SetTrigger("Up");
                go.GetComponent<Collider2D>().enabled = true;

            }
           

        }
        if (count >= buttonsList.Length&&count!=100)
        {
            EnableOrDisable();
            AudioManager.instance.Play("OpenButton");
        }
        else
        {


            if (auxtimeBetween > 0)
            {
               
                auxtimeBetween -= Time.deltaTime;
                if (auxtimeBetween <= 0)
                { count = 0;
                    auxtimeBetween = 0; AudioManager.instance.Play("ButtonUp");
                    foreach (GameObject go in buttonsList)
                    {
                      
                        go.GetComponent<Collider2D>().enabled = true;
                       go.GetComponent<Animator>().SetTrigger("Up");
                      
                        
                    }
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

    }
}
