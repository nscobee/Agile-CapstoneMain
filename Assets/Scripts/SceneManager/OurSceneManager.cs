using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OurSceneManager : MonoBehaviour
{
  public string firstScene;
  private string oldScene = null;

  public GameObject phantomPrefab;

  public GameObject pointsManager;

  private void Start()
  {
    if (firstScene != "")
    {
      SceneManager.LoadScene(firstScene, LoadSceneMode.Additive);

      oldScene = firstScene;

    }

  }

  public void LoadNewSceneAdditive(string newScene)
  {
    SceneManager.LoadScene(newScene, LoadSceneMode.Additive);

    if (oldScene != null)
    {
      SceneManager.UnloadSceneAsync(oldScene);

    }

    oldScene = newScene;

  }

  /**
 * What is does is takes in a newScene and a savePoint
 * after taht it loads the new scene then finds the point is supposed to load into
 * after it does that it creates a player and moves them to that location
 * 
 */

  public void LoadFromSaveNew(string newScene, int savePoint)
  {
    // loads new scene
    LoadNewSceneAdditive(newScene);

    StartCoroutine(ImSadThisIsTheOnlyWayICouldGetThisToWork(savePoint));

  }

  /** DONT MEME OPEN INSIDE
         ##    ###    ##    ## ##    ##       ###     ######     ######## ##     ##  ######  ##    ## 
         ##   ## ##   ###   ## ##   ##       ## ##   ##    ##    ##       ##     ## ##    ## ##   ##  
         ##  ##   ##  ####  ## ##  ##       ##   ##  ##          ##       ##     ## ##       ##  ##   
         ## ##     ## ## ## ## #####       ##     ##  ######     ######   ##     ## ##       #####    
   ##    ## ######### ##  #### ##  ##      #########       ##    ##       ##     ## ##       ##  ##   
   ##    ## ##     ## ##   ### ##   ##     ##     ## ##    ##    ##       ##     ## ##    ## ##   ##  
    ######  ##     ## ##    ## ##    ##    ##     ##  ######     ##        #######   ######  ##    ## 
   */
  // make a co co rutine that waits for a few frames then tries to make a charecter and set them to a location
  IEnumerator ImSadThisIsTheOnlyWayICouldGetThisToWork(int savePoint)
  {
    yield return new WaitForSeconds(.1F);

    GameObject savePointGameObject = GameObject.FindGameObjectWithTag("SavePoints");

    LevelSavePoints savePointComponent = savePointGameObject.GetComponent<LevelSavePoints>();

    Transform savePointTransform = savePointComponent.GetPositonOfPoint(savePoint);

    if (GameObject.FindGameObjectsWithTag("Player").Length > 0)
    {
      foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
      {
        Destroy(player);

      }
    }

    GameObject phantom = Instantiate(phantomPrefab, savePointTransform.position, new Quaternion(), savePointTransform);

    phantom.transform.parent = null;


  }


  public void LoadFromTransition(string newScene, int transitionPoint, GameObject player)
  {
    LoadNewSceneAdditive(newScene);

    // TODO: get vector2 from transitionPoint
    Transform transitionPointLocation = GameObject.FindGameObjectWithTag("TransitionPoints").GetComponent<LevelEntryPoints>().GetPositonOfPoint(transitionPoint);

    player.transform.position = transitionPointLocation.position;


  }

}