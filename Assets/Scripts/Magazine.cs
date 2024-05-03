using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Magazine : MonoBehaviour
{
    [Header("Magazine Properties")]
    public int magazineCapacity = 1;
    public int bulletsRemaining = 0;

    // Start is called before the first frame update
    void Start()
    {
        if(magazineCapacity < bulletsRemaining)
        {
            magazineCapacity = bulletsRemaining;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool DispenseBullet()
    {
        if(bulletsRemaining > 0)
        {
            bulletsRemaining--;
            return true;
        }

        return false;
    }

    public void ReloadBullet()
    {
        if (bulletsRemaining < magazineCapacity)
        {
            bulletsRemaining++;
        }
    }
}
