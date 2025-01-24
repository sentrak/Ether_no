using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private const string LastSceneKey = "LastScene"; // Clave para guardar la escena en PlayerPrefs

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// Guarda la escena actual como la última escena jugada.
    /// </summary>
    public void SaveScene()
    {
        string currentScene = SceneManager.GetActiveScene().name; // Obtiene el nombre de la escena actual
        PlayerPrefs.SetString(LastSceneKey, currentScene);
        PlayerPrefs.Save();
        Debug.Log($"Escena guardada: {currentScene}");
    }

    /// <summary>
    /// Carga la última escena guardada.
    /// </summary>
    public void LoadLastScene()
    {
        if (PlayerPrefs.HasKey(LastSceneKey))
        {
            string lastScene = PlayerPrefs.GetString(LastSceneKey);
            Debug.Log($"Cargando última escena guardada: {lastScene}");
            SceneManager.LoadScene(lastScene);
        }
        else
        {
            Debug.LogWarning("No se ha guardado ninguna escena previamente.");
        }
    }

    /// <summary>
    /// Limpia los datos de la escena guardada.
    /// </summary>
    public void ClearSavedScene()
    {
        if (PlayerPrefs.HasKey(LastSceneKey))
        {
            PlayerPrefs.DeleteKey(LastSceneKey);
            Debug.Log("Datos de escena guardada eliminados.");
        }
        else
        {
            Debug.LogWarning("No había datos de escena guardados para eliminar.");
        }
    }
}
