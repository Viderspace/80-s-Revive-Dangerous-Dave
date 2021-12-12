using System;
using UnityEngine;

public class CamTrigger : MonoBehaviour
/*
 This Script is attached to a "Cam trigger" (Game object).
 Cam trigger's are box-shaped invisible objects placed inside a level, with a collider attached to them.
 So whenever Dave crosses a curtain cam Trigger in the level, the camera follows him to the relevant direction.
*/

{
    #region Inspector

    [SerializeField] private float panAmount;
    [SerializeField] private float panSpeed = 1f;
    #endregion

    
    #region Fields

    private bool _initState;
    private Camera _mainCam;
    private bool _moveCam;
    private Vector3 _targetPos;



    #endregion


    #region Methods

    private void PanCamera()
    {
        _mainCam.transform.position = Vector3.MoveTowards(_mainCam.transform.position,
            _targetPos, panSpeed);
        if (_mainCam.transform.position == _targetPos) _moveCam = false;
    }
    

    #endregion
    

    #region MonoBehaviour

    private void Start()
    {
        _initState = true;
        _mainCam = FindObjectOfType<Camera>();
    }

    private void OnTriggerExit2D(Collider2D other) {
        
        if (!other.gameObject.CompareTag("Dave")) return;

        switch (_initState)
        {
            //camera moves right.
            case true when other.transform.position.x > gameObject.transform.position.x:
                _targetPos = _mainCam.transform.position + Vector3.right * panAmount;
                _initState = false;
                break;
            //camera moves back again.
            case false when other.transform.position.x < gameObject.transform.position.x:
                _targetPos = _mainCam.transform.position + Vector3.left * panAmount;
                _initState = true;
                break;
        }
        
        _moveCam = true;
    }

    private void FixedUpdate()
    {
        if (_moveCam)
        {
            PanCamera();
        }

        
    }

    #endregion
}