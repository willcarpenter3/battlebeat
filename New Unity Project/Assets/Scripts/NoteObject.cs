using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour
{

    public bool canBePressed;

    public KeyCode keyToPress;

    public NoteSpawner spawn;

    public GameObject hitEffect, goodEffect, perfectEffect, missEffect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keyToPress))
        {
            if (canBePressed)
            {
                gameObject.SetActive(false);


                //GameManager.instance.NoteHit();

                if(Mathf.Abs(5.5f - transform.position.x) > .25)
                {
                    Debug.Log("Hit");
                    GameManager.instance.NormalHit();
                    Instantiate(hitEffect, transform.position, hitEffect.transform.rotation);
                }
                else if (Mathf.Abs(5.5f - transform.position.x) > 0.05f)
                {
                    Debug.Log("Good");
                    GameManager.instance.GoodHit();
                    Instantiate(goodEffect, transform.position, goodEffect.transform.rotation);
                }
                else
                {
                    Debug.Log("Perfect");
                    GameManager.instance.PerfectHit();
                    Instantiate(perfectEffect, transform.position, perfectEffect.transform.rotation);
                }

                Destroy(gameObject);
            }
        }

        /*if(this.tag == "loop" && this.transform.position.x < 4.51 && this.transform.position.x > 4.49)
        {
            spawn.spawnNotes();
            gameObject.SetActive(false);
            Destroy(gameObject);
        }*/

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Activator" && this.tag != "loop")
        {
            canBePressed = true;
            GameManager.instance.totalNotes += 1;
        }

        if(other.tag == "delet")
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }

        /**if(other.tag == "Activator" && this.tag == "loop")
        {
            spawn.spawnNotes();
            gameObject.SetActive(false);
            Destroy(gameObject);
        }*/
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Activator" && this.tag != "loop")
        {
            canBePressed = false;

            GameManager.instance.NoteMissed();
            Instantiate(missEffect, transform.position, missEffect.transform.rotation);
        }
    }
}
