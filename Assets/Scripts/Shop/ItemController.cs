using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemController : MonoBehaviour
{

    [SerializeField] List<GameObject> tienda = new List<GameObject>();
    [SerializeField] List<GameObject> inv = new List<GameObject>();
    [SerializeField] float separationX;
    [SerializeField] float initialSpaceX;
    [SerializeField] float separationY;
    [SerializeField] float initialSpaceY;
    [SerializeField] int itemsPorLinea;

    [SerializeField] float invSeparationX;
    [SerializeField] float invInitialSpaceX;
    [SerializeField] float invSeparationY;
    [SerializeField] float invInitialSpaceY;
    [SerializeField] int invItemsPorLinea;

    // Use this for initialization
    void Start()
    {
        adjust();
        //itemsPorLinea--;
        //invItemsPorLinea--;
    }

    void adjust()
    {
        foreach(GameObject obj in tienda)
        {
            obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(initialSpaceX + separationX * (tienda.IndexOf(obj) % itemsPorLinea), initialSpaceY + separationY * (Mathf.Floor(tienda.IndexOf(obj) / itemsPorLinea)));
        }
        foreach(GameObject obj in inv)
        {
            obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(invInitialSpaceX + invSeparationX * (inv.IndexOf(obj) % invItemsPorLinea), invInitialSpaceY + invSeparationY * (Mathf.Floor(inv.IndexOf(obj) / invItemsPorLinea)));
        }
    }

    void transferToShop(GameObject obj)
    {
        inv.Remove(obj);
        tienda.Add(obj);
        adjust();
    }

    void addToInv(GameObject obj)
    {
        tienda.Remove(obj);
        inv.Add(obj);
        adjust();
    }

    public void transfer(GameObject obj)
    {
        if (inv.Contains(obj))
        {
            transferToShop(obj);
        }
        else
        {
            addToInv(obj);
        }
    }
}

