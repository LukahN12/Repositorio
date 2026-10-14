using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class NuevoSistemaInputs : MonoBehaviour
{
    public InputAction moveAction;

    public float velocity;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        moveAction.Enable();
    }

    // Update is called once per frame
    void Update() {
        Vector2 move = moveAction.ReadValue<Vector2>();

        Vector2 position = (Vector2) transform.position + move * velocity * Time.deltaTime; //Calculamos la nueva posición 

        transform.position = position; //Asignamos la nueva posición 
    }
}
