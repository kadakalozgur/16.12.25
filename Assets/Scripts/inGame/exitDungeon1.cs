using UnityEngine;
using UnityEngine.UIElements;

public class exitDungeon1 : MonoBehaviour
{
    public GameObject exitDungeonButton;

    public GameObject kristalGörseli;
    public Collider2D crystalCollider;

    private bool oyuncuKristalEtkileþimi = false;

    public bool bossOlduMu = false; // Boss ölünce bunu true yapacaz

    private bool aktifMi = false;

    // Þu anlýk burasý böyle kalsýn boss eklendiðinde düzeltiriz
    private void Start()
    {
        exitDungeonButton.SetActive(false);

        if (bossOlduMu) 
        { 
            ActiveCrystal(); 
        }

        else
        {
            kristalGörseli.SetActive(false);
            crystalCollider.enabled = false;
        }
    }

    private void Update()
    {

        if (bossOlduMu)
        {
            ActiveCrystal();
        }

        if (aktifMi && oyuncuKristalEtkileþimi && Input.GetKeyDown(KeyCode.E))
        {
            ExitDungeon();
        }
    }

    public void ActiveCrystal()
    {
        aktifMi = true;
        kristalGörseli.SetActive(true);
        crystalCollider.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && bossOlduMu)
        {
            oyuncuKristalEtkileþimi = true;
            exitDungeonButton.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            oyuncuKristalEtkileþimi = false;
            exitDungeonButton.SetActive(false);
        }
    }

    private void ExitDungeon()
    {
        exitDungeonButton.SetActive(false);
        oyuncuKristalEtkileþimi = false;
        spawnPlayer.karakterinSonKonumu = "outCastle1";
        StartCoroutine(FindObjectOfType<loadingScreen>().showLoadingScreen("GameScene"));
    }
}