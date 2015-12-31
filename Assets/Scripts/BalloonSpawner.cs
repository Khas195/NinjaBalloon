using UnityEngine;
using System.Collections;

public class BalloonSpawner : MonoBehaviour {

    [SerializeField]
    ObjectPool balloonPool;

    [SerializeField]
    float timePerSpawn;

    float counter = 0;

    [SerializeField]
    Sprite[] balloonSprite;
	// Use this for initialization
    void Awake()
    {
        counter += timePerSpawn;
    }
	void Start () {
        if (balloonPool == null)
        {
            this.gameObject.SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (counter < Time.time)
        {
            GameObject balloon = balloonPool.RequestObject();
            if (balloon == null)
            {
                Debug.Log("Balloon object return null");
            }
        }
	    
	}
}
