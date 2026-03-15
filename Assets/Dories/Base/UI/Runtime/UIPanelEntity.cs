using System;
using Dories.Base.Componentization.Runtime;

namespace Dories.Base.UI.Runtime
{
    public class UIPanelEntity : EntityMono
    {
        private UIPanelLogicEntity m_Entity;

        public new UIPanelLogicEntity Entity => m_Entity;

        protected internal void SetEntity(UIPanelLogicEntity entity)
        {
            m_Entity = entity;
        }
        
        protected internal virtual void OnInit(object userData = null)
        {
            
        }
        
        protected internal virtual void OnOpen(object userData = null)
        {
            
        }

        protected internal virtual void OnCover(Type panelType)
        {
            
        }

        protected internal virtual void OnClose(object userData = null)
        {
            
        }

        protected internal virtual void OnRecycle(object userData = null)
        {
            
        }
    }
}