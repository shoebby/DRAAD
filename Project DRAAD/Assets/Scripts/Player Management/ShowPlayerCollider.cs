using UnityEngine;

public class ShowPlayerCollider : MonoBehaviour
{
    [SerializeField] private MeshFilter meshFilter;
    [SerializeField] private Vector3 gizmoScale;
    [SerializeField] private Color gizmoColor;

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawWireMesh(meshFilter.sharedMesh, 0, transform.position, transform.rotation, gizmoScale);
    }
}
