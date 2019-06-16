using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public GameObject[] slots;
    public int[] amounts;
    public KeyCode switchKey;
    public KeyCode useKey;
    GameObject displayInstance;
    int currentIndex;
    public Text itemAmountText;

    // Start is called before the first frame update
    void Start()
    {
        currentIndex = 0;
        displayInstance = Instantiate(slots[currentIndex], this.transform.position, Quaternion.identity);
        displayInstance.transform.parent = this.transform;
        itemAmountText.text = "Item Amount: " + amounts[currentIndex];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(switchKey))
        {
            currentIndex++;
            if (currentIndex >= slots.Length)
            {
                currentIndex = 0;
            }
            Destroy(displayInstance);
            displayInstance = Instantiate(slots[currentIndex], this.transform.position, Quaternion.identity);
            displayInstance.transform.parent = this.transform;
            itemAmountText.text = "Item Amount: " + amounts[currentIndex];
        }

        if (Input.GetKeyDown(useKey))
        {
            if (amounts.Length > 0)
            {
                if (amounts[currentIndex] > 1)
                {
                    amounts[currentIndex]--;
                }
                else
                {
                    /*for (int i = currentIndex; i < slots.Length - 1; i++)
                    {
                        slots[i] = slots[i + 1];
                        amounts[i] = amounts[i + 1];
                    }
                    slots[slots.Length - 1] = null;
                    amounts[amounts.Length - 1] = 0;*/
                    amounts = RemoveIndices<int>(amounts, currentIndex);
                    slots = RemoveIndices<GameObject>(slots, currentIndex);
                    currentIndex++;
                    if (currentIndex >= slots.Length)
                    {
                        currentIndex = 0;
                    }
                    Destroy(displayInstance);
                    if (amounts.Length > 0)
                    {
                        
                        displayInstance = Instantiate(slots[currentIndex], this.transform.position, Quaternion.identity);
                        displayInstance.transform.parent = this.transform;
                    }
                    //itemAmountText.text = "Item Amount: " + amounts[currentIndex];
                }
                if (amounts.Length > 0)
                {
                    itemAmountText.text = "Item Amount: " + amounts[currentIndex];
                }
                else
                {
                    itemAmountText.text = "OUT OF ITEMS!!!";
                }
            }
        }
    }

    private T[] RemoveIndices<T>(T[] IndicesArray, int RemoveAt)
    {
        T[] newIndicesArray = new T[IndicesArray.Length - 1];

        int i = 0;
        int j = 0;
        while (i < IndicesArray.Length)
        {
            if (i != RemoveAt)
            {
                newIndicesArray[j] = IndicesArray[i];
                j++;
            }

            i++;
        }

        return newIndicesArray;
    }
}
