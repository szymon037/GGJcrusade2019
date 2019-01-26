public class Pair<T, U> {
	public T item1;
	public U item2;

	public Pair() {
		item1 = default(T);
		item2 = default(U);
	}

	public Pair(T first, U second) {
		item1 = first;
		item2 = second;
	}
}