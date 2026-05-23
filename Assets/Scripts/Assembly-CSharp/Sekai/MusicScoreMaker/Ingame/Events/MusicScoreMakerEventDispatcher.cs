using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using CP;
using Sekai.MusicScoreMaker.Ingame.Utilities;
using UnityEngine;

namespace Sekai.MusicScoreMaker.Ingame.Events
{
	public class MusicScoreMakerEventDispatcher : SingletonMonoBehaviour<MusicScoreMakerEventDispatcher>
	{
		private struct UndoableEvent
		{
			public Action UndoAction
			{
			[CompilerGenerated]
			readonly get;
			[CompilerGenerated]
			set;
			}

			public Action RedoAction
			{
			[CompilerGenerated]
			readonly get;
			[CompilerGenerated]
			set;
			}

			public long FocusTicksAfterUndo
			{
			[CompilerGenerated]
			readonly get;
			[CompilerGenerated]
			set;
			}

			public long FocusTicksAfterRedo
			{
			[CompilerGenerated]
			readonly get;
			[CompilerGenerated]
			set;
			}
		}

		private const long FocusTicksNone = -1L;

		private readonly Dictionary<Type, List<Delegate>> _actionList;

		private readonly Dictionary<Type, Dictionary<Action, Delegate>> _eventDataActionMap;

		private readonly Stack<UndoableEvent> _undoStack;

		private readonly Stack<UndoableEvent> _redoStack;

		private static Stack<UndoableEvent> _savedUndoStack;

		private static Stack<UndoableEvent> _savedRedoStack;

		private bool _isInTestPlay;

		private float _lastOperationTime;

		public Dictionary<Type, List<Delegate>> ActionList
		{
			get
			{
				return _actionList;
			}
		}

		public Func<Action, bool> EditGuard
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		public bool CanUndo
		{
			get
			{
				return _undoStack.Count > 0;
			}
		}

		public bool CanRedo
		{
			get
			{
				return _redoStack.Count > 0;
			}
		}

		protected override void OnInitialize()
		{
			InitActionList();
		}

		public void Register<T, TResult>(Func<T, TResult> func) where T : MusicScoreMakerDispatcherEventBase
		{
			Register(typeof(T), func);
		}

		public void Register<T>(Action<T> action) where T : MusicScoreMakerDispatcherEventBase
		{
			Register(typeof(T), action);
		}

		public void RegisterWithEventData(string eventClassName, Action action)
		{
			Type eventType = Type.GetType(GetEventName(eventClassName));
			if (eventType == null)
			{
				return;
			}
			MethodInfo method = typeof(MusicScoreMakerEventDispatcher).GetMethod("RegisterWithEventDataCore", BindingFlags.Instance | BindingFlags.NonPublic);
			Delegate eventAction = (Delegate)method?.MakeGenericMethod(eventType).Invoke(this, new object[1] { action });
			if (eventAction == null)
			{
				return;
			}
			if (!_eventDataActionMap.TryGetValue(eventType, out Dictionary<Action, Delegate> actionMap))
			{
				actionMap = new Dictionary<Action, Delegate>();
				_eventDataActionMap[eventType] = actionMap;
			}
			actionMap[action] = eventAction;
		}

		public void Register(string eventClassName, Action action)
		{
			Type type = Type.GetType(GetEventName(eventClassName));
			if (type != null)
			{
				Register(type, action);
			}
		}

		public void Remove<T, TResult>(Func<T, TResult> func) where T : MusicScoreMakerDispatcherEventBase
		{
			Remove(typeof(T), func);
		}

		public void Remove<T>(Action<T> action) where T : MusicScoreMakerDispatcherEventBase
		{
			Remove(typeof(T), action);
		}

		public void RemoveWithEventData(string eventClassName, Action action)
		{
			Type type = Type.GetType(GetEventName(eventClassName));
			if (type == null)
			{
				return;
			}
			if (_eventDataActionMap.TryGetValue(type, out Dictionary<Action, Delegate> actionMap) && actionMap.TryGetValue(action, out Delegate eventAction))
			{
				Remove(type, eventAction);
				actionMap.Remove(action);
			}
		}

		public void Remove(string eventClassName, Action openAnimation)
		{
			Type type = Type.GetType(GetEventName(eventClassName));
			if (type != null)
			{
				Remove(type, openAnimation);
			}
		}

		public List<TResult> Publish<T, TResult>(T eventData) where T : MusicScoreMakerDispatcherEventBase
		{
			List<TResult> results = new List<TResult>();
			if (_actionList.TryGetValue(typeof(T), out List<Delegate> actions))
			{
				foreach (Delegate action in actions.ToArray())
				{
					if (action is Func<T, TResult> func)
					{
						results.Add(func(eventData));
					}
				}
			}
			return results;
		}

