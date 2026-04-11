using System.Collections;
using UnityEngine;
using TMPro;

public class PlayerRespawn : MonoBehaviour
{
    [Header("UI Settings")]
    [SerializeField] private GameObject deathScreen;
    [SerializeField] private TextMeshProUGUI deathText;

    [Header("Referensi")]
    [SerializeField] private Health healthScript;
 
    private Checkpoint currentCheckpoint;
    private Vector3 startPosition;
    private PlayerMovementScript gerakScript;
    private Rigidbody2D rb;
    private float originalGravity;

    private bool isRespawning = false;

    void Start()
    {
        startPosition = transform.position;
        gerakScript = GetComponent<PlayerMovementScript>();
        rb = GetComponent<Rigidbody2D>();
        originalGravity = rb.gravityScale;
 
        if (healthScript == null)
            healthScript = GetComponent<Health>();
    }

    // dipanggil sama checkpoint
    public void SetNewCheckpoint(Checkpoint altarBaru)
    {
        if (currentCheckpoint != null && currentCheckpoint != altarBaru)
        {
            currentCheckpoint.MatiinPermanen();
        }
        
        currentCheckpoint = altarBaru;
    }

    // Dipanggil oleh Health.cs (damage musuh) dan OnTriggerEnter2D (jatuh void)
    public void TriggerDeath(string alasan)
    {
        // Guard: jangan proses kalau lagi respawn
        if (isRespawning) return;
        StartCoroutine(ProsesRespawn(alasan));
    }
 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Void"))
            TriggerDeath("Terjatuh ke Dalam Jurang!");
    }

    private IEnumerator ProsesRespawn(string alasan)
    {
        isRespawning = true;

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
        transform.position = currentCheckpoint != null
            ? currentCheckpoint.transform.position
            : startPosition;

        // 5. Reset HP, kembalikan gravitasi & hidupkan pergerakan
        healthScript.ResetHealth();

        rb.gravityScale = originalGravity;
        gerakScript.enabled = true;
        deathScreen.SetActive(false);

        isRespawning = false;
    }
}