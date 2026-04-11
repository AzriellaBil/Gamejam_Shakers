using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private Sprite altarNyala;
    [SerializeField] private Sprite altarMati;
    
    private SpriteRenderer sr;
    private bool isUsed = false; // Buat ngecek altar ini udah pernah dipake belum

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = altarMati; // Awal mulai pasti mati
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Kalau player nyentuh dan altarnya belum pernah dipake
        if (collision.CompareTag("Player") && !isUsed)
        {
            // Lapor ke script player buat nge-set spawnpoint baru
            collision.GetComponent<PlayerRespawn>().SetNewCheckpoint(this);
            Nyala();
        }
    }

    public void Nyala()
    {
        sr.sprite = altarNyala;
        isUsed = true; // Kunci altarnya biar nggak bisa nyala-mati berulang kali
    }

    public void MatiinPermanen()
    {
        sr.sprite = altarMati;
        // isUsed tetep TRUE! Jadi altar lama ini udah nggak bisa diinjek lagi
    }
}