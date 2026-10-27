using System;
using Unity.VisualScripting;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class ControlEnemigo : MonoBehaviour
{
    private Rigidbody2D fisica;

    private SpriteRenderer sprite;

    private Animator animador;

    private Boolean moviendose = true;

    [Header("Posiciones de desplazamiento")]

    public float velocidad = 1.2f;

    [SerializeField] private Vector2 posicionOriginal;

    [SerializeField] private Vector2 posicionFinal;

    [SerializeField] private float tolerancia = 0.01f;

    void Awake()
    {
        fisica = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animador = GetComponent<Animator>();
    }

    private void Start()
    {
        fisica.position = posicionOriginal;
    }

    void FixedUpdate()
    {
        if(velocidad <= 0)
        {
            return;
        }
        
        Vector2 posicionObjetivo;
        if (moviendose) {
            posicionObjetivo = posicionFinal;
        } else {
            posicionObjetivo = posicionOriginal;
        }

        if (Vector2.Distance(fisica.position, posicionObjetivo) <= tolerancia){
            moviendose = !moviendose;
            posicionObjetivo = moviendose ? posicionFinal : posicionOriginal;
                
        }
        fisica.MovePosition(Vector2.MoveTowards(fisica.position, posicionObjetivo, velocidad * Time.fixedDeltaTime));

        OrientarSprite();
        animarEnemigo();
    }

    private void animarEnemigo()
    {
        if (animador == null)
        {
            return;
        }

        if (posicionOriginal.x != posicionFinal.x)
        {
            animador.Play("EnemigoAndar");
        }
        else if (posicionOriginal.y != posicionFinal.y)
        {
            animador.Play("EnemigoQuieto");
        }
        
    }

    private void OrientarSprite()
    {
        if (sprite == null)
        {
            return;
        }

        if (posicionOriginal.x != posicionFinal.x) {
            if (moviendose)
            {

                sprite.flipX = false;
            }
            else
            {
                sprite.flipX = true;
            }
        } else if (posicionOriginal.y != posicionFinal.y){
            if (moviendose) {

                sprite.flipY = false;
            } else
            {
                sprite.flipY = true;
            }
        }

        
    }
}
