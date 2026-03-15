using System;
using Dories.Base.Componentization.Runtime;

namespace Dories.Base.UI.Runtime
{
    public class UIPanelLogicEntity : Entity
    {
        /// <summary>
        /// 是否激活
        /// </summary>
        public bool IsActive { get; internal set; }

        /// <summary>
        /// 所属界面组
        /// </summary>
        public Type GroupType { get; internal set; }

        public override void Dispose()
        {
            base.Dispose();

            IsActive = false;
            GroupType =  null;
        }
    }
}