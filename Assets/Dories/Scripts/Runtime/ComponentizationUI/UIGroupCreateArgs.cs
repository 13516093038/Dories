using Dories.Runtime.Reference;
using UnityEngine;

namespace Dories.Runtime.ComponentizationUI
{
    public class UIGroupCreateArgs : IReference
    {
        public GameObject UIRoot { get; private set; }
        
        public UIGroupTypeModel UIGroupTypeModel { get; private set; }

        public static UIGroupCreateArgs Create(GameObject uiRoot, UIGroupTypeModel uiGroupTypeModel)
        {
            var args = ReferencePool.Acquire<UIGroupCreateArgs>();
            args.UIRoot = uiRoot;
            args.UIGroupTypeModel = uiGroupTypeModel;
            return args;
        }

        public void Release()
        {
            ReferencePool.Release(this);
        }

        public void Dispose()
        {
            UIRoot = null;
            UIGroupTypeModel = null;
        }
    }
}