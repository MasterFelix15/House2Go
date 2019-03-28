using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarTeleportation : MonoBehaviour
{
    public AudioClip audioClip;
    public GameObject my_camera;
    public GameObject my_marker;
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
    }

    private void UpdateMarker() {
        Vector3 origin = my_camera.transform.position;
        Vector3 direction = my_camera.transform.forward;
        float range = 1000;
        RaycastHit hit;
        if (Physics.Raycast(origin, direction, out hit, range) && hit.point.y <0.01){
            // Debug.DrawRay(origin, direction, Color.green, 2, false);
            my_marker.transform.position = hit.point;
            my_marker.GetComponent<Renderer>().material.color = Color.green;
        } else {
            my_marker.GetComponent<Renderer>().material.color = Color.red;
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMarker();
        if (Input.GetMouseButtonDown(0) && my_marker.GetComponent<Renderer>().material.color == Color.green) {
            gameObject.transform.position = my_marker.transform.position;
        }
    }
}
