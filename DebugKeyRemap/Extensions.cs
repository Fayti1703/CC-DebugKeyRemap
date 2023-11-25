namespace DebugKeyRemap;

public static class Extensions {
	public static IEnumerable<Indexed<T>> WithIndex<T>(this IEnumerable<T> collection) {
		return collection.Select((value, index) => new Indexed<T>(index, value));
	}

}

public readonly struct Indexed<T> {
	public readonly int index;
	public readonly T value;

	public Indexed(int index, T value) {
		this.index = index;
		this.value = value;
	}

	public void Deconstruct(out int index, out T value) {
		index = this.index;
		value = this.value;
	}
}
