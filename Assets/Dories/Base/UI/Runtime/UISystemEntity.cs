using System;
using System.Collections.Generic;
using Dories.Base.Componentization.Runtime;
using Dories.Base.AssetLoader.Runtime;
using UnityEngine;

namespace Dories.Base.UI.Runtime
{
    public class UISystemEntity : Entity
    {
        private IAssetLoader m_AssetLoader;
        private Transform m_UIRoot;

        private Dictionary<Type, UIGroupHelper> m_GroupTypeModels = new Dictionary<Type, UIGroupHelper>();
        private Dictionary<Type, UIPanelEntity> m_LoadedPanels = new Dictionary<Type, UIPanelEntity>();

        private int m_PanelDistance;

        protected internal override void OnAcquire(object userData = null)
        {
            base.OnAcquire(userData);

            m_AssetLoader = (IAssetLoader)userData;
            m_UIRoot = new GameObject("UIRoot").transform;
        }

        /// <summary>
        ///  初始化
        /// </summary>
        /// <param name="groupTypeModels"></param>
        public void InitUI(List<UIGroupTypeModel> groupTypeModels)
        {
            foreach (var groupTypeModel in groupTypeModels)
            {
                m_GroupTypeModels.Add(groupTypeModels.GetType(),
                    new UIGroupHelper(UIGroupCreateArgs.Create(m_UIRoot.gameObject, groupTypeModel)));
            }
        }

        public override void Dispose()
        {
            base.Dispose();

            GameObject.Destroy(m_UIRoot);
        }

        public void OpenPanel<T, TK>(object userData = null) where T : UIPanelEntity where TK : UIGroupTypeModel
        {
            m_LoadedPanels.TryGetValue(typeof(T), out var panel);
            if (panel != null)
            {
                if (panel.Entity.IsActive)
                {
                    Debug.LogError($"panel {typeof(T)} already active");
                    return;
                }

                UIGroupHelper groupHelper = InternalGetGroupHelper(typeof(T));
                groupHelper.DecoratePanel(panel);

                panel.OnOpen(userData);
                panel.gameObject.SetActive(true);
                panel.Entity.IsActive = true;
            }
            else
            {
                InternalLoadPanel(userData, typeof(T));
            }
        }

        public void ClosePanel<T>(object userData = null) where T : UISystemEntity
        {
            m_LoadedPanels.TryGetValue(typeof(T), out var panel);
            if (panel != null)
            {
                if (!panel.Entity.IsActive)
                {
                    Debug.LogError($"panel {typeof(T)} is not open, msg");
                    return;
                }
                
                panel.OnClose(userData);
                panel.gameObject.SetActive(false);
                panel.Entity.IsActive = false;
            }
            else
            {
                Debug.LogError($"panel {typeof(T)} is not open, msg");
            }
        }

        public void DestroyPanel<T>(object userData = null) where T : UISystemEntity
        {
            m_LoadedPanels.TryGetValue(typeof(T), out var panel);
            if (panel == null)
            {
                Debug.LogError($"panel {typeof(T)} is not exist, msg: {userData}");
                return;
            }

            if (panel.Entity.IsActive)
            {
                ClosePanel<T>(userData);
            }

            panel.OnRecycle(userData);
            panel.Entity.Dispose();
            panel.Dispose();
            GameObject.Destroy(panel.gameObject);
        }

        private UIGroupHelper InternalGetGroupHelper(Type groupType)
        {
            if (!m_GroupTypeModels.TryGetValue(groupType, out var groupHelper))
            {
                throw new Exception($"groupType {groupType} not exist");
            }

            return groupHelper;
        }

        private void InternalLoadPanel(object userData, Type panelType)
        {
            m_AssetLoader.LoadResourceWithCallback<GameObject>(panelType.Name,
                t =>
                {
                    var panelTemp = GameObject.Instantiate(t).GetComponent<UIPanelEntity>();
                    UIGroupHelper groupHelper = InternalGetGroupHelper(panelType);
                    groupHelper.DecoratePanel(panelTemp);

                    panelTemp.OnInit(userData);
                    panelTemp.OnOpen(userData);
                    panelTemp.gameObject.SetActive(true);
                    panelTemp.Entity.IsActive = true;
                    
                    m_LoadedPanels.Add(panelType,  panelTemp);
                },
                meg => { Debug.LogError($"panel {panelType} open fail, msg: {meg}"); });
        }
    }
}