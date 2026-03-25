using System;
using Dories.Base.Patch.Runtime;
using UnityEngine;

namespace Dories.Base
{
    public class GameEntry : MonoBehaviour
    {
        [SerializeField] private PatchEntity patchEntity;

        private void Start()
        {
            patchEntity.StartPatch();
        }
    }
}