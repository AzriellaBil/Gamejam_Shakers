using System.Collections;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] public Transform attackPoint;
    [SerializeField] public float attackRange = 0.5f;
    [SerializeField] public int attackDamage = 40;

    [SerializeField] public LayerMask enemyLayers;
    [SerializeField] private float attackDuration = 0.15f;
    
    public bool isAttacking { get; private set; }
    private Animator anim;
    private PlayerMovementScript movement;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        movement = GetComponent<PlayerMovementScript>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J) && !isAttacking && !movement.IsWallSliding)
        {
            StartCoroutine(AttackAnimation());
        }
    }

    private IEnumerator AttackAnimation()
    {
        isAttacking = true;

        if(anim != null)
        {
            anim.SetTrigger("Attack");
        }

        Attack();

        float arahY = transform.eulerAngles.y;
        transform.eulerAngles = new Vector3(0f, arahY, 0f);

        yield return new WaitForSeconds(attackDuration);

        transform.eulerAngles = new Vector3(0f, arahY, 0f);
        isAttacking = false;
    }

    void Attack()
    {
        // 1. Buat lingkaran deteksi (Hitbox) dan cari semua yang kena
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemyCollider in hitEnemies)
        {
            EnemyHealth musuhBiasa = enemyCollider.GetComponent<EnemyHealth>();
            if (musuhBiasa != null)
            {
                musuhBiasa.TakeDamage(attackDamage);
                continue;
            }

            MinionBoss krocoBos = enemyCollider.GetComponent<MinionBoss>();
            if (krocoBos != null)
            {
                krocoBos.TakeDamage(attackDamage);
            }
        }
    }

    // Fitur tambahan biar kamu bisa lihat area serangannya di dalam Editor Unity (garis merah)
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}