using System.Collections.Generic;
using UnityEngine;

public class CacheComponent {
	private static Dictionary<Collider2D, Prop> props = new Dictionary<Collider2D, Prop>();

	public static Prop GetProp(Collider2D col) {
		if (!props.ContainsKey(col)) {
			props.Add(col, col.GetComponent<Prop>());
		}

		return props[col];
	}

	private static Dictionary<float, WaitForSeconds> WFS = new Dictionary<float, WaitForSeconds>();

	public static WaitForSeconds GetWFS(float time) {
		if (!WFS.ContainsKey(time)) {
			WFS.Add(time, new WaitForSeconds(time));
		}

		return WFS[time];
	}
}