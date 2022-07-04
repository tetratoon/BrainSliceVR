using System.Collections;
using System.Collections.Generic;
using DMT_Icon.DMT_Slice;
using TMPro;
using UnityEngine;

namespace DMT_Icon.DMT_Slice
{
    public class dmt_Slice : MonoBehaviour
    {
        public TMP_Text text;
        public Transform sliceObject;
        private Vector3 Origin; //nullpunkt 
        //private float Value = 0f;

        // Anzahl der Bilder
        private int steps = 0;

        //private List<dmt_Layer> layerList= new List<dmt_Layer>();
        //public int activeLayer = 0;



        public AXIS axis;

        public enum AXIS
        {
            X,
            Y,
            Z
        }

        void Start()
        {
            sliceObject = this.transform.Find("SlicePlane");
            Origin = sliceObject.localPosition;
        }

        // public void addLayer(string layerName)
        // {
        //
        //     Debug.Log("addlayer");
        //     dmt_Layer tLayer = new dmt_Layer();
        //     //tLayer.set(layerName);
        //     layerList.Add(tLayer);
        //     
        //     // Layer 0 als Default
        //     setLayerActive(0);
        //    
        //    
        //     
        //
        // }

        // public void setLayerActive(int layerToActivate)
        // {
        //     activeLayer = layerToActivate;
        //     //steps = layerList[layerList.Count-1].steps;
        //     
        // }

        public void showSlice(Texture2D texture, Vector3 v)
        {
            Debug.Log(v.ToString());
            if (v.x >= 0 && v.y >= 0 && v.z >= 0 && v.x <= 1f && v.y <= 1f && v.z <= 1f)
            {
                sliceObject.GetComponent<Renderer>().material.mainTexture = texture;
                sliceObject.gameObject.SetActive(true);
                //planes sind ursprünglich 10 Einheiten groß
                sliceObject.localPosition = Origin + v * 10f;
            }
        }

        // public void showSlice_old(Vector3 Val,  bool debug = false)
        // {
        //     Vector3 p = Val;
        //     switch (axis)
        //     {
        //         case AXIS.X:
        //             Value = Val.x;
        //             Val = new Vector3(Val.x, 0f, 0f);
        //             //steps = layerList[activeLayer].sliceX.steps;
        //             break;
        //         case AXIS.Y:
        //             Value = Val.y;
        //             Val = new Vector3(0f, Val.y, 0f);
        //             //steps = layerList[activeLayer].sliceY.steps;
        //             break;
        //         case AXIS.Z:
        //             Value = Val.z;
        //             Val = new Vector3(0f, Val.z, 0f);
        //             //steps = layerList[activeLayer].sliceZ.steps;
        //             break;
        //         default: break;
        //     }
        //
        //     // Slice nicht im Kopf (kleiner 0  oder grösser 1 => sliceObject nicht darstellen
        //     if (p.x > 0 && p.y > 0 &&p.z > 0 && p.x <= 1f && p.y <= 1f && p.z <= 1f)
        //     {
        //         sliceObject.gameObject.SetActive(true);
        //         //planes sind ursprünglich 10 Einheiten groß
        //         sliceObject.localPosition = Origin + Val * 10f; 
        //         
        //         // passendes Bild finden
        //         var i = Mathf.RoundToInt(Value * (steps - 1));
        //         if (debug) text.text = Value.ToString() + "\n" + i.ToString();
        //         switch (axis)
        //         {
        //             case AXIS.X:
        //                 //sliceObject.GetComponent<Renderer>().material.mainTexture =
        //                     //layerList[activeLayer].sliceX.Images[i];
        //                 break;
        //             case AXIS.Y:
        //                 //sliceObject.GetComponent<Renderer>().material.mainTexture =
        //                     //layerList[activeLayer].sliceY.Images[i];
        //                 break;
        //             case AXIS.Z:
        //                 //sliceObject.GetComponent<Renderer>().material.mainTexture =
        //                     //layerList[activeLayer].sliceZ.Images[i];
        //                 break;
        //         }
        //     }
        //     else sliceObject.gameObject.SetActive(false);
        //     
        //     
        //     
        //
        // }
    }
}