using UnityEngine;

namespace LethalESP
{
    public class LethalESPRunner : MonoBehaviour
    {
        private bool hasInitialized = false;

        void Update()
        {
            LethalESP.Instance?.UpdateESP();

            if (!hasInitialized && LethalESP.Instance != null)
            {
                NightVision.Create();
                gameObject.AddComponent<Breadcrumb>();
                hasInitialized = true;
            }
        }

        void OnGUI()
        {
            GUIStyle style = new GUIStyle(GUI.skin.label);
            style.fontSize = 20;
            style.normal.textColor = Color.cyan;

            GUI.Label(new Rect(50, 50, 300, 40), "Lethal Loaded", style);

            LethalESP.Instance?.OnGUI();
        }
    }
}
