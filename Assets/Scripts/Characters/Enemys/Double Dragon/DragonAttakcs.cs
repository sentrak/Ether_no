using System.Collections;
using UnityEngine;

public enum DragonStatus
{
    IDLE, ATTACK, SPECIAL, DEAD
}

public class DragonAttakcs : MonoBehaviour
{
    Animator anim;
    private Enemy enemy; // Referencia al script Enemy que contiene las estadísticas del enemigo
    public DragonStatus status;

    [SerializeField] private float statusChange; // Tiempo entre cambios de estado
    [SerializeField] private ProjectilePooling projectilePooling; // Referencia al sistema de pooling de proyectiles

    void Start()
    {
        status = DragonStatus.IDLE;
        anim = GetComponent<Animator>();

        // Validación de la referencia al sistema de proyectiles
        if (projectilePooling == null)
        {
            Debug.LogError("ProjectilePooling is not assigned in DragonAttacks.");
        }

        StartCoroutine(DragonStatuses());
    }

    IEnumerator DragonStatuses()
    {
        // Cambia el estado del dragón tras un tiempo aleatorio
        yield return new WaitForSeconds(statusChange);

        if (enemy != null && enemy.healtPoints % 10 == 0)
        {
            status = DragonStatus.SPECIAL;
        }
        else
        {
            status = DragonStatus.ATTACK;
        }

        StatusChanger();
    }

    public void StatusChanger()
    {
        switch (status)
        {
            case DragonStatus.IDLE:
                anim.SetBool("idle", true);
                StartCoroutine(DragonStatuses());
                break;

            case DragonStatus.ATTACK:
                anim.SetTrigger("attack");
                StartCoroutine(DragonStatuses());
                SpawnFireballs(); // Generar proyectiles
                break;

            case DragonStatus.SPECIAL:
                anim.SetTrigger("special");
                StartCoroutine(DragonStatuses());
                break;
        }
    }

    private void SpawnFireballs()
    {
        if (projectilePooling != null)
        {
            projectilePooling.SpawnProjectile();
        }
    }
}
