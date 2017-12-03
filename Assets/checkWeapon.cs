using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkWeapon : MonoBehaviour {

   
    private int id_Arme;

  
    public List<GameObject> list_Arme = new List<GameObject>();

    [SerializeField]

	void Start () {

       

    }
	
	
	void Update () {

        if(transform.childCount > 0)
        {
            id_Arme = gameObject.GetComponentInChildren<ItemOnObject>().item.itemID;
        }
        else
        {
            id_Arme = 0;

            for (int i = 0; i < list_Arme.Count; i++)
            {

                list_Arme[i].SetActive(false);
            }
        }

        if (id_Arme == 1 && transform.childCount > 0)
        {
            for (int i = 0; i < list_Arme.Count; i++)
            {
                if (i == 0)
                {
                    list_Arme[i].SetActive(true);
                }
            }

        }

        if (id_Arme == 2 && transform.childCount > 0)
        {
            for (int i = 1; i < list_Arme.Count; i++)
            {
                if (i == 1)
                {
                    list_Arme[i].SetActive(true);
                }
            }

        }

    }
}
