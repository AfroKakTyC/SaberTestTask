using System;

class ListRand
{
	public ListNode Head;
	public ListNode Tail;
	public int Count;

	public void Serialize(FileStream s)
	{
		if (s == null)
		{
			throw new ArgumentNullException("Parameter cannot be null", nameof(s));
		}

		try
		{
			using (StreamWriter streamWriter = new StreamWriter(s))
			{
				Dictionary<ListNode, int> nodesDictionaryWithIndexes = ListUtilites.CreateDictionaryFromList(this);

				//streamWriter.Write(Count + "\n");
				streamWriter.WriteLine(Count);
				ListNode currentNode = Head;
				for (int i = 0; i < Count; i++)
				{
					int randomNodeIndex = -1;
					try
					{
						randomNodeIndex = nodesDictionaryWithIndexes[currentNode.Rand];
					}
					catch (Exception ex)
					{
						Console.WriteLine($"Node with index {i} is pointing to random node, that not exist in list.\n" + ex.Message);
					}

					//streamWriter.Write(currentNode.Data + "\n");
					//streamWriter.Write(randomNodeIndex + "\n");
					streamWriter.WriteLine(currentNode.Data);
					streamWriter.WriteLine(randomNodeIndex);
					currentNode = currentNode.Next;
				}
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine("File open error.\n" + ex.Message);
		}
	}

	public void Deserialize(FileStream s)
	{
		if (s == null)
		{
			throw new ArgumentNullException("Parameter cannot be null", nameof(s));
		}

		try
		{
			using (StreamReader streamReader = new StreamReader(s))
			{
				int nodesAmount = 0;
				try
				{
					nodesAmount = int.Parse(streamReader.ReadLine());
				}
				catch (Exception ex)
				{
					Console.WriteLine($"File parse error on line 0.\n{ex.Message}");
				}

				Count = nodesAmount;

				if (nodesAmount == 0)
				{
					Head = null;
					Tail = null;
					return;
				}

				ListNode[] nodesInOrder = new ListNode[Count];
				int[] randomNodeIndexesInOrder = new int[Count];
				ListNode previousNode = null;
				ListNode currentNode = null;
				for (int i = 0; i < nodesAmount; i++)
				{
					String nodeData = streamReader.ReadLine();
					int randomNodeIndex = 0;
					try
					{
						randomNodeIndex = int.Parse(streamReader.ReadLine());
					}
					catch (Exception ex)
					{
						Console.WriteLine($"File parse error on line {i + 1}.\n{ex.Message}");
					}

					currentNode = new ListNode();

					if (i == 0)
					{
						Head = currentNode;
					}

					if (previousNode != null)
					{
						previousNode.Next = currentNode;
					}

					currentNode.Data = nodeData;
					currentNode.Prev = previousNode;
					previousNode = currentNode;

					if (i == nodesAmount - 1)
					{
						currentNode.Next = null;
						Tail = currentNode;
					}

					nodesInOrder[i] = currentNode;
					randomNodeIndexesInOrder[i] = randomNodeIndex;
				}

				ListUtilites.RecreateLinksToRandomListNodes(nodesInOrder, randomNodeIndexesInOrder);
				//for (int i = 0; i < nodesInOrder.Length; i++)
				//{
				//	try
				//	{
				//		nodesInOrder[i].Rand = nodesInOrder[randomNodeIndexesInOrder[i]];
				//	}
				//	catch (Exception ex)
				//	{
				//		Console.WriteLine($"Random element for node with index {i} is invalid.\n{ex.Message}");
				//	}
				//}
			}
		}
		catch(Exception ex)
		{
			Console.WriteLine("File open error.\n" + ex.Message);
		}
	}
}
