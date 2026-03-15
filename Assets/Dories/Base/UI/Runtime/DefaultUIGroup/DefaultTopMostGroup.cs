using UnityEngine;
using UnityEngine.UI;

namespace Dories.Base.UI.Runtime.DefaultUIGroup
{
    public class DefaultTopMostGroup : UIGroupTypeModel
    {
        public override string GroupName => "DefaultTopMost";
        public override int SortOrder => 1000;

        public override RenderMode RenderMode => RenderMode.ScreenSpaceOverlay;

        public override CanvasScaler.ScaleMode GroupScaleMode => CanvasScaler.ScaleMode.ScaleWithScreenSize;

        public override Vector2 ReferenceResolution => new Vector2(750, 1334);
    }
}