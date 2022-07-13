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
        public float AspectRatio = 1f;
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
            
            //TODO nicht mit namen sondern aus Verzeichnis alle Unterverzeichnisse
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

            LayerList[activeLayer].updateSlices(v);
            
        }

        public bool setActiveLayer(int _activeLayer)
        {
            if (_activeLayer<LayerList.Count )
            {
                activeLayer = _activeLayer;
                LayerList[activeLayer].ApplySettings();
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
                        
                        LayerList[activeLayer].ChangeActiveSlice(dmt_Slice.AXIS.AXIAL);
                        break;
                    case(Key.S): 
                        
                        LayerList[activeLayer].ChangeActiveSlice(dmt_Slice.AXIS.SAG);
                        break;
                    case(Key.C): 
                        
                        LayerList[activeLayer].ChangeActiveSlice(dmt_Slice.AXIS.COR);
                        break;
                    //operator
                    case(Key.F): 
                       
                        LayerList[activeLayer].ChangeSettings(_flip:true);
                        break;
                    case(Key.O): 
                       
                        LayerList[activeLayer].ChangeSettings(_order:true);
                        break;
                    case(Key.R): 
                        
                        LayerList[activeLayer].ChangeSettings(_rot:true);
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

