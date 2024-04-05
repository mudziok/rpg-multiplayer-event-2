using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

//Klasa która pozwala na generowanie obiektów w siatce
[ExecuteInEditMode]
public class GridGameObjectCollection : MonoBehaviour
{
    public int rows = 1;
    public int columns = 5;
    public float spacing = 1.0f;
    public float rotation = 0.0f;
    public GameObject prefab;
    [SerializeField] Vector3 prefabPositionOffset;
    [SerializeField] Vector3 prefabRotationOffset;

    public List<GameObject> gridObjects = new List<GameObject>();

    private void Start()
    {
        // Initialization or testing code can go here
    }

    // This method is responsible for adding an object to the grid
    public void AddObjectToGrid()
    {
        if (gridObjects.Count < rows * columns)
        {
            Vector3 nextPosition = CalculateNextPosition();
            GameObject newObject = Instantiate(prefab, nextPosition, transform.rotation * Quaternion.Euler(Vector3.up * rotation) * Quaternion.Euler(prefabRotationOffset), transform);
            gridObjects.Add(newObject);
        }
        else
        {
            Debug.Log("Grid is full.");
        }
    }

    private Vector3 CalculateNextPosition()
    {
        int currentCount = gridObjects.Count;
        int row = currentCount / columns;
        int column = currentCount % columns;
        return (Quaternion.Euler(Vector3.up * rotation) * new Vector3(column * spacing, row * spacing, 0)) + transform.position + prefabPositionOffset;
    }

    // This method will remove the last object from the grid
    public void RemoveObjectFromGrid()
    {
        if (gridObjects.Count > 0)
        {
            GameObject toRemove = gridObjects[gridObjects.Count - 1];
            gridObjects.Remove(toRemove);
            Destroy(toRemove);
        }
        else
        {
            Debug.Log("Grid is empty.");
        }
    }

    // Additional methods to visualize and manage the grid can be added here
}

// This custom editor script will draw the grid in the Scene view

#if UNITY_EDITOR
[CustomEditor(typeof(GridGameObjectCollection))]
public class GridGameObjectCollectionEditor : Editor
{
    void OnSceneGUI()
    {
        GridGameObjectCollection grid = (GridGameObjectCollection)target;

        Handles.color = Color.green;
        for (int i = 0; i < grid.rows; i++)
        {
            for (int j = 0; j < grid.columns; j++)
            {
                Vector3 position = (Quaternion.Euler(0.0f, grid.rotation, 0.0f) * new Vector3(j * grid.spacing, i * grid.spacing, 0)) + grid.transform.position;
                if (i * grid.columns + j < grid.gridObjects.Count)
                {
                    Handles.color = Color.red;
                }
                else
                {
                    Handles.color = Color.green;
                }
                Handles.DrawWireCube(position, new Vector3(0.9f * grid.spacing, 0.9f * grid.spacing, 0.9f * grid.spacing));
            }
        }
    }
}
#endif