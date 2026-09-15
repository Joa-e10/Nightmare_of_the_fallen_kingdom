using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using static SkillTree;

public class SkillTreeUI : MonoBehaviour
{
    private SkillTree _skillTree;
    [SerializeField] private List<Transform> _treeLimit = new List<Transform>();
    [SerializeField] private List<GameObject> _slotsA = new List<GameObject>();
    [SerializeField] private GameObject _slotPrefab;
    private NetworkObject _playerObject;

    public void TakeOwner()
    {
        foreach (var client in NetworkManager.Singleton.ConnectedClientsList)
        {
            if (client.PlayerObject.IsOwner)
            {

                _playerObject = client.PlayerObject;
                _skillTree = client.PlayerObject.GetComponent<SkillTree>();
            }
        }
    }
    public void RefreshTreeUI()
    {

        TakeOwner();
        int index = 0;
        foreach (GameObject slot in _slotsA)
        {
            Destroy(slot.gameObject);
        }
        _slotsA.Clear();

        foreach (SkillSlot item in _skillTree._ListOfSkills)
        {
            DataSkill data = item.data;

            GameObject newSlot = Instantiate(_slotPrefab, _treeLimit[index]);
            _slotsA.Add(newSlot);
            SlotBranch slotAttributes = newSlot.GetComponent<SlotBranch>();
            slotAttributes._textQuantity.text = data.quantityPoints.ToString();
            slotAttributes._icon.sprite = data.icon;
            slotAttributes._skillData = data;

            index++;
        }

    }

    
    private void Update()
    {
       // RefreshTreeUI();
    }
}
