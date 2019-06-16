using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public AudioSource theMusic;

    public bool startPlaying;

    public BeatScroller theBS;

    public static GameManager instance;

    public int currentScore;
    public int enemyScore;
    public int scorePerNote;
    public int scorePerGoodNote;
    public int scorePerPerfectNote;
    public int numPotions;
    public int healthPerPotion;
    public int numBeats;

    public int currentMultiplier;
    public int multiplierTracker;
    public int[] multiplierThresholds;

    public int specialMeter;

    public Text scoreText;
    public Text multiText;
    public Text potionsText;
    public Text enemyText;
    public Text specialText;

    public float totalNotes;
    public float normalHits;
    public float goodHits;
    public float perfectHits;
    public float missedHits;

    public GameObject resultsScreen;
    public Text percentHitText, normalsText, goodsText, perfectsText, missesText, rankText, finalScoreText;

    public KeyCode startKey;
    public KeyCode healKey;

    public NoteSpawner ns;
    

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        scoreText.text = "Party HP: 100";
        multiText.text = "Risk Multiplier: x1";
        currentMultiplier = 1;
        currentScore = 100;
        enemyScore = 1000;
        scorePerNote = 5;
        scorePerGoodNote = 10;
        scorePerPerfectNote = 15;
        numPotions = 5;
        healthPerPotion = 10;
        totalNotes = 0;
        numBeats = 2;
        specialMeter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!startPlaying)
        {
            if (Input.GetKeyDown(startKey))
            {
                startPlaying = true;
                theBS.hasStarted = true;

                theMusic.Play();

                StartCoroutine(Spawn());
            }
        } else if (Input.GetKeyDown(healKey) && numPotions > 0 && currentScore < 100)
        {
            numPotions--;
            currentScore += healthPerPotion;
            scoreText.text = "Party HP: " + currentScore;
            potionsText.text = "Potions: " + numPotions;
        }

        //End game if enemy health is 0
        if(enemyScore == 0 && !resultsScreen.activeInHierarchy)
        {
            theMusic.Stop();
            theBS.hasStarted = false;
            resultsScreen.SetActive(true);
            normalsText.text = "" + normalHits;
            goodsText.text = goodHits.ToString();
            perfectsText.text = perfectHits.ToString();
            missesText.text = "" + missedHits;

            float totalHit = normalHits + goodHits + perfectHits;
            float percentHit = (totalHit / totalNotes) * 100f;
            percentHitText.text = percentHit.ToString("F1") + "%";

            string rankVal = "F";
            if (percentHit > 60)
            {
                rankVal = "D";
                if (percentHit > 70)
                {
                    rankVal = "C";
                    if (percentHit > 80)
                    {
                        rankVal = "B";
                        if (percentHit > 90)
                        {
                            rankVal = "A";
                            if (percentHit > 98)
                            {
                                rankVal = "S";
                            }
                        }
                    }
                }
            }
            rankText.text = rankVal;

            finalScoreText.text = currentScore.ToString();
        }

        if (currentScore == 0 && !resultsScreen.activeInHierarchy)
        {
            theMusic.Stop();
            theBS.hasStarted = false;
            resultsScreen.SetActive(true);
            normalsText.text = "You are dead";
            goodsText.text = "Not big surprise";
            perfectsText.text = perfectHits.ToString();
            missesText.text = "" + missedHits;

            float totalHit = normalHits + goodHits + perfectHits;
            float percentHit = (totalHit / totalNotes) * 100f;
            percentHitText.text = percentHit.ToString("F1") + "%";

            string rankVal = "F";
            if (percentHit > 60)
            {
                rankVal = "D";
                if (percentHit > 70)
                {
                    rankVal = "C";
                    if (percentHit > 80)
                    {
                        rankVal = "B";
                        if (percentHit > 90)
                        {
                            rankVal = "A";
                            if (percentHit > 98)
                            {
                                rankVal = "S";
                            }
                        }
                    }
                }
            }
            rankText.text = rankVal;

            finalScoreText.text = currentScore.ToString();
        }
    }

    public void NoteHit()
    {
        Debug.Log("Hit On Time");

        if (currentMultiplier - 1 < multiplierThresholds.Length)
        {

            multiplierTracker++;

            if (multiplierThresholds[currentMultiplier - 1] <= multiplierTracker)
            {
                multiplierTracker = 0;
                currentMultiplier++;
            }
        }

        multiText.text = "Risk Multiplier: x" + currentMultiplier;

        //enemyScore -= scorePerNote * currentMultiplier;
        enemyText.text = "Enemy HP: " + enemyScore;


    }

    public void NormalHit()
    {
        AddSpecial(1);
        enemyScore = Mathf.Max(enemyScore- scorePerNote * currentMultiplier, 0);
        normalHits += 1;
        NoteHit();
    }

    public void GoodHit()
    {
        AddSpecial(2);
        enemyScore = Mathf.Max(enemyScore - scorePerGoodNote * currentMultiplier, 0);
        goodHits += 1;
        NoteHit();
    }

    public void PerfectHit()
    {
        AddSpecial(3);
        enemyScore = Mathf.Max(enemyScore - scorePerPerfectNote * currentMultiplier, 0);
        perfectHits += 1;
        NoteHit();
    }

    public void NoteMissed()
    {
        Debug.Log("Missed Note");
        currentScore = Mathf.Max(currentScore - scorePerNote * currentMultiplier, 0);
        scoreText.text = "Party HP: " + currentScore;
        currentMultiplier = 1;
        multiplierTracker = 0;
        multiText.text = "Risk Multiplier: x" + currentMultiplier;
        missedHits += 1;
        
    }

    public void AddSpecial(int amt)
    {
        specialMeter = Mathf.Min(specialMeter + amt, 100);
        specialText.text = "Special Meter: " + specialMeter;
    }

    IEnumerator Spawn()
    {
        float repeatTime = 1 / (theBS.beatTempo / numBeats);

        //InvokeRepeating("Spawn", 0f, repeatTime);
        yield return new WaitForSecondsRealtime(repeatTime);
        ns.spawnNotes();
        StartCoroutine(Spawn());

    }
}
