using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Car : MonoBehaviour
{
    public GameObject Target;
    public float MaxSpeed;
    private float Speed;
    public float StopDistance;
    public string CarTag;
    public string LightControlTag;
    public string LightControlRightTag;
    public string LightControlLeftTag;
    public string MarkerLeftTag;
    public string MarkerRightTag;
    public int Direction;
    void Start()
    {
        Speed = MaxSpeed;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0, LoadSceneMode.Single);
        }

        if (Target != null)
        {
            DirectionControl();
        }
        SpeedControl();
        MoveToTarget();

    }

    void MoveToTarget()
    {
        this.transform.Translate(Vector3.forward * Speed * Time.deltaTime);
    }

    void DirectionControl()
    {
        this.transform.LookAt(Target.transform);
    }

    void SpeedControl()
    {
        int layerMask = 1 << 8;
        layerMask = ~layerMask;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.green);
            if(hit.transform.tag == LightControlTag)
            {
                if(hit.transform.GetComponent<LightControler>().State == 0 && Vector3.Distance(this.transform.position,hit.transform.position) <= StopDistance)
                {
                    Speed = 0;
                }
                else if(hit.transform.GetComponent<LightControler>().State == 1)
                {
                    Speed = MaxSpeed;
                }
            }


            if (hit.transform.tag == LightControlLeftTag)
            {
                if (hit.transform.GetComponent<LightControler>().State == 0 && Vector3.Distance(this.transform.position, hit.transform.position) <= StopDistance)
                {
                    Speed = 0;
                }
                else if (hit.transform.GetComponent<LightControler>().State == 1)
                {
                    Speed = MaxSpeed;
                }
            }


            if (hit.transform.tag == LightControlRightTag)
            {
                if (hit.transform.GetComponent<LightControler>().State == 0 && Vector3.Distance(this.transform.position, hit.transform.position) <= StopDistance)
                {
                    Speed = 0;
                }
                else if (hit.transform.GetComponent<LightControler>().State == 1)
                {
                    Speed = MaxSpeed;
                }
            }


            if (hit.transform.tag == CarTag)
            {
                if (Vector3.Distance(this.transform.position, hit.transform.position) <= StopDistance)
                {
                    Speed = 0;
                }
                else
                {
                    Speed = MaxSpeed;
                }
            }
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == LightControlTag)
        {
            Direction = Random.Range(0, 2); // 0 - left, 1 - right
            if (Direction == 0)
            {
                Target = other.gameObject.GetComponent<LightControler>().LeftMarker;
            }

            if (Direction == 1)
            {
                Target = other.gameObject.GetComponent<LightControler>().RightMarker;
            }

            if (Direction == 2)
            {

            }
        }

        if (other.tag == LightControlLeftTag)
        {
            Direction = 0;
            if (Direction == 0)
            {
                Target = other.gameObject.GetComponent<LightControler>().LeftMarker;
            }

            if (Direction == 1)
            {
                Target = other.gameObject.GetComponent<LightControler>().RightMarker;
            }
        }

        if (other.tag == LightControlRightTag)
        {
            Direction = 1;
            if (Direction == 0)
            {
                Target = other.gameObject.GetComponent<LightControler>().LeftMarker;
            }

            if (Direction == 1)
            {
                Target = other.gameObject.GetComponent<LightControler>().RightMarker;
            }
        }

        if (other.tag == MarkerLeftTag)
        {
            Target = null;
            this.transform.rotation = other.transform.rotation;
        }

        if (other.tag == MarkerRightTag)
        {
            Target = null;
            this.transform.rotation = other.transform.rotation;
        }
    }
}
