using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tool
{
    public class PixelPaintTool : MonoBehaviour, ITool
    {
        //Properties
        /// <summary>
        /// The tool type
        /// </summary>
        public Type toolType => Type.PixelPaint;

        //Methods
        public void HandleToolEntry()
        {

        }

        public void HandleToolExit()
        {

        }
    }
}
