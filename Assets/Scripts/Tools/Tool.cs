using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tool
{
    public enum Type
    {
        PixelPaint,
    }

    public interface ITool
    {
        //Properties
        public Type toolType { get; }

        //Methods
        public void HandleToolEntry();
        public void HandleToolExit();
    }
}
