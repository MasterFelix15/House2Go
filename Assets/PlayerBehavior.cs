using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    private GameObject my_camera;
    private GameObject position_marker;
    private GameObject object_marker;
    private GameObject point_marker;
    public GameObject object_menu;
    private RaycastHit hit;
    private GameObject selected;
    public int numOfPages;
    private int pageNum;
    private int state;
    
    // Start is called before the first frame update
    void Start()
    {
        my_camera = GameObject.Find("Main Camera");
        position_marker = GameObject.Find("PositionMarker");
        object_marker = GameObject.Find("ObjectMarker");
        point_marker = GameObject.Find("PointMarker");
        // object_menu = GameObject.Find("Object Menu");
        Cursor.visible = false;
        state = -1;
        pageNum = 0;
    }

    private void UpdateMarkers() {
        Vector3 origin = my_camera.transform.position;
        Vector3 direction = my_camera.transform.forward;
        float range = 1000;
        if (Physics.Raycast(origin, direction, out hit, range)){
            // Debug.DrawRay(origin, direction, Color.green, 2, false);
            if ( hit.point.y <0.01) {
                // ray collide with floor
                state = 0;
                position_marker.SetActive(true);
                position_marker.transform.position = hit.point;
                position_marker.GetComponent<Renderer>().material.color = Color.green;
                point_marker.SetActive(false);
            } else {
                // ray collide with object or wall
                point_marker.SetActive(true);
                point_marker.transform.position = hit.point;
                if (hit.transform.gameObject.layer == 10) {
                    state = 1;
                    point_marker.GetComponent<Renderer>().material.color = Color.green;
                } else if (hit.transform.gameObject.layer == 11) {
                    state = 2;
                    point_marker.GetComponent<Renderer>().material.color = Color.blue;
                } else if (hit.transform.gameObject.layer == 12) {
                    state = 3;
                    point_marker.GetComponent<Renderer>().material.color = Color.blue;
                } else {
                    state = 4;
                    point_marker.GetComponent<Renderer>().material.color = Color.red;
                }
                position_marker.SetActive(false);
            }
        }
    }

    private void Select(GameObject go) {
        if (selected != null) {
            selected.GetComponent<ObjectBehavior>().SetSelect(false);
        }
        selected = go;
        selected.GetComponent<ObjectBehavior>().SetSelect(true);
        object_marker.transform.parent = selected.transform;
        object_marker.GetComponent<Renderer>().material.color = Color.green;
        object_marker.transform.localPosition = new Vector3(0,1,0);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMarkers();
        if (Input.GetMouseButtonDown(0)) {
            switch (state) {
                case 0: // ray collide with floor
                    gameObject.transform.position = position_marker.transform.position;
                    break;
                case 1: // ray collide with object
                    Select(hit.transform.gameObject);
                    break;
                case 2: // ray collide with menu option
                    GameObject newObject = GameObject.Instantiate(hit.transform.parent.Find("model").gameObject);
                    newObject.transform.localScale = new Vector3(1,1,1);
                    newObject.AddComponent<ObjectBehavior>();
                    Select(newObject);
                    newObject.GetComponent<ObjectBehavior>().SetPositionMarker(position_marker);
                    newObject.GetComponent<ObjectBehavior>().AttachToMarker();
                    break;
                case 3: // ray collide with menu button
                    GameObject buttonHit = hit.transform.parent.gameObject;
                    object_menu.GetComponent<MenuBehavior>().Apply(buttonHit.name);
                    break;
            }
        }
        if (Input.GetKeyDown(KeyCode.F)) {
            object_menu.SetActive(!object_menu.activeSelf);
            Vector3 camera_facing = my_camera.transform.forward;
            object_menu.transform.localPosition = 3.5f * new Vector3(camera_facing.x, 0, camera_facing.z) + new Vector3(0, 2.1f, 0);
            object_menu.transform.LookAt(my_camera.transform.position);
        }
        if (Input.GetKeyDown(KeyCode.R)) {
            object_marker.transform.parent = transform.parent;
            object_marker.transform.position = new Vector3(0,0,0);
            if (selected != null) {
                Destroy(selected);
            }
        }
    }
}
