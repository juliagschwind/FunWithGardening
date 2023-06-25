using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveCamera : MonoBehaviour
{
    private Vector3 position;
    private new Camera camera;
    private bool move;

    float bottom = -7;
    float top = 7;
    float left = -13;
    float right = 12;
    float zmax = 6;
    float zmin = 3;

    void Awake(){
        camera = GetComponent<Camera>();
    }

    void Update(){
        if(Input.touchCount == 1){
            //moving
            Touch t = Input.GetTouch(0);
            switch(t.phase){
                case TouchPhase.Began:
                    if(EventSystem.current.IsPointerOverGameObject(t.fingerId)){
                        move = false;
                    }else{
                        move = true;
                    }
                    position = camera.ScreenToWorldPoint((Vector3) t.position);
                    break;
                case TouchPhase.Moved: 
                    if(move){
                        Vector3 movement = position - camera.ScreenToWorldPoint((Vector3) t.position);
                        camera.transform.position += movement;

                        transform.position = new Vector3(
                            Mathf.Clamp(transform.position.x, left, right),
                            Mathf.Clamp(transform.position.y, bottom, top),
                            transform.position.z
                        );
                    }
                    break;
            }
        }
        if(Input.touchCount == 2){
            //zooms
            Touch finger1 = Input.GetTouch(0);
            Touch finger2 = Input.GetTouch(1);

            if(!EventSystem.current.IsPointerOverGameObject(finger1.fingerId)&&!EventSystem.current.IsPointerOverGameObject(finger2.fingerId)){
                Vector2 finger1prev = finger1.position - finger1.deltaPosition;
                Vector2 finger2prev = finger2.position - finger2.deltaPosition;

                float prevDistance = (finger1prev-finger2prev).magnitude;
                float curDistance = (finger1.position-finger2.position).magnitude;
                Zoom((curDistance-prevDistance)*0.01f);
            }
            

        }
    }

    void Zoom(float update){
        camera.orthographicSize = Mathf.Clamp(camera.orthographicSize-update, zmin, zmax);
    }
}
