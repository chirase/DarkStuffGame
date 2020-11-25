using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{
    public bool stunned;
    public bool hurt;
    public Vector2 userControls;
    public float speed;

    public Animator an;
    public Rigidbody2D rb;
    public SpriteRenderer sr;

    public bool walk;
    public bool idle;
    public bool attack;
    public bool jump;
    public float jumpForce;
    public bool onGround;

    public bool flipped;

    public bool dashButton;
    public LayerMask groundLayer;
    public LayerMask collideLayer;

    public float dashForce;
    public bool dashing;

    public GameObject echoes;

    public bool attackButton;

    public GameObject weapon;

    public int maxHealth;
    public int health;
    public GameObject uiHearthContainer;
    public GameObject[] uiHearths;

    public const int MAX_POSSIBLE_HEALTH = 10;

    public Color hearthDefaultColor;
    public Color brokenHearthColor;

    public GameObject playerCam;
    public GameObject cameraPrefab;

    public Color myColor;
    public Vector3 myVelocity;
    public Vector2 myPosition;

    public bool canPlayFallSound;

    public float jumpCd;
    public float jumpCdTime;

    void OnPlayerDisconnected() {
        Destroy(playerCam);
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {

        //playerCam = Instantiate(cameraPrefab, transform.position + new Vector3(0, 0, -10), transform.rotation);
        playerCam.GetComponent<CameraFollow>().target = this.gameObject;

        uiHearthContainer = GameObject.FindGameObjectsWithTag("heartContainer")[0];
        uiHearths = new GameObject[uiHearthContainer.transform.childCount];

        int i = 0;
        foreach (Transform child in uiHearthContainer.transform) {
            if (child.tag == "uiHearth") {
                uiHearths[i] = child.transform.gameObject;
            }
            i++;
        }

        UpdateHpVisuals();
        Physics2D.SetLayerCollisionMask(gameObject.layer, collideLayer);

    }

    // Update is called once per frame
    void Update()
    {
        UpdateHpVisuals();
        if (health <= 0) {
            KillPlayer();
        }

        // CONTROLS 
            userControls = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            dashButton = Input.GetKeyDown(KeyCode.LeftShift);
            attackButton = Input.GetMouseButtonDown(0);

            // DETECT ONGROUND
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.65f, groundLayer);
            onGround = hit.collider != null;
        if (!onGround)
        {
            canPlayFallSound = true;
        }
        else {
            if (canPlayFallSound) {
                canPlayFallSound = false;
                GetComponent<PlaySounds>().playSound(1);
            }
        }

            DeplacerPlayer(userControls);
            SetAnimation();

            if (dashing) {
                SpawnEchoes();
            }

            if (attackButton && weapon != null) {
                Attack();
            }

    }


    void DeplacerPlayer(Vector2 controls) {
        if (!stunned) {
            if (dashButton)
            {
                dashing = true;
                Dash();
                Invoke("StopDashing", 0.2f);
            }

            if (onGround && userControls.y > 0 && jumpCd <= 0)
            {
                InvokeRepeating("RemoveJumpCdTime",0, 0.1f);
                jumpCd += jumpCdTime;
                Jump();              
            }
            else if (userControls.x != 0) {
                Move();
            }
        }
    }

    void Move() {
        if (!dashing) {
            float speedFactor = (onGround) ? 1 : 0.7f;
            speedFactor = (hurt) ? speedFactor/1.5f : speedFactor;
            rb.velocity = new Vector2((speed * userControls.x * speedFactor), rb.velocity.y);
        }

    }

    void Jump() {
        GetComponent<PlaySounds>().playSound(1);
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    void Dash() {
        GetComponent<PlaySounds>().playSound(0);
        rb.velocity = new Vector2(rb.velocity.x + dashForce * ((flipped) ? -1 : 1), rb.velocity.y);
    }

    void SetAnimation() {
        if (!stunned) {
            if (!onGround) {
                jump = true;
                idle = false;
                walk = false;
            } else if (onGround && userControls.x == 0) {
                jump = false;
                idle = true;
                walk = false;
            } else if (onGround && userControls.x != 0) {
                jump = false;
                idle = false;
                walk = true;
            }

            if (userControls.x != 0) {
                flipped = userControls.x < 0;
            }

        }

        transform.localScale = new Vector2((flipped) ? -1 : 1, transform.localScale.y);
        an.SetBool("walk", walk);
        an.SetBool("jump", jump);
        an.SetBool("idle", idle);

    }

    void Attack() {

        if (weapon.GetComponent<Weapon>().attType == Weapon.AttackType.Thrust) {
            if (an.GetBool("attack") == false) {
                an.SetBool("attack", true);
                rb.velocity = new Vector2(rb.velocity.x + ((flipped) ? -5 : 5), rb.velocity.y);
                Invoke("StopAttack", 0.53f);
            }
        } else if (weapon.GetComponent<Weapon>().attType == Weapon.AttackType.Swipe) {
            if (an.GetBool("staticAttack") == false)
            {
                weapon.GetComponent<Animator>().SetTrigger("Swipe");
                an.SetBool("staticAttack", true);
            }
        }
    }


    public void StopStaticAttack() {
        an.SetBool("staticAttack", false);
    }

    void StopAttack() {
        an.SetBool("attack", false);
        an.SetBool("staticAttack", false);
        HideSword();
    }


    public void StopDashing() {
        dashing = false;
        rb.velocity = new Vector2(0,  rb.velocity.y);
    }

    public void SpawnEchoes() {
        GameObject echo = Instantiate(echoes, transform.position, transform.rotation);
        echo.GetComponent<SpriteRenderer>().flipX = flipped;
    }


    public void ShowSword() {
        weapon.GetComponent<Weapon>().ShowSelf();
    }

    public void HideSword() {
        weapon.GetComponent<Weapon>().HideSelf();
    }

    public void UpdateHpVisuals()
    {

        for (int i = 0; i < MAX_POSSIBLE_HEALTH; i++)
        {
            if (i + 1 <= maxHealth)
            {
                uiHearths[i].GetComponent<Image>().enabled = true;
            }
            else
            {
                uiHearths[i].GetComponent<Image>().enabled = false;
            }
        }

        for (int i = 0; i < maxHealth; i++)
        {
            if (i + 1 <= health)
            {
                uiHearths[i].GetComponent<Image>().color = hearthDefaultColor;
            }
            else
            {
                uiHearths[i].GetComponent<Image>().color = brokenHearthColor;
            }
        }

    }

    public void ReceiveDamage(int damage, Vector2 force) {
        print("awtch caliss");
        if (!hurt) {
            hurt = true;
            health -= damage;
            rb.velocity = new Vector2(force.x, force.y);
            Invoke("UnHurt", 0.5f);
        }
    }

    public void UnHurt() {
        hurt = false;
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.tag == "movingPlat") {
           
        }
    }

    void KillPlayer()
    {
        var thisScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(thisScene);
    }

    void RemoveJumpCdTime() {
        jumpCd -= 0.1f;

        jumpCd = Mathf.Clamp(jumpCd, 0, jumpCdTime);
        if (jumpCd == 0) {
            CancelInvoke("RemoveJumpCdTime");
        }
    }

}
