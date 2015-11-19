using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

public class DialogScript : MonoBehaviour {
  private static GameObject dialogue,dialogueImage;
  private static Text Dialogue;
  //private static List<string> dialogueSentences;
  private string csvfile= "dialogues.csv"; 
  private static string [] allLines; 
  private static string [] line;
  // Use this for initialization
  void Start () {
    dialogueImage = GameObject.Find("DialogueImage");
    dialogue = GameObject.Find("Dialogue");
    Dialogue = dialogue.GetComponent<Text>();
    dialogueImage.GetComponent<Image>().enabled = false;
    allLines = File.ReadAllLines(Application.dataPath+"/" +csvfile);


  }
	
  // Update is called once per frame
  void Update () {
    /*if (count == 0)
      StartCoroutine(DialogueStart("3")); 
      count =1;*/
  } 

  //public static void AddDialogue(string s){ dialogueSentences.Add(s);}
	 
  public static IEnumerator DialogueStart(string scene, GameObject o) 
  {
    dialogueImage.GetComponent<Image>().enabled = true; 
    yield return new WaitForSeconds (2f);

    foreach(var l in allLines)
    {  
      line = Regex.Split(l,",(?! )");
      //print(line[0]);
      if(line[0]== scene)
      {
        print(line[3]);
        Dialogue.text += line[2]+": ";
        foreach(var wrd in line[3].Split(' '))
        {
          foreach(var chr in wrd)
          {
            Dialogue.text += chr;
            yield return new WaitForSeconds (.05f);
          }

          yield return new WaitForSeconds (.05f);
          Dialogue.text += " ";
        }

        yield return new WaitForSeconds (3f);
        Dialogue.text = " ";
      }
    }

    //Dialogue.text += " ";
    
    dialogueImage.GetComponent<Image>().enabled = false;
    Destroy(o, .1f);
  }
}


