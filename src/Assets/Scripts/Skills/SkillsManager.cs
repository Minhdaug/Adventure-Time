using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkillsManager : MonoBehaviour
{
    private Dictionary<string, Skill> _skillsDict = new Dictionary<string, Skill>();
	public Dictionary<string, Skill> SkillsDict { get { return _skillsDict; } }
	

	private Dictionary<string, List<Skill>> _characterSkills = new Dictionary<string, List<Skill>>();
	public Dictionary<string, List<Skill>> CharacterSkills 
	{ 
		get { return _characterSkills; }
		set { _characterSkills = value; }
	}

private void Awake()
	{
		NormalAttack normalAttack = ScriptableObject.CreateInstance<NormalAttack>();
		normalAttack.SkillName = "Normal Attack";
		normalAttack.AoE = AoE.One;
		normalAttack.ManaCost = 0;
		normalAttack.SkillType = SkillType.Active;
		normalAttack.TargetType = Target.Enemy;

		SkillPhysical skillPhysical1 = ScriptableObject.CreateInstance<SkillPhysical>();
		skillPhysical1.SkillName = "Physical Attack 1";
		skillPhysical1.AoE = AoE.One;
		skillPhysical1.SkillStat = 65;
		skillPhysical1.ManaCost = 4;
		skillPhysical1.SkillType = SkillType.Active;
		skillPhysical1.TargetType = Target.Enemy;

		SkillPhysical skillPhysical2 = ScriptableObject.CreateInstance<SkillPhysical>();
		skillPhysical2.SkillName = "Physical Attack 2";
		skillPhysical2.AoE = AoE.All;
		skillPhysical2.SkillStat = 70;
		skillPhysical2.ManaCost = 10;
		skillPhysical2.SkillType = SkillType.Active;
		skillPhysical2.TargetType = Target.Enemy;

		SkillPhysical skillPhysical3 = ScriptableObject.CreateInstance<SkillPhysical>();
		skillPhysical3.SkillName = "Physical Attack 3";
		skillPhysical3.AoE = AoE.One;
		skillPhysical3.SkillStat = 90;
		skillPhysical3.ManaCost = 8;
		skillPhysical3.SkillType = SkillType.Active;
		skillPhysical3.TargetType = Target.Enemy;

		SkillPhysical skillPhysical4 = ScriptableObject.CreateInstance<SkillPhysical>();
		skillPhysical4.SkillName = "Physical Attack 4";
		skillPhysical4.AoE = AoE.One;
		skillPhysical4.SkillStat = 100;
		skillPhysical4.ManaCost = 16;
		skillPhysical4.SkillType = SkillType.Active;
		skillPhysical4.TargetType = Target.Enemy;

		_skillsDict.Add(skillPhysical1.SkillName, skillPhysical1);
		_skillsDict.Add(skillPhysical2.SkillName, skillPhysical2);
		_skillsDict.Add(skillPhysical3.SkillName, skillPhysical3);
		_skillsDict.Add(skillPhysical4.SkillName, skillPhysical4);

		SkillFire skillFire1 = ScriptableObject.CreateInstance<SkillFire>();
		skillFire1.SkillName = "Fire Attack 1";
		skillFire1.AoE = AoE.One;
		skillFire1.ElementType = ElementType.Fire;
		skillFire1.SkillStat = 40;
		skillFire1.ManaCost = 4;
		skillFire1.SkillType = SkillType.Active;
		skillFire1.TargetType = Target.Enemy;

		SkillFire skillFire2 = ScriptableObject.CreateInstance<SkillFire>();
		skillFire2.SkillName = "Fire Attack 2";
		skillFire2.AoE = AoE.All;
		skillFire1.ElementType = ElementType.Fire;
		skillFire2.SkillStat = 40;
		skillFire2.ManaCost = 10;
		skillFire1.SkillType = SkillType.Active;
		skillFire1.TargetType = Target.Enemy;

		SkillFire skillFire3 = ScriptableObject.CreateInstance<SkillFire>();
		skillFire3.SkillName = "Fire Attack 3";
		skillFire3.AoE = AoE.One;
		skillFire1.ElementType = ElementType.Fire;
		skillFire3.SkillStat = 90;
		skillFire3.ManaCost = 8;
		skillFire1.SkillType = SkillType.Active;
		skillFire1.TargetType = Target.Enemy;

		SkillFire skillFire4 = ScriptableObject.CreateInstance<SkillFire>();
		skillFire4.SkillName = "Fire Attack 4";
		skillFire4.AoE = AoE.All;
		skillFire1.ElementType = ElementType.Fire;
		skillFire4.SkillStat = 90;
		skillFire4.ManaCost = 16;
		skillFire1.SkillType = SkillType.Active;
		skillFire1.TargetType = Target.Enemy;

		_skillsDict.Add(skillFire1.SkillName, skillFire1);
		_skillsDict.Add(skillFire2.SkillName, skillFire2);
		_skillsDict.Add(skillFire3.SkillName, skillFire3);
		_skillsDict.Add(skillFire4.SkillName, skillFire4);


		SkillWater skillWater1 = ScriptableObject.CreateInstance<SkillWater>();
		skillWater1.SkillName = "Water Attack 1";
		skillWater1.AoE = AoE.One;
		skillWater1.ElementType = ElementType.Water;
		skillWater1.SkillStat = 40;
		skillWater1.ManaCost = 4;
		skillWater1.SkillType = SkillType.Active;
		skillWater1.TargetType = Target.Enemy;

		SkillWater skillWater2 = ScriptableObject.CreateInstance<SkillWater>();
		skillWater2.SkillName = "Water Attack 2";
		skillWater2.AoE = AoE.All;
		skillWater2.ElementType = ElementType.Water;
		skillWater2.SkillStat = 40;
		skillWater2.ManaCost = 10;
		skillWater2.SkillType = SkillType.Active;
		skillWater2.TargetType = Target.Enemy;

		SkillWater skillWater3 = ScriptableObject.CreateInstance<SkillWater>();
		skillWater3.SkillName = "Water Attack 3";
		skillWater3.AoE = AoE.One;
		skillWater3.ElementType = ElementType.Water;
		skillWater3.SkillStat = 90;
		skillWater3.ManaCost = 8;
		skillWater3.SkillType = SkillType.Active;
		skillWater3.TargetType = Target.Enemy;

		SkillWater skillWater4 = ScriptableObject.CreateInstance<SkillWater>();
		skillWater4.SkillName = "Water Attack 4";
		skillWater4.AoE = AoE.All;
		skillWater4.ElementType = ElementType.Water;
		skillWater4.SkillStat = 90;
		skillWater4.ManaCost = 16;
		skillWater4.SkillType = SkillType.Active;
		skillWater4.TargetType = Target.Enemy;

		_skillsDict.Add(skillWater1.SkillName, skillWater1);
		_skillsDict.Add(skillWater2.SkillName, skillWater2);
		_skillsDict.Add(skillWater3.SkillName, skillWater3);
		_skillsDict.Add(skillWater4.SkillName, skillWater4);


		SkillPoison skillPoison1 = ScriptableObject.CreateInstance<SkillPoison>();
		skillPoison1.SkillName = "Poison Attack 1";
		skillPoison1.AoE = AoE.One;
		skillPoison1.ElementType = ElementType.Poison;
		skillPoison1.SkillStat = 40;
		skillPoison1.ManaCost = 4;
		skillPoison1.SkillType = SkillType.Active;
		skillPoison1.TargetType = Target.Enemy;

		SkillPoison skillPoison2 = ScriptableObject.CreateInstance<SkillPoison>();
		skillPoison2.SkillName = "Poison Attack 2";
		skillPoison2.AoE = AoE.All;
		skillPoison2.ElementType = ElementType.Poison;
		skillPoison2.SkillStat = 40;
		skillPoison2.ManaCost = 10;
		skillPoison2.SkillType = SkillType.Active;
		skillPoison2.TargetType = Target.Enemy;

		SkillPoison skillPoison3 = ScriptableObject.CreateInstance<SkillPoison>();
		skillPoison3.SkillName = "Poison Attack 3";
		skillPoison3.AoE = AoE.One;
		skillPoison3.ElementType = ElementType.Poison;
		skillPoison3.SkillStat = 90;
		skillPoison3.ManaCost = 8;
		skillPoison3.SkillType = SkillType.Active;
		skillPoison3.TargetType = Target.Enemy;

		SkillPoison skillPoison4 = ScriptableObject.CreateInstance<SkillPoison>();
		skillPoison4.SkillName = "Poison Attack 4";
		skillPoison4.AoE = AoE.All;
		skillPoison4.ElementType = ElementType.Poison;
		skillPoison4.SkillStat = 90;
		skillPoison4.ManaCost = 16;
		skillPoison4.SkillType = SkillType.Active;
		skillPoison4.TargetType = Target.Enemy;

		_skillsDict.Add(skillPoison1.SkillName, skillPoison1);
		_skillsDict.Add(skillPoison2.SkillName, skillPoison2);
		_skillsDict.Add(skillPoison3.SkillName, skillPoison3);
		_skillsDict.Add(skillPoison4.SkillName, skillPoison4);


		SkillHeal skillHeal1 = ScriptableObject.CreateInstance<SkillHeal>();
		skillHeal1.SkillName = "Heal 1";
		skillHeal1.AoE = AoE.One;
		skillHeal1.ManaCost = 4;
		skillHeal1.HealthRecoveryRate = HealthRecoveryRate.Percentage30;
		skillHeal1.SkillType = SkillType.Active;
		skillHeal1.TargetType = Target.Enemy;

		SkillHeal skillHeal2 = ScriptableObject.CreateInstance<SkillHeal>();
		skillHeal2.SkillName = "Heal 2";
		skillHeal2.AoE = AoE.All;
		skillHeal2.ManaCost = 10;
		skillHeal2.HealthRecoveryRate = HealthRecoveryRate.Percentage30;
		skillHeal2.SkillType = SkillType.Active;
		skillHeal2.TargetType = Target.Enemy;

		SkillHeal skillHeal3 = ScriptableObject.CreateInstance<SkillHeal>();
		skillHeal3.SkillName = "Heal 3";
		skillHeal3.AoE = AoE.One;
		skillHeal3.ManaCost = 8;
		skillHeal3.HealthRecoveryRate = HealthRecoveryRate.Percentage50;
		skillHeal3.SkillType = SkillType.Active;
		skillHeal3.TargetType = Target.Enemy;

		SkillHeal skillHeal4 = ScriptableObject.CreateInstance<SkillHeal>();
		skillHeal4.SkillName = "Heal 4";
		skillHeal4.AoE = AoE.All;
		skillHeal4.ManaCost = 16;
		skillHeal4.HealthRecoveryRate = HealthRecoveryRate.Percentage50;
		skillHeal4.SkillType = SkillType.Active;
		skillHeal4.TargetType = Target.Enemy;

		_skillsDict.Add(skillHeal1.SkillName, skillHeal1);
		_skillsDict.Add(skillHeal2.SkillName, skillHeal2);
		_skillsDict.Add(skillHeal3.SkillName, skillHeal3);
		_skillsDict.Add(skillHeal4.SkillName, skillHeal4);


		SkillBuff skillAttackBuff = ScriptableObject.CreateInstance<SkillBuff>();
		skillAttackBuff.SkillName = "Attack Buff";
		skillAttackBuff.AoE = AoE.All;
		skillAttackBuff.ManaCost = 8;
		skillAttackBuff.BuffType = Buffs.Attack;
		skillAttackBuff.SkillType = SkillType.Active;
		skillAttackBuff.TargetType = Target.Enemy;

		SkillBuff skillAttackBuffPlus = ScriptableObject.CreateInstance<SkillBuff>();
		skillAttackBuffPlus.SkillName = "Attack Buff Plus";
		skillAttackBuffPlus.AoE = AoE.All;
		skillAttackBuffPlus.ManaCost = 24;
		skillAttackBuffPlus.BuffType = Buffs.Attack;
		skillAttackBuffPlus.SkillType = SkillType.Active;
		skillAttackBuffPlus.TargetType = Target.Enemy;

		SkillBuff skillDefenseBuff = ScriptableObject.CreateInstance<SkillBuff>();
		skillDefenseBuff.SkillName = "Defense Buff";
		skillDefenseBuff.AoE = AoE.All;
		skillDefenseBuff.ManaCost = 8;
		skillDefenseBuff.BuffType = Buffs.Defense;
		skillDefenseBuff.SkillType = SkillType.Active;
		skillDefenseBuff.TargetType = Target.Enemy;

		SkillBuff skillDefenseBuffPlus = ScriptableObject.CreateInstance<SkillBuff>();
		skillDefenseBuffPlus.SkillName = "Defense Buff Plus";
		skillDefenseBuffPlus.AoE = AoE.All;
		skillDefenseBuffPlus.ManaCost = 24;
		skillDefenseBuffPlus.BuffType = Buffs.Defense;
		skillDefenseBuffPlus.SkillType = SkillType.Active;
		skillDefenseBuffPlus.TargetType = Target.Enemy;

		_skillsDict.Add(skillAttackBuff.SkillName, skillAttackBuff);
		_skillsDict.Add(skillAttackBuffPlus.SkillName, skillAttackBuffPlus);
		_skillsDict.Add(skillDefenseBuff.SkillName, skillDefenseBuff);
		_skillsDict.Add(skillDefenseBuffPlus.SkillName, skillDefenseBuffPlus);


		SkillPassiveHealthRecovery skillHealthRecovery1 = ScriptableObject.CreateInstance<SkillPassiveHealthRecovery>();
		skillHealthRecovery1.SkillName = "Health Recovery 1";
		skillHealthRecovery1.SelfRecoveryType = SelfRecoveryTypes.Health;
		skillHealthRecovery1.RegenerationRate = RegenerationRate.Percentage2;
		skillHealthRecovery1.SkillType = SkillType.Active;
		skillHealthRecovery1.TargetType = Target.Enemy;

		SkillPassiveHealthRecovery skillHealthRecovery2 = ScriptableObject.CreateInstance<SkillPassiveHealthRecovery>();
		skillHealthRecovery2.SkillName = "Health Recovery 2";
		skillHealthRecovery2.SelfRecoveryType = SelfRecoveryTypes.Health;
		skillHealthRecovery2.RegenerationRate = RegenerationRate.Percentage4;
		skillHealthRecovery2.SkillType = SkillType.Active;
		skillHealthRecovery2.TargetType = Target.Enemy;


		SkillPassiveManaRecovery skillManaRecovery1 = ScriptableObject.CreateInstance<SkillPassiveManaRecovery>();
		skillManaRecovery1.SkillName = "Mana Recovery 1";
		skillManaRecovery1.SelfRecoveryType = SelfRecoveryTypes.Health;
		skillManaRecovery1.ManaRecoveryRate = ManaRecoveryRate.ManaUnit3;
		skillManaRecovery1.SkillType = SkillType.Active;
		skillManaRecovery1.TargetType = Target.Enemy;

		SkillPassiveManaRecovery skillManaRecovery2 = ScriptableObject.CreateInstance<SkillPassiveManaRecovery>();
		skillManaRecovery2.SkillName = "Mana Recovery 2";
		skillManaRecovery2.SelfRecoveryType = SelfRecoveryTypes.Health;
		skillManaRecovery2.ManaRecoveryRate = ManaRecoveryRate.ManaUnit5;
		skillManaRecovery2.SkillType = SkillType.Active;
		skillManaRecovery2.TargetType = Target.Enemy;

		_skillsDict.Add(skillHealthRecovery1.SkillName, skillHealthRecovery1);
		_skillsDict.Add(skillHealthRecovery2.SkillName, skillHealthRecovery2);
		_skillsDict.Add(skillManaRecovery1.SkillName, skillManaRecovery1);
		_skillsDict.Add(skillManaRecovery2.SkillName, skillManaRecovery2);

		// Heroes
		_characterSkills.Add("Mercy", new List<Skill>() 
		{ 
			normalAttack,
			skillPhysical1, skillHeal1, skillDefenseBuff, skillPhysical2,
			skillHealthRecovery1, skillPhysical3, skillPoison3,
			skillDefenseBuffPlus, skillHealthRecovery2, skillPhysical4
		});
		_characterSkills.Add("Inirius", new List<Skill>()
		{
			normalAttack,
			skillFire1, skillWater1, skillFire2, skillWater2,
			skillManaRecovery1, skillFire3, skillWater3,
			skillManaRecovery2, skillFire4, skillWater4, 
		});
		_characterSkills.Add("Eni", new List<Skill>()
		{
			normalAttack,
			skillHeal1, skillPoison1, skillAttackBuff, skillHeal2, 
			skillManaRecovery1, skillPoison2, skillHeal3,
			skillAttackBuffPlus, skillManaRecovery2, skillHeal4 
		});


		// Bosses
		_characterSkills.Add("Golem", new List<Skill>
		{
			normalAttack,
			skillPhysical1, skillPhysical2, skillWater2, skillPoison1
		});
		_characterSkills.Add("Death", new List<Skill>
		{
			normalAttack,
			skillPhysical2, skillFire3, skillPhysical3, skillPoison2
		});
		_characterSkills.Add("Dark Sorcerer", new List<Skill>
		{
			normalAttack,
			skillFire4, skillWater4, skillPhysical3, skillPoison4
		});

		// Mobs
		_characterSkills.Add("Slime", new List<Skill>
		{
			normalAttack,
			skillWater1
		});
		_characterSkills.Add("Bat", new List<Skill>
		{
			normalAttack,
			skillFire1 
		});
		_characterSkills.Add("Goblin", new List<Skill>
		{
			normalAttack,
			skillPhysical1 
		});
		_characterSkills.Add("Skeleton", new List<Skill> {
			normalAttack,
			skillPhysical2, skillWater2
		});
		_characterSkills.Add("Zombie Warrior", new List<Skill> {
			normalAttack,
			skillFire2, skillHeal1
		});
		_characterSkills.Add("Skull", new List<Skill> {
			normalAttack,
			skillPoison3, skillFire3, skillWater3
		});

	}
	public Skill GetSkill(string skillName)
	{
		if (_skillsDict.ContainsKey(skillName))
		{
			return _skillsDict[skillName];
		}
		return null;
	}

	public List<Skill> GetHeroSkill (string heroName, int currentLevel)
	{
		//Debug.Log($"Running for {heroName}");
		if (_characterSkills.ContainsKey(heroName))
		{
			List<Skill> heroSkillList = _characterSkills[heroName].ToList();
			List<Skill> result = new List<Skill>();

			result.Add(heroSkillList[0]);
			heroSkillList.Remove(heroSkillList[0]);

			for (int i = 1; i <= currentLevel; i++)
			{
				if (heroSkillList.Count == 0)
				{
					break;
				}
				if (i % 5 == 0)
				{
					Skill skillToReplace = result.FirstOrDefault(skill => skill.GetType() == heroSkillList[0].GetType());

					if (skillToReplace != null)
					{
						result[result.IndexOf(skillToReplace)] = heroSkillList[0];
					}
					else
					{
						result.Add(heroSkillList[0]);
					}
					heroSkillList.RemoveAt(0);
				}
			}
			return result;
		}
		else
		{
			throw new System.ArgumentException($"Hero {heroName} does not exists.");
		}
	}

	public List<Skill> GetEnemySkill(string enemyName)
	{
		if (_characterSkills.ContainsKey(enemyName))
		{
			return _characterSkills[enemyName].ToList();
		}
		else
		{
			throw new System.ArgumentException($"Enemy {enemyName} does not exists.");
		}
	}
}
