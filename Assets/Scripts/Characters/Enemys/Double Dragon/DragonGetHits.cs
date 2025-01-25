using System.Collections;
using UnityEngine;

/*
 * Clase: DragonGetHits.
 * Descripción: Gestiona la detección de golpes al dragón, aplica daño y reproduce efectos de sonido según el tipo de ataque recibido.
 */
public class DragonGetHits : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] private AudioClip getHit; // Sonido reproducido al recibir un golpe físico
    [SerializeField] private AudioClip getMagic; // Sonido reproducido al recibir un ataque mágico

    [Header("Dragon Components")]
    private DragonStats dragonStats; // Referencia al script DragonStats para gestionar la vida del dragón

    /*
     * Método: Start.
     * Parámetros: Ninguno.
     * Descripción: Inicializa las referencias necesarias y verifica la existencia del script DragonStats.
     */
    private void Start()
    {
        dragonStats = GetComponentInParent<DragonStats>();
    }

    /*
     * Método: OnTriggerEnter2D.
     * @param collision: Collider del objeto que entra en contacto.
     * Descripción: Detecta colisiones con el dragón y aplica el daño correspondiente según el tipo de ataque.
     */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerPunch"))
        {
            ApplyDamage(10);
            AudioManager.Instance.PlaySound(getHit);
        }
        else if (collision.CompareTag("PlayerCross"))
        {
            ApplyDamage(20);
            AudioManager.Instance.PlaySound(getMagic);
        }
    }

    /*
     * Método: ApplyDamage.
     * @param damage: Cantidad de daño a aplicar al dragón.
     * Descripción: Reduce los puntos de vida del dragón llamando al método TakeDamage en DragonStats.
     */
    private void ApplyDamage(int damage)
    {
        if (dragonStats != null)
        {
            dragonStats.TakeDamage(damage);
        }
    }
}
