using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
      if((  GameObject.FindObjectOfType<MovimientoPersonaje>()==null)){
            AudioManager.instance.Stop("Musica2");
            AudioManager.instance.Play("Musica1");
        }
        else{
            AudioManager.instance.Stop("Musica1");
            AudioManager.instance.Play("Musica2");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
