using System.Collections;
using System.Collections.Generic;
using Meta.XR.MRUtilityKit;
using UnityEngine;

public class MRUKController : MonoBehaviour
{
    [SerializeField] private MRUK mruk;
    [SerializeField] private OVRInput.Controller controller;
    [SerializeField] private GameObject prefabObject;

    private bool sceneLoaded = false;
    private MRUKRoom currentRoom;
    private List<GameObject> wallAnchorObjects = new();

    private void OnEnable()
    {
        mruk.RoomCreatedEvent.AddListener(OnRoomCreated);
    }

    private void OnDisable()
    {
        mruk.RoomCreatedEvent.RemoveListener(OnRoomCreated);
    }

    public void mrukEnabled()
    {
        sceneLoaded = true;
    }

    private void OnRoomCreated(MRUKRoom room)
    {
        currentRoom = room;
    }

    private void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, controller) && currentRoom && sceneLoaded)
        { 
            foreach (var wallAnchor in currentRoom.WallAnchors)
            {
                var wallAnchorObject = Instantiate(prefabObject, Vector3.zero, Quaternion.identity, wallAnchor.transform);
                wallAnchorObject.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
                wallAnchorObjects.Add(wallAnchorObject);
            }
        }
    }
}
