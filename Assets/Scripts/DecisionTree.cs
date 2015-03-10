using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System;

/// <summary>
/// A basic decision tree for guiding what characters are doing
/// </summary>
public class DecisionTree {

	private Node root;

	public Node Root {
		get {
			return root;
		}
	}

	/// <summary>
	/// Initializes the decision tree using the data from the file
	/// at the given path
	/// </summary>
	/// <param name="file">path to the data file</param>
	public DecisionTree(string file)
	{
		Load (file);
	}

	/// <summary>
	/// Loads the tree from the specified file
	/// </summary>
	/// <param name="file">path to the file to load from</param>
	private void Load(string file)
	{
		try
		{
			using (StreamReader reader = new StreamReader(file))
			{
				root = RecLoad (reader);
			}
		}
		catch (Exception)
		{
			Debug.Log("Decision tree file doesn't exist");
		}
	}

	private Node RecLoad(StreamReader reader)
	{
		try {
			string inBuffer = reader.ReadLine ();                      // Next line from file
			string nodeType = inBuffer.Substring (0, 1);                 // First letter of line
			string phrase = inBuffer.Substring (1, inBuffer.Length - 1); // Rest of the line
			Node tree = new Node (phrase);                               // Make a node for phrase
			
			if (nodeType.Equals ("I")) {                   // Interior node => recurse
				tree.YesPtr = RecLoad (reader);
				tree.NoPtr = RecLoad (reader);
			}
			return tree;                                // Return the node we just built
		}
		catch (Exception) {
			Debug.Log("The file is empty, so I don't know anything.");
			return null;
		}
	}

	/* Node class - Nodes have a string and 2 child Nodes.
	 * The children are either both null (a leaf node => string is a name)
	 * or they're both non-null (an interior node => string is a phrase)
	 */
	public class Node
	{
		private string data = null;	// Used for demo to input a string to ask the user
		private Node noPtr = null;  // Followed if the Test() fails
		private Node yesPtr = null;	// Followed if the Test() succeeds
		
		// Getters and setters
		public string Data
		{
			get { return data; }
			set { data = value; }
		}
		
		public Node NoPtr
		{
			get { return noPtr; }
			set { noPtr = value; }
		}

		public bool IsLeaf() {
			if (noPtr == null && yesPtr == null) return true; return false;
		}

		public Node YesPtr
		{
			get { return yesPtr; }
			set { yesPtr = value; }
		}
		
		public Node (string s)      // data string passed into constructor
		{
			data = s;
			noPtr = null;          // New nodes are leaf nodes
			yesPtr = null;
		}
	}
}
