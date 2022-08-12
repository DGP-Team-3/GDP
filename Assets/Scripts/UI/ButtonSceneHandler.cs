using UnityEngine;

public class ButtonSceneHandler : MonoBehaviour
{

    //////////////////////////////////////////
    ///
    ///
    public void RequestLoadFeedingMiniGame()
    {
        if (GameManager.Instance == null) return;

        GameManager.Instance.LoadFeedingMiniGame();
    }    
    
    //////////////////////////////////////////
    ///
    ///
    public void RequestLoadEntertainmentMiniGame()
    {
        if (GameManager.Instance == null) return;

        GameManager.Instance.LoadEntertainmentMiniGame();
    }
}
