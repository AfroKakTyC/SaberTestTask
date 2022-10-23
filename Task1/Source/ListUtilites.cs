using System;
using System.Collections.Generic;

static class ListUtilites
{
	public static Dictionary<ListNode, int> CreateDictionaryFromList(ListRand list)
	{
		Dictionary<ListNode, int> nodesDictionary = new Dictionary<ListNode, int>();
		ListNode currentNode = list.Head;
		for (int i = 0; i < list.Count; i++)
		{
			if (!nodesDictionary.ContainsKey(currentNode))
			{
				nodesDictionary.Add(currentNode, i);
			}
			currentNode = currentNode.Next;
		}

		return nodesDictionary;
	}

	public static void RecreateLinksToRandomListNodes(ListNode[] nodesInOrder, int[] randomNodeIndexesInOrder)
	{
		for (int i = 0; i < nodesInOrder.Length; i++)
		{
			try
			{
				nodesInOrder[i].Rand = nodesInOrder[randomNodeIndexesInOrder[i]];
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Random element for node with index {i} is invalid.\n{ex.Message}");
			}
		}
	}
}
