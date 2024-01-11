using System;
using System.Threading;
using Core.Player;
using UnityEngine;

public class RendererViewZone : MonoBehaviour
{ 
    [SerializeField]
    private LayerMask visionObstructingLayer;
    
    [SerializeField] 
    private Material visionConeMaterial;
    
    [SerializeField] 
    private float visionRange;
    
    [SerializeField] 
    private float visionAngle;
    
    [SerializeField] 
    private int VisionConeResolution = 120;
    
    private Mesh VisionConeMesh;

    [SerializeField, HideInInspector]
    private MeshRenderer meshRenderer;
    
    [SerializeField, HideInInspector]
    private MeshFilter meshFilter;
        
    [SerializeField, HideInInspector]
    private float visionAngleInRad;
    
    private CancellationTokenSource _playerInView = null;
    
    public event Action<PlayerBehaviour, CancellationTokenSource> PlayerInView;
    
    private void OnValidate()
    {
        meshRenderer = transform.GetComponent<MeshRenderer>();
        meshFilter = transform.GetComponent<MeshFilter>();
        visionAngleInRad = visionAngle * Mathf.Deg2Rad;
        
        meshRenderer.material = visionConeMaterial;
    }

    void Awake()
    {
        VisionConeMesh = new();
    }

    public void UpdateViewZone()
    {
        int[] triangles = new int[(VisionConeResolution - 1) * 3];
        Vector3[] Vertices = new Vector3[VisionConeResolution + 1];
        Vertices[0] = Vector3.zero;
        float Currentangle = -visionAngleInRad / 2;
        float angleIcrement = visionAngleInRad / (VisionConeResolution - 1);
        float Sine;
        float Cosine;
        bool isViewPlayer = false;
        
        for (int i = 0; i < VisionConeResolution; i++)
        {
            Sine = Mathf.Sin(Currentangle);
            Cosine = Mathf.Cos(Currentangle);
            Vector3 RaycastDirection = (transform.forward * Cosine) + (transform.right * Sine);
            Vector3 VertForward = (Vector3.forward * Cosine) + (Vector3.right * Sine);
            Vertices[i + 1] = VertForward * visionRange;
            
            var hits = Physics.RaycastAll(transform.position, RaycastDirection, visionRange, visionObstructingLayer);
            var around = GetAroundHits(hits, out var result);
            
            if (around && result.collider.TryGetComponent<PlayerBehaviour>(out var player))
            {
                DetectPlayer(player);
                isViewPlayer = true; 
            }
            if(hits.Length != 0)
                Vertices[i + 1] = VertForward * result.distance;
            else
                Vertices[i + 1] = VertForward * visionRange;
            
            Currentangle += angleIcrement;
        }
        for (int i = 0, j = 0; i < triangles.Length; i += 3, j++)
        {
            triangles[i] = 0;
            triangles[i + 1] = j + 1;
            triangles[i + 2] = j + 2;
        }

        if (!isViewPlayer && _playerInView != null)
        {
            _playerInView.Cancel();
            _playerInView = null;
        }

        VisionConeMesh.Clear();
        VisionConeMesh.vertices = Vertices;
        VisionConeMesh.triangles = triangles;
        meshFilter.mesh = VisionConeMesh;
    }

    private void DetectPlayer(PlayerBehaviour player)
    {
        Debug.LogError("Player View");
        
        if(_playerInView != null)
            return;
        
        PlayerInView?.Invoke(player, _playerInView = new());
    }
    
    private bool GetAroundHits(RaycastHit[] hits, out RaycastHit result)
    {
        if(hits.Length == 0)
        {
            result = default;
            
            return false;
        }
        
        result = hits[0];
        float minDistance = Mathf.Infinity;
        
        foreach (var item in hits)
        {
            if (item.distance < minDistance)
            {
                result = item;
                minDistance = item.distance;
            }
        }
        
        return true;
    }
}
