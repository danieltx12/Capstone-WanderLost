﻿using System;
using UnityEngine;

namespace MovingPlatformMaker2D {

	public static class Utils {

		public static float Round2(float f) {
			return Mathf.Round (f * 100f) / 100f;
		}
	}

}