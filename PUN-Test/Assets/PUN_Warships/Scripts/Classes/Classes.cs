using UnityEngine;
using System.Collections.Generic;
using System;

public class Classes{}


[System.Serializable]
public class PlayerStats
{
    public List<Ship> listOfShips;
	
	//all stats, resources ...
	public int gold;
	public int pearls;
	public int rum;
	public int wood;

	public int level = 1;

	public int experience = 1;
}

[System.Serializable]
public class Ship
{
	//single ship stats ...
	public string shipName;
	public int tier;
	public int strength;
	public int speed;
	public int turnSpeed;
	//public int health;

	public int attackValue;

	public int[] shipSkills = {0, 0, 0, 0};
	public string[] shipSkillsName = {"Strength", "Cannons Power", "Speed", "Turn Speed"}; ///NEED TO DESTROY THIS: - find save file and delete it

	#region Cannons
		public int singleCannonDmg;
		public int numberOfCannons;
	#endregion

	public string pathTo;

	public Ship(){}

	public Ship(string _shipName, int _tier /*  int _strength  */, int _speed, int _turnSpeed, int _strength, int _singleCannonDmg, int _numbOfCannons){
		shipName = _shipName;
		tier = _tier;
		speed = _speed;
		turnSpeed = _turnSpeed;
		strength = _strength;
		singleCannonDmg = _singleCannonDmg;
		numberOfCannons = _numbOfCannons;

		attackValue = _singleCannonDmg * _numbOfCannons;

		//strength = _singleCannonDmg * _numbOfCannons; // calculate by stats (numb of cannons * single canon dmg)

		pathTo = "Ships/Cartoon/" + _shipName;
	}
   
}

