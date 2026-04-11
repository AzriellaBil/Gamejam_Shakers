using System.Collections;
using UnityEngine;

public class BossMeowl : MonoBehaviour
{
    [Header("Stats Bos")]
    public int maxHP = 5; // Butuh bunuh 5 kroco buat ngalahin bos
    private int currentHP;
    private int curMinion;
    private bool BossEnabled = false;
    [SerializeField] int maxMinion;
    public LayerMask Ground;

    [Header("Sistem Summon")]
    public GameObject krocoPrefab; // Prefab musuh kroconya
    public float kekuatanSembur = 3f;
    public float waktuSummon = 4f; // Summon kroco setiap 4 detik

    [Header("Efek Terbang")]
    public float kecepatanMelayang = 2f;
    public float tinggiMelayang = 0.5f;
    private Vector3 posisiAwal;

    private Animator anim;
    public StringEventChannel onNewLocation;

    void OnEnable()
    {
        if (onNewLocation == null)
        {
            Debug.LogError("onNewLocation is not assigned in the Inspector!", this);
            return;
        }
        onNewLocation.OnEventRaised += EnableBoss;
    }

    void OnDisable()
    {
        onNewLocation.OnEventRaised -= EnableBoss;
    }

    void EnableBoss(string Locations)
    {

        if (Locations == "Boss Stage")
        {
            BossEnabled = true;
            currentHP = maxHP;
            posisiAwal = transform.position;
            anim = GetComponent<Animator>();

            // Mulai rutinitas summon kroco
            StartCoroutine(SummonKroco());

        }

    }


    void Update()
    {
        if (BossEnabled == true)
        {
            float hoverY = Mathf.Sin(Time.time * kecepatanMelayang) * tinggiMelayang;
            transform.position = posisiAwal + new Vector3(0, hoverY, 0);

        }
    }

    private IEnumerator SummonKroco()
    {
        while (currentHP > 0 && curMinion <= maxMinion)
        {
            yield return new WaitForSeconds(waktuSummon);

            // Munculin kroconya

            
            RaycastHit2D hit = Physics2D.Raycast(transform.position,Vector2.down, 30f, Ground);
            if (hit.collider != null)
            {
                // Vector2 HitPos = hit.point + new Vector2(0f,-5f);
                GameObject krocoBaru = Instantiate(krocoPrefab, transform.position + new Vector3(Random.Range(-10f,2f),0.2f), Quaternion.identity);
                curMinion += 1;

                // Kasih tau kroconya siapa bosnya
                MinionBoss scriptKroco = krocoBaru.transform.Find("BaseEnemy").GetComponent<MinionBoss>();
                if (scriptKroco != null)
                {
                    scriptKroco.bosAtasan = this;
                }

                // 3. Efek Terlempar (Disembur dari bos)
                Rigidbody2D rbKroco = krocoBaru.GetComponent<Rigidbody2D>();
                if (rbKroco != null)
                {
                    // Pilih arah X acak (ke kiri atau ke kanan)
                    float arahX = Random.Range(-20f, 2f);

                    // Bikin vektor arah lemparan (X acak, Y sedikit ke atas biar melengkung)
                    Vector2 arahLempar = new Vector2(arahX, 1f).normalized;

                    // Gunakan linearVelocity persis kayak di script Player kamu
                    rbKroco.linearVelocity = arahLempar * kekuatanSembur;
                }
            }


        }
    }

    // Fungsi ini bakal dipanggil SAMA SI KROCO pas dia mati
    public void KenaDamage()
    {
        curMinion -= 1;
        currentHP--;
        print("Sisa HP Bos: " + currentHP);

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