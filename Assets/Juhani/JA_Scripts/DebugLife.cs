using UnityEngine;

public class DebugLife : MonoBehaviour
{
   void OnEnable() { Debug.Log($"{name} Enabled", this); }
   void OnDisable() { Debug.Log($"{name} Disabled", this); }
   void OnDestroy() { Debug.Log($"{name} Destroyed", this); }
}
