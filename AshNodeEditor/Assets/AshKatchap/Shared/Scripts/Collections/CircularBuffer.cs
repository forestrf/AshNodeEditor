using System;
using System.Text;
using UnityEngine;

namespace Ashkatchap.Shared.Collections {
	public class CircularBuffer<T> {
		const int DEFAULT_CAPACITY = 16;
		const int DEFAULT_INCREMENT = 16;

		/// <summary>
		/// Don't touch this unless you know what you are doing
		/// </summary>
		public T[] array; // from left to right, oldest to newest
		private int increment;

		/// <summary>
		/// Don't modify this unless you know what you are doing
		/// </summary>
		public int tailIndex = 0;

		/// <summary>
		/// Don't modify this unless you know what you are doing
		/// </summary>
		public int headIndex = 0;

		/// <summary>
		/// Don't modify this unless you know what you are doing
		/// </summary>
		public int Count = 0;


		/// <summary>
		/// Don't use negative numbers. Supports cyclic indexing
		/// </summary>
		public T this[int index] {
			get { return array[(tailIndex + index) % array.Length]; }
			set { array[(tailIndex + index) % array.Length] = value; }
		}

		/// <param name="index">>=0</param>
		public int GetInternalArrayIndex(int index) {
			int i = tailIndex + index;
			return i >= array.Length ? i % array.Length : i;
		}

		public CircularBuffer(int initialCapacity, int increment) {
			array = new T[initialCapacity > 0 ? initialCapacity : DEFAULT_CAPACITY];
			this.increment = increment >= 0 ? increment : DEFAULT_INCREMENT;
			NormalEnumerator = new NormalEnumeratorImp(this);
			ReversedEnumerator = new ReversedEnumeratorImp(this);
		}
		public CircularBuffer() : this(DEFAULT_CAPACITY, DEFAULT_INCREMENT) { }

		/// <summary>
		/// Allocate one space, either by increasing the array or dequeuing an item
		/// </summary>
		private void FreeOne() {
			if (Count == array.Length) {
				if (increment > 0) {
					// Need a bigger array
					var tmp = new T[array.Length + increment];
					Array.Copy(array, 0, tmp, 0, headIndex);
					int newStartIndex = tmp.Length - (array.Length - tailIndex);
					Array.Copy(array, tailIndex, tmp, newStartIndex, array.Length - tailIndex);
					tailIndex = newStartIndex;
					array = tmp;
				}
				else {
					Dequeue();
				}
			}
		}

		public void Enqueue(T element) {
			Enqueue(ref element);
		}

		public void Enqueue(ref T element) {
			FreeOne();

			array[headIndex] = element;
			headIndex = (headIndex + 1) % array.Length;
			Count++;
		}
		/// <summary>
		/// Add this element to the head as the next item to dequeue
		/// </summary>
		public void Push(T element) {
			FreeOne();

			tailIndex = Mod(tailIndex - 1, array.Length);
			array[tailIndex] = element;
			Count++;
		}
		public T ExtractLastQueued() {
			if (Count > 0) {
				Count--;
				headIndex = Mod(headIndex - 1, array.Length);
				var toReturn = array[headIndex];
				array[headIndex] = default(T);
				return toReturn;
			}
			else {
				Debug.LogError("Trying to ExtractLastQueued without elements in the collection");
				return default(T);
			}
		}
		public T PeekLastQueued() {
			if (Count > 0) {
				int index = Mod(headIndex - 1, array.Length);
				return array[index];
			}
			else {
				Debug.LogError("Trying to ExtractLastQueued without elements in the collection");
				return default(T);
			}
		}
		public T Dequeue() {
			if (Count > 0) {
				Count--;
				var toReturn = array[tailIndex];
				array[tailIndex] = default(T);
				tailIndex = (tailIndex + 1) % array.Length;
				return toReturn;
			}
			else {
				Debug.LogError("Trying to Dequeue without elements in the collection");
				return default(T);
			}
		}
		public T PeekDequeue() {
			if (Count > 0) {
				return array[tailIndex];
			}
			else {
				Debug.LogError("Trying to PeekDequeue without elements in the collection");
				return default(T);
			}
		}

		/// <param name="fastMode">Only change the length without cleaning the inside of the internal array</param>
		public void Clear(bool fastMode = false) {
			if (!fastMode) {
				Array.Clear(array, 0, array.Length);
			}
			Count = 0;
			tailIndex = 0;
			headIndex = 0;
		}

		/// <summary>
		/// Use only if x may be negative, otherwise use %
		/// </summary>
		private static int Mod(int x, int m) {
			return (x % m + m) % m;
		}

		public override string ToString() {
			StringBuilder s = new StringBuilder();
			s.Append("Size: " + Count + " | ");
			for (int i = 0; i < Count; i++) {
				s.Append(array[i].ToString());
				if (i != Count - 1) s.Append(", ");
			}
			return s.ToString();
		}

		public NormalEnumeratorImp NormalEnumerator;
		public ReversedEnumeratorImp ReversedEnumerator;


		public class NormalEnumeratorImp {
			private readonly CircularBuffer<T> queue;
			public NormalEnumeratorImp(CircularBuffer<T> queue) {
				this.queue = queue;
			}
			public Enumerator GetEnumerator() {
				return new Enumerator(queue);
			}

			public struct Enumerator {
				private readonly CircularBuffer<T> queue;
				private int index;

				public Enumerator(CircularBuffer<T> queue) {
					this.queue = queue;
					index = -1;
				}

				public T Current {
					get { return queue[index]; }
				}

				public bool MoveNext() {
					index++;
					return index < queue.Count;
				}

				public void Reset() {
					index = -1;
				}
			}
		}
		public class ReversedEnumeratorImp {
			private readonly CircularBuffer<T> queue;
			public ReversedEnumeratorImp(CircularBuffer<T> queue) {
				this.queue = queue;
			}

			public Enumerator GetEnumerator() {
				return new Enumerator(queue);
			}

			public struct Enumerator {
				private readonly CircularBuffer<T> queue;
				private int index;

				public Enumerator(CircularBuffer<T> queue) {
					this.queue = queue;
					index = queue.Count;
				}

				public T Current {
					get { return queue[index]; }
				}

				public bool MoveNext() {
					index--;
					return index >= 0;
				}

				public void Reset() {
					index = queue.Count;
				}
			}
		}
	}
}
