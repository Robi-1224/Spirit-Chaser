using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviour
{
    public List<GameObject> ritualList = new List<GameObject>();
    public TextMeshProUGUI objectiveLeftText;

    [SerializeField] GameObject clearRoomDoor;
    [SerializeField] List<GameObject> traps = new List<GameObject>();

    [SerializeField] float timeToActivate;
    [SerializeField] int nextRoomNumber;

    private GameObject[] ghost;

    
    // Start is called before the first frame update
    void Awake()
    {
        StartCoroutine(Traps());
        ghost = GameObject.FindGameObjectsWithTag("Enemy");
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
            case <= 0 :foreach(GameObject ghost in ghost) Destroy(ghost); clearRoomDoor.SetActive(true); break;
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
        SceneManager.LoadScene("Room " + nextRoomNumber.ToString());
    }
}
