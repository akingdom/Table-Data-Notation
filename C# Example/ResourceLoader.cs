//  ResourceLoader.cs
//
//  Copyright © 2017 BNotro Software Development Pty Ltd. All rights reserved.
//
namespace BNotro.Core.resourcing
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    // The ResourceLoader class populates resource lists from file (or elsewhere)
    // revision 3.1
    //
    // USAGE: var manifestMap = ResourceLoader.Open( resourceMapFile );
    // foreach (ResourceRecord item in manifestMap.tables["chroma"])
    // {
    //     ...
    // }
    public static class ResourceLoader
    {
        // Ditto mark
        // Two full-stops (ASCII 0x2E 0x2E) 
        // Chosen as it is friendly with Microsoft Excel, friendly with international keyboards and fairly fast to type.
        const string dittomark = "..";

        static Dictionary<string, ResourceMap> theLists = new Dictionary<string, ResourceMap>();

        #region LOAD

        /// <summary>
        /// Close the specified list.
        /// </summary>
        /// <param name="listfile">Listfile.</param>
        public static void Close(string listfile)
        {
            theLists.Remove(listfile);
        }

        /// <summary>
        /// Load data from specified tab-delimited text file into a list of dictionaries.
        /// Once loaded it is cached in memory.
        /// </summary>
        public static ResourceMap Open(string listfile)
        {
            // result
            ResourceMap result;
            if (theLists.TryGetValue(listfile, out result))
                return result;


            TextAsset dataReader = Resources.Load<TextAsset>(listfile);
            if (dataReader == null)
            {
                throw new UnityException("Resource '" + listfile + "' not found within any Resources folder in the project.");
            }

            result = Parse(dataReader.text);
            result.filePath = listfile;
            theLists.Add(listfile, result);

            return result;
        }
        /// <summary>
        /// Parse the specified text.
        /// </summary>
        /// <returns>The parse.</returns>
        /// <param name="text">Text.</param>
        public static ResourceMap Parse(string text)
        {
            // The text contains any number of 2D tables of data.
            // Each table column is delimited by the tab ('\t' 0x09) character.
            // Each table begins with a mandatory header row.
            // Header rows contain keys within square brackets: [Example Header 1]
            // Header rows must start with a left square bracket '[' as the first character.
            // Data rows follow each header row, with corresponding columns.
            // Row data can be ragged (missing unneeded columns are assigned an empty value).  TODO Verify this is correct.
            // Column names (headings) must be at the start of the file (after any comment lines)
            // Header keys [Table], [Name] and [Kind] are reserved for specific purposes.
            // Optional header key [Table] names the table for reference. It is usually a column to the left.
            // Optional header key [Name] sets the record identifier.
            // Optional header key [Kind] sets the record type. 
            // [Kind] value 'List' defines a list and enables extended ResourceLoader functions.
            // [Kind] value 'Layout' defines a layout and enables extended ResourceLoader functions.
            // Our ditto mark, ('..' 0x2E 0x2E). may be used to repeat a value from a preceeding row.
            // Ditto does not work for header rows or the row immediately following them.
            // File lines with no tab characters are ignored.
            // File lines with no tab character are ignored.

            ResourceMap result = new ResourceMap();

            // temp
            string[] lines = text.Replace("\r\n", "\n").Replace("\r", "\n").Split('\n');

            ResourceRecord rowRecord;
            List<string> indexKeys = new List<string>();


            char fieldDelimiter = '\t';
            string[] records;
            char firstchar;
            char lastchar;
            int column;
            int tableRowCount = 0;
            foreach (string line in lines)
            {
                if (line != string.Empty && line[0] == '[')
                {
                    int hIndex = line.IndexOf(']', 1) + 1;
                    int index = line.IndexOf('[', hIndex + 1);
                    int matchLength = index - hIndex;
                    fieldDelimiter = matchLength == 1 ? line[hIndex] : '\t';
                }

                //Debug.Log( line+line.IndexOf('[') );
                records = line.Split(fieldDelimiter);

                if (line.Length != records.Length)  // if this line is not empty (and is not only tab characters). 
                {
                    // skip lines containing no values

                    // Row of Header Keys?

                    if (line != string.Empty && line[0] == '[')  // check first char
                    {
                        // Header key rows starts with with a '[', e.g. [PATH]...
                        //Debug.Log("KEY<");
                        indexKeys = new List<string>();  // clear any existing keys when we reach a new key row
                        tableRowCount = 1;  // reset
                        foreach (string key in records)
                        {
                            if (key != string.Empty)
                            {
                                firstchar = key[0];
                                lastchar = key[key.Length - 1];
                                if (firstchar == '[' && lastchar == ']')
                                {
                                    string name = key.Substring(1, key.Length - 2);  // length less square bracket chars.
                                    indexKeys.Add(name);
                                }
                                else
                                {
                                    indexKeys.Add(string.Empty);  // bad entry -- ignore, but include something to space out the columns
                                }
                                //Debug.Log("KEY: "+key);
                            }
                        }
                    }
                    else
                    {
                        // Row of Values corresponding to the above Header Keys

                        //Debug.Log("VALUE<");
                        rowRecord = new ResourceRecord();
                        column = -1;
                        tableRowCount++;
                        foreach (string value in records)
                        {
                            string assignvalue = value;
                            column++;
                            if (column < indexKeys.Count)  // ignore extra columns that have no key
                            {
                                string header = indexKeys[column];

                                if (dittomark == value)
                                {
                                    if (tableRowCount <= 2)
                                    {
                                        assignvalue = string.Empty;
                                    }
                                    else
                                    {
                                        ResourceRecord previousRow = result.sourcerows[result.sourcerows.Count - 1];  // (last record -- this record hasn't been added yet)
                                        assignvalue = previousRow.Value(header);  // repeat previous value
                                    }
                                }

                                rowRecord.AddField(header, assignvalue);
                                //Debug.Log("DATA("+column+"): "+header+", "+assignvalue);

                                // Reserved Header Keys...
                                switch (header)
                                {
                                    case "Table": rowRecord.table = assignvalue; break;
                                    case "List":
                                        if (rowRecord.table == string.Empty)
                                        {
                                            rowRecord.table = assignvalue;
                                        }
                                        break;
                                    case "Kind": rowRecord.kind = assignvalue; break;
                                    case "Name": rowRecord.name = assignvalue; break;
                                    case "Layout": rowRecord.layoutPath = assignvalue; break;
                                }
                            }
                        }

                        // Map
                        result.sourcerows.Add(rowRecord);
                        rowRecord.map = result;

                        // Table
                        ResourceTable recordsByTable;
                        if (!result.tables.TryGetValue(rowRecord.table, out recordsByTable))
                        {
                            // kind not found -- add it
                            recordsByTable = new ResourceTable();
                            recordsByTable.name = rowRecord.table;
                            recordsByTable.map = rowRecord.map;
                            result.tables.Add(rowRecord.table, recordsByTable);
                        }
                        recordsByTable.Add(rowRecord);  // add current record to its kind
                        rowRecord.recordsByTable = recordsByTable;
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// Returns a list of substrings in text that are delimited by the delimiter.
        /// </summary>
        //  This is slower than String.Split(..) but allows a multi-character delimiter
        static List<string> Split(string text, string delimiter)
        {
            // e.g. "012,4,67,9" @3,5,8 --> "012","4","67","9"

            List<string> result = new List<string>();

            int index = -delimiter.Length;
            int startIndex = 0;
            while (true)
            {
                index = text.IndexOf(delimiter, index + delimiter.Length, System.StringComparison.Ordinal);
                if (index == -1)
                {
                    // add final entry (or whole string if no delimiters were found)
                    result.Add(text.Substring(startIndex, text.Length - startIndex));
                    break;
                }
                result.Add(text.Substring(startIndex, index - startIndex));
                startIndex = index + delimiter.Length;
            }
            return result;
        }


        #endregion LOAD

        
    }
}
