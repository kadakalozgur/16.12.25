using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonManager : MonoBehaviour
{
    public static DungeonManager Instance;

    public GameObject canBari;
    public GameObject coin;
    public CanvasGroup fadeCanvasGroup;
    public float solmaSuresi = 0.5f;

    public Transform playerTransform;
    public Transform corridorSpawnPoint;
    public static int secilenKaleID = 0;

    [System.Serializable]
    public struct CastleSetup
    {
        public string kaleAdi;
        public List<Transform> odaSpawnPoints;
        public Transform bossOdasiSpawnPoint;
    }

    public CastleSetup[] kaleler;

    private List<Transform> gidilecekOdalarListesi;
    private int currentCastleIndex = 0;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += SeviyeYuklemesiTamamlandi;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= SeviyeYuklemesiTamamlandi;
    }

    void SeviyeYuklemesiTamamlandi(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "DungeonScene")
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                playerTransform = playerObj.transform;

            StartCoroutine(StartDungeonRoutine(secilenKaleID));
        }
    }

    IEnumerator StartDungeonRoutine(int castleID)
    {
        if (fadeCanvasGroup != null) fadeCanvasGroup.alpha = 1f;

        yield return new WaitForSeconds(0.1f);

        currentCastleIndex = castleID;

        if (kaleler != null && kaleler.Length > castleID)
        {
            gidilecekOdalarListesi = new List<Transform>(kaleler[castleID].odaSpawnPoints);
        }

        if (playerTransform != null)
        {
            playerTransform.position = new Vector3(652.3f, 250.5f, 0f);

            Rigidbody2D rb = playerTransform.GetComponent<Rigidbody2D>();
            if (rb != null) rb.velocity = Vector2.zero;
        }

        yield return StartCoroutine(FadeRoutine(1f, 0f));
    }

    public void GoToCorridor()
    {
        StartCoroutine(TransitionRoutine("Corridor"));
    }

    public void GoToNextRoom()
    {
        StartCoroutine(TransitionRoutine("NextRoom"));
    }

    IEnumerator TransitionRoutine(string hedef)
    {
        if (canBari) canBari.SetActive(false);
        if (coin) coin.SetActive(false);

        yield return StartCoroutine(FadeRoutine(0f, 1f));


        if (playerTransform != null)
        {
            Vector3 targetPosition = Vector3.zero;
            bool isTargetFound = false;

            if (hedef == "Corridor")
            {
                if (corridorSpawnPoint != null)
                {
                    targetPosition = corridorSpawnPoint.position;
                    isTargetFound = true;
                }
            }
            else if (hedef == "NextRoom")
            {

                if (gidilecekOdalarListesi != null && gidilecekOdalarListesi.Count > 0)
                {
                    int rastgeleIndex = Random.Range(0, gidilecekOdalarListesi.Count);

                    targetPosition = gidilecekOdalarListesi[rastgeleIndex].position;
                    isTargetFound = true;

                    gidilecekOdalarListesi.RemoveAt(rastgeleIndex);
                }
                else
                {
                    if (kaleler[currentCastleIndex].bossOdasiSpawnPoint != null)
                    {
                        targetPosition = kaleler[currentCastleIndex].bossOdasiSpawnPoint.position;
                        isTargetFound = true;
                    }
                }
            }

            if (isTargetFound)
            {
                playerTransform.position = new Vector3(targetPosition.x, targetPosition.y, 0f);

                Rigidbody2D rb = playerTransform.GetComponent<Rigidbody2D>();
                if (rb != null) rb.velocity = Vector2.zero;
            }
        }

        yield return new WaitForSeconds(0.2f);

        if (canBari) canBari.SetActive(true);
        if (coin) coin.SetActive(true);

        yield return StartCoroutine(FadeRoutine(1f, 0f));
    }

    IEnumerator FadeRoutine(float startAlpha, float endAlpha)
    {
        if (fadeCanvasGroup == null) yield break;

        float gecenZaman = 0f;
        while (gecenZaman < solmaSuresi)
        {
            gecenZaman += Time.deltaTime;
            fadeCanvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, gecenZaman / solmaSuresi);
            yield return null;
        }
        fadeCanvasGroup.alpha = endAlpha;
    }

    // Boss öldüðünde çýkýþ kristalini aktif etme
    /*
    public exitDungeon1 cikisKristali1;
    public exitDungeon2 cikisKristali2;
    
    public void bossOldurulduMu()
    {
        cikisKristali1.ActivateCrystal();
        cikisKristali2.ActivateCrystal();
    }
    */
}