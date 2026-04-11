using UnityEngine;

public class MinionBoss : MonoBehaviour , IDamageable
{

    [SerializeField] public int maxHealth;
    public int currentHealth { get; private set;}

    public BossMeowl bosAtasan;

    //private Animator anim;
    private bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;
        //anim = GetComponent<Animator>();
    }

    // Fungsi ini dipanggil dari script PlayerCombat kamu pas player mukul
    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Mati();
        }
    }

    private void Mati()
    {
        isDead = true;

        // 1. Lapor ke bos kalau kroco ini mati!
        if (bosAtasan != null)
        {
            bosAtasan.KenaDamage();
        }

        // 3. Matikan collider dan script biar nggak bisa dipukul lagi pas udah jadi mayat
        Collider2D coll = GetComponent<Collider2D>();
        if (coll != null) coll.enabled = false;
        
        this.enabled = false;
        Destroy(transform.parent.gameObject, 0.1f);
    }
}