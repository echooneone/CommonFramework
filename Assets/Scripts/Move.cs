using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float Accuracy=1;
    public float Speed = 10;
    public float RotSpeed = 10;
    public GameObject[] WayPoints;
    private Vector3 goal;

    private int currentIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        goal = transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        // RaycastHit hit;
        // Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        // if (Physics.Raycast(ray, out hit) && Input.GetMouseButton(0))
        // {
        //     goal = new Vector3(hit.point.x, transform.position.y, hit.point.z);
        // }

        goal = WayPoints[currentIndex].transform.position;
        if (Vector3.Distance(transform.position, goal) > Accuracy)
        {
            Vector3 distance = goal - transform.position;
            transform.rotation=Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(distance), Time.deltaTime * RotSpeed);
            
            transform.Translate(0,0, Speed*Time. deltaTime);
        }
        else
        {
            if (currentIndex < WayPoints.Length - 1)
            {
                currentIndex++;
            }
            else
            {
                currentIndex = 0;
            }
        }
    }


}
