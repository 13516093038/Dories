using System;
using Dories.Base.Componentization.Runtime.Utils;
using Dories.Base.UI.Runtime.Utils;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace Dories.Base.UI.Runtime
{
    internal class UIGroupHelper
    {
        private Canvas m_Canvas;
        private Transform m_UIRoot;
        private int m_CurrentLayer;
        private Type m_GroupType;

        private int m_layerDistance;

        internal UIGroupHelper(UIGroupCreateArgs createArgs)
        {
            m_UIRoot = createArgs.UIRoot.transform;
            m_Canvas = new GameObject(createArgs.UIGroupTypeModel.GroupName).AddComponent<Canvas>();
            
            m_Canvas.transform.SetParent(m_UIRoot);
            m_Canvas.transform.localPosition = Vector3.zero;
            m_Canvas.transform.localRotation = Quaternion.identity;
            m_Canvas.transform.localScale = Vector3.one;
            
            m_Canvas.renderMode = createArgs.UIGroupTypeModel.RenderMode;
            m_Canvas.worldCamera = createArgs.UIGroupTypeModel.RenderCamera;
            m_Canvas.sortingOrder = createArgs.UIGroupTypeModel.SortOrder;
            
            var canvasScaler = m_Canvas.AddComponent<CanvasScaler>();
            canvasScaler.uiScaleMode = createArgs.UIGroupTypeModel.GroupScaleMode;
            canvasScaler.referenceResolution = createArgs.UIGroupTypeModel.ReferenceResolution;
            canvasScaler.matchWidthOrHeight = createArgs.UIGroupTypeModel.Match;

            m_CurrentLayer  = createArgs.UIGroupTypeModel.SortOrder;
            m_layerDistance = createArgs.UIGroupTypeModel.PanelDistance;

            m_GroupType = createArgs.UIGroupTypeModel.GetType();

            m_Canvas.AddComponent<GraphicRaycaster>();
            
            createArgs.Release();
        }

        internal void DecoratePanel(UIPanelEntity panel)
        {
            if (panel.Entity == null)
            {
                panel.SetEntity(ComponentFactory.Acquire<UIPanelLogicEntity>());
            }
            
            var canvas = panel.transform.GetOrAddComponent<Canvas>();
            panel.transform.TryAddComponent<FayeGraphicRaycaster>();

            if (panel.Entity.GroupType != m_GroupType)
            {
                panel.transform.SetParent(m_Canvas.transform);
                panel.transform.localRotation = Quaternion.identity;
                panel.transform.localPosition = Vector3.zero;
                panel.transform.localScale = Vector3.one;
                
                canvas.sortingOrder = m_CurrentLayer;
                m_CurrentLayer += m_layerDistance;
            }
            
            panel.Entity.GroupType = m_GroupType;
        }
        
        internal void Destroy()
        {
            Object.Destroy(m_Canvas);
        }
    }
}