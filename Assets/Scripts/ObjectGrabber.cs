using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Events;

[RequireComponent(typeof(ARRaycastManager))]
public class ObjectGrabber : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Player _player;

    private ARRaycastManager _arRaycastManager;
    private List<ARRaycastHit> _hits = new List<ARRaycastHit>();

    public event UnityAction<CollectibleObject> Grabbed;

    private void Start()
    {
        _arRaycastManager = GetComponent<ARRaycastManager>();
        _player = GetComponent<Player>();
    }

    private void Update()
    {
        //ProcessTouch();
        ProcessClick();
    }

    public void ProcessTouch()
    {
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Vector2 grabPoint = Input.GetTouch(0).position;
                var ray = _camera.ScreenPointToRay(grabPoint);

                if (Physics.Raycast(ray, out RaycastHit raycastHit))
                {
                    if (raycastHit.collider.gameObject.TryGetComponent(out CollectibleObject grabbedObject))
                    {
                        GrabObject(grabbedObject);
                    }
                }
            }
        }
    }

    public void ProcessClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
                Vector2 grabPoint = Input.mousePosition;
                var ray = _camera.ScreenPointToRay(grabPoint);

                if (Physics.Raycast(ray, out RaycastHit raycastHit))
                {
                    if (raycastHit.collider.gameObject.TryGetComponent(out CollectibleObject grabbedObject))
                    {
                        GrabObject(grabbedObject);
                    }
                }         
        }
    }

    public void GrabObject(CollectibleObject grabbedObject)
    {
        Debug.Log("GrabObject "+ grabbedObject.Reward);
        _player.AddScore(grabbedObject.Reward);
        grabbedObject.GetCollected();
        Grabbed?.Invoke(grabbedObject);
    }
}
