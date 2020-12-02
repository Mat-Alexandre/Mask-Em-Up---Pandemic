using System.Collections;
using System.Collections.Generic;
using static GameOptions;
using UnityEngine;

public class RenderSorting : MonoBehaviour
{
    private int sortingOrderBase = 6000;
    private string bodyPart;
    private Renderer myRender;

    void Awake()
    {
        myRender = gameObject.GetComponent<Renderer>();
        bodyPart = gameObject.name;
    }

    void LateUpdate()
    {
        int offSet;
        if(GameOptions.layerSort.TryGetValue(bodyPart, out offSet))
        {
            myRender.sortingOrder = (int)(sortingOrderBase - (transform.position.z * 100) + offSet);
        }
    }
}
