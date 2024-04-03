using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
	[SerializeField] private Level[] levels;
	[SerializeField]private List<Prop> props;

	private Level currentLevel;

	public int PropsLeft => props.Count;
	public int propsleft;

	private void Awake() {
		currentLevel = levels[0];
	}

	private void Start() {
		currentLevel.OnInit();
		props = currentLevel.props;
		propsleft = PropsLeft;
	}

	public void OnPropInBucket(Prop prop) {
		if (props.Contains(prop)) {
			props.Remove(prop);
			propsleft = PropsLeft;
		}
	}

}
