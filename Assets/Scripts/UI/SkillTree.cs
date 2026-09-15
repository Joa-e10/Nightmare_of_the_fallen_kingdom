using System;
using System.Collections.Generic;
using UnityEngine;

public class SkillTree : MonoBehaviour
{
    public List<SkillSlot> _ListOfSkills = new List<SkillSlot>();
    private Player _player;

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



}
