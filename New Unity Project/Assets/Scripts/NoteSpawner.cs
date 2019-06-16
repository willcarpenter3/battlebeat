using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{ 
    public int offset;
    public GameObject downArrow;
    public GameObject upArrow;
    public GameObject specialLeft;
    public GameObject pattern1;
    public GameObject pattern2;
    public GameObject pattern3;

    public GameObject nh;

    public void spawnNotes()
    {
        int rand = Random.Range(1, 4);

        switch (rand)
        {
            case 1:
                Pattern1();
                break;
            case 2:
                Pattern2();
                break;
            case 3:
                Pattern3();
                break;
            default:
                Pattern1();
                break;
        }
    }

    private void Pattern1()
    {
        /**GameObject up = Instantiate(upArrow, new Vector3(2f, 0.5f, 0f), upArrow.transform.rotation);
        GameObject down = Instantiate(downArrow, new Vector3(1f, 0.5f, 0f), downArrow.transform.rotation);
        //GameObject special = Instantiate(specialLeft, new Vector3(2.45f, 1.5f, 0f), specialLeft.transform.rotation);
        up.transform.parent = nh.transform;
        down.transform.parent = nh.transform;
        /**special.transform.parent = nh.transform;
        NoteObject no = special.GetComponent<NoteObject>();
        no.spawn = this;*/
        GameManager.instance.numBeats = 4;
        GameObject pattern = Instantiate(pattern1, new Vector3(0f, 0f, 0f), Quaternion.identity);
        pattern.transform.parent = nh.transform;

    }

    private void Pattern2()
    {
        /**GameObject up = Instantiate(upArrow, new Vector3(1f, 0.5f, 0f), upArrow.transform.rotation);
        GameObject down = Instantiate(downArrow, new Vector3(2f, 0.5f, 0f), downArrow.transform.rotation);
        //GameObject special = Instantiate(specialLeft, new Vector3(2.45f, 1.5f, 0f), specialLeft.transform.rotation);
        up.transform.parent = nh.transform;
        down.transform.parent = nh.transform;
        /**special.transform.parent = nh.transform;
        NoteObject no = special.GetComponent<NoteObject>();
        no.spawn = this;*/
        GameManager.instance.numBeats = 4;
        GameObject pattern = Instantiate(pattern2, new Vector3(0f, 0f, 0f), Quaternion.identity);
        pattern.transform.parent = nh.transform;
    }

    private void Pattern3()
    {
        GameManager.instance.numBeats = 8;
        GameObject pattern = Instantiate(pattern3, new Vector3(0f, 0f, 0f), Quaternion.identity);
        pattern.transform.parent = nh.transform;
    }
}
