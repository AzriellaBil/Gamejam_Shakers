using System.Collections;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] public Transform attackPoint;
    [SerializeField]public float attackRange = 0.5f;
    [SerializeField] public int attackDamage = 40;
    [SerializeField] public LayerMask enemyLayers;
    [SerializeField] private float attackDuration = 0.15f;
    
    public bool isAttacking { get; private set; }

    void Update()
    {
        // Contoh: Kalau pencet klik kiri mouse atau tombol J
        if (Input.GetKeyDown(KeyCode.J) && !isAttacking)
        {
            StartCoroutine(AttackAnimation());
        }
    }

    private IEnumerator AttackAnimation()
    {
        isAttacking = true;

        float arahY = transform.eulerAngles.y;
        transform.eulerAngles = new Vector3(0f, arahY, -45f);

        Attack();
        yield return new WaitForSeconds(attackDuration);

        transform.eulerAngles = new Vector3(0f, arahY, 0f);
        isAttacking = false;
    }

    void Attack()
    {
        // 1. Buat lingkaran deteksi (Hitbox) dan cari semua yang kena
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        // 2. Kasih damage ke semua musuh yang kena di dalam lingkaran
        foreach (Collider2D enemyCollider in hitEnemies)
        {
            // Ambil komponen "Enemy" dari objek yang ketabrak, lalu panggil fungsi TakeDamage()
            enemyCollider.GetComponent<EnemyHealth>().TakeDamage(attackDamage);
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
