using System.Collections.Generic;
using UnityEngine;
using Inputs;

public class HoleController : MonoBehaviour
{
    [SerializeField] Transform holeCenter;

    [Header("Dependencies"), Space()]
    [SerializeField] HoleData holeData;
    [SerializeField] InputSettings input;

    [Header("Hole mesh")]
    [SerializeField] MeshFilter meshFilter;
    [SerializeField] MeshCollider meshCollider;

    private Mesh mesh;
    private List<int> holeVertices = new List<int>();
    private List<Vector3> offsets = new List<Vector3>();
    private int holeVerticesCount;

    #region Ground Limit Values
    private float XAxisMinLimit() => Ground.Instance.xLeftLimit.position.x;
    private float XAxisMaxLimit() => Ground.Instance.xRightLimit.position.x;
    private float ZAxisMinLimit() => Ground.Instance.zDownLimit.position.z;
    private float ZAxisMaxLimit() => Ground.Instance.zUpLimit.position.z;
    #endregion

    private void Start()
    {
        Game.isGameOver = false;
        Game.isMoving = false;

        mesh = meshFilter.mesh;

        FindHoleVertices();
    }

    private void Update()
    {
        Game.isMoving = Input.GetMouseButton(0);

        if (!Game.isGameOver && Game.isMoving)
        {
            HandleHoleMovement();

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
        return Vector3.Lerp(holePos, holePos + direction, holeData.MoveSpeed * Time.deltaTime);
    }

    private Vector3 ClampHolePosition(Vector3 holePos)
    {
        return new Vector3(
            Mathf.Clamp(holePos.x, XAxisMinLimit(), XAxisMaxLimit()),
            holePos.y,
            Mathf.Clamp(holePos.z, ZAxisMinLimit(), ZAxisMaxLimit()));
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

            if (distance < holeData.Radius)
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
        Gizmos.DrawSphere(holeCenter.position, holeData.Radius);
    }
}
