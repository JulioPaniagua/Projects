using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MovimientoPersonaje : MonoBehaviour
{
    public enum State
    {
        normalLocomotion,
        wallLocomotion
    }
    public State currentState = State.normalLocomotion;
    [Header("Movement")]
    public float acceleration = 0.5f;
    public float maxSpeed = 3f;
    public float currentSpeed = 0f;
    public float xInput = 0;
    public float yInput = 0;
    public float lastXinput = 0;
    public float lastYinput = 0;
    public bool facingRight = true;
    public bool recentTurn = false;
    public bool haveMaxSpeed = false;
    [Header("Jump")]
    public bool jumpStart = false;
    public bool jumpPressed = false;
    public bool jumpReleased = false;
    public float jumpSpeed = 1;
    public float actualJumpSpeed = 0;
    public float jumpDeccel = 0.2f;
    public float maxAscendingTime = 0.33f;
    public float auxAscend = 0;
    public float graceTime = 0.1f;
    public float auxGrace = 0;
    public float jumpCount = 0;
    public float dJumpSpeed = 2;
    public bool nospeedJump = false;
    public float auxNoSpeedJump = 0.12f;
    public float djumpDeccel = 0.1f;
    public float dmaxAscendingTime = 0.23f;
    public float dauxAscend = 0;
    public float dgraceTime = 0.08f;
    public float dauxGrace = 0;
    public float coyoteTime = 0.2f;
    public float jumpBuffer = 0.25f;
    public float auxCoyote = 0;
    public float auxJumpBuffer = 0;
    public bool bufferedJump = false;
    [Header("WallMove")]
    public bool touchingwall = false;
    public float accelDownWall = 10;
    public float currentDownSpeed = 0;
    public float noWallDetect = 0.12f;
    float auxnoWall = 0;
    public bool touchingLeft = false;
    public bool touchingRight = false;
    public bool isWalljump = false;
    public float maxWallAscend = 0.3f;
    public float graceWall = 0.05f;
    [Header("GroundDetection")]
    public bool grounded = true;
    float noGroundDetect = 0.12f;
    float auxnoGround = 0;
    public Transform ray1;
    public Transform ray2;
    public Transform ray3;
    public Transform raytopRight;
    public Transform raybotRight;
    public Transform raytopLeft;
    public Transform raybotLeft;
    public Transform raymidTop;
    public bool timerInitiatedwall = false;
    public float timerWall = 1f;
    public float auxtimerWall = 0;
    public LayerMask layerMaskGrounded;
    Rigidbody2D rb;
    [Header("Kunai")]
    public bool kunaiSpawned = false;
    public GameObject kunaiprefab;
    public float speedKunai = 100;
    public float kunaiDeccel = 10f;
    public bool kunaiPressed = false;
    public bool kunaiStart = false;
    public bool kunaiEnd = false;
    public float timeKunaiPress = 0;
    public float currentSpeedKunai = 0;
    public float kunaiDuration = 2f;
    public float auxkunaiDur = 0;
    public bool puedoKunai = true;
    public bool waitingReturnKunai = false;
    public float kunaiLifetime = 0;

    public bool recentTP = false;
    public float timeAfterTp = 0;
    public GameObject[] posKunai;
    GameObject kunai;
    public GameObject jumpParticles;
    public GameObject particulasTP;
    public Transform posJumpPart;
    public ParticleSystem leftWall;
    public Transform tpParticles;
    public ParticleSystem rightWall;
    public float speedYdownLimit = 1.5f;
    public float recentPWtime = 0.22f;
    public float auxrecentPw;
    public bool dead = false;
    public bool alltrueGroundcheck = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        Time.timeScale = 1;
    }
    void FixedUpdate()
    {
        if (!dead)
        {
           
            if (rb.velocity.y < -speedYdownLimit)
            {
                rb.velocity = new Vector2(rb.velocity.x, -speedYdownLimit);
            }
            if (facingRight)
            {
                this.GetComponent<SpriteRenderer>().flipX = false;
            }
            else
            {
                this.GetComponent<SpriteRenderer>().flipX = true;
            }
            this.GetComponent<Animator>().SetFloat("VelocityY", rb.velocity.y);
            this.GetComponent<Animator>().SetFloat("VelocityX", rb.velocity.x);
            this.GetComponent<Animator>().SetFloat("AbsVelocityX", Mathf.Abs(rb.velocity.x));

            if (touchingwall)
            {
                if (rb.velocity.y < 0 && timerInitiatedwall == false)
                {
                    rb.velocity = new Vector2(0, 0);

                    if (timerInitiatedwall == false)
                    {
                        currentDownSpeed = 0;
                        timerInitiatedwall = true;
                        auxtimerWall = timerWall;
                    }
                }
                if (auxtimerWall > 0)
                {
                    currentDownSpeed -= accelDownWall;
                    rb.velocity = new Vector2(rb.velocity.x, currentDownSpeed * Time.deltaTime);
                    auxtimerWall -= Time.deltaTime;
                    if (auxtimerWall <= 0)
                    {
                        auxtimerWall = 0;
                        timerInitiatedwall = false;
                    }
                }

            }
            else
            {
                timerInitiatedwall = false;
                auxtimerWall = 0;
            }

            if (auxnoWall <= 0)
            {
                if (xInput != 0)
                {

                    Acelerar();
                }
                else
                {
                    Frenar();
                }
            }

            if (jumpReleased == true)
            {

                if ((auxGrace > 0) && (rb.velocity.y > 0))
                {
                    rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
                }
                else
                {
                    if ((dauxGrace > 0) && (rb.velocity.y > 0))
                    {
                        rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
                    }
                }
                jumpReleased = false;
            }

            Salto();
            CheckFloor();
            CheckCeiling();
            CheckWalls();
            if (GameManager.Instance.actualLevel != 0) Kunai();


        }
        else
        {
            rb.velocity = Vector2.zero;
        }





    }
    private void Update()
    {
        if (!dead)
        {
            if (auxrecentPw > 0)
            {
                auxrecentPw -= Time.deltaTime;
                if (auxrecentPw <= 0)
                {
                    auxrecentPw = 0;
                }
            }

            if (GameManager.Instance.actualLevel != 0)
            {
                if (Input.GetKeyDown(KeyCode.X))
                {
                    if (kunaiSpawned == false && puedoKunai == true)
                    {
                        kunaiStart = true;
                        kunaiEnd = false;
                    }
                    else if (kunaiSpawned == true && puedoKunai == true)
                    {
                        kunaiStart = true;

                        if (auxrecentPw <= 0) { puedoKunai = false; }
                        else
                        {

                            kunaiEnd = false;
                            auxrecentPw = 0;
                        }

                    }
                }
                if (Input.GetKey(KeyCode.X))
                {

                    timeKunaiPress += Time.deltaTime;
                    kunaiPressed = true;

                }
                else
                {

                    kunaiPressed = false;
                }
                if (Input.GetKeyUp(KeyCode.X))
                {
                    if (kunaiSpawned == true)
                    {
                        kunaiEnd = true;


                    }
                }
            }


            if (nospeedJump == true)
            {
                auxNoSpeedJump -= Time.deltaTime;
                if (auxNoSpeedJump < 0)
                {
                    nospeedJump = false;
                }

            }
            else
            {
                auxNoSpeedJump = 0.08f;
            }

            xInput = Input.GetAxisRaw("Horizontal");
            yInput = Input.GetAxisRaw("Vertical");
            if (Input.GetAxisRaw("Horizontal") != 0)
            {
                lastXinput = Input.GetAxisRaw("Horizontal");
            }
            if (auxnoGround > 0)
            {
                auxnoGround -= Time.deltaTime;
                if (auxnoGround <= 0)
                {
                    auxnoGround = 0;
                }
            }
            if (auxnoWall > 0)
            {
                auxnoWall -= Time.deltaTime;
                if (auxnoWall <= 0)
                {
                    auxnoWall = 0;
                }
            }
            if (bufferedJump)
            {
                if (grounded && auxJumpBuffer > 0)
                {
                    jumpCount = 1;
                    this.GetComponent<Animator>().SetTrigger("JumpStart");
                    AudioManager.instance.Play("Jump");

                    jumpPressed = true;
                    jumpStart = true;

                    auxAscend = maxAscendingTime;
                    auxGrace = graceTime;


                    grounded = false;
                    bufferedJump = false;
                }
                else if (auxJumpBuffer <= 0)
                {
                    bufferedJump = false;
                }
                auxJumpBuffer -= Time.deltaTime;

            }

            if (Input.GetKeyDown(KeyCode.C))
            {

                if (grounded)
                {
                    jumpCount++;
                    jumpReleased = false;
                    jumpStart = true;
                    jumpPressed = true;
                    auxAscend = maxAscendingTime;
                    auxGrace = graceTime;
                    if ((auxCoyote > 0) && recentTP)
                    {
                        AudioManager.instance.Play("DoubleJump");

                        this.GetComponent<Animator>().SetTrigger("DoubleJump");
                    }
                    else
                    {
                        this.GetComponent<Animator>().SetTrigger("JumpStart");
                        AudioManager.instance.Play("Jump");
                    }

                    grounded = false;
                }
                else
                {
                    if (touchingwall)
                    {
                        AudioManager.instance.Play("Jump");

                        this.GetComponent<Animator>().SetTrigger("JumpStart");
                        auxnoWall = noWallDetect;
                        jumpCount = 1;
                        jumpReleased = false;
                        jumpStart = true;
                        jumpPressed = true;
                        auxAscend = maxWallAscend;
                        auxGrace = graceWall;
                        rb.velocity = new Vector2(rb.velocity.x, 0);

                        grounded = false;
                    }
                    else
                    {
                        if (jumpCount == 1)
                        {
                            AudioManager.instance.Play("DoubleJump");

                            this.GetComponent<Animator>().SetTrigger("DoubleJump");
                            jumpCount++;
                            jumpReleased = false;
                            jumpStart = true;
                            jumpPressed = true;
                            dauxAscend = dmaxAscendingTime;
                            dauxGrace = dgraceTime;
                            rb.velocity = new Vector2(rb.velocity.x, 0);
                        }
                        else if (jumpCount >= 2)
                        {
                            jumpCount++;
                            bufferedJump = true;
                            auxJumpBuffer = jumpBuffer;
                        }
                    }

                }


            }
            if (Input.GetKey(KeyCode.C))
            {
                jumpPressed = true;
                jumpReleased = false;
            }
            else
            {
                if (jumpPressed == true)
                {
                    jumpPressed = false;
                    jumpReleased = true;
                    jumpStart = false;
                }

            }
            if (Input.GetKeyUp(KeyCode.C))
            {
                jumpStart = false;
                jumpPressed = false;
                jumpReleased = true;
            }
        }
    }
    
    public void OnEndDeathAnim()
    {
        GameManager.Instance.panelFade.GetComponent<Animator>().SetTrigger("Fade");
        if (kunai != null) kunai.SetActive(false);
        kunaiLifetime = 0;
        kunaiSpawned = false;
        Time.timeScale = 1;

    }

    void Kunai()
    {


        if (recentTP)
        {
            timeAfterTp += Time.deltaTime;
            if (timeAfterTp < 0.15f)
            {
                if (timeAfterTp < 0.07f)
                {
                    if (rb.velocity.y < 0) rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
                    Time.timeScale = 0.5f;
                }
                else
                {
                    Time.timeScale = 1f;
                }

            }
            if (timeAfterTp > 0.22f)
            {
                timeAfterTp = 0;
                recentTP = false;
            }
        }
        if (kunaiSpawned)
        {
            kunaiLifetime += Time.deltaTime;
            if (kunaiLifetime < 0.28f)
            {
                if (kunaiLifetime < 0.12)
                {
                    Time.timeScale = 0.7f;
                }
                else
                {
                    Time.timeScale = 1f;
                }
                if (rb.velocity.y < 0) rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            }
            if (auxkunaiDur > 0)
            {
                currentSpeedKunai -= (kunaiDeccel * Time.deltaTime) + kunaiDeccel * Time.deltaTime;
                if (currentSpeedKunai < 0) currentSpeedKunai = 0;
                kunai.GetComponent<Rigidbody2D>().velocity = (kunai.transform.up * currentSpeedKunai * Time.deltaTime);
                auxkunaiDur -= Time.deltaTime;
                if (auxkunaiDur <= 0)
                {
                    auxkunaiDur = 0;
                    kunai.SetActive(false);
                    waitingReturnKunai = false;
                    AudioManager.instance.Play("KunaiDestroy");
                    kunaiLifetime = 0;
                    kunaiSpawned = false;
                }

            }
            if (kunaiEnd)
            {
                if (timeKunaiPress > 0.33f)
                {
                    GameObject part = Instantiate(particulasTP, tpParticles.transform.position, Quaternion.identity) as GameObject;
                    Destroy(part, 2f);
                    this.transform.position = kunai.transform.position;
                    kunai.SetActive(false);
                    AudioManager.instance.Play("Tp");

                    kunaiLifetime = 0;
                    GameObject part2 = Instantiate(particulasTP, tpParticles.transform.position, Quaternion.identity) as GameObject;
                    Destroy(part2, 2f);
                    recentTP = true;

                    kunaiSpawned = false;
                    if (auxrecentPw <= 0)
                    {
                        puedoKunai = false;
                    }
                    else
                    {

                        auxrecentPw = 0;
                    }
                    if (rb.velocity.y < 0)
                    {
                        rb.velocity = new Vector2(rb.velocity.x, 0);
                    }
                    if (jumpCount == 0 && (grounded || auxCoyote > 0))
                    {
                        jumpCount = 1;



                        dauxAscend = dmaxAscendingTime;
                        dauxGrace = dgraceTime;
                        rb.velocity = new Vector2(rb.velocity.x, 0);
                    }
                    waitingReturnKunai = false;
                    auxkunaiDur = 0;
                }
                else
                {

                    waitingReturnKunai = true;
                }
                timeKunaiPress = 0;
            }

        }
        else
        {
            if (kunaiEnd)
            {
                kunaiStart = false;
                waitingReturnKunai = false;
                auxkunaiDur = 0;

                timeKunaiPress = 0;
            }
        }
        if (kunaiStart && kunaiSpawned && waitingReturnKunai)
        {
            GameObject part = Instantiate(particulasTP, tpParticles.transform.position, Quaternion.identity) as GameObject;
            Destroy(part, 2f);
            this.transform.position = kunai.transform.position;
            kunai.SetActive(false);
            AudioManager.instance.Play("Tp");

            GameObject part2 = Instantiate(particulasTP, tpParticles.transform.position, Quaternion.identity) as GameObject;
            Destroy(part2, 2f);
            kunaiSpawned = false;
            kunaiLifetime = 0;
            recentTP = true;
            if (auxrecentPw <= 0) { puedoKunai = false; }
            else
            {

                auxrecentPw = 0;
            }
            if (rb.velocity.y < 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
            }
            if (jumpCount == 0 && (grounded || auxCoyote > 0))
            {
                jumpCount = 1;
            }
            waitingReturnKunai = false;

            auxkunaiDur = 0;
        }
        if (kunaiStart && kunaiSpawned == false && !waitingReturnKunai && recentTP == false && puedoKunai && (kunai == null || kunai.gameObject.activeSelf == false))
        {
            kunaiStart = false;
            kunai = Instantiate(kunaiprefab, this.transform.position, Quaternion.identity);
            this.GetComponent<Animator>().SetTrigger("KunaiThrow");
            AudioManager.instance.Play("Throw");

            Vector2 finalDir = new Vector2(lastXinput, 0);
            if (xInput == 1)
            {
                if (yInput == 1)
                {
                    finalDir = new Vector2(1, 1);
                }
                else if (yInput == -1)
                {
                    finalDir = new Vector2(1, -1);
                }
                if (yInput == 0)
                {
                    finalDir = new Vector2(1, 0);
                }
            }
            else if (xInput == -1)
            {
                if (yInput == 1)
                {
                    finalDir = new Vector2(-1, 1);
                }
                else if (yInput == -1)
                {
                    finalDir = new Vector2(-1, -1);
                }
                if (yInput == 0)
                {
                    finalDir = new Vector2(-1, 0);
                }
            }
            else if (xInput == 0)
            {
                if (yInput == 1)
                {
                    finalDir = new Vector2(0, 1);
                }
                else if (yInput == -1)
                {
                    finalDir = new Vector2(0, -1);
                }
                if (yInput == 0)
                {
                    finalDir = new Vector2(lastXinput, 0);
                }
            }
            kunaiSpawned = true;
            kunai.transform.up = finalDir;
            currentSpeedKunai = speedKunai;
            auxkunaiDur = kunaiDuration;
            kunai.GetComponent<Rigidbody2D>().velocity = (kunai.transform.up * currentSpeedKunai * Time.deltaTime);
        }
    }
    // Update is called once per frame
    void CheckWalls()
    {
        Debug.DrawRay(raytopRight.transform.position, Vector2.right * 0.04f, Color.red, 0.2f);
        Debug.DrawRay(raybotRight.transform.position, Vector2.right * 0.04f, Color.blue, 0.2f);
        Debug.DrawRay(raytopLeft.transform.position, Vector2.left * 0.04f, Color.blue, 0.2f);
        Debug.DrawRay(raybotLeft.transform.position, Vector2.left * 0.04f, Color.blue, 0.2f);


        RaycastHit2D rayC1 = Physics2D.Raycast(raytopRight.transform.position, Vector2.right, 0.04f, layerMaskGrounded);
        RaycastHit2D rayC2 = Physics2D.Raycast(raybotRight.transform.position, Vector2.right, 0.04f, layerMaskGrounded);
        RaycastHit2D rayC3 = Physics2D.Raycast(raytopLeft.transform.position, Vector2.left, 0.04f, layerMaskGrounded);
        RaycastHit2D rayC4 = Physics2D.Raycast(raybotLeft.transform.position, Vector2.left, 0.04f, layerMaskGrounded);
        if (auxnoWall <= 0)
        {
            if (grounded == false)
            {
                if ((rayC1 || rayC2 || rayC3 || rayC4) && !(rayC1.collider != null && rayC1.collider.GetComponent<PlatformEffector2D>() != null || rayC2.collider != null && rayC2.collider.GetComponent<PlatformEffector2D>() != null || rayC3.collider != null && rayC3.collider.GetComponent<PlatformEffector2D>() != null || rayC4.collider != null && rayC4.collider.GetComponent<PlatformEffector2D>() != null))
                {

                    this.GetComponent<Animator>().SetBool("TouchingWalls", true);
                    touchingwall = true;
                    if (rb.velocity.y < 0)
                    {
                        Sound s = Array.Find(AudioManager.instance.sounds, item => item.name == "ScratchWall");
                        if (s == null)
                        {
                            Debug.LogWarning("Sound: " + name + " not found!");
                            return;
                        }
                        if (!s.source.isPlaying) AudioManager.instance.Play("ScratchWall");

                    }
                    jumpCount = 1;
                    if (rayC1 || rayC2)
                    {
                        touchingRight = true;
                        touchingLeft = false;
                        if (rightWall.isStopped)
                        {
                            rightWall.Play();
                        }

                        //leftWall.Stop();
                    }
                    else
                    {
                        touchingRight = false;
                        touchingLeft = true;
                        if (leftWall.isStopped)
                        {
                            leftWall.Play();
                        }

                        //rightWall.Stop();
                    }
                }
                else if (!rayC1 && !rayC2 & !rayC3 && !rayC4)
                {
                    if (touchingwall)
                    {
                        if (auxAscend <= 0)
                        {
                            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
                        }
                    }
                    this.GetComponent<Animator>().SetBool("TouchingWalls", false);
                    if (leftWall.isPlaying)
                    {
                        leftWall.Stop();
                    }
                    if (rightWall.isPlaying)
                    {
                        rightWall.Stop();
                    }


                    AudioManager.instance.Stop("ScratchWall");

                    touchingwall = false;
                }

            }
            else
            {
                AudioManager.instance.Stop("ScratchWall");
                rightWall.Stop();
                leftWall.Stop();
                touchingwall = false;
                this.GetComponent<Animator>().SetBool("TouchingWalls", false);
            }
        }


    }
    void CheckCeiling()
    {
        Debug.DrawRay(raytopRight.transform.position, Vector2.up * 0.02f, Color.red, 0.2f);
        Debug.DrawRay(raytopLeft.transform.position, Vector2.up * 0.02f, Color.blue, 0.2f);
        Debug.DrawRay(raymidTop.transform.position, Vector2.up * 0.02f, Color.blue, 0.2f);

        RaycastHit2D rayC1 = Physics2D.Raycast(raytopRight.transform.position, Vector2.up, 0.06f, layerMaskGrounded);
        RaycastHit2D rayC2 = Physics2D.Raycast(raytopLeft.transform.position, Vector2.up, 0.06f, layerMaskGrounded);
        RaycastHit2D rayC3 = Physics2D.Raycast(raymidTop.transform.position, Vector2.up, 0.06f, layerMaskGrounded);

        if ((!rayC3) && !(rayC1.collider != null && rayC1.collider.GetComponent<PlatformEffector2D>() != null || rayC2.collider != null && rayC2.collider.GetComponent<PlatformEffector2D>() != null || rayC1.collider != null && rayC3.collider != null && rayC3.collider.GetComponent<PlatformEffector2D>() != null))
        {


            bool der = false;
            bool izq = false;
            if (rayC1) der = true;
            if (rayC2) izq = true;
            if (izq == true && der == false)
            {
                this.transform.position += new Vector3(2f * Time.deltaTime, 0);
            }
            if (izq == false && der == true)
            {
                this.transform.position += new Vector3(-2f * Time.deltaTime, 0);
            }
        }


    }
    void CheckFloor()
    {
        Debug.DrawRay(ray1.transform.position, Vector2.down * 0.08f, Color.red, 0.2f);
        Debug.DrawRay(ray2.transform.position, Vector2.down * 0.08f, Color.blue, 0.2f);
        Debug.DrawRay(ray3.transform.position, Vector2.down * 0.08f, Color.green, 0.2f);

        RaycastHit2D rayC1 = Physics2D.Raycast(ray1.transform.position, Vector2.down, 0.07f, layerMaskGrounded);
        RaycastHit2D rayC2 = Physics2D.Raycast(ray2.transform.position, Vector2.down, 0.07f, layerMaskGrounded);
        RaycastHit2D rayC3 = Physics2D.Raycast(ray3.transform.position, Vector2.down, 0.07f, layerMaskGrounded);
        if ((rayC1 || rayC2 || rayC3))
        {
            alltrueGroundcheck = true;

        }
        if ((rayC1 || rayC2 || rayC3) && auxnoGround <= 0)
        {
            jumpCount = 0;
            if (auxkunaiDur <= 0 && puedoKunai == false && !waitingReturnKunai && !kunaiSpawned)
            {
                puedoKunai = true;
            }
            this.GetComponent<Animator>().SetBool("Grounded", true);

            grounded = true;
            auxCoyote = 0;

        }
        else if (!rayC1 && !rayC2 & !rayC3)
        {
            alltrueGroundcheck = false;
            //COYOTEER
            if (grounded)
            {
                if (jumpPressed == false)
                {
                    if (auxCoyote == 0&&!touchingwall) auxCoyote = coyoteTime;
                }

            }
            auxCoyote -= Time.deltaTime;

            if (auxCoyote <= 0)
            {
                auxCoyote = 0;
                grounded = false;
                if (jumpCount == 0)
                {
                    jumpCount = 1;
                }
                this.GetComponent<Animator>().SetBool("Grounded", false);

            }

        }


    }
    void Acelerar()
    {
        if (nospeedJump)
        {
            currentSpeed = maxSpeed;
        }
        if (xInput > 0)
        {

            if (rb.velocity.x < 0)
            {
                recentTurn = true;
                currentSpeed = 0.3f * currentSpeed;

            }
            facingRight = true;


            if (Mathf.Abs(currentSpeed) < Mathf.Abs(maxSpeed))
            {
                if (grounded)
                {
                    currentSpeed += acceleration * Time.deltaTime;
                }
                else
                {
                    if (auxAscend > auxAscend * 0.5f)
                    {
                        currentSpeed += acceleration * 0.6f * Time.deltaTime;
                    }
                    else
                    {
                        if (touchingwall)
                        {
                            if (touchingLeft) currentSpeed += acceleration * 0.05f * Time.deltaTime;
                            if (touchingRight) currentSpeed = 0;
                        }
                        else
                        {
                            currentSpeed += acceleration * 1.2f * Time.deltaTime;
                        }

                    }
                }

                haveMaxSpeed = false;
            }
            else
            {
                haveMaxSpeed = true;
                currentSpeed = maxSpeed;
            }
            rb.velocity = new Vector2(currentSpeed * Time.deltaTime, rb.velocity.y);



        }
        else if (xInput < 0)
        {
            facingRight = false;
            if (rb.velocity.x > 0)
            {
                recentTurn = true;
                if (grounded)
                {
                    currentSpeed = 0.2f * currentSpeed;
                }
                else
                {
                    currentSpeed = 0;
                }

            }

            if (Mathf.Abs(currentSpeed) < Mathf.Abs(maxSpeed))
            {
                if (grounded)
                {
                    currentSpeed += acceleration * Time.deltaTime;
                }
                else
                {
                    if (auxAscend > auxAscend * 0.5f)
                    {
                        currentSpeed += acceleration * 0.3f * Time.deltaTime;
                    }
                    else
                    {
                        if (touchingwall)
                        {
                            if (touchingRight) currentSpeed += acceleration * 0.5f * Time.deltaTime;
                            if (touchingLeft) currentSpeed = 0;
                        }
                        else
                        {
                            currentSpeed += acceleration * 1.2f * Time.deltaTime;
                        }
                    }
                }
                haveMaxSpeed = false;
            }
            else
            {
                haveMaxSpeed = true;
                currentSpeed = maxSpeed;
            }
            rb.velocity = new Vector2(-currentSpeed * Time.deltaTime, rb.velocity.y);
        }
    }
    void Frenar()
    {
        if ((xInput == 0) && (currentSpeed > 0))
        {
            haveMaxSpeed = false;
            if (grounded)
            {
                currentSpeed -= 50 * acceleration * Time.deltaTime;
            }
            else
            {
                currentSpeed -= 1 * acceleration * Time.deltaTime;
            }

            if (currentSpeed < 0)
            {
                currentSpeed = 0;
            }
            if (lastXinput > 0)
            {
                rb.velocity = new Vector2(currentSpeed * Time.deltaTime, rb.velocity.y);
            }
            else if (lastXinput < 0)
            {
                rb.velocity = new Vector2(-currentSpeed * Time.deltaTime, rb.velocity.y);

            }
        }
    }
    void Salto()
    {
        if (jumpStart == true)
        {
            auxnoGround = noGroundDetect;
            jumpStart = false;

            //Start Jump
            if (jumpCount == 2)
            {
                GameObject part = Instantiate(jumpParticles, posJumpPart.transform.position, Quaternion.identity) as GameObject;
                if (!facingRight) part.transform.localScale = new Vector3(-1 * part.transform.localScale.x, part.transform.localScale.y);

                Destroy(part, 2f);

                actualJumpSpeed = dJumpSpeed;
                rb.velocity = new Vector2(rb.velocity.x + (rb.velocity.x * 3f * Time.deltaTime), actualJumpSpeed);
            }
            else if (jumpCount == 1)
            {
                GameObject part = Instantiate(jumpParticles, posJumpPart.transform.position, Quaternion.identity) as GameObject;
                if (!facingRight) part.transform.localScale = new Vector3(-1 * part.transform.localScale.x, part.transform.localScale.y);
                Destroy(part, 2f); actualJumpSpeed = jumpSpeed;
                if (touchingwall && !grounded)
                {
                    rb.velocity = new Vector2(rb.velocity.x,0);
                    touchingwall = false;
                    if (touchingRight)
                    {
                        rb.velocity = new Vector2(-maxSpeed * 0.5f * Time.deltaTime, actualJumpSpeed);

                    }
                    else if (touchingLeft)
                    {
                        rb.velocity = new Vector2(maxSpeed * 0.5f * Time.deltaTime, actualJumpSpeed);
                    }
                    currentSpeed = maxSpeed * 0.5f;
                }
                else if (!touchingwall)
                {
                   
                    rb.velocity = new Vector2(rb.velocity.x + (rb.velocity.x * 3f * Time.deltaTime), actualJumpSpeed);
                }

                if (Mathf.Abs(rb.velocity.x) < 0.3f)
                {
                    nospeedJump = true;
                }
            }
            grounded = false;

        }

        if (jumpPressed && !grounded)
        {
            if (jumpCount == 2)
            {
                if (dauxAscend > 0)
                {
                    dauxAscend -= Time.deltaTime;
                    actualJumpSpeed -= djumpDeccel * Time.deltaTime;
                    rb.velocity = new Vector2(rb.velocity.x, actualJumpSpeed);
                }
                else
                {
                    dauxAscend = 0;
                    if (dauxGrace > 0)
                    {
                        dauxGrace -= Time.deltaTime;
                        rb.velocity = new Vector2(rb.velocity.x, 0);
                    }
                    else
                    {
                        dauxGrace = 0;
                        rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + (rb.velocity.y * 0.5f * Time.deltaTime));
                    }


                }
            }
            else if (jumpCount == 1)
            {
                if (auxAscend > 0)
                {
                    auxAscend -= Time.deltaTime;
                    actualJumpSpeed -= jumpDeccel * Time.deltaTime;
                    rb.velocity = new Vector2(rb.velocity.x + (rb.velocity.x * 1.2f * Time.deltaTime), actualJumpSpeed);
                }
                else
                {
                    auxAscend = 0;
                    if (auxGrace > 0)
                    {
                        auxGrace -= Time.deltaTime;
                        rb.velocity = new Vector2(rb.velocity.x, 0);
                    }
                    else
                    {
                        auxGrace = 0;
                        rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + (rb.velocity.y * Time.deltaTime));
                    }


                }
            }




        }
        else if (!grounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + (rb.velocity.y * Time.deltaTime));
        }
    }
    public void PlaySound(string sound)
    {
        AudioManager.instance.Play(sound);

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Kill>() != null)
        {

            if (dead == false)
            {
                AudioManager.instance.Play("Death");
                this.GetComponent<Animator>().SetBool("Dead", true);
                this.GetComponent<Animator>().SetTrigger("JustDead");
                dead = true;
            }
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Kill>() != null)
        {
            if (dead == false)
            {
                AudioManager.instance.Play("Death");
                this.GetComponent<Animator>().SetBool("Dead", true);
                this.GetComponent<Animator>().SetTrigger("JustDead");
                dead = true;
            }

        }
    }
}
