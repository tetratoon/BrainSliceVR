using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace DMT_Icon.DMT_Slice
{
    public class dmt_HeadController : MonoBehaviour
    {
        
       
        //breite zu höhe
        public float AspectRatio = 0.5f;

       
        public Transform Head;
        //DEBUG
        public bool debug = true;
        public Transform DebugCenter;

        public Vector3 DebugInputPoint;
        // end DEBUG

        
        private GameObject headObject;
        private Vector3 headPos;
        
        
 //Die Layer 
        private List<dmt_Layer> LayerList;
        [SerializeField]
        public string[] LayerNames; //TODO automatisch
        public int activeLayer = 0;
        

        void Start()
        {
            if ((headObject = GameObject.FindWithTag("HEAD")) == null) throw new Exception("no Headobject found");
            headPos = headObject.transform.position;
            
            Vector3 t = headObject.transform.localScale;
            t.y *= AspectRatio;
            headObject.transform.localScale = t;
            
            LayerList = new List<dmt_Layer>();
            
            //TODO nicht mit namen sonder aus Verzeichnis alle Unterverzeichnisse
            for (int i = 0; i < LayerNames.Length; i++)
            {
                var tLayer = new dmt_Layer();
                tLayer.initLayer(LayerNames[i]);
                Debug.Log("new layer: " + tLayer.settings.name + "   " + i);
                LayerList.Add(tLayer);
                Debug.Log("LayerList: " + LayerList[LayerList.Count-1].settings.name);

            }
        }

        


        void Update()
        {
            DebugInputPoint = DebugCenter.position;
            var v = CalculateRelativePosition(DebugInputPoint);
//            Debug.Log("activeLayer: "+activeLayer+"  v: "+ v.ToString());
            LayerList[activeLayer].updateSlices(v);
            
        }

        public bool setActiveLayer(int _activeLayer)
        {
            if (_activeLayer<LayerList.Count )
            {
                activeLayer = _activeLayer;
                Debug.Log("active Layer = "+activeLayer);
                return true;
            }

            else
            {
                Debug.Log("active Layer = "+activeLayer);
                return false;
            }
        }

        public void getInput(InputAction.CallbackContext cc)
        {
            if (cc.canceled)
            {
                var buttonPressed = (KeyControl) cc.control;
                switch (buttonPressed.keyCode)
                {
                    //Slice
                    case(Key.A): 
                        Debug.Log("A");
                        break;
                    case(Key.S): 
                        Debug.Log("S");
                        break;
                    case(Key.C): 
                        Debug.Log("C");
                        break;
                    //operator
                    case(Key.F): 
                        Debug.Log("F");
                        break;
                    case(Key.O): 
                        Debug.Log("O");
                        break;
                    case(Key.R): 
                        Debug.Log("R");
                        break;
                    case(Key.UpArrow):
                        Debug.Log("a...active Layer = "+activeLayer);
                        if (!setActiveLayer(activeLayer + 1))
                        {
                            Debug.Log("b...active Layer = "+activeLayer);
                            setActiveLayer(0); //letzter layer erreicht => start
                        }
                
                        Debug.Log("UA");
                        break;
                    default: break;
                }
            }
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

