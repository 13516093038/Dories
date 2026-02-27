using Dories.Runtime.Componentization.Utils;

namespace Dories.Runtime.ObjectPool
{
    public class PoolEntity : EntityMono
    {
        internal int PoolId;
        
        protected internal virtual void OnInit(object userData = null)
        {
            
        }

        protected internal virtual void OnOpen(object userData = null)
        {
            
        }

        protected internal virtual void OnClose(object userData = null)
        {
            Dispose();
        }

        protected internal virtual void OnRecycle(object userData = null)
        {
            
        }
    }
}