using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using PrototypeGameplayDragPin;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
	[SerializeField] private Level[] levels;
	[SerializeField] private List<Prop> props = new List<Prop>(); // thang nay de private

	private Level currentLevel;
	private int levelIdx;
	
	[SerializeField]private int propsLeft; //thang nay cung de private
	
	#region HangingLevel

	private List<PinPoint> pinPoints = new List<PinPoint>();
	public bool IsHangingLevel => pinPoints.Count > 0; 

	#endregion
	private void Start() {
		levelIdx = 0;
		OnLoadLevel(levelIdx);
		OnInit();
	}

	private void OnInit() {
		currentLevel.OnInit();
		propsLeft = currentLevel.TotalProps;
		pinPoints = currentLevel.pinPoints;
	}

	private void OnLoadLevel(int index) {
		if (currentLevel) {
			Destroy(currentLevel.gameObject);
		}
		
		currentLevel = Instantiate(levels[index]);
	}

	public void OnPropCollect(Prop prop) {
		if (!props.Contains(prop)) {
			props.Add(prop);
			propsLeft--;
			if (propsLeft == 0) {
				UIManager.Ins.OpenUI<UINEXT>();
			}
		}
	}

	#region HangingLevel

	public void CheckPin(Prop prop) {
		if (!IsHangingLevel) return;
		for (int i = 0; i < pinPoints.Count; ++i) {
			if (pinPoints[i].IsPointInsidePin(prop.transform.TransformPoint(prop.AnchorJoint))) {
				pinPoints[i].CreateSpringJoint(prop);
			}
		}
	}
	
	#endregion

	public void OnNextLevel() {
		++levelIdx;
		OnLoadLevel(levelIdx);
		OnInit();
	}
	
	
	public void OnPropExit(Prop prop) {
		if (props.Contains(prop)) {
			props.Remove(prop);
			propsLeft++;
		}
	}
}
