using UnityEngine;

public class ControlPowerup : MonoBehaviour
{

    [SerializeField, Range(1, 10)] private int cantidad = 2;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
        {
            return;
        }

        if (collision.gameObject.TryGetComponent(out MovimientoJugador movimientoJugador))
        {
            movimientoJugador.incrementarPuntos(cantidad);
            Destroy(gameObject);
        } else
        {
            Debug.LogWarning("El objeto que colisionó no tiene el componente MovimientoJugador.");
        }
    }
}
