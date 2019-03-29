using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public AudioClip audioClip;
    public GameObject my_camera;
    public GameObject position_marker;
    public GameObject object_marker;
    public GameObject point_marker;
    private RaycastHit hit;
    private GameObject selected;
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
    }

    private void UpdateMarkers() {
        Vector3 origin = my_camera.transform.position;
        Vector3 direction = my_camera.transform.forward;
        float range = 1000;
        if (Physics.Raycast(origin, direction, out hit, range)){
            // Debug.DrawRay(origin, direction, Color.green, 2, false);
            if ( hit.point.y <0.01) {
                // ray collide with floor
                position_marker.SetActive(true);
                position_marker.transform.position = hit.point;
                position_marker.GetComponent<Renderer>().material.color = Color.green;
                point_marker.SetActive(false);
            } else {
                // ray collide with object or wall
                point_marker.SetActive(true);
                point_marker.transform.position = hit.point;
                if (hit.transform.gameObject.layer == 10) {
                    point_marker.GetComponent<Renderer>().material.color = Color.green;
                } else {
                    point_marker.GetComponent<Renderer>().material.color = Color.red;
                }
                position_marker.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMarkers();
        if (Input.GetMouseButtonDown(0)) {
            if (position_marker.activeSelf) {
                gameObject.transform.position = position_marker.transform.position;
            } else if (point_marker.activeSelf && point_marker.GetComponent<Renderer>().material.color == Color.green) {
                if (selected != null) {
                    selected.GetComponent<ObjectBehavior>().SetSelect(false);
                }
                selected = hit.transform.gameObject;
                selected.GetComponent<ObjectBehavior>().SetSelect(true);
                object_marker.transform.parent = selected.transform;
                object_marker.GetComponent<Renderer>().material.color = Color.green;
                object_marker.transform.localPosition = new Vector3(0,1,0);
            }
        }
    }
}
