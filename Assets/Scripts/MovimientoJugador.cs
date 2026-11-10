using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]

public class MovimientoJugador : MonoBehaviour
{
    [Header("Movimiento")]

    public float velocidad;
    
    [SerializeField] private InputActionReference actionMover;

    [Header("Salto")]

    public float impulso = 5f;
    [SerializeField] private InputActionReference actionSaltar;

    [SerializeField] private LayerMask capaSuelo;

    private Rigidbody2D fisica;

    private SpriteRenderer sprite;

    private Animator animador;

    [Header("Detectores")]

    public float longitudRaycast = 1.5f;

    private Vector2 entradaX;

    private Vector2 entradaY;

    private bool pendienteSaltar;

    private bool tocandoSuelo;

    [Header("Spawnpoint")]

    [SerializeField] private Transform spawnpoint;

    [Header("Puntos")]

    [SerializeField] private int puntos = 0;

    [Header("Salud")]

    [SerializeField] private int segundosInvulnerable = 2;

    [SerializeField] private int VidaTotal = 0;

    private int vidaActual => VidaTotal;

    private Color colorOriginal;    
    private bool vulnerable = true;

    [Header("Tiempo")]

    [SerializeField, Min(1)] private int tiempoLimite = 100;

    [SerializeField] private int tiempoEmpleado ;

    private float tiempoInicio;

    private float tiempoReal;

    // Se ejecuta antes que el Start y se ejecuta INCLUSO si el script esta desactivado. 
    private void Awake() {
        
        // Detiene el rigidBody para modificar su velocidad linear
        fisica = GetComponent<Rigidbody2D>();

        sprite = GetComponent<SpriteRenderer>();

        animador = GetComponent<Animator>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        if (sprite != null)
        {
            colorOriginal = sprite.color;
        } else
        {
           Debug.LogWarning("El SpriteRenderer no está asignado en el objeto " + gameObject.name);
        }

        tiempoInicio = Time.time;
    }

    // Update is called once per frame
    void Update() {

        entradaX = actionMover.action.ReadValue<Vector2>();

        saltarJugador();

        animarJugador();

        ComprobarFinJuegoTiempo();
    }

    private void ComprobarFinJuegoTiempo()
    {
        tiempoReal = Time.time - tiempoInicio;
        tiempoEmpleado = Mathf.FloorToInt(tiempoReal);
        if (tiempoEmpleado >= tiempoLimite)
        {
            FinJuego();
        }
    }

    private void saltarJugador()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, longitudRaycast, capaSuelo);

        tocandoSuelo = hit.collider != null;

        Color colorRayo = tocandoSuelo ? Color.green : Color.red;

        Debug.DrawRay(transform.position, Vector2.down * longitudRaycast, colorRayo);

        //Devuelve True UNA VEZ si el espacio a sido presionado, luego de eso se vuelve FALSE (Para registrar mantener presionado usar IsPressed() ) 
        if (actionSaltar.action.WasPressedThisFrame() && tocandoSuelo)
        {
            pendienteSaltar = true;
        }
    }

    private void FixedUpdate()
    {
        moverJugador();
        OrientarSprite();
    }

    private void moverJugador()
    {
        fisica.linearVelocity = new Vector2(entradaX.x * velocidad, fisica.linearVelocityY);

        if (pendienteSaltar)
        {
            fisica.AddForce(Vector2.up * impulso, ForceMode2D.Impulse);
            pendienteSaltar = false;
        }
    }

    public void ReaparecerJugador()
    {
        if(spawnpoint == null)
        {
            Debug.LogError("El spawnpoint no puede ser nulo",this);
            enabled = true;
            return;
        }
        fisica.position = (Vector2) spawnpoint.position;
        fisica.linearVelocity = Vector2.zero;
    }

    private void animarJugador()
    {
        if(animador == null)
        {
            return;
        }

        if (!tocandoSuelo){
            animador.Play("JugadorSaltando");
        } else if(fisica.linearVelocity.x != 0)
        {
            animador.Play("JugadorCorriendo");
        } else
        {
            animador.Play("JugadorParado");
        }
    }

    private void OnEnable()
    {
        if (actionMover == null || actionMover.action == null)
        {
            Debug.LogError("Asigna Mapa de acción Jugador/Mover en accionMover");
            enabled = false;
            return;
        }
        actionMover.action.Enable();

        if (actionSaltar == null || actionSaltar.action == null)
        {
            Debug.LogError("Asigna Mapa de acción Jugador/Saltar en accionSaltar");
            enabled = false;
            return;
        }
        actionSaltar.action.Enable();
    }

    private void OnDisable() {
        if (actionMover != null || actionMover.action != null)
        {
            actionMover.action.Disable();
        }

        if (actionSaltar != null || actionSaltar.action != null)
        {
            actionSaltar.action.Disable();
        }
    }

    private void OrientarSprite()
    {
        if (sprite == null)
        {
            return;
        }

        if (fisica.linearVelocity.x > 0)
        {
            sprite.flipX = false;
        }
        else if (fisica.linearVelocity.x < 0)
        {
            sprite.flipX = true;
        }
    }

    public void incrementarPuntos(int cantidad)
    {

        if (cantidad <= 0) return;

        puntos += cantidad;
    }

    public void quitarVida()
    {
        if (!vulnerable) return;
        vulnerable = false;
        VidaTotal--;
        if (sprite != null)
        {
            sprite.color = Color.blue;
        }
        if (vidaActual <= 0)
        {
            FinJuego();
        }
        Invoke(nameof(HacerVulnerable), segundosInvulnerable);
    }

    public void FinJuego()
    {
        Debug.Log("Fin del juego");
        gameObject.SetActive(false);
    }

    private void HacerVulnerable()
    {
        vulnerable = true;
        if (sprite != null)
        {
            sprite.color = colorOriginal;
        }
    }
}
