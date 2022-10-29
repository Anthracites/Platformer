using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Platformer.GamePlay
{
    public class PlatformController : MonoBehaviour
    {
        public class Factory : PlaceholderFactory<string, PlatformController>
        {

        }
    }
}
