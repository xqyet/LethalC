using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace LethalESP
{
    public class Breadcrumb : MonoBehaviour
    {
        private LineRenderer lineRenderer;
        private List<Vector3> trailPoints = new List<Vector3>();
        private Transform player;
        private float pointSpacing = 1.0f; // minimum distance before placing new breadcrumb
        private Vector3 lastPoint;

        private void Awake()
        {
            // Create the line renderer object
            GameObject lineObject = new GameObject("BreadcrumbTrail");
            lineRenderer = lineObject.AddComponent<LineRenderer>();

            // Configure line appearance
            lineRenderer.positionCount = 0;
            lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
            lineRenderer.startColor = new Color(0f, 1f, 0f, 0.5f); // transparent green
            lineRenderer.endColor = new Color(0f, 1f, 0f, 0.5f);
            lineRenderer.startWidth = 0.05f;
            lineRenderer.endWidth = 0.05f;
            lineRenderer.numCapVertices = 2;
            lineRenderer.useWorldSpace = true;

            // Track player
            player = GameNetworkManager.Instance?.localPlayerController?.transform;

            if (player != null)
                lastPoint = player.position;
        }

        private void Update()
        {
            // Press F9 to clear trail
            if (Keyboard.current.f9Key.wasPressedThisFrame)
            {
                trailPoints.Clear();
                lineRenderer.positionCount = 0;
                return;
            }

            if (player == null) return;

            float distance = Vector3.Distance(player.position, lastPoint);
            if (distance >= pointSpacing)
            {
                trailPoints.Add(player.position);
                lineRenderer.positionCount = trailPoints.Count;
                lineRenderer.SetPosition(trailPoints.Count - 1, player.position);
                lastPoint = player.position;
            }
        }
    }
}
