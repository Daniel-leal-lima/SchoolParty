using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class MatematicaPlayer : MonoBehaviourPunCallbacks
{
    public FixedJoystick joystickController;
    [SerializeField] private int velocidade;
    Vector2 movimento;
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private List<GameObject> respostas;

    [SerializeField] private string resposta;



        // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        Movimento();
    }

    void Movimento()
    {
        movimento.x = joystickController.Horizontal;
        movimento.y = joystickController.Vertical;

        rb.MovePosition(rb.position + movimento * velocidade * Time.fixedDeltaTime);

        if (movimento.x != 0)
        {
            // Rotaciona o personagem na Horizontal
            if (movimento.x > 0) { transform.localScale = new Vector3(1, 1, 1); }
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

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Piso"){
            respostas.Add(other.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "Piso"){
            respostas.Remove(other.gameObject);
        }
    }
    public List<GameObject> GetRespostas(){
        return respostas;
    }

}
