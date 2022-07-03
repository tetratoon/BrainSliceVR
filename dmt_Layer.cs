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
        
        public List<Texture2D> Images;
        
        
        //die drei Slice-objekte
        private dmt_Slice sliceX, sliceY, sliceZ;
        private dmt_Slice[] sliceList;

        void Start()
        {
            Images = new List<Texture2D>();
            
           
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
            setSlice(dmt_Slice.AXIS.X, ref stepsX);
            setSlice(dmt_Slice.AXIS.Y, ref stepsY);
            setSlice(dmt_Slice.AXIS.Z, ref stepsZ);
            
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
        
        
        
        private void setSlice(dmt_Slice.AXIS axis, ref int steps)
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
                Debug.Log(name+"/"+axis+"/"+i);
                Debug.Log(ImagesX.Count);
                //ImagesX.Add(Resources.Load<Texture2D>(name+"/"+axis+"/"+i));
                Debug.Log(i);
                i++;
            }while (!isEnd);

            steps = i;

        }
        
        
        

        //Layer ist für die Slices verantwortlich
        public void updateSlices(Vector3 v)
        {
            Debug.Log("updateSlice");
            //sliceX.showSlice(ImagesX[0]);
        }
      
    }
}