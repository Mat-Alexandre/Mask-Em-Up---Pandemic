using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusSpread : MonoBehaviour
{
    public float infectedPercent = 0.1f;
    public GameObject gameMananger;
    public List<GameObject> npcsNotInfected;

    // Start is called before the first frame update
    void Awake()
    {
        npcsNotInfected = new List<GameObject>();
        SetInfectedChild();
    }

    void Update()
    {
        if(npcsNotInfected.Count == 0)
        {
            gameMananger.GetComponent<GameMananger>().GameOverVirus();
        }
    }

    void SetInfectedChild()
    {
        int infected = Mathf.FloorToInt(transform.childCount * infectedPercent) + 1;
        int count = 0;

        foreach(Transform child in transform)
        {
            if(count < infected && Random.Range(0, 10) > 5)
            {
                child.GetComponent<Virus>().enabled = true;
                child.GetComponent<Enemy>().SetIsInfected(true);
                child.GetComponent<Enemy>().Particles();

                count++;
            }else
            {
                npcsNotInfected.Add(child.gameObject);
            }
        }
    }

    public void RemoveInfected(GameObject other)
    {
        if(npcsNotInfected.Contains(other))
        {
            npcsNotInfected.Remove(other);
        }
    }
}
