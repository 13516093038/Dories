using UnityEngine;
using UnityEngine.UI;

namespace Dories.Runtime.ComponentizationUI.DefaultUIGroup
{
    public class DefaultMainGroup :  UIGroupTypeModel
    {
        public override string GroupName => "DefaultMain";
        public override int SortOrder => 1000;

        public override RenderMode RenderMode => RenderMode.ScreenSpaceOverlay;

        public override CanvasScaler.ScaleMode GroupScaleMode => CanvasScaler.ScaleMode.ScaleWithScreenSize;

        public override Vector2 ReferenceResolution => new Vector2(750, 1334);
    }
}