using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotBallon : MonoBehaviour
{
    private void FixedUpdate()
    {
        movement();
    }
    Vector3 position;
    public void set_position(Vector3 pos)
    {
        position = new Vector3(Mathf.Clamp(pos.x, -5f, 7f) , Mathf.Clamp(pos.y, -3f, 3f));
    }
    void movement()
    {
        transform.position = Vector3.Lerp(transform.position , position , 2f * Time.deltaTime);
    }
}
