using UnityEngine;


public class ControlJugadorInputSystemAll : MonoBehaviour
{
    public float velocidad;
    private Rigidbody2D fisicas;
    private float entradaX;
    private float entradaY;

    private float entradaSalto;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fisicas = GetComponent<Rigidbody2D>(); 
        //Obtiene los valores del RigidBody2D del objeto player al iniciar ejecución
    }

    // Update is called once per frame
    void Update()
    {
        // vector2 position = transform.position;

        // position.x += 0.1f;

        // transform.position = position; 


    }

    // FixedUpdate es llamado una vez cada 0.5 segundos (aprox)
    private void FixedUpdate()
    {
        // devuelve 1 si el jugador presa la D o el click derecho
        // devuelve -1 si el jugador presa la A o el click izquierdo
        entradaX = Input.GetAxis("Horizontal");
        if(fisicas != null)
        {
            fisicas.linearVelocity = new Vector2(entradaX * velocidad, fisicas.linearVelocityY);
        }

        entradaY = Input.GetAxis("Vertical");
        fisicas.linearVelocity = new Vector2(fisicas.linearVelocityX,entradaY * velocidad);

        // entradaSalto = Input.GetAxis("Jump");
        // fisicas.linearVelocity = new Vector2(entradaSalto * velocidad, entradaSalto * velocidad);
    }
}