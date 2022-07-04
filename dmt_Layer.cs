using System.Collections.Generic;
using UnityEngine;
using DMT_Icon.DMT_Slice;

namespace DMT_Icon.DMT_Slice
{
    
    public class dmt_Layer:MonoBehaviour
    {

        //public string name;
        
        public int stepsX ,stepsY,stepsZ = 0;
       
        public List<Texture2D> ImagesX;
        public List<Texture2D> ImagesY;
        public List<Texture2D> ImagesZ;
       
        
        //die drei Slice-objekte
        private dmt_Slice sliceX, sliceY, sliceZ;

        public LayerSettings settings;
      

       
        public void setLayer(string _name)
        {
            settings = new LayerSettings();
            settings.name = _name;
            
            ImagesX = new List<Texture2D>();
            ImagesY = new List<Texture2D>();
            ImagesZ = new List<Texture2D>();
            Debug.Log("setLayer: ");
            //this.name = _name;
            
            setSlice(dmt_Slice.AXIS.X, ImagesX, ref settings.xSteps);
            setSlice(dmt_Slice.AXIS.Y, ImagesY,ref settings.ySteps);
            setSlice(dmt_Slice.AXIS.Z,ImagesZ, ref settings.zSteps);
            
            sliceX = GameObject.FindWithTag("sliceX").GetComponent<dmt_Slice>();
            sliceX.axis = dmt_Slice.AXIS.X;
            sliceY = GameObject.FindWithTag("sliceY").GetComponent<dmt_Slice>();
            sliceY.axis = dmt_Slice.AXIS.Y;
            sliceZ = GameObject.FindWithTag("sliceZ").GetComponent<dmt_Slice>();
            sliceZ.axis = dmt_Slice.AXIS.Z;
          
        }
        
        
        
        private void setSlice(dmt_Slice.AXIS axis,List<Texture2D> _Images, ref int steps)
        {
            
            // bezeichnungen *MÜSSEN* mit 0 starten 
            int i = 0;
            bool isEnd = false;
            do
            {
                if (Resources.Load<Texture2D>(settings.name+"/"+axis+"/"+i) == null)
                {
                    Debug.Log(settings.name+"/"+axis+"/"+i);
                    isEnd = true;
                    break;
                }
              
                _Images.Add(Resources.Load<Texture2D>(settings.name+"/"+axis+"/"+i));
               
                i++;
            }while (!isEnd);

            switch (axis)
            {
                case dmt_Slice.AXIS.X: 
                    settings.xSteps = i;
                    break;
                case dmt_Slice.AXIS.Y: 
                    settings.ySteps = i;
                    break;
                case dmt_Slice.AXIS.Z: 
                    settings.zSteps = i;
                    break;
            }
            //steps = i;
            
            

        }
        
        
        

        //Layer ist für die Slices verantwortlich
        public void updateSlices(Vector3 v)
        {

            
            var valx =new Vector3(v.x, 0f, 0f);
            var valy =new Vector3(0f, v.y, 0f);
            var valz =new Vector3(0f, v.z, 0f);


            if(v.x >=0 && v.x <=1f )
                 sliceX.showSlice(ImagesX[Mathf.RoundToInt(v.x * (settings.xSteps - 1))], valx );
            else if(v.x <0 ||  v.x >1f  ) sliceX.hideSlice();
            
            if(v.y >=0 && v.y <=1f  )
                sliceY.showSlice(ImagesY[Mathf.RoundToInt(v.y * (settings.ySteps - 1))], valy);
            else if(v.y <0  || v.y >1f) sliceY.hideSlice();
            
            if(v.z >=0 && v.z <=1f )
            {
                Debug.Log("frame: "+ Mathf.RoundToInt(v.z * (settings.zSteps - 1)));
                sliceZ.showSlice(ImagesZ[Mathf.RoundToInt(v.z * (settings.zSteps - 1))], valz);
                
            }   else if(v.z <0  || v.z >1f) sliceZ.hideSlice();
            
            
        }
    }
    public  struct LayerSettings
    {
        public string name;
        public float xOffset;
        public float xWidth;
        public int xSteps;
        public float yOffset;
        public float yWidth;
        public int ySteps;
        public float zOffset;
        public float zWidth;
        public int zSteps;
    }
}