using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float health;
    public float maxHealth;
    public Rigidbody2D targetRigidbody;

    public RuntimeAnimatorController[] animatorCon;
    Animator animator;

    WaitForFixedUpdate wait;

    bool isLive;

    Rigidbody2D rigid;
    Collider2D collider;
    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator =gameObject.GetComponent<Animator>();
        wait = new WaitForFixedUpdate();
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
        if (!isLive || animator.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
        {
            return;
        }
        Vector2 dirVec = targetRigidbody.position - rigid.position;
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector2.zero;
        spriteRenderer.flipX = nextVec.x < 0;
    }
    IEnumerator KnockBack()
    {
        yield return wait;
        if (!gameObject.activeSelf)
            yield break;
        Vector3 playerPos = GameManager.Instance.player.transform.position;
        Vector3 dirVec = transform.position - playerPos;
        rigid.AddForce(dirVec.normalized * 10, ForceMode2D.Impulse);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet") || !gameObject.activeSelf)
            return;
        health -= collision.GetComponent<Bullet>().damage;
        StartCoroutine("KnockBack");
        if (health > 0)
        {
            //««±Ô¿”
            animator.SetTrigger("Hit");
        }
        else
        {
            //∏ÛΩ∫≈Õ ªÁ∏¡
            Dead();
        }
    }
    void Dead()
    {
        gameObject.SetActive(false);

    }

}
