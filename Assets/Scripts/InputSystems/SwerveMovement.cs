using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwerveMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] float forwardSpeed;
    [SerializeField] float swerveSpeed;

    [Header("Boundary Settings")]
    [SerializeField] float xMin;
    [SerializeField] float xmax;

    [Header("Debug")]
    [SerializeField] Vector3 startingPos;
    [SerializeField] Vector3 currentPos;
    [SerializeField] float targetX;

    // Update is called once per frame
    void Update()
    {
        if(!GameManager.instance.isLevelActive) return;

        if (LevelManager.instance.gamePhases == GamePhases.BlockRace)
        {
            transform.position += transform.forward * Time.deltaTime * forwardSpeed;
        }

        else if (LevelManager.instance.gamePhases == GamePhases.Shooting)
        {
            if(Input.GetMouseButtonDown(0))
            {
                startingPos = Input.mousePosition;
            }
            if (Input.GetMouseButton(0))
            {
                transform.position += transform.forward * Time.deltaTime * forwardSpeed;

                
                currentPos = Input.mousePosition;

                float swerveDeltaX = (currentPos.x - startingPos.x);

                targetX = Mathf.Lerp(targetX, swerveDeltaX, 25 * Time.deltaTime);
                Vector3 targetVec = new Vector3(targetX,0,0) * swerveSpeed * Time.deltaTime;
                transform.position += targetVec;

                startingPos = Input.mousePosition;

                ClampWithBoundaries();
            }
            if (Input.GetMouseButtonUp(0))
            {
                startingPos = Vector3.zero;
                currentPos = Vector3.zero;

            }
        }
    }

    private void ClampWithBoundaries()
    {

        transform.position = new Vector3(

                    Mathf.Clamp(transform.position.x, xMin, xmax),
                    transform.position.y,
                    transform.position.z);
    }
}
