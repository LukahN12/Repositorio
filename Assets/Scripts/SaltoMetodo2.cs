using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class SaltoMetodo2 : MonoBehaviour
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

    public float distanciaSuelo = 0.2f; //El radio de la esfera en los pies

    public Transform bolaPie;

    private Vector2 entradaX;

    private bool pendienteSaltar;

    private bool tocandoSuelo;

    private void Awake()
    {
        // Detiene el rigidBody para modificar su velocidad linear
        fisica = GetComponent<Rigidbody2D>();
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        entradaX = actionMover.action.ReadValue<Vector2>();

        tocandoSuelo = Physics2D.OverlapCircle(bolaPie.position,distanciaSuelo,capaSuelo);



        if (actionSaltar.action.WasPressedThisFrame() && tocandoSuelo)
        {
            pendienteSaltar = true;
        }
    }

    private void FixedUpdate()
    {
        fisica.linearVelocity = new Vector2(entradaX.x * velocidad, fisica.linearVelocityY);

        // fisica.linearVelocity = new Vector2(fisica.linearVelocityX, entradaY.y * velocidad);


        //Genera un movimiento hacia arria (.down para abajo) y lo multiplica por el impulso
        if (pendienteSaltar)
        {
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

    private void OnDisable()
    {
        if (actionMover != null || actionMover.action != null)
        {
            actionMover.action.Disable();
        }

        if (actionSaltar != null || actionSaltar.action != null)
        {
            actionSaltar.action.Disable();
        }
    }

    private void OnDrawGizmosSelected()
    {
       if(bolaPie != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(bolaPie.position, distanciaSuelo);
        }
    }
}
