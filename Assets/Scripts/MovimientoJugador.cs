using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(Rigidbody2D))]
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

    [Header("Detectores")]

    public float longitudRaycast = 1.5f;

    private Vector2 entradaX;

    private Vector2 entradaY;

    private bool pendienteSaltar;

    private bool tocandoSuelo;

    // Se ejecuta antes que el Start y se ejecuta INCLUSO si el script esta desactivado. 
    private void Awake() {
        
        // Detiene el rigidBody para modificar su velocidad linear
        fisica = GetComponent<Rigidbody2D>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
       
    }

    // Update is called once per frame
    void Update() {

        entradaX = actionMover.action.ReadValue<Vector2>();



        // entradaY = actionMover.action.ReadValue<Vector2>();
        /*
        RaycastHit2D hit = Physics2D.Raycast(transform.position,Vector2.down,longitudRaycast,capaSuelo);

        tocandoSuelo = hit.collider != null;

        Color colorRayo = tocandoSuelo ? Color.green : Color.red;

        Debug.DrawRay(transform.position, Vector2.down * longitudRaycast, colorRayo);

        //Devuelve True UNA VEZ si el espacio a sido presionado, luego de eso se vuelve FALSE (Para registrar mantener presionado usar IsPressed() ) 
        if (actionSaltar.action.WasPressedThisFrame() && tocandoSuelo) {
            pendienteSaltar = true;
        }
        */

    }

    private void FixedUpdate()
    {
        fisica.linearVelocity = new Vector2(entradaX.x * velocidad, fisica.linearVelocityY);

        // fisica.linearVelocity = new Vector2(fisica.linearVelocityX, entradaY.y * velocidad);


        //Genera un movimiento hacia arria (.down para abajo) y lo multiplica por el impulso
        if (pendienteSaltar) {
            fisica.AddForce(Vector2.up * impulso, ForceMode2D.Impulse);
            pendienteSaltar = false;
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

}
