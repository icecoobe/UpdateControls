/**********************************************************************
 * 
 * Update Controls .NET
 * Copyright 2011 Michael L Perry
 * MIT License
 * 
 * This class based on a contribution by David Piepgrass.
 * 
 * http://updatecontrols.net
 * http://www.codeplex.com/updatecontrols/
 * 
 **********************************************************************/

using System;
using System.ComponentModel;

namespace UpdateControls.Fields
{
	public class Dependent<T> : Dependent
	{
		protected internal T _value;
		protected Func<T> _computeValue;

		public Dependent(Func<T> compute) : base(null)
		{
			base._update = Update; _computeValue = compute;
		}

		protected void Update()
		{
			_value = _computeValue();
			// TODO: don't propagate updates when _value did not change.
			//    T oldValue = _value;
			//    _value = _computeValue();
			//    return _value == null ? oldValue != null : !_value.Equals(oldValue);
		}

		public T Value
		{
			get { base.OnGet(); return _value; }
		}
		public static implicit operator T(Dependent<T> dependent)
		{
			return dependent.Value;
		}

		public override string VisualizerName(bool withValue)
		{
			string s = String.Format("Dependent<{0}>", typeof(T).Name);
			if (withValue)
				s += " = " + (_value == null ? "null" : _value.ToString());
			return s;
		}

		[Obsolete, EditorBrowsable(EditorBrowsableState.Never)]
		public Dependent DependentSentry
		{
			get { return this; }
		}
	}
}
