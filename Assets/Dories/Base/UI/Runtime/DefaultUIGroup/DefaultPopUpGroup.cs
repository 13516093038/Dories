using UnityEngine;
using UnityEngine.UI;

namespace Dories.Base.UI.Runtime.DefaultUIGroup
{
    public class DefaultPopUpGroup :  UIGroupTypeModel
    {
        public override string GroupName => "DefaultPopUp";
        public override int SortOrder => 1000;

        public override RenderMode RenderMode => RenderMode.ScreenSpaceOverlay;

        public override CanvasScaler.ScaleMode GroupScaleMode => CanvasScaler.ScaleMode.ScaleWithScreenSize;

        public override Vector2 ReferenceResolution => new Vector2(750, 1334);
    }
}