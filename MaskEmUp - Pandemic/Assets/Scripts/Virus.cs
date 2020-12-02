using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Virus : MonoBehaviour
{
    public float timeToSpread = 5f;
    public LayerMask layer;
    public GameObject enemyMananger;
    // Chance of infection being successful
    public float spreadChance = 0.6f;
    public float infectRadius = 2f;


    void Start()
    {
        InvokeRepeating("Infect", timeToSpread, timeToSpread);
    }

    void Infect()
    {
        RaycastHit[] hits = SpreadVirus(infectRadius, layer.value);
        for(int i = 0; i < hits.Length; i++)
        {
            // For each affected enemy in raycast range
            // If enemy is no infected, not protected and its spread chance OK
            Enemy currentNpc = hits[i].transform.GetComponent<Enemy>();
            if(!currentNpc.GetIsInfected() && Random.Range(0, 10) < 10 * spreadChance && !currentNpc.GetIsProtected())
            {
                currentNpc.gameObject.GetComponent<Virus>().enabled = true;
                currentNpc.SetIsInfected(true);
                currentNpc.Particles();
                // Remove the current npc from array
                enemyMananger.GetComponent<VirusSpread>().RemoveInfected(hits[i].transform.gameObject);
            }
        }
    }

    public RaycastHit[] SpreadVirus(float infectRadius, int layerMask)
    {
        return Physics.SphereCastAll(transform.position, 
        infectRadius, 
        transform.position, 
        infectRadius, 
        layerMask, 
        QueryTriggerInteraction.UseGlobal);
    }
}
