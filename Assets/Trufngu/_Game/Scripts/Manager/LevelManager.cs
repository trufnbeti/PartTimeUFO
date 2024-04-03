using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class LevelManager : Singleton<LevelManager>
{
	[SerializeField] private Level[] levels;
	[SerializeField]private List<Prop> props;

	private Level currentLevel;

	[SerializeField] private int propsLeft;

	private void Awake() {
		currentLevel = levels[0];
	}

	private void Start() {
		currentLevel.OnInit();
		props = currentLevel.props;
		propsLeft = currentLevel.totalProps;
	}

	public void OnPropInBucket(Prop prop) {
		--propsLeft;
	}

}
