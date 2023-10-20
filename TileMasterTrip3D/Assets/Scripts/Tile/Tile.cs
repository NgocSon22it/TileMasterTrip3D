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
    [SerializeField] AudioSource a;

    public Transform targetTransform;

    [SerializeField, Range(0f, 5f)]
    float movementSpeed;
    Vector3 defaultVector = new Vector3(0.1f, 0.08f, 0.12f);
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

            RotateTile();

            if (transform.position == targetTransform.position)
            {
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
        transform.rotation = Quaternion.Euler(90f, 0, 0);
    }

    public void SetDefaultSize()
    {
        transform.localScale = defaultVector;
    }

    private void OnMouseEnter()
    {
        if (!IsUse && Game_Manager.Instance.IsStart)
        {
            a.Play();
            gameObject.GetComponent<Outline>().OutlineWidth = 10f;
            RotateTile();
        }
    }

    private void OnMouseDown()
    {
        if (!IsUse && Game_Manager.Instance.IsStart)
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
