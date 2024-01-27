using System;
using System.Threading;
using Core.Player;
using UnityEngine;

public class RendererViewZone : MonoBehaviour
{ 
    [field: SerializeField]
    private LayerMask visionObstructingLayer;
    
    [field: SerializeField] 
    private Material visionConeMaterial;
    
    [field: SerializeField] 
    private float visionRange;
    
    [field: SerializeField] 
    private float visionAngle;
    
    [field: SerializeField] 
    private int visionConeResolution = 120;
    
    private Mesh _visionConeMesh;

    [field: SerializeField, HideInInspector]
    private MeshRenderer meshRenderer;
    
    [field: SerializeField, HideInInspector]
    private MeshFilter meshFilter;
        
    [field: SerializeField, HideInInspector]
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
        _visionConeMesh = new();
    }

    public void UpdateViewZone()
    {
        int[] triangles = new int[(visionConeResolution - 1) * 3];
        Vector3[] vertices = new Vector3[visionConeResolution + 1];
        vertices[0] = Vector3.zero;
        float currentangle = -visionAngleInRad / 2;
        float angleIcrement = visionAngleInRad / (visionConeResolution - 1);
        float sine;
        float cosine;
        bool isViewPlayer = false;
        
        for (int i = 0; i < visionConeResolution; i++)
        {
            sine = Mathf.Sin(currentangle);
            cosine = Mathf.Cos(currentangle);
            Vector3 raycastDirection = (transform.forward * cosine) + (transform.right * sine);
            Vector3 vertForward = (Vector3.forward * cosine) + (Vector3.right * sine);
            vertices[i + 1] = vertForward * visionRange;

            var hits = Physics.RaycastAll(transform.position, raycastDirection, visionRange, visionObstructingLayer);
            var around = GetAroundHits(hits, out var result);
            
            if (around && result.collider.TryGetComponent<PlayerBehaviour>(out var player))
            {
                DetectPlayer(player);
                isViewPlayer = true; 
            }
            if(hits.Length != 0)
                vertices[i + 1] = vertForward * result.distance;
            else
                vertices[i + 1] = vertForward * visionRange;
            
            currentangle += angleIcrement;
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

        _visionConeMesh.Clear();
        _visionConeMesh.vertices = vertices;
        _visionConeMesh.triangles = triangles;
        meshFilter.mesh = _visionConeMesh;
    }

    private void DetectPlayer(PlayerBehaviour player)
    {
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
