using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkinManager : MonoBehaviour
{
    public GameObject currentTop;
    public GameObject currentBottom;

    public PlayerAnimController playeranim;

    public void ChangeTop(GameObject newShirt)
    {
        if(newShirt== currentTop)
        {
            Destroy(currentTop);
            return;
        }

        if (currentTop != null)
        {
            Destroy(currentTop);
        }
        currentTop =  Instantiate(newShirt,transform.position,Quaternion.identity,transform);
        currentTop.GetComponent<PlayerAnimController>().ChangeAnimationState(playeranim.currentAnimState);
    }
    public void ChangeBottom(GameObject newShirt)
    {
        if (newShirt == currentBottom)
        {
            Destroy(currentBottom);
            return;
        }

        if (currentBottom != null)
        {
            Destroy(currentBottom);
        }
        currentBottom = Instantiate(newShirt, transform.position, Quaternion.identity, transform);
        print("curr ani" + playeranim.currentAnimState);
        currentBottom.GetComponent<PlayerAnimController>().ChangeAnimationState(playeranim.currentAnimState);
    }
}
