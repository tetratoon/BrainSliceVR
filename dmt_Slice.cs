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

        // Anzahl der Bilder
        private int steps = 0;
       


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

      

        public void showSlice(Texture2D texture, Vector3 v)
        {
     
            gameObject.SetActive(true);
            sliceObject.GetComponent<Renderer>().material.mainTexture = texture;
            sliceObject.gameObject.SetActive(true);
            //planes sind ursprünglich 10 Einheiten groß
            sliceObject.localPosition = Origin + v * 10f;
            
        }

        public void hideSlice()
        {
            gameObject.SetActive(false);
        }
        
    }
    
}