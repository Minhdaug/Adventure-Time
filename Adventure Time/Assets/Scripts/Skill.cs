using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BelongsTo
{
    Mercy, StoneGolem
}

public class Skill : MonoBehaviour
{
    [SerializeField] string name;
	[SerializeField] int level;
	[SerializeField] float scale;
	[SerializeField] BelongsTo belongsTo;
}
