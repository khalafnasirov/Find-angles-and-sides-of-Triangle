using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VectorScript : MonoBehaviour
{
    public LineRenderer line1;
    public LineRenderer line2;
    public LineRenderer line3;

    public Transform dot1;
    public Transform dot2;
    public Transform dot3;

    public TextMeshProUGUI side1;
    public TextMeshProUGUI side2;
    public TextMeshProUGUI side3;

    public TextMeshProUGUI angle1;
    public TextMeshProUGUI angle2;
    public TextMeshProUGUI angle3;

    void ShowSides()
    {
        if (side1 != null && side2 != null && side3 != null)
        {
            side1.text = $"AB = {(int) GetSideLength(dot1.position, dot2.position)}";
            side2.text = $"BC = {(int) GetSideLength(dot2.position, dot3.position)}";
            side3.text = $"AC = {(int) GetSideLength(dot1.position, dot3.position)}";
        }
    }

    void ShowAngles()
    {
        if (angle1 != null && angle2 != null && angle3 != null)
        {
            angle1.text = $"a = {(int) GetAngle(1)}";
            angle2.text = $"b = {(int) GetAngle(2)}";
            angle3.text = $"c = {(int) GetAngle(3)}";
        }
    }

    float GetSideLength(Vector3 first_pos, Vector3 last_pos)
    {
        float distance = Vector3.Distance(first_pos, last_pos);
        return distance;
    }

    float GetAngle(int dot)
    {
        float ab = GetSideLength(dot1.position, dot2.position);
        float bc = GetSideLength(dot2.position, dot3.position);
        float ac = GetSideLength(dot3.position, dot1.position);

        if (dot == 1)
        {
            return Mathf.Acos(((ab * ab) + (ac * ac) - (bc * bc)) / (2 * ab * ac)) * (180 / Mathf.PI);
        }
        else if (dot == 2)
        {
            return Mathf.Acos(((ab * ab) + (bc * bc) - (ac * ac)) / (2 * ab * bc)) * (180 / Mathf.PI);
        }
        else if (dot == 3)
        {
            return Mathf.Acos(((ac * ac) + (bc * bc) - (ab * ab)) / (2 * ac * bc)) * (180 / Mathf.PI);
        }
        return 0;
        
    }

    void GetPoint(Vector3 position)
    {
        line1.SetPosition(0, dot1.position);
        line1.SetPosition(1, dot2.position);

        line2.SetPosition(0, dot2.position);
        line2.SetPosition(1, dot3.position);

        line3.SetPosition(0, dot3.position);
        line3.SetPosition(1, dot1.position);
    }


    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 hitPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(hitPosition, Vector2.zero);

            if (hit.collider != null)
            {
                Debug.Log("Screen tapped");
                if (hit.collider.CompareTag("dot"))
                {
                    hit.collider.gameObject.transform.position = new Vector3(hitPosition.x, hitPosition.y, 0);                    
                    GetPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                }
            }
        }

        ShowSides();

        ShowAngles();
    }
}
