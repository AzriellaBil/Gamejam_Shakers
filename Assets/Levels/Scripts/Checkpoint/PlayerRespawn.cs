using System.Collections;
using UnityEngine;
using TMPro; // Wajib pakai ini buat UI Teks

public class PlayerRespawn : MonoBehaviour
{
    [Header("UI Settings")]
    [SerializeField] private GameObject deathScreen;
    [SerializeField] private TextMeshProUGUI deathText;

    [Header("Stats")]
    public int maxHP = 100;
    private int currentHP;

    private Checkpoint currentCheckpoint;
    private Vector3 startPosition; // Posisi awal kalau player belum nemu altar satupun
    private PlayerMovementScript gerakScript; // Referensi script jalan kamu
    private Rigidbody2D rb;

    void Start()
    {
        currentHP = maxHP;
        startPosition = transform.position;
        gerakScript = GetComponent<PlayerMovementScript>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Fungsi ini dipanggil sama altarnya
    public void SetNewCheckpoint(Checkpoint altarBaru)
    {
        // Kalau player udah punya altar lama, matiin altar lamanya!
        if (currentCheckpoint != null && currentCheckpoint != altarBaru)
        {
            currentCheckpoint.MatiinPermanen();
        }
        
        // Simpan altar baru ini sebagai tempat respawn
        currentCheckpoint = altarBaru;
    }

    // Fungsi kalau kena damage musuh
    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        if (currentHP <= 0)
        {
            Mati("Mati Dibantai Musuh!");
        }
    }

    // Fungsi buat ngecek jatuh ke Void
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Void"))
        {
            Mati("Terjatuh ke Dalam Jurang!");
        }
    }

    // Proses Kematian
    private void Mati(string alasanMati)
    {
        StartCoroutine(ProsesRespawn(alasanMati));
    }

    private IEnumerator ProsesRespawn(string alasan)
    {
        // 1. Matikan pergerakan & gravitasi biar player nggak tembus tanah pas mati
        gerakScript.enabled = false;
        rb.linearVelocity = Vector2.zero;
        rb.gravityScale = 0f;

        // 2. Tampilkan pesan
        deathText.text = alasan;
        deathScreen.SetActive(true);

        // 3. Tunggu 2 detik biar dramatis
        yield return new WaitForSeconds(2f);

        // 4. Pindahkan posisi player ke altar (atau posisi awal kalau belum nemu altar)
        if (currentCheckpoint != null)
        {
            transform.position = currentCheckpoint.transform.position;
        }
        else
        {
            transform.position = startPosition;
        }

        // 5. Reset HP, kembalikan gravitasi & hidupkan pergerakan
        currentHP = maxHP;
        rb.gravityScale = 6f; // Sesuaikan dengan gravitasi awal player kamu (misal 3)
        gerakScript.enabled = true;
        deathScreen.SetActive(false);
    }
}