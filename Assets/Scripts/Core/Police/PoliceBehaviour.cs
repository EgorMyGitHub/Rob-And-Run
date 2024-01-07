using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Core.Police
{
    public class PoliceBehaviour : MonoBehaviour
    {
        public class PolicePool : MonoMemoryPool<PoliceBehaviour>
        { }
    }
}
