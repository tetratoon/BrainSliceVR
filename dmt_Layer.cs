using System.Collections.Generic;
using UnityEngine;
using DMT_Icon.DMT_Slice;

namespace DMT_Icon.DMT_Slice
{
    
    public class dmt_Layer:MonoBehaviour
    {

        public string name;
        
        public int stepsX ,stepsY,stepsZ = 0;
       
        public List<Texture2D> ImagesX;
        public List<Texture2D> ImagesY;
        public List<Texture2D> ImagesZ;
       
        
        //die drei Slice-objekte
        private dmt_Slice sliceX, sliceY, sliceZ;
      

       
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
          
        }
        
        
        
        private void setSlice(dmt_Slice.AXIS axis,List<Texture2D> _Images, ref int steps)
        {
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

           
            
            //Debug.Log("updateSlice");
            sliceX.showSlice(ImagesX[Mathf.RoundToInt(v.x * (stepsX - 1))], valx );
            sliceY.showSlice(ImagesY[Mathf.RoundToInt(v.y * (stepsX - 1))], valy);
            sliceZ.showSlice(ImagesZ[Mathf.RoundToInt(v.z * (stepsX - 1))], valz);
        }
    }
}