using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlMenu : MonoBehaviour
{
   public void Jugar() { 
        SceneManager.LoadScene("Nivel1");
   }

    public void Creditos() { 
        SceneManager.LoadScene("Creditos");
    }

    public void SalirAlMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Salir() {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
