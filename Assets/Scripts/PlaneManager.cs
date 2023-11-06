using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
public class PlaneManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private ARPlaneManager arPlaneManager;
     [SerializeField] private GameObject model3DPrefab;
     private List<ARPlane> planes = new List<ARPlane>();
     private GameObject model3dPlane;

     private void OnEnable(){
        arPlaneManager.planesChanged += PlanesFound;
     }

     private void OnDisable(){
        arPlaneManager.planesChanged -= PlanesFound;

     }

     private void PlanesFound(ARPlanesChangedEventArgs planeData){
        if(planeData.added != null && planeData.added.Count>0){
            planes.AddRange(planeData.added);
        }
        foreach(var plane in planes){
            if(plane.extents.x * plane.extents.y>=0.4 && model3dPlane==null){
                model3dPlane = Instantiate(model3DPrefab);
                float yOffSet = model3dPlane.transform.localScale.y/2;
                model3dPlane.transform.position = new Vector3(plane.center.x,plane.center.y+yOffSet, plane.center.z);
                model3dPlane.transform.forward = plane.normal;
                StopPlaneDetection();
            }
        }
     }

     public void StopPlaneDetection(){
        //arPlaneManager.requestedDetectionMode = PlaneDetectionMode.None;
   foreach(var plane in arPlaneManager.trackables){
        plane.gameObject.SetActive(false);
   }
     }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
