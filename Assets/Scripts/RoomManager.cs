using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public List<GameObject> ritualList = new List<GameObject>();
    [SerializeField] GameObject clearRoomDoor;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Traps());
    }

    // Update is called once per frame
    void Update()
    {
        Ritual();
    }

    private void Ritual()
    {
        switch(ritualList.Count)
        {
            case <= 0 : clearRoomDoor.SetActive(true); break;
        }
    }

    private IEnumerator Traps()
    {
        while (true)
        {
            WaitForSeconds wait = new WaitForSeconds(1);


            yield return wait;
        }
    }
}
