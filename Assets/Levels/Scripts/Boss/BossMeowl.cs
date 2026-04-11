using System.Collections;
using UnityEngine;

public class BossMeowl : MonoBehaviour
{
    [Header("Stats Bos")]
    public int maxHP = 5; // Butuh bunuh 5 kroco buat ngalahin bos
    private int currentHP;

    [Header("Sistem Summon")]
    public GameObject krocoPrefab; // Prefab musuh kroconya
    public float kekuatanSembur = 3f;
    public float waktuSummon = 4f; // Summon kroco setiap 4 detik

    [Header("Efek Terbang")]
    public float kecepatanMelayang = 2f;
    public float tinggiMelayang = 0.5f;
    private Vector3 posisiAwal;

    private Animator anim;

    void Start()
    {
        currentHP = maxHP;
        posisiAwal = transform.position;
        anim = GetComponent<Animator>();

        // Mulai rutinitas summon kroco
        StartCoroutine(SummonKroco());
    }

    void Update()
    {
        // Bikin bosnya melayang naik-turun secara natural
        float hoverY = Mathf.Sin(Time.time * kecepatanMelayang) * tinggiMelayang;
        transform.position = posisiAwal + new Vector3(0, hoverY, 0);
    }

    private IEnumerator SummonKroco()
    {
        while (currentHP > 0)
        {
            yield return new WaitForSeconds(waktuSummon);

            // Munculin kroconya
            GameObject krocoBaru = Instantiate(krocoPrefab, transform.position, Quaternion.identity);

            // Kasih tau kroconya siapa bosnya
            MinionBoss scriptKroco = krocoBaru.GetComponent<MinionBoss>();
            if (scriptKroco != null)
            {
                scriptKroco.bosAtasan = this; 
            }

            // 3. Efek Terlempar (Disembur dari bos)
            Rigidbody2D rbKroco = krocoBaru.GetComponent<Rigidbody2D>();
            if (rbKroco != null)
            {
                // Pilih arah X acak (ke kiri atau ke kanan)
                float arahX = Random.Range(-1f, 1f); 
                
                // Bikin vektor arah lemparan (X acak, Y sedikit ke atas biar melengkung)
                Vector2 arahLempar = new Vector2(arahX, 1f).normalized; 
                
                // Gunakan linearVelocity persis kayak di script Player kamu
                rbKroco.linearVelocity = arahLempar * kekuatanSembur;
            }
        }
    }

    // Fungsi ini bakal dipanggil SAMA SI KROCO pas dia mati
    public void KenaDamage()
    {
        currentHP--;
        Debug.Log("Sisa HP Bos: " + currentHP);

        if (anim != null) anim.SetTrigger("Hurt"); // Memicu animasi kena hit

        if (currentHP <= 0)
        {
            Mati();
        }
    }

    private void Mati()
    {
        Debug.Log("Bos Berhasil Dikalahkan!");
        StopAllCoroutines(); // Stop summon kroco

        if (anim != null) anim.SetTrigger("Die");
        
        Destroy(gameObject, 3f); 
    }
}