using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FungusController : Entity
{
    void Start()
    {
        timeToLiveRemaining = DataManager.Instance.settings.Fungus_MaxLifespan;
        proliferationRate = DataManager.Instance.settings.Fungus_ProliferationRate;
        PopUpSelf(5f);
        actionMode = "eat-corpse";
    }

    //make decisions and live!
    protected override void LifeTic()
    {
        GameObject corpse = FindClosestByTag("Predator", 50, false, "dead");
        if (corpse == null) 
        {
            corpse = FindClosestByTag("Grazer", 50, false, "dead");
        }
        if (corpse != null)
        {
            //drain nearby corpses
            Consume(corpse.GetComponent<Entity>());
            TryReproduce();
        }
    }

    //handle Entity interactions
    private void OnCollisionEnter(Collision collision)
    {
        GameObject target = collision.collider.gameObject;
        if (target.CompareTag("Ground"))
        {
            isActive = true;  //enable normal activity after spawning
            return;
        }
        /*
        if (isActive && !isDead)
        {
            //Predator or Grazer corpse touches
            var entity = collision.collider.GetComponent<Entity>();
            if ((target.CompareTag("Predator") || target.CompareTag("Grazer")) && entity.isDead)
            {
                Consume(entity);
            }
        }
        */
    }
}
