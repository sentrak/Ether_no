using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class DragonDeathManager : MonoBehaviour
{
    private static DragonDeathManager _instance;

    public static DragonDeathManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("DragonDeathManager is NULL!");
            }
            return _instance;
        }
    }

    private bool isDragonDead = false; // Indica si el dragón está muerto
    private bool isElectricDragonDead = false; // Indica si el dragón eléctrico está muerto

    [SerializeField] private string nextSceneName; // Nombre de la escena a cargar cuando ambos dragones estén muertos

    private void Awake()
    {
        // Asegurar que el Singleton es único
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject); // No destruir este objeto al cambiar de escena
    }

    public void SetDragonDead()
    {
        isDragonDead = true;
        CheckBothDragonsDead();
    }

    public void SetElectricDragonDead()
    {
        isElectricDragonDead = true;
        CheckBothDragonsDead();
    }

    private void CheckBothDragonsDead()
    {
        if (isDragonDead && isElectricDragonDead)
        {
            Debug.Log("Both dragons are dead. Transitioning to next scene in 4 seconds...");
            StartCoroutine(LoadNextSceneWithDelay(4f));
        }
    }

    private IEnumerator LoadNextSceneWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Esperar los 4 segundos
        SceneManager.LoadScene(nextSceneName); // Cargar la siguiente escena
    }
}
