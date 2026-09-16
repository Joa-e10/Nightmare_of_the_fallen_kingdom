using System;
using System.Collections.Generic;
using UnityEngine;

public class SkillTree : MonoBehaviour
{
    public List<SkillSlot> _ListOfSkills = new List<SkillSlot>();
    private Player _player;
    private int _points;

    private void OnEnable()
    {
        _player = GetComponent<Player>();
    }

    [Serializable]
    public class SkillSlot
    {
        public DataSkill data;
        public enum accessType { available, blocked, inaccessible};
        public accessType access;
        public SkillSlot(DataSkill Newdata, accessType Newaccess)
        {
            this.data = Newdata;
            this.access = Newaccess;
        }
    }

    public void RedeemPoints(int requiredPoints, DataSkill currentData)
    {
        _points = _player.pointsSkills;

        if (_points >= requiredPoints)
        {
            _points -= requiredPoints;

            foreach (var skill in _ListOfSkills) 
            {
                if (currentData == skill.data) 
                {
                    skill.access = SkillSlot.accessType.available;
                }
            }
        }
        else 
        {
            Debug.Log("No tienes suficientes puntos bro");
        }
    }

}
