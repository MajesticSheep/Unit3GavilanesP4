using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //The code below is for the player to be able to jump
    private Rigidbody steveRb;
    public float jumpForce;
    public float gravityModifier;
    public bool isOnGround = true;
    //The code below is for the animation of the player
    public bool gameOver = false;
    private Animator playerAnim;
    //The code below is for the particle effects for the player
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;
    //The code below is for the sound effects for the player
    public AudioClip jumpSound;
    public AudioClip crashSound;
    //The code below is for the player to access the sound effects when moving
    private AudioSource playerAudio;
    //The code below is for the double jump ability for the player
    public bool doubleJumpUsed = false;
    public float doubleJumpForce;
    //The code below is for the dash effect for the player
    public bool doubleSpeed = false;
    // Start is called before the first frame update
    void Start()
    {
        steveRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
        playerAnim = GetComponent<Animator>();

        playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
       if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
           steveRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
           isOnGround = false;
            playerAnim.SetTrigger("Jump_trig");
            dirtParticle.Stop();

            playerAudio.PlayOneShot(jumpSound, 1.0f);

            doubleJumpUsed = false;
        }
       
        else if (Input.GetKeyDown(KeyCode.Space) && !isOnGround && !doubleJumpUsed)
        {
            doubleJumpUsed = true;
            steveRb.AddForce(Vector3.up * doubleJumpForce, ForceMode.Impulse);
            playerAnim.Play("Running_Jump", 3, 0f);
            playerAudio.PlayOneShot(jumpSound, 1.0f);
        }

       if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            doubleSpeed = true;
            playerAnim.SetFloat("Speed_Multiplier", 2.0f);
        }
       else if (doubleSpeed)
        {
            doubleSpeed = false;
            playerAnim.SetFloat("Speed_Multiplier", 1.0f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            dirtParticle.Play();
        }

        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameOver = true;
            Debug.Log("Game Over!");
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            explosionParticle.Play();
            dirtParticle.Stop();

            playerAudio.PlayOneShot(crashSound, 1.0f);
        }
    }
}
