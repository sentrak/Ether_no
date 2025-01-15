using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeAndDisableImage : MonoBehaviour
{
    [Header("Fade Settings")]
    [SerializeField] private float fadeDuration = 2f; // Duración del desvanecimiento
    [SerializeField] private float delayBeforeFade = 1f; // Tiempo de espera antes de iniciar el desvanecimiento

    private Image imageComponent; // Referencia al componente Image

    /*
     * Método: Awake.
     * Parámetros: Ninguno.
     * Descripción: Inicializa la referencia al componente Image.
     */
    private void Awake()
    {
        imageComponent = GetComponent<Image>();
        if (imageComponent == null)
        {
            Debug.LogError("El GameObject no tiene un componente Image asignado.");
        }
    }

    /*
     * Método: Start.
     * Parámetros: Ninguno.
     * Descripción: Inicia la rutina de desvanecimiento después de un retraso.
     */
    private void Start()
    {
        if (imageComponent != null)
        {
            StartCoroutine(FadeOutAndDisable());
        }
    }

    /*
     * Método: FadeOutAndDisable.
     * Parámetros: Ninguno.
     * Descripción: Desvanece la imagen gradualmente y desactiva el componente.
     */
    private IEnumerator FadeOutAndDisable()
    {
        yield return new WaitForSeconds(delayBeforeFade);
        Color originalColor = imageComponent.color;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            imageComponent.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }
        imageComponent.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
        imageComponent.enabled = false;
        Destroy(gameObject);
    }
}
