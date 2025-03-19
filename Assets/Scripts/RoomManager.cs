using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public List<GameObject> ritualList = new List<GameObject>();
    public TextMeshProUGUI objectiveLeftText;

    [SerializeField] GameObject clearRoomDoor;
    [SerializeField] List<GameObject> traps = new List<GameObject>();

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
            WaitForSeconds wait = new WaitForSeconds(5);
            for (int i = 0; i < traps.Count; i++)
            {
                traps[i].SetActive(true);
                yield return wait;
                traps[i].SetActive(false);
                yield return wait;
            }
        }
    }
}
