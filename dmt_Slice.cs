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
        private float Value = 0f;

        // Anzahl der Bilder
        private int steps = 0;

        private List<dmt_Layer> layerList= new List<dmt_Layer>();
        public int activeLayer = 0;



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

        public void addLayer(string layerName)
        {
           
            dmt_Layer tLayer=  this.gameObject.AddComponent<dmt_Layer>();
            tLayer.set(layerName, axis);
            layerList.Add(tLayer);
            
            // Layer 0 als Default
            setLayerActive(0);
           
           
            

        }

        public void setLayerActive(int layerToActivate)
        {
            activeLayer = layerToActivate;
            steps = layerList[layerList.Count-1].steps;
            
        }


        public void showSlice(Vector3 Val, bool debug = false)
        {
            switch (axis)
            {
                case AXIS.X:
                    Value = Val.x;
                    Val = new Vector3(Val.x, 0f, 0f);
                    break;
                case AXIS.Y:
                    Value = Val.y;
                    Val = new Vector3(0f, Val.y, 0f);
                    break;
                case AXIS.Z:
                    Value = Val.z;
                    Val = new Vector3(0f, Val.z, 0f);
                    break;
                default: break;
            }

            // Slice nicht im Kopf (kleiner 0  oder grösser 1 => sliceObject nicht darstellen
            if (Value > 0 && Value <= 1f)
            {
                sliceObject.gameObject.SetActive(true);
                //planes sind ursprünglich 10 Einheiten groß
                sliceObject.localPosition = Origin + Val * 10f; 
                
                // passendes Bild finden
                var i = Mathf.RoundToInt(Value * (steps - 1));
                if (debug) text.text = Value.ToString() + "\n" + i.ToString();
                sliceObject.GetComponent<Renderer>().material.mainTexture = layerList[activeLayer].Images[i];
            }
            else sliceObject.gameObject.SetActive(false);
            
            
            

        }
    }
}