		public TResult PublishFirst<T, TResult>(T eventData) where T : MusicScoreMakerDispatcherEventBase
		{
			List<TResult> results = Publish<T, TResult>(eventData);
			return results.Count > 0 ? results[0] : default(TResult);
		}

		public void Publish<T>(T eventData) where T : MusicScoreMakerDispatcherEventBase
		{
			if (_actionList.TryGetValue(typeof(T), out List<Delegate> actions))
			{
				foreach (Delegate action in actions.ToArray())
				{
					if (action is Action<T> typedAction)
					{
						typedAction(eventData);
					}
					else if (action is Action simpleAction)
					{
						simpleAction();
					}
				}
			}
		}

		public void PublishToAssemblyQualifiedName(string assemblyQualifiedName)
		{
			Type type = Type.GetType(assemblyQualifiedName);
			if (type == null || !_actionList.TryGetValue(type, out List<Delegate> actions))
			{
				return;
			}
			object eventData = Activator.CreateInstance(type);
			PublishDynamic(type, eventData, actions);
		}

		private string GetEventName(string className)
		{
			return typeof(MusicScoreMakerDispatcherEventBase).Namespace + "." + className + ", Assembly-CSharp";
		}

		public void Publish(string className)
		{
			Type type = Type.GetType(GetEventName(className));
			if (type == null || !_actionList.TryGetValue(type, out List<Delegate> actions))
			{
				return;
			}
			object eventData = Activator.CreateInstance(type);
			PublishDynamic(type, eventData, actions);
		}

		private void DestroyAll()
		{
			InitActionList();
			_eventDataActionMap.Clear();
		}

		public void Dispose()
		{
			EditGuard = null;
			DestroyAll();
			ClearUndoRedoStack();
			Destroy(this);
		}

		private void InitActionList()
		{
			_actionList.Clear();
		}

		public void Undo()
		{
			if (_undoStack.Count == 0)
			{
				return;
			}
			UndoableEvent undoableEvent = _undoStack.Pop();
			_redoStack.Push(undoableEvent);
			undoableEvent.UndoAction?.Invoke();
			if (undoableEvent.FocusTicksAfterUndo != FocusTicksNone && !IsFocusTicksWithinVisibleRange(undoableEvent.FocusTicksAfterUndo))
			{
				MusicScoreMakerUtility.SetFocusTicks(undoableEvent.FocusTicksAfterUndo);
			}
			NotifyUndoRedoStackChanged();
		}

		public void Redo()
		{
			if (_redoStack.Count == 0)
			{
				return;
			}
			UndoableEvent undoableEvent = _redoStack.Pop();
			_undoStack.Push(undoableEvent);
			undoableEvent.RedoAction?.Invoke();
			if (undoableEvent.FocusTicksAfterRedo != FocusTicksNone && !IsFocusTicksWithinVisibleRange(undoableEvent.FocusTicksAfterRedo))
			{
				MusicScoreMakerUtility.SetFocusTicks(undoableEvent.FocusTicksAfterRedo);
			}
			NotifyUndoRedoStackChanged();
		}

		private static bool IsFocusTicksWithinVisibleRange(long focusTicks)
		{
			long previewStartTicks = MusicScoreMakerUtility.GetPreviewStartTicks();
			return previewStartTicks <= focusTicks && MusicScoreMakerUtility.GetPreviewEndTicks() >= focusTicks;
		}

		public void ClearUndoRedoStack()
		{
			_undoStack.Clear();
			_redoStack.Clear();
			NotifyUndoRedoStackChanged();
		}

		public void PushUndoableActionAndDoAction(Action undoAction, Action redoAction, long focusTicksAfterUndo = -1L, long focusTicksAfterRedo = -1L, bool skipEditGuard = false)
		{
			if (!skipEditGuard && EditGuard != null && EditGuard(() => PushUndoableActionAndDoActionCore(undoAction, redoAction, focusTicksAfterUndo, focusTicksAfterRedo)))
			{
				return;
			}
			PushUndoableActionAndDoActionCore(undoAction, redoAction, focusTicksAfterUndo, focusTicksAfterRedo);
		}

