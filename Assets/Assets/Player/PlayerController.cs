using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using System;

public class PlayerController : MonoBehaviourPunCallbacks
{
    public FixedJoystick joystickController;
    [SerializeField] private int velocidade;
    Vector2 movimento;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private bool click;
    [SerializeField] private string proximoJogo;
    [SerializeField] private Text jogoText;


    [SerializeField] private Player photonPlayer;
    [SerializeField] private int id;

    [PunRPC]
    public void Inicializa(Player player){
        photonPlayer = player;
        id = player.ActorNumber;
        GerenciadorJogo.Instancia.Jogadores.Add(this);

        if(!photonView.IsMine){
            rb.isKinematic = false; // desligando o jogador inimigo
        }
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        MudaDeCena();
        
    }

    void MudaDeCena()
    {
        /*if (click = true && Input.GetKeyDown("space")){
            SceneManager.LoadScene(proximoJogo);
        }*/

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (click)
            {
                SceneManager.LoadScene(proximoJogo);
            }
        }

    }

    void FixedUpdate()
    {
        Movimento();
    }
    
   void Movimento()
    {

        movimento.x = joystickController.Horizontal;
        movimento.y = joystickController.Vertical;

        rb.MovePosition(rb.position + movimento * velocidade * Time.fixedDeltaTime);

        if (movimento.x != 0 )
        {
            // Rotaciona o personagem na Horizontal
            if (movimento.x > 0) { transform.localScale = new Vector3(1, 1, 1);}
            else { transform.localScale = new Vector3(-1, 1, 1); }
            
            GetComponent<Animator>().SetBool("Andar", true);
        }
        else if (movimento.y != 0)
        {
            GetComponent<Animator>().SetBool("Andar", true);
        }
        else
        {
            GetComponent<Animator>().SetBool("Andar", false);
        }
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Inicializador"){
            proximoJogo = other.gameObject.GetComponent<CarregadorJogo>().nomeJogo;
            other.gameObject.GetComponent<SpriteRenderer>().color = other.gameObject.GetComponent<CarregadorJogo>().corColisao;
            click = true;
            jogoText.gameObject.SetActive(true);
            jogoText.text = other.gameObject.GetComponent<CarregadorJogo>().frase;
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if(other.gameObject.tag == "Inicializador"){
            other.gameObject.GetComponent<SpriteRenderer>().color = other.gameObject.GetComponent<CarregadorJogo>().corOriginal;
            jogoText.gameObject.SetActive(false);
            proximoJogo = null;
            click = false;
        }
    }
}
