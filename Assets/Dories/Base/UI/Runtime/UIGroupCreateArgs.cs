using Dories.Base.Reference.Runtime;
using UnityEngine;

namespace Dories.Base.UI.Runtime
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