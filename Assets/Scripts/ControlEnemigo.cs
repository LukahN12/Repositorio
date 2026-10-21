using System;
using Unity.VisualScripting;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class ControlEnemigo : MonoBehaviour
{
    private Rigidbody2D fisica;

    private Boolean subiendo = true;

    [Header("Posiciones de desplazamiento")]

    public float velocidad = 1.2f;

    [SerializeField] private Vector2 posicionOriginal;

    [SerializeField] private Vector2 posicionFinal;

    [SerializeField] private float tolerancia = 0.01f;

    void Awake()
    {
        fisica = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        posicionOriginal = fisica.position;
    }

    void FixedUpdate()
    {
        if(velocidad <= 0)
        {
            return;
        }

        Vector2 posicionObjetivo;
        if (subiendo) {
            posicionObjetivo = posicionFinal;
        } else {
            posicionObjetivo = posicionOriginal;
        }

        if (Vector2.Distance(fisica.position, posicionObjetivo) <= tolerancia){
            subiendo = !subiendo;
            posicionObjetivo = subiendo ? posicionFinal : posicionOriginal;
                
        }
        fisica.MovePosition(Vector2.MoveTowards(fisica.position, posicionObjetivo, velocidad * Time.fixedDeltaTime));
    } 

    
}
