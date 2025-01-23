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
    [Header("Msuic and SFX Settings")]
    [SerializeField] private AudioClip projectile; // sonido de projectile 

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
        status = DragonStatus.ATTACK;


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
                SpawnFireballs(); 
                Debug.Log("Si activó");
                break;

            case DragonStatus.SPECIAL:
                anim.SetTrigger("special");
                StartCoroutine(DragonStatuses());
                break;
        }
    }

    private void SpawnFireballs()
    {
        AudioManager.Instance.PlaySound(projectile);
            projectilePooling.SpawnProjectile();
        
    }
}
