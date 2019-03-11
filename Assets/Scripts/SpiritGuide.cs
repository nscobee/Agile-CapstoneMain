using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritGuide : MonoBehaviour
{
    public List<Transform> pointsList = new List<Transform>();
    public float dialogueWait = 7f;
    public float speed = 5f;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FollowPoints(this.gameObject.transform, pointsList));
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    IEnumerator FollowPoints(Transform tutorialGhost, List<Transform> targets)
    {
        yield return new WaitForSeconds(dialogueWait);
        for (int i = 0; i < pointsList.Count; i++)
        {
            if (Vector2.Distance(tutorialGhost.position, pointsList[i].position) > 1f)
            {
                tutorialGhost.transform.position = Vector3.MoveTowards(tutorialGhost.position, pointsList[i].position, speed);
            }
            yield return new WaitForSecondsRealtime(3f);
        }
            

        yield return new WaitForSeconds(10f);
        Destroy(this.gameObject);
    }

}
