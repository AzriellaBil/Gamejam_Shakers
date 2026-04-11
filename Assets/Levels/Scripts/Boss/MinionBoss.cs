using UnityEngine;

public class MinionBoss : MonoBehaviour
{
    public int maxHealth = 40; 
    private int currentHealth;

    [HideInInspector] 
    public BossMeowl bosAtasan; // Disembunyikan di Inspector karena diisi otomatis sama script Bos

    private Animator anim;

    void Start()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
    }

    // Fungsi ini dipanggil dari script PlayerCombat kamu pas player mukul
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Mati();
        }
    }

    private void Mati()
    {
        // 1. Lapor ke bos kalau kroco ini mati!
        if (bosAtasan != null)
        {
            bosAtasan.KenaDamage();
        }

        // 3. Matikan collider dan script biar nggak bisa dipukul lagi pas udah jadi mayat
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;

        // 4. Hapus mayatnya setelah 1.5 detik
        Destroy(gameObject);
    }
}