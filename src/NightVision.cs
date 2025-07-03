using UnityEngine;

namespace LethalESP
{
    public class NightVision : MonoBehaviour
    {
        private Light nearLight;
        private Light farLight;

        // Adjustable brightness (1.0 = max, like Imperium)
        public static float Brightness = 1.0f;
        public static bool IsEnabled = true;

        public static NightVision Create()
        {
            var obj = new GameObject("MyNightVision");
            return obj.AddComponent<NightVision>();
        }

        private void Awake()
        {
            var mapCamera = GameObject.Find("MapCamera")?.transform;
            var camera = Camera.main?.transform;

            if (camera == null)
                return;

            transform.SetParent(camera);

            // Boost ambient visibility
            RenderSettings.fog = false;
            RenderSettings.ambientLight = Color.white;
            RenderSettings.ambientIntensity = 1f;

            // Near light
            nearLight = new GameObject("Near").AddComponent<Light>();
            nearLight.type = LightType.Point;
            nearLight.range = 70f;
            nearLight.color = new Color(0.875f, 0.788f, 0.791f, 1);
            nearLight.transform.SetParent(transform);
            nearLight.transform.position =
                (mapCamera != null ? mapCamera.position : camera.position) + Vector3.down * 80f;

            // Far light
            farLight = new GameObject("Far").AddComponent<Light>();
            farLight.type = LightType.Point;
            farLight.range = 500f;
            farLight.color = Color.white;
            farLight.transform.SetParent(transform);
            farLight.transform.position =
                (mapCamera != null ? mapCamera.position : camera.position) + Vector3.down * 30f;
        }

        private void Update()
        {
            // Always enabled at max brightness
            if (nearLight != null)
            {
                nearLight.enabled = IsEnabled;
                nearLight.intensity = Brightness * 100f;
            }

            if (farLight != null)
            {
                farLight.enabled = IsEnabled;
                farLight.intensity = Brightness * 1100f;
            }
        }
    }
}
