using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
	[SerializeField] private Level[] levels;
	[SerializeField] private List<Prop> props = new List<Prop>(); // thang nay cung de private

	private Container currentContainer;
	private Level currentLevel;

	[SerializeField]private int propsLeft; //thang nay de private

	private void Start() {
		currentLevel = Instantiate(levels[0]);
		currentContainer = currentLevel.container;
		OnInit();
	}

	private void OnInit() {
		currentContainer.OnInit();
		currentLevel.OnInit();
		propsLeft = currentLevel.TotalProps;
	}

	public void OnPropCollect(Prop prop) {
		if (!props.Contains(prop)) {
			props.Add(prop);
			propsLeft--;
		}
	}
	
	public void OnPropExit(Prop prop) {
		if (props.Contains(prop)) {
			props.Remove(prop);
			propsLeft++;
		}
	}
}
