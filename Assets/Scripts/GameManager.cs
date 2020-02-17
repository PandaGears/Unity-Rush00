
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject CompleteLevelUI;
    public GameObject GenocideLevelUI;
    public GameObject LostLevelUI;
    
    void start(){
        CompleteLevelUI.SetActive(false);
        LostLevelUI.SetActive(false);
        GenocideLevelUI.SetActive(false);
    }
    public void CompleteLevel(){
        CompleteLevelUI.SetActive(true);
    }
     public void GenocideLevel(){
        GenocideLevelUI.SetActive(true);
    }
        public void LostLevel(){
        LostLevelUI.SetActive(true);
    }
    public void endgame(){
        Debug.Log("END IT");
    }
    
}
