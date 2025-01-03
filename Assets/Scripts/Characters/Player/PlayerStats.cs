using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [Header("Player Stats")]
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int maxMana = 50;
    public float health;
    public float mana;
    public float inmunityTime;
    bool isInmune;
    //Blink material;
    SpriteRenderer sprite;
    public float knockBackForceX;
    public float knockBackForceY;
    Rigidbody2D rb;
    Animator animator;
    public Image healtImg;
    public Image ManaImg;
    private PlayerMoviement playerMoviement;


    void Start()
    {
        playerMoviement = GetComponent<PlayerMoviement>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        //material = GetComponent<Blink>();
        health = maxHealth;
        mana = maxMana;
    }

    // Update is called once per frame
    void Update()
    {
        healtImg.fillAmount = health / maxHealth;
        ManaImg.fillAmount = mana / maxMana;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        else if (health <= 0)
        {
            health = 0;
        }
        if (mana > maxMana)
        {
            mana = maxMana;
        }
        else if (mana <= 0)
        {
            mana = 0;
        }

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && !isInmune)
        {
            health -= 10;
            StartCoroutine(Inmunity());
            if (collision.transform.position.x > transform.position.x)
            {
                rb.AddForce(new Vector2(-knockBackForceX, knockBackForceY), ForceMode2D.Force);

            }
            else
            {
                rb.AddForce(new Vector2(knockBackForceX, knockBackForceY), ForceMode2D.Force);

            }

            if (health <= 0)
            {
                print("El jugador esta muerto murido, desvivido, se fue a conocer a san pedro, colgo los tenis, se lo cargaron los municipales, los tombos lo llevaron a dar una vuelta");
            }
        }
    }
    private void heal(int amount)
    {

    }
    IEnumerator Inmunity()
    {
        playerMoviement.horizontal = 0;
        animator.SetTrigger("getHit"); // Contacto con el suelo
        isInmune = true;
        //sprite = material.blink;
        yield return new WaitForSeconds(inmunityTime);
        //  sprite.material = material.original;
        isInmune = false;

    }

    public void UseMana(int amount)
    {
        mana -= amount;
    }
}
