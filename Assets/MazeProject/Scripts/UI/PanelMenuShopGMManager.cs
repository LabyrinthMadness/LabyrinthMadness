using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelMenuShopGMManager : MonoBehaviour
{
    private struct Skill{
        public string name;
        public int skillCost;        

        public Skill(string name,int skillCost)
        {
            this.name = name;
            this.skillCost = skillCost;
        }
    }

    private Skill[] passiveSkill;
    private Skill[] activeSkill;

   
    //Passive item    
    public Button btnSlow;
    public Button btnMine;
    public Button btnWallBlock;
    public Button btnBuff;

    public Button btnElectricCable;
    public Button btnRhyno;
    public Button btnDog;
    public Button btnGhost;

    float timerElectricCable = 2.0f;
    float timerToActivateBomb = 3.0f;
    float timerSlowDebuf = 4.0f;
    bool isBuffed = false;
    
    public GameObject objPassiveSkill;
    public GameObject objActiveSkill;
    public GameObject objSkillList;

    public static PanelMenuShopGMManager Instance;
    
    public Text categoryText;
    //public HunterSkillCategory HunterSkillCat;
    public int categoryPos;

    private void Awake()
    {
        passiveSkill = new Skill[4];
        passiveSkill[0] = new Skill("Electric cable",10);
        passiveSkill[1] = new Skill("Ghost", 10);
        passiveSkill[2] = new Skill("Rhyno", 10);
        passiveSkill[3] = new Skill("Dog", 10);

        activeSkill = new Skill[4];
        activeSkill[0] = new Skill("Mine", 10);
        activeSkill[1] = new Skill("Slow", 10);
        activeSkill[2] = new Skill("Wall", 10);
        activeSkill[3] = new Skill("Buff", 10);
        Instance = this;
    }

    private void UpdateSkillList()
    {

    }

    public void ChangeSkillButton()
    {

    }
}
