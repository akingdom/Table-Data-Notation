//  ResourceRecord.cs
//
//  Copyright © 2017 BNotro Software Development Pty Ltd. All rights reserved.
//
namespace BNotro.Core.resourcing
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

	[System.Serializable]
    
	/// <summary>
	/// A record representing a row of key-values from a resource map file.
	/// See ResourceLoader class for related methods and details.
    /// </summary>
	// was: public class ResourceData 
	public class ResourceRecord 
    {
        public ResourceRecord() { }
        
		public int pageIndex;

        // TODO: When this class is needed to be serialised, 
        //  a C# extension is needed to (de)serialise Dictionaries, which is not supported by default.

        #region x ==================
        /// <summary>
        /// Header key and column value for each field in this Record (row).
        /// </summary>
        public Dictionary<string, ResourceKeyValue> fields = new Dictionary<string, ResourceKeyValue>();

        /// <summary>
        /// Adds a field key-value to the fields for this record/row.
        /// </summary>
        /// <param name="header">Name of the header key.</param>
        /// <param name="field">Value of the field.</param>
        public void AddField(string header, string field)
		{
			if (!fields.ContainsKey(header))  // ignore columns with duplicate headers
			{
				fields.Add(header, new ResourceKeyValue(header,field));  // includes reverse-lookup header name
			}
		}
		#endregion

		#region ORDINARY COLUMN HEADERS ====================
		/// <summary>
		/// Returns the value for the specified key, if defined for this row
		/// </summary>
		public string Value(string key) { ResourceKeyValue r; if (fields.TryGetValue(key, out r)) return r.value; else return string.Empty; }

        public bool ValueBoolean(string key)
        {
            ResourceKeyValue r;
            if (fields.TryGetValue(key, out r))
            {
                switch (r.value.ToUpper())
                {
                    case "TRUE": return true;
                    case "YES": return true;
                    //case "-1": return true;
                    //case "1": return true;
                    default: return false;
                }
            }
            return default(bool);
        }
        public float ValueFloat(string key) 
        {
            ResourceKeyValue r;
            if (fields.TryGetValue(key, out r))
            {
                float f;
                if(float.TryParse(r.value,out f))
                    return f;
            }
            return float.NaN; 
        }
        public int ValueInteger(string key)
        {
            ResourceKeyValue r;
            if (fields.TryGetValue(key, out r))
            {
                int i;
                if (int.TryParse(r.value, out i))
                    return i;
            }
            return default(int);
        }
        public Color ValueColorRGB(string key)
        {
            ResourceKeyValue r;
            if (fields.TryGetValue(key, out r))
            {
                string s = r.value;
                if (s.Length < 6)
                    s = "000000".Substring(1,6-s.Length) + s;  // pad missing zeros

                Color color;
                if (ColorUtility.TryParseHtmlString("#" + r.value, out color))
                    return color;
            }
            return default(Color);
        }

        /// <summary>
        /// Tries to get the value corresponding to a particular key.
        /// </summary>
        /// <returns><c>true</c>, if the key-value existed, <c>false</c> otherwise.</returns>
        /// <param name="key">Key.</param>
        /// <param name="value">Result.</param>
        public bool TryGetValue(string key, out string value) { 
			ResourceKeyValue r; 
			if (fields.TryGetValue(key, out r)) 
			{
				value= r.value;
				return true;
			}
			else 
			{
				value=string.Empty;
				return false;
			}
		}
		#endregion

		#region SPECIAL COLUMN HEADERS ====================

		/// <summary>
		/// The kind of this record, if defined for this row, or empty string if not.
		/// </summary>
		public string kind= string.Empty;

		/// <summary>
		/// The table name for this record, if defined for this row, or empty string if not. 
		/// </summary>
		public string table= string.Empty;

		/// <summary>
		/// The name identifier for this record, if defined for this row, or empty string if not.
		/// </summary>
		public string name = string.Empty;

		/// <summary>
		/// The layout path for this record, if defined for this row, or empty string if not.
		/// </summary>
		public string layoutPath = string.Empty;

		/// <summary>
		/// Returns true if the key '[Layout]' is defined for this row
		/// </summary>
		public bool isLayout() { return kind == "Layout"; }
		#endregion

		#region OTHER RECORDS ===================
		[System.NonSerialized]
		/// <summary>
		/// A collection of all data found in the resource map file, 
		/// + all row header/field values.
		/// + all tables.
		/// (including this record)
		/// </summary>
		public ResourceMap map;
		#endregion

		#region RELATED RECORDS BY TABLE KIND ====================

		[UnityEngine.Serialization.FormerlySerializedAs("kind")]
		/// KIND -- contains all records of the same kind as this record
		/// 
		/// A list of rows in the source map that have resource values and are of the same 'kind'. (See kindKeys for a list of recognised kinds.)
		[System.NonSerialized]
		public ResourceTable recordsByTable;

		///
		/// Returns a random record with the specified table 
		public ResourceRecord random(string table) 
		{
			ResourceTable rec; 
			if (!map.tables.TryGetValue(table, out rec)) 
			throw new UnityException("Table '" + table + "' not found"); 
			else 
				if (rec.Count == 0) 
				throw new UnityException("Table '" + table + "' has no values"); 
				else 
					return rec[Random.Range(0, rec.Count)]; 
		}

		#endregion

		/// <summary>
		/// (Can be used for debugging, as required.)
		/// </summary>
		public string debug;

		// TRANSFORM
        // Transform this class to a string (useful for debugging)
        public override string ToString() { string s = string.Empty; foreach (ResourceKeyValue r in fields.Values) s += "{" + r + "} "; return s; }

        // CLEANUP
        void OnDestroy()
        {
			map = null;
			fields.Clear();
			fields= null;
			recordsByTable = null;
        }

    }

}

