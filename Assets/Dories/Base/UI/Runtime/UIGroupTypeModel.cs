using UnityEngine;
using UnityEngine.UI;

namespace Dories.Base.UI.Runtime
{
    public abstract class UIGroupTypeModel
    {
        /// <summary>
        /// Group name
        /// </summary>
        public abstract string GroupName { get; }
        
        /// <summary>
        /// Group sort order
        /// </summary>
        public abstract int SortOrder { get; }
        
        /// <summary>
        /// Group Rendermode
        /// </summary>
        public virtual RenderMode RenderMode { get; } = RenderMode.ScreenSpaceOverlay;

        /// <summary>
        /// Group render camera
        /// </summary>
        public virtual Camera RenderCamera { get; } = null;

        /// <summary>
        /// Group scale mode
        /// </summary>
        public virtual CanvasScaler.ScaleMode GroupScaleMode { get; } = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        
        /// <summary>
        /// Group reference resolution
        /// </summary>
        public virtual Vector2 ReferenceResolution { get; } = new Vector2(1334, 750);

        /// <summary>
        /// Group screen match
        /// </summary>
        public virtual int Match { get; } = 0;

        /// <summary>
        /// Group panel distance
        /// </summary>
        public virtual int PanelDistance { get; } = 5;
    }
}