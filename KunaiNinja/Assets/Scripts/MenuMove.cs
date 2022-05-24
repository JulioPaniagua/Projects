using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuMove : MonoBehaviour
{
    public Animator animator;
    public GameObject[] pos;
    public string[] names;
    public int count = 0;
    public float xInput;
    public float timerAfterArrive = 0.4f;
    public float auxTimer;
    public bool destination = false;
    public Vector2 destinationGo;
    public float speed = 10;
    public Vector3 originalScale;
    public Vector3 actualScale;
    public Text[] textosColecc;
    // Start is called before the first frame update
    void Start()
    {
        textosColecc[0].text = (string)(GameManager.Instance.coleccionablesCogidosLvl1.Count + " / " + GameManager.Instance.maxColecc[0]);
        textosColecc[1].text = (string)(GameManager.Instance.coleccionablesCogidosLvl2.Count + " / " + GameManager.Instance.maxColecc[1]);
        textosColecc[2].text = (string)(GameManager.Instance.coleccionablesCogidosLvl3.Count + " / " + GameManager.Instance.maxColecc[2]);
        textosColecc[3].text = (string)(GameManager.Instance.coleccionablesCogidosLvl4.Count + " / " + GameManager.Instance.maxColecc[3]);
        textosColecc[4].text = (string)(GameManager.Instance.coleccionablesCogidosLvl5.Count + " / " + GameManager.Instance.maxColecc[4]);

        this.transform.position = new Vector2(pos[GameManager.Instance.actualLevel].transform.position.x,this.transform.position.y);
        count = GameManager.Instance.actualLevel;
        originalScale = this.transform.localScale;
        actualScale = originalScale;
    }
    public Sprite[] sprites;
    public Sprite[] spritesIdle;
    public int spritePerFrame = 6;
    public bool loop = true;
    public bool destroyOnEnd = false;

    private int index = 0;
    private Image image;
    private int frame = 0;

    void Awake()
    {
        image = GetComponentsInChildren<Image>()[1];
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.X))
        {
            if (auxTimer > 0 || destination == false)
            {
                GameManager.Instance.LoadScene(names[count]);
            }
        }

        if (auxTimer > 0)
        {
       
            auxTimer -= Time.deltaTime;
            if (auxTimer <= 0)
            {
                auxTimer = 0;
                destination = false;

            }
        }
        xInput = Input.GetAxisRaw("Horizontal");
        if (xInput > 0 && destination == false)
        {
            this.transform.localScale = new Vector3(originalScale.x, originalScale.y, originalScale.z);
            if (count < pos.Length)
            {
                destination = true;
                count++;
                destinationGo = new Vector2(pos[count].transform.position.x, this.transform.position.y);
            }
        }
        else if (xInput < 0 && destination == false)
        {
            this.transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z);

            if (count > 0)
            {
                destination = true;
                count--;
                destinationGo = new Vector2(pos[count].transform.position.x, this.transform.position.y);
            }
        }
        if (destination == true)
        {
        
            this.transform.position = Vector2.MoveTowards(this.transform.position, destinationGo, speed * Time.deltaTime);
            if (Vector2.Distance(this.transform.position, destinationGo) < 1)
            {
                if (auxTimer == 0)
                {
                    auxTimer = timerAfterArrive;
                   

               
                }
                else
                {
 index = 0;
                    if (!loop && index == spritesIdle.Length) return;
                    frame++;
                    if (frame < spritePerFrame) return;
                    image.sprite = spritesIdle[index];
                    frame = 0;
                    index++;
                    if (index >= spritesIdle.Length)
                    {
                        if (loop) index = 0;
                        if (destroyOnEnd) Destroy(gameObject);
                    }
                   
                    frame = 0;
                }
            }
            else

            {

                if (!loop && index == sprites.Length) return;
                frame++;
                if (frame < spritePerFrame) return;
                image.sprite = sprites[index];
                frame = 0;
                if (frame % 3 == 0)
                {
                    AudioManager.instance.Play("Footstep");
                }
                index++;
                if (index >= sprites.Length)
                {
                    if (loop) index = 0;
                    if (destroyOnEnd) Destroy(gameObject);
                }


            }
           
        }
        else
        {
            if (!loop && index == spritesIdle.Length) return;
            frame++;
            if (frame < spritePerFrame) return;
            image.sprite = spritesIdle[index];
            frame = 0;
            index++;
            if (index >= spritesIdle.Length)
            {
                if (loop) index = 0;
                if (destroyOnEnd) Destroy(gameObject);
            }
        }
       
        {

        }
    }
}
