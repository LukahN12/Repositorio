using UnityEngine;

public class EfectoParalax : MonoBehaviour{

    [SerializeField, Range(0f, 1f)] private float factorHorizontal;

    [SerializeField] private Transform camara;

    [SerializeField] private Vector3 ultimaPosicionCamara;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (camara == null && Camera.main != null)
        {
            camara = Camera.main.transform;
        }
        if(camara == null)
        {
            Debug.LogError("Asigna una camara");
            enabled = true;
            return;
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 cambioCamara = camara.position - ultimaPosicionCamara;
        transform.position = new Vector3(cambioCamara.x * factorHorizontal, cambioCamara.y, 0f);
        ultimaPosicionCamara = camara.position;
    }

    private void Update()
    {
        
    }
}
