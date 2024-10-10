using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryLayouter : MonoBehaviour
{
    public List<Transform> children = new List<Transform>();

    [SerializeField] private Transform focusedChild;
    [SerializeField] private int focusedIndex;
    
    private Vector3 centralPos = new Vector3(0f, 0.74f, 6f);

    private void Update()
    {
        if (children.Count <= 0)
        {
            focusedChild = null;
            return;
        }

        if (focusedChild == null)
        {
            focusedIndex = children.Count / 2;
            focusedChild = children[focusedIndex];
        }
        
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (focusedIndex - 1 >= 0)
            {
                focusedIndex -= 1;
                focusedChild = children[focusedIndex];
            }
            else if (focusedIndex - 1 < 0)
            {
                focusedIndex = children.Count - 1;
                focusedChild = children[focusedIndex];
            }
        }
            
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (focusedIndex + 1 <= children.Count - 1)
            {
                focusedIndex += 1;
                focusedChild = children[focusedIndex];
            }
            else if (focusedIndex + 1 > children.Count - 1)
            {
                focusedIndex = 0;
                focusedChild = children[focusedIndex];
            }
                
        }

        foreach (Transform child in children)
        {
            child.localPosition = new Vector3(15f, centralPos.y, centralPos.z);
        }

        focusedChild.transform.localPosition = centralPos;
    }
}
