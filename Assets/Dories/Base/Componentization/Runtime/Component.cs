using System;
using System.Threading;

namespace Dories.Base.Componentization.Runtime
{
    public class Component : IDisposable
    {
        /// <summary>
        /// Component`s Parent
        /// </summary>
        public Component Parent { get; internal set; }

        private CancellationTokenSource m_DisposeCts;
        protected CancellationToken disposeCancellationToken;
        
        public long Id { get; internal set; }

        public bool IsDisposed => Id == 0;

        protected internal virtual void OnAcquire(object userData = null)
        {
            m_DisposeCts  = new CancellationTokenSource();
            disposeCancellationToken  = m_DisposeCts.Token;
        }
        
        public virtual void Dispose()
        {
            m_DisposeCts.Cancel();
            m_DisposeCts.Dispose();
        }
    }
}