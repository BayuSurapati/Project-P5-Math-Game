using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerController : MonoBehaviour
{
    public PlayerState curState;            // current player state
                                            // values
    public float moveSpeed;                 // force applied horizontally when moving
    public float flyingSpeed;               // force applied upwards when flying
    public bool grounded;                   // is the player currently standing on the ground?
    public float stunDuration;              // duration of a stun
    private float stunStartTime;            // time that the player was stunned
                                            // components
    public Rigidbody2D rig;                 // Rigidbody2D component
    public Animator anim;                   // Animator component
    public ParticleSystem jetpackParticle;  // ParticleSystem of jetpack


    // Start is called before the first frame update

    // moves the player horizontally
    void Move()
    {
        // get horizontal axis (A & D, Left Arrow & Right Arrow)
        float dir = Input.GetAxis("Horizontal");
        // flip player to face the direction they're moving
        if (dir > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (dir < 0)
            transform.localScale = new Vector3(-1, 1, 1);
        // set rigidbody horizontal velocity
        rig.velocity = new Vector2(dir * moveSpeed, rig.velocity.y);
    }

    // adds force upwards to player
    void Fly()
    {
        // add force upwards
        rig.AddForce(Vector2.up * flyingSpeed, ForceMode2D.Impulse);
        // play jetpack particle effect
        if (!jetpackParticle.isPlaying)
            jetpackParticle.Play();
    }
    
    //return true if the player is on the ground, false otherwise
    bool IsGrounded()
    {
        //menembak raycast dibawah pemain
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 0.85f), Vector2.down, 0.3f);

        //Apakah kita terkena hit?
        if(hit.collider != null)
        {
            //apakah ini lantainya?
            if (hit.collider.CompareTag("Floor"))
            {
                return true;
            }
        }
        return false;
    }

    // sets the player's state
    void SetState()
    {
        if(curState != PlayerState.Stunned)
        {
            //idle
            if (rig.velocity.magnitude == 0 && grounded)
                curState = PlayerState.Idle;
            //walking
            if (rig.velocity.x != 0 && grounded)
                curState = PlayerState.Walking;
            //flying
            if (rig.velocity.magnitude != 0 && !grounded)
                curState = PlayerState.Flying;
        }
        //memberi tahu animator bahwa kita mengganti state
        anim.SetInteger("State", (int)curState);
    }

    //Dipanggil saat playernya kena Stun
    public void Stun()
    {
        curState = PlayerState.Stunned;
        rig.velocity = Vector2.down * 3;
        stunStartTime = Time.time;
        jetpackParticle.Stop();
    }

    //check input dari user untuk mengontrol playernya
    void CheckInputs()
    {
        if(curState != PlayerState.Stunned)
        {
            //movement
            Move();

            //Flying
            if (Input.GetKey(KeyCode.W))
                Fly();
            else
                jetpackParticle.Stop();
        }
        //Update state yang sekarang
        SetState();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        grounded = IsGrounded();
        CheckInputs();

        //Apakah playernya terStun?
        if(curState == PlayerState.Stunned)
        {
            //durasi player terkena stun
            if(Time.time - stunStartTime >= stunDuration)
            {
                curState = PlayerState.Stunned;
            }
        }
    }

    //Dipanggil saat player memasuki object collider lain
    void OnTriggerEnter2D(Collider2D col)
    {
        //Jika player tidak ter stun, maka stun player dengan object yang merupakan obstacle
        if (curState != PlayerState.Stunned)
        {
            if (col.GetComponent<Obstacles>())
            {
                Stun();
            }
        }
    }
}
public enum PlayerState
{
    Idle,       // 0
    Walking,    // 1
    Flying,     // 2
    Stunned     // 3
}
