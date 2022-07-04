using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HotBallon : MonoBehaviour
{
    private void Awake() {
        cloud_layer = LayerMask.GetMask("Walls");
    }
    private void FixedUpdate()
    {
        movement();
        detect_collision();
    }
    private void OnDrawGizmos() {
        Gizmos.color = new Color(1f , 0 , 0);
        Gizmos.DrawWireCube(transform.position , detect_size);
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
    [SerializeField] TextMeshProUGUI ballon_text;
    public void set_ballon_text(string text){
        ballon_text.text = text;
    }
    Vector3 detect_size = new Vector3(0.64f , 2.56f , 0);
    LayerMask cloud_layer;
    void detect_collision(){
        Collider2D cloud = Physics2D.OverlapBox(transform.position , detect_size , 0 , cloud_layer);
        if(cloud == null) return;
        Cloud cloud_script = cloud.GetComponent<Cloud>();
        if(cloud_script.is_answer){
            cloud_script.destroy_whole_clouds();
        }
        else{
            Destroy(gameObject);
        }
    }
}
