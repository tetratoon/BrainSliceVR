using System.Collections.Generic;
using UnityEngine;
using DMT_Icon.DMT_Slice;

namespace DMT_Icon.DMT_Slice
{
    
    public class dmt_Layer:MonoBehaviour
    {

        public string name;
        //public List<Texture2D> Images;
        public int stepsX ,stepsY,stepsZ = 0;
        //public dmt_Slice.AXIS axis;
        public List<Texture2D> ImagesX;
        public List<Texture2D> ImagesY;
        public List<Texture2D> ImagesZ;
        //public int steps = 0;
        
        //public List<Texture2D> Images;
        
        
        //die drei Slice-objekte
        private dmt_Slice sliceX, sliceY, sliceZ;
        //private dmt_Slice[] sliceList;

        void Start()
        {
            //Images = new List<Texture2D>();
            
           
        }
        //die drei Achsen  
       
        // public dmt_layerSlice sliceX ;
        // public dmt_layerSlice sliceY ;
        // public dmt_layerSlice sliceZ ;
        public void setLayer(string _name)
        {
            ImagesX = new List<Texture2D>();
            ImagesY = new List<Texture2D>();
            ImagesZ = new List<Texture2D>();
            Debug.Log("setLayer: ");
            this.name = _name;
            setSlice(dmt_Slice.AXIS.X, ImagesX, ref stepsX);
            setSlice(dmt_Slice.AXIS.Y, ImagesY,ref stepsY);
            setSlice(dmt_Slice.AXIS.Z,ImagesZ, ref stepsZ);
            
            sliceX = GameObject.FindWithTag("sliceX").GetComponent<dmt_Slice>();
            sliceX.axis = dmt_Slice.AXIS.X;
            sliceY = GameObject.FindWithTag("sliceY").GetComponent<dmt_Slice>();
            sliceY.axis = dmt_Slice.AXIS.Y;
            sliceZ = GameObject.FindWithTag("sliceZ").GetComponent<dmt_Slice>();
            sliceZ.axis = dmt_Slice.AXIS.Z;
            // sliceX =  new dmt_layerSlice();
            // sliceX.set( name,dmt_Slice.AXIS.X);
            // sliceY = new dmt_layerSlice();
            // sliceY.set( name,dmt_Slice.AXIS.Y);
            // sliceZ = new dmt_layerSlice();
            // sliceZ.set( name,dmt_Slice.AXIS.Z);

        }
        
        
        
        private void setSlice(dmt_Slice.AXIS axis,List<Texture2D> _Images, ref int steps)
        {
            
            
            //iList = new List<Texture2D>();
            int i = 0;
            bool isEnd = false;
            do
            {
                if (Resources.Load<Texture2D>(name+"/"+axis+"/"+i) == null)
                {
                    Debug.Log(name+"/"+axis+"/"+i);
                    isEnd = true;
                    break;
                }
                //Debug.Log(name+"/"+axis+"/"+i);
                //Debug.Log(ImagesX.Count);
                //Debug.Log("_ImagesX.Count "+ _ImagesX.Count);
                _Images.Add(Resources.Load<Texture2D>(name+"/"+axis+"/"+i));
                //Debug.Log(i);
                i++;
            }while (!isEnd);

            steps = i;

        }
        
        
        

        //Layer ist für die Slices verantwortlich
        public void updateSlices(Vector3 v)
        {

            
            var valx =new Vector3(v.x, 0f, 0f);
            var valy =new Vector3(0f, v.y, 0f);
            var valz =new Vector3(0f, v.z, 0f);

            //var i = Mathf.RoundToInt(v.x * (stepsX - 1));
            
            //Debug.Log("updateSlice");
            sliceX.showSlice(ImagesX[Mathf.RoundToInt(v.x * (stepsX - 1))], valx );
            sliceY.showSlice(ImagesY[Mathf.RoundToInt(v.y * (stepsX - 1))], valy);
            sliceZ.showSlice(ImagesZ[Mathf.RoundToInt(v.z * (stepsX - 1))], valz);
        }
        
        
        /*
         public void showSlice_old(Vector3 Val,  bool debug = false)
        {
            Vector3 p = Val;
            switch (axis)
            {
                case AXIS.X:
                    Value = Val.x;
                    Val = new Vector3(Val.x, 0f, 0f);
                    //steps = layerList[activeLayer].sliceX.steps;
                    break;
                case AXIS.Y:
                    Value = Val.y;
                    Val = new Vector3(0f, Val.y, 0f);
                    //steps = layerList[activeLayer].sliceY.steps;
                    break;
                case AXIS.Z:
                    Value = Val.z;
                    Val = new Vector3(0f, Val.z, 0f);
                    //steps = layerList[activeLayer].sliceZ.steps;
                    break;
                default: break;
            }

            // Slice nicht im Kopf (kleiner 0  oder grösser 1 => sliceObject nicht darstellen
            if (p.x > 0 && p.y > 0 &&p.z > 0 && p.x <= 1f && p.y <= 1f && p.z <= 1f)
            {
                sliceObject.gameObject.SetActive(true);
                //planes sind ursprünglich 10 Einheiten groß
                sliceObject.localPosition = Origin + Val * 10f; 
                
                // passendes Bild finden
                var i = Mathf.RoundToInt(Value * (steps - 1));
                if (debug) text.text = Value.ToString() + "\n" + i.ToString();
                switch (axis)
                {
                    case AXIS.X:
                        //sliceObject.GetComponent<Renderer>().material.mainTexture =
                            //layerList[activeLayer].sliceX.Images[i];
                        break;
                    case AXIS.Y:
                        //sliceObject.GetComponent<Renderer>().material.mainTexture =
                            //layerList[activeLayer].sliceY.Images[i];
                        break;
                    case AXIS.Z:
                        //sliceObject.GetComponent<Renderer>().material.mainTexture =
                            //layerList[activeLayer].sliceZ.Images[i];
                        break;
                }
            }
            else sliceObject.gameObject.SetActive(false);
            
            
            

        }
    */    
      
    }
}