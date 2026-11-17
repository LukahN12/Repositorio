using TMPro;
using UnityEngine;

public class ControlHUD : MonoBehaviour
{
    public TMP_Text vidasTxT;
    public TMP_Text objetivosTxT;
    public TMP_Text tiempoTxT;

    public void Awake()
    {
        if (vidasTxT == null | objetivosTxT == null | tiempoTxT == null)
            Debug.LogError("No se ha asignado uno o más TMP_Text en el inspector.");

    }

    public void setVidas(int vidas)
    {
        if (vidasTxT != null)
            vidasTxT.text = "Vidas: " + vidas.ToString();
    }

    public void setObjetivos(int objetivos)
    {
        if (objetivosTxT != null)
            objetivosTxT.text = "Objetivos: " + objetivos.ToString();
    }

    public void setTiempo(int tiempo)
    {
        if (tiempoTxT == null) return;

        int minutos = tiempo / 60;
        int segundos = tiempo % 60;
        tiempoTxT.text = "Tiempo: " + minutos.ToString("00") + ":" + segundos.ToString("00");
    }
}
   
