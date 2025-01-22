using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsController : MonoBehaviour
{
    void Update()
    {
        // Detectar si se presiona la tecla "Espacio"
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Cargar la escena "01 PlayMenu"
            SceneManager.LoadScene("01 PlayMenu");
        }
    }
}