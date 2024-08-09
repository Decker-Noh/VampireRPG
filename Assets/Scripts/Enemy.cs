using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float health;
    public float maxHealth;
    public Rigidbody2D targetRigidbody;

    public RuntimeAnimatorController[] animatorCon;
    Animator animator;

    bool isLive;

    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator =gameObject.GetComponent<Animator>();
    }
    private void OnEnable()
    {
        targetRigidbody = GameManager.Instance.player.GetComponent<Rigidbody2D>();
        isLive = true;
        health = maxHealth;
    }
    public void EnemyInit(SpawnData spawnData)
    {
        speed = spawnData.speed;
        maxHealth = spawnData.health;
        health = maxHealth;
        animator.runtimeAnimatorController = animatorCon[spawnData.spriteInt];
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isLive)
        {
            return;
        }
        Vector2 dirVec = targetRigidbody.position - rigid.position;
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector2.zero;
        spriteRenderer.flipX = nextVec.x < 0;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet"))
            return;
        health -= collision.GetComponent<Bullet>().damage;
        if (health > 0)
        {
            Debug.Log(health + "ø©±‚2");
            //««±Ô¿”
        }
        else
        {
            Debug.Log(health + "ªÁ∏¡");
            //∏ÛΩ∫≈Õ ªÁ∏¡
            Dead();
        }
    }
    void Dead()
    {
        gameObject.SetActive(false);

    }

}