		private void PushUndoableActionAndDoActionCore(Action undoAction, Action redoAction, long focusTicksAfterUndo, long focusTicksAfterRedo)
		{
			_undoStack.Push(new UndoableEvent
			{
				UndoAction = undoAction,
				RedoAction = redoAction,
				FocusTicksAfterUndo = focusTicksAfterUndo,
				FocusTicksAfterRedo = focusTicksAfterRedo
			});
			_redoStack.Clear();
			int undoStackLimit = MusicScoreMakerSettingsManager.UndoStackLimit;
			if (_undoStack.Count > undoStackLimit)
			{
				UndoableEvent[] items = _undoStack.ToArray();
				_undoStack.Clear();
				for (int i = undoStackLimit - 1; i >= 0; i--)
				{
					_undoStack.Push(items[i]);
				}
			}
			redoAction();
			NotifyUndoRedoStackChanged();
			CheckAutoSave();
		}

		private void NotifyUndoRedoStackChanged()
		{
			Publish(new UndoRedoStackChangedEvent
			{
				CanUndo = CanUndo,
				CanRedo = CanRedo
			});
		}

		public void ResetAutoSaveTimer()
		{
			_lastOperationTime = Time.time;
		}

		private void CheckAutoSave()
		{
			if (!MusicScoreMakerSettingsManager.AutoSaveEnabled)
			{
				return;
			}
			if (Time.time - _lastOperationTime >= MusicScoreMakerSettingsManager.AutoSaveInterval)
			{
				_lastOperationTime = Time.time;
				Publish(new AutoSaveMusicScoreEvent());
			}
		}

		public void SaveUndoRedoStack()
		{
			_savedUndoStack = CloneStack(_undoStack);
			_savedRedoStack = CloneStack(_redoStack);
			_isInTestPlay = true;
		}

		public void RestoreUndoRedoStack()
		{
			if (_savedUndoStack != null)
			{
				RestoreStack(_undoStack, _savedUndoStack);
				_savedUndoStack = null;
			}
			if (_savedRedoStack != null)
			{
				RestoreStack(_redoStack, _savedRedoStack);
				_savedRedoStack = null;
			}
			_isInTestPlay = false;
		}

		public void ClearSavedUndoRedoStack()
		{
			_savedUndoStack = null;
			_savedRedoStack = null;
			_isInTestPlay = false;
		}

		public bool HasSavedUndoRedoStack()
		{
			return _savedUndoStack != null || _savedRedoStack != null;
		}

		public void ClearSavedUndoRedoStackIfNotTestPlay()
		{
			if (!_isInTestPlay)
			{
				ClearSavedUndoRedoStack();
			}
		}

		public MusicScoreMakerEventDispatcher()
		{
			_actionList = new Dictionary<Type, List<Delegate>>();
			_eventDataActionMap = new Dictionary<Type, Dictionary<Action, Delegate>>();
			_undoStack = new Stack<UndoableEvent>();
			_redoStack = new Stack<UndoableEvent>();
		}

		private void Register(Type type, Delegate action)
		{
			if (!_actionList.TryGetValue(type, out List<Delegate> actions))
			{
				actions = new List<Delegate>();
				_actionList[type] = actions;
			}
			actions.Add(action);
		}

		private Delegate RegisterWithEventDataCore<T>(Action action) where T : MusicScoreMakerDispatcherEventBase
		{
			Action<T> eventAction = delegate
			{
				action();
			};
			Register(eventAction);
			return eventAction;
		}

		private void Remove(Type type, Delegate action)
		{
			if (!_actionList.TryGetValue(type, out List<Delegate> actions))
			{
				return;
			}
			for (int i = 0; i < actions.Count; i++)
			{
				Delegate registeredAction = actions[i];
				if (registeredAction.Target == action.Target && registeredAction.Method == action.Method)
				{
					actions.RemoveAt(i);
					return;
				}
			}
		}

		private static void PublishDynamic(Type eventType, object eventData, List<Delegate> actions)
		{
			Type actionType = typeof(Action<>).MakeGenericType(eventType);
			foreach (Delegate action in actions.ToArray())
			{
				if (actionType.IsInstanceOfType(action))
				{
					action.DynamicInvoke(eventData);
				}
			}
		}

		private static Stack<UndoableEvent> CloneStack(Stack<UndoableEvent> source)
		{
			Stack<UndoableEvent> clone = new Stack<UndoableEvent>(source.Count);
			UndoableEvent[] items = source.ToArray();
			for (int i = items.Length - 1; i >= 0; i--)
			{
				clone.Push(items[i]);
			}
			return clone;
		}

		private static void RestoreStack(Stack<UndoableEvent> target, Stack<UndoableEvent> source)
		{
			target.Clear();
			UndoableEvent[] items = source.ToArray();
			for (int i = items.Length - 1; i >= 0; i--)
			{
				target.Push(items[i]);
			}
		}
	}
}
