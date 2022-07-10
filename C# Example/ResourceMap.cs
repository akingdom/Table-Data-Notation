//  ResourceMap.cs
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
	/// A set representing tables from a resource map file.
	/// See ResourceLoader class for support methods and details.
	/// See ResourceTable  class for table data.
	/// See ResourceRecord class for individual row data.
	/// </summary>
	public class ResourceMap
	{
		public ResourceMap ()
		{
		}

		public string filePath;


		/// <summary>
		/// A dictionary  of tables
		/// A dictionary lookup (by table name) of all tables named in the source map
		///  (tables with a [Table] column).
		/// All other records are recorded with the key string.Empty (empty string).
		/// </summary>
		[System.NonSerialized]
		public Dictionary<string,ResourceTable> tables = new Dictionary<string, ResourceTable>();

		[System.NonSerialized]
		/// <summary>
		/// A list of all rows (of header/field values) found in the source map
		/// </summary>
		public ResourceTable sourcerows = new ResourceTable();  // this was originally returned by ResourceLoader.Open() method as List<ResourceRecord>.

		/// <summary>
		/// (Can be used for debugging, as required.)
		/// </summary>
		//public string debug;

		// TRANSFORM
		// Transform this class to a string (useful for debugging)
		public override string ToString() 
		{
			string s = "filePath: \"" + filePath 
				+ "\"\nsourcerow Count = " + sourcerows.Count.ToString() 
				+ "\ntables ("+tables.Count.ToString()+"): {\n"; 
			int i = 0;
			foreach (string k in tables.Keys) 
			{
				ResourceTable table = tables[k];
				s += "index: "+i.ToString() + "\n"
					+"key: "+k+"\n" 
					+table.ToString()
					+"\n";
				i++;
			}
			s+= "}\n\n ";
			return s;
		}

		// CLEANUP
		void OnDestroy()
		{
			tables.Clear();
			tables= null;
			sourcerows = null; 
		}
	}
}
