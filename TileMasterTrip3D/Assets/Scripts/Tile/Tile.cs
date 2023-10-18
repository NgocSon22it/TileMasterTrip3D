using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [Header("Entity")]
    [SerializeField] public Tile_Scriptable tile_Scriptable;

    [Header("Component")]
    [SerializeField] Rigidbody rb;
    [SerializeField] BoxCollider boxCollider;

    public Transform targetTransform;

    [SerializeField, Range(0f, 5f)]
    float movementSpeed;

    bool IsUse;

    private void FixedUpdate()
    {
        MoveToTarget();
    }

    public void MoveToTarget()
    {
        if (targetTransform != null)
        {
            SetUp_Collider(false);
            transform.position = Vector3.MoveTowards(transform.position, targetTransform.position, Time.fixedDeltaTime * movementSpeed);

            if (transform.position == targetTransform.position)
            {
                RotateTile();
                IsUse = true;
            }
        }
    }

    public void Set_Target(Transform target)
    {
        targetTransform = target;
    }

    public void SetUp_Collider(bool value)
    {
        rb.useGravity = value;
        boxCollider.enabled = value;
    }

    public void Set_Earn()
    {
        gameObject.SetActive(false);
        Game_Manager.Instance.SetObjectFromPoolAtPosition(transform);
    }

    public void RotateTile()
    {
        transform.eulerAngles = new Vector3(90f, 0, 0);
    }

    private void OnMouseEnter()
    {
        if (!IsUse)
        {
            gameObject.GetComponent<Outline>().OutlineWidth = 10f;
            RotateTile();
        }
    }

    private void OnMouseDown()
    {
        if (!IsUse)
        {
            Game_Manager.Instance.Insert_Tile(this);
        }
    }

    private void OnMouseExit()
    {
        if (!IsUse)
        {
            gameObject.GetComponent<Outline>().OutlineWidth = 0f;
        }

    }
}
