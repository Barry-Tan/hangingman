using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class GameScript : MonoBehaviour
{
    public Text timeField;
    private float time;
    //private string[] wordsLocal = {"TTMATT","JOANNE","ROBBERT","MARRY JANE" };
    private int[] myNums = { 3, 5, 7, 8 };
    public Text wordToFindField;
    private string chosenWord;
    private string hiddenWord;
    public GameObject[] hangMan;
    private int fails;
    public GameObject winText;
    public GameObject loseTest;
    private bool gameEnd = false;
    private string[] words = File.ReadAllLines(@"Assets/Words.txt");
    public GameObject replayButton;


    // Start is called before the first frame update
    void Start()
    {

        chosenWord = words[Random.Range(0, words.Length)];
        wordToFindField.text = chosenWord;
        

        for (int i = 0; i < chosenWord.Length; i++) {
            char letter = chosenWord[i];
            if (char.IsWhiteSpace(letter))
            {
                hiddenWord += " ";
            }
            else {
                hiddenWord += "_";
            }
        }

        wordToFindField.text = hiddenWord;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameEnd == false) {
            time += Time.deltaTime;
            timeField.text = time.ToString();
        }
        
    }

    private void OnGUI() {
        Event e = Event.current;
        if (e.type == EventType.KeyDown && e.keyCode.ToString().Length == 1) {
            string pressedLetter = e.keyCode.ToString();
            Debug.Log("Keydown "+pressedLetter);

            if (chosenWord.Contains(pressedLetter))
            {
                int rightIndex = chosenWord.IndexOf(pressedLetter);
                while (rightIndex != -1) {
                   hiddenWord = hiddenWord.Substring(0, rightIndex) + pressedLetter+hiddenWord.Substring(rightIndex+1);
                   chosenWord = chosenWord.Substring(0, rightIndex) + "_" + chosenWord.Substring(rightIndex+1);
                    Debug.Log(hiddenWord);
                    rightIndex = chosenWord.IndexOf(pressedLetter);
                }

                wordToFindField.text = hiddenWord;
            }// add a hang man body part
            else {
                hangMan[fails].SetActive(true);
                fails++;
                
            }

            if (fails == hangMan.Length) {
                loseTest.SetActive(true);
                replayButton.SetActive(true);
                gameEnd = true;
            }

            if (!hiddenWord.Contains("_")) {
                winText.SetActive(true);
                gameEnd = true;
            }
        }
    }
}
