using FS25Gateway.Domain.Models;

namespace FS25Gateway.Application.StateStore;

/// <summary>Provides thread-safe access to the current farm aggregate.</summary>
public sealed class StateStore
{
	private readonly object _sync = new();
	private readonly FarmState _state;

	/// <summary>Raised after an update has been committed to the aggregate.</summary>
	public event Action<IReadOnlyList<FarmStateChange>>? Changed;

	/// <summary>Initializes an empty state store.</summary>
	public StateStore()
		: this(new FarmState())
	{
	}

	/// <summary>Initializes the state store with an existing aggregate.</summary>
	/// <param name="initialState">The initial farm state.</param>
	public StateStore(FarmState initialState)
	{
		_state = initialState ?? throw new ArgumentNullException(nameof(initialState));
	}

	/// <summary>Reads the aggregate while holding the store lock.</summary>
	/// <typeparam name="TResult">The result type produced by the read operation.</typeparam>
	/// <param name="readOperation">The operation to execute against the current state.</param>
	/// <returns>The result produced by the read operation.</returns>
	public TResult Read<TResult>(Func<FarmState, TResult> readOperation)
	{
		ArgumentNullException.ThrowIfNull(readOperation);

		lock (_sync)
		{
			return readOperation(_state);
		}
	}

	/// <summary>Updates the aggregate while holding the store lock.</summary>
	/// <param name="updateOperation">The operation that mutates the current state.</param>
	public void Update(Action<FarmState> updateOperation)
	{
		Update(updateOperation, Array.Empty<FarmStateChange>());
	}

	/// <summary>Updates the aggregate and publishes the typed changes produced by the update.</summary>
	/// <param name="updateOperation">The operation that mutates the current state.</param>
	/// <param name="changes">The typed changes produced while applying the operation.</param>
	public void Update(Action<FarmState> updateOperation, ICollection<FarmStateChange> changes)
	{
		ArgumentNullException.ThrowIfNull(updateOperation);
		ArgumentNullException.ThrowIfNull(changes);

		lock (_sync)
		{
			updateOperation(_state);
			_state.UpdatedAt = DateTime.UtcNow;
		}

		if (changes.Count > 0)
		{
			Changed?.Invoke(changes.ToArray());
		}
	}
}
