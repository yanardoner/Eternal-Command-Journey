using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float movementSpeed = 5f;
    float currentSpeed;
    private int health;
    [SerializeField] Rigidbody rb;
    Vector3 direction;
    [SerializeField] float shiftSpeed = 10f;
    [SerializeField] float XSpeed = 2f;
    float stamina = 7f;
    public bool dead;
    [SerializeField] Animator anim;
    [SerializeField] float jumpForce = 7f;
    bool isGrounded = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        currentSpeed = movementSpeed;
        health = 100;
    }

    public void ChangeHealth(int count)
    {
        // canı eksiltme kısmı
        health -= count;
        // eğer can sıfır veya altına düşerse
        if (health <= 0)
        {
            health = 0;
            dead = true;
            transform.Find("Main Camera").GetComponent<ThirdPersonCamera>().isSpectator = true;
            //PlayerController kodunu deaktive edelim ki karakter artık hareket edemesin
            this.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        direction = new Vector3(moveHorizontal, 0, moveVertical);
        direction = transform.TransformDirection(direction);
        
        if (moveVertical > 0 && isGrounded)
        {
            currentSpeed = movementSpeed;
            if(Input.GetKey(KeyCode.LeftShift))
            {
                if(stamina > 0)
                {
                    stamina -= Time.deltaTime;
                    currentSpeed = shiftSpeed;
                    anim.SetBool("Run", true);
                    anim.SetBool("Walk Forward", false);
                }

                else
                {
                    anim.SetBool("Run", false);
                    currentSpeed = movementSpeed;
                }
            }

            else if (!Input.GetKey(KeyCode.LeftShift))
            {
                anim.SetBool("Walk Forward", true);
                anim.SetBool("Walk Backward", false);
                anim.SetBool("Run", false);
            }
        }

        else if (moveVertical < 0 && isGrounded)
        {
            currentSpeed = movementSpeed;
            anim.SetBool("Walk Forward", false);
            anim.SetBool("Walk Backward", true);
        }

        else if (moveHorizontal > 0 && isGrounded)
        {
            currentSpeed = XSpeed;
            anim.SetBool("Walk Right", true);
            anim.SetBool("Walk Left", false);
        }
        else if (moveHorizontal < 0 && isGrounded)
        {
            currentSpeed = XSpeed;
            anim.SetBool("Walk Right", false);
            anim.SetBool("Walk Left", true);
        }

        else
        {
            anim.SetBool("Walk Forward", false);
            anim.SetBool("Walk Backward", false);
            anim.SetBool("Walk Right", false);
            anim.SetBool("Walk Left", false);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
            isGrounded = false;
            currentSpeed = movementSpeed;
            anim.SetBool("Jump", true);
            anim.SetBool("Walk Right", false);
            anim.SetBool("Walk Left", false);
            anim.SetBool("Walk Forward", false);
            anim.SetBool("Walk Backward", false);
            anim.SetBool("Run", false);
        }

        if (!Input.GetKey(KeyCode.LeftShift))
        {            
            stamina += Time.deltaTime;
        }

        if(stamina > 7f)
        {
            stamina = 7f;
        }

        else if (stamina < 0)
        {
            stamina = 0;
        }

        print(stamina);
        print(currentSpeed);
    }

    void FixedUpdate()
    {
        rb.MovePosition(transform.position + direction * currentSpeed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        isGrounded = true;
        anim.SetBool("Jump", false);
    }
}
