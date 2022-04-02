using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeltHandler : MonoBehaviour
{
    [SerializeField] BoxCollider2D _conveyorArea = null;

    //settings
    [SerializeField] Vector2 _moveForce = new Vector2(1, 0);

    //state
    List<Collider2D> _trashOnBelt = new List<Collider2D>();
    ContactFilter2D _cf2d = new ContactFilter2D();

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //FindTrash();
        //ConveyTrash();
    }

    //private void FindTrash()
    //{
    //    int amount = _conveyorArea.OverlapCollider(_cf2d, _trashOnBelt);
    //    Debug.Log($"found {amount} pieces of trash");
    //}

    //private void ConveyTrash()
    //{
    //    throw new NotImplementedException();
    //}

    private void OnCollisionStay2D(Collision2D collision)
    {
        Debug.Log("tap");
        collision.gameObject.GetComponent<Rigidbody2D>().AddForce(_moveForce);
    }
}
