using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Inputs;

public class HoleMovement : MonoBehaviour
{
    [Header("Hole mesh")]
    [SerializeField] MeshFilter meshFilter;
    [SerializeField] MeshCollider meshCollider;

    [Header("Hole vertices radius")]
    [SerializeField] Vector2 moveLimits;
    [SerializeField] Transform holeCenter;

    //HoleData
    [Space()]
    [SerializeField] float moveSpeed;
    [SerializeField] float radius;
    [SerializeField] InputSettings input;

    private Mesh mesh;
    private List<int> holeVertices = new List<int>();
    private List<Vector3> offsets = new List<Vector3>();
    private int holeVerticesCount;

    private void Start()
    {
        Game.isGameOver = false;
        Game.isMoving = false;

        mesh = meshFilter.mesh;

        //Find Hole vertices on the mesh
        FindHoleVertices();
    }

    private void Update()
    {
        //Mouse move
        Game.isMoving = Input.GetMouseButton(0);

        if (!Game.isGameOver && Game.isMoving)
        {
            //Move hole center
            HandleHoleMovement();

            //Update hole vertices
            UpdateHoleVerticesPosition();
        }
    }

    #region Hole Center Movement
    private void HandleHoleMovement()
    {
        holeCenter.position = ClampHolePosition(PositionLerp(holeCenter.position, CalculateDirection()));
    }

    private Vector3 CalculateDirection()
    {
        return Vector3.right * input.InputDrag.x + Vector3.forward * input.InputDrag.y;
    }

    private Vector3 PositionLerp(Vector3 holePos, Vector3 direction)
    {
        return Vector3.Lerp(holePos, holePos + direction, moveSpeed * Time.deltaTime);
    }

    private Vector3 ClampHolePosition(Vector3 holePos)
    {
        return new Vector3(
            Mathf.Clamp(holePos.x, -moveLimits.x, moveLimits.x),
            holePos.y,
            Mathf.Clamp(holePos.z, -moveLimits.y, moveLimits.y));
    }
    #endregion


    private void UpdateHoleVerticesPosition()
    {
        Vector3[] vertices = mesh.vertices;
        for (int i = 0; i < holeVerticesCount; i++)
        {
            vertices[holeVertices[i]] = holeCenter.position + offsets[i];
        }

        UpdateMesh(vertices);
    }

    private void UpdateMesh(Vector3[] vertices)
    {
        mesh.vertices = vertices;
        meshFilter.mesh = mesh;
        meshCollider.sharedMesh = mesh;
    }


    private void FindHoleVertices()
    {
        for (int i = 0; i < mesh.vertices.Length; i++)
        {
            float distance = Vector3.Distance(holeCenter.position, mesh.vertices[i]);

            if (distance < radius)
            {
                holeVertices.Add(i);
                offsets.Add(mesh.vertices[i] - holeCenter.position);
            }
        }

        holeVerticesCount = holeVertices.Count;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(holeCenter.position, radius);
    }
}
