using UnityEngine;
using UnityEngine.InputSystem;

public class SkillManager : MonoBehaviour
{
    [SerializeField]private SkillData _data;
    [SerializeField]private PlayerInput _playerInput;
    private float _cooldown;
    private bool inCooldown;
    private Vector3 _directionMouse;
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
        _cooldown = _data.cooldown;

    }
    private void OnUseSkill1(InputValue input) 
    {
        if (input.isPressed && inCooldown == false) 
        {
            Debug.Log("A");
            GameObject skillSpawn = Instantiate(_data._skillObject, transform.position, Quaternion.identity);
            OffensiveSkill component = skillSpawn.GetComponent<OffensiveSkill>();
            component.setDirectionSkill(_directionMouse.normalized);
            inCooldown = true;
        }
    }

    private void StartCooldown() 
    {
        if (inCooldown) 
        {
            _cooldown -= Time.deltaTime;
            if (_cooldown <= 0)
            {
                _cooldown = _data.cooldown;
                inCooldown = false;
            }
        }
    }

    /*private void CheckSkill() 
    {
        
    }*/
    void Update()
    {
        Vector2 mouseScreenPosition = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(mouseScreenPosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 mouseWorldPoint = hit.point;

            _directionMouse = mouseWorldPoint - transform.position;
            _directionMouse.Normalize();
        }
       // Vector3 mouseWorldPoint = Mouse.current.position.ReadValue();
       // _directionMouse = mouseWorldPoint - transform.position;
        Debug.Log("Direccion Oficial: "+_directionMouse);

        StartCooldown();
    }
}
