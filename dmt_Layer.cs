using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using DMT_Icon.DMT_Slice;
using Unity.VisualScripting;

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

        public dmt_Slice activeSlice;
        
        [SerializeField]
        public LayerSettings settings;


        

        public void initLayer(string _name)
        {
            
            settings = new LayerSettings();
            settings.name = _name;
            
            // WriteString(settings.name);
            ReadString(settings.name);
            
            ImagesX = new List<Texture2D>();
            ImagesY = new List<Texture2D>();
            ImagesZ = new List<Texture2D>();
            Debug.Log("setLayer: ");
            //this.name = _name;
            
            //loadSettings
            
            
            setSlice(dmt_Slice.AXIS.COR, ImagesX, ref settings.xSteps,settings.reverseOrderX);
            setSlice(dmt_Slice.AXIS.AXIAL, ImagesY,ref settings.ySteps,settings.reverseOrderY);
            setSlice(dmt_Slice.AXIS.SAG,ImagesZ, ref settings.zSteps,settings.reverseOrderZ);
            
          
            sliceX = GameObject.FindWithTag("sliceX").GetComponent<dmt_Slice>();
            sliceX.axis = dmt_Slice.AXIS.COR;
            sliceY = GameObject.FindWithTag("sliceY").GetComponent<dmt_Slice>();
            sliceY.axis = dmt_Slice.AXIS.AXIAL;
            sliceZ = GameObject.FindWithTag("sliceZ").GetComponent<dmt_Slice>();
            sliceZ.axis = dmt_Slice.AXIS.SAG;
            
            ApplySettings();
           

            activeSlice = sliceX; // default
        }

        public  void WriteString(string name)
        {
            string t = "";
            string path = "Assets/Resources/"+name+"/settings.txt";
           
            StreamWriter writer = new StreamWriter(path, false);
            writer.WriteLine(settings.name);
            
            writer.WriteLine(settings.reverseOrderX);
            
            //t= float.ToString("F2").Replace(",", ".");
            
            writer.WriteLine(settings.flipX.x);
            writer.WriteLine(settings.flipX.y);
            writer.WriteLine(settings.flipX.z);
                            
            writer.WriteLine(settings.rotateSliceX);

            writer.WriteLine(settings.reverseOrderY);
            writer.WriteLine(settings.flipY.x);
            writer.WriteLine(settings.flipY.y);
            writer.WriteLine(settings.flipY.z);
            
            writer.WriteLine(settings.rotateSliceY);
            
            writer.WriteLine(settings.reverseOrderZ);
            writer.WriteLine(settings.flipZ.x);
            writer.WriteLine(settings.flipZ.y);
            writer.WriteLine(settings.flipZ.z);
            
            writer.WriteLine(settings.rotateSliceZ);
            
            
            
            
            
            writer.Close();
            StreamReader reader = new StreamReader(path);
            //Print the text from the file
            Debug.Log(reader.ReadToEnd());
            reader.Close();
        }
        public void ReadString(string name)
        {
            string path = "Assets/Resources/"+name+"/settings.txt";
            //Read the text from directly from the test.txt file
            try
            {
                StreamReader reader = new StreamReader(path);
                Debug.Log("reading"+reader.ReadLine());
                settings.reverseOrderX=bool.Parse(reader.ReadLine());
                settings.flipX.x = float.Parse(reader.ReadLine());
                settings.flipX.y = float.Parse(reader.ReadLine());
                settings.flipX.z = float.Parse(reader.ReadLine());
                settings.rotateSliceX = int.Parse(reader.ReadLine());
                //
                settings.reverseOrderY=bool.Parse(reader.ReadLine());
                settings.flipY.x =float.Parse(reader.ReadLine());
                settings.flipY.y =float.Parse(reader.ReadLine());
                settings.flipY.z =float.Parse(reader.ReadLine());
                settings.rotateSliceY = int.Parse(reader.ReadLine());
                //
                settings.reverseOrderZ=bool.Parse(reader.ReadLine());
                settings.flipZ.x =float.Parse(reader.ReadLine());
                settings.flipZ.y =float.Parse(reader.ReadLine());
                settings.flipZ.z =float.Parse(reader.ReadLine());
                settings.rotateSliceZ = int.Parse(reader.ReadLine());
                Debug.Log("order: "+settings.reverseOrderX);
                Debug.Log("flip: "+settings.flipX);
                //ReadToEnd());
                reader.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                WriteString(name);
                //throw;
            }
            
            
           
        }
        
        
        
        public static Vector3 Parse(string str)
        {
            str = str.Replace("(", "").Replace(")"," ");//Replace "(" and ")" in the string with ""
            string[] s = str.Split(',');
            return new Vector3(float.Parse(s[0]), float.Parse(s[1]), float.Parse(s[2]));
        }
        
        
        public void ApplySettings()
        {
           
            //sliceX.sliceObject.Rotate(settings.rotateSliceX,0f,0f);
            Vector3 t = sliceX.sliceObject.localEulerAngles;
            t.x =settings.rotateSliceX;
            t.y = 0f;
            t.z = -90f;
            //sliceX.sliceObject.localRotation()
            
            sliceX.sliceObject.localEulerAngles = t;
            sliceX.sliceObject.transform.localScale =settings.flipX;
            
           
            //sliceY.sliceObject.Rotate(settings.rotateSliceY,0f,0f);
            t = sliceY.sliceObject.localEulerAngles;
            t.y =settings.rotateSliceY;
            sliceY.sliceObject.localEulerAngles = t;
            sliceY.sliceObject.transform.localScale =settings.flipY;
            
           
            //sliceZ.sliceObject.Rotate(0f,settings.rotateSliceZ,0f);
            t = sliceZ.sliceObject.localEulerAngles;
            t.y =settings.rotateSliceZ;
            sliceZ.sliceObject.localEulerAngles = t;
            sliceZ.sliceObject.transform.localScale =settings.flipZ;
            
            Debug.Log("ApplySettings");
            WriteString(settings.name);
            
        }
        

        public void ChangeActiveSlice(dmt_Slice.AXIS? _axis )
        {
            if (_axis != null)
            {
                switch (_axis)
                {
                    case dmt_Slice.AXIS.COR: 
                        activeSlice = sliceX;
                        break;
                    case dmt_Slice.AXIS.SAG: 
                        activeSlice = sliceZ;
                        break;
                    case dmt_Slice.AXIS.AXIAL: 
                        activeSlice = sliceY;
                        break;
                }
            }
            else
            {
                switch (activeSlice.axis)
                {
                    case dmt_Slice.AXIS.COR:
                        activeSlice = sliceY;
                        break;
                    case dmt_Slice.AXIS.SAG:  
                        activeSlice = sliceX;
                        break;
                    case dmt_Slice.AXIS.AXIAL: 
                        activeSlice = sliceZ;
                        break;
                }
            }
            Debug.Log("active Slice: "+activeSlice.axis.ToString());
        }
        
        public void ChangeSettings(bool _flip = false, bool _rot=false, bool _order=false)
        {
         
            Debug.Log("activeSlice.axis "+activeSlice.axis);
            switch (activeSlice.axis)
            {
                case dmt_Slice.AXIS.COR:
                    settings.flipX.x = _flip ? settings.flipX.x*-1f: settings.flipX.x;
                    settings.rotateSliceX = _rot ? settings.rotateSliceX + 90:settings.rotateSliceX;
                    settings.reverseOrderX = _order ? !settings.reverseOrderX : settings.reverseOrderX;
                    Debug.Log("rotateSliceX = "+settings.rotateSliceX);
                    break;
                case dmt_Slice.AXIS.SAG:  
                    settings.flipZ.x = _flip ? settings.flipZ.x*-1f: settings.flipZ.x;
                    settings.rotateSliceZ = _rot ? settings.rotateSliceZ + 90:settings.rotateSliceZ;
                    settings.reverseOrderZ = _order ? !settings.reverseOrderZ : settings.reverseOrderZ;
                    Debug.Log("0");
                    break;
                case dmt_Slice.AXIS.AXIAL: //geht
                    settings.flipY.z = _flip ? settings.flipY.z*-1f: settings.flipY.z;
                    settings.rotateSliceY = _rot ? settings.rotateSliceY + 90:settings.rotateSliceY;
                    settings.reverseOrderY = _order ? !settings.reverseOrderY : settings.reverseOrderY;
                    Debug.Log("0");
                    break;
            }
            
            ApplySettings();
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
            {
                v.x = settings.reverseOrderX ? 1f - v.x : v.x;
                
                sliceX.showSlice(ImagesX[
                        Mathf.RoundToInt(v.x * (settings.xSteps - 1))],
                    valx);
            }
            else if(v.x <0 ||  v.x >1f  ) sliceX.hideSlice();

            if (v.y >= 0 && v.y <= 1f)
            {
                v.y = settings.reverseOrderY ? 1f - v.y : v.y;

                sliceY.showSlice(ImagesY[Mathf.RoundToInt(v.y * (settings.ySteps - 1))], valy);
            }
            else if(v.y <0  || v.y >1f) sliceY.hideSlice();
            
            if(v.z >=0 && v.z <=1f )
            {
                v.z = settings.reverseOrderZ ? 1f - v.z : v.z;

                sliceZ.showSlice(ImagesZ[Mathf.RoundToInt(v.z * (settings.zSteps - 1))], valz);
                
            }   else if(v.z <0  || v.z >1f) sliceZ.hideSlice();
            
            
        }
        
    }
    
    /// <summary>
    /// die Werte sollen dann über eine externe datei abgerufen werden.
    /// </summary>
    ///
    [Serializable]
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