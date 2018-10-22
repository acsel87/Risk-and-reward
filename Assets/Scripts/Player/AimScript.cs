using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimScript : MonoBehaviour {

    public Transform maxAngleStart;
    public Transform maxAngleEnd;
    public Transform minAngleStart;    
    public Transform minAngleEnd;

    public float maxDist;
    public float turnTrigggerOffset;

    public Transform crosshair;
    public Transform firePoint;
    public Transform weaponModel;

    public Transform player;
    
    private Vector2 mousePos;

    
    public static Vector2 mousePoint;
    private Vector2 direction;

    private bool turned = false;
	
	void Update () {
        Aim();        
    }

    private void Aim()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePoint = mousePos;
        direction = (mousePoint - (Vector2)firePoint.position);
        direction.Normalize();

        SetRadiusLimit();

        ClampAngle();

        CheckIfTurnPlayer();

        RotateWeapon();        
        
        crosshair.position = Camera.main.WorldToScreenPoint(mousePoint);
    }

    private void SetRadiusLimit()
    {        
        float mouseDist = direction.magnitude;

        if (mouseDist > maxDist)
        {
            mousePoint = (Vector2)firePoint.position + (direction.normalized * maxDist);
        }
    }

    private void ClampAngle()
    {
        // y3 = ((x3 - x2) y1 + (x1 - x3) y2) / (x1 - x2)        

        float maxPointY = ((mousePoint.x - maxAngleStart.position.x) * maxAngleEnd.position.y + (maxAngleEnd.position.x - mousePoint.x) * maxAngleStart.position.y) / (maxAngleEnd.position.x - maxAngleStart.position.x);

        float minPointY = ((mousePoint.x - minAngleStart.position.x) * minAngleEnd.position.y + (minAngleEnd.position.x - mousePoint.x) * minAngleStart.position.y) / (minAngleEnd.position.x - minAngleStart.position.x);

        mousePoint.y = Mathf.Clamp(mousePoint.y, minPointY, maxPointY);
    }

    private void RotateWeapon()
    {
        Vector2 _direction = mousePoint - (Vector2)transform.position;
        float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
        weaponModel.transform.rotation = Quaternion.Euler(0, 0, -90 + angle);
    }

    private void CheckIfTurnPlayer()
    {
        if ((mousePos.x < maxAngleStart.position.x && !turned) || (mousePos.x > maxAngleStart.position.x && turned))
        {
            mousePoint.x = maxAngleStart.position.x;
        }

        if ((mousePos.x < transform.position.x - turnTrigggerOffset && !turned) || (mousePos.x > transform.position.x - turnTrigggerOffset && turned))
        {
            TurnPlayer();
        }
    }

    private void TurnPlayer()
    {
        turned = !turned;
        player.localScale = new Vector3(-1 * player.localScale.x, player.localScale.y, 1);
        turnTrigggerOffset = -turnTrigggerOffset;
    }
}
