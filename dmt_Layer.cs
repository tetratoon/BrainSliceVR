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
      

       
        public void initLayer(string _name)
        {
            settings = new LayerSettings();
            settings.name = _name;
            
            ImagesX = new List<Texture2D>();
            ImagesY = new List<Texture2D>();
            ImagesZ = new List<Texture2D>();
            Debug.Log("setLayer: ");
            //this.name = _name;
            
            setSlice(dmt_Slice.AXIS.COR, ImagesX, ref settings.xSteps,settings.reverseOrderX);
            setSlice(dmt_Slice.AXIS.AXIAL, ImagesY,ref settings.ySteps,settings.reverseOrderY);
            setSlice(dmt_Slice.AXIS.SAG,ImagesZ, ref settings.zSteps,settings.reverseOrderZ);
            
            sliceX = GameObject.FindWithTag("sliceX").GetComponent<dmt_Slice>();
            sliceX.axis = dmt_Slice.AXIS.COR;
            //sliceX.sliceObject.Rotate(settings.rotateSliceX,0f,0f);
            Vector3 t = sliceX.sliceObject.localEulerAngles;
            t.x+=settings.rotateSliceX;
            sliceX.sliceObject.localEulerAngles = t;
            sliceX.sliceObject.transform.localScale =settings.flipX;
            
            sliceY = GameObject.FindWithTag("sliceY").GetComponent<dmt_Slice>();
            sliceY.axis = dmt_Slice.AXIS.AXIAL;
            //sliceY.sliceObject.Rotate(settings.rotateSliceY,0f,0f);
            t = sliceY.sliceObject.localEulerAngles;
            t.x+=settings.rotateSliceY;
            sliceY.sliceObject.localEulerAngles = t;
            sliceY.sliceObject.transform.localScale =settings.flipY;
            
            sliceZ = GameObject.FindWithTag("sliceZ").GetComponent<dmt_Slice>();
            sliceZ.axis = dmt_Slice.AXIS.SAG;
            //sliceZ.sliceObject.Rotate(0f,settings.rotateSliceZ,0f);
            t = sliceZ.sliceObject.localEulerAngles;
            t.y+=settings.rotateSliceZ;
            sliceZ.sliceObject.localEulerAngles = t;
            sliceZ.sliceObject.transform.localScale =settings.flipZ;
        }
        
        
        
        private void setSlice(dmt_Slice.AXIS axis,List<Texture2D> _Images, ref int steps, bool reverseOrder)
        {
            
            // bezeichnungen *MÜSSEN* mit 0 starten 
            int i = 0;
            bool isEnd = false;
            Debug.Log(settings.name+"_1_/"+axis+"/"+i);
            do
            {
                if (Resources.Load<Texture2D>(settings.name+"/"+axis+"/"+i) == null)
                {
                    Debug.Log(settings.name+"_2_/"+axis+"/"+i);
                    isEnd = true;
                    break;
                }

                if (reverseOrder)
                    _Images.Insert(0,Resources.Load<Texture2D>(settings.name+"/"+axis+"/"+i));
                else _Images.Add(Resources.Load<Texture2D>(settings.name+"/"+axis+"/"+i));
               
                i ++;
            }while (!isEnd);

            switch (axis)
            {
                case dmt_Slice.AXIS.COR:
                    settings.xSteps = _Images.Count;// i;
                    break;
                case dmt_Slice.AXIS.AXIAL: 
                    settings.ySteps = _Images.Count;// i;
                    break;
                case dmt_Slice.AXIS.SAG: 
                    settings.zSteps = _Images.Count;// i;
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
                //Debug.Log("frame: "+ Mathf.RoundToInt(v.z * (settings.zSteps - 1)));
                sliceZ.showSlice(ImagesZ[Mathf.RoundToInt(v.z * (settings.zSteps - 1))], valz);
                
            }   else if(v.z <0  || v.z >1f) sliceZ.hideSlice();
            
            
        }
    }
    
    /// <summary>
    /// die Werte sollen dann über eine externe datei abgerufen werden.
    /// </summary>
    public  class LayerSettings
    {
        public string name;
        public bool reverseOrderX =true ;
        public Vector3 flipX =new Vector3(-1,1,-1) ; //(x und z)
        public int rotateSliceX = -90;
       
        public int xSteps;
        
        public bool reverseOrderY=false ;
        public Vector3 flipY =new Vector3(-1,1,1) ;
        public int rotateSliceY = 0;
       
        public int ySteps;
        
        public bool reverseOrderZ=true ;
        public Vector3 flipZ =new Vector3(-1,1,-1) ;
        public int rotateSliceZ = 90;
        
        public int zSteps;
    }
}