using NUnit.Framework.Constraints;
using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using MinAttribute = UnityEngine.MinAttribute;
[RequireComponent(typeof(Rigidbody2D))]
public class PlataformaMovil : MonoBehaviour
{
    private Rigidbody2D fisica;

    [SerializeField] private Transform posicionOriginal;

    [SerializeField] private Transform posicionFinal;

    [SerializeField,Min(0.1f),Max(2f)] private float velocidad = 0.5f;

    [SerializeField] private float tolerancia = 0.01f;
    private Boolean moviendose = true;


    void Awake()
    {
        fisica = GetComponent<Rigidbody2D>();
    }

    public Vector2 Velocidad
    {
        get; 
        private set;
    } 

    void Start()
    {
        if(posicionOriginal == null || posicionFinal == null)
        {
            Debug.LogError("Ninguna de las posiciones de origen o destino pueden estar vacias!");
            return;
        }
        fisica.position = (Vector2) posicionOriginal.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 posicionObjetivo;
        if (moviendose)
        {
            posicionObjetivo = posicionFinal.position;
        }
        else
        {
            posicionObjetivo = posicionOriginal.position;
        }

        if (Vector2.Distance(fisica.position, posicionObjetivo) <= tolerancia)
        {
            moviendose = !moviendose;
            posicionObjetivo = moviendose ? posicionFinal.position : posicionOriginal.position;

        }
        fisica.MovePosition(Vector2.MoveTowards(fisica.position, posicionObjetivo, velocidad * Time.fixedDeltaTime));
    }

    private void OnDrawGizmos()
    {
        if(posicionOriginal == null || posicionFinal == null)
        {
            return;
        }

        Gizmos.color = Color.red;
        Gizmos.DrawLine(posicionOriginal.position, posicionFinal.position);
    }
}
