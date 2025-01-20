using System.Collections;
using UnityEngine;

/*
 * Clase: DragonGetHits.
 * Descripción: Maneja la detección de golpes al dragón y aplica el daño correspondiente.
 */
public class DragonGetHits : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] private AudioClip getHit; // Clip de audio reproducido al recibir un golpe
    [SerializeField] private AudioClip getMagic; // Clip de audio reproducido al recibir un ataque mágico

    private DragonStats dragonStats; // Referencia al script DragonStats en el GameObject padre

    /*
     * Método: Start.
     * Descripción: Inicializa las referencias al script DragonStats y Animator desde el GameObject padre.
     */
    void Start()
    {
        dragonStats = GetComponentInParent<DragonStats>();

        if (dragonStats == null)
        {
            Debug.LogError("DragonStats script is missing on the parent object.");
        }
    }

    /*
     * Método: OnTriggerEnter2D.
     * @param collision: Collider del objeto que entra en contacto.
     * Descripción: Maneja el daño del dragón cuando recibe un golpe.
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
     * Descripción: Reduce los puntos de vida del dragón y activa la animación "getHit".
     */
    private void ApplyDamage(int damage)
    {
        if (dragonStats != null)
        {
            dragonStats.TakeDamage(damage);
        }
    }
}
