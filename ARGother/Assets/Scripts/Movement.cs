using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    public static Movement instance;

    Vector2 startLocation;

    public AudioClip stabAu;
    public AudioClip stepAu;
    public AudioClip coinAu;
    public AudioClip spellAu;
    AudioSource AS;


    public GameObject Overlay;
    UI display;

    public float jumpHeight;
    public float moveSpeed;
    public float fireRate;
    float nextFire;

    float spellPlace;
    int spellRot;


    public GameObject bolt;
    public GameObject dagger;

    Animator animator;

    float moveDirection = 0;
    float facing;

    Rigidbody2D rb;

    bool moving;
    bool jumping;
    bool melee;
    bool spell;
    bool falling;
    bool hurt;
    bool ducking;
    bool dropping;

    bool idle;
    bool sleep;
    float idleTime;

    bool grounded;

    Vector3 groundingCheck;
    bool bump;


    Vector2 hurtMove;
    float hurtTime;
    readonly float hurtDuration = .25f;

    RaycastHit2D plat;


    bool VBattack;
    bool VBspell;
    bool VBjump;
    bool VBleft;
    bool VBright;
    bool VBdown;


    // Start is called before the first frame update
    void Start()
    {
        instance = this;


        DontDestroyOnLoad(gameObject);

        AS = gameObject.GetComponent<AudioSource>();
        display = Overlay.GetComponent<UI>();

        startLocation = transform.position;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        dagger.transform.parent = gameObject.transform;

        animator.SetFloat("Movement", 1f);
        animator.SetBool("Moving", false);
        animator.SetBool("Jump", false);
        animator.SetBool("Spell", false);
        animator.SetBool("Melee", false);

        groundingCheck = new Vector3(0, 0, 0);
    }

    // Update is called once per framew
    void Update()
    {
        plat = new RaycastHit2D();


        ////////////////////////////////////////////////////////////////////////
        //TRIPLE GROUNDING RAYCAST CHECK////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////
        //Grounding Left
        if (Physics2D.Raycast(new Vector2(transform.position.x - 2, transform.position.y), new Vector2(0, -1), 7.5f, LayerMask.GetMask("Ground")))
        {
            grounded = true;
            bump = false;
            groundingCheck.x = 1;
            Debug.DrawRay(new Vector2(transform.position.x - 2, transform.position.y), new Vector2(0, -10), Color.green);
        }
        else
        {
            groundingCheck.x = 0;
            Debug.DrawRay(new Vector2(transform.position.x - 2, transform.position.y), new Vector2(0, -10), Color.red);
        }
        //Grounding Right
        if (Physics2D.Raycast(new Vector2(transform.position.x + 2, transform.position.y), new Vector2(0, -1), 7.5f, LayerMask.GetMask("Ground")))
        {
            grounded = true;
            bump = false;
            groundingCheck.y = 1;
            Debug.DrawRay(new Vector2(transform.position.x + 2, transform.position.y), new Vector2(0, -10), Color.green);
        }
        else
        {
            groundingCheck.y = 0;
            Debug.DrawRay(new Vector2(transform.position.x + 2, transform.position.y), new Vector2(0, -10), Color.red);
        }

        //Grounding Middle
        if ((plat = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), new Vector2(0, -1), 7.5f, LayerMask.GetMask("Platform") + LayerMask.GetMask("Breakable") + LayerMask.GetMask("Pushable"))))
        {
            grounded = true;
            bump = false;
            groundingCheck.z = 1;
            Debug.DrawRay(new Vector2(transform.position.x, transform.position.y), new Vector2(0, -10), Color.green);
        }
        else
        {
            groundingCheck.z = 0;
            Debug.DrawRay(new Vector2(transform.position.x, transform.position.y), new Vector2(0, -10), Color.red);
        }

        if (groundingCheck == new Vector3(0, 0, 0))
        {
            if (!bump)
            {
                jumping = true;
            }
            falling = true;
            grounded = false;
        }

        if (grounded == true)
        {
            falling = false;
        }
        //END RAYCAST/////////////////////////////////////////////////////////////////////////////////


        /////////////////////////////////////////////
        ///////////Jump - W//////////////////////////
        /////////////////////////////////////////////
        if (Input.GetKeyDown(KeyCode.W) && grounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);

            jumping = true;
            grounded = false;

        }
        if (Input.GetKey(KeyCode.W) && jumping)
        {
            falling = false;
            if (rb.velocity.y < 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
            }
        }
        if (Input.GetKeyUp(KeyCode.W) && !grounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, 10);

            falling = true;
        }
        //END JUMP////////////////////////////////////



        //////////////////////////////////////////////
        ///////////SideMovement///////////////////////
        /////////////////////////////////////////////
        //Facing Right - D
        if (Input.GetKey(KeyCode.D) && !melee && !ducking)
        {
            moveDirection = moveSpeed;

            facing = moveDirection;
            moving = true;

            spellPlace = 8;
            spellRot = 180;
        }
        //Facing Left - A
        if (Input.GetKey(KeyCode.A) && !melee && !ducking)
        {
            moveDirection = -moveSpeed;

            facing = moveDirection;
            moving = true;

            spellPlace = -8;
            spellRot = 0;
        }
        //NotMoving
        if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && !VBleft && !VBright)
        {
            moveDirection = 0;
            moving = false;
        }
        //Stepping Audi
        if (Input.GetKeyDown(KeyCode.A) && Input.GetKeyDown(KeyCode.D) && !melee && !ducking)
        {
            AS.Play();
        }
        if (Input.GetKeyUp(KeyCode.A) && Input.GetKeyUp(KeyCode.D))
        {
            AS.Stop();
        }
        //END SIDE MOVEMENT//




        //DAMAGE RECOIL
        if (hurt)
        {
            rb.velocity = new Vector2(hurtMove.x * (moveSpeed / 10), rb.velocity.y);
            if (hurtDuration < Time.time - hurtTime)
            {
                hurt = false;
            }
        }
        //ACTUAL MOVEMENT
        else
        {
            rb.velocity = new Vector2(moveDirection, rb.velocity.y);
        }




        //MELEE//
        if (Input.GetKey(KeyCode.E))
        {
            melee = true;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            AS.PlayOneShot(stabAu);
            dagger.SetActive(true);
            rb.AddForce(new Vector2(facing * 400, rb.velocity.y));
            dagger.transform.position = new Vector3(transform.position.x + (spellPlace / 2), transform.position.y - 2.4f);
            moveDirection = 0;
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            dagger.SetActive(false);
        }
        if (!Input.GetKey(KeyCode.E) && !VBattack)
        {
            melee = false;
        }
        //END MELEE



        //SPELL//
        if (Input.GetKey(KeyCode.F) && Time.time > nextFire && !ducking)
        {
            AS.PlayOneShot(spellAu);
            nextFire = Time.time + fireRate;
            spell = true;
            var obj = Instantiate(bolt, new Vector3(transform.position.x + spellPlace, transform.position.y - 2.4f, transform.position.z), Quaternion.Euler(0, spellRot, 0));
            Destroy(obj, 2);
        }
        else if (!Input.GetKey(KeyCode.F) && !VBspell)
        {
            spell = false;
        }
        //END SPELL//



        //PLATFORM DROP//
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (plat)
            {
                Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), plat.collider, true);
                dropping = true;
            }
            else
            {
                ducking = true;
            }
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            ducking = false;
        }



        //WHEN IDLE///
        if (!Input.anyKey)
        {
            idleTime += .1f;
            if (idleTime > 500)
            {
                idle = true;
            }
            if (idleTime > 800)
            {
                sleep = true;
            }
        }
        if (Input.anyKey)
        {
            idleTime = 0;
            idle = false;
            sleep = false;
        }





        if(spell == true)
        {
            Debug.Log("casting");
        }
        //ANIMATOR UPDATE//
        animator.SetBool("Moving", moving);
        animator.SetBool("Jump", jumping);
        animator.SetBool("Spell", spell);
        animator.SetBool("Melee", melee);
        animator.SetFloat("Movement", facing);
        animator.SetBool("Dropping", dropping);
        animator.SetBool("Falling", falling);
        animator.SetBool("Idle", idle);
        animator.SetBool("Sleeping", sleep);
        animator.SetBool("Hurt", hurt);
        animator.SetBool("Ducking", ducking);



        //VIRTUAL BUTTON CONTROLS
        if (VBjump)
        {
            if (jumping)
            {
                falling = false;
                if (rb.velocity.y < 0)
                {
                    rb.velocity = new Vector2(rb.velocity.x, 0);
                }
            }
        }
        if(VBright)
        {
            Debug.Log("Moving Right");
            if (!melee && !ducking)
            {
                AS.Play();
                moveDirection = moveSpeed;

                facing = moveDirection;
                moving = true;

                spellPlace = 8;
                spellRot = 180;
            }
        }
        if(VBleft)
        {
            Debug.Log("Moving Left");
            if (!melee && !ducking)
            {
                AS.Play();
                moveDirection = -moveSpeed;

                facing = moveDirection;
                moving = true;

                spellPlace = -8;
                spellRot = 0;
            }
        }
        if(!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && !VBleft && !VBright)
        {
            moveDirection = 0;
            moving = false;
            AS.Stop();
        }
        if(VBattack)
        {
            melee = true;
        }
        if(!Input.GetKey(KeyCode.E) && !VBattack)
        {
            melee = false;
        }
        if (VBspell)
        {
            if (Time.time > nextFire && !ducking)
            {
                AS.PlayOneShot(spellAu);
                nextFire = Time.time + fireRate;
                spell = true;
                var obj = Instantiate(bolt, new Vector3(transform.position.x + spellPlace, transform.position.y - 2.4f, transform.position.z), Quaternion.Euler(0, spellRot, 0));
                Destroy(obj, 2);
            }
        }
        if(!VBspell && !Input.GetKey(KeyCode.F))
        {
            spell = false;
        }

    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        jumping = false;
        falling = false;
        bump = true;

        //Snake 
        if (collision.gameObject.layer == 8 && !melee)
        {
            display.health -= 1;
            hurt = true;

            hurtTime = Time.time;
            hurtMove = new Vector2(transform.position.x - collision.transform.position.x, transform.position.y - collision.transform.position.y);

            //dead
            if (display.health == 0)
            {
                gameObject.transform.position = startLocation;
                display.health = 6;
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 11)
        {
            Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), collision.gameObject.GetComponent<BoxCollider2D>(), true);
        }

        //COIN
        if (collision.gameObject.layer == 13)
        {
            AS.PlayOneShot(coinAu);
            Destroy(collision.gameObject);
            display.money += 1;

        }

        //Potion
        if (collision.gameObject.layer == 17)
        {
            if (display.health <= 3)
            {
                display.health += 3;
                Destroy(collision.gameObject);
            }
            else if (display.health > 3 && display.health < 6)
            {
                display.health = 6;
                Destroy(collision.gameObject);
            }
        }

        //Exit
        if (collision.gameObject.layer == 16)
        {
            if (collision.gameObject.name == "Exit One")
            {
                SceneManager.LoadScene("Level 2");
                gameObject.transform.position = new Vector2(-110, -71);
                startLocation = transform.position;
            }
            if (collision.gameObject.name == "Exit Two")
            {
                SceneManager.LoadScene("Level 3");
                gameObject.transform.position = new Vector2(-316, 28);
                startLocation = transform.position;
                Background.instance.Mountain();
            }
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        //platforms
        if (collision.gameObject.layer == 11)
        {
            dropping = false;
            Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), collision.gameObject.GetComponent<BoxCollider2D>(), false);
        }

        //void drop
        if (collision.gameObject.layer == 12)
        {
            gameObject.transform.position = startLocation;
            display.health = 6;
        }
    }


    public void Destruct()
    {
        Destroy(gameObject);
    }


    public void Duck()
    {
        VBdown = true; 
        if (plat)
        {
            Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), plat.collider, true);
            dropping = true;
        }
        else
        {
            ducking = true;
        }
    }
    public void DuckStop()
    {
        VBdown = false;
        ducking = false;
    }
    public void VBLdown()
    {
        VBleft = true;
    }
    public void VBLup()
    {
        VBleft = false;
    }
    public void VBRdown()
    {
        VBright = true;
    }
    public void VBRup()
    {
        VBright = false; 
    }
    public void Jump()
    {
        VBjump = true;
        if(grounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);

            jumping = true;
            grounded = false;
        }
    }
    public void JumpDone()
    {
        VBjump = false;
        if (!grounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, 10);

            falling = true;
        }
    }
    public void Attack()
    {
        VBattack = true; 
        AS.PlayOneShot(stabAu);
        dagger.SetActive(true);
        rb.AddForce(new Vector2(facing * 400, rb.velocity.y));
        dagger.transform.position = new Vector3(transform.position.x + (spellPlace / 2), transform.position.y - 2.4f);
        moveDirection = 0;
    }
    public void AttackDone()
    {
        VBattack = false;
        dagger.SetActive(false);
    }
    public void Shoot()
    {
        VBspell = true;
    }
    public void ShootDone()
    {
        VBspell = false;
    }
}
