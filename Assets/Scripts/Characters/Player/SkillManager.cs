using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class SkillManager : MonoBehaviour
{
    [SerializeField] private SkillData[] _listSkill = new SkillData[2];
    [SerializeField] private PlayerInput _playerInput;
    private bool inCooldownQ;
    private bool inCooldownE;
    private Player _player;

    private Camera _camera;
    private Vector3 _directionMouse;
    private void Start()
    {
        _camera = Camera.main;
        _player = GetComponent<Player>();
    }
    private void OnUseSkill1(InputValue input)
    {
        if (input.isPressed && inCooldownQ == false)
        {
            if (_listSkill[0] != null)
            {
                CheckSkill(_listSkill[0]);
                Debug.Log("A");
                inCooldownQ = true;
                StartCoroutine(CooldownQ(_listSkill[0].cooldown));
            }
            else 
            {
                Debug.Log("No hay habilidad asignada");
            }
        }
    }

    private void OnUseSkill2(InputValue input)
    {
        if (input.isPressed && inCooldownE == false)
        {
            if (_listSkill[1] != null)
            {
                CheckSkill(_listSkill[1]);
                Debug.Log("E");
                inCooldownE = true;
                StartCoroutine(CooldownE(_listSkill[1].cooldown));
            }
            else
            {
                Debug.Log("No hay habilidad asignada");
            }
        }
    }

    private IEnumerator CooldownQ(float timeCooldown)
    {
        if (inCooldownQ == true)
        {
            yield return new WaitForSeconds(timeCooldown);
            inCooldownQ = false;
        }
    }

    private IEnumerator CooldownE(float timeCooldown)
    {
        if (inCooldownE == true)
        {
            yield return new WaitForSeconds(timeCooldown);
            inCooldownE = false;
        }
    }

    private void CheckSkill(SkillData skillInSlot) 
    {
        if (skillInSlot.type == SkillData.TypeSkill.offensive)
        {
            OffensiveSkill comp = skillInSlot._skillObject.GetComponent<OffensiveSkill>();
            comp.setDirectionSkill(_directionMouse);
            comp.setPosition(transform);
            comp.ActivateAbility();
        }
        else 
        {
            GameObject skillSpawn = Instantiate(skillInSlot._skillObject, transform.position, Quaternion.identity);
            DefensiveSkill comp = skillSpawn.GetComponent<DefensiveSkill>();
            comp.SetComponentPlayer(_player);
            comp.ActivateAbility();
        }
    }
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
    }
}
