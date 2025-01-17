using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/*
 * Clase: FadeAndDisableImage.
 * Descripción: Gestiona el desvanecimiento gradual de una imagen en un Canvas y la desactiva al completar el proceso.
 *              También destruye el GameObject al final del desvanecimiento.
 */
public class FadeAndDisableImage : MonoBehaviour
{
    [Header("Fade Settings")]
    [SerializeField] private float fadeDuration = 2f; // Duración del desvanecimiento en segundos
    [SerializeField] private float delayBeforeFade = 1f; // Tiempo de espera antes de iniciar el desvanecimiento

    private Image imageComponent; // Referencia al componente Image del GameObject

    /*
     * Método: Awake.
     * Parámetros: Ninguno.
     * Descripción: Inicializa la referencia al componente Image del GameObject.
     */
    private void Awake()
    {
        imageComponent = GetComponent<Image>();
    }

    /*
     * Método: Start.
     * Parámetros: Ninguno.
     * Descripción: Inicia una rutina para desvanecer la imagen después de un retraso especificado.
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
     * Descripción: Gradualmente reduce la opacidad de la imagen hasta que desaparece y desactiva el componente.
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
