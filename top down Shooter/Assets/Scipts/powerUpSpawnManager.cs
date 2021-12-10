using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerUpSpawnManager : MonoBehaviour
{
    public GameObject prefab;
    public float timeSpawn = 10f;
    [SerializeField] BoxCollider2D bc;

    private Vector2 cubeSize;
    private Vector2 cubeCenter;

    private void Awake(){
        Transform cubeTrans = bc.GetComponent<Transform>();
        cubeCenter = cubeTrans.position;

        cubeSize.x = cubeTrans.localScale.x * bc.size.x;
        cubeSize.y = cubeTrans.localScale.y * bc.size.y;
    }

    private void Start(){
        StartCoroutine(waitSpawn());
    }

    IEnumerator waitSpawn(){
        yield return new WaitForSeconds(timeSpawn - convert((scoreManager.getScore() + 1) * Time.deltaTime, 0, 1, 0.1f, timeSpawn - 1f));
        spawn();
    }

    private float convert(float value, float oldMin, float oldMax, float newMin, float newMax){
        return (((value - oldMin) * (newMax - newMin)) / (oldMax - oldMin)) + newMin;
    }

    void spawn(){
        GameObject enm;
        enm = Instantiate(prefab, GetRandomPosition(), Quaternion.identity);
        enm.transform.parent = this.transform;
        StartCoroutine(waitSpawn());
    }

    private bool checkPositionInCamera(Vector2 pos){
        Vector2 inCameraVector = Camera.main.ViewportToWorldPoint(pos);
        if ((inCameraVector.x >= 0 && inCameraVector.x <= 1) && (inCameraVector.y >= 0 && inCameraVector.y <= 1))
            return true;
        return false;
    }


    private Vector2 GetRandomPosition(){
        Vector2 randomPosition;
        do
        {
            randomPosition = new Vector2(Random.Range(-cubeSize.x / 2, cubeSize.x / 2), Random.Range(-cubeSize.y / 2, cubeSize.y / 2));
        } while (checkPositionInCamera(randomPosition));

        return cubeCenter + randomPosition;
    }
}
