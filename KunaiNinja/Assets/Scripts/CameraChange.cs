using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChange : MonoBehaviour
{
    public GameObject activeCamera;
    public GameObject respawnPos;
    public GameObject otherCameraChange;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void EnableCam()
    {

        if (respawnPos.activeSelf == false){
            respawnPos.SetActive(true);
        }
        GameManager.Instance.respawnPos = respawnPos;
        GameManager.Instance.panelFade.GetComponent<Animator>().SetTrigger("Fade");
        
        otherCameraChange.GetComponent<Collider2D>().enabled = false;
        print("fade");
        StartCoroutine(CambiarCamara(0.2f));
    }
    public IEnumerator CambiarCamara(float time)
    {
        yield return new WaitForSeconds(time);
        Camera[] cams = GameObject.FindObjectsOfType<Camera>();
        //List<Camera> camlist = new List<Camera>();
        //foreach(Camera cam in cams)
        //{
        //    if (cam.gameObject.activeSelf)
        //    {
        //        camlist.Add(cam);
        //    }
        //}

        foreach (Camera cam in cams)
        {
            cam.gameObject.SetActive(false);
        }
        activeCamera.SetActive(true);otherCameraChange.SetActive(true);
        otherCameraChange.GetComponent<Collider2D>().enabled = true;
        
        this.gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<MovimientoPersonaje>() != null)
        {
            this.GetComponent<Collider2D>().enabled = false;
            EnableCam();
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<MovimientoPersonaje>() != null)
        {
            this.GetComponent<Collider2D>().enabled = false;
            EnableCam();
        }
    }
}
