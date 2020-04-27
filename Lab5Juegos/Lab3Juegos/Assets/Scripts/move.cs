using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class move : MonoBehaviour
{
    public float force = 0;
    public float JumpForce = 0;
    int monedas = 0;
    Vector3 posicionInicial;
    Rigidbody rb;
    public Text ScoreText;
    public GameObject prefab;
    private Text Scoretext;


    //lab 5
    private AudioSource jumpSFX;
    private AudioSource coin;
    private AudioSource death;
    //nuevo proyecto
    public Text mitexto;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        posicionInicial = transform.position;
        //scores = ScoreText.GetComponent<Text>();
        //Lab5
        jumpSFX = GetComponents<AudioSource>()[0];
        coin = GetComponents<AudioSource>()[1];
        death = GetComponents<AudioSource>()[2];

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
            Jump();

        if (mitexto)
            mitexto.text = "Score: " + monedas.ToString();
    }
    private void FixedUpdate()
    {
        if (rb)
            rb.AddForce(Input.GetAxis("Horizontal") * force, 0, Input.GetAxis("Vertical") * force);
    }
    private void Jump()
    {
        if (rb)
            if (Mathf.Abs(rb.velocity.y) < 0.05f)
                if (jumpSFX) jumpSFX.Play();
                rb.AddForce(0,JumpForce,0, ForceMode.Impulse);
    }

    void OnTriggerEnter(Collider otro)
    {
        if (otro.CompareTag("salida"))
        {
            Debug.Log("Has salido, felicidades. Has recogido " + monedas + " monedas");
        }
        else if (otro.CompareTag("enemigo") && monedas !=3) 
        {
            if (death) death.Play();
            rb.MovePosition(posicionInicial);
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            Debug.Log("Recoge las monedas restantes para ganar");
        }
        else if (otro.CompareTag("moneda"))
        {
            if (coin) coin.Play();
            otro.gameObject.SetActive(false);
           // if (coin) coin.Play();
            monedas += 1;
        }

    }
}
