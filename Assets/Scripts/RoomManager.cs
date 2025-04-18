using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviour
{
    public List<GameObject> ritualList = new List<GameObject>();
    public TextMeshProUGUI objectiveLeftText;
    private AudioSource audioSource;

    [SerializeField] GameObject clearRoomDoor;
    [SerializeField] List<GameObject> traps = new List<GameObject>();
    [SerializeField] AudioClip exitDoorSFX;

    [SerializeField] float timeToActivate;

    protected GameObject[] enemies;

    // Start is called before the first frame update
    void Awake()
    {
        StartCoroutine(Traps());    
        audioSource= GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Ritual();  
    }

    private void Ritual()
    {
        objectiveLeftText.text = ritualList.Count.ToString();

        switch (ritualList.Count)
        {
            case 0: DestroyAllPro(); clearRoomDoor.SetActive(true); audioSource.PlayOneShot(exitDoorSFX, .15f); break;
        }
    }

    private IEnumerator Traps()
    {
        while (true)
        {
            WaitForSeconds wait = new WaitForSeconds(timeToActivate);
            for (int i = 0; i < traps.Count; i++)
            {
                traps[i].SetActive(true);
                yield return wait;
                traps[i].SetActive(false);
                yield return wait;
            }
        }
    }

    public void LoadNextLevel()
    {
        int nextLevel = SceneManager.GetActiveScene().buildIndex+ 1;
        SceneManager.LoadSceneAsync(nextLevel);      
    }

    private void DestroyAllPro()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject ghost in enemies) Destroy(ghost);
    }
}
