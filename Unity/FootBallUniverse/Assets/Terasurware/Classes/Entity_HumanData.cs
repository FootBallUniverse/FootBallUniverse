using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Entity_HumanData : ScriptableObject
{	
	public List<Param> param = new List<Param> ();

	[System.SerializableAttribute]
	public class Param
	{
		
		public int id;
		public string jpn_name;
		public string eng_name;
		public float player_speed;
	}
}