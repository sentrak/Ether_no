using System.Collections;
using UnityEngine;
using TMPro;

public class CameraTrigger : MonoBehaviour
{
    public Camera mainCamera; // Cámara principal
    public Transform targetPosition; // Posición de destino
    public float zoomAmount = 5f; // Nivel de zoom
    public float moveSpeed = 5f; // Velocidad de movimiento
    public GameObject textPanel; // Panel de texto
    public string storyText; // Texto que se mostrará

    private bool triggered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !triggered)
        {
            triggered = true;
            StartCoroutine(MoveCameraAndDisplayText());
        }
    }

    private IEnumerator MoveCameraAndDisplayText()
    {
        Debug.Log("Corrutina MoveCameraAndDisplayText");
        Debug.Log("Texto a mostrar: " + storyText);

        Vector3 startPos = mainCamera.transform.position;
        Vector3 endPos = new Vector3(targetPosition.position.x, targetPosition.position.y, mainCamera.transform.position.z);

        Debug.Log($"Posición inicial de la cámara: {startPos}");
        Debug.Log($"Posición de destino de la cámara: {endPos}");

        float journeyLength = Vector3.Distance(startPos, endPos);
        float startTime = Time.time;

        while (Vector3.Distance(mainCamera.transform.position, endPos) > 0.1f)
        {
            float distanceCovered = (Time.time - startTime) * moveSpeed;
            float fractionOfJourney = distanceCovered / journeyLength;
            mainCamera.transform.position = Vector3.Lerp(startPos, endPos, fractionOfJourney);


            float currentDistance = Vector3.Distance(mainCamera.transform.position, endPos);
            Debug.Log($"Distancia restante: {currentDistance}");

            if (currentDistance <= 0.1f)
            {
                break;
            }

            yield return null;
        }

        mainCamera.transform.position = endPos; 
        Debug.Log("Posición final de la cámara alcanzada.");

        // Ampliar el zoom
        mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, zoomAmount, 0.9f);
        Debug.Log("Ampliando zoom");

        // Mostrar el texto
        if (textPanel != null)
        {
            textPanel.SetActive(true);
            TextMeshProUGUI textComponent = textPanel.GetComponentInChildren<TextMeshProUGUI>();
            if (textComponent != null)
            {
                Debug.Log("Mostrando texto...");
                yield return StartCoroutine(DisplayText(textComponent));
            }
            else
            {
                Debug.LogWarning("No se encontró un componente TextMeshProUGUI en el panel de texto.");
            }
        }
        else
        {
            Debug.LogWarning("El panel de texto no está asignado.");
        }
    }

    private IEnumerator DisplayText(TextMeshProUGUI textComponent)
    {
        Debug.Log($"Texto a mostrar: {storyText}");
        Debug.Log("Asignando texto: " + storyText);
        textComponent.text = "";
        foreach (char c in storyText.ToCharArray())
        {
            Debug.Log($"Escribiendo letra: {c}");
            textComponent.text += c;
            yield return new WaitForSeconds(0.06f);
        }
    }
}
