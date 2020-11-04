using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightControler: MonoBehaviour
{
    public GameObject LeftMarker;
    public GameObject RightMarker;
    public GameObject Lamp;
    public string LampTag;
    public Material Material;
    public float TimeChange = 10;
    public float TimeRemaining;
    public int State;
    void Start()
    {
        Transform lamp = this.transform.Find(LampTag);
        Lamp = lamp.gameObject;
        TimeRemaining = TimeChange;
        Material = Lamp.GetComponent<MeshRenderer>().material;
        State = Random.Range(0, 2);
        ChangeColor();
    }

    void Update()
    {
        Timer();
    }

    void ChangeColor()
    { 
        if(State == 0)
        {
            State = 1;
            Material.color = Color.green;
        }else if(State == 1)
        {
            State = 0;
            Material.color = Color.red;
        }

    }


    void Timer()
    {
        if (TimeRemaining > 0)
        {
            TimeRemaining -= Time.deltaTime;
        }
        else
        {
            ChangeColor();
            TimeRemaining = TimeChange;
        }
    }

}
