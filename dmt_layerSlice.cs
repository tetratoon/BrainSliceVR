using System.Collections.Generic;
using UnityEngine;

namespace DMT_Icon.DMT_Slice
{
    public class dmt_layerSlice:MonoBehaviour
    {
        public dmt_Slice.AXIS axis;
        public List<Texture2D> Images;
        public int steps = 0;

       
        public void set(string _name, dmt_Slice.AXIS axis)
        {
            name = _name;
            Images = new List<Texture2D>();
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
                Images.Add(Resources.Load<Texture2D>(name+"/"+axis+"/"+i));
                Debug.Log(i);
                i++;
            }while (!isEnd);

            steps = i;

        }
    }
}