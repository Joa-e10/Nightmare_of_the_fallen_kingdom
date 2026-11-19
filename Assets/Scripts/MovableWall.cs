using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovableWall : MonoBehaviour
{
    public enum axisChanged { X, Y, Z };
    public axisChanged _axis;
    [SerializeField] private Transform _transformMove;
    [SerializeField] private int _limitOpen;
    [SerializeField]private bool _isOpen;
    //private bool _isPermitted;

    void OnEnable()
    {
        if (_isOpen)
        {
            Player.OnTestPermission += OpenWall;
        }
        else 
        {
            Player.OnTestPermission += ClosedWall;
        }
    }

    void OnDisable()
    {
        if (_isOpen)
        {
            Player.OnTestPermission -= OpenWall;
        }
        else
        {
            Player.OnTestPermission -= ClosedWall;
        }
    }

    /*private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();

        if (player != null && _isPermitted)
        {
            OpenWall();
        }

    }

    private void OnTriggerExit(Collider other)
    {
        Player player = other.GetComponent<Player>();
        if (player != null && _isPermitted)
        {
            ClosedWall();
        }
    }

    public void SetActivePermission()
    {
        _isPermitted = true;
    }*/

    public void OpenWall()
    {
        switch (_axis.ToString())
        {
            case "X":
                _transformMove.position = new Vector3(transform.position.x + _limitOpen, transform.position.y, _transformMove.position.z);
                break;
            case "Y":
                _transformMove.position = new Vector3(transform.position.x, _transformMove.position.y + _limitOpen, transform.position.z);
                break;
            case "Z":
                _transformMove.position = new Vector3(_transformMove.position.x, transform.position.y, transform.position.z + _limitOpen);
                break;
        }
    }
    public void ClosedWall()
    {
        switch (_axis.ToString())
        {
            case "X":
                _transformMove.position = new Vector3(transform.position.x - _limitOpen, transform.position.y, _transformMove.position.z);
                break;
            case "Y":
                _transformMove.position = new Vector3(transform.position.x, _transformMove.position.y - _limitOpen, transform.position.z);
                break;
            case "Z":
                _transformMove.position = new Vector3(_transformMove.position.x, transform.position.y, transform.position.z - _limitOpen);
                break;
        }
    }
}
