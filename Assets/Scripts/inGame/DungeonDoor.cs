using UnityEngine;
using UnityEngine.UIElements;

public class DungeonDoor : MonoBehaviour
{

    public GameObject sonrakiOdaDugmesi;
    public GameObject kapýKilitliIconu;

    private bool oyuncuKapiEtkilesimi = false;

    private void Start()
    {
        sonrakiOdaDugmesi.SetActive(false);
        kapýKilitliIconu.SetActive(false);
    }

    private void Update()
    {
        if(oyuncuKapiEtkilesimi && Input.GetKeyDown(KeyCode.E))
        {
            sonrakiOdaDugmesi.SetActive(false);

            if (kapiTipi == KapiTipi.koridor)
            {               
                DungeonManager.Instance.GoToCorridor();
                oyuncuKapiEtkilesimi = false;
            }

            else if (kapiTipi == KapiTipi.siradakiOda)
            {        
                DungeonManager.Instance.GoToNextRoom();
                oyuncuKapiEtkilesimi = false;
            }
        }
    }

    public enum KapiTipi
    {
        koridor, 
        siradakiOda  
    }

    public KapiTipi kapiTipi;
    public bool kilitliMi = true; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Player") && !kilitliMi)
        {
            oyuncuKapiEtkilesimi = true;
            sonrakiOdaDugmesi.SetActive(true);

        }

        else if (other.CompareTag("Player") && kilitliMi)
        {
            kapýKilitliIconu.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            sonrakiOdaDugmesi.SetActive(false);
            kapýKilitliIconu.SetActive(false);
        }
    }

    public void KilidiAc()
    {
        kilitliMi = false;
    }
}