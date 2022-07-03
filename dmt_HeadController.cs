using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DMT_Icon.DMT_Slice
{
    public class dmt_HeadController : MonoBehaviour
    {

        //DEBUG
        public bool debug = true;
        public Transform DebugCenter;

        public Vector3 DebugInputPoint;
        // end DEBUG

        //public float headScale = 0.05f;
        private GameObject headObject;
        private Vector3 headPos;
        
        
 //Die Layer 
        public string[] LayerNames;
        private int activeLayer = 0;
        public List<dmt_Layer> LayerList;


        void Start()
        {
            if ((headObject = GameObject.FindWithTag("HEAD")) == null) throw new Exception("no Headobject found");
            headPos = headObject.transform.position;
            
            LayerList = new List<dmt_Layer>();
            for (int i = 0; i < LayerNames.Length; i++)
            {
                var tLayer = new dmt_Layer();
                tLayer.setLayer(LayerNames[i]);
                Debug.Log("new layer: " + tLayer.name + "   " + i);
                LayerList.Add(tLayer);
                Debug.Log("LayerList: " + LayerList.ToString());

            }
        }

        /*
        //die drei Slice-objekte
        private dmt_Slice sliceX, sliceY, sliceZ;
        private dmt_Slice[] sliceList;

       

        void Start()
        {
            // Dummy im unteren Eck des Kopfes zur Positionierung 
            if ((headObject = GameObject.FindWithTag("HEAD")) == null) throw new Exception("no Headobject found");
            headPos = headObject.transform.position;
            sliceX = GameObject.FindWithTag("sliceX").GetComponent<dmt_Slice>();
            sliceX.axis = dmt_Slice.AXIS.X;
            sliceY = GameObject.FindWithTag("sliceY").GetComponent<dmt_Slice>();
            sliceY.axis = dmt_Slice.AXIS.Y;
            sliceZ = GameObject.FindWithTag("sliceZ").GetComponent<dmt_Slice>();
            sliceZ.axis = dmt_Slice.AXIS.Z;

            sliceList = new[] {sliceX, sliceY, sliceZ};

            for (int i = 0; i < LayerNames.Length; i++)
            {
                var tLayer = new dmt_Layer();
                tLayer.set(LayerNames[i]);
                LayerList.Add(tLayer);
                // foreach (var slice in sliceList)
                // {
                //     slice.addLayer(LayerNames[i]);
                // }
            }
            
            //TODO testen und aktivieren
            
            
            // foreach (var slice in sliceList)
            // {
            //     slice.setLayerActive(activeLayer);
            // }
            
            
        }
        */


        void Update()
        {
            DebugInputPoint = DebugCenter.position;
            var v = CalculateRelativePosition(DebugInputPoint);
Debug.Log("activeLayer: "+activeLayer);
            LayerList[activeLayer].updateSlices(v);
            // foreach (var slice in sliceList)
            // {
            //     slice.showSlice(v, debug);
            // }

        }

        private Vector3 CalculateRelativePosition(Vector3 point)
        {
            var t = DebugInputPoint - headPos;
            t.x /= headObject.transform.localScale.x;
            t.y /= headObject.transform.localScale.y;
            t.z /= headObject.transform.localScale.z;
            return t / 10f; //planes sind 10 Einheiten groß
        }
    }
}

