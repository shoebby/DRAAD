using UnityEngine;

public class ShowBoxCollider : MonoBehaviour
{
    [SerializeField] private MeshFilter meshFilter;
    [SerializeField] private Color gizmoColor;
    [SerializeField] private BoxCollider boxCollider;

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawWireMesh(meshFilter.sharedMesh, 0, transform.position, transform.rotation, boxCollider.size);
    }
}
